using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace WebSocketSharp.Net
{
	[Serializable]
	[ComVisible(true)]
	public class WebHeaderCollection : NameValueCollection, ISerializable
	{
		private static readonly Dictionary<string, HttpHeaderInfo> dictionary_0;

		private bool bool_0;

		private HttpHeaderType httpHeaderType_0;

		internal HttpHeaderType HttpHeaderType_0
		{
			get
			{
				return httpHeaderType_0;
			}
		}

		public override string[] AllKeys
		{
			get
			{
				return base.AllKeys;
			}
		}

		public override int Count
		{
			get
			{
				return base.Count;
			}
		}

		public string this[HttpRequestHeader header]
		{
			get
			{
				return Get(Convert(header));
			}
			set
			{
				Add(header, value);
			}
		}

		public string this[HttpResponseHeader header]
		{
			get
			{
				return Get(Convert(header));
			}
			set
			{
				Add(header, value);
			}
		}

		public override KeysCollection Keys
		{
			get
			{
				return base.Keys;
			}
		}

		internal WebHeaderCollection(HttpHeaderType httpHeaderType_1, bool bool_1)
		{
			httpHeaderType_0 = httpHeaderType_1;
			bool_0 = bool_1;
		}

		protected WebHeaderCollection(SerializationInfo serializationInfo_0, StreamingContext streamingContext_0)
		{
			if (serializationInfo_0 == null)
			{
				throw new ArgumentNullException("serializationInfo");
			}
			try
			{
				bool_0 = serializationInfo_0.GetBoolean("InternallyUsed");
				httpHeaderType_0 = (HttpHeaderType)serializationInfo_0.GetInt32("State");
				int @int = serializationInfo_0.GetInt32("Count");
				for (int i = 0; i < @int; i++)
				{
					base.Add(serializationInfo_0.GetString(i.ToString()), serializationInfo_0.GetString((@int + i).ToString()));
				}
			}
			catch (SerializationException ex)
			{
				throw new ArgumentException(ex.Message, "serializationInfo", ex);
			}
		}

		public WebHeaderCollection()
		{
		}

		static WebHeaderCollection()
		{
			dictionary_0 = new Dictionary<string, HttpHeaderInfo>(StringComparer.InvariantCultureIgnoreCase)
			{
				{
					"Accept",
					new HttpHeaderInfo("Accept", HttpHeaderType.Request | HttpHeaderType.Restricted | HttpHeaderType.MultiValue)
				},
				{
					"AcceptCharset",
					new HttpHeaderInfo("Accept-Charset", HttpHeaderType.Request | HttpHeaderType.MultiValue)
				},
				{
					"AcceptEncoding",
					new HttpHeaderInfo("Accept-Encoding", HttpHeaderType.Request | HttpHeaderType.MultiValue)
				},
				{
					"AcceptLanguage",
					new HttpHeaderInfo("Accept-Language", HttpHeaderType.Request | HttpHeaderType.MultiValue)
				},
				{
					"AcceptRanges",
					new HttpHeaderInfo("Accept-Ranges", HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					"Age",
					new HttpHeaderInfo("Age", HttpHeaderType.Response)
				},
				{
					"Allow",
					new HttpHeaderInfo("Allow", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					"Authorization",
					new HttpHeaderInfo("Authorization", HttpHeaderType.Request | HttpHeaderType.MultiValue)
				},
				{
					"CacheControl",
					new HttpHeaderInfo("Cache-Control", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					"Connection",
					new HttpHeaderInfo("Connection", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.Restricted | HttpHeaderType.MultiValue)
				},
				{
					"ContentEncoding",
					new HttpHeaderInfo("Content-Encoding", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					"ContentLanguage",
					new HttpHeaderInfo("Content-Language", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					"ContentLength",
					new HttpHeaderInfo("Content-Length", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.Restricted)
				},
				{
					"ContentLocation",
					new HttpHeaderInfo("Content-Location", HttpHeaderType.Request | HttpHeaderType.Response)
				},
				{
					"ContentMd5",
					new HttpHeaderInfo("Content-MD5", HttpHeaderType.Request | HttpHeaderType.Response)
				},
				{
					"ContentRange",
					new HttpHeaderInfo("Content-Range", HttpHeaderType.Request | HttpHeaderType.Response)
				},
				{
					"ContentType",
					new HttpHeaderInfo("Content-Type", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.Restricted)
				},
				{
					"Cookie",
					new HttpHeaderInfo("Cookie", HttpHeaderType.Request)
				},
				{
					"Cookie2",
					new HttpHeaderInfo("Cookie2", HttpHeaderType.Request)
				},
				{
					"Date",
					new HttpHeaderInfo("Date", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.Restricted)
				},
				{
					"Expect",
					new HttpHeaderInfo("Expect", HttpHeaderType.Request | HttpHeaderType.Restricted | HttpHeaderType.MultiValue)
				},
				{
					"Expires",
					new HttpHeaderInfo("Expires", HttpHeaderType.Request | HttpHeaderType.Response)
				},
				{
					"ETag",
					new HttpHeaderInfo("ETag", HttpHeaderType.Response)
				},
				{
					"From",
					new HttpHeaderInfo("From", HttpHeaderType.Request)
				},
				{
					"Host",
					new HttpHeaderInfo("Host", HttpHeaderType.Request | HttpHeaderType.Restricted)
				},
				{
					"IfMatch",
					new HttpHeaderInfo("If-Match", HttpHeaderType.Request | HttpHeaderType.MultiValue)
				},
				{
					"IfModifiedSince",
					new HttpHeaderInfo("If-Modified-Since", HttpHeaderType.Request | HttpHeaderType.Restricted)
				},
				{
					"IfNoneMatch",
					new HttpHeaderInfo("If-None-Match", HttpHeaderType.Request | HttpHeaderType.MultiValue)
				},
				{
					"IfRange",
					new HttpHeaderInfo("If-Range", HttpHeaderType.Request)
				},
				{
					"IfUnmodifiedSince",
					new HttpHeaderInfo("If-Unmodified-Since", HttpHeaderType.Request)
				},
				{
					"KeepAlive",
					new HttpHeaderInfo("Keep-Alive", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					"LastModified",
					new HttpHeaderInfo("Last-Modified", HttpHeaderType.Request | HttpHeaderType.Response)
				},
				{
					"Location",
					new HttpHeaderInfo("Location", HttpHeaderType.Response)
				},
				{
					"MaxForwards",
					new HttpHeaderInfo("Max-Forwards", HttpHeaderType.Request)
				},
				{
					"Pragma",
					new HttpHeaderInfo("Pragma", HttpHeaderType.Request | HttpHeaderType.Response)
				},
				{
					"ProxyAuthenticate",
					new HttpHeaderInfo("Proxy-Authenticate", HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					"ProxyAuthorization",
					new HttpHeaderInfo("Proxy-Authorization", HttpHeaderType.Request)
				},
				{
					"ProxyConnection",
					new HttpHeaderInfo("Proxy-Connection", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.Restricted)
				},
				{
					"Public",
					new HttpHeaderInfo("Public", HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					"Range",
					new HttpHeaderInfo("Range", HttpHeaderType.Request | HttpHeaderType.Restricted | HttpHeaderType.MultiValue)
				},
				{
					"Referer",
					new HttpHeaderInfo("Referer", HttpHeaderType.Request | HttpHeaderType.Restricted)
				},
				{
					"RetryAfter",
					new HttpHeaderInfo("Retry-After", HttpHeaderType.Response)
				},
				{
					"SecWebSocketAccept",
					new HttpHeaderInfo("Sec-WebSocket-Accept", HttpHeaderType.Response | HttpHeaderType.Restricted)
				},
				{
					"SecWebSocketExtensions",
					new HttpHeaderInfo("Sec-WebSocket-Extensions", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.Restricted | HttpHeaderType.MultiValueInRequest)
				},
				{
					"SecWebSocketKey",
					new HttpHeaderInfo("Sec-WebSocket-Key", HttpHeaderType.Request | HttpHeaderType.Restricted)
				},
				{
					"SecWebSocketProtocol",
					new HttpHeaderInfo("Sec-WebSocket-Protocol", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValueInRequest)
				},
				{
					"SecWebSocketVersion",
					new HttpHeaderInfo("Sec-WebSocket-Version", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.Restricted | HttpHeaderType.MultiValueInResponse)
				},
				{
					"Server",
					new HttpHeaderInfo("Server", HttpHeaderType.Response)
				},
				{
					"SetCookie",
					new HttpHeaderInfo("Set-Cookie", HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					"SetCookie2",
					new HttpHeaderInfo("Set-Cookie2", HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					"Te",
					new HttpHeaderInfo("TE", HttpHeaderType.Request)
				},
				{
					"Trailer",
					new HttpHeaderInfo("Trailer", HttpHeaderType.Request | HttpHeaderType.Response)
				},
				{
					"TransferEncoding",
					new HttpHeaderInfo("Transfer-Encoding", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.Restricted | HttpHeaderType.MultiValue)
				},
				{
					"Translate",
					new HttpHeaderInfo("Translate", HttpHeaderType.Request)
				},
				{
					"Upgrade",
					new HttpHeaderInfo("Upgrade", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					"UserAgent",
					new HttpHeaderInfo("User-Agent", HttpHeaderType.Request | HttpHeaderType.Restricted)
				},
				{
					"Vary",
					new HttpHeaderInfo("Vary", HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					"Via",
					new HttpHeaderInfo("Via", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					"Warning",
					new HttpHeaderInfo("Warning", HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					"WwwAuthenticate",
					new HttpHeaderInfo("WWW-Authenticate", HttpHeaderType.Response | HttpHeaderType.Restricted | HttpHeaderType.MultiValue)
				}
			};
		}

		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\nversion=\"1\">\r\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\nversion=\"1\"\r\nFlags=\"SerializationFormatter\"/>\r\n</PermissionSet>\r\n")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		private void add(string string_0, string string_1, bool bool_1)
		{
			Action<string, string> action_ = ((!bool_1) ? new Action<string, string>(addWithoutCheckingName) : new Action<string, string>(addWithoutCheckingNameAndRestricted));
			doWithCheckingState(action_, checkName(string_0), string_1, true);
		}

		private void addWithoutCheckingName(string string_0, string string_1)
		{
			doWithoutCheckingName(base.Add, string_0, string_1);
		}

		private void addWithoutCheckingNameAndRestricted(string string_0, string string_1)
		{
			base.Add(string_0, checkValue(string_1));
		}

		private static int checkColonSeparated(string string_0)
		{
			int num = string_0.IndexOf(':');
			if (num == -1)
			{
				throw new ArgumentException("No colon could be found.", "header");
			}
			return num;
		}

		private static HttpHeaderType checkHeaderType(string string_0)
		{
			HttpHeaderInfo headerInfo = getHeaderInfo(string_0);
			return (headerInfo != null) ? ((headerInfo.Boolean_2 && !headerInfo.Boolean_3) ? HttpHeaderType.Request : ((!headerInfo.Boolean_2 && headerInfo.Boolean_3) ? HttpHeaderType.Response : HttpHeaderType.Unspecified)) : HttpHeaderType.Unspecified;
		}

		private static string checkName(string string_0)
		{
			if (string_0 != null && string_0.Length != 0)
			{
				string_0 = string_0.Trim();
				if (!IsHeaderName(string_0))
				{
					throw new ArgumentException("Contains invalid characters.", "name");
				}
				return string_0;
			}
			throw new ArgumentNullException("name");
		}

		private void checkRestricted(string string_0)
		{
			if (!bool_0 && isRestricted(string_0, true))
			{
				throw new ArgumentException("This header must be modified with the appropiate property.");
			}
		}

		private void checkState(bool bool_1)
		{
			if (httpHeaderType_0 != 0)
			{
				if (bool_1 && httpHeaderType_0 == HttpHeaderType.Request)
				{
					throw new InvalidOperationException("This collection has already been used to store the request headers.");
				}
				if (!bool_1 && httpHeaderType_0 == HttpHeaderType.Response)
				{
					throw new InvalidOperationException("This collection has already been used to store the response headers.");
				}
			}
		}

		private static string checkValue(string string_0)
		{
			if (string_0 != null && string_0.Length != 0)
			{
				string_0 = string_0.Trim();
				if (string_0.Length > 65535)
				{
					throw new ArgumentOutOfRangeException("value", "Greater than 65,535 characters.");
				}
				if (!IsHeaderValue(string_0))
				{
					throw new ArgumentException("Contains invalid characters.", "value");
				}
				return string_0;
			}
			return string.Empty;
		}

		private static string convert(string string_0)
		{
			HttpHeaderInfo value;
			return (!dictionary_0.TryGetValue(string_0, out value)) ? string.Empty : value.String_0;
		}

		private void doWithCheckingState(Action<string, string> action_0, string string_0, string string_1, bool bool_1)
		{
			switch (checkHeaderType(string_0))
			{
			case HttpHeaderType.Request:
				doWithCheckingState(action_0, string_0, string_1, false, bool_1);
				break;
			case HttpHeaderType.Response:
				doWithCheckingState(action_0, string_0, string_1, true, bool_1);
				break;
			default:
				action_0(string_0, string_1);
				break;
			}
		}

		private void doWithCheckingState(Action<string, string> action_0, string string_0, string string_1, bool bool_1, bool bool_2)
		{
			checkState(bool_1);
			action_0(string_0, string_1);
			if (bool_2 && httpHeaderType_0 == HttpHeaderType.Unspecified)
			{
				httpHeaderType_0 = ((!bool_1) ? HttpHeaderType.Request : HttpHeaderType.Response);
			}
		}

		private void doWithoutCheckingName(Action<string, string> action_0, string string_0, string string_1)
		{
			checkRestricted(string_0);
			action_0(string_0, checkValue(string_1));
		}

		private static HttpHeaderInfo getHeaderInfo(string string_0)
		{
			foreach (HttpHeaderInfo value in dictionary_0.Values)
			{
				if (value.String_0.Equals(string_0, StringComparison.InvariantCultureIgnoreCase))
				{
					return value;
				}
			}
			return null;
		}

		private static bool isRestricted(string string_0, bool bool_1)
		{
			HttpHeaderInfo headerInfo = getHeaderInfo(string_0);
			return headerInfo != null && headerInfo.IsRestricted(bool_1);
		}

		private void removeWithoutCheckingName(string string_0, string string_1)
		{
			checkRestricted(string_0);
			base.Remove(string_0);
		}

		private void setWithoutCheckingName(string string_0, string string_1)
		{
			doWithoutCheckingName(base.Set, string_0, string_1);
		}

		internal static string Convert(HttpRequestHeader httpRequestHeader_0)
		{
			return convert(httpRequestHeader_0.ToString());
		}

		internal static string Convert(HttpResponseHeader httpResponseHeader_0)
		{
			return convert(httpResponseHeader_0.ToString());
		}

		internal void InternalRemove(string string_0)
		{
			base.Remove(string_0);
		}

		internal void InternalSet(string string_0, bool bool_1)
		{
			int num = checkColonSeparated(string_0);
			InternalSet(string_0.Substring(0, num), string_0.Substring(num + 1), bool_1);
		}

		internal void InternalSet(string string_0, string string_1, bool bool_1)
		{
			string_1 = checkValue(string_1);
			if (IsMultiValue(string_0, bool_1))
			{
				base.Add(string_0, string_1);
			}
			else
			{
				base.Set(string_0, string_1);
			}
		}

		internal static bool IsHeaderName(string string_0)
		{
			return string_0 != null && string_0.Length > 0 && string_0.IsToken();
		}

		internal static bool IsHeaderValue(string string_0)
		{
			return string_0.IsText();
		}

		internal static bool IsMultiValue(string string_0, bool bool_1)
		{
			if (string_0 != null && string_0.Length != 0)
			{
				HttpHeaderInfo headerInfo = getHeaderInfo(string_0);
				return headerInfo != null && headerInfo.IsMultiValue(bool_1);
			}
			return false;
		}

		internal string ToStringMultiValue(bool bool_1)
		{
			StringBuilder stringBuilder_0 = new StringBuilder();
			Count.Times(delegate(int int_0)
			{
				string key = GetKey(int_0);
				if (IsMultiValue(key, bool_1))
				{
					string[] values = GetValues(int_0);
					foreach (string arg in values)
					{
						stringBuilder_0.AppendFormat("{0}: {1}\r\n", key, arg);
					}
				}
				else
				{
					stringBuilder_0.AppendFormat("{0}: {1}\r\n", key, Get(int_0));
				}
			});
			return stringBuilder_0.Append("\r\n").ToString();
		}

		protected void AddWithoutValidate(string string_0, string string_1)
		{
			add(string_0, string_1, true);
		}

		public void Add(string string_0)
		{
			if (string_0 == null || string_0.Length == 0)
			{
				throw new ArgumentNullException("header");
			}
			int num = checkColonSeparated(string_0);
			add(string_0.Substring(0, num), string_0.Substring(num + 1), false);
		}

		public void Add(HttpRequestHeader httpRequestHeader_0, string string_0)
		{
			doWithCheckingState(addWithoutCheckingName, Convert(httpRequestHeader_0), string_0, false, true);
		}

		public void Add(HttpResponseHeader httpResponseHeader_0, string string_0)
		{
			doWithCheckingState(addWithoutCheckingName, Convert(httpResponseHeader_0), string_0, true, true);
		}

		public override void Add(string name, string value)
		{
			add(name, value, false);
		}

		public override void Clear()
		{
			base.Clear();
			httpHeaderType_0 = HttpHeaderType.Unspecified;
		}

		public override string Get(int index)
		{
			return base.Get(index);
		}

		public override string Get(string name)
		{
			return base.Get(name);
		}

		public override IEnumerator GetEnumerator()
		{
			return base.GetEnumerator();
		}

		public override string GetKey(int index)
		{
			return base.GetKey(index);
		}

		public override string[] GetValues(int index)
		{
			string[] values = base.GetValues(index);
			return (values == null || values.Length <= 0) ? null : values;
		}

		public override string[] GetValues(string name)
		{
			string[] values = base.GetValues(name);
			return (values == null || values.Length <= 0) ? null : values;
		}

		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\nversion=\"1\">\r\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\nversion=\"1\"\r\nFlags=\"SerializationFormatter\"/>\r\n</PermissionSet>\r\n")]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("serializationInfo");
			}
			info.AddValue("InternallyUsed", bool_0);
			info.AddValue("State", (int)httpHeaderType_0);
			int int_2 = Count;
			info.AddValue("Count", int_2);
			int_2.Times(delegate(int int_1)
			{
				info.AddValue(int_1.ToString(), GetKey(int_1));
				info.AddValue((int_2 + int_1).ToString(), Get(int_1));
			});
		}

		public static bool IsRestricted(string string_0)
		{
			return isRestricted(checkName(string_0), false);
		}

		public static bool IsRestricted(string string_0, bool bool_1)
		{
			return isRestricted(checkName(string_0), bool_1);
		}

		public override void OnDeserialization(object sender)
		{
		}

		public void Remove(HttpRequestHeader httpRequestHeader_0)
		{
			doWithCheckingState(removeWithoutCheckingName, Convert(httpRequestHeader_0), null, false, false);
		}

		public void Remove(HttpResponseHeader httpResponseHeader_0)
		{
			doWithCheckingState(removeWithoutCheckingName, Convert(httpResponseHeader_0), null, true, false);
		}

		public override void Remove(string name)
		{
			doWithCheckingState(removeWithoutCheckingName, checkName(name), null, false);
		}

		public void Set(HttpRequestHeader httpRequestHeader_0, string string_0)
		{
			doWithCheckingState(setWithoutCheckingName, Convert(httpRequestHeader_0), string_0, false, true);
		}

		public void Set(HttpResponseHeader httpResponseHeader_0, string string_0)
		{
			doWithCheckingState(setWithoutCheckingName, Convert(httpResponseHeader_0), string_0, true, true);
		}

		public override void Set(string name, string value)
		{
			doWithCheckingState(setWithoutCheckingName, checkName(name), value, true);
		}

		public byte[] ToByteArray()
		{
			return Encoding.UTF8.GetBytes(ToString());
		}

		public override string ToString()
		{
			StringBuilder stringBuilder_0 = new StringBuilder();
			Count.Times(delegate(int int_0)
			{
				stringBuilder_0.AppendFormat("{0}: {1}\r\n", GetKey(int_0), Get(int_0));
			});
			return stringBuilder_0.Append("\r\n").ToString();
		}
	}
}
