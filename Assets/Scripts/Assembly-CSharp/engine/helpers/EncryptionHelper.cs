using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using engine.protobuf;
using engine.system;

namespace engine.helpers
{
	public static class EncryptionHelper
	{
		public static readonly int int_0;

		private static readonly byte[] byte_0;

		static EncryptionHelper()
		{
			byte_0 = new byte[128];
			Random random = new Random();
			int_0 = random.Next();
			random.NextBytes(byte_0);
		}

		public static string ProcessXor(string string_0)
		{
			return Encoding.Unicode.GetString(ProcessXor(Encoding.Unicode.GetBytes(string_0)));
		}

		public static int ProcessXor(int int_1)
		{
			return int_1 ^ int_0;
		}

		public static long ProcessXor(long long_0)
		{
			return long_0 ^ int_0;
		}

		public static double ProcessXor(double double_0)
		{
			byte[] bytes = BitConverter.GetBytes(double_0);
			bytes = ProcessXor(bytes);
			return BitConverter.ToDouble(bytes, 0);
		}

		public static float ProcessXor(float float_0)
		{
			byte[] bytes = BitConverter.GetBytes(float_0);
			bytes = ProcessXor(bytes);
			return BitConverter.ToSingle(bytes, 0);
		}

		private static byte[] ProcessXor(byte[] byte_1)
		{
			byte[] array = new byte[byte_1.Length];
			for (int i = 0; i < byte_1.Length; i++)
			{
				array[i] = (byte)(byte_1[i] ^ byte_0[i % byte_0.Length]);
			}
			return array;
		}

		public static string ProcessXorASCII(string string_0)
		{
			string text = string.Empty;
			for (int i = 0; i < string_0.Length; i++)
			{
				int num = Convert.ToInt32(string_0[i]);
				num ^= int_0;
				text += char.ConvertFromUtf32(num);
			}
			return text;
		}

		public static string EncodeXorBase64(string string_0)
		{
			return Convert.ToBase64String(ProcessXor(Encoding.Unicode.GetBytes(string_0)));
		}

		public static string DecodeXorBase64(string string_0)
		{
			return Encoding.Unicode.GetString(ProcessXor(Convert.FromBase64String(string_0)));
		}

		public static string Md5Sum(string string_0, bool bool_0 = false)
		{
			using (MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider())
			{
				UTF8Encoding uTF8Encoding = new UTF8Encoding();
				byte[] bytes = uTF8Encoding.GetBytes(string_0);
				byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
				string text = string.Empty;
				for (int i = 0; i < array.Length; i++)
				{
					text += Convert.ToString(array[i], 16).PadLeft(2, '0');
				}
				text = text.PadLeft(32, '0');
				if (!bool_0)
				{
					return text;
				}
				return Convert.ToBase64String(uTF8Encoding.GetBytes(text));
			}
		}

		public static string Md5Sum(Stream stream_0, bool bool_0 = false)
		{
			using (MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider())
			{
				byte[] array = mD5CryptoServiceProvider.ComputeHash(stream_0);
				string text = string.Empty;
				for (int i = 0; i < array.Length; i++)
				{
					text += Convert.ToString(array[i], 16).PadLeft(2, '0');
				}
				text = text.PadLeft(32, '0');
				if (!bool_0)
				{
					return text;
				}
				UTF8Encoding uTF8Encoding = new UTF8Encoding();
				return Convert.ToBase64String(uTF8Encoding.GetBytes(text));
			}
		}

		public static string FileMd5Sum(string string_0)
		{
			using (FileStream stream_ = new FileStream(string_0, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				return Md5Sum(stream_);
			}
		}

		public static DictionaryProtoSerialize GenerateMd5AssemblyDllFiles(string string_0 = "")
		{
			DirectoryInfo directoryInfo = null;
			string_0 = ((!string.IsNullOrEmpty(string_0)) ? string_0 : BaseAppController.String_0);
			DirectoryInfo directory = new FileInfo(string_0).Directory;
			DirectoryInfo directoryInfo2 = directory.GetDirectories(Path.GetFileNameWithoutExtension(string_0) + "_Data")[0];
			directoryInfo = new DirectoryInfo(directoryInfo2.FullName + Path.DirectorySeparatorChar + "Managed");
			List<string> list = new List<string>();
			Regex regex = new Regex("^Assembly-.*\\.dll$", RegexOptions.IgnoreCase);
			string[] files = Directory.GetFiles(directoryInfo.FullName, "*.dll", SearchOption.TopDirectoryOnly);
			foreach (string text in files)
			{
				if (regex.IsMatch(Path.GetFileName(text)))
				{
					list.Add(text);
				}
			}
			DictionaryProtoSerialize dictionaryProtoSerialize = new DictionaryProtoSerialize();
			foreach (string item in list)
			{
				string gparam_ = FileMd5Sum(item);
				dictionaryProtoSerialize.Add(Path.GetFileName(item), WrapObjForProtobuf.Create(gparam_));
			}
			return dictionaryProtoSerialize;
		}
	}
}
