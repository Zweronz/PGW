using System;
using System.IO;
using System.Text;

namespace WebSocketSharp.Net
{
	internal class ResponseStream : Stream
	{
		private MemoryStream memoryStream_0;

		private static readonly byte[] byte_0 = new byte[2] { 13, 10 };

		private bool bool_0;

		private HttpListenerResponse httpListenerResponse_0;

		private bool bool_1;

		private Stream stream_0;

		private Action<byte[], int, int> action_0;

		private Action<byte[], int, int> action_1;

		private Action<byte[], int, int> action_2;

		public override bool CanRead
		{
			get
			{
				return false;
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
				return !bool_0;
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

		internal ResponseStream(Stream stream_1, HttpListenerResponse httpListenerResponse_1, bool bool_2)
		{
			stream_0 = stream_1;
			httpListenerResponse_0 = httpListenerResponse_1;
			if (bool_2)
			{
				action_0 = writeWithoutThrowingException;
				action_2 = writeChunkedWithoutThrowingException;
			}
			else
			{
				action_0 = stream_1.Write;
				action_2 = writeChunked;
			}
			memoryStream_0 = new MemoryStream();
		}

		private bool flush(bool bool_2)
		{
			if (!httpListenerResponse_0.Boolean_1)
			{
				if (!flushHeaders(bool_2))
				{
					if (bool_2)
					{
						httpListenerResponse_0.Boolean_0 = true;
					}
					return false;
				}
				bool_1 = httpListenerResponse_0.Boolean_3;
				action_1 = ((!bool_1) ? action_0 : action_2);
			}
			flushBody(bool_2);
			if (bool_2 && bool_1)
			{
				byte[] chunkSizeBytes = getChunkSizeBytes(0, true);
				action_0(chunkSizeBytes, 0, chunkSizeBytes.Length);
			}
			return true;
		}

		private void flushBody(bool bool_2)
		{
			using (memoryStream_0)
			{
				long length = memoryStream_0.Length;
				if (length > 2147483647L)
				{
					memoryStream_0.Position = 0L;
					int count = 1024;
					byte[] array = new byte[1024];
					int num = 0;
					while ((num = memoryStream_0.Read(array, 0, count)) > 0)
					{
						action_1(array, 0, num);
					}
				}
				else if (length > 0L)
				{
					action_1(memoryStream_0.GetBuffer(), 0, (int)length);
				}
			}
			memoryStream_0 = (bool_2 ? null : new MemoryStream());
		}

		private bool flushHeaders(bool bool_2)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				WebHeaderCollection webHeaderCollection = httpListenerResponse_0.WriteHeadersTo(memoryStream);
				long position = memoryStream.Position;
				long num = memoryStream.Length - position;
				if (num > 32768L)
				{
					return false;
				}
				if (!httpListenerResponse_0.Boolean_3 && httpListenerResponse_0.Int64_0 != memoryStream_0.Length)
				{
					return false;
				}
				action_0(memoryStream.GetBuffer(), (int)position, (int)num);
				httpListenerResponse_0.Boolean_0 = webHeaderCollection["Connection"] == "close";
				httpListenerResponse_0.Boolean_1 = true;
			}
			return true;
		}

		private static byte[] getChunkSizeBytes(int int_0, bool bool_2)
		{
			return Encoding.ASCII.GetBytes(string.Format("{0:x}\r\n{1}", int_0, (!bool_2) ? string.Empty : "\r\n"));
		}

		private void writeChunked(byte[] byte_1, int int_0, int int_1)
		{
			byte[] chunkSizeBytes = getChunkSizeBytes(int_1, false);
			stream_0.Write(chunkSizeBytes, 0, chunkSizeBytes.Length);
			stream_0.Write(byte_1, int_0, int_1);
			stream_0.Write(byte_0, 0, 2);
		}

		private void writeChunkedWithoutThrowingException(byte[] byte_1, int int_0, int int_1)
		{
			try
			{
				writeChunked(byte_1, int_0, int_1);
			}
			catch
			{
			}
		}

		private void writeWithoutThrowingException(byte[] byte_1, int int_0, int int_1)
		{
			try
			{
				stream_0.Write(byte_1, int_0, int_1);
			}
			catch
			{
			}
		}

		internal void Close(bool bool_2)
		{
			if (bool_0)
			{
				return;
			}
			bool_0 = true;
			if (!bool_2 && flush(true))
			{
				httpListenerResponse_0.Close();
			}
			else
			{
				if (bool_1)
				{
					byte[] chunkSizeBytes = getChunkSizeBytes(0, true);
					action_0(chunkSizeBytes, 0, chunkSizeBytes.Length);
				}
				memoryStream_0.Dispose();
				memoryStream_0 = null;
				httpListenerResponse_0.Abort();
			}
			httpListenerResponse_0 = null;
			stream_0 = null;
		}

		internal void InternalWrite(byte[] byte_1, int int_0, int int_1)
		{
			action_0(byte_1, int_0, int_1);
		}

		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (bool_0)
			{
				throw new ObjectDisposedException(GetType().ToString());
			}
			return memoryStream_0.BeginWrite(buffer, offset, count, callback, state);
		}

		public override void Close()
		{
			Close(false);
		}

		protected override void Dispose(bool disposing)
		{
			Close(!disposing);
		}

		public override int EndRead(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (bool_0)
			{
				throw new ObjectDisposedException(GetType().ToString());
			}
			memoryStream_0.EndWrite(asyncResult);
		}

		public override void Flush()
		{
			if (!bool_0 && (bool_1 || httpListenerResponse_0.Boolean_3))
			{
				flush(false);
			}
		}

		public override int Read(byte[] b, int off, int len)
		{
			throw new NotSupportedException();
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
			if (bool_0)
			{
				throw new ObjectDisposedException(GetType().ToString());
			}
			memoryStream_0.Write(buffer, offset, count);
		}
	}
}
