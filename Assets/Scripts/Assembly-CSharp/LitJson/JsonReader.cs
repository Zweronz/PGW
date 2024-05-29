using System;
using System.Collections.Generic;
using System.IO;

namespace LitJson
{
	public class JsonReader
	{
		private static IDictionary<int, IDictionary<int, int[]>> idictionary_0;

		private Stack<int> stack_0;

		private int int_0;

		private int int_1;

		private bool bool_0;

		private bool bool_1;

		private Lexer lexer_0;

		private bool bool_2;

		private bool bool_3;

		private bool bool_4;

		private TextReader textReader_0;

		private bool bool_5;

		private bool bool_6;

		private object object_0;

		private JsonToken jsonToken_0;

		public bool Boolean_0
		{
			get
			{
				return lexer_0.Boolean_0;
			}
			set
			{
				lexer_0.Boolean_0 = value;
			}
		}

		public bool Boolean_1
		{
			get
			{
				return lexer_0.Boolean_1;
			}
			set
			{
				lexer_0.Boolean_1 = value;
			}
		}

		public bool Boolean_2
		{
			get
			{
				return bool_6;
			}
			set
			{
				bool_6 = value;
			}
		}

		public bool Boolean_3
		{
			get
			{
				return bool_1;
			}
		}

		public bool Boolean_4
		{
			get
			{
				return bool_0;
			}
		}

		public JsonToken JsonToken_0
		{
			get
			{
				return jsonToken_0;
			}
		}

		public object Object_0
		{
			get
			{
				return object_0;
			}
		}

		public JsonReader(string string_0)
			: this(new StringReader(string_0), true)
		{
		}

		public JsonReader(TextReader textReader_1)
			: this(textReader_1, false)
		{
		}

		private JsonReader(TextReader textReader_1, bool bool_7)
		{
			if (textReader_1 == null)
			{
				throw new ArgumentNullException("reader");
			}
			bool_2 = false;
			bool_3 = false;
			bool_4 = false;
			stack_0 = new Stack<int>();
			stack_0.Push(65553);
			stack_0.Push(65543);
			lexer_0 = new Lexer(textReader_1);
			bool_1 = false;
			bool_0 = false;
			bool_6 = true;
			textReader_0 = textReader_1;
			bool_5 = bool_7;
		}

		static JsonReader()
		{
			PopulateParseTable();
		}

		private static void PopulateParseTable()
		{
			idictionary_0 = new Dictionary<int, IDictionary<int, int[]>>();
			TableAddRow(ParserToken.Array);
			TableAddCol(ParserToken.Array, 91, 91, 65549);
			TableAddRow(ParserToken.ArrayPrime);
			TableAddCol(ParserToken.ArrayPrime, 34, 65550, 65551, 93);
			TableAddCol(ParserToken.ArrayPrime, 91, 65550, 65551, 93);
			TableAddCol(ParserToken.ArrayPrime, 93, 93);
			TableAddCol(ParserToken.ArrayPrime, 123, 65550, 65551, 93);
			TableAddCol(ParserToken.ArrayPrime, 65537, 65550, 65551, 93);
			TableAddCol(ParserToken.ArrayPrime, 65538, 65550, 65551, 93);
			TableAddCol(ParserToken.ArrayPrime, 65539, 65550, 65551, 93);
			TableAddCol(ParserToken.ArrayPrime, 65540, 65550, 65551, 93);
			TableAddRow(ParserToken.Object);
			TableAddCol(ParserToken.Object, 123, 123, 65545);
			TableAddRow(ParserToken.ObjectPrime);
			TableAddCol(ParserToken.ObjectPrime, 34, 65546, 65547, 125);
			TableAddCol(ParserToken.ObjectPrime, 125, 125);
			TableAddRow(ParserToken.Pair);
			TableAddCol(ParserToken.Pair, 34, 65552, 58, 65550);
			TableAddRow(ParserToken.PairRest);
			TableAddCol(ParserToken.PairRest, 44, 44, 65546, 65547);
			TableAddCol(ParserToken.PairRest, 125, 65554);
			TableAddRow(ParserToken.String);
			TableAddCol(ParserToken.String, 34, 34, 65541, 34);
			TableAddRow(ParserToken.Text);
			TableAddCol(ParserToken.Text, 91, 65548);
			TableAddCol(ParserToken.Text, 123, 65544);
			TableAddRow(ParserToken.Value);
			TableAddCol(ParserToken.Value, 34, 65552);
			TableAddCol(ParserToken.Value, 91, 65548);
			TableAddCol(ParserToken.Value, 123, 65544);
			TableAddCol(ParserToken.Value, 65537, 65537);
			TableAddCol(ParserToken.Value, 65538, 65538);
			TableAddCol(ParserToken.Value, 65539, 65539);
			TableAddCol(ParserToken.Value, 65540, 65540);
			TableAddRow(ParserToken.ValueRest);
			TableAddCol(ParserToken.ValueRest, 44, 44, 65550, 65551);
			TableAddCol(ParserToken.ValueRest, 93, 65554);
		}

		private static void TableAddCol(ParserToken parserToken_0, int int_2, params int[] int_3)
		{
			idictionary_0[(int)parserToken_0].Add(int_2, int_3);
		}

		private static void TableAddRow(ParserToken parserToken_0)
		{
			idictionary_0.Add((int)parserToken_0, new Dictionary<int, int[]>());
		}

		private void ProcessNumber(string string_0)
		{
			double result;
			int result2;
			long result3;
			if ((string_0.IndexOf('.') != -1 || string_0.IndexOf('e') != -1 || string_0.IndexOf('E') != -1) && double.TryParse(string_0, out result))
			{
				jsonToken_0 = JsonToken.Double;
				object_0 = result;
			}
			else if (int.TryParse(string_0, out result2))
			{
				jsonToken_0 = JsonToken.Int;
				object_0 = result2;
			}
			else if (long.TryParse(string_0, out result3))
			{
				jsonToken_0 = JsonToken.Long;
				object_0 = result3;
			}
			else
			{
				jsonToken_0 = JsonToken.Int;
				object_0 = 0;
			}
		}

		private void ProcessSymbol()
		{
			if (int_1 == 91)
			{
				jsonToken_0 = JsonToken.ArrayStart;
				bool_3 = true;
			}
			else if (int_1 == 93)
			{
				jsonToken_0 = JsonToken.ArrayEnd;
				bool_3 = true;
			}
			else if (int_1 == 123)
			{
				jsonToken_0 = JsonToken.ObjectStart;
				bool_3 = true;
			}
			else if (int_1 == 125)
			{
				jsonToken_0 = JsonToken.ObjectEnd;
				bool_3 = true;
			}
			else if (int_1 == 34)
			{
				if (bool_2)
				{
					bool_2 = false;
					bool_3 = true;
					return;
				}
				if (jsonToken_0 == JsonToken.None)
				{
					jsonToken_0 = JsonToken.String;
				}
				bool_2 = true;
			}
			else if (int_1 == 65541)
			{
				object_0 = lexer_0.String_0;
			}
			else if (int_1 == 65539)
			{
				jsonToken_0 = JsonToken.Boolean;
				object_0 = false;
				bool_3 = true;
			}
			else if (int_1 == 65540)
			{
				jsonToken_0 = JsonToken.Null;
				bool_3 = true;
			}
			else if (int_1 == 65537)
			{
				ProcessNumber(lexer_0.String_0);
				bool_3 = true;
			}
			else if (int_1 == 65546)
			{
				jsonToken_0 = JsonToken.PropertyName;
			}
			else if (int_1 == 65538)
			{
				jsonToken_0 = JsonToken.Boolean;
				object_0 = true;
				bool_3 = true;
			}
		}

		private bool ReadToken()
		{
			if (bool_1)
			{
				return false;
			}
			lexer_0.NextToken();
			if (lexer_0.Boolean_2)
			{
				Close();
				return false;
			}
			int_0 = lexer_0.Int32_0;
			return true;
		}

		public void Close()
		{
			if (!bool_1)
			{
				bool_1 = true;
				bool_0 = true;
				if (bool_5)
				{
					textReader_0.Dispose();
				}
				textReader_0 = null;
			}
		}

		public bool Read()
		{
			if (bool_1)
			{
				return false;
			}
			if (bool_0)
			{
				bool_0 = false;
				stack_0.Clear();
				stack_0.Push(65553);
				stack_0.Push(65543);
			}
			bool_2 = false;
			bool_3 = false;
			jsonToken_0 = JsonToken.None;
			object_0 = null;
			if (!bool_4)
			{
				bool_4 = true;
				if (!ReadToken())
				{
					return false;
				}
			}
			while (true)
			{
				if (!bool_3)
				{
					int_1 = stack_0.Pop();
					ProcessSymbol();
					if (int_1 == int_0)
					{
						if (!ReadToken())
						{
							break;
						}
						continue;
					}
					int[] array;
					try
					{
						array = idictionary_0[int_1][int_0];
					}
					catch (KeyNotFoundException exception_)
					{
						throw new JsonException((ParserToken)int_0, exception_);
					}
					if (array[0] != 65554)
					{
						for (int num = array.Length - 1; num >= 0; num--)
						{
							stack_0.Push(array[num]);
						}
					}
					continue;
				}
				if (stack_0.Peek() == 65553)
				{
					bool_0 = true;
				}
				return true;
			}
			if (stack_0.Peek() != 65553)
			{
				throw new JsonException("Input doesn't evaluate to proper JSON text");
			}
			if (bool_3)
			{
				return true;
			}
			return false;
		}
	}
}
