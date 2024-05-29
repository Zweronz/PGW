using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BestHTTP.Decompression.Crc;

namespace BestHTTP.Decompression.Zlib
{
	internal class ZlibBaseStream : Stream
	{
		internal enum StreamMode
		{
			Writer = 0,
			Reader = 1,
			Undefined = 2
		}

		protected internal ZlibCodec zlibCodec_0;

		protected internal StreamMode streamMode_0 = StreamMode.Undefined;

		protected internal FlushType flushType_0;

		protected internal ZlibStreamFlavor zlibStreamFlavor_0;

		protected internal CompressionMode compressionMode_0;

		protected internal CompressionLevel compressionLevel_0;

		protected internal bool bool_0;

		protected internal byte[] byte_0;

		protected internal int int_0 = 16384;

		protected internal byte[] byte_1 = new byte[1];

		protected internal Stream stream_0;

		protected internal CompressionStrategy compressionStrategy_0;

		private CRC32 crc32_0;

		protected internal string string_0;

		protected internal string string_1;

		protected internal DateTime dateTime_0;

		protected internal int int_1;

		private bool bool_1;

		internal int Int32_0
		{
			get
			{
				if (crc32_0 == null)
				{
					return 0;
				}
				return crc32_0.Int32_0;
			}
		}

		protected internal bool Boolean_0
		{
			get
			{
				return compressionMode_0 == CompressionMode.Compress;
			}
		}

		private ZlibCodec ZlibCodec_0
		{
			get
			{
				if (zlibCodec_0 == null)
				{
					bool flag = zlibStreamFlavor_0 == ZlibStreamFlavor.ZLIB;
					zlibCodec_0 = new ZlibCodec();
					if (compressionMode_0 == CompressionMode.Decompress)
					{
						zlibCodec_0.InitializeInflate(flag);
					}
					else
					{
						zlibCodec_0.compressionStrategy_0 = compressionStrategy_0;
						zlibCodec_0.InitializeDeflate(compressionLevel_0, flag);
					}
				}
				return zlibCodec_0;
			}
		}

		private byte[] Byte_0
		{
			get
			{
				if (byte_0 == null)
				{
					byte_0 = new byte[int_0];
				}
				return byte_0;
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
				return stream_0.CanSeek;
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
				return stream_0.Length;
			}
		}

		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public ZlibBaseStream(Stream stream_1, CompressionMode compressionMode_1, CompressionLevel compressionLevel_1, ZlibStreamFlavor zlibStreamFlavor_1, bool bool_2)
		{
			flushType_0 = FlushType.None;
			stream_0 = stream_1;
			bool_0 = bool_2;
			compressionMode_0 = compressionMode_1;
			zlibStreamFlavor_0 = zlibStreamFlavor_1;
			compressionLevel_0 = compressionLevel_1;
			if (zlibStreamFlavor_1 == ZlibStreamFlavor.GZIP)
			{
				crc32_0 = new CRC32();
			}
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (crc32_0 != null)
			{
				crc32_0.SlurpBlock(buffer, offset, count);
			}
			if (streamMode_0 == StreamMode.Undefined)
			{
				streamMode_0 = StreamMode.Writer;
			}
			else if (streamMode_0 != 0)
			{
				throw new ZlibException("Cannot Write after Reading.");
			}
			if (count == 0)
			{
				return;
			}
			ZlibCodec_0.byte_0 = buffer;
			zlibCodec_0.int_0 = offset;
			zlibCodec_0.int_1 = count;
			bool flag = false;
			while (true)
			{
				zlibCodec_0.byte_1 = Byte_0;
				zlibCodec_0.int_2 = 0;
				zlibCodec_0.int_3 = byte_0.Length;
				int num = (Boolean_0 ? zlibCodec_0.Deflate(flushType_0) : zlibCodec_0.Inflate(flushType_0));
				if (num != 0 && num != 1)
				{
					break;
				}
				stream_0.Write(byte_0, 0, byte_0.Length - zlibCodec_0.int_3);
				flag = zlibCodec_0.int_1 == 0 && zlibCodec_0.int_3 != 0;
				if (zlibStreamFlavor_0 == ZlibStreamFlavor.GZIP && !Boolean_0)
				{
					flag = zlibCodec_0.int_1 == 8 && zlibCodec_0.int_3 != 0;
				}
				if (flag)
				{
					return;
				}
			}
			throw new ZlibException(((!Boolean_0) ? "in" : "de") + "flating: " + zlibCodec_0.string_0);
		}

		private void finish()
		{
			if (zlibCodec_0 == null)
			{
				return;
			}
			if (streamMode_0 == StreamMode.Writer)
			{
				bool flag = false;
				do
				{
					zlibCodec_0.byte_1 = Byte_0;
					zlibCodec_0.int_2 = 0;
					zlibCodec_0.int_3 = byte_0.Length;
					int num = (Boolean_0 ? zlibCodec_0.Deflate(FlushType.Finish) : zlibCodec_0.Inflate(FlushType.Finish));
					if (num == 1 || num == 0)
					{
						if (byte_0.Length - zlibCodec_0.int_3 > 0)
						{
							stream_0.Write(byte_0, 0, byte_0.Length - zlibCodec_0.int_3);
						}
						flag = zlibCodec_0.int_1 == 0 && zlibCodec_0.int_3 != 0;
						if (zlibStreamFlavor_0 == ZlibStreamFlavor.GZIP && !Boolean_0)
						{
							flag = zlibCodec_0.int_1 == 8 && zlibCodec_0.int_3 != 0;
						}
						continue;
					}
					string text = ((!Boolean_0) ? "in" : "de") + "flating";
					if (zlibCodec_0.string_0 == null)
					{
						throw new ZlibException(string.Format("{0}: (rc = {1})", text, num));
					}
					throw new ZlibException(text + ": " + zlibCodec_0.string_0);
				}
				while (!flag);
				Flush();
				if (zlibStreamFlavor_0 == ZlibStreamFlavor.GZIP)
				{
					if (!Boolean_0)
					{
						throw new ZlibException("Writing with decompression is not supported.");
					}
					int int32_ = crc32_0.Int32_0;
					stream_0.Write(BitConverter.GetBytes(int32_), 0, 4);
					int value = (int)(crc32_0.Int64_0 & 0xFFFFFFFFL);
					stream_0.Write(BitConverter.GetBytes(value), 0, 4);
				}
			}
			else
			{
				if (streamMode_0 != StreamMode.Reader || zlibStreamFlavor_0 != ZlibStreamFlavor.GZIP)
				{
					return;
				}
				if (Boolean_0)
				{
					throw new ZlibException("Reading with compression is not supported.");
				}
				if (zlibCodec_0.long_1 == 0L)
				{
					return;
				}
				byte[] array = new byte[8];
				if (zlibCodec_0.int_1 < 8)
				{
					Array.Copy(zlibCodec_0.byte_0, zlibCodec_0.int_0, array, 0, zlibCodec_0.int_1);
					int num2 = 8 - zlibCodec_0.int_1;
					int num3 = stream_0.Read(array, zlibCodec_0.int_1, num2);
					if (num2 != num3)
					{
						throw new ZlibException(string.Format("Missing or incomplete GZIP trailer. Expected 8 bytes, got {0}.", zlibCodec_0.int_1 + num3));
					}
				}
				else
				{
					Array.Copy(zlibCodec_0.byte_0, zlibCodec_0.int_0, array, 0, array.Length);
				}
				int num4 = BitConverter.ToInt32(array, 0);
				int int32_2 = crc32_0.Int32_0;
				int num5 = BitConverter.ToInt32(array, 4);
				int num6 = (int)(zlibCodec_0.long_1 & 0xFFFFFFFFL);
				if (int32_2 != num4)
				{
					throw new ZlibException(string.Format("Bad CRC32 in GZIP trailer. (actual({0:X8})!=expected({1:X8}))", int32_2, num4));
				}
				if (num6 != num5)
				{
					throw new ZlibException(string.Format("Bad size in GZIP trailer. (actual({0})!=expected({1}))", num6, num5));
				}
			}
		}

		private void end()
		{
			if (ZlibCodec_0 != null)
			{
				if (Boolean_0)
				{
					zlibCodec_0.EndDeflate();
				}
				else
				{
					zlibCodec_0.EndInflate();
				}
				zlibCodec_0 = null;
			}
		}

		public override void Close()
		{
			if (stream_0 == null)
			{
				return;
			}
			try
			{
				finish();
			}
			finally
			{
				end();
				if (!bool_0)
				{
					stream_0.Dispose();
				}
				stream_0 = null;
			}
		}

		public override void Flush()
		{
			stream_0.Flush();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		public override void SetLength(long value)
		{
			stream_0.SetLength(value);
		}

		private string ReadZeroTerminatedString()
		{
			List<byte> list = new List<byte>();
			bool flag = false;
			do
			{
				int num = stream_0.Read(byte_1, 0, 1);
				if (num == 1)
				{
					if (byte_1[0] == 0)
					{
						flag = true;
					}
					else
					{
						list.Add(byte_1[0]);
					}
					continue;
				}
				throw new ZlibException("Unexpected EOF reading GZIP header.");
			}
			while (!flag);
			byte[] array = list.ToArray();
			return GZipStream.encoding_0.GetString(array, 0, array.Length);
		}

		private int _ReadAndValidateGzipHeader()
		{
			int num = 0;
			byte[] array = new byte[10];
			int num2 = stream_0.Read(array, 0, array.Length);
			switch (num2)
			{
			case 0:
				return 0;
			default:
				throw new ZlibException("Not a valid GZIP stream.");
			case 10:
				if (array[0] == 31 && array[1] == 139 && array[2] == 8)
				{
					int num3 = BitConverter.ToInt32(array, 4);
					dateTime_0 = GZipStream.dateTime_0.AddSeconds(num3);
					num += num2;
					if ((array[3] & 4) == 4)
					{
						num2 = stream_0.Read(array, 0, 2);
						num += num2;
						short num4 = (short)(array[0] + array[1] * 256);
						byte[] array2 = new byte[num4];
						num2 = stream_0.Read(array2, 0, array2.Length);
						if (num2 != num4)
						{
							throw new ZlibException("Unexpected end-of-file reading GZIP header.");
						}
						num += num2;
					}
					if ((array[3] & 8) == 8)
					{
						string_0 = ReadZeroTerminatedString();
					}
					if ((array[3] & 0x10) == 16)
					{
						string_1 = ReadZeroTerminatedString();
					}
					if ((array[3] & 2) == 2)
					{
						Read(byte_1, 0, 1);
					}
					return num;
				}
				throw new ZlibException("Bad GZIP header.");
			}
		}

		public override int Read(byte[] b, int off, int len)
		{
			if (streamMode_0 == StreamMode.Undefined)
			{
				if (!stream_0.CanRead)
				{
					throw new ZlibException("The stream is not readable.");
				}
				streamMode_0 = StreamMode.Reader;
				ZlibCodec_0.int_1 = 0;
				if (zlibStreamFlavor_0 == ZlibStreamFlavor.GZIP)
				{
					int_1 = _ReadAndValidateGzipHeader();
					if (int_1 == 0)
					{
						return 0;
					}
				}
			}
			if (streamMode_0 != StreamMode.Reader)
			{
				throw new ZlibException("Cannot Read after Writing.");
			}
			if (len == 0)
			{
				return 0;
			}
			if (bool_1 && Boolean_0)
			{
				return 0;
			}
			if (b == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (len < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (off < b.GetLowerBound(0))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (off + len > b.GetLength(0))
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num = 0;
			zlibCodec_0.byte_1 = b;
			zlibCodec_0.int_2 = off;
			zlibCodec_0.int_3 = len;
			zlibCodec_0.byte_0 = Byte_0;
			do
			{
				if (zlibCodec_0.int_1 == 0 && !bool_1)
				{
					zlibCodec_0.int_0 = 0;
					zlibCodec_0.int_1 = stream_0.Read(byte_0, 0, byte_0.Length);
					if (zlibCodec_0.int_1 == 0)
					{
						bool_1 = true;
					}
				}
				num = ((!Boolean_0) ? zlibCodec_0.Inflate(flushType_0) : zlibCodec_0.Deflate(flushType_0));
				if (!bool_1 || num != -5)
				{
					if (num != 0 && num != 1)
					{
						throw new ZlibException(string.Format("{0}flating:  rc={1}  msg={2}", (!Boolean_0) ? "in" : "de", num, zlibCodec_0.string_0));
					}
					continue;
				}
				return 0;
			}
			while (((!bool_1 && num != 1) || zlibCodec_0.int_3 != len) && zlibCodec_0.int_3 > 0 && !bool_1 && num == 0);
			if (zlibCodec_0.int_3 > 0)
			{
				if (num != 0 || zlibCodec_0.int_1 == 0)
				{
				}
				if (bool_1 && Boolean_0)
				{
					num = zlibCodec_0.Deflate(FlushType.Finish);
					if (num != 0 && num != 1)
					{
						throw new ZlibException(string.Format("Deflating:  rc={0}  msg={1}", num, zlibCodec_0.string_0));
					}
				}
			}
			num = len - zlibCodec_0.int_3;
			if (crc32_0 != null)
			{
				crc32_0.SlurpBlock(b, off, num);
			}
			return num;
		}

		public static void CompressString(string string_2, Stream stream_1)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(string_2);
			using (stream_1)
			{
				stream_1.Write(bytes, 0, bytes.Length);
			}
		}

		public static void CompressBuffer(byte[] byte_2, Stream stream_1)
		{
			using (stream_1)
			{
				stream_1.Write(byte_2, 0, byte_2.Length);
			}
		}

		public static string UncompressString(byte[] byte_2, Stream stream_1)
		{
			byte[] array = new byte[1024];
			Encoding uTF = Encoding.UTF8;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (stream_1)
				{
					int count;
					while ((count = stream_1.Read(array, 0, array.Length)) != 0)
					{
						memoryStream.Write(array, 0, count);
					}
				}
				memoryStream.Seek(0L, SeekOrigin.Begin);
				StreamReader streamReader = new StreamReader(memoryStream, uTF);
				return streamReader.ReadToEnd();
			}
		}

		public static byte[] UncompressBuffer(byte[] byte_2, Stream stream_1)
		{
			byte[] array = new byte[1024];
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (stream_1)
				{
					int count;
					while ((count = stream_1.Read(array, 0, array.Length)) != 0)
					{
						memoryStream.Write(array, 0, count);
					}
				}
				return memoryStream.ToArray();
			}
		}
	}
}
