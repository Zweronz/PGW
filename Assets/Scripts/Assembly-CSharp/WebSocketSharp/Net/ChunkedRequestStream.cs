using System;
using System.IO;

namespace WebSocketSharp.Net
{
	internal class ChunkedRequestStream : RequestStream
	{
		private const int int_2 = 8192;

		private HttpListenerContext httpListenerContext_0;

		private ChunkStream chunkStream_0;

		private bool bool_1;

		private bool bool_2;

		internal ChunkStream ChunkStream_0
		{
			get
			{
				return chunkStream_0;
			}
			set
			{
				chunkStream_0 = value;
			}
		}

		internal ChunkedRequestStream(Stream stream_1, byte[] byte_1, int int_3, int int_4, HttpListenerContext httpListenerContext_1)
			: base(stream_1, byte_1, int_3, int_4)
		{
			httpListenerContext_0 = httpListenerContext_1;
			chunkStream_0 = new ChunkStream((WebHeaderCollection)httpListenerContext_1.HttpListenerRequest_0.NameValueCollection_0);
		}

		private void onRead(IAsyncResult iasyncResult_0)
		{
			ReadBufferState readBufferState = (ReadBufferState)iasyncResult_0.AsyncState;
			HttpStreamAsyncResult httpStreamAsyncResult_ = readBufferState.HttpStreamAsyncResult_0;
			try
			{
				int int_ = base.EndRead(iasyncResult_0);
				chunkStream_0.Write(httpStreamAsyncResult_.Byte_0, httpStreamAsyncResult_.Int32_1, int_);
				int_ = chunkStream_0.Read(readBufferState.Byte_0, readBufferState.Int32_2, readBufferState.Int32_0);
				readBufferState.Int32_2 += int_;
				readBufferState.Int32_0 -= int_;
				if (readBufferState.Int32_0 != 0 && chunkStream_0.Boolean_0 && int_ != 0)
				{
					httpStreamAsyncResult_.Int32_1 = 0;
					httpStreamAsyncResult_.Int32_0 = Math.Min(8192, chunkStream_0.Int32_0 + 6);
					base.BeginRead(httpStreamAsyncResult_.Byte_0, httpStreamAsyncResult_.Int32_1, httpStreamAsyncResult_.Int32_0, (AsyncCallback)onRead, (object)readBufferState);
				}
				else
				{
					bool_2 = !chunkStream_0.Boolean_0 && int_ == 0;
					httpStreamAsyncResult_.Int32_0 = readBufferState.Int32_1 - readBufferState.Int32_0;
					httpStreamAsyncResult_.Complete();
				}
			}
			catch (Exception ex)
			{
				httpListenerContext_0.HttpConnection_0.SendError(ex.Message, 400);
				httpStreamAsyncResult_.Complete(ex);
			}
		}

		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (bool_1)
			{
				throw new ObjectDisposedException(GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "A negative value.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "A negative value.");
			}
			int num = buffer.Length;
			if (offset + count > num)
			{
				throw new ArgumentException("The sum of 'offset' and 'count' is greater than 'buffer' length.");
			}
			HttpStreamAsyncResult httpStreamAsyncResult = new HttpStreamAsyncResult(callback, state);
			if (bool_2)
			{
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			int num2 = chunkStream_0.Read(buffer, offset, count);
			offset += num2;
			count -= num2;
			if (count == 0)
			{
				httpStreamAsyncResult.Int32_0 = num2;
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			if (!chunkStream_0.Boolean_0)
			{
				bool_2 = num2 == 0;
				httpStreamAsyncResult.Int32_0 = num2;
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			httpStreamAsyncResult.Byte_0 = new byte[8192];
			httpStreamAsyncResult.Int32_1 = 0;
			httpStreamAsyncResult.Int32_0 = 8192;
			ReadBufferState readBufferState = new ReadBufferState(buffer, offset, count, httpStreamAsyncResult);
			readBufferState.Int32_1 += num2;
			base.BeginRead(httpStreamAsyncResult.Byte_0, httpStreamAsyncResult.Int32_1, httpStreamAsyncResult.Int32_0, (AsyncCallback)onRead, (object)readBufferState);
			return httpStreamAsyncResult;
		}

		public override void Close()
		{
			if (!bool_1)
			{
				bool_1 = true;
				base.Close();
			}
		}

		public override int EndRead(IAsyncResult asyncResult)
		{
			if (bool_1)
			{
				throw new ObjectDisposedException(GetType().ToString());
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			HttpStreamAsyncResult httpStreamAsyncResult = asyncResult as HttpStreamAsyncResult;
			if (httpStreamAsyncResult == null)
			{
				throw new ArgumentException("A wrong IAsyncResult.", "asyncResult");
			}
			if (!httpStreamAsyncResult.IsCompleted)
			{
				httpStreamAsyncResult.AsyncWaitHandle.WaitOne();
			}
			if (httpStreamAsyncResult.Boolean_0)
			{
				throw new HttpListenerException(400, "I/O operation aborted.");
			}
			return httpStreamAsyncResult.Int32_0;
		}

		public override int Read(byte[] b, int off, int len)
		{
			IAsyncResult asyncResult = BeginRead(b, off, len, null, null);
			return EndRead(asyncResult);
		}
	}
}
