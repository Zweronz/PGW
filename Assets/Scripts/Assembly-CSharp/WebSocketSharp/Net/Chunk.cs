using System;

namespace WebSocketSharp.Net
{
	internal class Chunk
	{
		private byte[] byte_0;

		private int int_0;

		public int Int32_0
		{
			get
			{
				return byte_0.Length - int_0;
			}
		}

		public Chunk(byte[] byte_1)
		{
			byte_0 = byte_1;
		}

		public int Read(byte[] byte_1, int int_1, int int_2)
		{
			int num = byte_0.Length - int_0;
			if (num == 0)
			{
				return num;
			}
			if (int_2 > num)
			{
				int_2 = num;
			}
			Buffer.BlockCopy(byte_0, int_0, byte_1, int_1, int_2);
			int_0 += int_2;
			return int_2;
		}
	}
}
