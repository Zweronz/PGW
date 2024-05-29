using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

public class MiniJSON
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

	protected static int int_13 = -1;

	protected static string string_0 = string.Empty;

	public static object jsonDecode(string string_1)
	{
		string_0 = string_1;
		if (string_1 != null)
		{
			char[] char_ = string_1.ToCharArray();
			int int_ = 0;
			bool bool_ = true;
			object result = parseValue(char_, ref int_, ref bool_);
			if (bool_)
			{
				int_13 = -1;
			}
			else
			{
				int_13 = int_;
			}
			return result;
		}
		return null;
	}

	public static string jsonEncode(object object_0)
	{
		StringBuilder stringBuilder = new StringBuilder(2000);
		return (!serializeValue(object_0, stringBuilder)) ? null : stringBuilder.ToString();
	}

	public static bool lastDecodeSuccessful()
	{
		return int_13 == -1;
	}

	public static int getLastErrorIndex()
	{
		return int_13;
	}

	public static string getLastErrorSnippet()
	{
		if (int_13 == -1)
		{
			return string.Empty;
		}
		int num = int_13 - 5;
		int num2 = int_13 + 15;
		if (num < 0)
		{
			num = 0;
		}
		if (num2 >= string_0.Length)
		{
			num2 = string_0.Length - 1;
		}
		return string_0.Substring(num, num2 - num + 1);
	}

	protected static Hashtable parseObject(char[] char_0, ref int int_14)
	{
		Hashtable hashtable = new Hashtable();
		nextToken(char_0, ref int_14);
		bool flag = false;
		while (!flag)
		{
			switch (lookAhead(char_0, int_14))
			{
			case 6:
				nextToken(char_0, ref int_14);
				continue;
			case 2:
				nextToken(char_0, ref int_14);
				return hashtable;
			case 0:
				return null;
			}
			string text = parseString(char_0, ref int_14);
			if (text != null)
			{
				int num = nextToken(char_0, ref int_14);
				if (num == 5)
				{
					bool bool_ = true;
					object value = parseValue(char_0, ref int_14, ref bool_);
					if (bool_)
					{
						hashtable[text] = value;
						continue;
					}
					return null;
				}
				return null;
			}
			return null;
		}
		return hashtable;
	}

	protected static ArrayList parseArray(char[] char_0, ref int int_14)
	{
		ArrayList arrayList = new ArrayList();
		nextToken(char_0, ref int_14);
		bool flag = false;
		while (true)
		{
			if (!flag)
			{
				int num = lookAhead(char_0, int_14);
				if (num == 0)
				{
					break;
				}
				switch (num)
				{
				case 6:
					nextToken(char_0, ref int_14);
					continue;
				default:
				{
					bool bool_ = true;
					object value = parseValue(char_0, ref int_14, ref bool_);
					if (bool_)
					{
						arrayList.Add(value);
						continue;
					}
					return null;
				}
				case 4:
					break;
				}
				nextToken(char_0, ref int_14);
			}
			return arrayList;
		}
		return null;
	}

	protected static object parseValue(char[] char_0, ref int int_14, ref bool bool_0)
	{
		switch (lookAhead(char_0, int_14))
		{
		case 1:
			return parseObject(char_0, ref int_14);
		case 3:
			return parseArray(char_0, ref int_14);
		default:
			bool_0 = false;
			return null;
		case 7:
			return parseString(char_0, ref int_14);
		case 8:
			return parseNumber(char_0, ref int_14);
		case 9:
			nextToken(char_0, ref int_14);
			return bool.Parse("TRUE");
		case 10:
			nextToken(char_0, ref int_14);
			return bool.Parse("FALSE");
		case 11:
			nextToken(char_0, ref int_14);
			return null;
		}
	}

	protected static string parseString(char[] char_0, ref int int_14)
	{
		string text = string.Empty;
		eatWhitespace(char_0, ref int_14);
		char c = char_0[int_14++];
		bool flag = false;
		while (!flag && int_14 != char_0.Length)
		{
			c = char_0[int_14++];
			switch (c)
			{
			case '\\':
				if (int_14 != char_0.Length)
				{
					switch (char_0[int_14++])
					{
					case '"':
						text += '"';
						continue;
					case '\\':
						text += '\\';
						continue;
					case '/':
						text += '/';
						continue;
					case 'b':
						text += '\b';
						continue;
					case 'f':
						text += '\f';
						continue;
					case 'n':
						text += '\n';
						continue;
					case 'r':
						text += '\r';
						continue;
					case 't':
						text += '\t';
						continue;
					case 'u':
						break;
					default:
						continue;
					}
					int num = char_0.Length - int_14;
					if (num >= 4)
					{
						char[] array = new char[4];
						Array.Copy(char_0, int_14, array, 0, 4);
						uint utf = uint.Parse(new string(array), NumberStyles.HexNumber);
						text += char.ConvertFromUtf32((int)utf);
						int_14 += 4;
						continue;
					}
				}
				break;
			default:
				text += c;
				continue;
			case '"':
				flag = true;
				break;
			}
			break;
		}
		if (!flag)
		{
			return null;
		}
		return text;
	}

	protected static double parseNumber(char[] char_0, ref int int_14)
	{
		eatWhitespace(char_0, ref int_14);
		int lastIndexOfNumber = getLastIndexOfNumber(char_0, int_14);
		int num = lastIndexOfNumber - int_14 + 1;
		char[] array = new char[num];
		Array.Copy(char_0, int_14, array, 0, num);
		int_14 = lastIndexOfNumber + 1;
		return double.Parse(new string(array));
	}

	protected static int getLastIndexOfNumber(char[] char_0, int int_14)
	{
		int i;
		for (i = int_14; i < char_0.Length && "0123456789+-.eE".IndexOf(char_0[i]) != -1; i++)
		{
		}
		return i - 1;
	}

	protected static void eatWhitespace(char[] char_0, ref int int_14)
	{
		while (int_14 < char_0.Length && " \t\n\r".IndexOf(char_0[int_14]) != -1)
		{
			int_14++;
		}
	}

	protected static int lookAhead(char[] char_0, int int_14)
	{
		int int_15 = int_14;
		return nextToken(char_0, ref int_15);
	}

	protected static int nextToken(char[] char_0, ref int int_14)
	{
		eatWhitespace(char_0, ref int_14);
		if (int_14 == char_0.Length)
		{
			return 0;
		}
		char c = char_0[int_14];
		int_14++;
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
			int_14--;
			int num = char_0.Length - int_14;
			if (num >= 5 && char_0[int_14] == 'f' && char_0[int_14 + 1] == 'a' && char_0[int_14 + 2] == 'l' && char_0[int_14 + 3] == 's' && char_0[int_14 + 4] == 'e')
			{
				int_14 += 5;
				return 10;
			}
			if (num >= 4 && char_0[int_14] == 't' && char_0[int_14 + 1] == 'r' && char_0[int_14 + 2] == 'u' && char_0[int_14 + 3] == 'e')
			{
				int_14 += 4;
				return 9;
			}
			if (num >= 4 && char_0[int_14] == 'n' && char_0[int_14 + 1] == 'u' && char_0[int_14 + 2] == 'l' && char_0[int_14 + 3] == 'l')
			{
				int_14 += 4;
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

	protected static bool serializeObjectOrArray(object object_0, StringBuilder stringBuilder_0)
	{
		if (object_0 is Hashtable)
		{
			return serializeObject((Hashtable)object_0, stringBuilder_0);
		}
		if (object_0 is ArrayList)
		{
			return serializeArray((ArrayList)object_0, stringBuilder_0);
		}
		return false;
	}

	protected static bool serializeObject(Hashtable hashtable_0, StringBuilder stringBuilder_0)
	{
		stringBuilder_0.Append("{");
		IDictionaryEnumerator enumerator = hashtable_0.GetEnumerator();
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
				serializeString(string_, stringBuilder_0);
				stringBuilder_0.Append(":");
				if (!serializeValue(value, stringBuilder_0))
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

	protected static bool serializeDictionary(Dictionary<string, string> dictionary_0, StringBuilder stringBuilder_0)
	{
		stringBuilder_0.Append("{");
		bool flag = true;
		foreach (KeyValuePair<string, string> item in dictionary_0)
		{
			if (!flag)
			{
				stringBuilder_0.Append(", ");
			}
			serializeString(item.Key, stringBuilder_0);
			stringBuilder_0.Append(":");
			serializeString(item.Value, stringBuilder_0);
			flag = false;
		}
		stringBuilder_0.Append("}");
		return true;
	}

	protected static bool serializeArray(ArrayList arrayList_0, StringBuilder stringBuilder_0)
	{
		stringBuilder_0.Append("[");
		bool flag = true;
		int num = 0;
		while (true)
		{
			if (num < arrayList_0.Count)
			{
				object object_ = arrayList_0[num];
				if (!flag)
				{
					stringBuilder_0.Append(", ");
				}
				if (!serializeValue(object_, stringBuilder_0))
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

	protected static bool serializeValue(object object_0, StringBuilder stringBuilder_0)
	{
		if (object_0 == null)
		{
			stringBuilder_0.Append("null");
		}
		else if (object_0.GetType().IsArray)
		{
			serializeArray(new ArrayList((ICollection)object_0), stringBuilder_0);
		}
		else if (object_0 is string)
		{
			serializeString((string)object_0, stringBuilder_0);
		}
		else if (object_0 is char)
		{
			serializeString(Convert.ToString((char)object_0), stringBuilder_0);
		}
		else if (object_0 is decimal)
		{
			serializeString(Convert.ToString((decimal)object_0), stringBuilder_0);
		}
		else if (object_0 is Hashtable)
		{
			serializeObject((Hashtable)object_0, stringBuilder_0);
		}
		else if (object_0 is Dictionary<string, string>)
		{
			serializeDictionary((Dictionary<string, string>)object_0, stringBuilder_0);
		}
		else if (object_0 is ArrayList)
		{
			serializeArray((ArrayList)object_0, stringBuilder_0);
		}
		else if (object_0 is bool && (bool)object_0)
		{
			stringBuilder_0.Append("true");
		}
		else if (object_0 is bool && !(bool)object_0)
		{
			stringBuilder_0.Append("false");
		}
		else
		{
			if (!object_0.GetType().IsPrimitive)
			{
				return false;
			}
			serializeNumber(Convert.ToDouble(object_0), stringBuilder_0);
		}
		return true;
	}

	protected static void serializeString(string string_1, StringBuilder stringBuilder_0)
	{
		stringBuilder_0.Append("\"");
		char[] array = string_1.ToCharArray();
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
	}

	protected static void serializeNumber(double double_0, StringBuilder stringBuilder_0)
	{
		stringBuilder_0.Append(Convert.ToString(double_0));
	}
}
