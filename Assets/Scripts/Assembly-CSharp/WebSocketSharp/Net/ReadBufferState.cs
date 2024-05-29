namespace WebSocketSharp.Net
{
	internal class ReadBufferState
	{
		private HttpStreamAsyncResult httpStreamAsyncResult_0;

		private byte[] byte_0;

		private int int_0;

		private int int_1;

		private int int_2;

		public HttpStreamAsyncResult HttpStreamAsyncResult_0
		{
			get
			{
				return httpStreamAsyncResult_0;
			}
			set
			{
				httpStreamAsyncResult_0 = value;
			}
		}

		public byte[] Byte_0
		{
			get
			{
				return byte_0;
			}
			set
			{
				byte_0 = value;
			}
		}

		public int Int32_0
		{
			get
			{
				return int_0;
			}
			set
			{
				int_0 = value;
			}
		}

		public int Int32_1
		{
			get
			{
				return int_1;
			}
			set
			{
				int_1 = value;
			}
		}

		public int Int32_2
		{
			get
			{
				return int_2;
			}
			set
			{
				int_2 = value;
			}
		}

		public ReadBufferState(byte[] byte_1, int int_3, int int_4, HttpStreamAsyncResult httpStreamAsyncResult_1)
		{
			byte_0 = byte_1;
			int_2 = int_3;
			int_0 = int_4;
			int_1 = int_4;
			httpStreamAsyncResult_0 = httpStreamAsyncResult_1;
		}
	}
}
