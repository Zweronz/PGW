using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using WebSocketSharp.Net;

namespace WebSocketSharp
{
	internal class HttpResponse : HttpBase
	{
		private string string_1;

		private string string_2;

		public CookieCollection CookieCollection_0
		{
			get
			{
				return base.NameValueCollection_0.GetCookies(true);
			}
		}

		public bool Boolean_0
		{
			get
			{
				return base.NameValueCollection_0.Contains("Connection", "close");
			}
		}

		public bool Boolean_1
		{
			get
			{
				return string_1 == "407";
			}
		}

		public bool Boolean_2
		{
			get
			{
				return string_1 == "301" || string_1 == "302";
			}
		}

		public bool Boolean_3
		{
			get
			{
				return string_1 == "401";
			}
		}

		public bool Boolean_4
		{
			get
			{
				NameValueCollection nameValueCollection = base.NameValueCollection_0;
				return base.Version_0 > HttpVersion.version_0 && string_1 == "101" && nameValueCollection.Contains("Upgrade", "websocket") && nameValueCollection.Contains("Connection", "Upgrade");
			}
		}

		public string String_1
		{
			get
			{
				return string_2;
			}
		}

		public string String_2
		{
			get
			{
				return string_1;
			}
		}

		private HttpResponse(string string_3, string string_4, Version version_1, NameValueCollection nameValueCollection_1)
			: base(version_1, nameValueCollection_1)
		{
			string_1 = string_3;
			string_2 = string_4;
		}

		internal HttpResponse(HttpStatusCode httpStatusCode_0)
			: this(httpStatusCode_0, httpStatusCode_0.GetDescription())
		{
		}

		internal HttpResponse(HttpStatusCode httpStatusCode_0, string string_3)
			: this(((int)httpStatusCode_0).ToString(), string_3, HttpVersion.version_1, new NameValueCollection())
		{
			base.NameValueCollection_0["Server"] = "websocket-sharp/1.0";
		}

		internal static HttpResponse CreateCloseResponse(HttpStatusCode httpStatusCode_0)
		{
			HttpResponse httpResponse = new HttpResponse(httpStatusCode_0);
			httpResponse.NameValueCollection_0["Connection"] = "close";
			return httpResponse;
		}

		internal static HttpResponse CreateUnauthorizedResponse(string string_3)
		{
			HttpResponse httpResponse = new HttpResponse(HttpStatusCode.Unauthorized);
			httpResponse.NameValueCollection_0["WWW-Authenticate"] = string_3;
			return httpResponse;
		}

		internal static HttpResponse CreateWebSocketResponse()
		{
			HttpResponse httpResponse = new HttpResponse(HttpStatusCode.SwitchingProtocols);
			NameValueCollection nameValueCollection = httpResponse.NameValueCollection_0;
			nameValueCollection["Upgrade"] = "websocket";
			nameValueCollection["Connection"] = "Upgrade";
			return httpResponse;
		}

		internal static HttpResponse Parse(string[] string_3)
		{
			string[] array = string_3[0].Split(new char[1] { ' ' }, 3);
			if (array.Length != 3)
			{
				throw new ArgumentException("Invalid status line: " + string_3[0]);
			}
			WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
			for (int i = 1; i < string_3.Length; i++)
			{
				webHeaderCollection.InternalSet(string_3[i], true);
			}
			return new HttpResponse(array[1], array[2], new Version(array[0].Substring(5)), webHeaderCollection);
		}

		internal static HttpResponse Read(Stream stream_0, int int_1)
		{
			return HttpBase.Read(stream_0, Parse, int_1);
		}

		public void SetCookies(CookieCollection cookieCollection_0)
		{
			if (cookieCollection_0 == null || cookieCollection_0.Count == 0)
			{
				return;
			}
			NameValueCollection nameValueCollection = base.NameValueCollection_0;
			foreach (Cookie item in cookieCollection_0.IEnumerable_0)
			{
				nameValueCollection.Add("Set-Cookie", item.ToResponseString());
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.AppendFormat("HTTP/{0} {1} {2}{3}", base.Version_0, string_1, string_2, "\r\n");
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
