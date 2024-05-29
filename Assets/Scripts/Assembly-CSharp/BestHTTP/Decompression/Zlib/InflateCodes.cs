using System;

namespace BestHTTP.Decompression.Zlib
{
	internal sealed class InflateCodes
	{
		private const int int_0 = 0;

		private const int int_1 = 1;

		private const int int_2 = 2;

		private const int int_3 = 3;

		private const int int_4 = 4;

		private const int int_5 = 5;

		private const int int_6 = 6;

		private const int int_7 = 7;

		private const int int_8 = 8;

		private const int int_9 = 9;

		internal int int_10;

		internal int int_11;

		internal int[] int_12;

		internal int int_13;

		internal int int_14;

		internal int int_15;

		internal int int_16;

		internal int int_17;

		internal byte byte_0;

		internal byte byte_1;

		internal int[] int_18;

		internal int int_19;

		internal int[] int_20;

		internal int int_21;

		internal InflateCodes()
		{
		}

		internal void Init(int int_22, int int_23, int[] int_24, int int_25, int[] int_26, int int_27)
		{
			int_10 = 0;
			byte_0 = (byte)int_22;
			byte_1 = (byte)int_23;
			int_18 = int_24;
			int_19 = int_25;
			int_20 = int_26;
			int_21 = int_27;
			int_12 = null;
		}

		internal int Process(InflateBlocks inflateBlocks_0, int int_22)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			ZlibCodec zlibCodec_ = inflateBlocks_0.zlibCodec_0;
			num3 = zlibCodec_.int_0;
			int num4 = zlibCodec_.int_1;
			num = inflateBlocks_0.int_10;
			num2 = inflateBlocks_0.int_9;
			int num5 = inflateBlocks_0.int_14;
			int num6 = ((num5 >= inflateBlocks_0.int_13) ? (inflateBlocks_0.int_12 - num5) : (inflateBlocks_0.int_13 - num5 - 1));
			while (true)
			{
				switch (int_10)
				{
				case 6:
					if (num6 == 0)
					{
						if (num5 == inflateBlocks_0.int_12 && inflateBlocks_0.int_13 != 0)
						{
							num5 = 0;
							num6 = ((0 >= inflateBlocks_0.int_13) ? (inflateBlocks_0.int_12 - num5) : (inflateBlocks_0.int_13 - num5 - 1));
						}
						if (num6 == 0)
						{
							inflateBlocks_0.int_14 = num5;
							int_22 = inflateBlocks_0.Flush(int_22);
							num5 = inflateBlocks_0.int_14;
							num6 = ((num5 >= inflateBlocks_0.int_13) ? (inflateBlocks_0.int_12 - num5) : (inflateBlocks_0.int_13 - num5 - 1));
							if (num5 == inflateBlocks_0.int_12 && inflateBlocks_0.int_13 != 0)
							{
								num5 = 0;
								num6 = ((0 >= inflateBlocks_0.int_13) ? (inflateBlocks_0.int_12 - num5) : (inflateBlocks_0.int_13 - num5 - 1));
							}
							if (num6 == 0)
							{
								inflateBlocks_0.int_10 = num;
								inflateBlocks_0.int_9 = num2;
								zlibCodec_.int_1 = num4;
								zlibCodec_.long_0 += num3 - zlibCodec_.int_0;
								zlibCodec_.int_0 = num3;
								inflateBlocks_0.int_14 = num5;
								return inflateBlocks_0.Flush(int_22);
							}
						}
					}
					int_22 = 0;
					inflateBlocks_0.byte_0[num5++] = (byte)int_15;
					num6--;
					int_10 = 0;
					break;
				case 5:
				{
					int i;
					for (i = num5 - int_17; i < 0; i += inflateBlocks_0.int_12)
					{
					}
					while (int_11 != 0)
					{
						if (num6 == 0)
						{
							if (num5 == inflateBlocks_0.int_12 && inflateBlocks_0.int_13 != 0)
							{
								num5 = 0;
								num6 = ((0 >= inflateBlocks_0.int_13) ? (inflateBlocks_0.int_12 - num5) : (inflateBlocks_0.int_13 - num5 - 1));
							}
							if (num6 == 0)
							{
								inflateBlocks_0.int_14 = num5;
								int_22 = inflateBlocks_0.Flush(int_22);
								num5 = inflateBlocks_0.int_14;
								num6 = ((num5 >= inflateBlocks_0.int_13) ? (inflateBlocks_0.int_12 - num5) : (inflateBlocks_0.int_13 - num5 - 1));
								if (num5 == inflateBlocks_0.int_12 && inflateBlocks_0.int_13 != 0)
								{
									num5 = 0;
									num6 = ((0 >= inflateBlocks_0.int_13) ? (inflateBlocks_0.int_12 - num5) : (inflateBlocks_0.int_13 - num5 - 1));
								}
								if (num6 == 0)
								{
									inflateBlocks_0.int_10 = num;
									inflateBlocks_0.int_9 = num2;
									zlibCodec_.int_1 = num4;
									zlibCodec_.long_0 += num3 - zlibCodec_.int_0;
									zlibCodec_.int_0 = num3;
									inflateBlocks_0.int_14 = num5;
									return inflateBlocks_0.Flush(int_22);
								}
							}
						}
						inflateBlocks_0.byte_0[num5++] = inflateBlocks_0.byte_0[i++];
						num6--;
						if (i == inflateBlocks_0.int_12)
						{
							i = 0;
						}
						int_11--;
					}
					int_10 = 0;
					break;
				}
				case 4:
				{
					int num7;
					for (num7 = int_16; num2 < num7; num2 += 8)
					{
						if (num4 != 0)
						{
							int_22 = 0;
							num4--;
							num |= (zlibCodec_.byte_0[num3++] & 0xFF) << num2;
							continue;
						}
						inflateBlocks_0.int_10 = num;
						inflateBlocks_0.int_9 = num2;
						zlibCodec_.int_1 = num4;
						zlibCodec_.long_0 += num3 - zlibCodec_.int_0;
						zlibCodec_.int_0 = num3;
						inflateBlocks_0.int_14 = num5;
						return inflateBlocks_0.Flush(int_22);
					}
					int_17 += num & InternalInflateConstants.int_0[num7];
					num >>= num7;
					num2 -= num7;
					int_10 = 5;
					goto case 5;
				}
				case 3:
				{
					int num7;
					for (num7 = int_14; num2 < num7; num2 += 8)
					{
						if (num4 != 0)
						{
							int_22 = 0;
							num4--;
							num |= (zlibCodec_.byte_0[num3++] & 0xFF) << num2;
							continue;
						}
						inflateBlocks_0.int_10 = num;
						inflateBlocks_0.int_9 = num2;
						zlibCodec_.int_1 = num4;
						zlibCodec_.long_0 += num3 - zlibCodec_.int_0;
						zlibCodec_.int_0 = num3;
						inflateBlocks_0.int_14 = num5;
						return inflateBlocks_0.Flush(int_22);
					}
					int num8 = (int_13 + (num & InternalInflateConstants.int_0[num7])) * 3;
					num >>= int_12[num8 + 1];
					num2 -= int_12[num8 + 1];
					int num9 = int_12[num8];
					if (((uint)num9 & 0x10u) != 0)
					{
						int_16 = num9 & 0xF;
						int_17 = int_12[num8 + 2];
						int_10 = 4;
						break;
					}
					if ((num9 & 0x40) == 0)
					{
						int_14 = num9;
						int_13 = num8 / 3 + int_12[num8 + 2];
						break;
					}
					int_10 = 9;
					zlibCodec_.string_0 = "invalid distance code";
					int_22 = -3;
					inflateBlocks_0.int_10 = num;
					inflateBlocks_0.int_9 = num2;
					zlibCodec_.int_1 = num4;
					zlibCodec_.long_0 += num3 - zlibCodec_.int_0;
					zlibCodec_.int_0 = num3;
					inflateBlocks_0.int_14 = num5;
					return inflateBlocks_0.Flush(-3);
				}
				case 2:
				{
					int num7;
					for (num7 = int_16; num2 < num7; num2 += 8)
					{
						if (num4 != 0)
						{
							int_22 = 0;
							num4--;
							num |= (zlibCodec_.byte_0[num3++] & 0xFF) << num2;
							continue;
						}
						inflateBlocks_0.int_10 = num;
						inflateBlocks_0.int_9 = num2;
						zlibCodec_.int_1 = num4;
						zlibCodec_.long_0 += num3 - zlibCodec_.int_0;
						zlibCodec_.int_0 = num3;
						inflateBlocks_0.int_14 = num5;
						return inflateBlocks_0.Flush(int_22);
					}
					int_11 += num & InternalInflateConstants.int_0[num7];
					num >>= num7;
					num2 -= num7;
					int_14 = byte_1;
					int_12 = int_20;
					int_13 = int_21;
					int_10 = 3;
					goto case 3;
				}
				case 1:
				{
					int num7;
					for (num7 = int_14; num2 < num7; num2 += 8)
					{
						if (num4 != 0)
						{
							int_22 = 0;
							num4--;
							num |= (zlibCodec_.byte_0[num3++] & 0xFF) << num2;
							continue;
						}
						inflateBlocks_0.int_10 = num;
						inflateBlocks_0.int_9 = num2;
						zlibCodec_.int_1 = num4;
						zlibCodec_.long_0 += num3 - zlibCodec_.int_0;
						zlibCodec_.int_0 = num3;
						inflateBlocks_0.int_14 = num5;
						return inflateBlocks_0.Flush(int_22);
					}
					int num8 = (int_13 + (num & InternalInflateConstants.int_0[num7])) * 3;
					num >>= int_12[num8 + 1];
					num2 -= int_12[num8 + 1];
					int num9 = int_12[num8];
					if (num9 == 0)
					{
						int_15 = int_12[num8 + 2];
						int_10 = 6;
						break;
					}
					if (((uint)num9 & 0x10u) != 0)
					{
						int_16 = num9 & 0xF;
						int_11 = int_12[num8 + 2];
						int_10 = 2;
						break;
					}
					if ((num9 & 0x40) == 0)
					{
						int_14 = num9;
						int_13 = num8 / 3 + int_12[num8 + 2];
						break;
					}
					if (((uint)num9 & 0x20u) != 0)
					{
						int_10 = 7;
						break;
					}
					int_10 = 9;
					zlibCodec_.string_0 = "invalid literal/length code";
					int_22 = -3;
					inflateBlocks_0.int_10 = num;
					inflateBlocks_0.int_9 = num2;
					zlibCodec_.int_1 = num4;
					zlibCodec_.long_0 += num3 - zlibCodec_.int_0;
					zlibCodec_.int_0 = num3;
					inflateBlocks_0.int_14 = num5;
					return inflateBlocks_0.Flush(-3);
				}
				case 0:
					if (num6 >= 258 && num4 >= 10)
					{
						inflateBlocks_0.int_10 = num;
						inflateBlocks_0.int_9 = num2;
						zlibCodec_.int_1 = num4;
						zlibCodec_.long_0 += num3 - zlibCodec_.int_0;
						zlibCodec_.int_0 = num3;
						inflateBlocks_0.int_14 = num5;
						int_22 = InflateFast(byte_0, byte_1, int_18, int_19, int_20, int_21, inflateBlocks_0, zlibCodec_);
						num3 = zlibCodec_.int_0;
						num4 = zlibCodec_.int_1;
						num = inflateBlocks_0.int_10;
						num2 = inflateBlocks_0.int_9;
						num5 = inflateBlocks_0.int_14;
						num6 = ((num5 >= inflateBlocks_0.int_13) ? (inflateBlocks_0.int_12 - num5) : (inflateBlocks_0.int_13 - num5 - 1));
						if (int_22 != 0)
						{
							int_10 = ((int_22 != 1) ? 9 : 7);
							break;
						}
					}
					int_14 = byte_0;
					int_12 = int_18;
					int_13 = int_19;
					int_10 = 1;
					goto case 1;
				default:
					int_22 = -2;
					inflateBlocks_0.int_10 = num;
					inflateBlocks_0.int_9 = num2;
					zlibCodec_.int_1 = num4;
					zlibCodec_.long_0 += num3 - zlibCodec_.int_0;
					zlibCodec_.int_0 = num3;
					inflateBlocks_0.int_14 = num5;
					return inflateBlocks_0.Flush(-2);
				case 7:
					if (num2 > 7)
					{
						num2 -= 8;
						num4++;
						num3--;
					}
					inflateBlocks_0.int_14 = num5;
					int_22 = inflateBlocks_0.Flush(int_22);
					num5 = inflateBlocks_0.int_14;
					num6 = ((num5 >= inflateBlocks_0.int_13) ? (inflateBlocks_0.int_12 - num5) : (inflateBlocks_0.int_13 - num5 - 1));
					if (inflateBlocks_0.int_13 != inflateBlocks_0.int_14)
					{
						inflateBlocks_0.int_10 = num;
						inflateBlocks_0.int_9 = num2;
						zlibCodec_.int_1 = num4;
						zlibCodec_.long_0 += num3 - zlibCodec_.int_0;
						zlibCodec_.int_0 = num3;
						inflateBlocks_0.int_14 = num5;
						return inflateBlocks_0.Flush(int_22);
					}
					int_10 = 8;
					goto case 8;
				case 8:
					int_22 = 1;
					inflateBlocks_0.int_10 = num;
					inflateBlocks_0.int_9 = num2;
					zlibCodec_.int_1 = num4;
					zlibCodec_.long_0 += num3 - zlibCodec_.int_0;
					zlibCodec_.int_0 = num3;
					inflateBlocks_0.int_14 = num5;
					return inflateBlocks_0.Flush(1);
				case 9:
					int_22 = -3;
					inflateBlocks_0.int_10 = num;
					inflateBlocks_0.int_9 = num2;
					zlibCodec_.int_1 = num4;
					zlibCodec_.long_0 += num3 - zlibCodec_.int_0;
					zlibCodec_.int_0 = num3;
					inflateBlocks_0.int_14 = num5;
					return inflateBlocks_0.Flush(-3);
				}
			}
		}

		internal int InflateFast(int int_22, int int_23, int[] int_24, int int_25, int[] int_26, int int_27, InflateBlocks inflateBlocks_0, ZlibCodec zlibCodec_0)
		{
			int num = zlibCodec_0.int_0;
			int num2 = zlibCodec_0.int_1;
			int num3 = inflateBlocks_0.int_10;
			int num4 = inflateBlocks_0.int_9;
			int num5 = inflateBlocks_0.int_14;
			int num6 = ((num5 >= inflateBlocks_0.int_13) ? (inflateBlocks_0.int_12 - num5) : (inflateBlocks_0.int_13 - num5 - 1));
			int num7 = InternalInflateConstants.int_0[int_22];
			int num8 = InternalInflateConstants.int_0[int_23];
			int num13;
			while (true)
			{
				if (num4 < 20)
				{
					num2--;
					num3 |= (zlibCodec_0.byte_0[num++] & 0xFF) << num4;
					num4 += 8;
					continue;
				}
				int num9 = num3 & num7;
				int[] array = int_24;
				int num10 = int_25;
				int num11 = (num10 + num9) * 3;
				int num12;
				if ((num12 = array[num11]) == 0)
				{
					num3 >>= array[num11 + 1];
					num4 -= array[num11 + 1];
					inflateBlocks_0.byte_0[num5++] = (byte)array[num11 + 2];
					num6--;
				}
				else
				{
					while (true)
					{
						num3 >>= array[num11 + 1];
						num4 -= array[num11 + 1];
						if ((num12 & 0x10) == 0)
						{
							if ((num12 & 0x40) == 0)
							{
								num9 += array[num11 + 2];
								num9 += num3 & InternalInflateConstants.int_0[num12];
								num11 = (num10 + num9) * 3;
								if ((num12 = array[num11]) == 0)
								{
									num3 >>= array[num11 + 1];
									num4 -= array[num11 + 1];
									inflateBlocks_0.byte_0[num5++] = (byte)array[num11 + 2];
									num6--;
									break;
								}
								continue;
							}
							if (((uint)num12 & 0x20u) != 0)
							{
								num13 = zlibCodec_0.int_1 - num2;
								num13 = ((num4 >> 3 >= num13) ? num13 : (num4 >> 3));
								num2 += num13;
								num -= num13;
								num4 -= num13 << 3;
								inflateBlocks_0.int_10 = num3;
								inflateBlocks_0.int_9 = num4;
								zlibCodec_0.int_1 = num2;
								zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
								zlibCodec_0.int_0 = num;
								inflateBlocks_0.int_14 = num5;
								return 1;
							}
							zlibCodec_0.string_0 = "invalid literal/length code";
							num13 = zlibCodec_0.int_1 - num2;
							num13 = ((num4 >> 3 >= num13) ? num13 : (num4 >> 3));
							num2 += num13;
							num -= num13;
							num4 -= num13 << 3;
							inflateBlocks_0.int_10 = num3;
							inflateBlocks_0.int_9 = num4;
							zlibCodec_0.int_1 = num2;
							zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
							zlibCodec_0.int_0 = num;
							inflateBlocks_0.int_14 = num5;
							return -3;
						}
						num12 &= 0xF;
						num13 = array[num11 + 2] + (num3 & InternalInflateConstants.int_0[num12]);
						num3 >>= num12;
						for (num4 -= num12; num4 < 15; num4 += 8)
						{
							num2--;
							num3 |= (zlibCodec_0.byte_0[num++] & 0xFF) << num4;
						}
						num9 = num3 & num8;
						array = int_26;
						num10 = int_27;
						num11 = (num10 + num9) * 3;
						num12 = array[num11];
						while (true)
						{
							num3 >>= array[num11 + 1];
							num4 -= array[num11 + 1];
							if (((uint)num12 & 0x10u) != 0)
							{
								break;
							}
							if ((num12 & 0x40) == 0)
							{
								num9 += array[num11 + 2];
								num9 += num3 & InternalInflateConstants.int_0[num12];
								num11 = (num10 + num9) * 3;
								num12 = array[num11];
								continue;
							}
							zlibCodec_0.string_0 = "invalid distance code";
							num13 = zlibCodec_0.int_1 - num2;
							num13 = ((num4 >> 3 >= num13) ? num13 : (num4 >> 3));
							num2 += num13;
							num -= num13;
							num4 -= num13 << 3;
							inflateBlocks_0.int_10 = num3;
							inflateBlocks_0.int_9 = num4;
							zlibCodec_0.int_1 = num2;
							zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
							zlibCodec_0.int_0 = num;
							inflateBlocks_0.int_14 = num5;
							return -3;
						}
						for (num12 &= 0xF; num4 < num12; num4 += 8)
						{
							num2--;
							num3 |= (zlibCodec_0.byte_0[num++] & 0xFF) << num4;
						}
						int num14 = array[num11 + 2] + (num3 & InternalInflateConstants.int_0[num12]);
						num3 >>= num12;
						num4 -= num12;
						num6 -= num13;
						int num15;
						if (num5 >= num14)
						{
							num15 = num5 - num14;
							if (num5 - num15 > 0 && 2 > num5 - num15)
							{
								inflateBlocks_0.byte_0[num5++] = inflateBlocks_0.byte_0[num15++];
								inflateBlocks_0.byte_0[num5++] = inflateBlocks_0.byte_0[num15++];
								num13 -= 2;
							}
							else
							{
								Array.Copy(inflateBlocks_0.byte_0, num15, inflateBlocks_0.byte_0, num5, 2);
								num5 += 2;
								num15 += 2;
								num13 -= 2;
							}
						}
						else
						{
							num15 = num5 - num14;
							do
							{
								num15 += inflateBlocks_0.int_12;
							}
							while (num15 < 0);
							num12 = inflateBlocks_0.int_12 - num15;
							if (num13 > num12)
							{
								num13 -= num12;
								if (num5 - num15 > 0 && num12 > num5 - num15)
								{
									do
									{
										inflateBlocks_0.byte_0[num5++] = inflateBlocks_0.byte_0[num15++];
									}
									while (--num12 != 0);
								}
								else
								{
									Array.Copy(inflateBlocks_0.byte_0, num15, inflateBlocks_0.byte_0, num5, num12);
									num5 += num12;
									num15 += num12;
									num12 = 0;
								}
								num15 = 0;
							}
						}
						if (num5 - num15 > 0 && num13 > num5 - num15)
						{
							do
							{
								inflateBlocks_0.byte_0[num5++] = inflateBlocks_0.byte_0[num15++];
							}
							while (--num13 != 0);
							break;
						}
						Array.Copy(inflateBlocks_0.byte_0, num15, inflateBlocks_0.byte_0, num5, num13);
						num5 += num13;
						num15 += num13;
						num13 = 0;
						break;
					}
				}
				if (num6 < 258 || num2 < 10)
				{
					break;
				}
			}
			num13 = zlibCodec_0.int_1 - num2;
			num13 = ((num4 >> 3 >= num13) ? num13 : (num4 >> 3));
			num2 += num13;
			num -= num13;
			num4 -= num13 << 3;
			inflateBlocks_0.int_10 = num3;
			inflateBlocks_0.int_9 = num4;
			zlibCodec_0.int_1 = num2;
			zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
			zlibCodec_0.int_0 = num;
			inflateBlocks_0.int_14 = num5;
			return 0;
		}
	}
}
