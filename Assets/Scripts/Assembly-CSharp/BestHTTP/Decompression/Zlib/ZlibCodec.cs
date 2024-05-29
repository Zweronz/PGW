using System;

namespace BestHTTP.Decompression.Zlib
{
	internal sealed class ZlibCodec
	{
		public byte[] byte_0;

		public int int_0;

		public int int_1;

		public long long_0;

		public byte[] byte_1;

		public int int_2;

		public int int_3;

		public long long_1;

		public string string_0;

		internal DeflateManager deflateManager_0;

		internal InflateManager inflateManager_0;

		internal uint uint_0;

		public CompressionLevel compressionLevel_0 = CompressionLevel.Default;

		public int int_4 = 15;

		public CompressionStrategy compressionStrategy_0;

		public int Int32_0
		{
			get
			{
				return (int)uint_0;
			}
		}

		public ZlibCodec()
		{
		}

		public ZlibCodec(CompressionMode compressionMode_0)
		{
			switch (compressionMode_0)
			{
			case CompressionMode.Compress:
				if (InitializeDeflate() != 0)
				{
					throw new ZlibException("Cannot initialize for deflate.");
				}
				break;
			case CompressionMode.Decompress:
				if (InitializeInflate() != 0)
				{
					throw new ZlibException("Cannot initialize for inflate.");
				}
				break;
			default:
				throw new ZlibException("Invalid ZlibStreamFlavor.");
			}
		}

		public int InitializeInflate()
		{
			return InitializeInflate(int_4);
		}

		public int InitializeInflate(bool bool_0)
		{
			return InitializeInflate(int_4, bool_0);
		}

		public int InitializeInflate(int int_5)
		{
			int_4 = int_5;
			return InitializeInflate(int_5, true);
		}

		public int InitializeInflate(int int_5, bool bool_0)
		{
			int_4 = int_5;
			if (deflateManager_0 != null)
			{
				throw new ZlibException("You may not call InitializeInflate() after calling InitializeDeflate().");
			}
			inflateManager_0 = new InflateManager(bool_0);
			return inflateManager_0.Initialize(this, int_5);
		}

		public int Inflate(FlushType flushType_0)
		{
			if (inflateManager_0 == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return inflateManager_0.Inflate(flushType_0);
		}

		public int EndInflate()
		{
			if (inflateManager_0 == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			int result = inflateManager_0.End();
			inflateManager_0 = null;
			return result;
		}

		public int SyncInflate()
		{
			if (inflateManager_0 == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return inflateManager_0.Sync();
		}

		public int InitializeDeflate()
		{
			return _InternalInitializeDeflate(true);
		}

		public int InitializeDeflate(CompressionLevel compressionLevel_1)
		{
			compressionLevel_0 = compressionLevel_1;
			return _InternalInitializeDeflate(true);
		}

		public int InitializeDeflate(CompressionLevel compressionLevel_1, bool bool_0)
		{
			compressionLevel_0 = compressionLevel_1;
			return _InternalInitializeDeflate(bool_0);
		}

		public int InitializeDeflate(CompressionLevel compressionLevel_1, int int_5)
		{
			compressionLevel_0 = compressionLevel_1;
			int_4 = int_5;
			return _InternalInitializeDeflate(true);
		}

		public int InitializeDeflate(CompressionLevel compressionLevel_1, int int_5, bool bool_0)
		{
			compressionLevel_0 = compressionLevel_1;
			int_4 = int_5;
			return _InternalInitializeDeflate(bool_0);
		}

		private int _InternalInitializeDeflate(bool bool_0)
		{
			if (inflateManager_0 != null)
			{
				throw new ZlibException("You may not call InitializeDeflate() after calling InitializeInflate().");
			}
			deflateManager_0 = new DeflateManager();
			deflateManager_0.Boolean_0 = bool_0;
			return deflateManager_0.Initialize(this, compressionLevel_0, int_4, compressionStrategy_0);
		}

		public int Deflate(FlushType flushType_0)
		{
			if (deflateManager_0 == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return deflateManager_0.Deflate(flushType_0);
		}

		public int EndDeflate()
		{
			if (deflateManager_0 == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			deflateManager_0 = null;
			return 0;
		}

		public void ResetDeflate()
		{
			if (deflateManager_0 == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			deflateManager_0.Reset();
		}

		public int SetDeflateParams(CompressionLevel compressionLevel_1, CompressionStrategy compressionStrategy_1)
		{
			if (deflateManager_0 == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return deflateManager_0.SetParams(compressionLevel_1, compressionStrategy_1);
		}

		public int SetDictionary(byte[] byte_2)
		{
			if (inflateManager_0 != null)
			{
				return inflateManager_0.SetDictionary(byte_2);
			}
			if (deflateManager_0 == null)
			{
				throw new ZlibException("No Inflate or Deflate state!");
			}
			return deflateManager_0.SetDictionary(byte_2);
		}

		internal void flush_pending()
		{
			int int_ = deflateManager_0.int_21;
			if (int_ > int_3)
			{
				int_ = int_3;
			}
			if (int_ != 0)
			{
				if (deflateManager_0.byte_0.Length <= deflateManager_0.int_20 || byte_1.Length <= int_2 || deflateManager_0.byte_0.Length < deflateManager_0.int_20 + int_ || byte_1.Length < int_2 + int_)
				{
					throw new ZlibException(string.Format("Invalid State. (pending.Length={0}, pendingCount={1})", deflateManager_0.byte_0.Length, deflateManager_0.int_21));
				}
				Array.Copy(deflateManager_0.byte_0, deflateManager_0.int_20, byte_1, int_2, int_);
				int_2 += int_;
				deflateManager_0.int_20 += int_;
				long_1 += int_;
				int_3 -= int_;
				deflateManager_0.int_21 -= int_;
				if (deflateManager_0.int_21 == 0)
				{
					deflateManager_0.int_20 = 0;
				}
			}
		}

		internal int read_buf(byte[] byte_2, int int_5, int int_6)
		{
			int num = int_1;
			if (num > int_6)
			{
				num = int_6;
			}
			if (num == 0)
			{
				return 0;
			}
			int_1 -= num;
			if (deflateManager_0.Boolean_0)
			{
				uint_0 = Adler.Adler32(uint_0, byte_0, int_0, num);
			}
			Array.Copy(byte_0, int_0, byte_2, int_5, num);
			int_0 += num;
			long_0 += num;
			return num;
		}
	}
}
