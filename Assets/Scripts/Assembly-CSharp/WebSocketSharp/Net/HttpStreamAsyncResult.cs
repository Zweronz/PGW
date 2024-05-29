using System;
using System.Threading;

namespace WebSocketSharp.Net
{
	internal class HttpStreamAsyncResult : IAsyncResult
	{
		private byte[] byte_0;

		private AsyncCallback asyncCallback_0;

		private bool bool_0;

		private int int_0;

		private Exception exception_0;

		private int int_1;

		private object object_0;

		private object object_1;

		private int int_2;

		private ManualResetEvent manualResetEvent_0;

		internal byte[] Byte_0
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

		internal int Int32_0
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

		internal Exception Exception_0
		{
			get
			{
				return exception_0;
			}
		}

		internal bool Boolean_0
		{
			get
			{
				return exception_0 != null;
			}
		}

		internal int Int32_1
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

		internal int Int32_2
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

		public object AsyncState
		{
			get
			{
				return object_0;
			}
		}

		public WaitHandle AsyncWaitHandle
		{
			get
			{
				lock (object_1)
				{
					return manualResetEvent_0 ?? (manualResetEvent_0 = new ManualResetEvent(bool_0));
				}
			}
		}

		public bool CompletedSynchronously
		{
			get
			{
				return int_2 == int_0;
			}
		}

		public bool IsCompleted
		{
			get
			{
				lock (object_1)
				{
					return bool_0;
				}
			}
		}

		internal HttpStreamAsyncResult(AsyncCallback asyncCallback_1, object object_2)
		{
			asyncCallback_0 = asyncCallback_1;
			object_0 = object_2;
			object_1 = new object();
		}

		internal void Complete()
		{
			lock (object_1)
			{
				if (bool_0)
				{
					return;
				}
				bool_0 = true;
				if (manualResetEvent_0 != null)
				{
					manualResetEvent_0.Set();
				}
				if (asyncCallback_0 != null)
				{
					asyncCallback_0.BeginInvoke(this, delegate(IAsyncResult iasyncResult_0)
					{
						asyncCallback_0.EndInvoke(iasyncResult_0);
					}, null);
				}
			}
		}

		internal void Complete(Exception exception_1)
		{
			exception_0 = exception_1;
			Complete();
		}
	}
}
