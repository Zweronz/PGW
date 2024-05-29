using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WebSocketSharp
{
	internal class WebSocketFrame : IEnumerable, IEnumerable<byte>
	{
		private byte[] byte_0;

		private Fin fin_0;

		private Mask mask_0;

		private byte[] byte_1;

		private Opcode opcode_0;

		private PayloadData payloadData_0;

		private byte byte_2;

		private Rsv rsv_0;

		private Rsv rsv_1;

		private Rsv rsv_2;

		internal static readonly byte[] byte_3;

		public byte[] Byte_0
		{
			get
			{
				return byte_0;
			}
		}

		public Fin Fin_0
		{
			get
			{
				return fin_0;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return opcode_0 == Opcode.Binary;
			}
		}

		public bool Boolean_1
		{
			get
			{
				return opcode_0 == Opcode.Close;
			}
		}

		public bool Boolean_2
		{
			get
			{
				return rsv_0 == Rsv.On;
			}
		}

		public bool Boolean_3
		{
			get
			{
				return opcode_0 == Opcode.Cont;
			}
		}

		public bool Boolean_4
		{
			get
			{
				return opcode_0 == Opcode.Close || opcode_0 == Opcode.Ping || opcode_0 == Opcode.Pong;
			}
		}

		public bool Boolean_5
		{
			get
			{
				return opcode_0 == Opcode.Binary || opcode_0 == Opcode.Text;
			}
		}

		public bool Boolean_6
		{
			get
			{
				return fin_0 == Fin.Final;
			}
		}

		public bool Boolean_7
		{
			get
			{
				return fin_0 == Fin.More || opcode_0 == Opcode.Cont;
			}
		}

		public bool Boolean_8
		{
			get
			{
				return mask_0 == Mask.Mask;
			}
		}

		public bool Boolean_9
		{
			get
			{
				return (opcode_0 == Opcode.Binary || opcode_0 == Opcode.Text) && rsv_0 == Rsv.On;
			}
		}

		public bool Boolean_10
		{
			get
			{
				return opcode_0 == Opcode.Ping;
			}
		}

		public bool Boolean_11
		{
			get
			{
				return opcode_0 == Opcode.Pong;
			}
		}

		public bool Boolean_12
		{
			get
			{
				return opcode_0 == Opcode.Text;
			}
		}

		public ulong UInt64_0
		{
			get
			{
				return (ulong)(2L + (byte_0.Length + byte_1.Length)) + payloadData_0.UInt64_0;
			}
		}

		public Mask Mask_0
		{
			get
			{
				return mask_0;
			}
		}

		public byte[] Byte_1
		{
			get
			{
				return byte_1;
			}
		}

		public Opcode Opcode_0
		{
			get
			{
				return opcode_0;
			}
		}

		public PayloadData PayloadData_0
		{
			get
			{
				return payloadData_0;
			}
		}

		public byte Byte_2
		{
			get
			{
				return byte_2;
			}
		}

		public Rsv Rsv_0
		{
			get
			{
				return rsv_0;
			}
		}

		public Rsv Rsv_1
		{
			get
			{
				return rsv_1;
			}
		}

		public Rsv Rsv_2
		{
			get
			{
				return rsv_2;
			}
		}

		private WebSocketFrame()
		{
		}

		internal WebSocketFrame(Opcode opcode_1, PayloadData payloadData_1, bool bool_0)
			: this(Fin.Final, opcode_1, payloadData_1, false, bool_0)
		{
		}

		internal WebSocketFrame(Fin fin_1, Opcode opcode_1, byte[] byte_4, bool bool_0, bool bool_1)
			: this(fin_1, opcode_1, new PayloadData(byte_4), bool_0, bool_1)
		{
		}

		internal WebSocketFrame(Fin fin_1, Opcode opcode_1, PayloadData payloadData_1, bool bool_0, bool bool_1)
		{
			fin_0 = fin_1;
			rsv_0 = ((isData(opcode_1) && bool_0) ? Rsv.On : Rsv.Off);
			rsv_1 = Rsv.Off;
			rsv_2 = Rsv.Off;
			opcode_0 = opcode_1;
			ulong uInt64_ = payloadData_1.UInt64_0;
			if (uInt64_ < 126L)
			{
				byte_2 = (byte)uInt64_;
				byte_0 = Ext.byte_1;
			}
			else if (uInt64_ < 65536L)
			{
				byte_2 = 126;
				byte_0 = ((ushort)uInt64_).InternalToByteArray(ByteOrder.Big);
			}
			else
			{
				byte_2 = 127;
				byte_0 = uInt64_.InternalToByteArray(ByteOrder.Big);
			}
			if (bool_1)
			{
				mask_0 = Mask.Mask;
				byte_1 = createMaskingKey();
				payloadData_1.Mask(byte_1);
			}
			else
			{
				mask_0 = Mask.Unmask;
				byte_1 = Ext.byte_1;
			}
			payloadData_0 = payloadData_1;
		}

		static WebSocketFrame()
		{
			byte_3 = CreatePingFrame(false).ToByteArray();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private static byte[] createMaskingKey()
		{
			byte[] array = new byte[4];
			WebSocket.randomNumberGenerator_0.GetBytes(array);
			return array;
		}

		private static string dump(WebSocketFrame webSocketFrame_0)
		{
			ulong uInt64_ = webSocketFrame_0.UInt64_0;
			long num = (long)(uInt64_ / 4L);
			int num2 = (int)(uInt64_ % 4L);
			int num3;
			string arg;
			if (num < 10000L)
			{
				num3 = 4;
				arg = "{0,4}";
			}
			else if (num < 65536L)
			{
				num3 = 4;
				arg = "{0,4:X}";
			}
			else if (num < 4294967296L)
			{
				num3 = 8;
				arg = "{0,8:X}";
			}
			else
			{
				num3 = 16;
				arg = "{0,16:X}";
			}
			string arg2 = string.Format("{{0,{0}}}", num3);
			string format = string.Format("\r\n{0} 01234567 89ABCDEF 01234567 89ABCDEF\r\n{0}+--------+--------+--------+--------+\\n", arg2);
			string string_4 = string.Format("{0}|{{1,8}} {{2,8}} {{3,8}} {{4,8}}|\n", arg);
			string format2 = string.Format("{0}+--------+--------+--------+--------+", arg2);
			StringBuilder stringBuilder_0 = new StringBuilder(64);
			Func<Action<string, string, string, string>> func = delegate
			{
				long long_0 = 0L;
				return delegate(string string_0, string string_1, string string_2, string string_3)
				{
					stringBuilder_0.AppendFormat(string_4, ++long_0, string_0, string_1, string_2, string_3);
				};
			};
			Action<string, string, string, string> action = func();
			stringBuilder_0.AppendFormat(format, string.Empty);
			byte[] array = webSocketFrame_0.ToByteArray();
			for (long num4 = 0L; num4 <= num; num4++)
			{
				long num5 = num4 * 4L;
				if (num4 < num)
				{
					action(Convert.ToString(array[num5], 2).PadLeft(8, '0'), Convert.ToString(array[num5 + 1L], 2).PadLeft(8, '0'), Convert.ToString(array[num5 + 2L], 2).PadLeft(8, '0'), Convert.ToString(array[num5 + 3L], 2).PadLeft(8, '0'));
				}
				else if (num2 > 0)
				{
					action(Convert.ToString(array[num5], 2).PadLeft(8, '0'), (num2 < 2) ? string.Empty : Convert.ToString(array[num5 + 1L], 2).PadLeft(8, '0'), (num2 != 3) ? string.Empty : Convert.ToString(array[num5 + 2L], 2).PadLeft(8, '0'), string.Empty);
				}
			}
			stringBuilder_0.AppendFormat(format2, string.Empty);
			return stringBuilder_0.ToString();
		}

		private static bool isControl(Opcode opcode_1)
		{
			return opcode_1 == Opcode.Close || opcode_1 == Opcode.Ping || opcode_1 == Opcode.Pong;
		}

		private static bool isData(Opcode opcode_1)
		{
			return opcode_1 == Opcode.Text || opcode_1 == Opcode.Binary;
		}

		private static string print(WebSocketFrame webSocketFrame_0)
		{
			string text = webSocketFrame_0.opcode_0.ToString();
			byte b = webSocketFrame_0.byte_2;
			string text2 = ((b < 126) ? string.Empty : ((b != 126) ? webSocketFrame_0.byte_0.ToUInt64(ByteOrder.Big).ToString() : webSocketFrame_0.byte_0.ToUInt16(ByteOrder.Big).ToString()));
			bool boolean_;
			string text3 = ((!(boolean_ = webSocketFrame_0.Boolean_8)) ? string.Empty : BitConverter.ToString(webSocketFrame_0.byte_1));
			string text4 = ((b == 0) ? string.Empty : ((b > 125) ? string.Format("A {0} frame.", text.ToLower()) : ((boolean_ || webSocketFrame_0.Boolean_7 || webSocketFrame_0.Boolean_2 || !webSocketFrame_0.Boolean_12) ? webSocketFrame_0.payloadData_0.ToString() : Encoding.UTF8.GetString(webSocketFrame_0.payloadData_0.Byte_0))));
			string format = "\r\n                    FIN: {0}\r\n                   RSV1: {1}\r\n                   RSV2: {2}\r\n                   RSV3: {3}\r\n                 Opcode: {4}\r\n                   MASK: {5}\r\n         Payload Length: {6}\r\nExtended Payload Length: {7}\r\n            Masking Key: {8}\r\n           Payload Data: {9}";
			return string.Format(format, webSocketFrame_0.fin_0, webSocketFrame_0.rsv_0, webSocketFrame_0.rsv_1, webSocketFrame_0.rsv_2, text, webSocketFrame_0.mask_0, b, text2, text3, text4);
		}

		private static WebSocketFrame read(byte[] byte_4, Stream stream_0, bool bool_0)
		{
			Fin fin = (((byte_4[0] & 0x80) == 128) ? Fin.Final : Fin.More);
			Rsv rsv = (((byte_4[0] & 0x40) == 64) ? Rsv.On : Rsv.Off);
			Rsv rsv2 = (((byte_4[0] & 0x20) == 32) ? Rsv.On : Rsv.Off);
			Rsv rsv3 = (((byte_4[0] & 0x10) == 16) ? Rsv.On : Rsv.Off);
			Opcode opcode_ = (Opcode)(byte_4[0] & 0xFu);
			Mask mask = (((byte_4[1] & 0x80) == 128) ? Mask.Mask : Mask.Unmask);
			byte b = (byte)(byte_4[1] & 0x7Fu);
			string text = ((isControl(opcode_) && b > 125) ? "A control frame has payload data which is greater than the allowable max length." : ((isControl(opcode_) && fin == Fin.More) ? "A control frame is fragmented." : ((isData(opcode_) || rsv != Rsv.On) ? null : "A non data frame is compressed.")));
			if (text != null)
			{
				throw new WebSocketException(CloseStatusCode.ProtocolError, text);
			}
			WebSocketFrame webSocketFrame = new WebSocketFrame();
			webSocketFrame.fin_0 = fin;
			webSocketFrame.rsv_0 = rsv;
			webSocketFrame.rsv_1 = rsv2;
			webSocketFrame.rsv_2 = rsv3;
			webSocketFrame.opcode_0 = opcode_;
			webSocketFrame.mask_0 = mask;
			webSocketFrame.byte_2 = b;
			int num = ((b >= 126) ? ((b != 126) ? 8 : 2) : 0);
			byte[] array = ((num <= 0) ? Ext.byte_1 : stream_0.ReadBytes(num));
			if (num > 0 && array.Length != num)
			{
				throw new WebSocketException("The 'Extended Payload Length' of a frame cannot be read from the data source.");
			}
			webSocketFrame.byte_0 = array;
			bool flag;
			byte[] array2 = ((!(flag = mask == Mask.Mask)) ? Ext.byte_1 : stream_0.ReadBytes(4));
			if (flag && array2.Length != 4)
			{
				throw new WebSocketException("The 'Masking Key' of a frame cannot be read from the data source.");
			}
			webSocketFrame.byte_1 = array2;
			ulong num2 = ((b < 126) ? b : ((b != 126) ? array.ToUInt64(ByteOrder.Big) : array.ToUInt16(ByteOrder.Big)));
			byte[] array3 = null;
			if (num2 > 0L)
			{
				if (b > 126 && num2 > long.MaxValue)
				{
					throw new WebSocketException(CloseStatusCode.TooBig, "The length of 'Payload Data' of a frame is greater than the allowable max length.");
				}
				array3 = ((b <= 126) ? stream_0.ReadBytes((int)num2) : stream_0.ReadBytes((long)num2, 1024));
				if (array3.LongLength != (long)num2)
				{
					throw new WebSocketException("The 'Payload Data' of a frame cannot be read from the data source.");
				}
			}
			else
			{
				array3 = Ext.byte_1;
			}
			webSocketFrame.payloadData_0 = new PayloadData(array3, flag);
			if (bool_0 && flag)
			{
				webSocketFrame.Unmask();
			}
			return webSocketFrame;
		}

		internal static WebSocketFrame CreateCloseFrame(PayloadData payloadData_1, bool bool_0)
		{
			return new WebSocketFrame(Fin.Final, Opcode.Close, payloadData_1, false, bool_0);
		}

		internal static WebSocketFrame CreatePingFrame(bool bool_0)
		{
			return new WebSocketFrame(Fin.Final, Opcode.Ping, new PayloadData(), false, bool_0);
		}

		internal static WebSocketFrame CreatePingFrame(byte[] byte_4, bool bool_0)
		{
			return new WebSocketFrame(Fin.Final, Opcode.Ping, new PayloadData(byte_4), false, bool_0);
		}

		internal static WebSocketFrame Read(Stream stream_0)
		{
			return Read(stream_0, true);
		}

		internal static WebSocketFrame Read(Stream stream_0, bool bool_0)
		{
			byte[] array = stream_0.ReadBytes(2);
			if (array.Length != 2)
			{
				throw new WebSocketException("The header part of a frame cannot be read from the data source.");
			}
			return read(array, stream_0, bool_0);
		}

		internal static void ReadAsync(Stream stream_0, Action<WebSocketFrame> action_0, Action<Exception> action_1)
		{
			ReadAsync(stream_0, true, action_0, action_1);
		}

		internal static void ReadAsync(Stream stream_0, bool bool_0, Action<WebSocketFrame> action_0, Action<Exception> action_1)
		{
			stream_0.ReadBytesAsync(2, delegate(byte[] byte_0)
			{
				if (byte_0.Length != 2)
				{
					throw new WebSocketException("The header part of a frame cannot be read from the data source.");
				}
				WebSocketFrame obj = read(byte_0, stream_0, bool_0);
				if (action_0 != null)
				{
					action_0(obj);
				}
			}, action_1);
		}

		internal void Unmask()
		{
			if (mask_0 != 0)
			{
				mask_0 = Mask.Unmask;
				payloadData_0.Mask(byte_1);
				byte_1 = Ext.byte_1;
			}
		}

		public IEnumerator<byte> GetEnumerator()
		{
			byte[] array = ToByteArray();
			for (int i = 0; i < array.Length; i++)
			{
				yield return array[i];
			}
		}

		public void Print(bool bool_0)
		{
			Console.WriteLine((!bool_0) ? print(this) : dump(this));
		}

		public string PrintToString(bool bool_0)
		{
			return (!bool_0) ? print(this) : dump(this);
		}

		public byte[] ToByteArray()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = (int)fin_0;
				num = (num << 1) + (int)rsv_0;
				num = (num << 1) + (int)rsv_1;
				num = (num << 1) + (int)rsv_2;
				num = (num << 4) + (int)opcode_0;
				num = (num << 1) + (int)mask_0;
				num = (num << 7) + byte_2;
				memoryStream.Write(((ushort)num).InternalToByteArray(ByteOrder.Big), 0, 2);
				if (byte_2 > 125)
				{
					memoryStream.Write(byte_0, 0, (byte_2 != 126) ? 8 : 2);
				}
				if (mask_0 == Mask.Mask)
				{
					memoryStream.Write(byte_1, 0, 4);
				}
				if (byte_2 > 0)
				{
					byte[] array = payloadData_0.ToByteArray();
					if (byte_2 < 127)
					{
						memoryStream.Write(array, 0, array.Length);
					}
					else
					{
						memoryStream.WriteBytes(array);
					}
				}
				memoryStream.Close();
				return memoryStream.ToArray();
			}
		}

		public override string ToString()
		{
			return BitConverter.ToString(ToByteArray());
		}
	}
}
