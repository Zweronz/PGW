using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.IO
{
	public sealed class RecyclableMemoryStreamManager
	{
		public sealed class Events
		{
			public enum MemoryStreamBufferType
			{
				Small = 0,
				Large = 1
			}

			public enum MemoryStreamDiscardReason
			{
				TooLarge = 0,
				EnoughFree = 1
			}

			public static Events events_0 = new Events();

			public void MemoryStreamCreated(Guid guid_0, string string_0, int int_0)
			{
			}

			public void MemoryStreamDisposed(Guid guid_0, string string_0)
			{
			}

			public void MemoryStreamDoubleDispose(Guid guid_0, string string_0, string string_1, string string_2, string string_3)
			{
			}

			public void MemoryStreamFinalized(Guid guid_0, string string_0, string string_1)
			{
			}

			public void MemoryStreamToArray(Guid guid_0, string string_0, string string_1, int int_0)
			{
			}

			public void MemoryStreamManagerInitialized(int int_0, int int_1, int int_2)
			{
			}

			public void MemoryStreamNewBlockCreated(long long_0)
			{
			}

			public void MemoryStreamNewLargeBufferCreated(int int_0, long long_0)
			{
			}

			public void MemoryStreamNonPooledLargeBufferCreated(int int_0, string string_0, string string_1)
			{
			}

			public void MemoryStreamDiscardBuffer(MemoryStreamBufferType memoryStreamBufferType_0, string string_0, MemoryStreamDiscardReason memoryStreamDiscardReason_0)
			{
			}

			public void MemoryStreamOverCapacity(int int_0, long long_0, string string_0, string string_1)
			{
			}
		}

		public delegate void EventHandler();

		public delegate void LargeBufferDiscardedEventHandler(Events.MemoryStreamDiscardReason memoryStreamDiscardReason_0);

		public delegate void StreamLengthReportHandler(long long_0);

		public delegate void UsageReportEventHandler(long long_0, long long_1, long long_2, long long_3);

		public const int int_0 = 131072;

		public const int int_1 = 1048576;

		public const int int_2 = 134217728;

		private readonly int int_3;

		private readonly long[] long_0;

		private readonly long[] long_1;

		private readonly int int_4;

		private readonly ConcurrentStack<byte[]>[] concurrentStack_0;

		private readonly int int_5;

		private readonly ConcurrentStack<byte[]> concurrentStack_1;

		private long long_2;

		private long long_3;

		private EventHandler eventHandler_0;

		private EventHandler eventHandler_1;

		private EventHandler eventHandler_2;

		private EventHandler eventHandler_3;

		private EventHandler eventHandler_4;

		private EventHandler eventHandler_5;

		private StreamLengthReportHandler streamLengthReportHandler_0;

		private EventHandler eventHandler_6;

		private LargeBufferDiscardedEventHandler largeBufferDiscardedEventHandler_0;

		private UsageReportEventHandler usageReportEventHandler_0;

		[CompilerGenerated]
		private long long_4;

		[CompilerGenerated]
		private long long_5;

		[CompilerGenerated]
		private long long_6;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private bool bool_1;

		public int Int32_0
		{
			get
			{
				return int_3;
			}
		}

		public int Int32_1
		{
			get
			{
				return int_4;
			}
		}

		public int Int32_2
		{
			get
			{
				return int_5;
			}
		}

		public long Int64_0
		{
			get
			{
				return long_2;
			}
		}

		public long Int64_1
		{
			get
			{
				return long_3;
			}
		}

		public long Int64_2
		{
			get
			{
				return long_0.Sum();
			}
		}

		public long Int64_3
		{
			get
			{
				return long_1.Sum();
			}
		}

		public long Int64_4
		{
			get
			{
				return concurrentStack_1.Count;
			}
		}

		public long Int64_5
		{
			get
			{
				long num = 0L;
				ConcurrentStack<byte[]>[] array = concurrentStack_0;
				foreach (ConcurrentStack<byte[]> concurrentStack in array)
				{
					num += concurrentStack.Count;
				}
				return num;
			}
		}

		public long Int64_6
		{
			[CompilerGenerated]
			get
			{
				return long_4;
			}
			[CompilerGenerated]
			set
			{
				long_4 = value;
			}
		}

		public long Int64_7
		{
			[CompilerGenerated]
			get
			{
				return long_5;
			}
			[CompilerGenerated]
			set
			{
				long_5 = value;
			}
		}

		public long Int64_8
		{
			[CompilerGenerated]
			get
			{
				return long_6;
			}
			[CompilerGenerated]
			set
			{
				long_6 = value;
			}
		}

		public bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_0;
			}
			[CompilerGenerated]
			set
			{
				bool_0 = value;
			}
		}

		public bool Boolean_1
		{
			[CompilerGenerated]
			get
			{
				return bool_1;
			}
			[CompilerGenerated]
			set
			{
				bool_1 = value;
			}
		}

		public event EventHandler BlockCreated
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_0 = (EventHandler)Delegate.Combine(eventHandler_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_0 = (EventHandler)Delegate.Remove(eventHandler_0, value);
			}
		}

		public event EventHandler BlockDiscarded
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_1 = (EventHandler)Delegate.Combine(eventHandler_1, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_1 = (EventHandler)Delegate.Remove(eventHandler_1, value);
			}
		}

		public event EventHandler LargeBufferCreated
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_2 = (EventHandler)Delegate.Combine(eventHandler_2, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_2 = (EventHandler)Delegate.Remove(eventHandler_2, value);
			}
		}

		public event EventHandler StreamCreated
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_3 = (EventHandler)Delegate.Combine(eventHandler_3, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_3 = (EventHandler)Delegate.Remove(eventHandler_3, value);
			}
		}

		public event EventHandler StreamDisposed
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_4 = (EventHandler)Delegate.Combine(eventHandler_4, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_4 = (EventHandler)Delegate.Remove(eventHandler_4, value);
			}
		}

		public event EventHandler StreamFinalized
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_5 = (EventHandler)Delegate.Combine(eventHandler_5, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_5 = (EventHandler)Delegate.Remove(eventHandler_5, value);
			}
		}

		public event StreamLengthReportHandler StreamLength
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				streamLengthReportHandler_0 = (StreamLengthReportHandler)Delegate.Combine(streamLengthReportHandler_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				streamLengthReportHandler_0 = (StreamLengthReportHandler)Delegate.Remove(streamLengthReportHandler_0, value);
			}
		}

		public event EventHandler StreamConvertedToArray
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_6 = (EventHandler)Delegate.Combine(eventHandler_6, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_6 = (EventHandler)Delegate.Remove(eventHandler_6, value);
			}
		}

		public event LargeBufferDiscardedEventHandler LargeBufferDiscarded
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				largeBufferDiscardedEventHandler_0 = (LargeBufferDiscardedEventHandler)Delegate.Combine(largeBufferDiscardedEventHandler_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				largeBufferDiscardedEventHandler_0 = (LargeBufferDiscardedEventHandler)Delegate.Remove(largeBufferDiscardedEventHandler_0, value);
			}
		}

		public event UsageReportEventHandler UsageReport
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				usageReportEventHandler_0 = (UsageReportEventHandler)Delegate.Combine(usageReportEventHandler_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				usageReportEventHandler_0 = (UsageReportEventHandler)Delegate.Remove(usageReportEventHandler_0, value);
			}
		}

		public RecyclableMemoryStreamManager()
			: this(131072, 1048576, 134217728)
		{
		}

		public RecyclableMemoryStreamManager(int int_6, int int_7, int int_8)
		{
			if (int_6 <= 0)
			{
				throw new ArgumentOutOfRangeException("blockSize", int_6, "blockSize must be a positive number");
			}
			if (int_7 <= 0)
			{
				throw new ArgumentOutOfRangeException("largeBufferMultiple", "largeBufferMultiple must be a positive number");
			}
			if (int_8 < int_6)
			{
				throw new ArgumentOutOfRangeException("maximumBufferSize", "maximumBufferSize must be at least blockSize");
			}
			int_3 = int_6;
			int_4 = int_7;
			int_5 = int_8;
			if (!IsLargeBufferMultiple(int_8))
			{
				throw new ArgumentException("maximumBufferSize is not a multiple of largeBufferMultiple", "maximumBufferSize");
			}
			concurrentStack_1 = new ConcurrentStack<byte[]>();
			int num = int_8 / int_7;
			long_1 = new long[num + 1];
			long_0 = new long[num];
			concurrentStack_0 = new ConcurrentStack<byte[]>[num];
			for (int i = 0; i < concurrentStack_0.Length; i++)
			{
				concurrentStack_0[i] = new ConcurrentStack<byte[]>();
			}
			Events.events_0.MemoryStreamManagerInitialized(int_6, int_7, int_8);
		}

		internal byte[] GetBlock()
		{
			byte[] gparam_;
			if (!concurrentStack_1.TryPop(out gparam_))
			{
				gparam_ = new byte[Int32_0];
				Events.events_0.MemoryStreamNewBlockCreated(long_3);
				if (eventHandler_0 != null)
				{
					eventHandler_0();
				}
			}
			else
			{
				Interlocked.Add(ref long_2, -Int32_0);
			}
			Interlocked.Add(ref long_3, Int32_0);
			return gparam_;
		}

		internal byte[] GetLargeBuffer(int int_6, string string_0)
		{
			int_6 = RoundToLargeBufferMultiple(int_6);
			int num = int_6 / int_4 - 1;
			byte[] gparam_;
			if (num < concurrentStack_0.Length)
			{
				if (!concurrentStack_0[num].TryPop(out gparam_))
				{
					gparam_ = new byte[int_6];
					Events.events_0.MemoryStreamNewLargeBufferCreated(int_6, Int64_3);
					if (eventHandler_2 != null)
					{
						eventHandler_2();
					}
				}
				else
				{
					Interlocked.Add(ref long_0[num], -gparam_.Length);
				}
			}
			else
			{
				num = long_1.Length - 1;
				gparam_ = new byte[int_6];
				string string_ = null;
				if (Boolean_0)
				{
					string_ = Environment.StackTrace;
				}
				Events.events_0.MemoryStreamNonPooledLargeBufferCreated(int_6, string_0, string_);
				if (eventHandler_2 != null)
				{
					eventHandler_2();
				}
			}
			Interlocked.Add(ref long_1[num], gparam_.Length);
			return gparam_;
		}

		private int RoundToLargeBufferMultiple(int int_6)
		{
			return (int_6 + Int32_1 - 1) / Int32_1 * Int32_1;
		}

		private bool IsLargeBufferMultiple(int int_6)
		{
			return int_6 != 0 && int_6 % Int32_1 == 0;
		}

		internal void ReturnLargeBuffer(byte[] byte_0, string string_0)
		{
			if (byte_0 == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (!IsLargeBufferMultiple(byte_0.Length))
			{
				throw new ArgumentException("buffer did not originate from this memory manager. The size is not a multiple of " + Int32_1);
			}
			int num = byte_0.Length / int_4 - 1;
			if (num < concurrentStack_0.Length)
			{
				if ((concurrentStack_0[num].Count + 1) * byte_0.Length > Int64_7 && Int64_7 != 0L)
				{
					Events.events_0.MemoryStreamDiscardBuffer(Events.MemoryStreamBufferType.Large, string_0, Events.MemoryStreamDiscardReason.EnoughFree);
					if (largeBufferDiscardedEventHandler_0 != null)
					{
						largeBufferDiscardedEventHandler_0(Events.MemoryStreamDiscardReason.EnoughFree);
					}
				}
				else
				{
					concurrentStack_0[num].Push(byte_0);
					Interlocked.Add(ref long_0[num], byte_0.Length);
				}
			}
			else
			{
				num = long_1.Length - 1;
				Events.events_0.MemoryStreamDiscardBuffer(Events.MemoryStreamBufferType.Large, string_0, Events.MemoryStreamDiscardReason.TooLarge);
				if (largeBufferDiscardedEventHandler_0 != null)
				{
					largeBufferDiscardedEventHandler_0(Events.MemoryStreamDiscardReason.TooLarge);
				}
			}
			Interlocked.Add(ref long_1[num], -byte_0.Length);
			if (usageReportEventHandler_0 != null)
			{
				usageReportEventHandler_0(long_3, long_2, Int64_3, Int64_2);
			}
		}

		internal void ReturnBlocks(ICollection<byte[]> icollection_0, string string_0)
		{
			if (icollection_0 == null)
			{
				throw new ArgumentNullException("blocks");
			}
			int num = icollection_0.Count * Int32_0;
			Interlocked.Add(ref long_3, -num);
			foreach (byte[] item in icollection_0)
			{
				if (item == null || item.Length != Int32_0)
				{
					throw new ArgumentException("blocks contains buffers that are not BlockSize in length");
				}
			}
			foreach (byte[] item2 in icollection_0)
			{
				if (Int64_6 == 0L || Int64_0 < Int64_6)
				{
					Interlocked.Add(ref long_2, Int32_0);
					concurrentStack_1.Push(item2);
					continue;
				}
				Events.events_0.MemoryStreamDiscardBuffer(Events.MemoryStreamBufferType.Small, string_0, Events.MemoryStreamDiscardReason.EnoughFree);
				if (eventHandler_1 != null)
				{
					eventHandler_1();
				}
				break;
			}
			if (usageReportEventHandler_0 != null)
			{
				usageReportEventHandler_0(long_3, long_2, Int64_3, Int64_2);
			}
		}

		internal void ReportStreamCreated()
		{
			if (eventHandler_3 != null)
			{
				eventHandler_3();
			}
		}

		internal void ReportStreamDisposed()
		{
			if (eventHandler_4 != null)
			{
				eventHandler_4();
			}
		}

		internal void ReportStreamFinalized()
		{
			if (eventHandler_5 != null)
			{
				eventHandler_5();
			}
		}

		internal void ReportStreamLength(long long_7)
		{
			if (streamLengthReportHandler_0 != null)
			{
				streamLengthReportHandler_0(long_7);
			}
		}

		internal void ReportStreamToArray()
		{
			if (eventHandler_6 != null)
			{
				eventHandler_6();
			}
		}

		public MemoryStream GetStream()
		{
			return new RecyclableMemoryStream(this);
		}

		public MemoryStream GetStream(string string_0)
		{
			return new RecyclableMemoryStream(this, string_0);
		}

		public MemoryStream GetStream(string string_0, int int_6)
		{
			return new RecyclableMemoryStream(this, string_0, int_6);
		}

		public MemoryStream GetStream(string string_0, int int_6, bool bool_2)
		{
			if (bool_2 && int_6 > Int32_0)
			{
				return new RecyclableMemoryStream(this, string_0, int_6, GetLargeBuffer(int_6, string_0));
			}
			return GetStream(string_0, int_6);
		}

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public MemoryStream GetStream(string string_0, byte[] byte_0, int int_6, int int_7)
		{
			RecyclableMemoryStream recyclableMemoryStream = new RecyclableMemoryStream(this, string_0, int_7);
			recyclableMemoryStream.Write(byte_0, int_6, int_7);
			recyclableMemoryStream.Position = 0L;
			return recyclableMemoryStream;
		}
	}
}
