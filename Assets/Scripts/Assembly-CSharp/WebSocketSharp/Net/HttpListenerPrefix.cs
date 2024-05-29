using System;
using System.Net;

namespace WebSocketSharp.Net
{
	internal sealed class HttpListenerPrefix
	{
		private IPAddress[] ipaddress_0;

		private string string_0;

		private HttpListener httpListener_0;

		private string string_1;

		private string string_2;

		private ushort ushort_0;

		private bool bool_0;

		public IPAddress[] IPAddress_0
		{
			get
			{
				return ipaddress_0;
			}
			set
			{
				ipaddress_0 = value;
			}
		}

		public string String_0
		{
			get
			{
				return string_0;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return bool_0;
			}
		}

		public HttpListener HttpListener_0
		{
			get
			{
				return httpListener_0;
			}
			set
			{
				httpListener_0 = value;
			}
		}

		public string String_1
		{
			get
			{
				return string_2;
			}
		}

		public int Int32_0
		{
			get
			{
				return ushort_0;
			}
		}

		internal HttpListenerPrefix(string string_3)
		{
			string_1 = string_3;
			parse(string_3);
		}

		private void parse(string string_3)
		{
			int num = ((!string_3.StartsWith("https://")) ? 80 : 443);
			if (num == 443)
			{
				bool_0 = true;
			}
			int length = string_3.Length;
			int num2 = string_3.IndexOf(':') + 3;
			int num3 = string_3.IndexOf(':', num2, length - num2);
			int num4 = 0;
			if (num3 > 0)
			{
				num4 = string_3.IndexOf('/', num3, length - num3);
				string_0 = string_3.Substring(num2, num3 - num2);
				ushort_0 = (ushort)int.Parse(string_3.Substring(num3 + 1, num4 - num3 - 1));
			}
			else
			{
				num4 = string_3.IndexOf('/', num2, length - num2);
				string_0 = string_3.Substring(num2, num4 - num2);
				ushort_0 = (ushort)num;
			}
			string_2 = string_3.Substring(num4);
			int length2 = string_2.Length;
			if (length2 > 1)
			{
				string_2 = string_2.Substring(0, length2 - 1);
			}
		}

		public static void CheckPrefix(string string_3)
		{
			if (string_3 == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			int length = string_3.Length;
			if (length == 0)
			{
				throw new ArgumentException("An empty string.");
			}
			if (!string_3.StartsWith("http://") && !string_3.StartsWith("https://"))
			{
				throw new ArgumentException("The scheme isn't 'http' or 'https'.");
			}
			int num = string_3.IndexOf(':') + 3;
			if (num >= length)
			{
				throw new ArgumentException("No host is specified.");
			}
			int num2 = string_3.IndexOf(':', num, length - num);
			if (num == num2)
			{
				throw new ArgumentException("No host is specified.");
			}
			if (num2 > 0)
			{
				int num3 = string_3.IndexOf('/', num2, length - num2);
				if (num3 == -1)
				{
					throw new ArgumentException("No path is specified.");
				}
				int result;
				if (!int.TryParse(string_3.Substring(num2 + 1, num3 - num2 - 1), out result) || !result.IsPortNumber())
				{
					throw new ArgumentException("An invalid port is specified.");
				}
			}
			else
			{
				int num4 = string_3.IndexOf('/', num, length - num);
				if (num4 == -1)
				{
					throw new ArgumentException("No path is specified.");
				}
			}
			if (string_3[length - 1] != '/')
			{
				throw new ArgumentException("Ends without '/'.");
			}
		}

		public override bool Equals(object obj)
		{
			HttpListenerPrefix httpListenerPrefix = obj as HttpListenerPrefix;
			return httpListenerPrefix != null && httpListenerPrefix.string_1 == string_1;
		}

		public override int GetHashCode()
		{
			return string_1.GetHashCode();
		}

		public override string ToString()
		{
			return string_1;
		}
	}
}
