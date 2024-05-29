using System;
using System.IO;
using System.Text;

namespace LitJson
{
	internal class Lexer
	{
		private delegate bool StateHandler(FsmContext fsmContext_0);

		private static int[] int_0;

		private static StateHandler[] stateHandler_0;

		private bool bool_0;

		private bool bool_1;

		private bool bool_2;

		private FsmContext fsmContext_0;

		private int int_1;

		private int int_2;

		private TextReader textReader_0;

		private int int_3;

		private StringBuilder stringBuilder_0;

		private string string_0;

		private int int_4;

		private int int_5;

		public bool Boolean_0
		{
			get
			{
				return bool_0;
			}
			set
			{
				bool_0 = value;
			}
		}

		public bool Boolean_1
		{
			get
			{
				return bool_1;
			}
			set
			{
				bool_1 = value;
			}
		}

		public bool Boolean_2
		{
			get
			{
				return bool_2;
			}
		}

		public int Int32_0
		{
			get
			{
				return int_4;
			}
		}

		public string String_0
		{
			get
			{
				return string_0;
			}
		}

		public Lexer(TextReader textReader_1)
		{
			bool_0 = true;
			bool_1 = true;
			int_1 = 0;
			stringBuilder_0 = new StringBuilder(128);
			int_3 = 1;
			bool_2 = false;
			textReader_0 = textReader_1;
			fsmContext_0 = new FsmContext();
			fsmContext_0.lexer_0 = this;
		}

		static Lexer()
		{
			PopulateFsmTables();
		}

		private static int HexValue(int int_6)
		{
			switch (int_6)
			{
			default:
				return int_6 - 48;
			case 65:
			case 97:
				return 10;
			case 66:
			case 98:
				return 11;
			case 67:
			case 99:
				return 12;
			case 68:
			case 100:
				return 13;
			case 69:
			case 101:
				return 14;
			case 70:
			case 102:
				return 15;
			}
		}

		private static void PopulateFsmTables()
		{
			stateHandler_0 = new StateHandler[28]
			{
				State1, State2, State3, State4, State5, State6, State7, State8, State9, State10,
				State11, State12, State13, State14, State15, State16, State17, State18, State19, State20,
				State21, State22, State23, State24, State25, State26, State27, State28
			};
			int_0 = new int[28]
			{
				65542, 0, 65537, 65537, 0, 65537, 0, 65537, 0, 0,
				65538, 0, 0, 0, 65539, 0, 0, 65540, 65541, 65542,
				0, 0, 65541, 65542, 0, 0, 0, 0
			};
		}

		private static char ProcessEscChar(int int_6)
		{
			switch (int_6)
			{
			case 114:
				return '\r';
			default:
				return '?';
			case 110:
				return '\n';
			case 102:
				return '\f';
			case 98:
				return '\b';
			case 34:
			case 39:
			case 47:
			case 92:
				return Convert.ToChar(int_6);
			case 116:
				return '\t';
			}
		}

		private static bool State1(FsmContext fsmContext_1)
		{
			do
			{
				if (!fsmContext_1.lexer_0.GetChar())
				{
					return true;
				}
			}
			while (fsmContext_1.lexer_0.int_2 == 32 || (fsmContext_1.lexer_0.int_2 >= 9 && fsmContext_1.lexer_0.int_2 <= 13));
			if (fsmContext_1.lexer_0.int_2 >= 49 && fsmContext_1.lexer_0.int_2 <= 57)
			{
				fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
				fsmContext_1.int_0 = 3;
				return true;
			}
			switch (fsmContext_1.lexer_0.int_2)
			{
			case 39:
				if (!fsmContext_1.lexer_0.bool_1)
				{
					return false;
				}
				fsmContext_1.lexer_0.int_2 = 34;
				fsmContext_1.int_0 = 23;
				fsmContext_1.bool_0 = true;
				return true;
			case 45:
				fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
				fsmContext_1.int_0 = 2;
				return true;
			default:
				return false;
			case 116:
				fsmContext_1.int_0 = 9;
				return true;
			case 110:
				fsmContext_1.int_0 = 16;
				return true;
			case 102:
				fsmContext_1.int_0 = 12;
				return true;
			case 34:
				fsmContext_1.int_0 = 19;
				fsmContext_1.bool_0 = true;
				return true;
			case 44:
			case 58:
			case 91:
			case 93:
			case 123:
			case 125:
				fsmContext_1.int_0 = 1;
				fsmContext_1.bool_0 = true;
				return true;
			case 47:
				if (!fsmContext_1.lexer_0.bool_0)
				{
					return false;
				}
				fsmContext_1.int_0 = 25;
				return true;
			case 48:
				fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
				fsmContext_1.int_0 = 4;
				return true;
			}
		}

		private static bool State2(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			if (fsmContext_1.lexer_0.int_2 >= 49 && fsmContext_1.lexer_0.int_2 <= 57)
			{
				fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
				fsmContext_1.int_0 = 3;
				return true;
			}
			int num = fsmContext_1.lexer_0.int_2;
			if (num != 48)
			{
				return false;
			}
			fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
			fsmContext_1.int_0 = 4;
			return true;
		}

		private static bool State3(FsmContext fsmContext_1)
		{
			while (true)
			{
				if (fsmContext_1.lexer_0.GetChar())
				{
					if (fsmContext_1.lexer_0.int_2 < 48 || fsmContext_1.lexer_0.int_2 > 57)
					{
						break;
					}
					fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
					continue;
				}
				return true;
			}
			if (fsmContext_1.lexer_0.int_2 != 32 && (fsmContext_1.lexer_0.int_2 < 9 || fsmContext_1.lexer_0.int_2 > 13))
			{
				switch (fsmContext_1.lexer_0.int_2)
				{
				default:
					return false;
				case 44:
				case 93:
				case 125:
					fsmContext_1.lexer_0.UngetChar();
					fsmContext_1.bool_0 = true;
					fsmContext_1.int_0 = 1;
					return true;
				case 69:
				case 101:
					fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
					fsmContext_1.int_0 = 7;
					return true;
				case 46:
					fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
					fsmContext_1.int_0 = 5;
					return true;
				}
			}
			fsmContext_1.bool_0 = true;
			fsmContext_1.int_0 = 1;
			return true;
		}

		private static bool State4(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			if (fsmContext_1.lexer_0.int_2 != 32 && (fsmContext_1.lexer_0.int_2 < 9 || fsmContext_1.lexer_0.int_2 > 13))
			{
				switch (fsmContext_1.lexer_0.int_2)
				{
				default:
					return false;
				case 44:
				case 93:
				case 125:
					fsmContext_1.lexer_0.UngetChar();
					fsmContext_1.bool_0 = true;
					fsmContext_1.int_0 = 1;
					return true;
				case 69:
				case 101:
					fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
					fsmContext_1.int_0 = 7;
					return true;
				case 46:
					fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
					fsmContext_1.int_0 = 5;
					return true;
				}
			}
			fsmContext_1.bool_0 = true;
			fsmContext_1.int_0 = 1;
			return true;
		}

		private static bool State5(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			if (fsmContext_1.lexer_0.int_2 >= 48 && fsmContext_1.lexer_0.int_2 <= 57)
			{
				fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
				fsmContext_1.int_0 = 6;
				return true;
			}
			return false;
		}

		private static bool State6(FsmContext fsmContext_1)
		{
			while (true)
			{
				if (fsmContext_1.lexer_0.GetChar())
				{
					if (fsmContext_1.lexer_0.int_2 < 48 || fsmContext_1.lexer_0.int_2 > 57)
					{
						break;
					}
					fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
					continue;
				}
				return true;
			}
			if (fsmContext_1.lexer_0.int_2 != 32 && (fsmContext_1.lexer_0.int_2 < 9 || fsmContext_1.lexer_0.int_2 > 13))
			{
				switch (fsmContext_1.lexer_0.int_2)
				{
				default:
					return false;
				case 69:
				case 101:
					fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
					fsmContext_1.int_0 = 7;
					return true;
				case 44:
				case 93:
				case 125:
					fsmContext_1.lexer_0.UngetChar();
					fsmContext_1.bool_0 = true;
					fsmContext_1.int_0 = 1;
					return true;
				}
			}
			fsmContext_1.bool_0 = true;
			fsmContext_1.int_0 = 1;
			return true;
		}

		private static bool State7(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			if (fsmContext_1.lexer_0.int_2 >= 48 && fsmContext_1.lexer_0.int_2 <= 57)
			{
				fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
				fsmContext_1.int_0 = 8;
				return true;
			}
			switch (fsmContext_1.lexer_0.int_2)
			{
			default:
				return false;
			case 43:
			case 45:
				fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
				fsmContext_1.int_0 = 8;
				return true;
			}
		}

		private static bool State8(FsmContext fsmContext_1)
		{
			while (true)
			{
				if (fsmContext_1.lexer_0.GetChar())
				{
					if (fsmContext_1.lexer_0.int_2 < 48 || fsmContext_1.lexer_0.int_2 > 57)
					{
						break;
					}
					fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
					continue;
				}
				return true;
			}
			if (fsmContext_1.lexer_0.int_2 != 32 && (fsmContext_1.lexer_0.int_2 < 9 || fsmContext_1.lexer_0.int_2 > 13))
			{
				int num = fsmContext_1.lexer_0.int_2;
				if (num != 44 && num != 93 && num != 125)
				{
					return false;
				}
				fsmContext_1.lexer_0.UngetChar();
				fsmContext_1.bool_0 = true;
				fsmContext_1.int_0 = 1;
				return true;
			}
			fsmContext_1.bool_0 = true;
			fsmContext_1.int_0 = 1;
			return true;
		}

		private static bool State9(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			int num = fsmContext_1.lexer_0.int_2;
			if (num != 114)
			{
				return false;
			}
			fsmContext_1.int_0 = 10;
			return true;
		}

		private static bool State10(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			int num = fsmContext_1.lexer_0.int_2;
			if (num != 117)
			{
				return false;
			}
			fsmContext_1.int_0 = 11;
			return true;
		}

		private static bool State11(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			int num = fsmContext_1.lexer_0.int_2;
			if (num != 101)
			{
				return false;
			}
			fsmContext_1.bool_0 = true;
			fsmContext_1.int_0 = 1;
			return true;
		}

		private static bool State12(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			int num = fsmContext_1.lexer_0.int_2;
			if (num != 97)
			{
				return false;
			}
			fsmContext_1.int_0 = 13;
			return true;
		}

		private static bool State13(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			int num = fsmContext_1.lexer_0.int_2;
			if (num != 108)
			{
				return false;
			}
			fsmContext_1.int_0 = 14;
			return true;
		}

		private static bool State14(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			int num = fsmContext_1.lexer_0.int_2;
			if (num != 115)
			{
				return false;
			}
			fsmContext_1.int_0 = 15;
			return true;
		}

		private static bool State15(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			int num = fsmContext_1.lexer_0.int_2;
			if (num != 101)
			{
				return false;
			}
			fsmContext_1.bool_0 = true;
			fsmContext_1.int_0 = 1;
			return true;
		}

		private static bool State16(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			int num = fsmContext_1.lexer_0.int_2;
			if (num != 117)
			{
				return false;
			}
			fsmContext_1.int_0 = 17;
			return true;
		}

		private static bool State17(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			int num = fsmContext_1.lexer_0.int_2;
			if (num != 108)
			{
				return false;
			}
			fsmContext_1.int_0 = 18;
			return true;
		}

		private static bool State18(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			int num = fsmContext_1.lexer_0.int_2;
			if (num != 108)
			{
				return false;
			}
			fsmContext_1.bool_0 = true;
			fsmContext_1.int_0 = 1;
			return true;
		}

		private static bool State19(FsmContext fsmContext_1)
		{
			while (fsmContext_1.lexer_0.GetChar())
			{
				switch (fsmContext_1.lexer_0.int_2)
				{
				case 92:
					fsmContext_1.int_1 = 19;
					fsmContext_1.int_0 = 21;
					return true;
				case 34:
					fsmContext_1.lexer_0.UngetChar();
					fsmContext_1.bool_0 = true;
					fsmContext_1.int_0 = 20;
					return true;
				}
				fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
			}
			return true;
		}

		private static bool State20(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			int num = fsmContext_1.lexer_0.int_2;
			if (num != 34)
			{
				return false;
			}
			fsmContext_1.bool_0 = true;
			fsmContext_1.int_0 = 1;
			return true;
		}

		private static bool State21(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			switch (fsmContext_1.lexer_0.int_2)
			{
			default:
				return false;
			case 34:
			case 39:
			case 47:
			case 92:
			case 98:
			case 102:
			case 110:
			case 114:
			case 116:
				fsmContext_1.lexer_0.stringBuilder_0.Append(ProcessEscChar(fsmContext_1.lexer_0.int_2));
				fsmContext_1.int_0 = fsmContext_1.int_1;
				return true;
			case 117:
				fsmContext_1.int_0 = 22;
				return true;
			}
		}

		private static bool State22(FsmContext fsmContext_1)
		{
			int num = 0;
			int num2 = 4096;
			fsmContext_1.lexer_0.int_5 = 0;
			while (true)
			{
				if (fsmContext_1.lexer_0.GetChar())
				{
					if ((fsmContext_1.lexer_0.int_2 < 48 || fsmContext_1.lexer_0.int_2 > 57) && (fsmContext_1.lexer_0.int_2 < 65 || fsmContext_1.lexer_0.int_2 > 70) && (fsmContext_1.lexer_0.int_2 < 97 || fsmContext_1.lexer_0.int_2 > 102))
					{
						break;
					}
					fsmContext_1.lexer_0.int_5 += HexValue(fsmContext_1.lexer_0.int_2) * num2;
					num++;
					num2 /= 16;
					if (num == 4)
					{
						fsmContext_1.lexer_0.stringBuilder_0.Append(Convert.ToChar(fsmContext_1.lexer_0.int_5));
						fsmContext_1.int_0 = fsmContext_1.int_1;
						return true;
					}
					continue;
				}
				return true;
			}
			return false;
		}

		private static bool State23(FsmContext fsmContext_1)
		{
			while (fsmContext_1.lexer_0.GetChar())
			{
				switch (fsmContext_1.lexer_0.int_2)
				{
				case 92:
					fsmContext_1.int_1 = 23;
					fsmContext_1.int_0 = 21;
					return true;
				case 39:
					fsmContext_1.lexer_0.UngetChar();
					fsmContext_1.bool_0 = true;
					fsmContext_1.int_0 = 24;
					return true;
				}
				fsmContext_1.lexer_0.stringBuilder_0.Append((char)fsmContext_1.lexer_0.int_2);
			}
			return true;
		}

		private static bool State24(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			int num = fsmContext_1.lexer_0.int_2;
			if (num != 39)
			{
				return false;
			}
			fsmContext_1.lexer_0.int_2 = 34;
			fsmContext_1.bool_0 = true;
			fsmContext_1.int_0 = 1;
			return true;
		}

		private static bool State25(FsmContext fsmContext_1)
		{
			fsmContext_1.lexer_0.GetChar();
			switch (fsmContext_1.lexer_0.int_2)
			{
			default:
				return false;
			case 47:
				fsmContext_1.int_0 = 26;
				return true;
			case 42:
				fsmContext_1.int_0 = 27;
				return true;
			}
		}

		private static bool State26(FsmContext fsmContext_1)
		{
			do
			{
				if (!fsmContext_1.lexer_0.GetChar())
				{
					return true;
				}
			}
			while (fsmContext_1.lexer_0.int_2 != 10);
			fsmContext_1.int_0 = 1;
			return true;
		}

		private static bool State27(FsmContext fsmContext_1)
		{
			do
			{
				if (!fsmContext_1.lexer_0.GetChar())
				{
					return true;
				}
			}
			while (fsmContext_1.lexer_0.int_2 != 42);
			fsmContext_1.int_0 = 28;
			return true;
		}

		private static bool State28(FsmContext fsmContext_1)
		{
			do
			{
				if (!fsmContext_1.lexer_0.GetChar())
				{
					return true;
				}
			}
			while (fsmContext_1.lexer_0.int_2 == 42);
			if (fsmContext_1.lexer_0.int_2 == 47)
			{
				fsmContext_1.int_0 = 1;
				return true;
			}
			fsmContext_1.int_0 = 27;
			return true;
		}

		private bool GetChar()
		{
			if ((int_2 = NextChar()) != -1)
			{
				return true;
			}
			bool_2 = true;
			return false;
		}

		private int NextChar()
		{
			if (int_1 != 0)
			{
				int result = int_1;
				int_1 = 0;
				return result;
			}
			return textReader_0.Read();
		}

		public bool NextToken()
		{
			fsmContext_0.bool_0 = false;
			while (true)
			{
				StateHandler stateHandler = stateHandler_0[int_3 - 1];
				if (stateHandler(fsmContext_0))
				{
					if (bool_2)
					{
						break;
					}
					if (!fsmContext_0.bool_0)
					{
						int_3 = fsmContext_0.int_0;
						continue;
					}
					string_0 = stringBuilder_0.ToString();
					stringBuilder_0.Remove(0, stringBuilder_0.Length);
					int_4 = int_0[int_3 - 1];
					if (int_4 == 65542)
					{
						int_4 = int_2;
					}
					int_3 = fsmContext_0.int_0;
					return true;
				}
				throw new JsonException(int_2);
			}
			return false;
		}

		private void UngetChar()
		{
			int_1 = int_2;
		}
	}
}
