using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace WebSocketSharp.Net
{
	public sealed class HttpListenerRequest
	{
		private static readonly byte[] byte_0;

		private string[] string_0;

		private bool bool_0;

		private Encoding encoding_0;

		private long long_0;

		private bool bool_1;

		private HttpListenerContext httpListenerContext_0;

		private CookieCollection cookieCollection_0;

		private WebHeaderCollection webHeaderCollection_0;

		private Guid guid_0;

		private Stream stream_0;

		private bool bool_2;

		private bool bool_3;

		private string string_1;

		private NameValueCollection nameValueCollection_0;

		private Uri uri_0;

		private string string_2;

		private Uri uri_1;

		private string[] string_3;

		private Version version_0;

		private bool bool_4;

		private bool bool_5;

		public string[] String_0
		{
			get
			{
				return string_0;
			}
		}

		public int Int32_0
		{
			get
			{
				return 0;
			}
		}

		public Encoding Encoding_0
		{
			get
			{
				return encoding_0 ?? (encoding_0 = Encoding.Default);
			}
		}

		public long Int64_0
		{
			get
			{
				return long_0;
			}
		}

		public string String_1
		{
			get
			{
				return webHeaderCollection_0["Content-Type"];
			}
		}

		public CookieCollection CookieCollection_0
		{
			get
			{
				return cookieCollection_0 ?? (cookieCollection_0 = webHeaderCollection_0.GetCookies(false));
			}
		}

		public bool Boolean_0
		{
			get
			{
				return long_0 > 0L || bool_0;
			}
		}

		public NameValueCollection NameValueCollection_0
		{
			get
			{
				return webHeaderCollection_0;
			}
		}

		public string String_2
		{
			get
			{
				return string_1;
			}
		}

		public Stream Stream_0
		{
			get
			{
				return stream_0 ?? (stream_0 = ((!Boolean_0) ? Stream.Null : httpListenerContext_0.HttpConnection_0.GetRequestStream(long_0, bool_0)));
			}
		}

		public bool Boolean_1
		{
			get
			{
				return httpListenerContext_0.IPrincipal_0 != null;
			}
		}

		public bool Boolean_2
		{
			get
			{
				return IPEndPoint_1.Address.IsLocal();
			}
		}

		public bool Boolean_3
		{
			get
			{
				return httpListenerContext_0.HttpConnection_0.Boolean_1;
			}
		}

		public bool Boolean_4
		{
			get
			{
				if (!bool_5)
				{
					bool_4 = string_1 == "GET" && version_0 > HttpVersion.version_0 && webHeaderCollection_0.Contains("Upgrade", "websocket") && webHeaderCollection_0.Contains("Connection", "Upgrade");
					bool_5 = true;
				}
				return bool_4;
			}
		}

		public bool Boolean_5
		{
			get
			{
				if (!bool_3)
				{
					string text;
					bool_2 = version_0 > HttpVersion.version_0 || webHeaderCollection_0.Contains("Connection", "keep-alive") || ((text = webHeaderCollection_0["Keep-Alive"]) != null && text != "closed");
					bool_3 = true;
				}
				return bool_2;
			}
		}

		public IPEndPoint IPEndPoint_0
		{
			get
			{
				return httpListenerContext_0.HttpConnection_0.IPEndPoint_0;
			}
		}

		public Version Version_0
		{
			get
			{
				return version_0;
			}
		}

		public NameValueCollection NameValueCollection_1
		{
			get
			{
				return nameValueCollection_0 ?? (nameValueCollection_0 = HttpUtility.InternalParseQueryString(uri_1.Query, Encoding.UTF8));
			}
		}

		public string String_3
		{
			get
			{
				return uri_1.PathAndQuery;
			}
		}

		public IPEndPoint IPEndPoint_1
		{
			get
			{
				return httpListenerContext_0.HttpConnection_0.IPEndPoint_1;
			}
		}

		public Guid Guid_0
		{
			get
			{
				return guid_0;
			}
		}

		public Uri Uri_0
		{
			get
			{
				return uri_1;
			}
		}

		public Uri Uri_1
		{
			get
			{
				return uri_0;
			}
		}

		public string String_4
		{
			get
			{
				return webHeaderCollection_0["User-Agent"];
			}
		}

		public string String_5
		{
			get
			{
				return IPEndPoint_0.ToString();
			}
		}

		public string String_6
		{
			get
			{
				return webHeaderCollection_0["Host"];
			}
		}

		public string[] String_7
		{
			get
			{
				return string_3;
			}
		}

		internal HttpListenerRequest(HttpListenerContext httpListenerContext_1)
		{
			httpListenerContext_0 = httpListenerContext_1;
			long_0 = -1L;
			webHeaderCollection_0 = new WebHeaderCollection();
			guid_0 = Guid.NewGuid();
		}

		static HttpListenerRequest()
		{
			byte_0 = Encoding.ASCII.GetBytes("HTTP/1.1 100 Continue\r\n\r\n");
		}

		private static bool tryCreateVersion(string string_4, out Version version_1)
		{
			try
			{
				version_1 = new Version(string_4);
				return true;
			}
			catch
			{
				version_1 = null;
				return false;
			}
		}

		internal void AddHeader(string string_4)
		{
			int num = string_4.IndexOf(':');
			if (num == -1)
			{
				httpListenerContext_0.String_0 = "Invalid header";
				return;
			}
			string text = string_4.Substring(0, num).Trim();
			string text2 = string_4.Substring(num + 1).Trim();
			webHeaderCollection_0.InternalSet(text, text2, false);
			switch (text.ToLower(CultureInfo.InvariantCulture))
			{
			case "accept":
				string_0 = new List<string>(text2.SplitHeaderValue(',')).ToArray();
				break;
			case "accept-language":
				string_3 = text2.Split(',');
				break;
			case "content-length":
			{
				long result;
				if (long.TryParse(text2, out result) && result >= 0L)
				{
					long_0 = result;
					bool_1 = true;
				}
				else
				{
					httpListenerContext_0.String_0 = "Invalid Content-Length header";
				}
				break;
			}
			case "content-type":
				try
				{
					encoding_0 = HttpUtility.GetEncoding(text2);
					break;
				}
				catch
				{
					httpListenerContext_0.String_0 = "Invalid Content-Type header";
					break;
				}
			case "referer":
				uri_0 = text2.ToUri();
				break;
			}
		}

		internal void FinishInitialization()
		{
			string text = webHeaderCollection_0["Host"];
			bool flag = text == null || text.Length == 0;
			if (version_0 > HttpVersion.version_0 && flag)
			{
				httpListenerContext_0.String_0 = "Invalid Host header";
				return;
			}
			if (flag)
			{
				text = String_5;
			}
			uri_1 = HttpUtility.CreateRequestUrl(string_2, text, Boolean_4, Boolean_3);
			if (uri_1 == null)
			{
				httpListenerContext_0.String_0 = "Invalid request url";
				return;
			}
			string text2 = NameValueCollection_0["Transfer-Encoding"];
			if (version_0 > HttpVersion.version_0 && text2 != null && text2.Length > 0)
			{
				bool_0 = text2.ToLower() == "chunked";
				if (!bool_0)
				{
					httpListenerContext_0.String_0 = string.Empty;
					httpListenerContext_0.Int32_0 = 501;
					return;
				}
			}
			if (!bool_0 && !bool_1)
			{
				string text3 = string_1.ToLower();
				if (text3 == "post" || text3 == "put")
				{
					httpListenerContext_0.String_0 = string.Empty;
					httpListenerContext_0.Int32_0 = 411;
					return;
				}
			}
			string text4 = NameValueCollection_0["Expect"];
			if (text4 != null && text4.Length > 0 && text4.ToLower() == "100-continue")
			{
				ResponseStream responseStream = httpListenerContext_0.HttpConnection_0.GetResponseStream();
				responseStream.InternalWrite(byte_0, 0, byte_0.Length);
			}
		}

		internal bool FlushInput()
		{
			if (!Boolean_0)
			{
				return true;
			}
			int num = 2048;
			if (long_0 > 0L)
			{
				num = (int)Math.Min(long_0, num);
			}
			byte[] buffer = new byte[num];
			while (true)
			{
				try
				{
					IAsyncResult asyncResult = Stream_0.BeginRead(buffer, 0, num, null, null);
					if (!asyncResult.IsCompleted && !asyncResult.AsyncWaitHandle.WaitOne(100))
					{
						return false;
					}
					if (Stream_0.EndRead(asyncResult) <= 0)
					{
						return true;
					}
				}
				catch
				{
					return false;
				}
			}
		}

		internal void SetRequestLine(string string_4)
		{
			string[] array = string_4.Split(new char[1] { ' ' }, 3);
			if (array.Length != 3)
			{
				httpListenerContext_0.String_0 = "Invalid request line (parts)";
				return;
			}
			string_1 = array[0];
			if (!string_1.IsToken())
			{
				httpListenerContext_0.String_0 = "Invalid request line (method)";
				return;
			}
			string_2 = array[1];
			string text = array[2];
			if (text.Length != 8 || !text.StartsWith("HTTP/") || !tryCreateVersion(text.Substring(5), out version_0) || version_0.Major < 1)
			{
				httpListenerContext_0.String_0 = "Invalid request line (version)";
			}
		}

		public IAsyncResult BeginGetClientCertificate(AsyncCallback asyncCallback_0, object object_0)
		{
			throw new NotImplementedException();
		}

		public X509Certificate2 EndGetClientCertificate(IAsyncResult iasyncResult_0)
		{
			throw new NotImplementedException();
		}

		public X509Certificate2 GetClientCertificate()
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.AppendFormat("{0} {1} HTTP/{2}\r\n", string_1, string_2, version_0);
			stringBuilder.Append(webHeaderCollection_0.ToString());
			return stringBuilder.ToString();
		}
	}
}
