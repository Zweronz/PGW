using System;
using System.Text.RegularExpressions;
using engine.helpers;

namespace engine.filesystem
{
	public struct FileName
	{
		private static Regex regex_0 = new Regex("/[^/]*$");

		private static Regex regex_1 = new Regex("\\\\");

		private static string string_0 = "ts=";

		private static string string_1 = "?ts=";

		private string string_2;

		private uint uint_0;

		private string string_3;

		public uint UInt32_0
		{
			get
			{
				return uint_0;
			}
		}

		public int Int32_0
		{
			get
			{
				return Convert.ToInt32(uint_0);
			}
		}

		public string String_0
		{
			get
			{
				return uint_0.ToString();
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
				return string_2 + string_0 + uint_0;
			}
		}

		public string String_3
		{
			get
			{
				return string_2 + string_0;
			}
		}

		public string String_4
		{
			get
			{
				if (string_3 != null && string_3.Length != 0)
				{
					return string_3;
				}
				return string_2;
			}
		}

		public string String_5
		{
			get
			{
				return regex_0.Replace(string_2, string.Empty);
			}
		}

		public FileName(string string_4, uint uint_1, string string_5 = "")
		{
			string_2 = string_4;
			uint_0 = uint_1;
			string_3 = string_5;
		}

		public static bool CreateFileNameFromUrl(string string_4, out FileName fileName_0)
		{
			string text = string_1;
			int num = string_4.IndexOf(text);
			if (num == -1)
			{
				text = string_0;
				num = string_4.IndexOf(text);
			}
			if (num != -1)
			{
				uint result = 0u;
				if (uint.TryParse(string_4.Substring(num + text.Length), out result))
				{
					string_4 = regex_1.Replace(string_4.Substring(0, num), "/");
					fileName_0 = new FileName(string_4, result, string.Empty);
					return true;
				}
				string text2 = "Filed to parse ts for " + string_4;
				Log.AddLine(text2);
			}
			fileName_0 = default(FileName);
			return false;
		}

		public override string ToString()
		{
			return string.Format("[Filename: {0}]", String_2);
		}
	}
}
