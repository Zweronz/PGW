namespace BestHTTP.Decompression.Zlib
{
	internal sealed class InflateManager
	{
		private enum InflateManagerMode
		{
			METHOD = 0,
			FLAG = 1,
			DICT4 = 2,
			DICT3 = 3,
			DICT2 = 4,
			DICT1 = 5,
			DICT0 = 6,
			BLOCKS = 7,
			CHECK4 = 8,
			CHECK3 = 9,
			CHECK2 = 10,
			CHECK1 = 11,
			DONE = 12,
			BAD = 13
		}

		private const int int_0 = 32;

		private const int int_1 = 8;

		private InflateManagerMode inflateManagerMode_0;

		internal ZlibCodec zlibCodec_0;

		internal int int_2;

		internal uint uint_0;

		internal uint uint_1;

		internal int int_3;

		private bool bool_0 = true;

		internal int int_4;

		internal InflateBlocks inflateBlocks_0;

		private static readonly byte[] byte_0 = new byte[4] { 0, 0, 255, 255 };

		internal bool Boolean_0
		{
			get
			{
				return bool_0;
			}
			set
			{
				bool_0 = value;
			}
		}

		public InflateManager()
		{
		}

		public InflateManager(bool bool_1)
		{
			bool_0 = bool_1;
		}

		internal int Reset()
		{
			ZlibCodec zlibCodec = zlibCodec_0;
			zlibCodec_0.long_1 = 0L;
			zlibCodec.long_0 = 0L;
			zlibCodec_0.string_0 = null;
			inflateManagerMode_0 = ((!Boolean_0) ? InflateManagerMode.BLOCKS : InflateManagerMode.METHOD);
			inflateBlocks_0.Reset();
			return 0;
		}

		internal int End()
		{
			if (inflateBlocks_0 != null)
			{
				inflateBlocks_0.Free();
			}
			inflateBlocks_0 = null;
			return 0;
		}

		internal int Initialize(ZlibCodec zlibCodec_1, int int_5)
		{
			zlibCodec_0 = zlibCodec_1;
			zlibCodec_0.string_0 = null;
			inflateBlocks_0 = null;
			if (int_5 >= 8 && int_5 <= 15)
			{
				int_4 = int_5;
				inflateBlocks_0 = new InflateBlocks(zlibCodec_1, (!Boolean_0) ? null : this, 1 << int_5);
				Reset();
				return 0;
			}
			End();
			throw new ZlibException("Bad window size.");
		}

		internal int Inflate(FlushType flushType_0)
		{
			if (zlibCodec_0.byte_0 == null)
			{
				throw new ZlibException("InputBuffer is null. ");
			}
			int num = 0;
			int num2 = -5;
			while (true)
			{
				switch (inflateManagerMode_0)
				{
				case InflateManagerMode.CHECK1:
					if (zlibCodec_0.int_1 != 0)
					{
						num2 = num;
						zlibCodec_0.int_1--;
						zlibCodec_0.long_0++;
						uint_1 += (uint)(zlibCodec_0.byte_0[zlibCodec_0.int_0++] & 0xFF);
						if (uint_0 != uint_1)
						{
							inflateManagerMode_0 = InflateManagerMode.BAD;
							zlibCodec_0.string_0 = "incorrect data check";
							int_3 = 5;
							break;
						}
						inflateManagerMode_0 = InflateManagerMode.DONE;
						return 1;
					}
					return num2;
				case InflateManagerMode.CHECK2:
					if (zlibCodec_0.int_1 != 0)
					{
						num2 = num;
						zlibCodec_0.int_1--;
						zlibCodec_0.long_0++;
						uint_1 += (uint)((zlibCodec_0.byte_0[zlibCodec_0.int_0++] << 8) & 0xFF00);
						inflateManagerMode_0 = InflateManagerMode.CHECK1;
						break;
					}
					return num2;
				case InflateManagerMode.CHECK3:
					if (zlibCodec_0.int_1 != 0)
					{
						num2 = num;
						zlibCodec_0.int_1--;
						zlibCodec_0.long_0++;
						uint_1 += (uint)((zlibCodec_0.byte_0[zlibCodec_0.int_0++] << 16) & 0xFF0000);
						inflateManagerMode_0 = InflateManagerMode.CHECK2;
						break;
					}
					return num2;
				case InflateManagerMode.CHECK4:
					if (zlibCodec_0.int_1 != 0)
					{
						num2 = num;
						zlibCodec_0.int_1--;
						zlibCodec_0.long_0++;
						uint_1 = (uint)((ulong)(zlibCodec_0.byte_0[zlibCodec_0.int_0++] << 24) & 0xFF000000uL);
						inflateManagerMode_0 = InflateManagerMode.CHECK3;
						break;
					}
					return num2;
				case InflateManagerMode.BLOCKS:
					num2 = inflateBlocks_0.Process(num2);
					switch (num2)
					{
					case -3:
						inflateManagerMode_0 = InflateManagerMode.BAD;
						int_3 = 0;
						goto end_IL_05fe;
					case 0:
						num2 = num;
						break;
					}
					if (num2 == 1)
					{
						num2 = num;
						uint_0 = inflateBlocks_0.Reset();
						if (Boolean_0)
						{
							inflateManagerMode_0 = InflateManagerMode.CHECK4;
							break;
						}
						inflateManagerMode_0 = InflateManagerMode.DONE;
						return 1;
					}
					return num2;
				case InflateManagerMode.DICT2:
					if (zlibCodec_0.int_1 != 0)
					{
						num2 = num;
						zlibCodec_0.int_1--;
						zlibCodec_0.long_0++;
						uint_1 += (uint)((zlibCodec_0.byte_0[zlibCodec_0.int_0++] << 8) & 0xFF00);
						inflateManagerMode_0 = InflateManagerMode.DICT1;
						break;
					}
					return num2;
				case InflateManagerMode.DICT3:
					if (zlibCodec_0.int_1 != 0)
					{
						num2 = num;
						zlibCodec_0.int_1--;
						zlibCodec_0.long_0++;
						uint_1 += (uint)((zlibCodec_0.byte_0[zlibCodec_0.int_0++] << 16) & 0xFF0000);
						inflateManagerMode_0 = InflateManagerMode.DICT2;
						break;
					}
					return num2;
				case InflateManagerMode.DICT4:
					if (zlibCodec_0.int_1 != 0)
					{
						num2 = num;
						zlibCodec_0.int_1--;
						zlibCodec_0.long_0++;
						uint_1 = (uint)((ulong)(zlibCodec_0.byte_0[zlibCodec_0.int_0++] << 24) & 0xFF000000uL);
						inflateManagerMode_0 = InflateManagerMode.DICT3;
						break;
					}
					return num2;
				case InflateManagerMode.FLAG:
					if (zlibCodec_0.int_1 != 0)
					{
						num2 = num;
						zlibCodec_0.int_1--;
						zlibCodec_0.long_0++;
						int num3 = zlibCodec_0.byte_0[zlibCodec_0.int_0++] & 0xFF;
						if (((int_2 << 8) + num3) % 31 != 0)
						{
							inflateManagerMode_0 = InflateManagerMode.BAD;
							zlibCodec_0.string_0 = "incorrect header check";
							int_3 = 5;
						}
						else
						{
							inflateManagerMode_0 = ((((uint)num3 & 0x20u) != 0) ? InflateManagerMode.DICT4 : InflateManagerMode.BLOCKS);
						}
						break;
					}
					return num2;
				case InflateManagerMode.METHOD:
					if (zlibCodec_0.int_1 != 0)
					{
						num2 = num;
						zlibCodec_0.int_1--;
						zlibCodec_0.long_0++;
						if (((int_2 = zlibCodec_0.byte_0[zlibCodec_0.int_0++]) & 0xF) != 8)
						{
							inflateManagerMode_0 = InflateManagerMode.BAD;
							zlibCodec_0.string_0 = string.Format("unknown compression method (0x{0:X2})", int_2);
							int_3 = 5;
						}
						else if ((int_2 >> 4) + 8 > int_4)
						{
							inflateManagerMode_0 = InflateManagerMode.BAD;
							zlibCodec_0.string_0 = string.Format("invalid window size ({0})", (int_2 >> 4) + 8);
							int_3 = 5;
						}
						else
						{
							inflateManagerMode_0 = InflateManagerMode.FLAG;
						}
						break;
					}
					return num2;
				default:
					throw new ZlibException("Stream error.");
				case InflateManagerMode.DICT1:
					if (zlibCodec_0.int_1 == 0)
					{
						return num2;
					}
					num2 = num;
					zlibCodec_0.int_1--;
					zlibCodec_0.long_0++;
					uint_1 += (uint)(zlibCodec_0.byte_0[zlibCodec_0.int_0++] & 0xFF);
					zlibCodec_0.uint_0 = uint_1;
					inflateManagerMode_0 = InflateManagerMode.DICT0;
					return 2;
				case InflateManagerMode.DICT0:
					inflateManagerMode_0 = InflateManagerMode.BAD;
					zlibCodec_0.string_0 = "need dictionary";
					int_3 = 0;
					return -2;
				case InflateManagerMode.BAD:
					throw new ZlibException(string.Format("Bad state ({0})", zlibCodec_0.string_0));
				case InflateManagerMode.DONE:
					{
						return 1;
					}
					end_IL_05fe:
					break;
				}
			}
		}

		internal int SetDictionary(byte[] byte_1)
		{
			int int_ = 0;
			int num = byte_1.Length;
			if (inflateManagerMode_0 != InflateManagerMode.DICT0)
			{
				throw new ZlibException("Stream error.");
			}
			if (Adler.Adler32(1u, byte_1, 0, byte_1.Length) != zlibCodec_0.uint_0)
			{
				return -3;
			}
			zlibCodec_0.uint_0 = Adler.Adler32(0u, null, 0, 0);
			if (num >= 1 << int_4)
			{
				num = (1 << int_4) - 1;
				int_ = byte_1.Length - num;
			}
			inflateBlocks_0.SetDictionary(byte_1, int_, num);
			inflateManagerMode_0 = InflateManagerMode.BLOCKS;
			return 0;
		}

		internal int Sync()
		{
			if (inflateManagerMode_0 != InflateManagerMode.BAD)
			{
				inflateManagerMode_0 = InflateManagerMode.BAD;
				int_3 = 0;
			}
			int num;
			if ((num = zlibCodec_0.int_1) == 0)
			{
				return -5;
			}
			int num2 = zlibCodec_0.int_0;
			int num3 = int_3;
			while (num != 0 && num3 < 4)
			{
				num3 = ((zlibCodec_0.byte_0[num2] != byte_0[num3]) ? ((zlibCodec_0.byte_0[num2] == 0) ? (4 - num3) : 0) : (num3 + 1));
				num2++;
				num--;
			}
			zlibCodec_0.long_0 += num2 - zlibCodec_0.int_0;
			zlibCodec_0.int_0 = num2;
			zlibCodec_0.int_1 = num;
			int_3 = num3;
			if (num3 != 4)
			{
				return -3;
			}
			long long_ = zlibCodec_0.long_0;
			long long_2 = zlibCodec_0.long_1;
			Reset();
			zlibCodec_0.long_0 = long_;
			zlibCodec_0.long_1 = long_2;
			inflateManagerMode_0 = InflateManagerMode.BLOCKS;
			return 0;
		}

		internal int SyncPoint(ZlibCodec zlibCodec_1)
		{
			return inflateBlocks_0.SyncPoint();
		}
	}
}
