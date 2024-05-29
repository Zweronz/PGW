using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace I2.Loc
{
	public class LocalizationReader
	{
		public static Dictionary<string, string> ReadTextAsset(TextAsset textAsset_0)
		{
			string @string = Encoding.UTF8.GetString(textAsset_0.bytes, 0, textAsset_0.bytes.Length);
			@string = @string.Replace("\r\n", "\n");
			StringReader stringReader = new StringReader(@string);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string string_;
			while ((string_ = stringReader.ReadLine()) != null)
			{
				string string_2;
				string string_3;
				string string_4;
				string string_5;
				string string_6;
				if (TextAsset_ReadLine(string_, out string_2, out string_3, out string_4, out string_5, out string_6) && !string.IsNullOrEmpty(string_2) && !string.IsNullOrEmpty(string_3))
				{
					dictionary[string_2] = string_3;
				}
			}
			return dictionary;
		}

		public static bool TextAsset_ReadLine(string string_0, out string string_1, out string string_2, out string string_3, out string string_4, out string string_5)
		{
			string_1 = string.Empty;
			string_3 = string.Empty;
			string_4 = string.Empty;
			string_5 = string.Empty;
			string_2 = string.Empty;
			int num = string_0.LastIndexOf("//");
			if (num >= 0)
			{
				string_4 = string_0.Substring(num + 2).Trim();
				string_4 = DecodeString(string_4);
				string_0 = string_0.Substring(0, num);
			}
			int num2 = string_0.IndexOf("=");
			if (num2 < 0)
			{
				return false;
			}
			string_1 = string_0.Substring(0, num2).Trim();
			string_2 = string_0.Substring(num2 + 1).Trim();
			string_2 = string_2.Replace("\r\n", "\n").Replace("\n", "\\n");
			string_2 = DecodeString(string_2);
			if (string_1.Length > 2 && string_1[0] == '[')
			{
				int num3 = string_1.IndexOf(']');
				if (num3 >= 0)
				{
					string_5 = string_1.Substring(1, num3 - 1);
					string_1 = string_1.Substring(num3 + 1);
				}
			}
			ValidateFullTerm(ref string_1);
			return true;
		}

		public static string ReadCSVfile(string string_0)
		{
			string text = string.Empty;
			using (StreamReader streamReader = File.OpenText(string_0))
			{
				text = streamReader.ReadToEnd();
			}
			return text.Replace("\r\n", "\n");
		}

		public static List<string[]> ReadCSV(string string_0)
		{
			int int_ = 0;
			List<string[]> list = new List<string[]>();
			while (int_ < string_0.Length)
			{
				string[] array = ParseCSVline(string_0, ref int_);
				if (array == null)
				{
					break;
				}
				list.Add(array);
			}
			return list;
		}

		private static string[] ParseCSVline(string string_0, ref int int_0)
		{
			List<string> list_ = new List<string>();
			int length = string_0.Length;
			int int_ = int_0;
			bool flag = false;
			for (; int_0 < length; int_0++)
			{
				char c = string_0[int_0];
				if (flag)
				{
					if (c != '"')
					{
						continue;
					}
					if (int_0 + 1 < length && string_0[int_0 + 1] == '"')
					{
						if (int_0 + 2 < length && string_0[int_0 + 2] == '"')
						{
							flag = false;
							int_0 += 2;
						}
						else
						{
							int_0++;
						}
					}
					else
					{
						flag = false;
					}
					continue;
				}
				switch (c)
				{
				case '"':
					flag = true;
					continue;
				case '\n':
				case ',':
					break;
				default:
					continue;
				}
				AddCSVtoken(ref list_, ref string_0, int_0, ref int_);
				if (c != '\n')
				{
					continue;
				}
				int_0++;
				break;
			}
			if (int_0 > int_)
			{
				AddCSVtoken(ref list_, ref string_0, int_0, ref int_);
			}
			return list_.ToArray();
		}

		private static void AddCSVtoken(ref List<string> list_0, ref string string_0, int int_0, ref int int_1)
		{
			string text = string_0.Substring(int_1, int_0 - int_1);
			int_1 = int_0 + 1;
			text = text.Replace("\"\"", "\"");
			if (text.Length > 1 && text[0] == '"' && text[text.Length - 1] == '"')
			{
				text = text.Substring(1, text.Length - 2);
			}
			list_0.Add(text);
		}

		public static void ValidateFullTerm(ref string string_0)
		{
			string_0 = string_0.Replace('\\', '/');
			int num = string_0.IndexOf('/');
			if (num >= 0)
			{
				int startIndex;
				while ((startIndex = string_0.LastIndexOf('/')) != num)
				{
					string_0 = string_0.Remove(startIndex, 1);
				}
			}
		}

		public static string EncodeString(string string_0)
		{
			if (string.IsNullOrEmpty(string_0))
			{
				return string.Empty;
			}
			return string_0.Replace("\r\n", "<\\n>").Replace("\r", "<\\n>").Replace("\n", "<\\n>");
		}

		public static string DecodeString(string string_0)
		{
			if (string.IsNullOrEmpty(string_0))
			{
				return string.Empty;
			}
			return string_0.Replace("<\\n>", "\r\n");
		}
	}
}
