using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;
using WebSocketSharp.Server;

namespace WebSocketSharp
{
	public static class Ext
	{
		private const string string_0 = "()<>@,;:\\\"/[]?={} \t";

		private static readonly byte[] byte_0 = new byte[1];

		internal static readonly byte[] byte_1 = new byte[0];

		[CompilerGenerated]
		private static Func<string, bool> func_0;

		private static byte[] compress(this byte[] byte_2)
		{
			if (byte_2.LongLength == 0L)
			{
				return byte_2;
			}
			using (MemoryStream stream_ = new MemoryStream(byte_2))
			{
				return stream_.compressToArray();
			}
		}

		private static MemoryStream compress(this Stream stream_0)
		{
			MemoryStream memoryStream = new MemoryStream();
			if (stream_0.Length == 0L)
			{
				return memoryStream;
			}
			stream_0.Position = 0L;
			using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress, true))
			{
				stream_0.CopyTo(deflateStream);
				deflateStream.Close();
				memoryStream.Write(byte_0, 0, 1);
				memoryStream.Position = 0L;
				return memoryStream;
			}
		}

		private static byte[] compressToArray(this Stream stream_0)
		{
			using (MemoryStream memoryStream = stream_0.compress())
			{
				memoryStream.Close();
				return memoryStream.ToArray();
			}
		}

		private static byte[] decompress(this byte[] byte_2)
		{
			if (byte_2.LongLength == 0L)
			{
				return byte_2;
			}
			using (MemoryStream stream_ = new MemoryStream(byte_2))
			{
				return stream_.decompressToArray();
			}
		}

		private static MemoryStream decompress(this Stream stream_0)
		{
			MemoryStream memoryStream = new MemoryStream();
			if (stream_0.Length == 0L)
			{
				return memoryStream;
			}
			stream_0.Position = 0L;
			using (DeflateStream stream_ = new DeflateStream(stream_0, CompressionMode.Decompress, true))
			{
				stream_.CopyTo(memoryStream);
				memoryStream.Position = 0L;
				return memoryStream;
			}
		}

		private static byte[] decompressToArray(this Stream stream_0)
		{
			using (MemoryStream memoryStream = stream_0.decompress())
			{
				memoryStream.Close();
				return memoryStream.ToArray();
			}
		}

		private static byte[] readBytes(this Stream stream_0, byte[] byte_2, int int_0, int int_1)
		{
			int i = 0;
			try
			{
				i = stream_0.Read(byte_2, int_0, int_1);
				if (i < 1)
				{
					return byte_2.SubArray(0, int_0);
				}
				int num;
				for (; i < int_1; i += num)
				{
					num = stream_0.Read(byte_2, int_0 + i, int_1 - i);
					if (num < 1)
					{
						break;
					}
				}
			}
			catch
			{
			}
			return (i >= int_1) ? byte_2 : byte_2.SubArray(0, int_0 + i);
		}

		private static void times(this ulong ulong_0, Action action_0)
		{
			for (ulong num = 0uL; num < ulong_0; num++)
			{
				action_0();
			}
		}

		private static bool writeTo(this Stream stream_0, Stream stream_1, int int_0, byte[] byte_2)
		{
			byte[] array = stream_0.readBytes(byte_2, 0, int_0);
			int num = array.Length;
			stream_1.Write(array, 0, num);
			return num == int_0;
		}

		internal static byte[] Append(this ushort ushort_0, string string_1)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] buffer = ushort_0.InternalToByteArray(ByteOrder.Big);
				memoryStream.Write(buffer, 0, 2);
				if (string_1 != null && string_1.Length > 0)
				{
					buffer = Encoding.UTF8.GetBytes(string_1);
					memoryStream.Write(buffer, 0, buffer.Length);
				}
				memoryStream.Close();
				return memoryStream.ToArray();
			}
		}

		internal static string CheckIfAvailable(this ServerState serverState_0, bool bool_0, bool bool_1, bool bool_2)
		{
			return ((bool_0 || (serverState_0 != 0 && serverState_0 != ServerState.Stop)) && (bool_1 || serverState_0 != ServerState.Start) && (bool_2 || serverState_0 != ServerState.ShuttingDown)) ? null : ("This operation isn't available in: " + serverState_0);
		}

		internal static string CheckIfAvailable(this WebSocketState webSocketState_0, bool bool_0, bool bool_1, bool bool_2, bool bool_3)
		{
			return ((bool_0 || webSocketState_0 != 0) && (bool_1 || webSocketState_0 != WebSocketState.Open) && (bool_2 || webSocketState_0 != WebSocketState.Closing) && (bool_3 || webSocketState_0 != WebSocketState.Closed)) ? null : ("This operation isn't available in: " + webSocketState_0);
		}

		internal static string CheckIfCanRead(this Stream stream_0)
		{
			return (stream_0 == null) ? "'stream' is null." : (stream_0.CanRead ? null : "'stream' cannot be read.");
		}

		internal static string CheckIfValidProtocols(this string[] string_1)
		{
			return string_1.Contains((string string_2) => string_2 == null || string_1.Length == 0 || !string_2.IsToken()) ? "Contains an invalid value." : ((!string_1.ContainsTwice()) ? null : "Contains a value twice.");
		}

		internal static string CheckIfValidSendData(this byte[] byte_2)
		{
			return (byte_2 != null) ? null : "'data' is null.";
		}

		internal static string CheckIfValidSendData(this FileInfo fileInfo_0)
		{
			return (fileInfo_0 != null) ? null : "'file' is null.";
		}

		internal static string CheckIfValidSendData(this string string_1)
		{
			return (string_1 != null) ? null : "'data' is null.";
		}

		internal static string CheckIfValidServicePath(this string string_1)
		{
			return (string_1 == null || string_1.Length == 0) ? "'path' is null or empty." : ((string_1[0] != '/') ? "'path' isn't an absolute path." : ((string_1.IndexOfAny(new char[2] { '?', '#' }) <= -1) ? null : "'path' includes either or both query and fragment components."));
		}

		internal static string CheckIfValidSessionID(this string string_1)
		{
			return (string_1 == null || string_1.Length == 0) ? "'id' is null or empty." : null;
		}

		internal static string CheckIfValidWaitTime(this TimeSpan timeSpan_0)
		{
			return (!(timeSpan_0 <= TimeSpan.Zero)) ? null : "A wait time is zero or less.";
		}

		internal static void Close(this WebSocketSharp.Net.HttpListenerResponse httpListenerResponse_0, WebSocketSharp.Net.HttpStatusCode httpStatusCode_0)
		{
			httpListenerResponse_0.Int32_0 = (int)httpStatusCode_0;
			httpListenerResponse_0.Stream_0.Close();
		}

		internal static void CloseWithAuthChallenge(this WebSocketSharp.Net.HttpListenerResponse httpListenerResponse_0, string string_1)
		{
			httpListenerResponse_0.WebHeaderCollection_0.InternalSet("WWW-Authenticate", string_1, true);
			httpListenerResponse_0.Close(WebSocketSharp.Net.HttpStatusCode.Unauthorized);
		}

		internal static byte[] Compress(this byte[] byte_2, CompressionMethod compressionMethod_0)
		{
			return (compressionMethod_0 != CompressionMethod.Deflate) ? byte_2 : byte_2.compress();
		}

		internal static Stream Compress(this Stream stream_0, CompressionMethod compressionMethod_0)
		{
			return (compressionMethod_0 != CompressionMethod.Deflate) ? stream_0 : stream_0.compress();
		}

		internal static byte[] CompressToArray(this Stream stream_0, CompressionMethod compressionMethod_0)
		{
			return (compressionMethod_0 != CompressionMethod.Deflate) ? stream_0.ToByteArray() : stream_0.compressToArray();
		}

		internal static bool Contains<T>(this IEnumerable<T> ienumerable_0, Func<T, bool> func_1)
		{
			foreach (T item in ienumerable_0)
			{
				if (func_1(item))
				{
					return true;
				}
			}
			return false;
		}

		internal static bool ContainsTwice(this string[] string_1)
		{
			int int_2 = string_1.Length;
			Func<int, bool> func_0 = null;
			func_0 = delegate(int int_1)
			{
				if (int_1 < int_2 - 1)
				{
					int num = int_1 + 1;
					while (true)
					{
						if (num >= int_2)
						{
							return func_0(++int_1);
						}
						if (string_1[num] == string_1[int_1])
						{
							break;
						}
						num++;
					}
					return true;
				}
				return false;
			};
			return func_0(0);
		}

		internal static T[] Copy<T>(this T[] gparam_0, long long_0)
		{
			T[] array = new T[long_0];
			Array.Copy(gparam_0, 0L, array, 0L, long_0);
			return array;
		}

		internal static void CopyTo(this Stream stream_0, Stream stream_1)
		{
			int count = 1024;
			byte[] buffer = new byte[1024];
			int num = 0;
			while ((num = stream_0.Read(buffer, 0, count)) > 0)
			{
				stream_1.Write(buffer, 0, num);
			}
		}

		internal static byte[] Decompress(this byte[] byte_2, CompressionMethod compressionMethod_0)
		{
			return (compressionMethod_0 != CompressionMethod.Deflate) ? byte_2 : byte_2.decompress();
		}

		internal static Stream Decompress(this Stream stream_0, CompressionMethod compressionMethod_0)
		{
			return (compressionMethod_0 != CompressionMethod.Deflate) ? stream_0 : stream_0.decompress();
		}

		internal static byte[] DecompressToArray(this Stream stream_0, CompressionMethod compressionMethod_0)
		{
			return (compressionMethod_0 != CompressionMethod.Deflate) ? stream_0.ToByteArray() : stream_0.decompressToArray();
		}

		internal static bool EqualsWith(this int int_0, char char_0, Action<int> action_0)
		{
			action_0(int_0);
			return int_0 == char_0;
		}

		internal static string GetAbsolutePath(this Uri uri_0)
		{
			if (uri_0.IsAbsoluteUri)
			{
				return uri_0.AbsolutePath;
			}
			string originalString = uri_0.OriginalString;
			if (originalString[0] != '/')
			{
				return null;
			}
			int num = originalString.IndexOfAny(new char[2] { '?', '#' });
			return (num <= 0) ? originalString : originalString.Substring(0, num);
		}

		internal static string GetMessage(this CloseStatusCode closeStatusCode_0)
		{
			object result;
			switch (closeStatusCode_0)
			{
			case CloseStatusCode.ProtocolError:
				result = "A WebSocket protocol error has occurred.";
				break;
			case CloseStatusCode.UnsupportedData:
				result = "Unsupported data has been received.";
				break;
			case CloseStatusCode.Abnormal:
				result = "An exception has occurred.";
				break;
			case CloseStatusCode.InvalidData:
				result = "Invalid data has been received.";
				break;
			case CloseStatusCode.PolicyViolation:
				result = "A policy violation has occurred.";
				break;
			case CloseStatusCode.TooBig:
				result = "A too big message has been received.";
				break;
			case CloseStatusCode.MandatoryExtension:
				result = "WebSocket client didn't receive expected extension(s).";
				break;
			case CloseStatusCode.ServerError:
				result = "WebSocket server got an internal error.";
				break;
			case CloseStatusCode.TlsHandshakeFailure:
				result = "An error has occurred during a TLS handshake.";
				break;
			default:
				result = string.Empty;
				break;
			}
			return (string)result;
		}

		internal static string GetName(this string string_1, char char_0)
		{
			int num = string_1.IndexOf(char_0);
			return (num <= 0) ? null : string_1.Substring(0, num).Trim();
		}

		internal static string GetValue(this string string_1, char char_0)
		{
			int num = string_1.IndexOf(char_0);
			return (num <= -1 || num >= string_1.Length - 1) ? null : string_1.Substring(num + 1).Trim();
		}

		internal static string GetValue(this string string_1, char char_0, bool bool_0)
		{
			int num = string_1.IndexOf(char_0);
			if (num >= 0 && num != string_1.Length - 1)
			{
				string text = string_1.Substring(num + 1).Trim();
				return (!bool_0) ? text : text.Unquote();
			}
			return null;
		}

		internal static TcpListenerWebSocketContext GetWebSocketContext(this TcpClient tcpClient_0, string string_1, bool bool_0, ServerSslConfiguration serverSslConfiguration_0, Logger logger_0)
		{
			return new TcpListenerWebSocketContext(tcpClient_0, string_1, bool_0, serverSslConfiguration_0, logger_0);
		}

		internal static byte[] InternalToByteArray(this ushort ushort_0, ByteOrder byteOrder_0)
		{
			byte[] bytes = BitConverter.GetBytes(ushort_0);
			if (!byteOrder_0.IsHostOrder())
			{
				Array.Reverse(bytes);
			}
			return bytes;
		}

		internal static byte[] InternalToByteArray(this ulong ulong_0, ByteOrder byteOrder_0)
		{
			byte[] bytes = BitConverter.GetBytes(ulong_0);
			if (!byteOrder_0.IsHostOrder())
			{
				Array.Reverse(bytes);
			}
			return bytes;
		}

		internal static bool IsCompressionExtension(this string string_1, CompressionMethod compressionMethod_0)
		{
			return string_1.StartsWith(compressionMethod_0.ToExtensionString());
		}

		internal static bool IsPortNumber(this int int_0)
		{
			return int_0 > 0 && int_0 < 65536;
		}

		internal static bool IsReserved(this ushort ushort_0)
		{
			return ushort_0 == 1004 || ushort_0 == 1005 || ushort_0 == 1006 || ushort_0 == 1015;
		}

		internal static bool IsReserved(this CloseStatusCode closeStatusCode_0)
		{
			return closeStatusCode_0 == CloseStatusCode.Undefined || closeStatusCode_0 == CloseStatusCode.NoStatus || closeStatusCode_0 == CloseStatusCode.Abnormal || closeStatusCode_0 == CloseStatusCode.TlsHandshakeFailure;
		}

		internal static bool IsText(this string string_1)
		{
			int length = string_1.Length;
			int num = 0;
			while (true)
			{
				if (num < length)
				{
					char c = string_1[num];
					if (c < ' ' && !"\r\n\t".Contains(c))
					{
						break;
					}
					switch (c)
					{
					case '\n':
						if (++num < length)
						{
							c = string_1[num];
							if (!" \t".Contains(c))
							{
								return false;
							}
						}
						break;
					case '\u007f':
						return false;
					}
					num++;
					continue;
				}
				return true;
			}
			return false;
		}

		internal static bool IsToken(this string string_1)
		{
			int num = 0;
			while (true)
			{
				if (num < string_1.Length)
				{
					char c = string_1[num];
					if (c < ' ' || c >= '\u007f' || "()<>@,;:\\\"/[]?={} \t".Contains(c))
					{
						break;
					}
					num++;
					continue;
				}
				return true;
			}
			return false;
		}

		internal static string Quote(this string string_1)
		{
			return string.Format("\"{0}\"", string_1.Replace("\"", "\\\""));
		}

		internal static byte[] ReadBytes(this Stream stream_0, int int_0)
		{
			return stream_0.readBytes(new byte[int_0], 0, int_0);
		}

		internal static byte[] ReadBytes(this Stream stream_0, long long_0, int int_0)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				long num = long_0 / int_0;
				int num2 = (int)(long_0 % int_0);
				byte[] byte_ = new byte[int_0];
				bool flag = false;
				for (long num3 = 0L; num3 < num; num3++)
				{
					if (!stream_0.writeTo(memoryStream, int_0, byte_))
					{
						flag = true;
						break;
					}
				}
				if (!flag && num2 > 0)
				{
					stream_0.writeTo(memoryStream, num2, new byte[num2]);
				}
				memoryStream.Close();
				return memoryStream.ToArray();
			}
		}

		internal static void ReadBytesAsync(this Stream stream_0, int int_0, Action<byte[]> action_0, Action<Exception> action_1)
		{
			byte[] byte_0 = new byte[int_0];
			stream_0.BeginRead(byte_0, 0, int_0, delegate(IAsyncResult iasyncResult_0)
			{
				try
				{
					byte[] array = null;
					try
					{
						int num = stream_0.EndRead(iasyncResult_0);
						array = ((num < 1) ? byte_1 : ((num >= int_0) ? byte_0 : stream_0.readBytes(byte_0, num, int_0 - num)));
					}
					catch
					{
						array = byte_1;
					}
					if (action_0 != null)
					{
						action_0(array);
					}
				}
				catch (Exception obj2)
				{
					if (action_1 != null)
					{
						action_1(obj2);
					}
				}
			}, null);
		}

		internal static string RemovePrefix(this string string_1, params string[] string_2)
		{
			int num = 0;
			foreach (string text in string_2)
			{
				if (string_1.StartsWith(text))
				{
					num = text.Length;
					break;
				}
			}
			return (num <= 0) ? string_1 : string_1.Substring(num);
		}

		internal static T[] Reverse<T>(this T[] gparam_0)
		{
			int num = gparam_0.Length;
			T[] array = new T[num];
			int num2 = num - 1;
			for (int i = 0; i <= num2; i++)
			{
				array[i] = gparam_0[num2 - i];
			}
			return array;
		}

		internal static IEnumerable<string> SplitHeaderValue(this string string_1, params char[] char_0)
		{
			int length = string_1.Length;
			string string_2 = new string(char_0);
			StringBuilder stringBuilder = new StringBuilder(32);
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < length; i++)
			{
				char c = string_1[i];
				switch (c)
				{
				case '"':
					if (flag)
					{
						flag = !flag;
					}
					else
					{
						flag2 = !flag2;
					}
					goto IL_0136;
				case '\\':
					if (i < length - 1 && string_1[i + 1] == '"')
					{
						flag = true;
					}
					goto IL_0136;
				default:
					{
						if (string_2.Contains(c) && !flag2)
						{
							yield return stringBuilder.ToString();
							stringBuilder.Length = 0;
							break;
						}
						goto IL_0136;
					}
					IL_0136:
					stringBuilder.Append(c);
					break;
				}
			}
			if (stringBuilder.Length > 0)
			{
				yield return stringBuilder.ToString();
			}
		}

		internal static byte[] ToByteArray(this Stream stream_0)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				stream_0.Position = 0L;
				stream_0.CopyTo(memoryStream);
				memoryStream.Close();
				return memoryStream.ToArray();
			}
		}

		internal static CompressionMethod ToCompressionMethod(this string string_1)
		{
			foreach (byte value in Enum.GetValues(typeof(CompressionMethod)))
			{
				if (((CompressionMethod)value).ToExtensionString() == string_1)
				{
					return (CompressionMethod)value;
				}
			}
			return CompressionMethod.None;
		}

		internal static string ToExtensionString(this CompressionMethod compressionMethod_0, params string[] string_1)
		{
			if (compressionMethod_0 == CompressionMethod.None)
			{
				return string.Empty;
			}
			string text = string.Format("permessage-{0}", compressionMethod_0.ToString().ToLower());
			if (string_1 != null && string_1.Length != 0)
			{
				return string.Format("{0}; {1}", text, string_1.ToString("; "));
			}
			return text;
		}

		internal static IPAddress ToIPAddress(this string string_1)
		{
			try
			{
				return Dns.GetHostAddresses(string_1)[0];
			}
			catch
			{
				return null;
			}
		}

		internal static List<TSource> ToList<TSource>(this IEnumerable<TSource> ienumerable_0)
		{
			return new List<TSource>(ienumerable_0);
		}

		internal static ushort ToUInt16(this byte[] byte_2, ByteOrder byteOrder_0)
		{
			return BitConverter.ToUInt16(byte_2.ToHostOrder(byteOrder_0), 0);
		}

		internal static ulong ToUInt64(this byte[] byte_2, ByteOrder byteOrder_0)
		{
			return BitConverter.ToUInt64(byte_2.ToHostOrder(byteOrder_0), 0);
		}

		internal static string TrimEndSlash(this string string_1)
		{
			string_1 = string_1.TrimEnd('/');
			return (string_1.Length <= 0) ? "/" : string_1;
		}

		internal static bool TryCreateWebSocketUri(this string string_1, out Uri uri_0, out string string_2)
		{
			uri_0 = null;
			Uri uri = string_1.ToUri();
			if (uri == null)
			{
				string_2 = "An invalid URI string: " + string_1;
				return false;
			}
			if (!uri.IsAbsoluteUri)
			{
				string_2 = "Not an absolute URI: " + string_1;
				return false;
			}
			string scheme = uri.Scheme;
			if (!(scheme == "ws") && !(scheme == "wss"))
			{
				string_2 = "The scheme part isn't 'ws' or 'wss': " + string_1;
				return false;
			}
			if (uri.Fragment.Length > 0)
			{
				string_2 = "Includes the fragment component: " + string_1;
				return false;
			}
			int port = uri.Port;
			if (port == 0)
			{
				string_2 = "The port part is zero: " + string_1;
				return false;
			}
			uri_0 = ((port == -1) ? new Uri(string.Format("{0}://{1}:{2}{3}", scheme, uri.Host, (!(scheme == "ws")) ? 443 : 80, uri.PathAndQuery)) : uri);
			string_2 = string.Empty;
			return true;
		}

		internal static string Unquote(this string string_1)
		{
			int num = string_1.IndexOf('"');
			if (num < 0)
			{
				return string_1;
			}
			int num2 = string_1.LastIndexOf('"');
			int num3 = num2 - num - 1;
			return (num3 < 0) ? string_1 : ((num3 != 0) ? string_1.Substring(num + 1, num3).Replace("\\\"", "\"") : string.Empty);
		}

		internal static void WriteBytes(this Stream stream_0, byte[] byte_2)
		{
			using (MemoryStream stream_ = new MemoryStream(byte_2))
			{
				stream_.CopyTo(stream_0);
			}
		}

		public static bool Contains(this string string_1, params char[] char_0)
		{
			return char_0 == null || char_0.Length == 0 || (string_1 != null && string_1.Length != 0 && string_1.IndexOfAny(char_0) > -1);
		}

		public static bool Contains(this NameValueCollection nameValueCollection_0, string string_1)
		{
			return nameValueCollection_0 != null && nameValueCollection_0.Count > 0 && nameValueCollection_0[string_1] != null;
		}

		public static bool Contains(this NameValueCollection nameValueCollection_0, string string_1, string string_2)
		{
			if (nameValueCollection_0 != null && nameValueCollection_0.Count != 0)
			{
				string text = nameValueCollection_0[string_1];
				if (text == null)
				{
					return false;
				}
				string[] array = text.Split(',');
				int num = 0;
				while (true)
				{
					if (num < array.Length)
					{
						string text2 = array[num];
						if (text2.Trim().Equals(string_2, StringComparison.OrdinalIgnoreCase))
						{
							break;
						}
						num++;
						continue;
					}
					return false;
				}
				return true;
			}
			return false;
		}

		public static void Emit(this EventHandler eventHandler_0, object object_0, EventArgs eventArgs_0)
		{
			if (eventHandler_0 != null)
			{
				eventHandler_0(object_0, eventArgs_0);
			}
		}

		public static void Emit<TEventArgs>(this EventHandler<TEventArgs> eventHandler_0, object object_0, TEventArgs gparam_0) where TEventArgs : EventArgs
		{
			if (eventHandler_0 != null)
			{
				eventHandler_0(object_0, gparam_0);
			}
		}

		public static WebSocketSharp.Net.CookieCollection GetCookies(this NameValueCollection nameValueCollection_0, bool bool_0)
		{
			string text = ((!bool_0) ? "Cookie" : "Set-Cookie");
			return (nameValueCollection_0 == null || !nameValueCollection_0.Contains(text)) ? new WebSocketSharp.Net.CookieCollection() : WebSocketSharp.Net.CookieCollection.Parse(nameValueCollection_0[text], bool_0);
		}

		public static string GetDescription(this WebSocketSharp.Net.HttpStatusCode httpStatusCode_0)
		{
			return ((int)httpStatusCode_0).GetStatusDescription();
		}

		public static string GetStatusDescription(this int int_0)
		{
			switch (int_0)
			{
			case 400:
				return "Bad Request";
			case 401:
				return "Unauthorized";
			case 402:
				return "Payment Required";
			case 403:
				return "Forbidden";
			case 404:
				return "Not Found";
			case 405:
				return "Method Not Allowed";
			case 406:
				return "Not Acceptable";
			case 407:
				return "Proxy Authentication Required";
			case 408:
				return "Request Timeout";
			case 409:
				return "Conflict";
			case 410:
				return "Gone";
			case 411:
				return "Length Required";
			case 412:
				return "Precondition Failed";
			case 413:
				return "Request Entity Too Large";
			case 414:
				return "Request-Uri Too Long";
			case 415:
				return "Unsupported Media Type";
			case 416:
				return "Requested Range Not Satisfiable";
			case 417:
				return "Expectation Failed";
			case 300:
				return "Multiple Choices";
			case 301:
				return "Moved Permanently";
			case 302:
				return "Found";
			case 303:
				return "See Other";
			case 304:
				return "Not Modified";
			case 305:
				return "Use Proxy";
			case 500:
				return "Internal Server Error";
			case 501:
				return "Not Implemented";
			case 502:
				return "Bad Gateway";
			case 503:
				return "Service Unavailable";
			case 504:
				return "Gateway Timeout";
			case 505:
				return "Http Version Not Supported";
			default:
				return string.Empty;
			case 100:
				return "Continue";
			case 101:
				return "Switching Protocols";
			case 102:
				return "Processing";
			case 507:
				return "Insufficient Storage";
			case 307:
				return "Temporary Redirect";
			case 200:
				return "OK";
			case 201:
				return "Created";
			case 202:
				return "Accepted";
			case 203:
				return "Non-Authoritative Information";
			case 204:
				return "No Content";
			case 205:
				return "Reset Content";
			case 206:
				return "Partial Content";
			case 207:
				return "Multi-Status";
			case 422:
				return "Unprocessable Entity";
			case 423:
				return "Locked";
			case 424:
				return "Failed Dependency";
			}
		}

		public static bool IsCloseStatusCode(this ushort ushort_0)
		{
			return ushort_0 > 999 && ushort_0 < 5000;
		}

		public static bool IsEnclosedIn(this string string_1, char char_0)
		{
			return string_1 != null && string_1.Length > 1 && string_1[0] == char_0 && string_1[string_1.Length - 1] == char_0;
		}

		public static bool IsHostOrder(this ByteOrder byteOrder_0)
		{
			return !(BitConverter.IsLittleEndian ^ (byteOrder_0 == ByteOrder.Little));
		}

		public static bool IsLocal(this IPAddress ipaddress_0)
		{
			if (ipaddress_0 == null)
			{
				return false;
			}
			if (!ipaddress_0.Equals(IPAddress.Any) && !IPAddress.IsLoopback(ipaddress_0))
			{
				string hostName = Dns.GetHostName();
				IPAddress[] hostAddresses = Dns.GetHostAddresses(hostName);
				IPAddress[] array = hostAddresses;
				int num = 0;
				while (true)
				{
					if (num < array.Length)
					{
						IPAddress other = array[num];
						if (ipaddress_0.Equals(other))
						{
							break;
						}
						num++;
						continue;
					}
					return false;
				}
				return true;
			}
			return true;
		}

		public static bool IsNullOrEmpty(this string string_1)
		{
			return string_1 == null || string_1.Length == 0;
		}

		public static bool IsPredefinedScheme(this string string_1)
		{
			if (string_1 != null && string_1.Length >= 2)
			{
				char c = string_1[0];
				if (c == 'h')
				{
					return string_1 == "http" || string_1 == "https";
				}
				if (c == 'w')
				{
					return string_1 == "ws" || string_1 == "wss";
				}
				if (c == 'f')
				{
					return string_1 == "file" || string_1 == "ftp";
				}
				if (c == 'n')
				{
					c = string_1[1];
					return (c != 'e') ? (string_1 == "nntp") : (string_1 == "news" || string_1 == "net.pipe" || string_1 == "net.tcp");
				}
				return (c == 'g' && string_1 == "gopher") || (c == 'm' && string_1 == "mailto");
			}
			return false;
		}

		public static bool IsUpgradeTo(this WebSocketSharp.Net.HttpListenerRequest httpListenerRequest_0, string string_1)
		{
			if (httpListenerRequest_0 == null)
			{
				throw new ArgumentNullException("request");
			}
			if (string_1 == null)
			{
				throw new ArgumentNullException("protocol");
			}
			if (string_1.Length == 0)
			{
				throw new ArgumentException("An empty string.", "protocol");
			}
			return httpListenerRequest_0.NameValueCollection_0.Contains("Upgrade", string_1) && httpListenerRequest_0.NameValueCollection_0.Contains("Connection", "Upgrade");
		}

		public static bool MaybeUri(this string string_1)
		{
			if (string_1 != null && string_1.Length != 0)
			{
				int num = string_1.IndexOf(':');
				if (num == -1)
				{
					return false;
				}
				if (num >= 10)
				{
					return false;
				}
				return string_1.Substring(0, num).IsPredefinedScheme();
			}
			return false;
		}

		public static T[] SubArray<T>(this T[] gparam_0, int int_0, int int_1)
		{
			int num;
			if (gparam_0 != null && (num = gparam_0.Length) != 0)
			{
				if (int_0 >= 0 && int_1 > 0 && int_0 + int_1 <= num)
				{
					if (int_0 == 0 && int_1 == num)
					{
						return gparam_0;
					}
					T[] array = new T[int_1];
					Array.Copy(gparam_0, int_0, array, 0, int_1);
					return array;
				}
				return new T[0];
			}
			return new T[0];
		}

		public static T[] SubArray<T>(this T[] gparam_0, long long_0, long long_1)
		{
			long longLength;
			if (gparam_0 != null && (longLength = gparam_0.LongLength) != 0L)
			{
				if (long_0 >= 0L && long_1 > 0L && long_0 + long_1 <= longLength)
				{
					if (long_0 == 0L && long_1 == longLength)
					{
						return gparam_0;
					}
					T[] array = new T[long_1];
					Array.Copy(gparam_0, long_0, array, 0L, long_1);
					return array;
				}
				return new T[0];
			}
			return new T[0];
		}

		public static void Times(this int int_0, Action action_0)
		{
			if (int_0 > 0 && action_0 != null)
			{
				((ulong)int_0).times(action_0);
			}
		}

		public static void Times(this long long_0, Action action_0)
		{
			if (long_0 > 0L && action_0 != null)
			{
				((ulong)long_0).times(action_0);
			}
		}

		public static void Times(this uint uint_0, Action action_0)
		{
			if (uint_0 != 0 && action_0 != null)
			{
				times(uint_0, action_0);
			}
		}

		public static void Times(this ulong ulong_0, Action action_0)
		{
			if (ulong_0 > 0L && action_0 != null)
			{
				ulong_0.times(action_0);
			}
		}

		public static void Times(this int int_0, Action<int> action_0)
		{
			if (int_0 > 0 && action_0 != null)
			{
				for (int i = 0; i < int_0; i++)
				{
					action_0(i);
				}
			}
		}

		public static void Times(this long long_0, Action<long> action_0)
		{
			if (long_0 > 0L && action_0 != null)
			{
				for (long num = 0L; num < long_0; num++)
				{
					action_0(num);
				}
			}
		}

		public static void Times(this uint uint_0, Action<uint> action_0)
		{
			if (uint_0 != 0 && action_0 != null)
			{
				for (uint num = 0u; num < uint_0; num++)
				{
					action_0(num);
				}
			}
		}

		public static void Times(this ulong ulong_0, Action<ulong> action_0)
		{
			if (ulong_0 > 0L && action_0 != null)
			{
				for (ulong num = 0uL; num < ulong_0; num++)
				{
					action_0(num);
				}
			}
		}

		public static T To<T>(this byte[] byte_2, ByteOrder byteOrder_0) where T : struct
		{
			if (byte_2 == null)
			{
				throw new ArgumentNullException("source");
			}
			if (byte_2.Length == 0)
			{
				return default(T);
			}
			Type typeFromHandle = typeof(T);
			byte[] value = byte_2.ToHostOrder(byteOrder_0);
			return (typeFromHandle == typeof(bool)) ? ((T)(object)BitConverter.ToBoolean(value, 0)) : ((typeFromHandle == typeof(char)) ? ((T)(object)BitConverter.ToChar(value, 0)) : ((typeFromHandle == typeof(double)) ? ((T)(object)BitConverter.ToDouble(value, 0)) : ((typeFromHandle == typeof(short)) ? ((T)(object)BitConverter.ToInt16(value, 0)) : ((typeFromHandle == typeof(int)) ? ((T)(object)BitConverter.ToInt32(value, 0)) : ((typeFromHandle == typeof(long)) ? ((T)(object)BitConverter.ToInt64(value, 0)) : ((typeFromHandle == typeof(float)) ? ((T)(object)BitConverter.ToSingle(value, 0)) : ((typeFromHandle == typeof(ushort)) ? ((T)(object)BitConverter.ToUInt16(value, 0)) : ((typeFromHandle == typeof(uint)) ? ((T)(object)BitConverter.ToUInt32(value, 0)) : ((typeFromHandle != typeof(ulong)) ? default(T) : ((T)(object)BitConverter.ToUInt64(value, 0)))))))))));
		}

		public static byte[] ToByteArray<T>(this T gparam_0, ByteOrder byteOrder_0) where T : struct
		{
			Type typeFromHandle = typeof(T);
			byte[] array = ((typeFromHandle == typeof(bool)) ? BitConverter.GetBytes((bool)(object)gparam_0) : ((typeFromHandle == typeof(byte)) ? new byte[1] { (byte)(object)gparam_0 } : ((typeFromHandle == typeof(char)) ? BitConverter.GetBytes((char)(object)gparam_0) : ((typeFromHandle == typeof(double)) ? BitConverter.GetBytes((double)(object)gparam_0) : ((typeFromHandle == typeof(short)) ? BitConverter.GetBytes((short)(object)gparam_0) : ((typeFromHandle == typeof(int)) ? BitConverter.GetBytes((int)(object)gparam_0) : ((typeFromHandle == typeof(long)) ? BitConverter.GetBytes((long)(object)gparam_0) : ((typeFromHandle == typeof(float)) ? BitConverter.GetBytes((float)(object)gparam_0) : ((typeFromHandle == typeof(ushort)) ? BitConverter.GetBytes((ushort)(object)gparam_0) : ((typeFromHandle == typeof(uint)) ? BitConverter.GetBytes((uint)(object)gparam_0) : ((typeFromHandle != typeof(ulong)) ? byte_1 : BitConverter.GetBytes((ulong)(object)gparam_0))))))))))));
			if (array.Length > 1 && !byteOrder_0.IsHostOrder())
			{
				Array.Reverse(array);
			}
			return array;
		}

		public static byte[] ToHostOrder(this byte[] byte_2, ByteOrder byteOrder_0)
		{
			if (byte_2 == null)
			{
				throw new ArgumentNullException("source");
			}
			return (byte_2.Length <= 1 || byteOrder_0.IsHostOrder()) ? byte_2 : byte_2.Reverse();
		}

		public static string ToString<T>(this T[] gparam_0, string string_1)
		{
			if (gparam_0 == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = gparam_0.Length;
			if (num == 0)
			{
				return string.Empty;
			}
			if (string_1 == null)
			{
				string_1 = string.Empty;
			}
			StringBuilder buff = new StringBuilder(64);
			(num - 1).Times(delegate(int int_0)
			{
				buff.AppendFormat("{0}{1}", gparam_0[int_0].ToString(), string_1);
			});
			buff.Append(gparam_0[num - 1].ToString());
			return buff.ToString();
		}

		public static Uri ToUri(this string string_1)
		{
			Uri result;
			Uri.TryCreate(string_1, string_1.MaybeUri() ? UriKind.Absolute : UriKind.Relative, out result);
			return result;
		}

		public static string UrlDecode(this string string_1)
		{
			return (string_1 == null || string_1.Length <= 0) ? string_1 : HttpUtility.UrlDecode(string_1);
		}

		public static string UrlEncode(this string string_1)
		{
			return (string_1 == null || string_1.Length <= 0) ? string_1 : HttpUtility.UrlEncode(string_1);
		}

		public static void WriteContent(this WebSocketSharp.Net.HttpListenerResponse httpListenerResponse_0, byte[] byte_2)
		{
			if (httpListenerResponse_0 == null)
			{
				throw new ArgumentNullException("response");
			}
			if (byte_2 == null)
			{
				throw new ArgumentNullException("content");
			}
			long longLength = byte_2.LongLength;
			if (longLength == 0L)
			{
				httpListenerResponse_0.Close();
				return;
			}
			httpListenerResponse_0.Int64_0 = longLength;
			Stream stream_ = httpListenerResponse_0.Stream_0;
			if (longLength <= 2147483647L)
			{
				stream_.Write(byte_2, 0, (int)longLength);
			}
			else
			{
				stream_.WriteBytes(byte_2);
			}
			stream_.Close();
		}
	}
}
