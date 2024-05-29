using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using WebSocketSharp.Net;

namespace WebSocketSharp
{
	internal class HttpRequest : HttpBase
	{
		private string string_1;

		private string string_2;

		private bool bool_0;

		private bool bool_1;

		public AuthenticationResponse AuthenticationResponse_0
		{
			get
			{
				string text = base.NameValueCollection_0["Authorization"];
				return (text == null || text.Length <= 0) ? null : AuthenticationResponse.Parse(text);
			}
		}

		public CookieCollection CookieCollection_0
		{
			get
			{
				return base.NameValueCollection_0.GetCookies(false);
			}
		}

		public string String_1
		{
			get
			{
				return string_1;
			}
		}

		public bool Boolean_0
		{
			get
			{
				if (!bool_1)
				{
					NameValueCollection nameValueCollection = base.NameValueCollection_0;
					bool_0 = string_1 == "GET" && base.Version_0 > HttpVersion.version_0 && nameValueCollection.Contains("Upgrade", "websocket") && nameValueCollection.Contains("Connection", "Upgrade");
					bool_1 = true;
				}
				return bool_0;
			}
		}

		public string String_2
		{
			get
			{
				return string_2;
			}
		}

		private HttpRequest(string string_3, string string_4, Version version_1, NameValueCollection nameValueCollection_1)
			: base(version_1, nameValueCollection_1)
		{
			string_1 = string_3;
			string_2 = string_4;
		}

		internal HttpRequest(string string_3, string string_4)
			: this(string_3, string_4, HttpVersion.version_1, new NameValueCollection())
		{
			base.NameValueCollection_0["User-Agent"] = "websocket-sharp/1.0";
		}

		internal static HttpRequest CreateConnectRequest(Uri uri_0)
		{
			string dnsSafeHost = uri_0.DnsSafeHost;
			int port = uri_0.Port;
			string text = string.Format("{0}:{1}", dnsSafeHost, port);
			HttpRequest httpRequest = new HttpRequest("CONNECT", text);
			httpRequest.NameValueCollection_0["Host"] = ((port != 80) ? text : dnsSafeHost);
			return httpRequest;
		}

		internal static HttpRequest CreateWebSocketRequest(Uri uri_0)
		{
			HttpRequest httpRequest = new HttpRequest("GET", uri_0.PathAndQuery);
			NameValueCollection nameValueCollection = httpRequest.NameValueCollection_0;
			int port = uri_0.Port;
			string scheme = uri_0.Scheme;
			nameValueCollection["Host"] = (((port != 80 || !(scheme == "ws")) && (port != 443 || !(scheme == "wss"))) ? uri_0.Authority : uri_0.DnsSafeHost);
			nameValueCollection["Upgrade"] = "websocket";
			nameValueCollection["Connection"] = "Upgrade";
			return httpRequest;
		}

		internal HttpResponse GetResponse(Stream stream_0, int int_1)
		{
			byte[] array = ToByteArray();
			stream_0.Write(array, 0, array.Length);
			return HttpBase.Read(stream_0, HttpResponse.Parse, int_1);
		}

		internal static HttpRequest Parse(string[] string_3)
		{
			string[] array = string_3[0].Split(new char[1] { ' ' }, 3);
			if (array.Length != 3)
			{
				throw new ArgumentException("Invalid request line: " + string_3[0]);
			}
			WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
			for (int i = 1; i < string_3.Length; i++)
			{
				webHeaderCollection.InternalSet(string_3[i], false);
			}
			return new HttpRequest(array[0], array[1], new Version(array[2].Substring(5)), webHeaderCollection);
		}

		internal static HttpRequest Read(Stream stream_0, int int_1)
		{
			return HttpBase.Read(stream_0, Parse, int_1);
		}

		public void SetCookies(CookieCollection cookieCollection_0)
		{
			if (cookieCollection_0 == null || cookieCollection_0.Count == 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(64);
			foreach (Cookie item in cookieCollection_0.IEnumerable_0)
			{
				if (!item.Boolean_2)
				{
					stringBuilder.AppendFormat("{0}; ", item.ToString());
				}
			}
			int length = stringBuilder.Length;
			if (length > 2)
			{
				stringBuilder.Length = length - 2;
				base.NameValueCollection_0["Cookie"] = stringBuilder.ToString();
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.AppendFormat("{0} {1} HTTP/{2}{3}", string_1, string_2, base.Version_0, "\r\n");
			NameValueCollection nameValueCollection = base.NameValueCollection_0;
			string[] allKeys = nameValueCollection.AllKeys;
			foreach (string text in allKeys)
			{
				stringBuilder.AppendFormat("{0}: {1}{2}", text, nameValueCollection[text], "\r\n");
			}
			stringBuilder.Append("\r\n");
			string text2 = base.String_0;
			if (text2.Length > 0)
			{
				stringBuilder.Append(text2);
			}
			return stringBuilder.ToString();
		}
	}
}
