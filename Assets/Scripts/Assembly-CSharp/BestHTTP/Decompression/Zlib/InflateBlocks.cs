using System;

namespace BestHTTP.Decompression.Zlib
{
	internal sealed class InflateBlocks
	{
		private enum InflateBlockMode
		{
			TYPE = 0,
			LENS = 1,
			STORED = 2,
			TABLE = 3,
			BTREE = 4,
			DTREE = 5,
			CODES = 6,
			DRY = 7,
			DONE = 8,
			BAD = 9
		}

		private const int int_0 = 1440;

		internal static readonly int[] int_1 = new int[19]
		{
			16, 17, 18, 0, 8, 7, 9, 6, 10, 5,
			11, 4, 12, 3, 13, 2, 14, 1, 15
		};

		private InflateBlockMode inflateBlockMode_0;

		internal int int_2;

		internal int int_3;

		internal int int_4;

		internal int[] int_5;

		internal int[] int_6 = new int[1];

		internal int[] int_7 = new int[1];

		internal InflateCodes inflateCodes_0 = new InflateCodes();

		internal int int_8;

		internal ZlibCodec zlibCodec_0;

		internal int int_9;

		internal int int_10;

		internal int[] int_11;

		internal byte[] byte_0;

		internal int int_12;

		internal int int_13;

		internal int int_14;

		internal object object_0;

		internal uint uint_0;

		internal InfTree infTree_0 = new InfTree();

		internal InflateBlocks(ZlibCodec zlibCodec_1, object object_1, int int_15)
		{
			zlibCodec_0 = zlibCodec_1;
			int_11 = new int[4320];
			byte_0 = new byte[int_15];
			int_12 = int_15;
			object_0 = object_1;
			inflateBlockMode_0 = InflateBlockMode.TYPE;
			Reset();
		}

		internal uint Reset()
		{
			uint result = uint_0;
			inflateBlockMode_0 = InflateBlockMode.TYPE;
			int_9 = 0;
			int_10 = 0;
			int_14 = 0;
			int_13 = 0;
			if (object_0 != null)
			{
				zlibCodec_0.uint_0 = (uint_0 = Adler.Adler32(0u, null, 0, 0));
			}
			return result;
		}

		internal int Process(int int_15)
		{
			int num = zlibCodec_0.int_0;
			int num2 = zlibCodec_0.int_1;
			int num3 = int_10;
			int i = int_9;
			int num4 = int_14;
			int num5 = ((num4 >= int_13) ? (int_12 - num4) : (int_13 - num4 - 1));
			while (true)
			{
				switch (inflateBlockMode_0)
				{
				case InflateBlockMode.CODES:
					int_10 = num3;
					int_9 = i;
					zlibCodec_0.int_1 = num2;
					zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
					zlibCodec_0.int_0 = num;
					int_14 = num4;
					int_15 = inflateCodes_0.Process(this, int_15);
					if (int_15 == 1)
					{
						int_15 = 0;
						num = zlibCodec_0.int_0;
						num2 = zlibCodec_0.int_1;
						num3 = int_10;
						i = int_9;
						num4 = int_14;
						num5 = ((num4 >= int_13) ? (int_12 - num4) : (int_13 - num4 - 1));
						if (int_8 == 0)
						{
							inflateBlockMode_0 = InflateBlockMode.TYPE;
							break;
						}
						inflateBlockMode_0 = InflateBlockMode.DRY;
						goto case InflateBlockMode.DRY;
					}
					return Flush(int_15);
				case InflateBlockMode.DTREE:
				{
					int num6;
					while (true)
					{
						num6 = int_3;
						if (int_4 >= 258 + (num6 & 0x1F) + ((num6 >> 5) & 0x1F))
						{
							break;
						}
						for (num6 = int_6[0]; i < num6; i += 8)
						{
							if (num2 != 0)
							{
								int_15 = 0;
								num2--;
								num3 |= (zlibCodec_0.byte_0[num++] & 0xFF) << i;
								continue;
							}
							int_10 = num3;
							int_9 = i;
							zlibCodec_0.int_1 = num2;
							zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
							zlibCodec_0.int_0 = num;
							int_14 = num4;
							return Flush(int_15);
						}
						num6 = int_11[(int_7[0] + (num3 & InternalInflateConstants.int_0[num6])) * 3 + 1];
						int num7 = int_11[(int_7[0] + (num3 & InternalInflateConstants.int_0[num6])) * 3 + 2];
						if (num7 < 16)
						{
							num3 >>= num6;
							i -= num6;
							int_5[int_4++] = num7;
							continue;
						}
						int num8 = ((num7 != 18) ? (num7 - 14) : 7);
						int num9 = ((num7 != 18) ? 3 : 11);
						for (; i < num6 + num8; i += 8)
						{
							if (num2 != 0)
							{
								int_15 = 0;
								num2--;
								num3 |= (zlibCodec_0.byte_0[num++] & 0xFF) << i;
								continue;
							}
							int_10 = num3;
							int_9 = i;
							zlibCodec_0.int_1 = num2;
							zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
							zlibCodec_0.int_0 = num;
							int_14 = num4;
							return Flush(int_15);
						}
						num3 >>= num6;
						i -= num6;
						num9 += num3 & InternalInflateConstants.int_0[num8];
						num3 >>= num8;
						i -= num8;
						num8 = int_4;
						num6 = int_3;
						if (num8 + num9 <= 258 + (num6 & 0x1F) + ((num6 >> 5) & 0x1F) && (num7 != 16 || num8 >= 1))
						{
							num7 = ((num7 == 16) ? int_5[num8 - 1] : 0);
							do
							{
								int_5[num8++] = num7;
							}
							while (--num9 != 0);
							int_4 = num8;
							continue;
						}
						int_5 = null;
						inflateBlockMode_0 = InflateBlockMode.BAD;
						zlibCodec_0.string_0 = "invalid bit length repeat";
						int_15 = -3;
						int_10 = num3;
						int_9 = i;
						zlibCodec_0.int_1 = num2;
						zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
						zlibCodec_0.int_0 = num;
						int_14 = num4;
						return Flush(-3);
					}
					int_7[0] = -1;
					int[] array5 = new int[1] { 9 };
					int[] array6 = new int[1] { 6 };
					int[] array7 = new int[1];
					int[] array8 = new int[1];
					num6 = int_3;
					num6 = infTree_0.inflate_trees_dynamic(257 + (num6 & 0x1F), 1 + ((num6 >> 5) & 0x1F), int_5, array5, array6, array7, array8, int_11, zlibCodec_0);
					if (num6 == 0)
					{
						inflateCodes_0.Init(array5[0], array6[0], int_11, array7[0], int_11, array8[0]);
						inflateBlockMode_0 = InflateBlockMode.CODES;
						goto case InflateBlockMode.CODES;
					}
					if (num6 == -3)
					{
						int_5 = null;
						inflateBlockMode_0 = InflateBlockMode.BAD;
					}
					int_15 = num6;
					int_10 = num3;
					int_9 = i;
					zlibCodec_0.int_1 = num2;
					zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
					zlibCodec_0.int_0 = num;
					int_14 = num4;
					return Flush(int_15);
				}
				case InflateBlockMode.BTREE:
				{
					while (int_4 < 4 + (int_3 >> 10))
					{
						for (; i < 3; i += 8)
						{
							if (num2 != 0)
							{
								int_15 = 0;
								num2--;
								num3 |= (zlibCodec_0.byte_0[num++] & 0xFF) << i;
								continue;
							}
							int_10 = num3;
							int_9 = i;
							zlibCodec_0.int_1 = num2;
							zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
							zlibCodec_0.int_0 = num;
							int_14 = num4;
							return Flush(int_15);
						}
						int_5[int_1[int_4++]] = num3 & 7;
						num3 >>= 3;
						i -= 3;
					}
					while (int_4 < 19)
					{
						int_5[int_1[int_4++]] = 0;
					}
					int_6[0] = 7;
					int num6 = infTree_0.inflate_trees_bits(int_5, int_6, int_7, int_11, zlibCodec_0);
					if (num6 == 0)
					{
						int_4 = 0;
						inflateBlockMode_0 = InflateBlockMode.DTREE;
						goto case InflateBlockMode.DTREE;
					}
					int_15 = num6;
					if (int_15 == -3)
					{
						int_5 = null;
						inflateBlockMode_0 = InflateBlockMode.BAD;
					}
					int_10 = num3;
					int_9 = i;
					zlibCodec_0.int_1 = num2;
					zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
					zlibCodec_0.int_0 = num;
					int_14 = num4;
					return Flush(int_15);
				}
				case InflateBlockMode.TABLE:
				{
					for (; i < 14; i += 8)
					{
						if (num2 != 0)
						{
							int_15 = 0;
							num2--;
							num3 |= (zlibCodec_0.byte_0[num++] & 0xFF) << i;
							continue;
						}
						int_10 = num3;
						int_9 = i;
						zlibCodec_0.int_1 = num2;
						zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
						zlibCodec_0.int_0 = num;
						int_14 = num4;
						return Flush(int_15);
					}
					int num6 = (int_3 = num3 & 0x3FFF);
					if ((num6 & 0x1F) <= 29 && ((num6 >> 5) & 0x1F) <= 29)
					{
						num6 = 258 + (num6 & 0x1F) + ((num6 >> 5) & 0x1F);
						if (int_5 != null && int_5.Length >= num6)
						{
							Array.Clear(int_5, 0, num6);
						}
						else
						{
							int_5 = new int[num6];
						}
						num3 >>= 14;
						i -= 14;
						int_4 = 0;
						inflateBlockMode_0 = InflateBlockMode.BTREE;
						goto case InflateBlockMode.BTREE;
					}
					inflateBlockMode_0 = InflateBlockMode.BAD;
					zlibCodec_0.string_0 = "too many length or distance symbols";
					int_15 = -3;
					int_10 = num3;
					int_9 = i;
					zlibCodec_0.int_1 = num2;
					zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
					zlibCodec_0.int_0 = num;
					int_14 = num4;
					return Flush(-3);
				}
				case InflateBlockMode.STORED:
					if (num2 != 0)
					{
						if (num5 == 0)
						{
							if (num4 == int_12 && int_13 != 0)
							{
								num4 = 0;
								num5 = ((0 >= int_13) ? (int_12 - num4) : (int_13 - num4 - 1));
							}
							if (num5 == 0)
							{
								int_14 = num4;
								int_15 = Flush(int_15);
								num4 = int_14;
								num5 = ((num4 >= int_13) ? (int_12 - num4) : (int_13 - num4 - 1));
								if (num4 == int_12 && int_13 != 0)
								{
									num4 = 0;
									num5 = ((0 >= int_13) ? (int_12 - num4) : (int_13 - num4 - 1));
								}
								if (num5 == 0)
								{
									int_10 = num3;
									int_9 = i;
									zlibCodec_0.int_1 = num2;
									zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
									zlibCodec_0.int_0 = num;
									int_14 = num4;
									return Flush(int_15);
								}
							}
						}
						int_15 = 0;
						int num6 = int_2;
						if (num6 > num2)
						{
							num6 = num2;
						}
						if (num6 > num5)
						{
							num6 = num5;
						}
						Array.Copy(zlibCodec_0.byte_0, num, byte_0, num4, num6);
						num += num6;
						num2 -= num6;
						num4 += num6;
						num5 -= num6;
						if ((int_2 -= num6) == 0)
						{
							inflateBlockMode_0 = ((int_8 != 0) ? InflateBlockMode.DRY : InflateBlockMode.TYPE);
						}
						break;
					}
					int_10 = num3;
					int_9 = i;
					zlibCodec_0.int_1 = num2;
					zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
					zlibCodec_0.int_0 = num;
					int_14 = num4;
					return Flush(int_15);
				case InflateBlockMode.LENS:
					for (; i < 32; i += 8)
					{
						if (num2 != 0)
						{
							int_15 = 0;
							num2--;
							num3 |= (zlibCodec_0.byte_0[num++] & 0xFF) << i;
							continue;
						}
						int_10 = num3;
						int_9 = i;
						zlibCodec_0.int_1 = num2;
						zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
						zlibCodec_0.int_0 = num;
						int_14 = num4;
						return Flush(int_15);
					}
					if (((~num3 >> 16) & 0xFFFF) == (num3 & 0xFFFF))
					{
						int_2 = num3 & 0xFFFF;
						i = 0;
						num3 = 0;
						inflateBlockMode_0 = ((int_2 != 0) ? InflateBlockMode.STORED : ((int_8 != 0) ? InflateBlockMode.DRY : InflateBlockMode.TYPE));
						break;
					}
					inflateBlockMode_0 = InflateBlockMode.BAD;
					zlibCodec_0.string_0 = "invalid stored block lengths";
					int_15 = -3;
					int_10 = num3;
					int_9 = i;
					zlibCodec_0.int_1 = num2;
					zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
					zlibCodec_0.int_0 = num;
					int_14 = num4;
					return Flush(-3);
				case InflateBlockMode.TYPE:
				{
					for (; i < 3; i += 8)
					{
						if (num2 != 0)
						{
							int_15 = 0;
							num2--;
							num3 |= (zlibCodec_0.byte_0[num++] & 0xFF) << i;
							continue;
						}
						int_10 = num3;
						int_9 = i;
						zlibCodec_0.int_1 = num2;
						zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
						zlibCodec_0.int_0 = num;
						int_14 = num4;
						return Flush(int_15);
					}
					int num6 = num3 & 7;
					int_8 = num6 & 1;
					switch ((uint)num6 >> 1)
					{
					case 0u:
						num3 >>= 3;
						i -= 3;
						num6 = i & 7;
						num3 >>= num6;
						i -= num6;
						inflateBlockMode_0 = InflateBlockMode.LENS;
						break;
					case 1u:
					{
						int[] array = new int[1];
						int[] array2 = new int[1];
						int[][] array3 = new int[1][];
						int[][] array4 = new int[1][];
						InfTree.inflate_trees_fixed(array, array2, array3, array4, zlibCodec_0);
						inflateCodes_0.Init(array[0], array2[0], array3[0], 0, array4[0], 0);
						num3 >>= 3;
						i -= 3;
						inflateBlockMode_0 = InflateBlockMode.CODES;
						break;
					}
					case 2u:
						num3 >>= 3;
						i -= 3;
						inflateBlockMode_0 = InflateBlockMode.TABLE;
						break;
					case 3u:
						num3 >>= 3;
						i -= 3;
						inflateBlockMode_0 = InflateBlockMode.BAD;
						zlibCodec_0.string_0 = "invalid block type";
						int_15 = -3;
						int_10 = num3;
						int_9 = i;
						zlibCodec_0.int_1 = num2;
						zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
						zlibCodec_0.int_0 = num;
						int_14 = num4;
						return Flush(-3);
					}
					break;
				}
				default:
					int_15 = -2;
					int_10 = num3;
					int_9 = i;
					zlibCodec_0.int_1 = num2;
					zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
					zlibCodec_0.int_0 = num;
					int_14 = num4;
					return Flush(-2);
				case InflateBlockMode.DRY:
					int_14 = num4;
					int_15 = Flush(int_15);
					num4 = int_14;
					num5 = ((num4 >= int_13) ? (int_12 - num4) : (int_13 - num4 - 1));
					if (int_13 != int_14)
					{
						int_10 = num3;
						int_9 = i;
						zlibCodec_0.int_1 = num2;
						zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
						zlibCodec_0.int_0 = num;
						int_14 = num4;
						return Flush(int_15);
					}
					inflateBlockMode_0 = InflateBlockMode.DONE;
					goto case InflateBlockMode.DONE;
				case InflateBlockMode.DONE:
					int_15 = 1;
					int_10 = num3;
					int_9 = i;
					zlibCodec_0.int_1 = num2;
					zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
					zlibCodec_0.int_0 = num;
					int_14 = num4;
					return Flush(1);
				case InflateBlockMode.BAD:
					int_15 = -3;
					int_10 = num3;
					int_9 = i;
					zlibCodec_0.int_1 = num2;
					zlibCodec_0.long_0 += num - zlibCodec_0.int_0;
					zlibCodec_0.int_0 = num;
					int_14 = num4;
					return Flush(-3);
				}
			}
		}

		internal void Free()
		{
			Reset();
			byte_0 = null;
			int_11 = null;
		}

		internal void SetDictionary(byte[] byte_1, int int_15, int int_16)
		{
			Array.Copy(byte_1, int_15, byte_0, 0, int_16);
			int_13 = (int_14 = int_16);
		}

		internal int SyncPoint()
		{
			return (inflateBlockMode_0 == InflateBlockMode.LENS) ? 1 : 0;
		}

		internal int Flush(int int_15)
		{
			int num = 0;
			while (true)
			{
				if (num >= 2)
				{
					return int_15;
				}
				int num2 = ((num != 0) ? (int_14 - int_13) : (((int_13 > int_14) ? int_12 : int_14) - int_13));
				if (num2 == 0)
				{
					break;
				}
				if (num2 > zlibCodec_0.int_3)
				{
					num2 = zlibCodec_0.int_3;
				}
				if (num2 != 0 && int_15 == -5)
				{
					int_15 = 0;
				}
				zlibCodec_0.int_3 -= num2;
				zlibCodec_0.long_1 += num2;
				if (object_0 != null)
				{
					zlibCodec_0.uint_0 = (uint_0 = Adler.Adler32(uint_0, byte_0, int_13, num2));
				}
				Array.Copy(byte_0, int_13, zlibCodec_0.byte_1, zlibCodec_0.int_2, num2);
				zlibCodec_0.int_2 += num2;
				int_13 += num2;
				if (int_13 == int_12 && num == 0)
				{
					int_13 = 0;
					if (int_14 == int_12)
					{
						int_14 = 0;
					}
				}
				else
				{
					num++;
				}
				num++;
			}
			if (int_15 == -5)
			{
				int_15 = 0;
			}
			return int_15;
		}
	}
}
