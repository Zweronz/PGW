using System;
using System.Collections.Generic;
using System.Threading;

namespace UnityThreading
{
	public class Channel<T> : IDisposable
	{
		private List<T> buffer = new List<T>();

		private object setSyncRoot = new object();

		private object getSyncRoot = new object();

		private object disposeRoot = new object();

		private ManualResetEvent setEvent = new ManualResetEvent(false);

		private ManualResetEvent getEvent = new ManualResetEvent(true);

		private ManualResetEvent exitEvent = new ManualResetEvent(false);

		private bool disposed;

		public int Int32_0 { get; private set; }

		public Channel()
			: this(1)
		{
		}

		public Channel(int int_0)
		{
			if (int_0 < 1)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Must be greater or equal to 1.");
			}
			Int32_0 = int_0;
		}

		~Channel()
		{
			Dispose();
		}

		public void Resize(int int_0)
		{
			if (int_0 < 1)
			{
				throw new ArgumentOutOfRangeException("newBufferSize", "Must be greater or equal to 1.");
			}
			lock (setSyncRoot)
			{
				if (!disposed && WaitHandle.WaitAny(new WaitHandle[2] { exitEvent, getEvent }) != 0)
				{
					buffer.Clear();
					if (int_0 != Int32_0)
					{
						Int32_0 = int_0;
					}
				}
			}
		}

		public bool Set(T gparam_0)
		{
			return Set(gparam_0, int.MaxValue);
		}

		public bool Set(T gparam_0, int int_0)
		{
			lock (setSyncRoot)
			{
				if (disposed)
				{
					return false;
				}
				int num = WaitHandle.WaitAny(new WaitHandle[2] { exitEvent, getEvent }, int_0);
				if (num != 258 && num != 0)
				{
					buffer.Add(gparam_0);
					if (buffer.Count == Int32_0)
					{
						setEvent.Set();
						getEvent.Reset();
					}
					return true;
				}
				return false;
			}
		}

		public T Get()
		{
			return Get(int.MaxValue, default(T));
		}

		public T Get(int int_0, T gparam_0)
		{
			lock (getSyncRoot)
			{
				if (disposed)
				{
					return gparam_0;
				}
				int num = WaitHandle.WaitAny(new WaitHandle[2] { exitEvent, setEvent }, int_0);
				if (num != 258 && num != 0)
				{
					T result = buffer[0];
					buffer.RemoveAt(0);
					if (buffer.Count == 0)
					{
						getEvent.Set();
						setEvent.Reset();
					}
					return result;
				}
				return gparam_0;
			}
		}

		public void Close()
		{
			lock (disposeRoot)
			{
				if (!disposed)
				{
					exitEvent.Set();
				}
			}
		}

		public void Dispose()
		{
			if (disposed)
			{
				return;
			}
			lock (disposeRoot)
			{
				exitEvent.Set();
				lock (getSyncRoot)
				{
					lock (setSyncRoot)
					{
						setEvent.Close();
						setEvent = null;
						getEvent.Close();
						getEvent = null;
						exitEvent.Close();
						exitEvent = null;
						disposed = true;
					}
				}
			}
		}
	}
	public class Channel : Channel<object>
	{
	}
}
