using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Server
{
	public class HttpServer
	{
		private IPAddress ipaddress_0;

		private string string_0;

		private WebSocketSharp.Net.HttpListener httpListener_0;

		private Logger logger_0;

		private int int_0;

		private Thread thread_0;

		private string string_1;

		private bool bool_0;

		private WebSocketServiceManager webSocketServiceManager_0;

		private volatile ServerState serverState_0;

		private object object_0;

		private bool bool_1;

		private EventHandler<HttpRequestEventArgs> eventHandler_0;

		private EventHandler<HttpRequestEventArgs> eventHandler_1;

		private EventHandler<HttpRequestEventArgs> eventHandler_2;

		private EventHandler<HttpRequestEventArgs> eventHandler_3;

		private EventHandler<HttpRequestEventArgs> eventHandler_4;

		private EventHandler<HttpRequestEventArgs> eventHandler_5;

		private EventHandler<HttpRequestEventArgs> eventHandler_6;

		private EventHandler<HttpRequestEventArgs> eventHandler_7;

		private EventHandler<HttpRequestEventArgs> eventHandler_8;

		public IPAddress IPAddress_0
		{
			get
			{
				return ipaddress_0;
			}
		}

		public WebSocketSharp.Net.AuthenticationSchemes AuthenticationSchemes_0
		{
			get
			{
				return httpListener_0.AuthenticationSchemes_0;
			}
			set
			{
				string text = serverState_0.CheckIfAvailable(true, false, false);
				if (text != null)
				{
					logger_0.Error(text);
				}
				else
				{
					httpListener_0.AuthenticationSchemes_0 = value;
				}
			}
		}

		public bool Boolean_0
		{
			get
			{
				return serverState_0 == ServerState.Start;
			}
		}

		public bool Boolean_1
		{
			get
			{
				return bool_0;
			}
		}

		public bool Boolean_2
		{
			get
			{
				return webSocketServiceManager_0.Boolean_0;
			}
			set
			{
				string text = serverState_0.CheckIfAvailable(true, false, false);
				if (text != null)
				{
					logger_0.Error(text);
				}
				else
				{
					webSocketServiceManager_0.Boolean_0 = value;
				}
			}
		}

		public Logger Logger_0
		{
			get
			{
				return logger_0;
			}
		}

		public int Int32_0
		{
			get
			{
				return int_0;
			}
		}

		public string String_0
		{
			get
			{
				return httpListener_0.String_1;
			}
			set
			{
				string text = serverState_0.CheckIfAvailable(true, false, false);
				if (text != null)
				{
					logger_0.Error(text);
				}
				else
				{
					httpListener_0.String_1 = value;
				}
			}
		}

		public bool Boolean_3
		{
			get
			{
				return httpListener_0.Boolean_1;
			}
			set
			{
				string text = serverState_0.CheckIfAvailable(true, false, false);
				if (text != null)
				{
					logger_0.Error(text);
				}
				else
				{
					httpListener_0.Boolean_1 = value;
				}
			}
		}

		public string String_1
		{
			get
			{
				return (string_1 == null || string_1.Length <= 0) ? (string_1 = "./Public") : string_1;
			}
			set
			{
				string text = serverState_0.CheckIfAvailable(true, false, false);
				if (text != null)
				{
					logger_0.Error(text);
				}
				else
				{
					string_1 = value;
				}
			}
		}

		public ServerSslConfiguration ServerSslConfiguration_0
		{
			get
			{
				return httpListener_0.ServerSslConfiguration_0;
			}
			set
			{
				string text = serverState_0.CheckIfAvailable(true, false, false);
				if (text != null)
				{
					logger_0.Error(text);
				}
				else
				{
					httpListener_0.ServerSslConfiguration_0 = value;
				}
			}
		}

		public Func<IIdentity, WebSocketSharp.Net.NetworkCredential> Func_0
		{
			get
			{
				return httpListener_0.Func_1;
			}
			set
			{
				string text = serverState_0.CheckIfAvailable(true, false, false);
				if (text != null)
				{
					logger_0.Error(text);
				}
				else
				{
					httpListener_0.Func_1 = value;
				}
			}
		}

		public TimeSpan TimeSpan_0
		{
			get
			{
				return webSocketServiceManager_0.TimeSpan_0;
			}
			set
			{
				string text = serverState_0.CheckIfAvailable(true, false, false) ?? value.CheckIfValidWaitTime();
				if (text != null)
				{
					logger_0.Error(text);
				}
				else
				{
					webSocketServiceManager_0.TimeSpan_0 = value;
				}
			}
		}

		public WebSocketServiceManager WebSocketServiceManager_0
		{
			get
			{
				return webSocketServiceManager_0;
			}
		}

		public event EventHandler<HttpRequestEventArgs> OnConnect
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_0 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_0 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler_0, value);
			}
		}

		public event EventHandler<HttpRequestEventArgs> OnDelete
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_1 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler_1, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_1 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler_1, value);
			}
		}

		public event EventHandler<HttpRequestEventArgs> OnGet
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_2 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler_2, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_2 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler_2, value);
			}
		}

		public event EventHandler<HttpRequestEventArgs> OnHead
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_3 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler_3, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_3 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler_3, value);
			}
		}

		public event EventHandler<HttpRequestEventArgs> OnOptions
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_4 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler_4, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_4 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler_4, value);
			}
		}

		public event EventHandler<HttpRequestEventArgs> OnPatch
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_5 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler_5, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_5 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler_5, value);
			}
		}

		public event EventHandler<HttpRequestEventArgs> OnPost
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_6 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler_6, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_6 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler_6, value);
			}
		}

		public event EventHandler<HttpRequestEventArgs> OnPut
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_7 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler_7, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_7 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler_7, value);
			}
		}

		public event EventHandler<HttpRequestEventArgs> OnTrace
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_8 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler_8, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_8 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler_8, value);
			}
		}

		public HttpServer()
		{
			init("*", IPAddress.Any, 80, false);
		}

		public HttpServer(int int_1)
			: this(int_1, int_1 == 443)
		{
		}

		public HttpServer(string string_2)
		{
			if (string_2 == null)
			{
				throw new ArgumentNullException("url");
			}
			if (string_2.Length == 0)
			{
				throw new ArgumentException("An empty string.", "url");
			}
			Uri uri_;
			string string_3;
			if (!tryCreateUri(string_2, out uri_, out string_3))
			{
				throw new ArgumentException(string_3, "url");
			}
			string dnsSafeHost = uri_.DnsSafeHost;
			IPAddress ipaddress_ = dnsSafeHost.ToIPAddress();
			if (!ipaddress_.IsLocal())
			{
				throw new ArgumentException("The host part isn't a local host name: " + string_2, "url");
			}
			init(dnsSafeHost, ipaddress_, uri_.Port, uri_.Scheme == "https");
		}

		public HttpServer(int int_1, bool bool_2)
		{
			if (!int_1.IsPortNumber())
			{
				throw new ArgumentOutOfRangeException("port", "Not between 1 and 65535 inclusive: " + int_1);
			}
			init("*", IPAddress.Any, int_1, bool_2);
		}

		public HttpServer(IPAddress ipaddress_1, int int_1)
			: this(ipaddress_1, int_1, int_1 == 443)
		{
		}

		public HttpServer(IPAddress ipaddress_1, int int_1, bool bool_2)
		{
			if (ipaddress_1 == null)
			{
				throw new ArgumentNullException("address");
			}
			if (!ipaddress_1.IsLocal())
			{
				throw new ArgumentException("Not a local IP address: " + ipaddress_1, "address");
			}
			if (!int_1.IsPortNumber())
			{
				throw new ArgumentOutOfRangeException("port", "Not between 1 and 65535 inclusive: " + int_1);
			}
			init(null, ipaddress_1, int_1, bool_2);
		}

		private void abort()
		{
			lock (object_0)
			{
				if (!Boolean_0)
				{
					return;
				}
				serverState_0 = ServerState.ShuttingDown;
			}
			webSocketServiceManager_0.Stop(new CloseEventArgs(CloseStatusCode.ServerError), true, false);
			httpListener_0.Abort();
			serverState_0 = ServerState.Stop;
		}

		private string checkIfCertificateExists()
		{
			if (!bool_0)
			{
				return null;
			}
			bool flag = httpListener_0.ServerSslConfiguration_0.X509Certificate2_0 != null;
			bool flag2 = EndPointListener.CertificateExists(int_0, httpListener_0.String_0);
			if (flag && flag2)
			{
				logger_0.Warn("The server certificate associated with the port number already exists.");
				return null;
			}
			return (flag || flag2) ? null : "The secure connection requires a server certificate.";
		}

		private void init(string string_2, IPAddress ipaddress_1, int int_1, bool bool_2)
		{
			string_0 = string_2 ?? ipaddress_1.ToString();
			ipaddress_0 = ipaddress_1;
			int_0 = int_1;
			bool_0 = bool_2;
			httpListener_0 = new WebSocketSharp.Net.HttpListener();
			httpListener_0.HttpListenerPrefixCollection_0.Add(string.Format("http{0}://{1}:{2}/", (!bool_2) ? string.Empty : "s", string_0, int_1));
			logger_0 = httpListener_0.Logger_0;
			webSocketServiceManager_0 = new WebSocketServiceManager(logger_0);
			object_0 = new object();
			OperatingSystem oSVersion = Environment.OSVersion;
			bool_1 = oSVersion.Platform != PlatformID.Unix && oSVersion.Platform != PlatformID.MacOSX;
		}

		private void processRequest(WebSocketSharp.Net.HttpListenerContext httpListenerContext_0)
		{
			object obj;
			switch (httpListenerContext_0.HttpListenerRequest_0.String_2)
			{
			case "GET":
				obj = eventHandler_2;
				break;
			case "HEAD":
				obj = eventHandler_3;
				break;
			case "POST":
				obj = eventHandler_6;
				break;
			case "PUT":
				obj = eventHandler_7;
				break;
			case "DELETE":
				obj = eventHandler_1;
				break;
			case "OPTIONS":
				obj = eventHandler_4;
				break;
			case "TRACE":
				obj = eventHandler_8;
				break;
			case "CONNECT":
				obj = eventHandler_0;
				break;
			case "PATCH":
				obj = eventHandler_5;
				break;
			default:
				obj = null;
				break;
			}
			EventHandler<HttpRequestEventArgs> eventHandler = (EventHandler<HttpRequestEventArgs>)obj;
			if (eventHandler != null)
			{
				eventHandler(this, new HttpRequestEventArgs(httpListenerContext_0));
			}
			else
			{
				httpListenerContext_0.HttpListenerResponse_0.Int32_0 = 501;
			}
			httpListenerContext_0.HttpListenerResponse_0.Close();
		}

		private void processRequest(HttpListenerWebSocketContext httpListenerWebSocketContext_0)
		{
			WebSocketServiceHost webSocketServiceHost_;
			if (!webSocketServiceManager_0.InternalTryGetServiceHost(httpListenerWebSocketContext_0.Uri_0.AbsolutePath, out webSocketServiceHost_))
			{
				httpListenerWebSocketContext_0.Close(WebSocketSharp.Net.HttpStatusCode.NotImplemented);
			}
			else
			{
				webSocketServiceHost_.StartSession(httpListenerWebSocketContext_0);
			}
		}

		private void receiveRequest()
		{
			while (true)
			{
				try
				{
					WebSocketSharp.Net.HttpListenerContext httpListenerContext_0 = httpListener_0.GetContext();
					ThreadPool.QueueUserWorkItem(delegate
					{
						try
						{
							if (httpListenerContext_0.HttpListenerRequest_0.IsUpgradeTo("websocket"))
							{
								processRequest(httpListenerContext_0.AcceptWebSocket(null));
							}
							else
							{
								processRequest(httpListenerContext_0);
							}
						}
						catch (Exception ex3)
						{
							logger_0.Fatal(ex3.ToString());
							httpListenerContext_0.HttpConnection_0.Close(true);
						}
					});
				}
				catch (WebSocketSharp.Net.HttpListenerException ex)
				{
					logger_0.Warn("Receiving has been stopped.\n  reason: " + ex.Message);
					break;
				}
				catch (Exception ex2)
				{
					logger_0.Fatal(ex2.ToString());
					break;
				}
			}
			if (Boolean_0)
			{
				abort();
			}
		}

		private void startReceiving()
		{
			httpListener_0.Start();
			thread_0 = new Thread(receiveRequest);
			thread_0.IsBackground = true;
			thread_0.Start();
		}

		private void stopReceiving(int int_1)
		{
			httpListener_0.Close();
			thread_0.Join(int_1);
		}

		private static bool tryCreateUri(string string_2, out Uri uri_0, out string string_3)
		{
			uri_0 = null;
			Uri uri = string_2.ToUri();
			if (uri == null)
			{
				string_3 = "An invalid URI string: " + string_2;
				return false;
			}
			if (!uri.IsAbsoluteUri)
			{
				string_3 = "Not an absolute URI: " + string_2;
				return false;
			}
			string scheme = uri.Scheme;
			if (!(scheme == "http") && !(scheme == "https"))
			{
				string_3 = "The scheme part isn't 'http' or 'https': " + string_2;
				return false;
			}
			if (uri.PathAndQuery != "/")
			{
				string_3 = "Includes the path or query component: " + string_2;
				return false;
			}
			if (uri.Fragment.Length > 0)
			{
				string_3 = "Includes the fragment component: " + string_2;
				return false;
			}
			if (uri.Port == 0)
			{
				string_3 = "The port part is zero: " + string_2;
				return false;
			}
			uri_0 = uri;
			string_3 = string.Empty;
			return true;
		}

		public void AddWebSocketService<TBehavior>(string string_2, Func<TBehavior> func_0) where TBehavior : WebSocketBehavior
		{
			string text = string_2.CheckIfValidServicePath() ?? ((func_0 != null) ? null : "'initializer' is null.");
			if (text != null)
			{
				logger_0.Error(text);
			}
			else
			{
				webSocketServiceManager_0.Add(string_2, func_0);
			}
		}

		public void AddWebSocketService<TBehaviorWithNew>(string string_2) where TBehaviorWithNew : WebSocketBehavior, new()
		{
			AddWebSocketService(string_2, () => new TBehaviorWithNew());
		}

		public byte[] GetFile(string string_2)
		{
			string_2 = String_1 + string_2;
			if (bool_1)
			{
				string_2 = string_2.Replace("/", "\\");
			}
			return (!File.Exists(string_2)) ? null : File.ReadAllBytes(string_2);
		}

		public bool RemoveWebSocketService(string string_2)
		{
			string text = string_2.CheckIfValidServicePath();
			if (text != null)
			{
				logger_0.Error(text);
				return false;
			}
			return webSocketServiceManager_0.Remove(string_2);
		}

		public void Start()
		{
			lock (object_0)
			{
				string text = serverState_0.CheckIfAvailable(true, false, false) ?? checkIfCertificateExists();
				if (text != null)
				{
					logger_0.Error(text);
					return;
				}
				webSocketServiceManager_0.Start();
				startReceiving();
				serverState_0 = ServerState.Start;
			}
		}

		public void Stop()
		{
			lock (object_0)
			{
				string text = serverState_0.CheckIfAvailable(false, true, false);
				if (text != null)
				{
					logger_0.Error(text);
					return;
				}
				serverState_0 = ServerState.ShuttingDown;
			}
			webSocketServiceManager_0.Stop(new CloseEventArgs(), true, true);
			stopReceiving(5000);
			serverState_0 = ServerState.Stop;
		}

		public void Stop(ushort ushort_0, string string_2)
		{
			lock (object_0)
			{
				string text = serverState_0.CheckIfAvailable(false, true, false) ?? WebSocket.CheckCloseParameters(ushort_0, string_2, false);
				if (text != null)
				{
					logger_0.Error(text);
					return;
				}
				serverState_0 = ServerState.ShuttingDown;
			}
			if (ushort_0 == 1005)
			{
				webSocketServiceManager_0.Stop(new CloseEventArgs(), true, true);
			}
			else
			{
				bool bool_ = !ushort_0.IsReserved();
				webSocketServiceManager_0.Stop(new CloseEventArgs(ushort_0, string_2), bool_, bool_);
			}
			stopReceiving(5000);
			serverState_0 = ServerState.Stop;
		}

		public void Stop(CloseStatusCode closeStatusCode_0, string string_2)
		{
			lock (object_0)
			{
				string text = serverState_0.CheckIfAvailable(false, true, false) ?? WebSocket.CheckCloseParameters(closeStatusCode_0, string_2, false);
				if (text != null)
				{
					logger_0.Error(text);
					return;
				}
				serverState_0 = ServerState.ShuttingDown;
			}
			if (closeStatusCode_0 == CloseStatusCode.NoStatus)
			{
				webSocketServiceManager_0.Stop(new CloseEventArgs(), true, true);
			}
			else
			{
				bool bool_ = !closeStatusCode_0.IsReserved();
				webSocketServiceManager_0.Stop(new CloseEventArgs(closeStatusCode_0, string_2), bool_, bool_);
			}
			stopReceiving(5000);
			serverState_0 = ServerState.Stop;
		}
	}
}
