using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Rilisoft.MiniJson
{
	public static class Json
	{
		private sealed class Parser : IDisposable
		{
			private enum TOKEN
			{
				NONE = 0,
				CURLY_OPEN = 1,
				CURLY_CLOSE = 2,
				SQUARED_OPEN = 3,
				SQUARED_CLOSE = 4,
				COLON = 5,
				COMMA = 6,
				STRING = 7,
				NUMBER = 8,
				TRUE = 9,
				FALSE = 10,
				NULL = 11
			}

			private const string string_0 = "{}[],:\"";

			private StringReader stringReader_0;

			[CompilerGenerated]
			private static Dictionary<string, int> dictionary_0;

			private char Char_0
			{
				get
				{
					int num = stringReader_0.Peek();
					try
					{
						return Convert.ToChar(num);
					}
					catch (OverflowException ex)
					{
						ex.Data.Add("Character", num);
						throw ex;
					}
				}
			}

			private char Char_1
			{
				get
				{
					return Convert.ToChar(stringReader_0.Read());
				}
			}

			private string String_0
			{
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (!IsWordBreak(Char_0))
					{
						stringBuilder.Append(Char_1);
						if (stringReader_0.Peek() == -1)
						{
							break;
						}
					}
					return stringBuilder.ToString();
				}
			}

			private TOKEN TOKEN_0
			{
				get
				{
					EatWhitespace();
					if (stringReader_0.Peek() == -1)
					{
						return TOKEN.NONE;
					}
					switch (Char_0)
					{
					case '"':
						return TOKEN.STRING;
					case ',':
						stringReader_0.Read();
						return TOKEN.COMMA;
					case '[':
						return TOKEN.SQUARED_OPEN;
					case '{':
						return TOKEN.CURLY_OPEN;
					default:
						switch (String_0)
						{
						case "false":
							return TOKEN.FALSE;
						case "true":
							return TOKEN.TRUE;
						case "null":
							return TOKEN.NULL;
						default:
							return TOKEN.NONE;
						}
					case '}':
						stringReader_0.Read();
						return TOKEN.CURLY_CLOSE;
					case ']':
						stringReader_0.Read();
						return TOKEN.SQUARED_CLOSE;
					case '-':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						return TOKEN.NUMBER;
					case ':':
						return TOKEN.COLON;
					}
				}
			}

			private Parser(string string_1)
			{
				stringReader_0 = new StringReader(string_1);
			}

			public static bool IsWordBreak(char char_0)
			{
				return char.IsWhiteSpace(char_0) || "{}[],:\"".IndexOf(char_0) != -1;
			}

			public static object Parse(string string_1)
			{
				using (Parser parser = new Parser(string_1))
				{
					return parser.ParseValue();
				}
			}

			public void Dispose()
			{
				stringReader_0.Dispose();
				stringReader_0 = null;
			}

			private Dictionary<string, object> ParseObject()
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				stringReader_0.Read();
				while (true)
				{
					switch (TOKEN_0)
					{
					case TOKEN.COMMA:
						continue;
					case TOKEN.NONE:
						return null;
					case TOKEN.CURLY_CLOSE:
						return dictionary;
					}
					string text = ParseString();
					if (text != null)
					{
						if (TOKEN_0 != TOKEN.COLON)
						{
							return null;
						}
						goto IL_002d;
					}
					return null;
					IL_002d:
					stringReader_0.Read();
					dictionary[text] = ParseValue();
				}
			}

			private List<object> ParseArray()
			{
				List<object> list = new List<object>();
				stringReader_0.Read();
				bool flag = true;
				while (flag)
				{
					TOKEN tOKEN_ = TOKEN_0;
					switch (tOKEN_)
					{
					case TOKEN.SQUARED_CLOSE:
						flag = false;
						break;
					default:
					{
						object item = ParseByToken(tOKEN_);
						list.Add(item);
						break;
					}
					case TOKEN.COMMA:
						break;
					case TOKEN.NONE:
						return null;
					}
				}
				return list;
			}

			private object ParseValue()
			{
				TOKEN tOKEN_ = TOKEN_0;
				return ParseByToken(tOKEN_);
			}

			private object ParseByToken(TOKEN token_0)
			{
				switch (token_0)
				{
				case TOKEN.CURLY_OPEN:
					return ParseObject();
				case TOKEN.SQUARED_OPEN:
					return ParseArray();
				default:
					return null;
				case TOKEN.STRING:
					return ParseString();
				case TOKEN.NUMBER:
					return ParseNumber();
				case TOKEN.TRUE:
					return true;
				case TOKEN.FALSE:
					return false;
				case TOKEN.NULL:
					return null;
				}
			}

			private string ParseString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringReader_0.Read();
				bool flag = true;
				while (flag)
				{
					if (stringReader_0.Peek() != -1)
					{
						char char_ = Char_1;
						switch (char_)
						{
						default:
							stringBuilder.Append(char_);
							break;
						case '\\':
							if (stringReader_0.Peek() == -1)
							{
								flag = false;
								break;
							}
							char_ = Char_1;
							switch (char_)
							{
							case 'n':
								stringBuilder.Append('\n');
								break;
							case 'r':
								stringBuilder.Append('\r');
								break;
							case 'f':
								stringBuilder.Append('\f');
								break;
							case 'b':
								stringBuilder.Append('\b');
								break;
							case '"':
							case '/':
							case '\\':
								stringBuilder.Append(char_);
								break;
							case 't':
								stringBuilder.Append('\t');
								break;
							case 'u':
							{
								char[] array = new char[4];
								for (int i = 0; i < 4; i++)
								{
									array[i] = Char_1;
								}
								stringBuilder.Append((char)Convert.ToInt32(new string(array), 16));
								break;
							}
							}
							break;
						case '"':
							flag = false;
							break;
						}
						continue;
					}
					flag = false;
					break;
				}
				return stringBuilder.ToString();
			}

			private object ParseNumber()
			{
				string text = String_0;
				if (text.IndexOf('.') == -1)
				{
					long result;
					long.TryParse(text, out result);
					return result;
				}
				double result2;
				double.TryParse(text, out result2);
				return result2;
			}

			private void EatWhitespace()
			{
				if (stringReader_0.Peek() == -1)
				{
					return;
				}
				while (char.IsWhiteSpace(Char_0))
				{
					stringReader_0.Read();
					if (stringReader_0.Peek() == -1)
					{
						break;
					}
				}
			}
		}

		private sealed class Serializer
		{
			private StringBuilder stringBuilder_0;

			private Serializer()
			{
				stringBuilder_0 = new StringBuilder();
			}

			public static string Serialize(object object_0)
			{
				Serializer serializer = new Serializer();
				serializer.SerializeValue(object_0);
				return serializer.stringBuilder_0.ToString();
			}

			private void SerializeValue(object object_0)
			{
				string string_;
				IList ilist_;
				IDictionary idictionary_;
				if (object_0 == null)
				{
					stringBuilder_0.Append("null");
				}
				else if ((string_ = object_0 as string) != null)
				{
					SerializeString(string_);
				}
				else if (object_0 is bool)
				{
					stringBuilder_0.Append((!(bool)object_0) ? "false" : "true");
				}
				else if ((ilist_ = object_0 as IList) != null)
				{
					SerializeArray(ilist_);
				}
				else if ((idictionary_ = object_0 as IDictionary) != null)
				{
					SerializeObject(idictionary_);
				}
				else if (object_0 is char)
				{
					SerializeString(new string((char)object_0, 1));
				}
				else
				{
					SerializeOther(object_0);
				}
			}

			private void SerializeObject(IDictionary idictionary_0)
			{
				bool flag = true;
				stringBuilder_0.Append('{');
				foreach (object key in idictionary_0.Keys)
				{
					if (!flag)
					{
						stringBuilder_0.Append(',');
					}
					SerializeString(key.ToString());
					stringBuilder_0.Append(':');
					SerializeValue(idictionary_0[key]);
					flag = false;
				}
				stringBuilder_0.Append('}');
			}

			private void SerializeArray(IList ilist_0)
			{
				stringBuilder_0.Append('[');
				bool flag = true;
				foreach (object item in ilist_0)
				{
					if (!flag)
					{
						stringBuilder_0.Append(',');
					}
					SerializeValue(item);
					flag = false;
				}
				stringBuilder_0.Append(']');
			}

			private void SerializeString(string string_0)
			{
				stringBuilder_0.Append('"');
				char[] array = string_0.ToCharArray();
				char[] array2 = array;
				foreach (char c in array2)
				{
					switch (c)
					{
					case '\b':
						stringBuilder_0.Append("\\b");
						continue;
					case '\t':
						stringBuilder_0.Append("\\t");
						continue;
					case '\n':
						stringBuilder_0.Append("\\n");
						continue;
					case '\\':
						stringBuilder_0.Append("\\\\");
						continue;
					case '"':
						stringBuilder_0.Append("\\\"");
						continue;
					case '\f':
						stringBuilder_0.Append("\\f");
						continue;
					case '\r':
						stringBuilder_0.Append("\\r");
						continue;
					}
					int num = Convert.ToInt32(c);
					if (num >= 32 && num <= 126)
					{
						stringBuilder_0.Append(c);
						continue;
					}
					stringBuilder_0.Append("\\u");
					stringBuilder_0.Append(num.ToString("x4"));
				}
				stringBuilder_0.Append('"');
			}

			private void SerializeOther(object object_0)
			{
				if (object_0 is float)
				{
					stringBuilder_0.Append(((float)object_0).ToString("R", CultureInfo.InvariantCulture));
				}
				else if (!(object_0 is int) && !(object_0 is uint) && !(object_0 is long) && !(object_0 is sbyte) && !(object_0 is byte) && !(object_0 is short) && !(object_0 is ushort) && !(object_0 is ulong))
				{
					if (!(object_0 is double) && !(object_0 is decimal))
					{
						SerializeString(object_0.ToString());
					}
					else
					{
						stringBuilder_0.Append(Convert.ToDouble(object_0).ToString("R", CultureInfo.InvariantCulture));
					}
				}
				else
				{
					stringBuilder_0.Append(Convert.ToInt64(object_0).ToString(CultureInfo.InvariantCulture));
				}
			}
		}

		public static object Deserialize(string string_0)
		{
			if (string.IsNullOrEmpty(string_0))
			{
				return null;
			}
			return Parser.Parse(string_0);
		}

		public static string Serialize(object object_0)
		{
			return Serializer.Serialize(object_0);
		}
	}
}
