using System;
using System.IO;

namespace BestHTTP.Decompression.Zlib
{
	internal class DeflateStream : Stream
	{
		internal ZlibBaseStream zlibBaseStream_0;

		internal Stream stream_0;

		private bool bool_0;

		public virtual FlushType FlushType_0
		{
			get
			{
				return zlibBaseStream_0.flushType_0;
			}
			set
			{
				if (bool_0)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				zlibBaseStream_0.flushType_0 = value;
			}
		}

		public int Int32_0
		{
			get
			{
				return zlibBaseStream_0.int_0;
			}
			set
			{
				if (bool_0)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				if (zlibBaseStream_0.byte_0 != null)
				{
					throw new ZlibException("The working buffer is already set.");
				}
				if (value < 1024)
				{
					throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer, at least {1}.", value, 1024));
				}
				zlibBaseStream_0.int_0 = value;
			}
		}

		public CompressionStrategy CompressionStrategy_0
		{
			get
			{
				return zlibBaseStream_0.compressionStrategy_0;
			}
			set
			{
				if (bool_0)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				zlibBaseStream_0.compressionStrategy_0 = value;
			}
		}

		public virtual long Int64_0
		{
			get
			{
				return zlibBaseStream_0.zlibCodec_0.long_0;
			}
		}

		public virtual long Int64_1
		{
			get
			{
				return zlibBaseStream_0.zlibCodec_0.long_1;
			}
		}

		public override bool CanRead
		{
			get
			{
				if (bool_0)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return zlibBaseStream_0.stream_0.CanRead;
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
				if (bool_0)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return zlibBaseStream_0.stream_0.CanWrite;
			}
		}

		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override long Position
		{
			get
			{
				if (zlibBaseStream_0.streamMode_0 == ZlibBaseStream.StreamMode.Writer)
				{
					return zlibBaseStream_0.zlibCodec_0.long_1;
				}
				if (zlibBaseStream_0.streamMode_0 == ZlibBaseStream.StreamMode.Reader)
				{
					return zlibBaseStream_0.zlibCodec_0.long_0;
				}
				return 0L;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public DeflateStream(Stream stream_1, CompressionMode compressionMode_0)
			: this(stream_1, compressionMode_0, CompressionLevel.Default, false)
		{
		}

		public DeflateStream(Stream stream_1, CompressionMode compressionMode_0, CompressionLevel compressionLevel_0)
			: this(stream_1, compressionMode_0, compressionLevel_0, false)
		{
		}

		public DeflateStream(Stream stream_1, CompressionMode compressionMode_0, bool bool_1)
			: this(stream_1, compressionMode_0, CompressionLevel.Default, bool_1)
		{
		}

		public DeflateStream(Stream stream_1, CompressionMode compressionMode_0, CompressionLevel compressionLevel_0, bool bool_1)
		{
			stream_0 = stream_1;
			zlibBaseStream_0 = new ZlibBaseStream(stream_1, compressionMode_0, compressionLevel_0, ZlibStreamFlavor.DEFLATE, bool_1);
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!bool_0)
				{
					if (disposing && zlibBaseStream_0 != null)
					{
						zlibBaseStream_0.Close();
					}
					bool_0 = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		public override void Flush()
		{
			if (bool_0)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			zlibBaseStream_0.Flush();
		}

		public override int Read(byte[] b, int off, int len)
		{
			if (bool_0)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			return zlibBaseStream_0.Read(b, off, len);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (bool_0)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			zlibBaseStream_0.Write(buffer, offset, count);
		}

		public static byte[] CompressString(string string_0)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream_ = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(string_0, stream_);
				return memoryStream.ToArray();
			}
		}

		public static byte[] CompressBuffer(byte[] byte_0)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream_ = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(byte_0, stream_);
				return memoryStream.ToArray();
			}
		}

		public static string UncompressString(byte[] byte_0)
		{
			using (MemoryStream stream_ = new MemoryStream(byte_0))
			{
				Stream stream_2 = new DeflateStream(stream_, CompressionMode.Decompress);
				return ZlibBaseStream.UncompressString(byte_0, stream_2);
			}
		}

		public static byte[] UncompressBuffer(byte[] byte_0)
		{
			using (MemoryStream stream_ = new MemoryStream(byte_0))
			{
				Stream stream_2 = new DeflateStream(stream_, CompressionMode.Decompress);
				return ZlibBaseStream.UncompressBuffer(byte_0, stream_2);
			}
		}
	}
}
