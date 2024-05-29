using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace WebSocketSharp.Net
{
	internal sealed class EndPointListener
	{
		private List<HttpListenerPrefix> list_0;

		private static readonly string string_0;

		private IPEndPoint ipendPoint_0;

		private Dictionary<HttpListenerPrefix, HttpListener> dictionary_0;

		private bool bool_0;

		private Socket socket_0;

		private ServerSslConfiguration serverSslConfiguration_0;

		private List<HttpListenerPrefix> list_1;

		private Dictionary<HttpConnection, HttpConnection> dictionary_1;

		private object object_0;

		public IPAddress IPAddress_0
		{
			get
			{
				return ipendPoint_0.Address;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return bool_0;
			}
		}

		public int Int32_0
		{
			get
			{
				return ipendPoint_0.Port;
			}
		}

		public ServerSslConfiguration ServerSslConfiguration_0
		{
			get
			{
				return serverSslConfiguration_0;
			}
		}

		internal EndPointListener(IPAddress ipaddress_0, int int_0, bool bool_1, string string_1, ServerSslConfiguration serverSslConfiguration_1, bool bool_2)
		{
			if (bool_1)
			{
				X509Certificate2 certificate = getCertificate(int_0, string_1, serverSslConfiguration_1.X509Certificate2_0);
				if (certificate == null)
				{
					throw new ArgumentException("No server certificate could be found.");
				}
				bool_0 = bool_1;
				serverSslConfiguration_0 = serverSslConfiguration_1;
				serverSslConfiguration_0.X509Certificate2_0 = certificate;
			}
			dictionary_0 = new Dictionary<HttpListenerPrefix, HttpListener>();
			dictionary_1 = new Dictionary<HttpConnection, HttpConnection>();
			object_0 = ((ICollection)dictionary_1).SyncRoot;
			socket_0 = new Socket(ipaddress_0.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			if (bool_2)
			{
				socket_0.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			}
			ipendPoint_0 = new IPEndPoint(ipaddress_0, int_0);
			socket_0.Bind(ipendPoint_0);
			socket_0.Listen(500);
			SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
			socketAsyncEventArgs.UserToken = this;
			socketAsyncEventArgs.Completed += onAccept;
			if (!socket_0.AcceptAsync(socketAsyncEventArgs))
			{
				onAccept(this, socketAsyncEventArgs);
			}
		}

		static EndPointListener()
		{
			string_0 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		}

		private static void addSpecial(List<HttpListenerPrefix> list_2, HttpListenerPrefix httpListenerPrefix_0)
		{
			string string_ = httpListenerPrefix_0.String_1;
			foreach (HttpListenerPrefix item in list_2)
			{
				if (item.String_1 == string_)
				{
					throw new HttpListenerException(400, "The prefix is already in use.");
				}
			}
			list_2.Add(httpListenerPrefix_0);
		}

		private void checkIfRemove()
		{
			if (dictionary_0.Count > 0)
			{
				return;
			}
			List<HttpListenerPrefix> list = list_1;
			if (list == null || list.Count <= 0)
			{
				list = list_0;
				if (list == null || list.Count <= 0)
				{
					EndPointManager.RemoveEndPoint(this);
				}
			}
		}

		private static RSACryptoServiceProvider createRSAFromFile(string string_1)
		{
			byte[] array = null;
			using (FileStream fileStream = File.Open(string_1, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
			}
			RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
			rSACryptoServiceProvider.ImportCspBlob(array);
			return rSACryptoServiceProvider;
		}

		private static X509Certificate2 getCertificate(int int_0, string string_1, X509Certificate2 x509Certificate2_0)
		{
			if (string_1 == null || string_1.Length == 0)
			{
				string_1 = string_0;
			}
			try
			{
				string text = Path.Combine(string_1, string.Format("{0}.cer", int_0));
				string text2 = Path.Combine(string_1, string.Format("{0}.key", int_0));
				if (File.Exists(text) && File.Exists(text2))
				{
					X509Certificate2 x509Certificate = new X509Certificate2(text);
					x509Certificate.PrivateKey = createRSAFromFile(text2);
					return x509Certificate;
				}
			}
			catch
			{
			}
			return x509Certificate2_0;
		}

		private static HttpListener matchFromList(string string_1, string string_2, List<HttpListenerPrefix> list_2, out HttpListenerPrefix httpListenerPrefix_0)
		{
			httpListenerPrefix_0 = null;
			if (list_2 == null)
			{
				return null;
			}
			HttpListener result = null;
			int num = -1;
			foreach (HttpListenerPrefix item in list_2)
			{
				string string_3 = item.String_1;
				if (string_3.Length >= num && string_2.StartsWith(string_3))
				{
					num = string_3.Length;
					result = item.HttpListener_0;
					httpListenerPrefix_0 = item;
				}
			}
			return result;
		}

		private static void onAccept(object sender, EventArgs e)
		{
			SocketAsyncEventArgs socketAsyncEventArgs = (SocketAsyncEventArgs)e;
			EndPointListener endPointListener = (EndPointListener)socketAsyncEventArgs.UserToken;
			Socket socket = null;
			if (socketAsyncEventArgs.SocketError == SocketError.Success)
			{
				socket = socketAsyncEventArgs.AcceptSocket;
				socketAsyncEventArgs.AcceptSocket = null;
			}
			try
			{
				while (!endPointListener.socket_0.AcceptAsync(socketAsyncEventArgs))
				{
					if (socket != null)
					{
						processAccepted(socket, endPointListener);
						socket = null;
					}
					if (socketAsyncEventArgs.SocketError == SocketError.Success)
					{
						socket = socketAsyncEventArgs.AcceptSocket;
						socketAsyncEventArgs.AcceptSocket = null;
					}
				}
			}
			catch
			{
				if (socket != null)
				{
					socket.Close();
				}
				return;
			}
			if (socket != null)
			{
				processAccepted(socket, endPointListener);
			}
		}

		private static void processAccepted(Socket socket_1, EndPointListener endPointListener_0)
		{
			HttpConnection httpConnection = null;
			try
			{
				httpConnection = new HttpConnection(socket_1, endPointListener_0);
				lock (endPointListener_0.object_0)
				{
					endPointListener_0.dictionary_1[httpConnection] = httpConnection;
				}
				httpConnection.BeginReadRequest();
			}
			catch
			{
				if (httpConnection != null)
				{
					httpConnection.Close(true);
				}
				else
				{
					socket_1.Close();
				}
			}
		}

		private static bool removeSpecial(List<HttpListenerPrefix> list_2, HttpListenerPrefix httpListenerPrefix_0)
		{
			string string_ = httpListenerPrefix_0.String_1;
			int count = list_2.Count;
			int num = 0;
			while (true)
			{
				if (num < count)
				{
					if (list_2[num].String_1 == string_)
					{
						break;
					}
					num++;
					continue;
				}
				return false;
			}
			list_2.RemoveAt(num);
			return true;
		}

		private HttpListener searchListener(Uri uri_0, out HttpListenerPrefix httpListenerPrefix_0)
		{
			httpListenerPrefix_0 = null;
			if (uri_0 == null)
			{
				return null;
			}
			string host = uri_0.Host;
			bool flag = Uri.CheckHostName(host) == UriHostNameType.Dns;
			int port = uri_0.Port;
			string text = HttpUtility.UrlDecode(uri_0.AbsolutePath);
			string text2 = ((text[text.Length - 1] != '/') ? (text + "/") : text);
			HttpListener result = null;
			int num = -1;
			if (host != null && host.Length > 0)
			{
				foreach (HttpListenerPrefix key in dictionary_0.Keys)
				{
					string string_ = key.String_1;
					if (string_.Length < num || key.Int32_0 != port)
					{
						continue;
					}
					if (flag)
					{
						string text3 = key.String_0;
						if (Uri.CheckHostName(text3) == UriHostNameType.Dns && text3 != host)
						{
							continue;
						}
					}
					if (text.StartsWith(string_) || text2.StartsWith(string_))
					{
						num = string_.Length;
						result = dictionary_0[key];
						httpListenerPrefix_0 = key;
					}
				}
				if (num != -1)
				{
					return result;
				}
			}
			List<HttpListenerPrefix> list_ = list_1;
			result = matchFromList(host, text, list_, out httpListenerPrefix_0);
			if (text != text2 && result == null)
			{
				result = matchFromList(host, text2, list_, out httpListenerPrefix_0);
			}
			if (result != null)
			{
				return result;
			}
			list_ = list_0;
			result = matchFromList(host, text, list_, out httpListenerPrefix_0);
			if (text != text2 && result == null)
			{
				result = matchFromList(host, text2, list_, out httpListenerPrefix_0);
			}
			if (result != null)
			{
				return result;
			}
			return null;
		}

		internal static bool CertificateExists(int int_0, string string_1)
		{
			if (string_1 == null || string_1.Length == 0)
			{
				string_1 = string_0;
			}
			string path = Path.Combine(string_1, string.Format("{0}.cer", int_0));
			string path2 = Path.Combine(string_1, string.Format("{0}.key", int_0));
			return File.Exists(path) && File.Exists(path2);
		}

		internal void RemoveConnection(HttpConnection httpConnection_0)
		{
			lock (object_0)
			{
				dictionary_1.Remove(httpConnection_0);
			}
		}

		public void AddPrefix(HttpListenerPrefix httpListenerPrefix_0, HttpListener httpListener_0)
		{
			if (httpListenerPrefix_0.String_0 == "*")
			{
				List<HttpListenerPrefix> list;
				List<HttpListenerPrefix> list2;
				do
				{
					list = list_1;
					list2 = ((list != null) ? new List<HttpListenerPrefix>(list) : new List<HttpListenerPrefix>());
					httpListenerPrefix_0.HttpListener_0 = httpListener_0;
					addSpecial(list2, httpListenerPrefix_0);
				}
				while (Interlocked.CompareExchange(ref list_1, list2, list) != list);
				return;
			}
			if (httpListenerPrefix_0.String_0 == "+")
			{
				List<HttpListenerPrefix> list;
				List<HttpListenerPrefix> list2;
				do
				{
					list = list_0;
					list2 = ((list != null) ? new List<HttpListenerPrefix>(list) : new List<HttpListenerPrefix>());
					httpListenerPrefix_0.HttpListener_0 = httpListener_0;
					addSpecial(list2, httpListenerPrefix_0);
				}
				while (Interlocked.CompareExchange(ref list_0, list2, list) != list);
				return;
			}
			Dictionary<HttpListenerPrefix, HttpListener> dictionary;
			while (true)
			{
				dictionary = dictionary_0;
				if (dictionary.ContainsKey(httpListenerPrefix_0))
				{
					break;
				}
				Dictionary<HttpListenerPrefix, HttpListener> dictionary2 = new Dictionary<HttpListenerPrefix, HttpListener>(dictionary);
				dictionary2[httpListenerPrefix_0] = httpListener_0;
				if (Interlocked.CompareExchange(ref dictionary_0, dictionary2, dictionary) == dictionary)
				{
					return;
				}
			}
			if (dictionary[httpListenerPrefix_0] == httpListener_0)
			{
				return;
			}
			throw new HttpListenerException(400, string.Format("There's another listener for {0}.", httpListenerPrefix_0));
		}

		public bool BindContext(HttpListenerContext httpListenerContext_0)
		{
			HttpListenerPrefix httpListenerPrefix_;
			HttpListener httpListener = searchListener(httpListenerContext_0.HttpListenerRequest_0.Uri_0, out httpListenerPrefix_);
			if (httpListener == null)
			{
				return false;
			}
			httpListenerContext_0.HttpListener_0 = httpListener;
			httpListenerContext_0.HttpConnection_0.HttpListenerPrefix_0 = httpListenerPrefix_;
			return true;
		}

		public void Close()
		{
			socket_0.Close();
			lock (object_0)
			{
				List<HttpConnection> list = new List<HttpConnection>(dictionary_1.Keys);
				dictionary_1.Clear();
				foreach (HttpConnection item in list)
				{
					item.Close(true);
				}
				list.Clear();
			}
		}

		public void RemovePrefix(HttpListenerPrefix httpListenerPrefix_0, HttpListener httpListener_0)
		{
			if (httpListenerPrefix_0.String_0 == "*")
			{
				List<HttpListenerPrefix> list;
				List<HttpListenerPrefix> list2;
				do
				{
					list = list_1;
					if (list == null)
					{
						break;
					}
					list2 = new List<HttpListenerPrefix>(list);
				}
				while (removeSpecial(list2, httpListenerPrefix_0) && Interlocked.CompareExchange(ref list_1, list2, list) != list);
				checkIfRemove();
				return;
			}
			if (httpListenerPrefix_0.String_0 == "+")
			{
				List<HttpListenerPrefix> list;
				List<HttpListenerPrefix> list2;
				do
				{
					list = list_0;
					if (list == null)
					{
						break;
					}
					list2 = new List<HttpListenerPrefix>(list);
				}
				while (removeSpecial(list2, httpListenerPrefix_0) && Interlocked.CompareExchange(ref list_0, list2, list) != list);
				checkIfRemove();
				return;
			}
			Dictionary<HttpListenerPrefix, HttpListener> dictionary;
			Dictionary<HttpListenerPrefix, HttpListener> dictionary2;
			do
			{
				dictionary = dictionary_0;
				if (!dictionary.ContainsKey(httpListenerPrefix_0))
				{
					break;
				}
				dictionary2 = new Dictionary<HttpListenerPrefix, HttpListener>(dictionary);
				dictionary2.Remove(httpListenerPrefix_0);
			}
			while (Interlocked.CompareExchange(ref dictionary_0, dictionary2, dictionary) != dictionary);
			checkIfRemove();
		}

		public void UnbindContext(HttpListenerContext httpListenerContext_0)
		{
			if (httpListenerContext_0 != null && httpListenerContext_0.HttpListener_0 != null)
			{
				httpListenerContext_0.HttpListener_0.UnregisterContext(httpListenerContext_0);
			}
		}
	}
}
