using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace WebSocketSharp.Net
{
	public sealed class HttpListenerResponse : IDisposable
	{
		private bool bool_0;

		private Encoding encoding_0;

		private long long_0;

		private string string_0;

		private HttpListenerContext httpListenerContext_0;

		private CookieCollection cookieCollection_0;

		private bool bool_1;

		private WebHeaderCollection webHeaderCollection_0;

		private bool bool_2;

		private bool bool_3;

		private string string_1;

		private ResponseStream responseStream_0;

		private bool bool_4;

		private int int_0;

		private string string_2;

		private Version version_0;

		internal bool Boolean_0
		{
			get
			{
				return bool_0;
			}
			set
			{
				bool_0 = value;
			}
		}

		internal bool Boolean_1
		{
			get
			{
				return bool_2;
			}
			set
			{
				bool_2 = value;
			}
		}

		public Encoding Encoding_0
		{
			get
			{
				return encoding_0;
			}
			set
			{
				checkDisposed();
				encoding_0 = value;
			}
		}

		public long Int64_0
		{
			get
			{
				return long_0;
			}
			set
			{
				checkDisposedOrHeadersSent();
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("Less than zero.", "value");
				}
				long_0 = value;
			}
		}

		public string String_0
		{
			get
			{
				return string_0;
			}
			set
			{
				checkDisposed();
				if (value != null && value.Length == 0)
				{
					throw new ArgumentException("An empty string.", "value");
				}
				string_0 = value;
			}
		}

		public CookieCollection CookieCollection_0
		{
			get
			{
				return cookieCollection_0 ?? (cookieCollection_0 = new CookieCollection());
			}
			set
			{
				cookieCollection_0 = value;
			}
		}

		public WebHeaderCollection WebHeaderCollection_0
		{
			get
			{
				return webHeaderCollection_0 ?? (webHeaderCollection_0 = new WebHeaderCollection(HttpHeaderType.Response, false));
			}
			set
			{
				if (value != null && value.HttpHeaderType_0 != HttpHeaderType.Response)
				{
					throw new InvalidOperationException("The specified headers aren't valid for a response.");
				}
				webHeaderCollection_0 = value;
			}
		}

		public bool Boolean_2
		{
			get
			{
				return bool_3;
			}
			set
			{
				checkDisposedOrHeadersSent();
				bool_3 = value;
			}
		}

		public Stream Stream_0
		{
			get
			{
				checkDisposed();
				return responseStream_0 ?? (responseStream_0 = httpListenerContext_0.HttpConnection_0.GetResponseStream());
			}
		}

		public Version Version_0
		{
			get
			{
				return version_0;
			}
			set
			{
				checkDisposedOrHeadersSent();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Major != 1 || (value.Minor != 0 && value.Minor != 1))
				{
					throw new ArgumentException("Not 1.0 or 1.1.", "value");
				}
				version_0 = value;
			}
		}

		public string String_1
		{
			get
			{
				return string_1;
			}
			set
			{
				checkDisposed();
				if (value == null)
				{
					string_1 = null;
					return;
				}
				Uri result = null;
				if (!value.MaybeUri() || !Uri.TryCreate(value, UriKind.Absolute, out result))
				{
					throw new ArgumentException("Not an absolute URL.", "value");
				}
				string_1 = value;
			}
		}

		public bool Boolean_3
		{
			get
			{
				return bool_4;
			}
			set
			{
				checkDisposedOrHeadersSent();
				bool_4 = value;
			}
		}

		public int Int32_0
		{
			get
			{
				return int_0;
			}
			set
			{
				checkDisposedOrHeadersSent();
				if (value < 100 || value > 999)
				{
					throw new ProtocolViolationException("A value isn't between 100 and 999 inclusive.");
				}
				int_0 = value;
				string_2 = value.GetStatusDescription();
			}
		}

		public string String_2
		{
			get
			{
				return string_2;
			}
			set
			{
				checkDisposedOrHeadersSent();
				if (value != null && value.Length != 0)
				{
					if (!value.IsText() || value.IndexOfAny(new char[2] { '\r', '\n' }) > -1)
					{
						throw new ArgumentException("Contains invalid characters.", "value");
					}
					string_2 = value;
				}
				else
				{
					string_2 = int_0.GetStatusDescription();
				}
			}
		}

		internal HttpListenerResponse(HttpListenerContext httpListenerContext_1)
		{
			httpListenerContext_0 = httpListenerContext_1;
			bool_3 = true;
			int_0 = 200;
			string_2 = "OK";
			version_0 = HttpVersion.version_1;
		}

		void IDisposable.Dispose()
		{
			if (!bool_1)
			{
				close(true);
			}
		}

		private bool canAddOrUpdate(Cookie cookie_0)
		{
			if (cookieCollection_0 != null && cookieCollection_0.Count != 0)
			{
				List<Cookie> list = findCookie(cookie_0).ToList();
				if (list.Count == 0)
				{
					return true;
				}
				int int32_ = cookie_0.Int32_2;
				foreach (Cookie item in list)
				{
					if (item.Int32_2 == int32_)
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		private void checkDisposed()
		{
			if (bool_1)
			{
				throw new ObjectDisposedException(GetType().ToString());
			}
		}

		private void checkDisposedOrHeadersSent()
		{
			if (bool_1)
			{
				throw new ObjectDisposedException(GetType().ToString());
			}
			if (bool_2)
			{
				throw new InvalidOperationException("Cannot be changed after the headers are sent.");
			}
		}

		private void close(bool bool_5)
		{
			bool_1 = true;
			httpListenerContext_0.HttpConnection_0.Close(bool_5);
		}

		private IEnumerable<Cookie> findCookie(Cookie cookie_0)
		{
			string text = cookie_0.String_2;
			string text2 = cookie_0.String_1;
			string string_ = cookie_0.String_3;
			if (cookieCollection_0 != null)
			{
				IEnumerator enumerator = cookieCollection_0.GetEnumerator();
				/*Error near IL_007f: Could not find block for branch target IL_00f0*/;
			}
			yield break;
		}

		internal WebHeaderCollection WriteHeadersTo(MemoryStream memoryStream_0)
		{
			WebHeaderCollection webHeaderCollection = new WebHeaderCollection(HttpHeaderType.Response, true);
			if (webHeaderCollection_0 != null)
			{
				webHeaderCollection.Add(webHeaderCollection_0);
			}
			if (string_0 != null)
			{
				string text = ((string_0.IndexOf("charset=", StringComparison.Ordinal) != -1 || encoding_0 == null) ? string_0 : string.Format("{0}; charset={1}", string_0, encoding_0.WebName));
				webHeaderCollection.InternalSet("Content-Type", text, true);
			}
			if (webHeaderCollection["Server"] == null)
			{
				webHeaderCollection.InternalSet("Server", "websocket-sharp/1.0", true);
			}
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			if (webHeaderCollection["Date"] == null)
			{
				webHeaderCollection.InternalSet("Date", DateTime.UtcNow.ToString("r", invariantCulture), true);
			}
			if (!bool_4)
			{
				webHeaderCollection.InternalSet("Content-Length", long_0.ToString(invariantCulture), true);
			}
			else
			{
				webHeaderCollection.InternalSet("Transfer-Encoding", "chunked", true);
			}
			bool flag = !httpListenerContext_0.HttpListenerRequest_0.Boolean_5 || !bool_3 || int_0 == 400 || int_0 == 408 || int_0 == 411 || int_0 == 413 || int_0 == 414 || int_0 == 500 || int_0 == 503;
			int int32_ = httpListenerContext_0.HttpConnection_0.Int32_0;
			if (!flag && int32_ < 100)
			{
				webHeaderCollection.InternalSet("Keep-Alive", string.Format("timeout=15,max={0}", 100 - int32_), true);
				if (httpListenerContext_0.HttpListenerRequest_0.Version_0 < HttpVersion.version_1)
				{
					webHeaderCollection.InternalSet("Connection", "keep-alive", true);
				}
			}
			else
			{
				webHeaderCollection.InternalSet("Connection", "close", true);
			}
			if (string_1 != null)
			{
				webHeaderCollection.InternalSet("Location", string_1, true);
			}
			if (cookieCollection_0 != null)
			{
				foreach (Cookie item in cookieCollection_0)
				{
					webHeaderCollection.InternalSet("Set-Cookie", item.ToResponseString(), true);
				}
			}
			Encoding encoding = encoding_0 ?? Encoding.Default;
			StreamWriter streamWriter = new StreamWriter(memoryStream_0, encoding, 256);
			streamWriter.Write("HTTP/{0} {1} {2}\r\n", version_0, int_0, string_2);
			streamWriter.Write(webHeaderCollection.ToStringMultiValue(true));
			streamWriter.Flush();
			memoryStream_0.Position = ((encoding.CodePage != 65001) ? encoding.GetPreamble().Length : 3);
			return webHeaderCollection;
		}

		public void Abort()
		{
			if (!bool_1)
			{
				close(true);
			}
		}

		public void AddHeader(string string_3, string string_4)
		{
			WebHeaderCollection_0.Set(string_3, string_4);
		}

		public void AppendCookie(Cookie cookie_0)
		{
			CookieCollection_0.Add(cookie_0);
		}

		public void AppendHeader(string string_3, string string_4)
		{
			WebHeaderCollection_0.Add(string_3, string_4);
		}

		public void Close()
		{
			if (!bool_1)
			{
				close(false);
			}
		}

		public void Close(byte[] byte_0, bool bool_5)
		{
			checkDisposed();
			if (byte_0 == null)
			{
				throw new ArgumentNullException("responseEntity");
			}
			int count = byte_0.Length;
			Stream stream_0 = Stream_0;
			if (bool_5)
			{
				stream_0.Write(byte_0, 0, count);
				close(false);
				return;
			}
			stream_0.BeginWrite(byte_0, 0, count, delegate(IAsyncResult iasyncResult_0)
			{
				stream_0.EndWrite(iasyncResult_0);
				close(false);
			}, null);
		}

		public void CopyFrom(HttpListenerResponse httpListenerResponse_0)
		{
			if (httpListenerResponse_0 == null)
			{
				throw new ArgumentNullException("templateResponse");
			}
			if (httpListenerResponse_0.webHeaderCollection_0 != null)
			{
				if (webHeaderCollection_0 != null)
				{
					webHeaderCollection_0.Clear();
				}
				WebHeaderCollection_0.Add(httpListenerResponse_0.webHeaderCollection_0);
			}
			else if (webHeaderCollection_0 != null)
			{
				webHeaderCollection_0 = null;
			}
			long_0 = httpListenerResponse_0.long_0;
			int_0 = httpListenerResponse_0.int_0;
			string_2 = httpListenerResponse_0.string_2;
			bool_3 = httpListenerResponse_0.bool_3;
			version_0 = httpListenerResponse_0.version_0;
		}

		public void Redirect(string string_3)
		{
			checkDisposedOrHeadersSent();
			if (string_3 == null)
			{
				throw new ArgumentNullException("url");
			}
			Uri result = null;
			if (!string_3.MaybeUri() || !Uri.TryCreate(string_3, UriKind.Absolute, out result))
			{
				throw new ArgumentException("Not an absolute URL.", "url");
			}
			string_1 = string_3;
			int_0 = 302;
			string_2 = "Found";
		}

		public void SetCookie(Cookie cookie_0)
		{
			if (cookie_0 == null)
			{
				throw new ArgumentNullException("cookie");
			}
			if (!canAddOrUpdate(cookie_0))
			{
				throw new ArgumentException("Cannot be replaced.", "cookie");
			}
			CookieCollection_0.Add(cookie_0);
		}
	}
}
