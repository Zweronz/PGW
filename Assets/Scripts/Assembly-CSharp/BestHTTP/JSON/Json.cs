using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BestHTTP.JSON
{
	public class Json
	{
		private const int int_0 = 0;

		private const int int_1 = 1;

		private const int int_2 = 2;

		private const int int_3 = 3;

		private const int int_4 = 4;

		private const int int_5 = 5;

		private const int int_6 = 6;

		private const int int_7 = 7;

		private const int int_8 = 8;

		private const int int_9 = 9;

		private const int int_10 = 10;

		private const int int_11 = 11;

		private const int int_12 = 2000;

		public static object Decode(string string_0)
		{
			bool bool_ = true;
			return Decode(string_0, ref bool_);
		}

		public static object Decode(string string_0, ref bool bool_0)
		{
			bool_0 = true;
			if (string_0 != null)
			{
				char[] char_ = string_0.ToCharArray();
				int int_ = 0;
				return ParseValue(char_, ref int_, ref bool_0);
			}
			return null;
		}

		public static string Encode(object object_0)
		{
			StringBuilder stringBuilder = new StringBuilder(2000);
			return (!SerializeValue(object_0, stringBuilder)) ? null : stringBuilder.ToString();
		}

		protected static Dictionary<string, object> ParseObject(char[] char_0, ref int int_13, ref bool bool_0)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			NextToken(char_0, ref int_13);
			bool flag = false;
			while (!flag)
			{
				switch (LookAhead(char_0, int_13))
				{
				case 6:
					NextToken(char_0, ref int_13);
					continue;
				case 2:
					NextToken(char_0, ref int_13);
					return dictionary;
				case 0:
					bool_0 = false;
					return null;
				}
				string key = ParseString(char_0, ref int_13, ref bool_0);
				if (bool_0)
				{
					int num = NextToken(char_0, ref int_13);
					if (num == 5)
					{
						object value = ParseValue(char_0, ref int_13, ref bool_0);
						if (bool_0)
						{
							dictionary[key] = value;
							continue;
						}
						bool_0 = false;
						return null;
					}
					bool_0 = false;
					return null;
				}
				bool_0 = false;
				return null;
			}
			return dictionary;
		}

		protected static List<object> ParseArray(char[] char_0, ref int int_13, ref bool bool_0)
		{
			List<object> list = new List<object>();
			NextToken(char_0, ref int_13);
			bool flag = false;
			while (true)
			{
				if (!flag)
				{
					int num = LookAhead(char_0, int_13);
					if (num == 0)
					{
						break;
					}
					switch (num)
					{
					case 6:
						NextToken(char_0, ref int_13);
						continue;
					default:
					{
						object item = ParseValue(char_0, ref int_13, ref bool_0);
						if (bool_0)
						{
							list.Add(item);
							continue;
						}
						return null;
					}
					case 4:
						break;
					}
					NextToken(char_0, ref int_13);
				}
				return list;
			}
			bool_0 = false;
			return null;
		}

		protected static object ParseValue(char[] char_0, ref int int_13, ref bool bool_0)
		{
			switch (LookAhead(char_0, int_13))
			{
			case 1:
				return ParseObject(char_0, ref int_13, ref bool_0);
			case 3:
				return ParseArray(char_0, ref int_13, ref bool_0);
			default:
				bool_0 = false;
				return null;
			case 7:
				return ParseString(char_0, ref int_13, ref bool_0);
			case 8:
				return ParseNumber(char_0, ref int_13, ref bool_0);
			case 9:
				NextToken(char_0, ref int_13);
				return true;
			case 10:
				NextToken(char_0, ref int_13);
				return false;
			case 11:
				NextToken(char_0, ref int_13);
				return null;
			}
		}

		protected static string ParseString(char[] char_0, ref int int_13, ref bool bool_0)
		{
			StringBuilder stringBuilder = new StringBuilder(2000);
			EatWhitespace(char_0, ref int_13);
			char c = char_0[int_13++];
			bool flag = false;
			while (!flag && int_13 != char_0.Length)
			{
				c = char_0[int_13++];
				switch (c)
				{
				case '\\':
				{
					if (int_13 == char_0.Length)
					{
						break;
					}
					switch (char_0[int_13++])
					{
					case '"':
						stringBuilder.Append('"');
						continue;
					case '\\':
						stringBuilder.Append('\\');
						continue;
					case '/':
						stringBuilder.Append('/');
						continue;
					case 'b':
						stringBuilder.Append('\b');
						continue;
					case 'f':
						stringBuilder.Append('\f');
						continue;
					case 'n':
						stringBuilder.Append('\n');
						continue;
					case 'r':
						stringBuilder.Append('\r');
						continue;
					case 't':
						stringBuilder.Append('\t');
						continue;
					case 'u':
						break;
					default:
						continue;
					}
					int num = char_0.Length - int_13;
					if (num < 4)
					{
						break;
					}
					uint result;
					if (bool_0 = uint.TryParse(new string(char_0, int_13, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result))
					{
						stringBuilder.Append(char.ConvertFromUtf32((int)result));
						int_13 += 4;
						continue;
					}
					return string.Empty;
				}
				default:
					stringBuilder.Append(c);
					continue;
				case '"':
					flag = true;
					break;
				}
				break;
			}
			if (!flag)
			{
				bool_0 = false;
				return null;
			}
			return stringBuilder.ToString();
		}

		protected static double ParseNumber(char[] char_0, ref int int_13, ref bool bool_0)
		{
			EatWhitespace(char_0, ref int_13);
			int lastIndexOfNumber = GetLastIndexOfNumber(char_0, int_13);
			int length = lastIndexOfNumber - int_13 + 1;
			double result;
			bool_0 = double.TryParse(new string(char_0, int_13, length), NumberStyles.Any, CultureInfo.InvariantCulture, out result);
			int_13 = lastIndexOfNumber + 1;
			return result;
		}

		protected static int GetLastIndexOfNumber(char[] char_0, int int_13)
		{
			int i;
			for (i = int_13; i < char_0.Length && "0123456789+-.eE".IndexOf(char_0[i]) != -1; i++)
			{
			}
			return i - 1;
		}

		protected static void EatWhitespace(char[] char_0, ref int int_13)
		{
			while (int_13 < char_0.Length && " \t\n\r".IndexOf(char_0[int_13]) != -1)
			{
				int_13++;
			}
		}

		protected static int LookAhead(char[] char_0, int int_13)
		{
			int int_14 = int_13;
			return NextToken(char_0, ref int_14);
		}

		protected static int NextToken(char[] char_0, ref int int_13)
		{
			EatWhitespace(char_0, ref int_13);
			if (int_13 == char_0.Length)
			{
				return 0;
			}
			char c = char_0[int_13];
			int_13++;
			switch (c)
			{
			case '"':
				return 7;
			case ',':
				return 6;
			case '[':
				return 3;
			case '{':
				return 1;
			default:
			{
				int_13--;
				int num = char_0.Length - int_13;
				if (num >= 5 && char_0[int_13] == 'f' && char_0[int_13 + 1] == 'a' && char_0[int_13 + 2] == 'l' && char_0[int_13 + 3] == 's' && char_0[int_13 + 4] == 'e')
				{
					int_13 += 5;
					return 10;
				}
				if (num >= 4 && char_0[int_13] == 't' && char_0[int_13 + 1] == 'r' && char_0[int_13 + 2] == 'u' && char_0[int_13 + 3] == 'e')
				{
					int_13 += 4;
					return 9;
				}
				if (num >= 4 && char_0[int_13] == 'n' && char_0[int_13 + 1] == 'u' && char_0[int_13 + 2] == 'l' && char_0[int_13 + 3] == 'l')
				{
					int_13 += 4;
					return 11;
				}
				return 0;
			}
			case '}':
				return 2;
			case ']':
				return 4;
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
				return 8;
			case ':':
				return 5;
			}
		}

		protected static bool SerializeValue(object object_0, StringBuilder stringBuilder_0)
		{
			bool result = true;
			if (object_0 is string)
			{
				result = SerializeString((string)object_0, stringBuilder_0);
			}
			else if (object_0 is IDictionary)
			{
				result = SerializeObject((IDictionary)object_0, stringBuilder_0);
			}
			else if (object_0 is IList)
			{
				result = SerializeArray(object_0 as IList, stringBuilder_0);
			}
			else if (object_0 is bool && (bool)object_0)
			{
				stringBuilder_0.Append("true");
			}
			else if (object_0 is bool && !(bool)object_0)
			{
				stringBuilder_0.Append("false");
			}
			else if (object_0 is ValueType)
			{
				result = SerializeNumber(Convert.ToDouble(object_0), stringBuilder_0);
			}
			else if (object_0 == null)
			{
				stringBuilder_0.Append("null");
			}
			else
			{
				result = false;
			}
			return result;
		}

		protected static bool SerializeObject(IDictionary idictionary_0, StringBuilder stringBuilder_0)
		{
			stringBuilder_0.Append("{");
			IDictionaryEnumerator enumerator = idictionary_0.GetEnumerator();
			bool flag = true;
			while (true)
			{
				if (enumerator.MoveNext())
				{
					string string_ = enumerator.Key.ToString();
					object value = enumerator.Value;
					if (!flag)
					{
						stringBuilder_0.Append(", ");
					}
					SerializeString(string_, stringBuilder_0);
					stringBuilder_0.Append(":");
					if (!SerializeValue(value, stringBuilder_0))
					{
						break;
					}
					flag = false;
					continue;
				}
				stringBuilder_0.Append("}");
				return true;
			}
			return false;
		}

		protected static bool SerializeArray(IList ilist_0, StringBuilder stringBuilder_0)
		{
			stringBuilder_0.Append("[");
			bool flag = true;
			int num = 0;
			while (true)
			{
				if (num < ilist_0.Count)
				{
					object object_ = ilist_0[num];
					if (!flag)
					{
						stringBuilder_0.Append(", ");
					}
					if (!SerializeValue(object_, stringBuilder_0))
					{
						break;
					}
					flag = false;
					num++;
					continue;
				}
				stringBuilder_0.Append("]");
				return true;
			}
			return false;
		}

		protected static bool SerializeString(string string_0, StringBuilder stringBuilder_0)
		{
			stringBuilder_0.Append("\"");
			char[] array = string_0.ToCharArray();
			foreach (char c in array)
			{
				switch (c)
				{
				case '"':
					stringBuilder_0.Append("\\\"");
					continue;
				case '\\':
					stringBuilder_0.Append("\\\\");
					continue;
				case '\b':
					stringBuilder_0.Append("\\b");
					continue;
				case '\f':
					stringBuilder_0.Append("\\f");
					continue;
				case '\n':
					stringBuilder_0.Append("\\n");
					continue;
				case '\r':
					stringBuilder_0.Append("\\r");
					continue;
				case '\t':
					stringBuilder_0.Append("\\t");
					continue;
				}
				int num = Convert.ToInt32(c);
				if (num >= 32 && num <= 126)
				{
					stringBuilder_0.Append(c);
				}
				else
				{
					stringBuilder_0.Append("\\u" + Convert.ToString(num, 16).PadLeft(4, '0'));
				}
			}
			stringBuilder_0.Append("\"");
			return true;
		}

		protected static bool SerializeNumber(double double_0, StringBuilder stringBuilder_0)
		{
			stringBuilder_0.Append(Convert.ToString(double_0, CultureInfo.InvariantCulture));
			return true;
		}
	}
}
