using System;
using System.Text;

namespace WebSocketSharp
{
	public class CloseEventArgs : EventArgs
	{
		private bool bool_0;

		private ushort ushort_0;

		private PayloadData payloadData_0;

		private byte[] byte_0;

		private string string_0;

		internal PayloadData PayloadData_0
		{
			get
			{
				return payloadData_0 ?? (payloadData_0 = new PayloadData(byte_0));
			}
		}

		internal byte[] Byte_0
		{
			get
			{
				return byte_0;
			}
		}

		public ushort UInt16_0
		{
			get
			{
				return ushort_0;
			}
		}

		public string String_0
		{
			get
			{
				return string_0 ?? string.Empty;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return bool_0;
			}
			internal set
			{
				bool_0 = value;
			}
		}

		internal CloseEventArgs()
		{
			ushort_0 = 1005;
			payloadData_0 = new PayloadData();
			byte_0 = payloadData_0.Byte_0;
		}

		internal CloseEventArgs(ushort ushort_1)
		{
			ushort_0 = ushort_1;
			byte_0 = ushort_1.InternalToByteArray(ByteOrder.Big);
		}

		internal CloseEventArgs(CloseStatusCode closeStatusCode_0)
			: this((ushort)closeStatusCode_0)
		{
		}

		internal CloseEventArgs(PayloadData payloadData_1)
		{
			payloadData_0 = payloadData_1;
			byte_0 = payloadData_1.Byte_0;
			int num = byte_0.Length;
			ushort_0 = (ushort)((num <= 1) ? 1005 : byte_0.SubArray(0, 2).ToUInt16(ByteOrder.Big));
			string_0 = ((num <= 2) ? string.Empty : Encoding.UTF8.GetString(byte_0.SubArray(2, num - 2)));
		}

		internal CloseEventArgs(ushort ushort_1, string string_1)
		{
			ushort_0 = ushort_1;
			string_0 = string_1;
			byte_0 = ushort_1.Append(string_1);
		}

		internal CloseEventArgs(CloseStatusCode closeStatusCode_0, string string_1)
			: this((ushort)closeStatusCode_0, string_1)
		{
		}
	}
}
