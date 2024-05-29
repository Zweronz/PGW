using System;

namespace WebSocketSharp.Net
{
	public class NetworkCredential
	{
		private string string_0;

		private string string_1;

		private string[] string_2;

		private string string_3;

		public string String_0
		{
			get
			{
				return string_0 ?? string.Empty;
			}
			internal set
			{
				string_0 = value;
			}
		}

		public string String_1
		{
			get
			{
				return string_1 ?? string.Empty;
			}
			internal set
			{
				string_1 = value;
			}
		}

		public string[] String_2
		{
			get
			{
				return string_2;
			}
			internal set
			{
				string_2 = value;
			}
		}

		public string String_3
		{
			get
			{
				return string_3;
			}
			internal set
			{
				string_3 = value;
			}
		}

		public NetworkCredential(string string_4, string string_5)
			: this(string_4, string_5, null)
		{
		}

		public NetworkCredential(string string_4, string string_5, string string_6, params string[] string_7)
		{
			if (string_4 == null || string_4.Length == 0)
			{
				throw new ArgumentException("Must not be null or empty.", "username");
			}
			string_3 = string_4;
			string_1 = string_5;
			string_0 = string_6;
			string_2 = string_7;
		}
	}
}
