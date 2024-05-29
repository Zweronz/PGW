using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Tasharen
{
	public class DataNode
	{
		public string string_0;

		public object object_0;

		public List<DataNode> list_0 = new List<DataNode>();

		private static object[] object_1 = new object[1];

		private static Dictionary<string, Type> dictionary_0 = new Dictionary<string, Type>();

		private static Dictionary<Type, string> dictionary_1 = new Dictionary<Type, string>();

		public Type Type_0
		{
			get
			{
				return (object_0 == null) ? typeof(void) : object_0.GetType();
			}
		}

		public object Get(Type type_0)
		{
			return ConvertValue(object_0, type_0);
		}

		public T Get<T>()
		{
			if (object_0 is T)
			{
				return (T)object_0;
			}
			object obj = Get(typeof(T));
			return (object_0 == null) ? default(T) : ((T)obj);
		}

		public DataNode AddChild()
		{
			DataNode dataNode = new DataNode();
			list_0.Add(dataNode);
			return dataNode;
		}

		public DataNode AddChild(string string_1)
		{
			DataNode dataNode = AddChild();
			dataNode.string_0 = string_1;
			return dataNode;
		}

		public DataNode AddChild(string string_1, object object_2)
		{
			DataNode dataNode = AddChild();
			dataNode.string_0 = string_1;
			dataNode.object_0 = ((!(object_2 is Enum)) ? object_2 : object_2.ToString());
			return dataNode;
		}

		public DataNode SetChild(string string_1, object object_2)
		{
			DataNode dataNode = GetChild(string_1);
			if (dataNode == null)
			{
				dataNode = AddChild();
			}
			dataNode.string_0 = string_1;
			dataNode.object_0 = ((!(object_2 is Enum)) ? object_2 : object_2.ToString());
			return dataNode;
		}

		public DataNode GetChild(string string_1)
		{
			int num = 0;
			while (true)
			{
				if (num < list_0.Count)
				{
					if (list_0[num].string_0 == string_1)
					{
						break;
					}
					num++;
					continue;
				}
				return null;
			}
			return list_0[num];
		}

		public T GetChild<T>(string string_1)
		{
			DataNode child = GetChild(string_1);
			if (child == null)
			{
				return default(T);
			}
			return child.Get<T>();
		}

		public T GetChild<T>(string string_1, T gparam_0)
		{
			DataNode child = GetChild(string_1);
			if (child == null)
			{
				return gparam_0;
			}
			return child.Get<T>();
		}

		public void Write(StreamWriter streamWriter_0)
		{
			Write(streamWriter_0, 0);
		}

		public void Read(StreamReader streamReader_0)
		{
			string nextLine = GetNextLine(streamReader_0);
			int int_ = CalculateTabs(nextLine);
			Read(streamReader_0, nextLine, ref int_);
		}

		public void Clear()
		{
			object_0 = null;
			list_0.Clear();
		}

		private string GetValueDataString()
		{
			if (object_0 is float)
			{
				return ((float)object_0).ToString(CultureInfo.InvariantCulture);
			}
			if (object_0 is Vector2)
			{
				Vector2 vector = (Vector2)object_0;
				return vector.x.ToString(CultureInfo.InvariantCulture) + ", " + vector.y.ToString(CultureInfo.InvariantCulture);
			}
			if (object_0 is Vector3)
			{
				Vector3 vector2 = (Vector3)object_0;
				return vector2.x.ToString(CultureInfo.InvariantCulture) + ", " + vector2.y.ToString(CultureInfo.InvariantCulture) + ", " + vector2.z.ToString(CultureInfo.InvariantCulture);
			}
			if (object_0 is Vector4)
			{
				Vector4 vector3 = (Vector4)object_0;
				return vector3.x.ToString(CultureInfo.InvariantCulture) + ", " + vector3.y.ToString(CultureInfo.InvariantCulture) + ", " + vector3.z.ToString(CultureInfo.InvariantCulture) + ", " + vector3.w.ToString(CultureInfo.InvariantCulture);
			}
			if (object_0 is Quaternion)
			{
				Vector3 eulerAngles = ((Quaternion)object_0).eulerAngles;
				return eulerAngles.x.ToString(CultureInfo.InvariantCulture) + ", " + eulerAngles.y.ToString(CultureInfo.InvariantCulture) + ", " + eulerAngles.z.ToString(CultureInfo.InvariantCulture);
			}
			if (object_0 is Color)
			{
				Color color = (Color)object_0;
				return color.r.ToString(CultureInfo.InvariantCulture) + ", " + color.g.ToString(CultureInfo.InvariantCulture) + ", " + color.b.ToString(CultureInfo.InvariantCulture) + ", " + color.a.ToString(CultureInfo.InvariantCulture);
			}
			if (object_0 is Color32)
			{
				Color color2 = (Color32)object_0;
				return color2.r + ", " + color2.g + ", " + color2.b + ", " + color2.a;
			}
			if (object_0 is Rect)
			{
				Rect rect = (Rect)object_0;
				return rect.x.ToString(CultureInfo.InvariantCulture) + ", " + rect.y.ToString(CultureInfo.InvariantCulture) + ", " + rect.width.ToString(CultureInfo.InvariantCulture) + ", " + rect.height.ToString(CultureInfo.InvariantCulture);
			}
			if (object_0 != null)
			{
				return object_0.ToString().Replace("\n", "\\n");
			}
			return string.Empty;
		}

		private string GetValueString()
		{
			if (Type_0 == typeof(string))
			{
				return string.Concat("\"", object_0, "\"");
			}
			if (Type_0 != typeof(Vector2) && Type_0 != typeof(Vector3) && Type_0 != typeof(Color))
			{
				return string.Format("{0}({1})", TypeToName(Type_0), GetValueDataString());
			}
			return "(" + GetValueDataString() + ")";
		}

		private bool SetValue(string string_1, Type type_0, string[] string_2)
		{
			if (type_0 != null && type_0 != typeof(void))
			{
				if (type_0 == typeof(string))
				{
					object_0 = string_1;
				}
				else if (type_0 == typeof(bool))
				{
					bool result;
					if (bool.TryParse(string_1, out result))
					{
						object_0 = result;
					}
				}
				else if (type_0 == typeof(byte))
				{
					byte result2;
					if (byte.TryParse(string_1, out result2))
					{
						object_0 = result2;
					}
				}
				else if (type_0 == typeof(short))
				{
					short result3;
					if (short.TryParse(string_1, out result3))
					{
						object_0 = result3;
					}
				}
				else if (type_0 == typeof(ushort))
				{
					ushort result4;
					if (ushort.TryParse(string_1, out result4))
					{
						object_0 = result4;
					}
				}
				else if (type_0 == typeof(int))
				{
					int result5;
					if (int.TryParse(string_1, out result5))
					{
						object_0 = result5;
					}
				}
				else if (type_0 == typeof(uint))
				{
					uint result6;
					if (uint.TryParse(string_1, out result6))
					{
						object_0 = result6;
					}
				}
				else if (type_0 == typeof(float))
				{
					float result7;
					if (float.TryParse(string_1, NumberStyles.Float, CultureInfo.InvariantCulture, out result7))
					{
						object_0 = result7;
					}
				}
				else if (type_0 == typeof(double))
				{
					double result8;
					if (double.TryParse(string_1, NumberStyles.Float, CultureInfo.InvariantCulture, out result8))
					{
						object_0 = result8;
					}
				}
				else if (type_0 == typeof(Vector2))
				{
					if (string_2 == null)
					{
						string_2 = string_1.Split(',');
					}
					Vector2 vector = default(Vector2);
					if (string_2.Length == 2 && float.TryParse(string_2[0], NumberStyles.Float, CultureInfo.InvariantCulture, out vector.x) && float.TryParse(string_2[1], NumberStyles.Float, CultureInfo.InvariantCulture, out vector.y))
					{
						object_0 = vector;
					}
				}
				else if (type_0 == typeof(Vector3))
				{
					if (string_2 == null)
					{
						string_2 = string_1.Split(',');
					}
					Vector3 vector2 = default(Vector3);
					if (string_2.Length == 3 && float.TryParse(string_2[0], NumberStyles.Float, CultureInfo.InvariantCulture, out vector2.x) && float.TryParse(string_2[1], NumberStyles.Float, CultureInfo.InvariantCulture, out vector2.y) && float.TryParse(string_2[2], NumberStyles.Float, CultureInfo.InvariantCulture, out vector2.z))
					{
						object_0 = vector2;
					}
				}
				else if (type_0 == typeof(Vector4))
				{
					if (string_2 == null)
					{
						string_2 = string_1.Split(',');
					}
					Vector4 vector3 = default(Vector4);
					if (string_2.Length == 4 && float.TryParse(string_2[0], NumberStyles.Float, CultureInfo.InvariantCulture, out vector3.x) && float.TryParse(string_2[1], NumberStyles.Float, CultureInfo.InvariantCulture, out vector3.y) && float.TryParse(string_2[2], NumberStyles.Float, CultureInfo.InvariantCulture, out vector3.z) && float.TryParse(string_2[3], NumberStyles.Float, CultureInfo.InvariantCulture, out vector3.w))
					{
						object_0 = vector3;
					}
				}
				else if (type_0 == typeof(Quaternion))
				{
					if (string_2 == null)
					{
						string_2 = string_1.Split(',');
					}
					Quaternion quaternion = default(Quaternion);
					if (string_2.Length == 4 && float.TryParse(string_2[0], NumberStyles.Float, CultureInfo.InvariantCulture, out quaternion.x) && float.TryParse(string_2[1], NumberStyles.Float, CultureInfo.InvariantCulture, out quaternion.y) && float.TryParse(string_2[2], NumberStyles.Float, CultureInfo.InvariantCulture, out quaternion.z) && float.TryParse(string_2[3], NumberStyles.Float, CultureInfo.InvariantCulture, out quaternion.w))
					{
						object_0 = quaternion;
					}
				}
				else if (type_0 == typeof(Color32))
				{
					if (string_2 == null)
					{
						string_2 = string_1.Split(',');
					}
					Color32 color = default(Color32);
					if (string_2.Length == 4 && byte.TryParse(string_2[0], out color.r) && byte.TryParse(string_2[1], out color.g) && byte.TryParse(string_2[2], out color.b) && byte.TryParse(string_2[3], out color.a))
					{
						object_0 = color;
					}
				}
				else if (type_0 == typeof(Color))
				{
					if (string_2 == null)
					{
						string_2 = string_1.Split(',');
					}
					Color color2 = default(Color);
					if (string_2.Length == 4 && float.TryParse(string_2[0], NumberStyles.Float, CultureInfo.InvariantCulture, out color2.r) && float.TryParse(string_2[1], NumberStyles.Float, CultureInfo.InvariantCulture, out color2.g) && float.TryParse(string_2[2], NumberStyles.Float, CultureInfo.InvariantCulture, out color2.b) && float.TryParse(string_2[3], NumberStyles.Float, CultureInfo.InvariantCulture, out color2.a))
					{
						object_0 = color2;
					}
				}
				else if (type_0 == typeof(Rect))
				{
					if (string_2 == null)
					{
						string_2 = string_1.Split(',');
					}
					Vector4 vector4 = default(Vector4);
					if (string_2.Length == 4 && float.TryParse(string_2[0], NumberStyles.Float, CultureInfo.InvariantCulture, out vector4.x) && float.TryParse(string_2[1], NumberStyles.Float, CultureInfo.InvariantCulture, out vector4.y) && float.TryParse(string_2[2], NumberStyles.Float, CultureInfo.InvariantCulture, out vector4.z) && float.TryParse(string_2[3], NumberStyles.Float, CultureInfo.InvariantCulture, out vector4.w))
					{
						object_0 = new Rect(vector4.x, vector4.y, vector4.z, vector4.w);
					}
				}
				else
				{
					if (type_0.IsSubclassOf(typeof(Component)))
					{
						return false;
					}
					try
					{
						MethodInfo method = type_0.GetMethod("FromString", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
						if (method == null)
						{
							return false;
						}
						object_1[0] = string_1.Replace("\\n", "\n");
						object_0 = method.Invoke(null, object_1);
					}
					catch (Exception ex)
					{
						Debug.LogWarning(ex.Message);
						return false;
					}
				}
			}
			else
			{
				object_0 = null;
			}
			return true;
		}

		public override string ToString()
		{
			string string_ = string.Empty;
			Write(ref string_, 0);
			return string_;
		}

		private void Write(ref string string_1, int int_0)
		{
			if (!string.IsNullOrEmpty(string_0))
			{
				for (int i = 0; i < int_0; i++)
				{
					string_1 += "\t";
				}
				string_1 += Escape(string_0);
				if (object_0 != null)
				{
					string_1 = string_1 + " = " + GetValueString();
				}
				string_1 += "\n";
				for (int j = 0; j < list_0.Count; j++)
				{
					list_0[j].Write(ref string_1, int_0 + 1);
				}
			}
		}

		private void Write(StreamWriter streamWriter_0, int int_0)
		{
			if (!string.IsNullOrEmpty(string_0))
			{
				for (int i = 0; i < int_0; i++)
				{
					streamWriter_0.Write("\t");
				}
				streamWriter_0.Write(Escape(string_0));
				if (object_0 != null)
				{
					streamWriter_0.Write(" = ");
					streamWriter_0.Write(GetValueString());
				}
				streamWriter_0.Write("\n");
				for (int j = 0; j < list_0.Count; j++)
				{
					list_0[j].Write(streamWriter_0, int_0 + 1);
				}
			}
		}

		private string Read(StreamReader streamReader_0, string string_1, ref int int_0)
		{
			if (string_1 != null)
			{
				int num = int_0;
				Set(string_1, num);
				string_1 = GetNextLine(streamReader_0);
				int_0 = CalculateTabs(string_1);
				while (string_1 != null && int_0 == num + 1)
				{
					string_1 = AddChild().Read(streamReader_0, string_1, ref int_0);
				}
			}
			return string_1;
		}

		private bool Set(string string_1, int int_0)
		{
			int num = string_1.IndexOf("=", int_0);
			if (num == -1)
			{
				string_0 = Unescape(string_1.Substring(int_0)).Trim();
				return true;
			}
			string_0 = Unescape(string_1.Substring(int_0, num - int_0)).Trim();
			string_1 = string_1.Substring(num + 1).Trim();
			if (string_1.Length < 3)
			{
				return false;
			}
			if (string_1[0] == '"' && string_1[string_1.Length - 1] == '"')
			{
				object_0 = string_1.Substring(1, string_1.Length - 2);
				return true;
			}
			if (string_1[0] == '(' && string_1[string_1.Length - 1] == ')')
			{
				string_1 = string_1.Substring(1, string_1.Length - 2);
				string[] array = string_1.Split(',');
				if (array.Length == 1)
				{
					return SetValue(string_1, typeof(float), null);
				}
				if (array.Length == 2)
				{
					return SetValue(string_1, typeof(Vector2), array);
				}
				if (array.Length == 3)
				{
					return SetValue(string_1, typeof(Vector3), array);
				}
				if (array.Length == 4)
				{
					return SetValue(string_1, typeof(Color), array);
				}
				object_0 = string_1;
				return true;
			}
			Type type_ = typeof(string);
			int num2 = string_1.IndexOf('(');
			if (num2 != -1)
			{
				int num3 = ((string_1[string_1.Length - 1] != ')') ? string_1.LastIndexOf(')', num2) : (string_1.Length - 1));
				if (num3 != -1 && string_1.Length > 2)
				{
					string string_2 = string_1.Substring(0, num2);
					type_ = NameToType(string_2);
					string_1 = string_1.Substring(num2 + 1, num3 - num2 - 1);
				}
			}
			return SetValue(string_1, type_, null);
		}

		private static string GetNextLine(StreamReader streamReader_0)
		{
			string text = streamReader_0.ReadLine();
			while (text != null && text.Trim().StartsWith("//"))
			{
				text = streamReader_0.ReadLine();
				if (text == null)
				{
					return null;
				}
			}
			return text;
		}

		private static int CalculateTabs(string string_1)
		{
			if (string_1 != null)
			{
				for (int i = 0; i < string_1.Length; i++)
				{
					if (string_1[i] != '\t')
					{
						return i;
					}
				}
			}
			return 0;
		}

		private static string Escape(string string_1)
		{
			if (!string.IsNullOrEmpty(string_1))
			{
				string_1 = string_1.Replace("\n", "\\n");
				string_1 = string_1.Replace("\t", "\\t");
			}
			return string_1;
		}

		private static string Unescape(string string_1)
		{
			if (!string.IsNullOrEmpty(string_1))
			{
				string_1 = string_1.Replace("\\n", "\n");
				string_1 = string_1.Replace("\\t", "\t");
			}
			return string_1;
		}

		private static Type NameToType(string string_1)
		{
			Type value;
			if (!dictionary_0.TryGetValue(string_1, out value))
			{
				value = Type.GetType(string_1);
				if (value == null)
				{
					switch (string_1)
					{
					case "String":
						value = typeof(string);
						break;
					case "Vector2":
						value = typeof(Vector2);
						break;
					case "Vector3":
						value = typeof(Vector3);
						break;
					case "Vector4":
						value = typeof(Vector4);
						break;
					case "Quaternion":
						value = typeof(Quaternion);
						break;
					case "Color":
						value = typeof(Color);
						break;
					case "Rect":
						value = typeof(Rect);
						break;
					case "Color32":
						value = typeof(Color32);
						break;
					}
				}
				dictionary_0[string_1] = value;
			}
			return value;
		}

		private static string TypeToName(Type type_0)
		{
			string value;
			if (!dictionary_1.TryGetValue(type_0, out value))
			{
				value = type_0.ToString();
				if (value.StartsWith("System."))
				{
					value = value.Substring(7);
				}
				if (value.StartsWith("UnityEngine."))
				{
					value = value.Substring(12);
				}
				dictionary_1[type_0] = value;
			}
			return value;
		}

		private static object ConvertValue(object object_2, Type type_0)
		{
			if (type_0.IsAssignableFrom(object_2.GetType()))
			{
				return object_2;
			}
			if (type_0.IsEnum)
			{
				if (object_2.GetType() == typeof(int))
				{
					return object_2;
				}
				if (object_2.GetType() == typeof(string))
				{
					string text = (string)object_2;
					if (!string.IsNullOrEmpty(text))
					{
						string[] names = Enum.GetNames(type_0);
						for (int i = 0; i < names.Length; i++)
						{
							if (names[i] == text)
							{
								return Enum.GetValues(type_0).GetValue(i);
							}
						}
					}
				}
			}
			return null;
		}
	}
}
