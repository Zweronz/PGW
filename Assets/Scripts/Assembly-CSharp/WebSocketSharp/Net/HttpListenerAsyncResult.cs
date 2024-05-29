using System;
using System.Threading;

namespace WebSocketSharp.Net
{
	internal class HttpListenerAsyncResult : IAsyncResult
	{
		private AsyncCallback asyncCallback_0;

		private bool bool_0;

		private HttpListenerContext httpListenerContext_0;

		private bool bool_1;

		private Exception exception_0;

		private bool bool_2;

		private object object_0;

		private object object_1;

		private bool bool_3;

		private ManualResetEvent manualResetEvent_0;

		internal bool Boolean_0
		{
			get
			{
				return bool_1;
			}
			set
			{
				bool_1 = value;
			}
		}

		internal bool Boolean_1
		{
			get
			{
				return bool_2;
			}
			set
			{
				bool_2 = value;
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
				return bool_3;
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

		internal HttpListenerAsyncResult(AsyncCallback asyncCallback_1, object object_2)
		{
			asyncCallback_0 = asyncCallback_1;
			object_0 = object_2;
			object_1 = new object();
		}

		private static void complete(HttpListenerAsyncResult httpListenerAsyncResult_0)
		{
			httpListenerAsyncResult_0.bool_0 = true;
			ManualResetEvent manualResetEvent = httpListenerAsyncResult_0.manualResetEvent_0;
			if (manualResetEvent != null)
			{
				manualResetEvent.Set();
			}
			AsyncCallback asyncCallback_0 = httpListenerAsyncResult_0.asyncCallback_0;
			if (asyncCallback_0 == null)
			{
				return;
			}
			//ThreadPool.UnsafeQueueUserWorkItem(delegate
			//{
			//	try
			//	{
			//		asyncCallback_0(httpListenerAsyncResult_0);
			//	}
			//	catch
			//	{
			//	}
			//}, null);
		}

		internal void Complete(Exception exception_1)
		{
			exception_0 = ((!bool_2 || !(exception_1 is ObjectDisposedException)) ? exception_1 : new HttpListenerException(500, "Listener closed."));
			lock (object_1)
			{
				complete(this);
			}
		}

		internal void Complete(HttpListenerContext httpListenerContext_1)
		{
			Complete(httpListenerContext_1, false);
		}

		internal void Complete(HttpListenerContext httpListenerContext_1, bool bool_4)
		{
			HttpListener httpListener_ = httpListenerContext_1.HttpListener_0;
			if (!httpListener_.Authenticate(httpListenerContext_1))
			{
				httpListener_.BeginGetContext(this);
				return;
			}
			httpListenerContext_0 = httpListenerContext_1;
			bool_3 = bool_4;
			lock (object_1)
			{
				complete(this);
			}
		}

		internal HttpListenerContext GetContext()
		{
			if (exception_0 != null)
			{
				throw exception_0;
			}
			return httpListenerContext_0;
		}
	}
}
