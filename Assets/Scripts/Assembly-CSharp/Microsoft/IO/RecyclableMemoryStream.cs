using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Microsoft.IO
{
	public sealed class RecyclableMemoryStream : MemoryStream
	{
		private const long long_0 = 2147483647L;

		private readonly List<byte[]> list_0 = new List<byte[]>(1);

		private byte[] byte_0;

		private List<byte[]> list_1;

		private readonly Guid guid_0;

		private readonly string string_0;

		private readonly RecyclableMemoryStreamManager recyclableMemoryStreamManager_0;

		private bool bool_0;

		private readonly string string_1;

		private string string_2;

		private readonly byte[] byte_1 = new byte[1];

		private int int_0;

		private int int_1;

		internal Guid Guid_0
		{
			get
			{
				CheckDisposed();
				return guid_0;
			}
		}

		internal string String_0
		{
			get
			{
				CheckDisposed();
				return string_0;
			}
		}

		internal RecyclableMemoryStreamManager RecyclableMemoryStreamManager_0
		{
			get
			{
				CheckDisposed();
				return recyclableMemoryStreamManager_0;
			}
		}

		internal string String_1
		{
			get
			{
				return string_1;
			}
		}

		internal string String_2
		{
			get
			{
				return string_2;
			}
		}

		public override int Capacity
		{
			get
			{
				CheckDisposed();
				if (byte_0 != null)
				{
					return byte_0.Length;
				}
				if (list_0.Count > 0)
				{
					return list_0.Count * recyclableMemoryStreamManager_0.Int32_0;
				}
				return 0;
			}
			set
			{
				EnsureCapacity(value);
			}
		}

		public override long Length
		{
			get
			{
				CheckDisposed();
				return int_0;
			}
		}

		public override long Position
		{
			get
			{
				CheckDisposed();
				return int_1;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "value must be non-negative");
				}
				if (value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", "value cannot be more than " + 2147483647L);
				}
				int_1 = (int)value;
			}
		}

		public override bool CanRead
		{
			get
			{
				return !bool_0;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return !bool_0;
			}
		}

		public override bool CanTimeout
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

		public RecyclableMemoryStream(RecyclableMemoryStreamManager recyclableMemoryStreamManager_1)
			: this(recyclableMemoryStreamManager_1, null)
		{
		}

		public RecyclableMemoryStream(RecyclableMemoryStreamManager recyclableMemoryStreamManager_1, string string_3)
			: this(recyclableMemoryStreamManager_1, string_3, 0)
		{
		}

		public RecyclableMemoryStream(RecyclableMemoryStreamManager recyclableMemoryStreamManager_1, string string_3, int int_2)
			: this(recyclableMemoryStreamManager_1, string_3, int_2, null)
		{
		}

		internal RecyclableMemoryStream(RecyclableMemoryStreamManager recyclableMemoryStreamManager_1, string string_3, int int_2, byte[] byte_2)
		{
			recyclableMemoryStreamManager_0 = recyclableMemoryStreamManager_1;
			guid_0 = Guid.NewGuid();
			string_0 = string_3;
			if (int_2 < recyclableMemoryStreamManager_1.Int32_0)
			{
				int_2 = recyclableMemoryStreamManager_1.Int32_0;
			}
			if (byte_2 == null)
			{
				EnsureCapacity(int_2);
			}
			else
			{
				byte_0 = byte_2;
			}
			bool_0 = false;
			if (recyclableMemoryStreamManager_0.Boolean_0)
			{
				string_1 = Environment.StackTrace;
			}
			RecyclableMemoryStreamManager.Events.events_0.MemoryStreamCreated(guid_0, string_0, int_2);
			recyclableMemoryStreamManager_0.ReportStreamCreated();
		}

		~RecyclableMemoryStream()
		{
			Dispose(false);
		}

		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "We have different disposal semantics, so SuppressFinalize is in a different spot.")]
		protected override void Dispose(bool disposing)
		{
			if (bool_0)
			{
				string string_ = null;
				if (recyclableMemoryStreamManager_0.Boolean_0)
				{
					string_ = Environment.StackTrace;
				}
				RecyclableMemoryStreamManager.Events.events_0.MemoryStreamDoubleDispose(guid_0, string_0, string_1, string_2, string_);
				throw new InvalidOperationException("Cannot dispose of RecyclableMemoryStream twice");
			}
			RecyclableMemoryStreamManager.Events.events_0.MemoryStreamDisposed(guid_0, string_0);
			if (recyclableMemoryStreamManager_0.Boolean_0)
			{
				string_2 = Environment.StackTrace;
			}
			if (disposing)
			{
				bool_0 = true;
				recyclableMemoryStreamManager_0.ReportStreamDisposed();
				GC.SuppressFinalize(this);
			}
			else
			{
				RecyclableMemoryStreamManager.Events.events_0.MemoryStreamFinalized(guid_0, string_0, string_1);
				if (AppDomain.CurrentDomain.IsFinalizingForUnload())
				{
					base.Dispose(disposing);
					return;
				}
				recyclableMemoryStreamManager_0.ReportStreamFinalized();
			}
			recyclableMemoryStreamManager_0.ReportStreamLength(int_0);
			if (byte_0 != null)
			{
				recyclableMemoryStreamManager_0.ReturnLargeBuffer(byte_0, string_0);
			}
			if (list_1 != null)
			{
				foreach (byte[] item in list_1)
				{
					recyclableMemoryStreamManager_0.ReturnLargeBuffer(item, string_0);
				}
			}
			recyclableMemoryStreamManager_0.ReturnBlocks(list_0, string_0);
			base.Dispose(disposing);
		}

		public override void Close()
		{
			Dispose(true);
		}

		public override byte[] GetBuffer()
		{
			CheckDisposed();
			if (byte_0 != null)
			{
				return byte_0;
			}
			if (list_0.Count == 1)
			{
				return list_0[0];
			}
			byte[] largeBuffer = RecyclableMemoryStreamManager_0.GetLargeBuffer(Capacity, string_0);
			InternalRead(largeBuffer, 0, int_0, 0);
			byte_0 = largeBuffer;
			if (list_0.Count > 0 && recyclableMemoryStreamManager_0.Boolean_1)
			{
				recyclableMemoryStreamManager_0.ReturnBlocks(list_0, string_0);
				list_0.Clear();
			}
			return byte_0;
		}

		public override byte[] ToArray()
		{
			CheckDisposed();
			byte[] array = new byte[Length];
			InternalRead(array, 0, int_0, 0);
			string text = ((!recyclableMemoryStreamManager_0.Boolean_0) ? null : Environment.StackTrace);
			RecyclableMemoryStreamManager.Events.events_0.MemoryStreamToArray(guid_0, string_0, text, 0);
			recyclableMemoryStreamManager_0.ReportStreamToArray();
			return array;
		}

		public override int Read(byte[] b, int off, int len)
		{
			CheckDisposed();
			if (b == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (off < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "offset cannot be negative");
			}
			if (len < 0)
			{
				throw new ArgumentOutOfRangeException("count", "count cannot be negative");
			}
			if (off + len > b.Length)
			{
				throw new ArgumentException("buffer length must be at least offset + count");
			}
			int num = InternalRead(b, off, len, int_1);
			int_1 += num;
			return num;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "Offset must be in the range of 0 - buffer.Length-1");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "count must be non-negative");
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentException("count must be greater than buffer.Length - offset");
			}
			if (Position + count > 2147483647L)
			{
				throw new IOException("Maximum capacity exceeded");
			}
			int num = (int)Position + count;
			int int32_ = recyclableMemoryStreamManager_0.Int32_0;
			int num2 = (num + int32_ - 1) / int32_;
			if (num2 * int32_ > 2147483647L)
			{
				throw new IOException("Maximum capacity exceeded");
			}
			EnsureCapacity(num);
			if (byte_0 == null)
			{
				int num3 = count;
				int num4 = 0;
				int num5 = OffsetToBlockIndex(int_1);
				int num6 = OffsetToBlockOffset(int_1);
				while (num3 > 0)
				{
					byte[] dst = list_0[num5];
					int val = int32_ - num6;
					int num7 = Math.Min(val, num3);
					Buffer.BlockCopy(buffer, offset + num4, dst, num6, num7);
					num3 -= num7;
					num4 += num7;
					num5++;
					num6 = 0;
				}
			}
			else
			{
				Buffer.BlockCopy(buffer, offset, byte_0, int_1, count);
			}
			Position = num;
			int_0 = Math.Max(int_1, int_0);
		}

		public override string ToString()
		{
			return string.Format("Id = {0}, Tag = {1}, Length = {2:N0} bytes", Guid_0, String_0, Length);
		}

		public override void WriteByte(byte b)
		{
			CheckDisposed();
			byte_1[0] = b;
			Write(byte_1, 0, 1);
		}

		public override int ReadByte()
		{
			CheckDisposed();
			if (int_1 == int_0)
			{
				return -1;
			}
			byte b = 0;
			if (byte_0 == null)
			{
				int index = OffsetToBlockIndex(int_1);
				int num = OffsetToBlockOffset(int_1);
				b = list_0[index][num];
			}
			else
			{
				b = byte_0[int_1];
			}
			int_1++;
			return b;
		}

		public override void SetLength(long value)
		{
			CheckDisposed();
			if (value >= 0L && value <= 2147483647L)
			{
				EnsureCapacity((int)value);
				int_0 = (int)value;
				if (int_1 > value)
				{
					int_1 = (int)value;
				}
				return;
			}
			throw new ArgumentOutOfRangeException("value", "value must be non-negative and at most " + 2147483647L);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			CheckDisposed();
			if (offset > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("offset", "offset cannot be larger than " + 2147483647L);
			}
			int num;
			switch (origin)
			{
			default:
				throw new ArgumentException("Invalid seek origin", "loc");
			case SeekOrigin.Begin:
				num = (int)offset;
				break;
			case SeekOrigin.Current:
				num = (int)offset + int_1;
				break;
			case SeekOrigin.End:
				num = (int)offset + int_0;
				break;
			}
			if (num < 0)
			{
				throw new IOException("Seek before beginning");
			}
			int_1 = num;
			return int_1;
		}

		public override void WriteTo(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (byte_0 == null)
			{
				int num = 0;
				int num2 = int_0;
				while (num2 > 0)
				{
					int num3 = Math.Min(list_0[num].Length, num2);
					stream.Write(list_0[num], 0, num3);
					num2 -= num3;
					num++;
				}
			}
			else
			{
				stream.Write(byte_0, 0, int_0);
			}
		}

		private void CheckDisposed()
		{
			if (bool_0)
			{
				throw new ObjectDisposedException(string.Format("The stream with Id {0} and Tag {1} is disposed.", guid_0, string_0));
			}
		}

		private int InternalRead(byte[] byte_2, int int_2, int int_3, int int_4)
		{
			if (int_0 - int_4 <= 0)
			{
				return 0;
			}
			if (byte_0 == null)
			{
				int num = OffsetToBlockIndex(int_4);
				int num2 = 0;
				int num3 = Math.Min(int_3, int_0 - int_4);
				int num4 = OffsetToBlockOffset(int_4);
				while (num3 > 0)
				{
					int num5 = Math.Min(list_0[num].Length - num4, num3);
					Buffer.BlockCopy(list_0[num], num4, byte_2, num2 + int_2, num5);
					num2 += num5;
					num3 -= num5;
					num++;
					num4 = 0;
				}
				return num2;
			}
			int num6 = Math.Min(int_3, int_0 - int_4);
			Buffer.BlockCopy(byte_0, int_4, byte_2, int_2, num6);
			return num6;
		}

		private int OffsetToBlockIndex(int int_2)
		{
			return int_2 / recyclableMemoryStreamManager_0.Int32_0;
		}

		private int OffsetToBlockOffset(int int_2)
		{
			return int_2 % recyclableMemoryStreamManager_0.Int32_0;
		}

		private void EnsureCapacity(int int_2)
		{
			if (int_2 > recyclableMemoryStreamManager_0.Int64_8 && recyclableMemoryStreamManager_0.Int64_8 > 0L)
			{
				RecyclableMemoryStreamManager.Events.events_0.MemoryStreamOverCapacity(int_2, recyclableMemoryStreamManager_0.Int64_8, string_0, string_1);
				throw new InvalidOperationException("Requested capacity is too large: " + int_2 + ". Limit is " + recyclableMemoryStreamManager_0.Int64_8);
			}
			if (byte_0 != null)
			{
				if (int_2 > byte_0.Length)
				{
					byte[] largeBuffer = recyclableMemoryStreamManager_0.GetLargeBuffer(int_2, string_0);
					InternalRead(largeBuffer, 0, int_0, 0);
					ReleaseLargeBuffer();
					byte_0 = largeBuffer;
				}
			}
			else
			{
				while (Capacity < int_2)
				{
					list_0.Add(RecyclableMemoryStreamManager_0.GetBlock());
				}
			}
		}

		private void ReleaseLargeBuffer()
		{
			if (recyclableMemoryStreamManager_0.Boolean_1)
			{
				recyclableMemoryStreamManager_0.ReturnLargeBuffer(byte_0, string_0);
			}
			else
			{
				if (list_1 == null)
				{
					list_1 = new List<byte[]>(1);
				}
				list_1.Add(byte_0);
			}
			byte_0 = null;
		}
	}
}
