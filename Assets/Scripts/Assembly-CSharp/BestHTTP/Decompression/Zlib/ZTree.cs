using System;

namespace BestHTTP.Decompression.Zlib
{
	internal sealed class ZTree
	{
		internal const int int_0 = 16;

		private static readonly int int_1 = 2 * InternalConstants.int_5 + 1;

		internal static readonly int[] int_2 = new int[29]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1, 2, 2, 2, 2, 3, 3, 3, 3,
			4, 4, 4, 4, 5, 5, 5, 5, 0
		};

		internal static readonly int[] int_3 = new int[30]
		{
			0, 0, 0, 0, 1, 1, 2, 2, 3, 3,
			4, 4, 5, 5, 6, 6, 7, 7, 8, 8,
			9, 9, 10, 10, 11, 11, 12, 12, 13, 13
		};

		internal static readonly int[] int_4 = new int[19]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 2, 3, 7
		};

		internal static readonly sbyte[] sbyte_0 = new sbyte[19]
		{
			16, 17, 18, 0, 8, 7, 9, 6, 10, 5,
			11, 4, 12, 3, 13, 2, 14, 1, 15
		};

		private static readonly sbyte[] sbyte_1 = new sbyte[512]
		{
			0, 1, 2, 3, 4, 4, 5, 5, 6, 6,
			6, 6, 7, 7, 7, 7, 8, 8, 8, 8,
			8, 8, 8, 8, 9, 9, 9, 9, 9, 9,
			9, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 11, 11,
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11, 12, 12, 12, 12, 12, 12,
			12, 12, 12, 12, 12, 12, 12, 12, 12, 12,
			12, 12, 12, 12, 12, 12, 12, 12, 12, 12,
			12, 12, 12, 12, 12, 12, 13, 13, 13, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 0, 0, 16, 17,
			18, 18, 19, 19, 20, 20, 20, 20, 21, 21,
			21, 21, 22, 22, 22, 22, 22, 22, 22, 22,
			23, 23, 23, 23, 23, 23, 23, 23, 24, 24,
			24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
			24, 24, 24, 24, 25, 25, 25, 25, 25, 25,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29
		};

		internal static readonly sbyte[] sbyte_2 = new sbyte[256]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 8,
			9, 9, 10, 10, 11, 11, 12, 12, 12, 12,
			13, 13, 13, 13, 14, 14, 14, 14, 15, 15,
			15, 15, 16, 16, 16, 16, 16, 16, 16, 16,
			17, 17, 17, 17, 17, 17, 17, 17, 18, 18,
			18, 18, 18, 18, 18, 18, 19, 19, 19, 19,
			19, 19, 19, 19, 20, 20, 20, 20, 20, 20,
			20, 20, 20, 20, 20, 20, 20, 20, 20, 20,
			21, 21, 21, 21, 21, 21, 21, 21, 21, 21,
			21, 21, 21, 21, 21, 21, 22, 22, 22, 22,
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 24, 24,
			24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
			24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
			24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			25, 25, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 28
		};

		internal static readonly int[] int_5 = new int[29]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 10,
			12, 14, 16, 20, 24, 28, 32, 40, 48, 56,
			64, 80, 96, 112, 128, 160, 192, 224, 0
		};

		internal static readonly int[] int_6 = new int[30]
		{
			0, 1, 2, 3, 4, 6, 8, 12, 16, 24,
			32, 48, 64, 96, 128, 192, 256, 384, 512, 768,
			1024, 1536, 2048, 3072, 4096, 6144, 8192, 12288, 16384, 24576
		};

		internal short[] short_0;

		internal int int_7;

		internal StaticTree staticTree_0;

		internal static int DistanceCode(int int_8)
		{
			return (int_8 >= 256) ? sbyte_1[256 + SharedUtils.URShift(int_8, 7)] : sbyte_1[int_8];
		}

		internal void gen_bitlen(DeflateManager deflateManager_0)
		{
			short[] array = short_0;
			short[] short_ = staticTree_0.short_2;
			int[] array2 = staticTree_0.int_0;
			int num = staticTree_0.int_1;
			int num2 = staticTree_0.int_3;
			int num3 = 0;
			for (int i = 0; i <= InternalConstants.int_0; i++)
			{
				deflateManager_0.short_5[i] = 0;
			}
			array[deflateManager_0.int_40[deflateManager_0.int_42] * 2 + 1] = 0;
			int j;
			for (j = deflateManager_0.int_42 + 1; j < int_1; j++)
			{
				int num4 = deflateManager_0.int_40[j];
				int i = array[array[num4 * 2 + 1] * 2 + 1] + 1;
				if (i > num2)
				{
					i = num2;
					num3++;
				}
				array[num4 * 2 + 1] = (short)i;
				if (num4 <= int_7)
				{
					deflateManager_0.short_5[i]++;
					int num5 = 0;
					if (num4 >= num)
					{
						num5 = array2[num4 - num];
					}
					short num6 = array[num4 * 2];
					deflateManager_0.int_47 += num6 * (i + num5);
					if (short_ != null)
					{
						deflateManager_0.int_48 += num6 * (short_[num4 * 2 + 1] + num5);
					}
				}
			}
			if (num3 == 0)
			{
				return;
			}
			do
			{
				int i = num2 - 1;
				while (deflateManager_0.short_5[i] == 0)
				{
					i--;
				}
				deflateManager_0.short_5[i]--;
				deflateManager_0.short_5[i + 1] = (short)(deflateManager_0.short_5[i + 1] + 2);
				deflateManager_0.short_5[num2]--;
				num3 -= 2;
			}
			while (num3 > 0);
			for (int i = num2; i != 0; i--)
			{
				int num4 = deflateManager_0.short_5[i];
				while (num4 != 0)
				{
					int num7 = deflateManager_0.int_40[--j];
					if (num7 <= int_7)
					{
						if (array[num7 * 2 + 1] != i)
						{
							deflateManager_0.int_47 = (int)(deflateManager_0.int_47 + ((long)i - (long)array[num7 * 2 + 1]) * array[num7 * 2]);
							array[num7 * 2 + 1] = (short)i;
						}
						num4--;
					}
				}
			}
		}

		internal void build_tree(DeflateManager deflateManager_0)
		{
			short[] array = short_0;
			short[] short_ = staticTree_0.short_2;
			int num = staticTree_0.int_2;
			int num2 = -1;
			deflateManager_0.int_41 = 0;
			deflateManager_0.int_42 = int_1;
			for (int i = 0; i < num; i++)
			{
				if (array[i * 2] != 0)
				{
					num2 = (deflateManager_0.int_40[++deflateManager_0.int_41] = i);
					deflateManager_0.sbyte_1[i] = 0;
				}
				else
				{
					array[i * 2 + 1] = 0;
				}
			}
			int num3;
			while (deflateManager_0.int_41 < 2)
			{
				num3 = (deflateManager_0.int_40[++deflateManager_0.int_41] = ((num2 < 2) ? (++num2) : 0));
				array[num3 * 2] = 1;
				deflateManager_0.sbyte_1[num3] = 0;
				deflateManager_0.int_47--;
				if (short_ != null)
				{
					deflateManager_0.int_48 -= short_[num3 * 2 + 1];
				}
			}
			int_7 = num2;
			for (int i = deflateManager_0.int_41 / 2; i >= 1; i--)
			{
				deflateManager_0.pqdownheap(array, i);
			}
			num3 = num;
			do
			{
				int i = deflateManager_0.int_40[1];
				deflateManager_0.int_40[1] = deflateManager_0.int_40[deflateManager_0.int_41--];
				deflateManager_0.pqdownheap(array, 1);
				int num4 = deflateManager_0.int_40[1];
				deflateManager_0.int_40[--deflateManager_0.int_42] = i;
				deflateManager_0.int_40[--deflateManager_0.int_42] = num4;
				array[num3 * 2] = (short)(array[i * 2] + array[num4 * 2]);
				deflateManager_0.sbyte_1[num3] = (sbyte)(Math.Max((byte)deflateManager_0.sbyte_1[i], (byte)deflateManager_0.sbyte_1[num4]) + 1);
				array[i * 2 + 1] = (array[num4 * 2 + 1] = (short)num3);
				deflateManager_0.int_40[1] = num3++;
				deflateManager_0.pqdownheap(array, 1);
			}
			while (deflateManager_0.int_41 >= 2);
			deflateManager_0.int_40[--deflateManager_0.int_42] = deflateManager_0.int_40[1];
			gen_bitlen(deflateManager_0);
			gen_codes(array, num2, deflateManager_0.short_5);
		}

		internal static void gen_codes(short[] short_1, int int_8, short[] short_2)
		{
			short[] array = new short[InternalConstants.int_0 + 1];
			short num = 0;
			for (int i = 1; i <= InternalConstants.int_0; i++)
			{
				num = (array[i] = (short)(num + short_2[i - 1] << 1));
			}
			for (int j = 0; j <= int_8; j++)
			{
				int num2 = short_1[j * 2 + 1];
				if (num2 != 0)
				{
					short_1[j * 2] = (short)bi_reverse(array[num2]++, num2);
				}
			}
		}

		internal static int bi_reverse(int int_8, int int_9)
		{
			int num = 0;
			do
			{
				num |= int_8 & 1;
				int_8 >>= 1;
				num <<= 1;
			}
			while (--int_9 > 0);
			return num >> 1;
		}
	}
}
