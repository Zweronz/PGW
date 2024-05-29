using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace WebSocketSharp.Net
{
	internal class ChunkStream
	{
		private int int_0;

		private int int_1;

		private List<Chunk> list_0;

		private bool bool_0;

		private WebHeaderCollection webHeaderCollection_0;

		private StringBuilder stringBuilder_0;

		private bool bool_1;

		private InputChunkState inputChunkState_0;

		private int int_2;

		internal WebHeaderCollection WebHeaderCollection_0
		{
			get
			{
				return webHeaderCollection_0;
			}
		}

		public int Int32_0
		{
			get
			{
				return int_1 - int_0;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return inputChunkState_0 != InputChunkState.End;
			}
		}

		public ChunkStream(WebHeaderCollection webHeaderCollection_1)
		{
			webHeaderCollection_0 = webHeaderCollection_1;
			int_1 = -1;
			list_0 = new List<Chunk>();
			stringBuilder_0 = new StringBuilder();
		}

		public ChunkStream(byte[] byte_0, int int_3, int int_4, WebHeaderCollection webHeaderCollection_1)
			: this(webHeaderCollection_1)
		{
			Write(byte_0, int_3, int_4);
		}

		private int read(byte[] byte_0, int int_3, int int_4)
		{
			int num = 0;
			int count = list_0.Count;
			for (int i = 0; i < count; i++)
			{
				Chunk chunk = list_0[i];
				if (chunk == null)
				{
					continue;
				}
				if (chunk.Int32_0 == 0)
				{
					list_0[i] = null;
					continue;
				}
				num += chunk.Read(byte_0, int_3 + num, int_4 - num);
				if (num == int_4)
				{
					break;
				}
			}
			return num;
		}

		private static string removeChunkExtension(string string_0)
		{
			int num = string_0.IndexOf(';');
			return (num <= -1) ? string_0 : string_0.Substring(0, num);
		}

		private InputChunkState seekCrLf(byte[] byte_0, ref int int_3, int int_4)
		{
			if (!bool_1)
			{
				if (byte_0[int_3++] != 13)
				{
					throwProtocolViolation("CR is expected.");
				}
				bool_1 = true;
				if (int_3 == int_4)
				{
					return InputChunkState.DataEnded;
				}
			}
			if (byte_0[int_3++] != 10)
			{
				throwProtocolViolation("LF is expected.");
			}
			return InputChunkState.None;
		}

		private InputChunkState setChunkSize(byte[] byte_0, ref int int_3, int int_4)
		{
			byte b = 0;
			while (int_3 < int_4)
			{
				b = byte_0[int_3++];
				if (!bool_1)
				{
					switch (b)
					{
					case 13:
						bool_1 = true;
						continue;
					case 10:
						throwProtocolViolation("LF is unexpected.");
						break;
					}
					if (b == 32)
					{
						bool_0 = true;
					}
					if (!bool_0)
					{
						stringBuilder_0.Append((char)b);
					}
					if (stringBuilder_0.Length > 20)
					{
						throwProtocolViolation("The chunk size is too long.");
					}
					continue;
				}
				if (b != 10)
				{
					throwProtocolViolation("LF is expected.");
				}
				break;
			}
			if (bool_1 && b == 10)
			{
				int_0 = 0;
				try
				{
					int_1 = int.Parse(removeChunkExtension(stringBuilder_0.ToString()), NumberStyles.HexNumber);
				}
				catch
				{
					throwProtocolViolation("The chunk size cannot be parsed.");
				}
				if (int_1 == 0)
				{
					int_2 = 2;
					return InputChunkState.Trailer;
				}
				return InputChunkState.Data;
			}
			return InputChunkState.None;
		}

		private InputChunkState setTrailer(byte[] byte_0, ref int int_3, int int_4)
		{
			if (int_2 == 2 && byte_0[int_3] == 13 && stringBuilder_0.Length == 0)
			{
				int_3++;
				if (int_3 < int_4 && byte_0[int_3] == 10)
				{
					int_3++;
					return InputChunkState.End;
				}
				int_3--;
			}
			while (int_3 < int_4 && int_2 < 4)
			{
				byte b = byte_0[int_3++];
				stringBuilder_0.Append((char)b);
				if (stringBuilder_0.Length > 4196)
				{
					throwProtocolViolation("The trailer is too long.");
				}
				if (int_2 != 1 && int_2 != 3)
				{
					switch (b)
					{
					case 13:
						int_2++;
						continue;
					case 10:
						throwProtocolViolation("LF is unexpected.");
						break;
					}
					int_2 = 0;
				}
				else
				{
					if (b != 10)
					{
						throwProtocolViolation("LF is expected.");
					}
					int_2++;
				}
			}
			if (int_2 < 4)
			{
				return InputChunkState.Trailer;
			}
			stringBuilder_0.Length -= 2;
			StringReader stringReader = new StringReader(stringBuilder_0.ToString());
			string text;
			while ((text = stringReader.ReadLine()) != null && text.Length > 0)
			{
				webHeaderCollection_0.Add(text);
			}
			return InputChunkState.End;
		}

		private static void throwProtocolViolation(string string_0)
		{
			throw new WebException(string_0, null, WebExceptionStatus.ServerProtocolViolation, null);
		}

		private void write(byte[] byte_0, ref int int_3, int int_4)
		{
			if (inputChunkState_0 == InputChunkState.End)
			{
				throwProtocolViolation("The chunks were ended.");
			}
			if (inputChunkState_0 == InputChunkState.None)
			{
				inputChunkState_0 = setChunkSize(byte_0, ref int_3, int_4);
				if (inputChunkState_0 == InputChunkState.None)
				{
					return;
				}
				stringBuilder_0.Length = 0;
				bool_1 = false;
				bool_0 = false;
			}
			if (inputChunkState_0 == InputChunkState.Data && int_3 < int_4)
			{
				inputChunkState_0 = writeData(byte_0, ref int_3, int_4);
				if (inputChunkState_0 == InputChunkState.Data)
				{
					return;
				}
			}
			if (inputChunkState_0 == InputChunkState.DataEnded && int_3 < int_4)
			{
				inputChunkState_0 = seekCrLf(byte_0, ref int_3, int_4);
				if (inputChunkState_0 == InputChunkState.DataEnded)
				{
					return;
				}
				bool_1 = false;
			}
			if (inputChunkState_0 == InputChunkState.Trailer && int_3 < int_4)
			{
				inputChunkState_0 = setTrailer(byte_0, ref int_3, int_4);
				if (inputChunkState_0 == InputChunkState.Trailer)
				{
					return;
				}
				stringBuilder_0.Length = 0;
			}
			if (int_3 < int_4)
			{
				write(byte_0, ref int_3, int_4);
			}
		}

		private InputChunkState writeData(byte[] byte_0, ref int int_3, int int_4)
		{
			int num = int_4 - int_3;
			int num2 = int_1 - int_0;
			if (num > num2)
			{
				num = num2;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(byte_0, int_3, array, 0, num);
			list_0.Add(new Chunk(array));
			int_3 += num;
			int_0 += num;
			return (int_0 != int_1) ? InputChunkState.Data : InputChunkState.DataEnded;
		}

		internal void ResetBuffer()
		{
			int_0 = 0;
			int_1 = -1;
			list_0.Clear();
		}

		internal int WriteAndReadBack(byte[] byte_0, int int_3, int int_4, int int_5)
		{
			Write(byte_0, int_3, int_4);
			return Read(byte_0, int_3, int_5);
		}

		public int Read(byte[] byte_0, int int_3, int int_4)
		{
			if (int_4 <= 0)
			{
				return 0;
			}
			return read(byte_0, int_3, int_4);
		}

		public void Write(byte[] byte_0, int int_3, int int_4)
		{
			if (int_4 > 0)
			{
				write(byte_0, ref int_3, int_3 + int_4);
			}
		}
	}
}
