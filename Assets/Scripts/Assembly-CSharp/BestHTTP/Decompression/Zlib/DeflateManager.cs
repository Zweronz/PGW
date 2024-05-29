using System;

namespace BestHTTP.Decompression.Zlib
{
	internal sealed class DeflateManager
	{
		internal class Config
		{
			internal int int_0;

			internal int int_1;

			internal int int_2;

			internal int int_3;

			internal DeflateFlavor deflateFlavor_0;

			private static readonly Config[] config_0;

			private Config(int int_4, int int_5, int int_6, int int_7, DeflateFlavor deflateFlavor_1)
			{
				int_0 = int_4;
				int_1 = int_5;
				int_2 = int_6;
				int_3 = int_7;
				deflateFlavor_0 = deflateFlavor_1;
			}

			static Config()
			{
				config_0 = new Config[10]
				{
					new Config(0, 0, 0, 0, DeflateFlavor.Store),
					new Config(4, 4, 8, 4, DeflateFlavor.Fast),
					new Config(4, 5, 16, 8, DeflateFlavor.Fast),
					new Config(4, 6, 32, 32, DeflateFlavor.Fast),
					new Config(4, 4, 16, 16, DeflateFlavor.Slow),
					new Config(8, 16, 32, 32, DeflateFlavor.Slow),
					new Config(8, 16, 128, 128, DeflateFlavor.Slow),
					new Config(8, 32, 128, 256, DeflateFlavor.Slow),
					new Config(32, 128, 258, 1024, DeflateFlavor.Slow),
					new Config(32, 258, 258, 4096, DeflateFlavor.Slow)
				};
			}

			public static Config Lookup(CompressionLevel compressionLevel_0)
			{
				return config_0[(int)compressionLevel_0];
			}
		}

		internal delegate BlockState CompressFunc(FlushType flushType_0);

		private static readonly int int_0 = 9;

		private static readonly int int_1 = 8;

		private CompressFunc compressFunc_0;

		private static readonly string[] string_0 = new string[10]
		{
			"need dictionary",
			"stream end",
			string.Empty,
			"file error",
			"stream error",
			"data error",
			"insufficient memory",
			"buffer error",
			"incompatible version",
			string.Empty
		};

		private static readonly int int_2 = 32;

		private static readonly int int_3 = 42;

		private static readonly int int_4 = 113;

		private static readonly int int_5 = 666;

		private static readonly int int_6 = 8;

		private static readonly int int_7 = 0;

		private static readonly int int_8 = 1;

		private static readonly int int_9 = 2;

		private static readonly int int_10 = 0;

		private static readonly int int_11 = 1;

		private static readonly int int_12 = 2;

		private static readonly int int_13 = 16;

		private static readonly int int_14 = 3;

		private static readonly int int_15 = 258;

		private static readonly int int_16 = int_15 + int_14 + 1;

		private static readonly int int_17 = 2 * InternalConstants.int_5 + 1;

		private static readonly int int_18 = 256;

		internal ZlibCodec zlibCodec_0;

		internal int int_19;

		internal byte[] byte_0;

		internal int int_20;

		internal int int_21;

		internal sbyte sbyte_0;

		internal int int_22;

		internal int int_23;

		internal int int_24;

		internal int int_25;

		internal byte[] byte_1;

		internal int int_26;

		internal short[] short_0;

		internal short[] short_1;

		internal int int_27;

		internal int int_28;

		internal int int_29;

		internal int int_30;

		internal int int_31;

		internal int int_32;

		private Config config_0;

		internal int int_33;

		internal int int_34;

		internal int int_35;

		internal int int_36;

		internal int int_37;

		internal int int_38;

		internal int int_39;

		internal CompressionLevel compressionLevel_0;

		internal CompressionStrategy compressionStrategy_0;

		internal short[] short_2;

		internal short[] short_3;

		internal short[] short_4;

		internal ZTree ztree_0 = new ZTree();

		internal ZTree ztree_1 = new ZTree();

		internal ZTree ztree_2 = new ZTree();

		internal short[] short_5 = new short[InternalConstants.int_0 + 1];

		internal int[] int_40 = new int[2 * InternalConstants.int_5 + 1];

		internal int int_41;

		internal int int_42;

		internal sbyte[] sbyte_1 = new sbyte[2 * InternalConstants.int_5 + 1];

		internal int int_43;

		internal int int_44;

		internal int int_45;

		internal int int_46;

		internal int int_47;

		internal int int_48;

		internal int int_49;

		internal int int_50;

		internal short short_6;

		internal int int_51;

		private bool bool_0;

		private bool bool_1 = true;

		internal bool Boolean_0
		{
			get
			{
				return bool_1;
			}
			set
			{
				bool_1 = value;
			}
		}

		internal DeflateManager()
		{
			short_2 = new short[int_17 * 2];
			short_3 = new short[(2 * InternalConstants.int_2 + 1) * 2];
			short_4 = new short[(2 * InternalConstants.int_1 + 1) * 2];
		}

		private void _InitializeLazyMatch()
		{
			int_26 = 2 * int_23;
			Array.Clear(short_1, 0, int_28);
			config_0 = Config.Lookup(compressionLevel_0);
			SetDeflater();
			int_36 = 0;
			int_32 = 0;
			int_38 = 0;
			int_33 = (int_39 = int_14 - 1);
			int_35 = 0;
			int_27 = 0;
		}

		private void _InitializeTreeData()
		{
			ztree_0.short_0 = short_2;
			ztree_0.staticTree_0 = StaticTree.staticTree_0;
			ztree_1.short_0 = short_3;
			ztree_1.staticTree_0 = StaticTree.staticTree_1;
			ztree_2.short_0 = short_4;
			ztree_2.staticTree_0 = StaticTree.staticTree_2;
			short_6 = 0;
			int_51 = 0;
			int_50 = 8;
			_InitializeBlocks();
		}

		internal void _InitializeBlocks()
		{
			for (int i = 0; i < InternalConstants.int_5; i++)
			{
				short_2[i * 2] = 0;
			}
			for (int j = 0; j < InternalConstants.int_2; j++)
			{
				short_3[j * 2] = 0;
			}
			for (int k = 0; k < InternalConstants.int_1; k++)
			{
				short_4[k * 2] = 0;
			}
			short_2[int_18 * 2] = 1;
			int_48 = 0;
			int_47 = 0;
			int_49 = 0;
			int_45 = 0;
		}

		internal void pqdownheap(short[] short_7, int int_52)
		{
			int num = int_40[int_52];
			for (int num2 = int_52 << 1; num2 <= int_41; num2 <<= 1)
			{
				if (num2 < int_41 && _IsSmaller(short_7, int_40[num2 + 1], int_40[num2], sbyte_1))
				{
					num2++;
				}
				if (_IsSmaller(short_7, num, int_40[num2], sbyte_1))
				{
					break;
				}
				int_40[int_52] = int_40[num2];
				int_52 = num2;
			}
			int_40[int_52] = num;
		}

		internal static bool _IsSmaller(short[] short_7, int int_52, int int_53, sbyte[] sbyte_2)
		{
			short num = short_7[int_52 * 2];
			short num2 = short_7[int_53 * 2];
			return num < num2 || (num == num2 && sbyte_2[int_52] <= sbyte_2[int_53]);
		}

		internal void scan_tree(short[] short_7, int int_52)
		{
			int num = -1;
			int num2 = short_7[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			if (num2 == 0)
			{
				num4 = 138;
				num5 = 3;
			}
			short_7[(int_52 + 1) * 2 + 1] = short.MaxValue;
			for (int i = 0; i <= int_52; i++)
			{
				int num6 = num2;
				num2 = short_7[(i + 1) * 2 + 1];
				if (++num3 < num4 && num6 == num2)
				{
					continue;
				}
				if (num3 < num5)
				{
					short_4[num6 * 2] = (short)(short_4[num6 * 2] + num3);
				}
				else if (num6 != 0)
				{
					if (num6 != num)
					{
						short_4[num6 * 2]++;
					}
					short_4[InternalConstants.int_7 * 2]++;
				}
				else if (num3 <= 10)
				{
					short_4[InternalConstants.int_8 * 2]++;
				}
				else
				{
					short_4[InternalConstants.int_9 * 2]++;
				}
				num3 = 0;
				num = num6;
				if (num2 == 0)
				{
					num4 = 138;
					num5 = 3;
				}
				else if (num6 == num2)
				{
					num4 = 6;
					num5 = 3;
				}
				else
				{
					num4 = 7;
					num5 = 4;
				}
			}
		}

		internal int build_bl_tree()
		{
			scan_tree(short_2, ztree_0.int_7);
			scan_tree(short_3, ztree_1.int_7);
			ztree_2.build_tree(this);
			int num = InternalConstants.int_1 - 1;
			while (num >= 3 && short_4[ZTree.sbyte_0[num] * 2 + 1] == 0)
			{
				num--;
			}
			int_47 += 3 * (num + 1) + 5 + 5 + 4;
			return num;
		}

		internal void send_all_trees(int int_52, int int_53, int int_54)
		{
			send_bits(int_52 - 257, 5);
			send_bits(int_53 - 1, 5);
			send_bits(int_54 - 4, 4);
			for (int i = 0; i < int_54; i++)
			{
				send_bits(short_4[ZTree.sbyte_0[i] * 2 + 1], 3);
			}
			send_tree(short_2, int_52 - 1);
			send_tree(short_3, int_53 - 1);
		}

		internal void send_tree(short[] short_7, int int_52)
		{
			int num = -1;
			int num2 = short_7[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			if (num2 == 0)
			{
				num4 = 138;
				num5 = 3;
			}
			for (int i = 0; i <= int_52; i++)
			{
				int num6 = num2;
				num2 = short_7[(i + 1) * 2 + 1];
				if (++num3 < num4 && num6 == num2)
				{
					continue;
				}
				if (num3 < num5)
				{
					do
					{
						send_code(num6, short_4);
					}
					while (--num3 != 0);
				}
				else if (num6 != 0)
				{
					if (num6 != num)
					{
						send_code(num6, short_4);
						num3--;
					}
					send_code(InternalConstants.int_7, short_4);
					send_bits(num3 - 3, 2);
				}
				else if (num3 <= 10)
				{
					send_code(InternalConstants.int_8, short_4);
					send_bits(num3 - 3, 3);
				}
				else
				{
					send_code(InternalConstants.int_9, short_4);
					send_bits(num3 - 11, 7);
				}
				num3 = 0;
				num = num6;
				if (num2 == 0)
				{
					num4 = 138;
					num5 = 3;
				}
				else if (num6 == num2)
				{
					num4 = 6;
					num5 = 3;
				}
				else
				{
					num4 = 7;
					num5 = 4;
				}
			}
		}

		private void put_bytes(byte[] byte_2, int int_52, int int_53)
		{
			Array.Copy(byte_2, int_52, byte_0, int_21, int_53);
			int_21 += int_53;
		}

		internal void send_code(int int_52, short[] short_7)
		{
			int num = int_52 * 2;
			send_bits(short_7[num] & 0xFFFF, short_7[num + 1] & 0xFFFF);
		}

		internal void send_bits(int int_52, int int_53)
		{
			if (int_51 > int_13 - int_53)
			{
				short_6 |= (short)((int_52 << int_51) & 0xFFFF);
				byte_0[int_21++] = (byte)short_6;
				byte_0[int_21++] = (byte)(short_6 >> 8);
				short_6 = (short)((uint)int_52 >> int_13 - int_51);
				int_51 += int_53 - int_13;
			}
			else
			{
				short_6 |= (short)((int_52 << int_51) & 0xFFFF);
				int_51 += int_53;
			}
		}

		internal void _tr_align()
		{
			send_bits(int_8 << 1, 3);
			send_code(int_18, StaticTree.short_0);
			bi_flush();
			if (1 + int_50 + 10 - int_51 < 9)
			{
				send_bits(int_8 << 1, 3);
				send_code(int_18, StaticTree.short_0);
				bi_flush();
			}
			int_50 = 7;
		}

		internal bool _tr_tally(int int_52, int int_53)
		{
			byte_0[int_46 + int_45 * 2] = (byte)((uint)int_52 >> 8);
			byte_0[int_46 + int_45 * 2 + 1] = (byte)int_52;
			byte_0[int_43 + int_45] = (byte)int_53;
			int_45++;
			if (int_52 == 0)
			{
				short_2[int_53 * 2]++;
			}
			else
			{
				int_49++;
				int_52--;
				short_2[(ZTree.sbyte_2[int_53] + InternalConstants.int_3 + 1) * 2]++;
				short_3[ZTree.DistanceCode(int_52) * 2]++;
			}
			if ((int_45 & 0x1FFF) == 0 && compressionLevel_0 > CompressionLevel.Level2)
			{
				int num = int_45 << 3;
				int num2 = int_36 - int_32;
				for (int i = 0; i < InternalConstants.int_2; i++)
				{
					num = (int)(num + short_3[i * 2] * (5L + ZTree.int_3[i]));
				}
				num >>= 3;
				if (int_49 < int_45 / 2 && num < num2 / 2)
				{
					return true;
				}
			}
			return int_45 == int_44 - 1 || int_45 == int_44;
		}

		internal void send_compressed_block(short[] short_7, short[] short_8)
		{
			int num = 0;
			if (int_45 != 0)
			{
				do
				{
					int num2 = int_46 + num * 2;
					int num3 = ((byte_0[num2] << 8) & 0xFF00) | (byte_0[num2 + 1] & 0xFF);
					int num4 = byte_0[int_43 + num] & 0xFF;
					num++;
					if (num3 != 0)
					{
						int num5 = ZTree.sbyte_2[num4];
						send_code(num5 + InternalConstants.int_3 + 1, short_7);
						int num6 = ZTree.int_2[num5];
						if (num6 != 0)
						{
							num4 -= ZTree.int_5[num5];
							send_bits(num4, num6);
						}
						num3--;
						num5 = ZTree.DistanceCode(num3);
						send_code(num5, short_8);
						num6 = ZTree.int_3[num5];
						if (num6 != 0)
						{
							num3 -= ZTree.int_6[num5];
							send_bits(num3, num6);
						}
					}
					else
					{
						send_code(num4, short_7);
					}
				}
				while (num < int_45);
			}
			send_code(int_18, short_7);
			int_50 = short_7[int_18 * 2 + 1];
		}

		internal void set_data_type()
		{
			int i = 0;
			int num = 0;
			int num2 = 0;
			for (; i < 7; i++)
			{
				num2 += short_2[i * 2];
			}
			for (; i < 128; i++)
			{
				num += short_2[i * 2];
			}
			for (; i < InternalConstants.int_3; i++)
			{
				num2 += short_2[i * 2];
			}
			sbyte_0 = (sbyte)((num2 <= num >> 2) ? int_11 : int_10);
		}

		internal void bi_flush()
		{
			if (int_51 == 16)
			{
				byte_0[int_21++] = (byte)short_6;
				byte_0[int_21++] = (byte)(short_6 >> 8);
				short_6 = 0;
				int_51 = 0;
			}
			else if (int_51 >= 8)
			{
				byte_0[int_21++] = (byte)short_6;
				short_6 >>= 8;
				int_51 -= 8;
			}
		}

		internal void bi_windup()
		{
			if (int_51 > 8)
			{
				byte_0[int_21++] = (byte)short_6;
				byte_0[int_21++] = (byte)(short_6 >> 8);
			}
			else if (int_51 > 0)
			{
				byte_0[int_21++] = (byte)short_6;
			}
			short_6 = 0;
			int_51 = 0;
		}

		internal void copy_block(int int_52, int int_53, bool bool_2)
		{
			bi_windup();
			int_50 = 8;
			if (bool_2)
			{
				byte_0[int_21++] = (byte)int_53;
				byte_0[int_21++] = (byte)(int_53 >> 8);
				byte_0[int_21++] = (byte)(~int_53);
				byte_0[int_21++] = (byte)(~int_53 >> 8);
			}
			put_bytes(byte_1, int_52, int_53);
		}

		internal void flush_block_only(bool bool_2)
		{
			_tr_flush_block((int_32 < 0) ? (-1) : int_32, int_36 - int_32, bool_2);
			int_32 = int_36;
			zlibCodec_0.flush_pending();
		}

		internal BlockState DeflateNone(FlushType flushType_0)
		{
			int num = 65535;
			if (65535 > byte_0.Length - 5)
			{
				num = byte_0.Length - 5;
			}
			while (true)
			{
				if (int_38 <= 1)
				{
					_fillWindow();
					if (int_38 == 0 && flushType_0 == FlushType.None)
					{
						return BlockState.NeedMore;
					}
					if (int_38 == 0)
					{
						flush_block_only(flushType_0 == FlushType.Finish);
						if (zlibCodec_0.int_3 == 0)
						{
							return (flushType_0 == FlushType.Finish) ? BlockState.FinishStarted : BlockState.NeedMore;
						}
						return (flushType_0 != FlushType.Finish) ? BlockState.BlockDone : BlockState.FinishDone;
					}
				}
				int_36 += int_38;
				int_38 = 0;
				int num2 = int_32 + num;
				if (int_36 == 0 || int_36 >= num2)
				{
					int_38 = int_36 - num2;
					int_36 = num2;
					flush_block_only(false);
					if (zlibCodec_0.int_3 == 0)
					{
						break;
					}
				}
				if (int_36 - int_32 >= int_23 - int_16)
				{
					flush_block_only(false);
					if (zlibCodec_0.int_3 == 0)
					{
						return BlockState.NeedMore;
					}
				}
			}
			return BlockState.NeedMore;
		}

		internal void _tr_stored_block(int int_52, int int_53, bool bool_2)
		{
			send_bits((int_7 << 1) + (bool_2 ? 1 : 0), 3);
			copy_block(int_52, int_53, true);
		}

		internal void _tr_flush_block(int int_52, int int_53, bool bool_2)
		{
			int num = 0;
			int num2;
			int num3;
			if (compressionLevel_0 > CompressionLevel.None)
			{
				if (sbyte_0 == int_12)
				{
					set_data_type();
				}
				ztree_0.build_tree(this);
				ztree_1.build_tree(this);
				num = build_bl_tree();
				num2 = int_47 + 3 + 7 >> 3;
				num3 = int_48 + 3 + 7 >> 3;
				if (num3 <= num2)
				{
					num2 = num3;
				}
			}
			else
			{
				num2 = (num3 = int_53 + 5);
			}
			if (int_53 + 4 <= num2 && int_52 != -1)
			{
				_tr_stored_block(int_52, int_53, bool_2);
			}
			else if (num3 == num2)
			{
				send_bits((int_8 << 1) + (bool_2 ? 1 : 0), 3);
				send_compressed_block(StaticTree.short_0, StaticTree.short_1);
			}
			else
			{
				send_bits((int_9 << 1) + (bool_2 ? 1 : 0), 3);
				send_all_trees(ztree_0.int_7 + 1, ztree_1.int_7 + 1, num + 1);
				send_compressed_block(short_2, short_3);
			}
			_InitializeBlocks();
			if (bool_2)
			{
				bi_windup();
			}
		}

		private void _fillWindow()
		{
			do
			{
				int num = int_26 - int_38 - int_36;
				if (num == 0 && int_36 == 0 && int_38 == 0)
				{
					num = int_23;
				}
				else if (num == -1)
				{
					num--;
				}
				else if (int_36 >= int_23 + int_23 - int_16)
				{
					Array.Copy(byte_1, int_23, byte_1, 0, int_23);
					int_37 -= int_23;
					int_36 -= int_23;
					int_32 -= int_23;
					int num2 = int_28;
					int num3 = num2;
					do
					{
						int num4 = short_1[--num3] & 0xFFFF;
						short_1[num3] = (short)((num4 >= int_23) ? (num4 - int_23) : 0);
					}
					while (--num2 != 0);
					num2 = int_23;
					num3 = num2;
					do
					{
						int num4 = short_0[--num3] & 0xFFFF;
						short_0[num3] = (short)((num4 >= int_23) ? (num4 - int_23) : 0);
					}
					while (--num2 != 0);
					num += int_23;
				}
				if (zlibCodec_0.int_1 != 0)
				{
					int num2 = zlibCodec_0.read_buf(byte_1, int_36 + int_38, num);
					int_38 += num2;
					if (int_38 >= int_14)
					{
						int_27 = byte_1[int_36] & 0xFF;
						int_27 = ((int_27 << int_31) ^ (byte_1[int_36 + 1] & 0xFF)) & int_30;
					}
					continue;
				}
				break;
			}
			while (int_38 < int_16 && zlibCodec_0.int_1 != 0);
		}

		internal BlockState DeflateFast(FlushType flushType_0)
		{
			int num = 0;
			while (true)
			{
				if (int_38 < int_16)
				{
					_fillWindow();
					if (int_38 < int_16 && flushType_0 == FlushType.None)
					{
						return BlockState.NeedMore;
					}
					if (int_38 == 0)
					{
						flush_block_only(flushType_0 == FlushType.Finish);
						if (zlibCodec_0.int_3 == 0)
						{
							if (flushType_0 == FlushType.Finish)
							{
								return BlockState.FinishStarted;
							}
							return BlockState.NeedMore;
						}
						return (flushType_0 != FlushType.Finish) ? BlockState.BlockDone : BlockState.FinishDone;
					}
				}
				if (int_38 >= int_14)
				{
					int_27 = ((int_27 << int_31) ^ (byte_1[int_36 + (int_14 - 1)] & 0xFF)) & int_30;
					num = short_1[int_27] & 0xFFFF;
					short_0[int_36 & int_25] = short_1[int_27];
					short_1[int_27] = (short)int_36;
				}
				if (num != 0L && ((int_36 - num) & 0xFFFF) <= int_23 - int_16 && compressionStrategy_0 != CompressionStrategy.HuffmanOnly)
				{
					int_33 = longest_match(num);
				}
				bool flag;
				if (int_33 >= int_14)
				{
					flag = _tr_tally(int_36 - int_37, int_33 - int_14);
					int_38 -= int_33;
					if (int_33 <= config_0.int_1 && int_38 >= int_14)
					{
						int_33--;
						do
						{
							int_36++;
							int_27 = ((int_27 << int_31) ^ (byte_1[int_36 + (int_14 - 1)] & 0xFF)) & int_30;
							num = short_1[int_27] & 0xFFFF;
							short_0[int_36 & int_25] = short_1[int_27];
							short_1[int_27] = (short)int_36;
						}
						while (--int_33 != 0);
						int_36++;
					}
					else
					{
						int_36 += int_33;
						int_33 = 0;
						int_27 = byte_1[int_36] & 0xFF;
						int_27 = ((int_27 << int_31) ^ (byte_1[int_36 + 1] & 0xFF)) & int_30;
					}
				}
				else
				{
					flag = _tr_tally(0, byte_1[int_36] & 0xFF);
					int_38--;
					int_36++;
				}
				if (flag)
				{
					flush_block_only(false);
					if (zlibCodec_0.int_3 == 0)
					{
						break;
					}
				}
			}
			return BlockState.NeedMore;
		}

		internal BlockState DeflateSlow(FlushType flushType_0)
		{
			int num = 0;
			while (true)
			{
				if (int_38 < int_16)
				{
					_fillWindow();
					if (int_38 < int_16 && flushType_0 == FlushType.None)
					{
						return BlockState.NeedMore;
					}
					if (int_38 == 0)
					{
						if (int_35 != 0)
						{
							bool flag = _tr_tally(0, byte_1[int_36 - 1] & 0xFF);
							int_35 = 0;
						}
						flush_block_only(flushType_0 == FlushType.Finish);
						if (zlibCodec_0.int_3 == 0)
						{
							if (flushType_0 == FlushType.Finish)
							{
								return BlockState.FinishStarted;
							}
							return BlockState.NeedMore;
						}
						return (flushType_0 != FlushType.Finish) ? BlockState.BlockDone : BlockState.FinishDone;
					}
				}
				if (int_38 >= int_14)
				{
					int_27 = ((int_27 << int_31) ^ (byte_1[int_36 + (int_14 - 1)] & 0xFF)) & int_30;
					num = short_1[int_27] & 0xFFFF;
					short_0[int_36 & int_25] = short_1[int_27];
					short_1[int_27] = (short)int_36;
				}
				int_39 = int_33;
				int_34 = int_37;
				int_33 = int_14 - 1;
				if (num != 0 && int_39 < config_0.int_1 && ((int_36 - num) & 0xFFFF) <= int_23 - int_16)
				{
					if (compressionStrategy_0 != CompressionStrategy.HuffmanOnly)
					{
						int_33 = longest_match(num);
					}
					if (int_33 <= 5 && (compressionStrategy_0 == CompressionStrategy.Filtered || (int_33 == int_14 && int_36 - int_37 > 4096)))
					{
						int_33 = int_14 - 1;
					}
				}
				if (int_39 >= int_14 && int_33 <= int_39)
				{
					int num2 = int_36 + int_38 - int_14;
					bool flag = _tr_tally(int_36 - 1 - int_34, int_39 - int_14);
					int_38 -= int_39 - 1;
					int_39 -= 2;
					do
					{
						if (++int_36 <= num2)
						{
							int_27 = ((int_27 << int_31) ^ (byte_1[int_36 + (int_14 - 1)] & 0xFF)) & int_30;
							num = short_1[int_27] & 0xFFFF;
							short_0[int_36 & int_25] = short_1[int_27];
							short_1[int_27] = (short)int_36;
						}
					}
					while (--int_39 != 0);
					int_35 = 0;
					int_33 = int_14 - 1;
					int_36++;
					if (flag)
					{
						flush_block_only(false);
						if (zlibCodec_0.int_3 == 0)
						{
							return BlockState.NeedMore;
						}
					}
				}
				else if (int_35 != 0)
				{
					bool flag;
					if (flag = _tr_tally(0, byte_1[int_36 - 1] & 0xFF))
					{
						flush_block_only(false);
					}
					int_36++;
					int_38--;
					if (zlibCodec_0.int_3 == 0)
					{
						break;
					}
				}
				else
				{
					int_35 = 1;
					int_36++;
					int_38--;
				}
			}
			return BlockState.NeedMore;
		}

		internal int longest_match(int int_52)
		{
			int num = config_0.int_3;
			int num2 = int_36;
			int num3 = int_39;
			int num4 = ((int_36 > int_23 - int_16) ? (int_36 - (int_23 - int_16)) : 0);
			int num5 = config_0.int_2;
			int num6 = int_25;
			int num7 = int_36 + int_15;
			byte b = byte_1[num2 + num3 - 1];
			byte b2 = byte_1[num2 + num3];
			if (int_39 >= config_0.int_0)
			{
				num >>= 2;
			}
			if (num5 > int_38)
			{
				num5 = int_38;
			}
			do
			{
				int num8 = int_52;
				if (byte_1[num8 + num3] != b2 || byte_1[num8 + num3 - 1] != b || byte_1[num8] != byte_1[num2] || byte_1[++num8] != byte_1[num2 + 1])
				{
					continue;
				}
				num2 += 2;
				num8++;
				while (byte_1[++num2] == byte_1[++num8] && byte_1[++num2] == byte_1[++num8] && byte_1[++num2] == byte_1[++num8] && byte_1[++num2] == byte_1[++num8] && byte_1[++num2] == byte_1[++num8] && byte_1[++num2] == byte_1[++num8] && byte_1[++num2] == byte_1[++num8] && byte_1[++num2] == byte_1[++num8] && num2 < num7)
				{
				}
				int num9 = int_15 - (num7 - num2);
				num2 = num7 - int_15;
				if (num9 > num3)
				{
					int_37 = int_52;
					num3 = num9;
					if (num9 >= num5)
					{
						break;
					}
					b = byte_1[num2 + num3 - 1];
					b2 = byte_1[num2 + num3];
				}
			}
			while ((int_52 = short_0[int_52 & num6] & 0xFFFF) > num4 && --num != 0);
			if (num3 <= int_38)
			{
				return num3;
			}
			return int_38;
		}

		internal int Initialize(ZlibCodec zlibCodec_1, CompressionLevel compressionLevel_1)
		{
			return Initialize(zlibCodec_1, compressionLevel_1, 15);
		}

		internal int Initialize(ZlibCodec zlibCodec_1, CompressionLevel compressionLevel_1, int int_52)
		{
			return Initialize(zlibCodec_1, compressionLevel_1, int_52, int_1, CompressionStrategy.Default);
		}

		internal int Initialize(ZlibCodec zlibCodec_1, CompressionLevel compressionLevel_1, int int_52, CompressionStrategy compressionStrategy_1)
		{
			return Initialize(zlibCodec_1, compressionLevel_1, int_52, int_1, compressionStrategy_1);
		}

		internal int Initialize(ZlibCodec zlibCodec_1, CompressionLevel compressionLevel_1, int int_52, int int_53, CompressionStrategy compressionStrategy_1)
		{
			zlibCodec_0 = zlibCodec_1;
			zlibCodec_0.string_0 = null;
			if (int_52 >= 9 && int_52 <= 15)
			{
				if (int_53 < 1 || int_53 > int_0)
				{
					throw new ZlibException(string.Format("memLevel must be in the range 1.. {0}", int_0));
				}
				zlibCodec_0.deflateManager_0 = this;
				int_24 = int_52;
				int_23 = 1 << int_24;
				int_25 = int_23 - 1;
				int_29 = int_53 + 7;
				int_28 = 1 << int_29;
				int_30 = int_28 - 1;
				int_31 = (int_29 + int_14 - 1) / int_14;
				byte_1 = new byte[int_23 * 2];
				short_0 = new short[int_23];
				short_1 = new short[int_28];
				int_44 = 1 << int_53 + 6;
				byte_0 = new byte[int_44 * 4];
				int_46 = int_44;
				int_43 = 3 * int_44;
				compressionLevel_0 = compressionLevel_1;
				compressionStrategy_0 = compressionStrategy_1;
				Reset();
				return 0;
			}
			throw new ZlibException("windowBits must be in the range 9..15.");
		}

		internal void Reset()
		{
			ZlibCodec zlibCodec = zlibCodec_0;
			zlibCodec_0.long_1 = 0L;
			zlibCodec.long_0 = 0L;
			zlibCodec_0.string_0 = null;
			int_21 = 0;
			int_20 = 0;
			bool_0 = false;
			int_19 = ((!Boolean_0) ? int_4 : int_3);
			zlibCodec_0.uint_0 = Adler.Adler32(0u, null, 0, 0);
			int_22 = 0;
			_InitializeTreeData();
			_InitializeLazyMatch();
		}

		internal int End()
		{
			if (int_19 != int_3 && int_19 != int_4 && int_19 != int_5)
			{
				return -2;
			}
			byte_0 = null;
			short_1 = null;
			short_0 = null;
			byte_1 = null;
			return (int_19 == int_4) ? (-3) : 0;
		}

		private void SetDeflater()
		{
			switch (config_0.deflateFlavor_0)
			{
			case DeflateFlavor.Store:
				compressFunc_0 = DeflateNone;
				break;
			case DeflateFlavor.Fast:
				compressFunc_0 = DeflateFast;
				break;
			case DeflateFlavor.Slow:
				compressFunc_0 = DeflateSlow;
				break;
			}
		}

		internal int SetParams(CompressionLevel compressionLevel_1, CompressionStrategy compressionStrategy_1)
		{
			int result = 0;
			if (compressionLevel_0 != compressionLevel_1)
			{
				Config config = Config.Lookup(compressionLevel_1);
				if (config.deflateFlavor_0 != config_0.deflateFlavor_0 && zlibCodec_0.long_0 != 0L)
				{
					result = zlibCodec_0.Deflate(FlushType.Partial);
				}
				compressionLevel_0 = compressionLevel_1;
				config_0 = config;
				SetDeflater();
			}
			compressionStrategy_0 = compressionStrategy_1;
			return result;
		}

		internal int SetDictionary(byte[] byte_2)
		{
			int num = byte_2.Length;
			int sourceIndex = 0;
			if (byte_2 != null && int_19 == int_3)
			{
				zlibCodec_0.uint_0 = Adler.Adler32(zlibCodec_0.uint_0, byte_2, 0, byte_2.Length);
				if (num < int_14)
				{
					return 0;
				}
				if (num > int_23 - int_16)
				{
					num = int_23 - int_16;
					sourceIndex = byte_2.Length - num;
				}
				Array.Copy(byte_2, sourceIndex, byte_1, 0, num);
				int_36 = num;
				int_32 = num;
				int_27 = byte_1[0] & 0xFF;
				int_27 = ((int_27 << int_31) ^ (byte_1[1] & 0xFF)) & int_30;
				for (int i = 0; i <= num - int_14; i++)
				{
					int_27 = ((int_27 << int_31) ^ (byte_1[i + (int_14 - 1)] & 0xFF)) & int_30;
					short_0[i & int_25] = short_1[int_27];
					short_1[int_27] = (short)i;
				}
				return 0;
			}
			throw new ZlibException("Stream error.");
		}

		internal int Deflate(FlushType flushType_0)
		{
			if (zlibCodec_0.byte_1 != null && (zlibCodec_0.byte_0 != null || zlibCodec_0.int_1 == 0) && (int_19 != int_5 || flushType_0 == FlushType.Finish))
			{
				if (zlibCodec_0.int_3 == 0)
				{
					zlibCodec_0.string_0 = string_0[7];
					throw new ZlibException("OutputBuffer is full (AvailableBytesOut == 0)");
				}
				int num = int_22;
				int_22 = (int)flushType_0;
				if (int_19 == int_3)
				{
					int num2 = int_6 + (int_24 - 8 << 4) << 8;
					int num3 = (int)((compressionLevel_0 - 1) & (CompressionLevel)255) >> 1;
					if (num3 > 3)
					{
						num3 = 3;
					}
					num2 |= num3 << 6;
					if (int_36 != 0)
					{
						num2 |= int_2;
					}
					num2 += 31 - num2 % 31;
					int_19 = int_4;
					byte_0[int_21++] = (byte)(num2 >> 8);
					byte_0[int_21++] = (byte)num2;
					if (int_36 != 0)
					{
						byte_0[int_21++] = (byte)((zlibCodec_0.uint_0 & 0xFF000000u) >> 24);
						byte_0[int_21++] = (byte)((zlibCodec_0.uint_0 & 0xFF0000) >> 16);
						byte_0[int_21++] = (byte)((zlibCodec_0.uint_0 & 0xFF00) >> 8);
						byte_0[int_21++] = (byte)(zlibCodec_0.uint_0 & 0xFFu);
					}
					zlibCodec_0.uint_0 = Adler.Adler32(0u, null, 0, 0);
				}
				if (int_21 != 0)
				{
					zlibCodec_0.flush_pending();
					if (zlibCodec_0.int_3 == 0)
					{
						int_22 = -1;
						return 0;
					}
				}
				else if (zlibCodec_0.int_1 == 0 && (int)flushType_0 <= num && flushType_0 != FlushType.Finish)
				{
					return 0;
				}
				if (int_19 == int_5 && zlibCodec_0.int_1 != 0)
				{
					zlibCodec_0.string_0 = string_0[7];
					throw new ZlibException("status == FINISH_STATE && _codec.AvailableBytesIn != 0");
				}
				if (zlibCodec_0.int_1 != 0 || int_38 != 0 || (flushType_0 != 0 && int_19 != int_5))
				{
					BlockState blockState = compressFunc_0(flushType_0);
					if (blockState == BlockState.FinishStarted || blockState == BlockState.FinishDone)
					{
						int_19 = int_5;
					}
					if (blockState == BlockState.NeedMore || blockState == BlockState.FinishStarted)
					{
						if (zlibCodec_0.int_3 == 0)
						{
							int_22 = -1;
						}
						return 0;
					}
					if (blockState == BlockState.BlockDone)
					{
						if (flushType_0 == FlushType.Partial)
						{
							_tr_align();
						}
						else
						{
							_tr_stored_block(0, 0, false);
							if (flushType_0 == FlushType.Full)
							{
								for (int i = 0; i < int_28; i++)
								{
									short_1[i] = 0;
								}
							}
						}
						zlibCodec_0.flush_pending();
						if (zlibCodec_0.int_3 == 0)
						{
							int_22 = -1;
							return 0;
						}
					}
				}
				if (flushType_0 != FlushType.Finish)
				{
					return 0;
				}
				if (Boolean_0 && !bool_0)
				{
					byte_0[int_21++] = (byte)((zlibCodec_0.uint_0 & 0xFF000000u) >> 24);
					byte_0[int_21++] = (byte)((zlibCodec_0.uint_0 & 0xFF0000) >> 16);
					byte_0[int_21++] = (byte)((zlibCodec_0.uint_0 & 0xFF00) >> 8);
					byte_0[int_21++] = (byte)(zlibCodec_0.uint_0 & 0xFFu);
					zlibCodec_0.flush_pending();
					bool_0 = true;
					return (int_21 == 0) ? 1 : 0;
				}
				return 1;
			}
			zlibCodec_0.string_0 = string_0[4];
			throw new ZlibException(string.Format("Something is fishy. [{0}]", zlibCodec_0.string_0));
		}
	}
}
