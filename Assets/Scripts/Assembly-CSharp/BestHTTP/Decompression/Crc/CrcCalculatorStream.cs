using System;
using System.IO;

namespace BestHTTP.Decompression.Crc
{
	internal class CrcCalculatorStream : Stream, IDisposable
	{
		private static readonly long long_0 = -99L;

		internal Stream stream_0;

		private CRC32 crc32_0;

		private long long_1 = -99L;

		private bool bool_0;

		public long Int64_0
		{
			get
			{
				return crc32_0.Int64_0;
			}
		}

		public int Int32_0
		{
			get
			{
				return crc32_0.Int32_0;
			}
		}

		public bool Boolean_0
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

		public override bool CanRead
		{
			get
			{
				return stream_0.CanRead;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return stream_0.CanWrite;
			}
		}

		public override long Length
		{
			get
			{
				if (long_1 == long_0)
				{
					return stream_0.Length;
				}
				return long_1;
			}
		}

		public override long Position
		{
			get
			{
				return crc32_0.Int64_0;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public CrcCalculatorStream(Stream stream_1)
			: this(true, long_0, stream_1, null)
		{
		}

		public CrcCalculatorStream(Stream stream_1, bool bool_1)
			: this(bool_1, long_0, stream_1, null)
		{
		}

		public CrcCalculatorStream(Stream stream_1, long long_2)
			: this(true, long_2, stream_1, null)
		{
			if (long_2 < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		public CrcCalculatorStream(Stream stream_1, long long_2, bool bool_1)
			: this(bool_1, long_2, stream_1, null)
		{
			if (long_2 < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		public CrcCalculatorStream(Stream stream_1, long long_2, bool bool_1, CRC32 crc32_1)
			: this(bool_1, long_2, stream_1, crc32_1)
		{
			if (long_2 < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		private CrcCalculatorStream(bool bool_1, long long_2, Stream stream_1, CRC32 crc32_1)
		{
			stream_0 = stream_1;
			crc32_0 = crc32_1 ?? new CRC32();
			long_1 = long_2;
			bool_0 = bool_1;
		}

		void IDisposable.Dispose()
		{
			Close();
		}

		public override int Read(byte[] b, int off, int len)
		{
			int count = len;
			if (long_1 != long_0)
			{
				if (crc32_0.Int64_0 >= long_1)
				{
					return 0;
				}
				long num = long_1 - crc32_0.Int64_0;
				if (num < len)
				{
					count = (int)num;
				}
			}
			int num2 = stream_0.Read(b, off, count);
			if (num2 > 0)
			{
				crc32_0.SlurpBlock(b, off, num2);
			}
			return num2;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count > 0)
			{
				crc32_0.SlurpBlock(buffer, offset, count);
			}
			stream_0.Write(buffer, offset, count);
		}

		public override void Flush()
		{
			stream_0.Flush();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override void Close()
		{
			Dispose();
			if (!bool_0)
			{
				stream_0.Dispose();
			}
		}
	}
}
