using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Threading;
using WebSocketSharp.Net;

namespace WebSocketSharp
{
	internal abstract class HttpBase
	{
		private const int int_0 = 8192;

		protected const string string_0 = "\r\n";

		private NameValueCollection nameValueCollection_0;

		private Version version_0;

		internal byte[] byte_0;

		public string String_0
		{
			get
			{
				if (byte_0 != null && byte_0.LongLength != 0L)
				{
					Encoding encoding = null;
					string text = nameValueCollection_0["Content-Type"];
					if (text != null && text.Length > 0)
					{
						encoding = HttpUtility.GetEncoding(text);
					}
					return (encoding ?? Encoding.UTF8).GetString(byte_0);
				}
				return string.Empty;
			}
		}

		public NameValueCollection NameValueCollection_0
		{
			get
			{
				return nameValueCollection_0;
			}
		}

		public Version Version_0
		{
			get
			{
				return version_0;
			}
		}

		protected HttpBase(Version version_1, NameValueCollection nameValueCollection_1)
		{
			version_0 = version_1;
			nameValueCollection_0 = nameValueCollection_1;
		}

		private static byte[] readEntityBody(Stream stream_0, string string_1)
		{
			long result;
			if (!long.TryParse(string_1, out result))
			{
				throw new ArgumentException("Cannot be parsed.", "length");
			}
			if (result < 0L)
			{
				throw new ArgumentOutOfRangeException("length", "Less than zero.");
			}
			return (result > 1024L) ? stream_0.ReadBytes(result, 1024) : ((result <= 0L) ? null : stream_0.ReadBytes((int)result));
		}

		private static string[] readHeaders(Stream stream_0, int int_1)
		{
			List<byte> list_0 = new List<byte>();
			int int_2 = 0;
			Action<int> action_ = delegate(int int_3)
			{
				if (int_3 == -1)
				{
					throw new EndOfStreamException("The header cannot be read from the data source.");
				}
				list_0.Add((byte)int_3);
				int_3++;
			};
			bool flag = false;
			while (int_2 < int_1)
			{
				if (stream_0.ReadByte().EqualsWith('\r', action_) && stream_0.ReadByte().EqualsWith('\n', action_) && stream_0.ReadByte().EqualsWith('\r', action_) && stream_0.ReadByte().EqualsWith('\n', action_))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				throw new WebSocketException("The length of header part is greater than the max length.");
			}
			return Encoding.UTF8.GetString(list_0.ToArray()).Replace("\r\n ", " ").Replace("\r\n\t", " ")
				.Split(new string[1] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
		}

		protected static T Read<T>(Stream stream_0, Func<string[], T> func_0, int int_1) where T : HttpBase
		{
			bool timeout = false;
			Timer timer = new Timer(delegate
			{
				timeout = true;
				stream_0.Close();
			}, null, int_1, -1);
			T val = (T)null;
			Exception ex = null;
			try
			{
				val = func_0(readHeaders(stream_0, 8192));
				string text = val.NameValueCollection_0["Content-Length"];
				if (text != null && text.Length > 0)
				{
					val.byte_0 = readEntityBody(stream_0, text);
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			finally
			{
				timer.Change(-1, -1);
				timer.Dispose();
			}
			string text2 = (timeout ? "A timeout has occurred while reading an HTTP request/response." : ((ex == null) ? null : "An exception has occurred while reading an HTTP request/response."));
			if (text2 != null)
			{
				throw new WebSocketException(text2, ex);
			}
			return val;
		}

		public byte[] ToByteArray()
		{
			return Encoding.UTF8.GetBytes(ToString());
		}
	}
}
