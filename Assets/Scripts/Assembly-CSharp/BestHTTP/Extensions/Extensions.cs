using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace BestHTTP.Extensions
{
	public static class Extensions
	{
		[CompilerGenerated]
		private static Func<char, bool> func_0;

		[CompilerGenerated]
		private static Func<char, bool> func_1;

		public static string AsciiToString(this byte[] byte_0)
		{
			StringBuilder stringBuilder = new StringBuilder(byte_0.Length);
			foreach (byte b in byte_0)
			{
				stringBuilder.Append((char)((b > 127) ? 63 : b));
			}
			return stringBuilder.ToString();
		}

		public static byte[] GetASCIIBytes(this string string_0)
		{
			byte[] array = new byte[string_0.Length];
			for (int i = 0; i < string_0.Length; i++)
			{
				char c = string_0[i];
				array[i] = (byte)((c >= '\u0080') ? '?' : c);
			}
			return array;
		}

		public static void SendAsASCII(this BinaryWriter binaryWriter_0, string string_0)
		{
			foreach (char c in string_0)
			{
				binaryWriter_0.Write((byte)((c >= '\u0080') ? '?' : c));
			}
		}

		public static void WriteLine(this FileStream fileStream_0)
		{
			fileStream_0.Write(HTTPRequest.byte_0, 0, 2);
		}

		public static void WriteLine(this FileStream fileStream_0, string string_0)
		{
			byte[] aSCIIBytes = string_0.GetASCIIBytes();
			fileStream_0.Write(aSCIIBytes, 0, aSCIIBytes.Length);
			fileStream_0.WriteLine();
		}

		public static void WriteLine(this FileStream fileStream_0, string string_0, params object[] object_0)
		{
			byte[] aSCIIBytes = string.Format(string_0, object_0).GetASCIIBytes();
			fileStream_0.Write(aSCIIBytes, 0, aSCIIBytes.Length);
			fileStream_0.WriteLine();
		}

		public static string[] FindOption(this string string_0, string string_1)
		{
			string[] array = string_0.ToLower().Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			string_1 = string_1.ToLower();
			int num = 0;
			while (true)
			{
				if (num < array.Length)
				{
					if (array[num].Contains(string_1))
					{
						break;
					}
					num++;
					continue;
				}
				return null;
			}
			return array[num].Split(new char[1] { '=' }, StringSplitOptions.RemoveEmptyEntries);
		}

		public static int ToInt32(this string string_0, int int_0 = 0)
		{
			if (string_0 == null)
			{
				return int_0;
			}
			try
			{
				return int.Parse(string_0);
			}
			catch
			{
				return int_0;
			}
		}

		public static long ToInt64(this string string_0, long long_0 = 0L)
		{
			if (string_0 == null)
			{
				return long_0;
			}
			try
			{
				return long.Parse(string_0);
			}
			catch
			{
				return long_0;
			}
		}

		public static DateTime ToDateTime(this string string_0, [Optional] DateTime dateTime_0)
		{
			if (string_0 == null)
			{
				return dateTime_0;
			}
			try
			{
				DateTime.TryParse(string_0, out dateTime_0);
				return dateTime_0.ToUniversalTime();
			}
			catch
			{
				return dateTime_0;
			}
		}

		public static string ToStrOrEmpty(this string string_0)
		{
			if (string_0 == null)
			{
				return string.Empty;
			}
			return string_0;
		}

		public static string CalculateMD5Hash(this string string_0)
		{
			return string_0.GetASCIIBytes().CalculateMD5Hash();
		}

		public static string CalculateMD5Hash(this byte[] byte_0)
		{
			byte[] array = MD5.Create().ComputeHash(byte_0);
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array2 = array;
			foreach (byte b in array2)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		internal static string Read(this string string_0, ref int int_0, char char_0, bool bool_0 = true)
		{
			return string_0.Read(ref int_0, (char char_1) => char_1 != char_0, bool_0);
		}

		internal static string Read(this string string_0, ref int int_0, Func<char, bool> func_2, bool bool_0 = true)
		{
			if (int_0 >= string_0.Length)
			{
				return string.Empty;
			}
			string_0.SkipWhiteSpace(ref int_0);
			int num = int_0;
			while (int_0 < string_0.Length && func_2(string_0[int_0]))
			{
				int_0++;
			}
			string result = ((!bool_0) ? null : string_0.Substring(num, int_0 - num));
			int_0++;
			return result;
		}

		internal static string ReadQuotedText(this string string_0, ref int int_0)
		{
			string empty = string.Empty;
			if (string_0 == null)
			{
				return empty;
			}
			if (string_0[int_0] == '"')
			{
				string_0.Read(ref int_0, '"', false);
				empty = string_0.Read(ref int_0, '"');
				string_0.Read(ref int_0, ',', false);
			}
			else
			{
				empty = string_0.Read(ref int_0, ',');
			}
			return empty;
		}

		internal static void SkipWhiteSpace(this string string_0, ref int int_0)
		{
			if (int_0 < string_0.Length)
			{
				while (int_0 < string_0.Length && char.IsWhiteSpace(string_0[int_0]))
				{
					int_0++;
				}
			}
		}

		internal static string TrimAndLower(this string string_0)
		{
			if (string_0 == null)
			{
				return null;
			}
			char[] array = new char[string_0.Length];
			int length = 0;
			foreach (char c in string_0)
			{
				if (!char.IsWhiteSpace(c) && !char.IsControl(c))
				{
					array[length++] = char.ToLowerInvariant(c);
				}
			}
			return new string(array, 0, length);
		}

		internal static List<KeyValuePair> ParseOptionalHeader(this string string_0)
		{
			List<KeyValuePair> list = new List<KeyValuePair>();
			if (string_0 == null)
			{
				return list;
			}
			int int_ = 0;
			while (int_ < string_0.Length)
			{
				string string_ = string_0.Read(ref int_, (char char_0) => char_0 != '=' && char_0 != ',').TrimAndLower();
				KeyValuePair keyValuePair = new KeyValuePair(string_);
				if (string_0[int_ - 1] == '=')
				{
					keyValuePair.String_1 = string_0.ReadQuotedText(ref int_);
				}
				list.Add(keyValuePair);
			}
			return list;
		}

		internal static List<KeyValuePair> ParseQualityParams(this string string_0)
		{
			List<KeyValuePair> list = new List<KeyValuePair>();
			if (string_0 == null)
			{
				return list;
			}
			int int_ = 0;
			while (int_ < string_0.Length)
			{
				string string_ = string_0.Read(ref int_, (char char_0) => char_0 != ',' && char_0 != ';').TrimAndLower();
				KeyValuePair keyValuePair = new KeyValuePair(string_);
				if (string_0[int_ - 1] == ';')
				{
					string_0.Read(ref int_, '=', false);
					keyValuePair.String_1 = string_0.Read(ref int_, ',');
				}
				list.Add(keyValuePair);
			}
			return list;
		}

		public static void ReadBuffer(this Stream stream_0, byte[] byte_0)
		{
			int num = 0;
			do
			{
				num += stream_0.Read(byte_0, num, byte_0.Length - num);
			}
			while (num < byte_0.Length);
		}

		public static void WriteAll(this MemoryStream memoryStream_0, byte[] byte_0)
		{
			memoryStream_0.Write(byte_0, 0, byte_0.Length);
		}

		public static void WriteString(this MemoryStream memoryStream_0, string string_0)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(string_0);
			memoryStream_0.WriteAll(bytes);
		}

		public static void WriteLine(this MemoryStream memoryStream_0)
		{
			memoryStream_0.WriteAll(HTTPRequest.byte_0);
		}

		public static void WriteLine(this MemoryStream memoryStream_0, string string_0)
		{
			memoryStream_0.WriteString(string_0);
			memoryStream_0.WriteLine();
		}
	}
}
