using System;
using System.IO;
using System.Text;

namespace BestHTTP.Decompression.Zlib
{
	internal class GZipStream : Stream
	{
		public DateTime? nullable_0;

		private int int_0;

		internal ZlibBaseStream zlibBaseStream_0;

		private bool bool_0;

		private bool bool_1;

		private string string_0;

		private string string_1;

		private int int_1;

		internal static readonly DateTime dateTime_0 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		internal static readonly Encoding encoding_0 = Encoding.GetEncoding("iso-8859-1");

		public string String_0
		{
			get
			{
				return string_1;
			}
			set
			{
				if (bool_0)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				string_1 = value;
			}
		}

		public string String_1
		{
			get
			{
				return string_0;
			}
			set
			{
				if (bool_0)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				string_0 = value;
				if (string_0 != null)
				{
					if (string_0.IndexOf("/") != -1)
					{
						string_0 = string_0.Replace("/", "\\");
					}
					if (string_0.EndsWith("\\"))
					{
						throw new Exception("Illegal filename");
					}
					if (string_0.IndexOf("\\") != -1)
					{
						string_0 = Path.GetFileName(string_0);
					}
				}
			}
		}

		public int Int32_0
		{
			get
			{
				return int_1;
			}
		}

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
					throw new ObjectDisposedException("GZipStream");
				}
				zlibBaseStream_0.flushType_0 = value;
			}
		}

		public int Int32_1
		{
			get
			{
				return zlibBaseStream_0.int_0;
			}
			set
			{
				if (bool_0)
				{
					throw new ObjectDisposedException("GZipStream");
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
					throw new ObjectDisposedException("GZipStream");
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
					throw new ObjectDisposedException("GZipStream");
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
					return zlibBaseStream_0.zlibCodec_0.long_1 + int_0;
				}
				if (zlibBaseStream_0.streamMode_0 == ZlibBaseStream.StreamMode.Reader)
				{
					return zlibBaseStream_0.zlibCodec_0.long_0 + zlibBaseStream_0.int_1;
				}
				return 0L;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public GZipStream(Stream stream_0, CompressionMode compressionMode_0)
			: this(stream_0, compressionMode_0, CompressionLevel.Default, false)
		{
		}

		public GZipStream(Stream stream_0, CompressionMode compressionMode_0, CompressionLevel compressionLevel_0)
			: this(stream_0, compressionMode_0, compressionLevel_0, false)
		{
		}

		public GZipStream(Stream stream_0, CompressionMode compressionMode_0, bool bool_2)
			: this(stream_0, compressionMode_0, CompressionLevel.Default, bool_2)
		{
		}

		public GZipStream(Stream stream_0, CompressionMode compressionMode_0, CompressionLevel compressionLevel_0, bool bool_2)
		{
			zlibBaseStream_0 = new ZlibBaseStream(stream_0, compressionMode_0, compressionLevel_0, ZlibStreamFlavor.GZIP, bool_2);
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
						int_1 = zlibBaseStream_0.Int32_0;
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
				throw new ObjectDisposedException("GZipStream");
			}
			zlibBaseStream_0.Flush();
		}

		public override int Read(byte[] b, int off, int len)
		{
			if (bool_0)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			int result = zlibBaseStream_0.Read(b, off, len);
			if (!bool_1)
			{
				bool_1 = true;
				String_1 = zlibBaseStream_0.string_0;
				String_0 = zlibBaseStream_0.string_1;
			}
			return result;
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
				throw new ObjectDisposedException("GZipStream");
			}
			if (zlibBaseStream_0.streamMode_0 == ZlibBaseStream.StreamMode.Undefined)
			{
				if (!zlibBaseStream_0.Boolean_0)
				{
					throw new InvalidOperationException();
				}
				int_0 = EmitHeader();
			}
			zlibBaseStream_0.Write(buffer, offset, count);
		}

		private int EmitHeader()
		{
			byte[] array = ((String_0 != null) ? encoding_0.GetBytes(String_0) : null);
			byte[] array2 = ((String_1 != null) ? encoding_0.GetBytes(String_1) : null);
			int num = ((String_0 != null) ? (array.Length + 1) : 0);
			int num2 = ((String_1 != null) ? (array2.Length + 1) : 0);
			int num3 = 10 + num + num2;
			byte[] array3 = new byte[num3];
			int num4 = 0;
			num4 = 1;
			array3[0] = 31;
			num4 = 2;
			array3[1] = 139;
			num4 = 3;
			array3[2] = 8;
			byte b = 0;
			if (String_0 != null)
			{
				b = (byte)(b ^ 0x10u);
			}
			if (String_1 != null)
			{
				b = (byte)(b ^ 8u);
			}
			array3[num4++] = b;
			if (!nullable_0.HasValue)
			{
				nullable_0 = DateTime.Now;
			}
			int value = (int)(nullable_0.Value - dateTime_0).TotalSeconds;
			Array.Copy(BitConverter.GetBytes(value), 0, array3, num4, 4);
			num4 += 4;
			array3[num4++] = 0;
			array3[num4++] = byte.MaxValue;
			if (num2 != 0)
			{
				Array.Copy(array2, 0, array3, num4, num2 - 1);
				num4 += num2 - 1;
				array3[num4++] = 0;
			}
			if (num != 0)
			{
				Array.Copy(array, 0, array3, num4, num - 1);
				num4 += num - 1;
				array3[num4++] = 0;
			}
			zlibBaseStream_0.stream_0.Write(array3, 0, array3.Length);
			return array3.Length;
		}

		public static byte[] CompressString(string string_2)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream_ = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(string_2, stream_);
				return memoryStream.ToArray();
			}
		}

		public static byte[] CompressBuffer(byte[] byte_0)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream_ = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(byte_0, stream_);
				return memoryStream.ToArray();
			}
		}

		public static string UncompressString(byte[] byte_0)
		{
			using (MemoryStream stream_ = new MemoryStream(byte_0))
			{
				Stream stream_2 = new GZipStream(stream_, CompressionMode.Decompress);
				return ZlibBaseStream.UncompressString(byte_0, stream_2);
			}
		}

		public static byte[] UncompressBuffer(byte[] byte_0)
		{
			using (MemoryStream stream_ = new MemoryStream(byte_0))
			{
				Stream stream_2 = new GZipStream(stream_, CompressionMode.Decompress);
				return ZlibBaseStream.UncompressBuffer(byte_0, stream_2);
			}
		}
	}
}
