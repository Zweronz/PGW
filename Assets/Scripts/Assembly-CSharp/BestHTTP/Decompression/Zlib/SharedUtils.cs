using System.IO;
using System.Text;

namespace BestHTTP.Decompression.Zlib
{
	internal class SharedUtils
	{
		public static int URShift(int int_0, int int_1)
		{
			return (int)((uint)int_0 >> int_1);
		}

		public static int ReadInput(TextReader textReader_0, byte[] byte_0, int int_0, int int_1)
		{
			if (byte_0.Length == 0)
			{
				return 0;
			}
			char[] array = new char[byte_0.Length];
			int num = textReader_0.Read(array, int_0, int_1);
			if (num == 0)
			{
				return -1;
			}
			for (int i = int_0; i < int_0 + num; i++)
			{
				byte_0[i] = (byte)array[i];
			}
			return num;
		}

		internal static byte[] ToByteArray(string string_0)
		{
			return Encoding.UTF8.GetBytes(string_0);
		}

		internal static char[] ToCharArray(byte[] byte_0)
		{
			return Encoding.UTF8.GetChars(byte_0);
		}
	}
}
