using System;
using System.Runtime.InteropServices;

namespace BestHTTP.PlatformSupport.Cryptography
{
	[ComVisible(true)]
	public sealed class MD5CryptoServiceProvider : MD5
	{
		private const int int_0 = 64;

		private uint[] uint_0;

		private uint[] uint_1;

		private ulong ulong_0;

		private byte[] byte_0;

		private int int_1;

		private static readonly uint[] uint_2 = new uint[64]
		{
			3614090360u, 3905402710u, 606105819u, 3250441966u, 4118548399u, 1200080426u, 2821735955u, 4249261313u, 1770035416u, 2336552879u,
			4294925233u, 2304563134u, 1804603682u, 4254626195u, 2792965006u, 1236535329u, 4129170786u, 3225465664u, 643717713u, 3921069994u,
			3593408605u, 38016083u, 3634488961u, 3889429448u, 568446438u, 3275163606u, 4107603335u, 1163531501u, 2850285829u, 4243563512u,
			1735328473u, 2368359562u, 4294588738u, 2272392833u, 1839030562u, 4259657740u, 2763975236u, 1272893353u, 4139469664u, 3200236656u,
			681279174u, 3936430074u, 3572445317u, 76029189u, 3654602809u, 3873151461u, 530742520u, 3299628645u, 4096336452u, 1126891415u,
			2878612391u, 4237533241u, 1700485571u, 2399980690u, 4293915773u, 2240044497u, 1873313359u, 4264355552u, 2734768916u, 1309151649u,
			4149444226u, 3174756917u, 718787259u, 3951481745u
		};

		public MD5CryptoServiceProvider()
		{
			uint_0 = new uint[4];
			uint_1 = new uint[16];
			byte_0 = new byte[64];
			Initialize();
		}

		~MD5CryptoServiceProvider()
		{
			Dispose(false);
		}

		protected override void Dispose(bool disposing)
		{
			if (byte_0 != null)
			{
				Array.Clear(byte_0, 0, byte_0.Length);
				byte_0 = null;
			}
			if (uint_0 != null)
			{
				Array.Clear(uint_0, 0, uint_0.Length);
				uint_0 = null;
			}
			if (uint_1 != null)
			{
				Array.Clear(uint_1, 0, uint_1.Length);
				uint_1 = null;
			}
		}

		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			State = 1;
			if (int_1 != 0)
			{
				if (cbSize < 64 - int_1)
				{
					Buffer.BlockCopy(array, ibStart, byte_0, int_1, cbSize);
					int_1 += cbSize;
					return;
				}
				int num = 64 - int_1;
				Buffer.BlockCopy(array, ibStart, byte_0, int_1, num);
				ProcessBlock(byte_0, 0);
				int_1 = 0;
				ibStart += num;
				cbSize -= num;
			}
			for (int num = 0; num < cbSize - cbSize % 64; num += 64)
			{
				ProcessBlock(array, ibStart + num);
			}
			if (cbSize % 64 != 0)
			{
				Buffer.BlockCopy(array, cbSize - cbSize % 64 + ibStart, byte_0, 0, cbSize % 64);
				int_1 = cbSize % 64;
			}
		}

		protected override byte[] HashFinal()
		{
			byte[] array = new byte[16];
			ProcessFinalBlock(byte_0, 0, int_1);
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					array[i * 4 + j] = (byte)(uint_0[i] >> j * 8);
				}
			}
			return array;
		}

		public override void Initialize()
		{
			ulong_0 = 0uL;
			int_1 = 0;
			uint_0[0] = 1732584193u;
			uint_0[1] = 4023233417u;
			uint_0[2] = 2562383102u;
			uint_0[3] = 271733878u;
		}

		private void ProcessBlock(byte[] byte_1, int int_2)
		{
			ulong_0 += 64uL;
			for (int i = 0; i < 16; i++)
			{
				uint_1[i] = (uint)(byte_1[int_2 + 4 * i] | (byte_1[int_2 + 4 * i + 1] << 8) | (byte_1[int_2 + 4 * i + 2] << 16) | (byte_1[int_2 + 4 * i + 3] << 24));
			}
			uint num = uint_0[0];
			uint num2 = uint_0[1];
			uint num3 = uint_0[2];
			uint num4 = uint_0[3];
			num += (((num3 ^ num4) & num2) ^ num4) + uint_2[0] + uint_1[0];
			num = (num << 7) | (num >> 25);
			num += num2;
			num4 += (((num2 ^ num3) & num) ^ num3) + uint_2[1] + uint_1[1];
			num4 = (num4 << 12) | (num4 >> 20);
			num4 += num;
			num3 += (((num ^ num2) & num4) ^ num2) + uint_2[2] + uint_1[2];
			num3 = (num3 << 17) | (num3 >> 15);
			num3 += num4;
			num2 += (((num4 ^ num) & num3) ^ num) + uint_2[3] + uint_1[3];
			num2 = (num2 << 22) | (num2 >> 10);
			num2 += num3;
			num += (((num3 ^ num4) & num2) ^ num4) + uint_2[4] + uint_1[4];
			num = (num << 7) | (num >> 25);
			num += num2;
			num4 += (((num2 ^ num3) & num) ^ num3) + uint_2[5] + uint_1[5];
			num4 = (num4 << 12) | (num4 >> 20);
			num4 += num;
			num3 += (((num ^ num2) & num4) ^ num2) + uint_2[6] + uint_1[6];
			num3 = (num3 << 17) | (num3 >> 15);
			num3 += num4;
			num2 += (((num4 ^ num) & num3) ^ num) + uint_2[7] + uint_1[7];
			num2 = (num2 << 22) | (num2 >> 10);
			num2 += num3;
			num += (((num3 ^ num4) & num2) ^ num4) + uint_2[8] + uint_1[8];
			num = (num << 7) | (num >> 25);
			num += num2;
			num4 += (((num2 ^ num3) & num) ^ num3) + uint_2[9] + uint_1[9];
			num4 = (num4 << 12) | (num4 >> 20);
			num4 += num;
			num3 += (((num ^ num2) & num4) ^ num2) + uint_2[10] + uint_1[10];
			num3 = (num3 << 17) | (num3 >> 15);
			num3 += num4;
			num2 += (((num4 ^ num) & num3) ^ num) + uint_2[11] + uint_1[11];
			num2 = (num2 << 22) | (num2 >> 10);
			num2 += num3;
			num += (((num3 ^ num4) & num2) ^ num4) + uint_2[12] + uint_1[12];
			num = (num << 7) | (num >> 25);
			num += num2;
			num4 += (((num2 ^ num3) & num) ^ num3) + uint_2[13] + uint_1[13];
			num4 = (num4 << 12) | (num4 >> 20);
			num4 += num;
			num3 += (((num ^ num2) & num4) ^ num2) + uint_2[14] + uint_1[14];
			num3 = (num3 << 17) | (num3 >> 15);
			num3 += num4;
			num2 += (((num4 ^ num) & num3) ^ num) + uint_2[15] + uint_1[15];
			num2 = (num2 << 22) | (num2 >> 10);
			num2 += num3;
			num += (((num2 ^ num3) & num4) ^ num3) + uint_2[16] + uint_1[1];
			num = (num << 5) | (num >> 27);
			num += num2;
			num4 += (((num ^ num2) & num3) ^ num2) + uint_2[17] + uint_1[6];
			num4 = (num4 << 9) | (num4 >> 23);
			num4 += num;
			num3 += (((num4 ^ num) & num2) ^ num) + uint_2[18] + uint_1[11];
			num3 = (num3 << 14) | (num3 >> 18);
			num3 += num4;
			num2 += (((num3 ^ num4) & num) ^ num4) + uint_2[19] + uint_1[0];
			num2 = (num2 << 20) | (num2 >> 12);
			num2 += num3;
			num += (((num2 ^ num3) & num4) ^ num3) + uint_2[20] + uint_1[5];
			num = (num << 5) | (num >> 27);
			num += num2;
			num4 += (((num ^ num2) & num3) ^ num2) + uint_2[21] + uint_1[10];
			num4 = (num4 << 9) | (num4 >> 23);
			num4 += num;
			num3 += (((num4 ^ num) & num2) ^ num) + uint_2[22] + uint_1[15];
			num3 = (num3 << 14) | (num3 >> 18);
			num3 += num4;
			num2 += (((num3 ^ num4) & num) ^ num4) + uint_2[23] + uint_1[4];
			num2 = (num2 << 20) | (num2 >> 12);
			num2 += num3;
			num += (((num2 ^ num3) & num4) ^ num3) + uint_2[24] + uint_1[9];
			num = (num << 5) | (num >> 27);
			num += num2;
			num4 += (((num ^ num2) & num3) ^ num2) + uint_2[25] + uint_1[14];
			num4 = (num4 << 9) | (num4 >> 23);
			num4 += num;
			num3 += (((num4 ^ num) & num2) ^ num) + uint_2[26] + uint_1[3];
			num3 = (num3 << 14) | (num3 >> 18);
			num3 += num4;
			num2 += (((num3 ^ num4) & num) ^ num4) + uint_2[27] + uint_1[8];
			num2 = (num2 << 20) | (num2 >> 12);
			num2 += num3;
			num += (((num2 ^ num3) & num4) ^ num3) + uint_2[28] + uint_1[13];
			num = (num << 5) | (num >> 27);
			num += num2;
			num4 += (((num ^ num2) & num3) ^ num2) + uint_2[29] + uint_1[2];
			num4 = (num4 << 9) | (num4 >> 23);
			num4 += num;
			num3 += (((num4 ^ num) & num2) ^ num) + uint_2[30] + uint_1[7];
			num3 = (num3 << 14) | (num3 >> 18);
			num3 += num4;
			num2 += (((num3 ^ num4) & num) ^ num4) + uint_2[31] + uint_1[12];
			num2 = (num2 << 20) | (num2 >> 12);
			num2 += num3;
			num += (num2 ^ num3 ^ num4) + uint_2[32] + uint_1[5];
			num = (num << 4) | (num >> 28);
			num += num2;
			num4 += (num ^ num2 ^ num3) + uint_2[33] + uint_1[8];
			num4 = (num4 << 11) | (num4 >> 21);
			num4 += num;
			num3 += (num4 ^ num ^ num2) + uint_2[34] + uint_1[11];
			num3 = (num3 << 16) | (num3 >> 16);
			num3 += num4;
			num2 += (num3 ^ num4 ^ num) + uint_2[35] + uint_1[14];
			num2 = (num2 << 23) | (num2 >> 9);
			num2 += num3;
			num += (num2 ^ num3 ^ num4) + uint_2[36] + uint_1[1];
			num = (num << 4) | (num >> 28);
			num += num2;
			num4 += (num ^ num2 ^ num3) + uint_2[37] + uint_1[4];
			num4 = (num4 << 11) | (num4 >> 21);
			num4 += num;
			num3 += (num4 ^ num ^ num2) + uint_2[38] + uint_1[7];
			num3 = (num3 << 16) | (num3 >> 16);
			num3 += num4;
			num2 += (num3 ^ num4 ^ num) + uint_2[39] + uint_1[10];
			num2 = (num2 << 23) | (num2 >> 9);
			num2 += num3;
			num += (num2 ^ num3 ^ num4) + uint_2[40] + uint_1[13];
			num = (num << 4) | (num >> 28);
			num += num2;
			num4 += (num ^ num2 ^ num3) + uint_2[41] + uint_1[0];
			num4 = (num4 << 11) | (num4 >> 21);
			num4 += num;
			num3 += (num4 ^ num ^ num2) + uint_2[42] + uint_1[3];
			num3 = (num3 << 16) | (num3 >> 16);
			num3 += num4;
			num2 += (num3 ^ num4 ^ num) + uint_2[43] + uint_1[6];
			num2 = (num2 << 23) | (num2 >> 9);
			num2 += num3;
			num += (num2 ^ num3 ^ num4) + uint_2[44] + uint_1[9];
			num = (num << 4) | (num >> 28);
			num += num2;
			num4 += (num ^ num2 ^ num3) + uint_2[45] + uint_1[12];
			num4 = (num4 << 11) | (num4 >> 21);
			num4 += num;
			num3 += (num4 ^ num ^ num2) + uint_2[46] + uint_1[15];
			num3 = (num3 << 16) | (num3 >> 16);
			num3 += num4;
			num2 += (num3 ^ num4 ^ num) + uint_2[47] + uint_1[2];
			num2 = (num2 << 23) | (num2 >> 9);
			num2 += num3;
			num += ((~num4 | num2) ^ num3) + uint_2[48] + uint_1[0];
			num = (num << 6) | (num >> 26);
			num += num2;
			num4 += ((~num3 | num) ^ num2) + uint_2[49] + uint_1[7];
			num4 = (num4 << 10) | (num4 >> 22);
			num4 += num;
			num3 += ((~num2 | num4) ^ num) + uint_2[50] + uint_1[14];
			num3 = (num3 << 15) | (num3 >> 17);
			num3 += num4;
			num2 += ((~num | num3) ^ num4) + uint_2[51] + uint_1[5];
			num2 = (num2 << 21) | (num2 >> 11);
			num2 += num3;
			num += ((~num4 | num2) ^ num3) + uint_2[52] + uint_1[12];
			num = (num << 6) | (num >> 26);
			num += num2;
			num4 += ((~num3 | num) ^ num2) + uint_2[53] + uint_1[3];
			num4 = (num4 << 10) | (num4 >> 22);
			num4 += num;
			num3 += ((~num2 | num4) ^ num) + uint_2[54] + uint_1[10];
			num3 = (num3 << 15) | (num3 >> 17);
			num3 += num4;
			num2 += ((~num | num3) ^ num4) + uint_2[55] + uint_1[1];
			num2 = (num2 << 21) | (num2 >> 11);
			num2 += num3;
			num += ((~num4 | num2) ^ num3) + uint_2[56] + uint_1[8];
			num = (num << 6) | (num >> 26);
			num += num2;
			num4 += ((~num3 | num) ^ num2) + uint_2[57] + uint_1[15];
			num4 = (num4 << 10) | (num4 >> 22);
			num4 += num;
			num3 += ((~num2 | num4) ^ num) + uint_2[58] + uint_1[6];
			num3 = (num3 << 15) | (num3 >> 17);
			num3 += num4;
			num2 += ((~num | num3) ^ num4) + uint_2[59] + uint_1[13];
			num2 = (num2 << 21) | (num2 >> 11);
			num2 += num3;
			num += ((~num4 | num2) ^ num3) + uint_2[60] + uint_1[4];
			num = (num << 6) | (num >> 26);
			num += num2;
			num4 += ((~num3 | num) ^ num2) + uint_2[61] + uint_1[11];
			num4 = (num4 << 10) | (num4 >> 22);
			num4 += num;
			num3 += ((~num2 | num4) ^ num) + uint_2[62] + uint_1[2];
			num3 = (num3 << 15) | (num3 >> 17);
			num3 += num4;
			num2 += ((~num | num3) ^ num4) + uint_2[63] + uint_1[9];
			num2 = (num2 << 21) | (num2 >> 11);
			num2 += num3;
			uint_0[0] += num;
			uint_0[1] += num2;
			uint_0[2] += num3;
			uint_0[3] += num4;
		}

		private void ProcessFinalBlock(byte[] byte_1, int int_2, int int_3)
		{
			ulong num = ulong_0 + (ulong)int_3;
			int num2 = (int)(56L - num % 64L);
			if (num2 < 1)
			{
				num2 += 64;
			}
			byte[] array = new byte[int_3 + num2 + 8];
			for (int i = 0; i < int_3; i++)
			{
				array[i] = byte_1[i + int_2];
			}
			array[int_3] = 128;
			for (int j = int_3 + 1; j < int_3 + num2; j++)
			{
				array[j] = 0;
			}
			ulong ulong_ = num << 3;
			AddLength(ulong_, array, int_3 + num2);
			ProcessBlock(array, 0);
			if (int_3 + num2 + 8 == 128)
			{
				ProcessBlock(array, 64);
			}
		}

		internal void AddLength(ulong ulong_1, byte[] byte_1, int int_2)
		{
			byte_1[int_2++] = (byte)ulong_1;
			byte_1[int_2++] = (byte)(ulong_1 >> 8);
			byte_1[int_2++] = (byte)(ulong_1 >> 16);
			byte_1[int_2++] = (byte)(ulong_1 >> 24);
			byte_1[int_2++] = (byte)(ulong_1 >> 32);
			byte_1[int_2++] = (byte)(ulong_1 >> 40);
			byte_1[int_2++] = (byte)(ulong_1 >> 48);
			byte_1[int_2] = (byte)(ulong_1 >> 56);
		}
	}
}
