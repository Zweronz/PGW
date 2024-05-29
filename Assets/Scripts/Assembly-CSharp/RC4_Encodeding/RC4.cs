using System.Linq;

namespace RC4_Encodeding
{
	public class RC4
	{
		private byte[] byte_0 = new byte[256];

		private int int_0;

		private int int_1;

		public RC4(byte[] byte_1)
		{
			init(byte_1);
		}

		private void init(byte[] byte_1)
		{
			int num = byte_1.Length;
			for (int i = 0; i < 256; i++)
			{
				byte_0[i] = (byte)i;
			}
			int num2 = 0;
			for (int j = 0; j < 256; j++)
			{
				num2 = (num2 + byte_0[j] + byte_1[j % num]) % 256;
				byte_0.Swap(j, num2);
			}
		}

		public byte[] Encode(byte[] byte_1, int int_2)
		{
			byte[] array = byte_1.Take(int_2).ToArray();
			byte[] array2 = new byte[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = (byte)(array[i] ^ keyItem());
			}
			return array2;
		}

		public byte[] Decode(byte[] byte_1, int int_2)
		{
			return Encode(byte_1, int_2);
		}

		private byte keyItem()
		{
			int_0 = (int_0 + 1) % 256;
			int_1 = (int_1 + byte_0[int_0]) % 256;
			byte_0.Swap(int_0, int_1);
			return byte_0[(byte_0[int_0] + byte_0[int_1]) % 256];
		}
	}
}
