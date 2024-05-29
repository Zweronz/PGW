using System;
using System.Text;

namespace WebSocketSharp
{
	public class MessageEventArgs : EventArgs
	{
		private string string_0;

		private bool bool_0;

		private Opcode opcode_0;

		private byte[] byte_0;

		public string String_0
		{
			get
			{
				if (!bool_0)
				{
					string_0 = ((opcode_0 == Opcode.Binary) ? BitConverter.ToString(byte_0) : convertToString(byte_0));
					bool_0 = true;
				}
				return string_0;
			}
		}

		public byte[] Byte_0
		{
			get
			{
				return byte_0;
			}
		}

		public Opcode Opcode_0
		{
			get
			{
				return opcode_0;
			}
		}

		internal MessageEventArgs(WebSocketFrame webSocketFrame_0)
		{
			opcode_0 = webSocketFrame_0.Opcode_0;
			byte_0 = webSocketFrame_0.PayloadData_0.Byte_0;
		}

		internal MessageEventArgs(Opcode opcode_1, byte[] byte_1)
		{
			if ((ulong)byte_1.LongLength > 9223372036854775807uL)
			{
				throw new WebSocketException(CloseStatusCode.TooBig);
			}
			opcode_0 = opcode_1;
			byte_0 = byte_1;
		}

		private static string convertToString(byte[] byte_1)
		{
			try
			{
				return Encoding.UTF8.GetString(byte_1);
			}
			catch
			{
				return null;
			}
		}
	}
}
