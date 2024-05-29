using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace LitJson
{
	public class JsonWriter
	{
		private static NumberFormatInfo numberFormatInfo_0;

		private WriterContext writerContext_0;

		private Stack<WriterContext> stack_0;

		private bool bool_0;

		private char[] char_0;

		private int int_0;

		private int int_1;

		private StringBuilder stringBuilder_0;

		private bool bool_1;

		private bool bool_2;

		private TextWriter textWriter_0;

		public int Int32_0
		{
			get
			{
				return int_1;
			}
			set
			{
				int_0 = int_0 / int_1 * value;
				int_1 = value;
			}
		}

		public bool Boolean_0
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

		public TextWriter TextWriter_0
		{
			get
			{
				return textWriter_0;
			}
		}

		public bool Boolean_1
		{
			get
			{
				return bool_2;
			}
			set
			{
				bool_2 = value;
			}
		}

		public JsonWriter()
		{
			stringBuilder_0 = new StringBuilder();
			textWriter_0 = new StringWriter(stringBuilder_0);
			Init();
		}

		public JsonWriter(StringBuilder stringBuilder_1)
			: this(new StringWriter(stringBuilder_1))
		{
		}

		public JsonWriter(TextWriter textWriter_1)
		{
			if (textWriter_1 == null)
			{
				throw new ArgumentNullException("writer");
			}
			textWriter_0 = textWriter_1;
			Init();
		}

		static JsonWriter()
		{
			numberFormatInfo_0 = NumberFormatInfo.InvariantInfo;
		}

		private void DoValidation(Condition condition_0)
		{
			if (!writerContext_0.bool_2)
			{
				writerContext_0.int_0++;
			}
			if (!bool_2)
			{
				return;
			}
			if (bool_0)
			{
				throw new JsonException("A complete JSON symbol has already been written");
			}
			switch (condition_0)
			{
			case Condition.InArray:
				if (!writerContext_0.bool_0)
				{
					throw new JsonException("Can't close an array here");
				}
				break;
			case Condition.InObject:
				if (!writerContext_0.bool_1 || writerContext_0.bool_2)
				{
					throw new JsonException("Can't close an object here");
				}
				break;
			case Condition.NotAProperty:
				if (writerContext_0.bool_1 && !writerContext_0.bool_2)
				{
					throw new JsonException("Expected a property");
				}
				break;
			case Condition.Property:
				if (!writerContext_0.bool_1 || writerContext_0.bool_2)
				{
					throw new JsonException("Can't add a property here");
				}
				break;
			case Condition.Value:
				if (!writerContext_0.bool_0 && (!writerContext_0.bool_1 || !writerContext_0.bool_2))
				{
					throw new JsonException("Can't add a value here");
				}
				break;
			}
		}

		private void Init()
		{
			bool_0 = false;
			char_0 = new char[4];
			int_0 = 0;
			int_1 = 4;
			bool_1 = false;
			bool_2 = true;
			stack_0 = new Stack<WriterContext>();
			writerContext_0 = new WriterContext();
			stack_0.Push(writerContext_0);
		}

		private static void IntToHex(int int_2, char[] char_1)
		{
			for (int i = 0; i < 4; i++)
			{
				int num = int_2 % 16;
				if (num < 10)
				{
					char_1[3 - i] = (char)(48 + num);
				}
				else
				{
					char_1[3 - i] = (char)(65 + (num - 10));
				}
				int_2 >>= 4;
			}
		}

		private void Indent()
		{
			if (bool_1)
			{
				int_0 += int_1;
			}
		}

		private void Put(string string_0)
		{
			if (bool_1 && !writerContext_0.bool_2)
			{
				for (int i = 0; i < int_0; i++)
				{
					textWriter_0.Write(' ');
				}
			}
			textWriter_0.Write(string_0);
		}

		private void PutNewline()
		{
			PutNewline(true);
		}

		private void PutNewline(bool bool_3)
		{
			if (bool_3 && !writerContext_0.bool_2 && writerContext_0.int_0 > 1)
			{
				textWriter_0.Write(',');
			}
			if (bool_1 && !writerContext_0.bool_2)
			{
				textWriter_0.Write('\n');
			}
		}

		private void PutString(string string_0)
		{
			Put(string.Empty);
			textWriter_0.Write('"');
			int length = string_0.Length;
			for (int i = 0; i < length; i++)
			{
				switch (string_0[i])
				{
				case '\b':
					textWriter_0.Write("\\b");
					continue;
				case '\t':
					textWriter_0.Write("\\t");
					continue;
				case '\n':
					textWriter_0.Write("\\n");
					continue;
				case '"':
				case '\\':
					textWriter_0.Write('\\');
					textWriter_0.Write(string_0[i]);
					continue;
				case '\f':
					textWriter_0.Write("\\f");
					continue;
				case '\r':
					textWriter_0.Write("\\r");
					continue;
				}
				if (string_0[i] >= ' ' && string_0[i] <= '~')
				{
					textWriter_0.Write(string_0[i]);
					continue;
				}
				IntToHex(string_0[i], char_0);
				textWriter_0.Write("\\u");
				textWriter_0.Write(char_0);
			}
			textWriter_0.Write('"');
		}

		private void Unindent()
		{
			if (bool_1)
			{
				int_0 -= int_1;
			}
		}

		public override string ToString()
		{
			if (stringBuilder_0 == null)
			{
				return string.Empty;
			}
			return stringBuilder_0.ToString();
		}

		public void Reset()
		{
			bool_0 = false;
			stack_0.Clear();
			writerContext_0 = new WriterContext();
			stack_0.Push(writerContext_0);
			if (stringBuilder_0 != null)
			{
				stringBuilder_0.Remove(0, stringBuilder_0.Length);
			}
		}

		public void Write(bool bool_3)
		{
			DoValidation(Condition.Value);
			PutNewline();
			Put((!bool_3) ? "false" : "true");
			writerContext_0.bool_2 = false;
		}

		public void Write(decimal decimal_0)
		{
			DoValidation(Condition.Value);
			PutNewline();
			Put(Convert.ToString(decimal_0, numberFormatInfo_0));
			writerContext_0.bool_2 = false;
		}

		public void Write(double double_0)
		{
			DoValidation(Condition.Value);
			PutNewline();
			string text = Convert.ToString(double_0, numberFormatInfo_0);
			Put(text);
			if (text.IndexOf('.') == -1 && text.IndexOf('E') == -1)
			{
				textWriter_0.Write(".0");
			}
			writerContext_0.bool_2 = false;
		}

		public void Write(int int_2)
		{
			DoValidation(Condition.Value);
			PutNewline();
			Put(Convert.ToString(int_2, numberFormatInfo_0));
			writerContext_0.bool_2 = false;
		}

		public void Write(long long_0)
		{
			DoValidation(Condition.Value);
			PutNewline();
			Put(Convert.ToString(long_0, numberFormatInfo_0));
			writerContext_0.bool_2 = false;
		}

		public void Write(string string_0)
		{
			DoValidation(Condition.Value);
			PutNewline();
			if (string_0 == null)
			{
				Put("null");
			}
			else
			{
				PutString(string_0);
			}
			writerContext_0.bool_2 = false;
		}

		public void Write(ulong ulong_0)
		{
			DoValidation(Condition.Value);
			PutNewline();
			Put(Convert.ToString(ulong_0, numberFormatInfo_0));
			writerContext_0.bool_2 = false;
		}

		public void WriteArrayEnd()
		{
			DoValidation(Condition.InArray);
			PutNewline(false);
			stack_0.Pop();
			if (stack_0.Count == 1)
			{
				bool_0 = true;
			}
			else
			{
				writerContext_0 = stack_0.Peek();
				writerContext_0.bool_2 = false;
			}
			Unindent();
			Put("]");
		}

		public void WriteArrayStart()
		{
			DoValidation(Condition.NotAProperty);
			PutNewline();
			Put("[");
			writerContext_0 = new WriterContext();
			writerContext_0.bool_0 = true;
			stack_0.Push(writerContext_0);
			Indent();
		}

		public void WriteObjectEnd()
		{
			DoValidation(Condition.InObject);
			PutNewline(false);
			stack_0.Pop();
			if (stack_0.Count == 1)
			{
				bool_0 = true;
			}
			else
			{
				writerContext_0 = stack_0.Peek();
				writerContext_0.bool_2 = false;
			}
			Unindent();
			Put("}");
		}

		public void WriteObjectStart()
		{
			DoValidation(Condition.NotAProperty);
			PutNewline();
			Put("{");
			writerContext_0 = new WriterContext();
			writerContext_0.bool_1 = true;
			stack_0.Push(writerContext_0);
			Indent();
		}

		public void WritePropertyName(string string_0)
		{
			DoValidation(Condition.Property);
			PutNewline();
			PutString(string_0);
			if (bool_1)
			{
				if (string_0.Length > writerContext_0.int_1)
				{
					writerContext_0.int_1 = string_0.Length;
				}
				for (int num = writerContext_0.int_1 - string_0.Length; num >= 0; num--)
				{
					textWriter_0.Write(' ');
				}
				textWriter_0.Write(": ");
			}
			else
			{
				textWriter_0.Write(':');
			}
			writerContext_0.bool_2 = true;
		}
	}
}
