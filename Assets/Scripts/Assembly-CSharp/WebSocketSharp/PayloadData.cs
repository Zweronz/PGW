using System;
using System.Collections;
using System.Collections.Generic;

namespace WebSocketSharp
{
	internal class PayloadData : IEnumerable, IEnumerable<byte>
	{
		public const ulong ulong_0 = 9223372036854775807uL;

		private byte[] byte_0;

		private long long_0;

		private long long_1;

		private bool bool_0;

		internal long Int64_0
		{
			get
			{
				return long_0;
			}
			set
			{
				long_0 = value;
			}
		}

		internal bool Boolean_0
		{
			get
			{
				return long_1 > 1L && byte_0.SubArray(0, 2).ToUInt16(ByteOrder.Big).IsReserved();
			}
		}

		public byte[] Byte_0
		{
			get
			{
				return (long_0 <= 0L) ? byte_0 : byte_0.SubArray(long_0, long_1 - long_0);
			}
		}

		public byte[] Byte_1
		{
			get
			{
				return (long_0 <= 0L) ? Ext.byte_1 : byte_0.SubArray(0L, long_0);
			}
		}

		public bool Boolean_1
		{
			get
			{
				return bool_0;
			}
		}

		public ulong UInt64_0
		{
			get
			{
				return (ulong)long_1;
			}
		}

		internal PayloadData()
		{
			byte_0 = Ext.byte_1;
		}

		internal PayloadData(byte[] byte_1)
			: this(byte_1, false)
		{
		}

		internal PayloadData(byte[] byte_1, bool bool_1)
		{
			byte_0 = byte_1;
			bool_0 = bool_1;
			long_1 = byte_1.LongLength;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		internal void Mask(byte[] byte_1)
		{
			for (long num = 0L; num < long_1; num++)
			{
				byte_0[num] ^= byte_1[num % 4L];
			}
			bool_0 = !bool_0;
		}

		public IEnumerator<byte> GetEnumerator()
		{
			byte[] array = byte_0;
			for (int i = 0; i < array.Length; i++)
			{
				yield return array[i];
			}
		}

		public byte[] ToByteArray()
		{
			return byte_0;
		}

		public override string ToString()
		{
			return BitConverter.ToString(byte_0);
		}
	}
}
