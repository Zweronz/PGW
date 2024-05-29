using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace WebSocketSharp.Net
{
	internal sealed class EndPointManager
	{
		private static Dictionary<IPAddress, Dictionary<int, EndPointListener>> dictionary_0;

		private EndPointManager()
		{
		}

		static EndPointManager()
		{
			dictionary_0 = new Dictionary<IPAddress, Dictionary<int, EndPointListener>>();
		}

		private static void addPrefix(string string_0, HttpListener httpListener_0)
		{
			HttpListenerPrefix httpListenerPrefix = new HttpListenerPrefix(string_0);
			if (httpListenerPrefix.String_1.IndexOf('%') != -1)
			{
				throw new HttpListenerException(400, "Invalid path.");
			}
			if (httpListenerPrefix.String_1.IndexOf("//", StringComparison.Ordinal) != -1)
			{
				throw new HttpListenerException(400, "Invalid path.");
			}
			EndPointListener endPointListener = getEndPointListener(httpListenerPrefix.String_0, httpListenerPrefix.Int32_0, httpListener_0, httpListenerPrefix.Boolean_0);
			endPointListener.AddPrefix(httpListenerPrefix, httpListener_0);
		}

		private static IPAddress convertToAddress(string string_0)
		{
			if (!(string_0 == "*") && !(string_0 == "+"))
			{
				IPAddress address;
				if (IPAddress.TryParse(string_0, out address))
				{
					return address;
				}
				try
				{
					IPHostEntry hostEntry = Dns.GetHostEntry(string_0);
					return (hostEntry == null) ? IPAddress.Any : hostEntry.AddressList[0];
				}
				catch
				{
					return IPAddress.Any;
				}
			}
			return IPAddress.Any;
		}

		private static EndPointListener getEndPointListener(string string_0, int int_0, HttpListener httpListener_0, bool bool_0)
		{
			IPAddress iPAddress = convertToAddress(string_0);
			Dictionary<int, EndPointListener> dictionary = null;
			if (dictionary_0.ContainsKey(iPAddress))
			{
				dictionary = dictionary_0[iPAddress];
			}
			else
			{
				dictionary = new Dictionary<int, EndPointListener>();
				dictionary_0[iPAddress] = dictionary;
			}
			EndPointListener endPointListener = null;
			return dictionary.ContainsKey(int_0) ? dictionary[int_0] : (dictionary[int_0] = new EndPointListener(iPAddress, int_0, bool_0, httpListener_0.String_0, httpListener_0.ServerSslConfiguration_0, httpListener_0.Boolean_1));
		}

		private static void removePrefix(string string_0, HttpListener httpListener_0)
		{
			HttpListenerPrefix httpListenerPrefix = new HttpListenerPrefix(string_0);
			if (httpListenerPrefix.String_1.IndexOf('%') == -1 && httpListenerPrefix.String_1.IndexOf("//", StringComparison.Ordinal) == -1)
			{
				EndPointListener endPointListener = getEndPointListener(httpListenerPrefix.String_0, httpListenerPrefix.Int32_0, httpListener_0, httpListenerPrefix.Boolean_0);
				endPointListener.RemovePrefix(httpListenerPrefix, httpListener_0);
			}
		}

		internal static void RemoveEndPoint(EndPointListener endPointListener_0)
		{
			lock (((ICollection)dictionary_0).SyncRoot)
			{
				IPAddress iPAddress_ = endPointListener_0.IPAddress_0;
				Dictionary<int, EndPointListener> dictionary = dictionary_0[iPAddress_];
				dictionary.Remove(endPointListener_0.Int32_0);
				if (dictionary.Count == 0)
				{
					dictionary_0.Remove(iPAddress_);
				}
				endPointListener_0.Close();
			}
		}

		public static void AddListener(HttpListener httpListener_0)
		{
			List<string> list = new List<string>();
			lock (((ICollection)dictionary_0).SyncRoot)
			{
				try
				{
					foreach (string item in httpListener_0.HttpListenerPrefixCollection_0)
					{
						addPrefix(item, httpListener_0);
						list.Add(item);
					}
				}
				catch
				{
					foreach (string item2 in list)
					{
						removePrefix(item2, httpListener_0);
					}
					throw;
				}
			}
		}

		public static void AddPrefix(string string_0, HttpListener httpListener_0)
		{
			lock (((ICollection)dictionary_0).SyncRoot)
			{
				addPrefix(string_0, httpListener_0);
			}
		}

		public static void RemoveListener(HttpListener httpListener_0)
		{
			lock (((ICollection)dictionary_0).SyncRoot)
			{
				foreach (string item in httpListener_0.HttpListenerPrefixCollection_0)
				{
					removePrefix(item, httpListener_0);
				}
			}
		}

		public static void RemovePrefix(string string_0, HttpListener httpListener_0)
		{
			lock (((ICollection)dictionary_0).SyncRoot)
			{
				removePrefix(string_0, httpListener_0);
			}
		}
	}
}
