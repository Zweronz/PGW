using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using BestHTTP.Extensions;

namespace BestHTTP.WebSocket.Frames
{
	public sealed class WebSocketFrameReader
	{
		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private WebSocketFrameTypes webSocketFrameTypes_0;

		[CompilerGenerated]
		private bool bool_1;

		[CompilerGenerated]
		private ulong ulong_0;

		[CompilerGenerated]
		private byte[] byte_0;

		[CompilerGenerated]
		private byte[] byte_1;

		public bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_0;
			}
			[CompilerGenerated]
			private set
			{
				bool_0 = value;
			}
		}

		public WebSocketFrameTypes WebSocketFrameTypes_0
		{
			[CompilerGenerated]
			get
			{
				return webSocketFrameTypes_0;
			}
			[CompilerGenerated]
			private set
			{
				webSocketFrameTypes_0 = value;
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
			private set
			{
				bool_1 = value;
			}
		}

		public ulong UInt64_0
		{
			[CompilerGenerated]
			get
			{
				return ulong_0;
			}
			[CompilerGenerated]
			private set
			{
				ulong_0 = value;
			}
		}

		public byte[] Byte_0
		{
			[CompilerGenerated]
			get
			{
				return byte_0;
			}
			[CompilerGenerated]
			private set
			{
				byte_0 = value;
			}
		}

		public byte[] Byte_1
		{
			[CompilerGenerated]
			get
			{
				return byte_1;
			}
			[CompilerGenerated]
			private set
			{
				byte_1 = value;
			}
		}

		internal void Read(Stream stream_0)
		{
			byte b = (byte)stream_0.ReadByte();
			Boolean_0 = (b & 0x80) != 0;
			WebSocketFrameTypes_0 = (WebSocketFrameTypes)(b & 0xFu);
			b = (byte)stream_0.ReadByte();
			Boolean_1 = (b & 0x80) != 0;
			UInt64_0 = (ulong)(int)(b & 0x7Fu);
			if (UInt64_0 == 126L)
			{
				byte[] array = new byte[2];
				stream_0.ReadBuffer(array);
				if (BitConverter.IsLittleEndian)
				{
					Array.Reverse(array, 0, array.Length);
				}
				UInt64_0 = BitConverter.ToUInt16(array, 0);
			}
			else if (UInt64_0 == 127L)
			{
				byte[] array2 = new byte[8];
				stream_0.ReadBuffer(array2);
				if (BitConverter.IsLittleEndian)
				{
					Array.Reverse(array2, 0, array2.Length);
				}
				UInt64_0 = BitConverter.ToUInt64(array2, 0);
			}
			if (Boolean_1)
			{
				Byte_0 = new byte[4];
				stream_0.Read(Byte_0, 0, 4);
			}
			Byte_1 = new byte[UInt64_0];
			if (UInt64_0 == 0L)
			{
				return;
			}
			int num = 0;
			do
			{
				num += stream_0.Read(Byte_1, num, Byte_1.Length - num);
			}
			while (num < Byte_1.Length);
			if (Boolean_1)
			{
				for (int i = 0; i < Byte_1.Length; i++)
				{
					Byte_1[i] ^= Byte_0[i % 4];
				}
			}
		}

		internal void Assemble(List<WebSocketFrameReader> list_0)
		{
			list_0.Add(this);
			ulong num = 0uL;
			for (int i = 0; i < list_0.Count; i++)
			{
				num += list_0[i].UInt64_0;
			}
			byte[] destinationArray = new byte[num];
			ulong num2 = 0uL;
			for (int j = 0; j < list_0.Count; j++)
			{
				Array.Copy(list_0[j].Byte_1, 0, destinationArray, (int)num2, (int)list_0[j].UInt64_0);
				num2 += list_0[j].UInt64_0;
			}
			WebSocketFrameTypes_0 = list_0[0].WebSocketFrameTypes_0;
			UInt64_0 = num;
			Byte_1 = destinationArray;
		}
	}
}
