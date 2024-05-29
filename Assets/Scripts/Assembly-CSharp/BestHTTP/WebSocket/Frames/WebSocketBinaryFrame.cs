using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace BestHTTP.WebSocket.Frames
{
	public class WebSocketBinaryFrame : IWebSocketFrameWriter
	{
		private static readonly byte[] byte_0 = new byte[0];

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private byte[] byte_1;

		[CompilerGenerated]
		private ulong ulong_0;

		[CompilerGenerated]
		private ulong ulong_1;

		public virtual WebSocketFrameTypes WebSocketFrameTypes_0
		{
			get
			{
				return WebSocketFrameTypes.Binary;
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
			protected set
			{
				bool_0 = value;
			}
		}

		protected byte[] Byte_0
		{
			[CompilerGenerated]
			get
			{
				return byte_1;
			}
			[CompilerGenerated]
			set
			{
				byte_1 = value;
			}
		}

		protected ulong UInt64_0
		{
			[CompilerGenerated]
			get
			{
				return ulong_0;
			}
			[CompilerGenerated]
			set
			{
				ulong_0 = value;
			}
		}

		protected ulong UInt64_1
		{
			[CompilerGenerated]
			get
			{
				return ulong_1;
			}
			[CompilerGenerated]
			set
			{
				ulong_1 = value;
			}
		}

		public WebSocketBinaryFrame(byte[] byte_2)
			: this(byte_2, 0uL, (byte_2 == null) ? 0uL : ((ulong)byte_2.Length), true)
		{
		}

		public WebSocketBinaryFrame(byte[] byte_2, bool bool_1)
			: this(byte_2, 0uL, (byte_2 == null) ? 0uL : ((ulong)byte_2.Length), bool_1)
		{
		}

		public WebSocketBinaryFrame(byte[] byte_2, ulong ulong_2, ulong ulong_3, bool bool_1)
		{
			Byte_0 = byte_2;
			UInt64_0 = ulong_2;
			UInt64_1 = ulong_3;
			Boolean_0 = bool_1;
		}

		public virtual byte[] Get()
		{
			if (Byte_0 == null)
			{
				Byte_0 = byte_0;
			}
			using (MemoryStream memoryStream = new MemoryStream((int)UInt64_1 + 9))
			{
				byte b = (byte)(Boolean_0 ? 128u : 0u);
				memoryStream.WriteByte((byte)((uint)b | (uint)WebSocketFrameTypes_0));
				if (UInt64_1 < 126L)
				{
					memoryStream.WriteByte((byte)(0x80u | (byte)UInt64_1));
				}
				else if (UInt64_1 < 65535L)
				{
					memoryStream.WriteByte(254);
					byte[] bytes = BitConverter.GetBytes((ushort)UInt64_1);
					if (BitConverter.IsLittleEndian)
					{
						Array.Reverse(bytes, 0, bytes.Length);
					}
					memoryStream.Write(bytes, 0, bytes.Length);
				}
				else
				{
					memoryStream.WriteByte(byte.MaxValue);
					byte[] bytes2 = BitConverter.GetBytes(UInt64_1);
					if (BitConverter.IsLittleEndian)
					{
						Array.Reverse(bytes2, 0, bytes2.Length);
					}
					memoryStream.Write(bytes2, 0, bytes2.Length);
				}
				byte[] bytes3 = BitConverter.GetBytes(GetHashCode());
				memoryStream.Write(bytes3, 0, bytes3.Length);
				for (ulong num = UInt64_0; num < UInt64_0 + UInt64_1; num++)
				{
					memoryStream.WriteByte((byte)(Byte_0[num] ^ bytes3[(num - UInt64_0) % 4L]));
				}
				return memoryStream.ToArray();
			}
		}
	}
}
