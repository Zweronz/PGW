using System;
using System.IO;

namespace WebSocketSharp.Net
{
	internal class RequestStream : Stream
	{
		private long long_0;

		private byte[] byte_0;

		private int int_0;

		private bool bool_0;

		private int int_1;

		private Stream stream_0;

		public override bool CanRead
		{
			get
			{
				return true;
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
				return false;
			}
		}

		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		internal RequestStream(Stream stream_1, byte[] byte_1, int int_2, int int_3)
			: this(stream_1, byte_1, int_2, int_3, -1L)
		{
		}

		internal RequestStream(Stream stream_1, byte[] byte_1, int int_2, int int_3, long long_1)
		{
			stream_0 = stream_1;
			byte_0 = byte_1;
			int_1 = int_2;
			int_0 = int_3;
			long_0 = long_1;
		}

		private int fillFromBuffer(byte[] byte_1, int int_2, int int_3)
		{
			if (byte_1 == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (int_2 < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "A negative value.");
			}
			if (int_3 < 0)
			{
				throw new ArgumentOutOfRangeException("count", "A negative value.");
			}
			int num = byte_1.Length;
			if (int_2 + int_3 > num)
			{
				throw new ArgumentException("The sum of 'offset' and 'count' is greater than 'buffer' length.");
			}
			if (long_0 == 0L)
			{
				return -1;
			}
			if (int_0 != 0 && int_3 != 0)
			{
				if (int_3 > int_0)
				{
					int_3 = int_0;
				}
				if (long_0 > 0L && int_3 > long_0)
				{
					int_3 = (int)long_0;
				}
				Buffer.BlockCopy(byte_0, int_1, byte_1, int_2, int_3);
				int_1 += int_3;
				int_0 -= int_3;
				if (long_0 > 0L)
				{
					long_0 -= int_3;
				}
				return int_3;
			}
			return 0;
		}

		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (bool_0)
			{
				throw new ObjectDisposedException(GetType().ToString());
			}
			int num = fillFromBuffer(buffer, offset, count);
			if (num <= 0 && num != -1)
			{
				if (long_0 >= 0L && count > long_0)
				{
					count = (int)long_0;
				}
				return stream_0.BeginRead(buffer, offset, count, callback, state);
			}
			HttpStreamAsyncResult httpStreamAsyncResult = new HttpStreamAsyncResult(callback, state);
			httpStreamAsyncResult.Byte_0 = buffer;
			httpStreamAsyncResult.Int32_1 = offset;
			httpStreamAsyncResult.Int32_0 = count;
			httpStreamAsyncResult.Int32_2 = ((num > 0) ? num : 0);
			httpStreamAsyncResult.Complete();
			return httpStreamAsyncResult;
		}

		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		public override void Close()
		{
			bool_0 = true;
		}

		public override int EndRead(IAsyncResult asyncResult)
		{
			if (bool_0)
			{
				throw new ObjectDisposedException(GetType().ToString());
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (asyncResult is HttpStreamAsyncResult)
			{
				HttpStreamAsyncResult httpStreamAsyncResult = (HttpStreamAsyncResult)asyncResult;
				if (!httpStreamAsyncResult.IsCompleted)
				{
					httpStreamAsyncResult.AsyncWaitHandle.WaitOne();
				}
				return httpStreamAsyncResult.Int32_2;
			}
			int num = stream_0.EndRead(asyncResult);
			if (num > 0 && long_0 > 0L)
			{
				long_0 -= num;
			}
			return num;
		}

		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		public override void Flush()
		{
		}

		public override int Read(byte[] b, int off, int len)
		{
			if (bool_0)
			{
				throw new ObjectDisposedException(GetType().ToString());
			}
			int num = fillFromBuffer(b, off, len);
			if (num == -1)
			{
				return 0;
			}
			if (num > 0)
			{
				return num;
			}
			num = stream_0.Read(b, off, len);
			if (num > 0 && long_0 > 0L)
			{
				long_0 -= num;
			}
			return num;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}
	}
}
