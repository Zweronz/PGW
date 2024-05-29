using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace SimpleJSON
{
	public class JSONNode
	{
		public virtual JSONNode this[int aIndex]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public virtual JSONNode this[string aKey]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public virtual string String_0
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		public virtual int Int32_0
		{
			get
			{
				return 0;
			}
		}

		public virtual IEnumerable<JSONNode> Prop_0
		{
			get
			{
				yield break;
			}
		}

		public IEnumerable<JSONNode> IEnumerable_0
		{
			get
			{
				IEnumerator<JSONNode> enumerator = Prop_0.GetEnumerator();
				/*Error near IL_003c: Could not find block for branch target IL_00da*/;
				yield break;
			}
		}

		public virtual int Int32_1
		{
			get
			{
				int result = 0;
				if (int.TryParse(String_0, out result))
				{
					return result;
				}
				return 0;
			}
			set
			{
				String_0 = value.ToString();
			}
		}

		public virtual float Single_0
		{
			get
			{
				float result = 0f;
				if (float.TryParse(String_0, out result))
				{
					return result;
				}
				return 0f;
			}
			set
			{
				String_0 = value.ToString();
			}
		}

		public virtual double Double_0
		{
			get
			{
				double result = 0.0;
				if (double.TryParse(String_0, out result))
				{
					return result;
				}
				return 0.0;
			}
			set
			{
				String_0 = value.ToString();
			}
		}

		public virtual bool Boolean_0
		{
			get
			{
				bool result = false;
				if (bool.TryParse(String_0, out result))
				{
					return result;
				}
				return !string.IsNullOrEmpty(String_0);
			}
			set
			{
				String_0 = ((!value) ? "false" : "true");
			}
		}

		public virtual JSONArray JSONArray_0
		{
			get
			{
				return this as JSONArray;
			}
		}

		public virtual JSONClass JSONClass_0
		{
			get
			{
				return this as JSONClass;
			}
		}

		public virtual void Add(string string_0, JSONNode jsonnode_0)
		{
		}

		public virtual void Add(JSONNode jsonnode_0)
		{
			Add(string.Empty, jsonnode_0);
		}

		public virtual JSONNode Remove(string string_0)
		{
			return null;
		}

		public virtual JSONNode Remove(int int_0)
		{
			return null;
		}

		public virtual JSONNode Remove(JSONNode jsonnode_0)
		{
			return jsonnode_0;
		}

		public override string ToString()
		{
			return "JSONNode";
		}

		public virtual string ToString(string string_0)
		{
			return "JSONNode";
		}

		public override bool Equals(object obj)
		{
			return object.ReferenceEquals(this, obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		internal static string Escape(string string_0)
		{
			string text = string.Empty;
			foreach (char c in string_0)
			{
				switch (c)
				{
				case '\b':
					text += "\\b";
					break;
				case '\t':
					text += "\\t";
					break;
				case '\n':
					text += "\\n";
					break;
				default:
					text += c;
					break;
				case '\\':
					text += "\\\\";
					break;
				case '"':
					text += "\\\"";
					break;
				case '\f':
					text += "\\f";
					break;
				case '\r':
					text += "\\r";
					break;
				}
			}
			return text;
		}

		public static JSONNode Parse(string string_0)
		{
			Stack<JSONNode> stack = new Stack<JSONNode>();
			JSONNode jSONNode = null;
			int i = 0;
			string text = string.Empty;
			string text2 = string.Empty;
			bool flag = false;
			for (; i < string_0.Length; i++)
			{
				switch (string_0[i])
				{
				case '\t':
				case ' ':
					if (flag)
					{
						text += string_0[i];
					}
					break;
				case '{':
					if (flag)
					{
						text += string_0[i];
						break;
					}
					stack.Push(new JSONClass());
					if (jSONNode != null)
					{
						text2 = text2.Trim();
						if (jSONNode is JSONArray)
						{
							jSONNode.Add(stack.Peek());
						}
						else if (text2 != string.Empty)
						{
							jSONNode.Add(text2, stack.Peek());
						}
					}
					text2 = string.Empty;
					text = string.Empty;
					jSONNode = stack.Peek();
					break;
				default:
					text += string_0[i];
					break;
				case ':':
					if (flag)
					{
						text += string_0[i];
						break;
					}
					text2 = text;
					text = string.Empty;
					break;
				case ',':
					if (flag)
					{
						text += string_0[i];
						break;
					}
					if (text != string.Empty)
					{
						if (jSONNode is JSONArray)
						{
							jSONNode.Add(text);
						}
						else if (text2 != string.Empty)
						{
							jSONNode.Add(text2, text);
						}
					}
					text2 = string.Empty;
					text = string.Empty;
					break;
				case '[':
					if (flag)
					{
						text += string_0[i];
						break;
					}
					stack.Push(new JSONArray());
					if (jSONNode != null)
					{
						text2 = text2.Trim();
						if (jSONNode is JSONArray)
						{
							jSONNode.Add(stack.Peek());
						}
						else if (text2 != string.Empty)
						{
							jSONNode.Add(text2, stack.Peek());
						}
					}
					text2 = string.Empty;
					text = string.Empty;
					jSONNode = stack.Peek();
					break;
				case '\\':
					i++;
					if (flag)
					{
						char c = string_0[i];
						switch (c)
						{
						case 'n':
							text += '\n';
							break;
						case 'r':
							text += '\r';
							break;
						default:
							text += c;
							break;
						case 'f':
							text += '\f';
							break;
						case 'b':
							text += '\b';
							break;
						case 't':
							text += '\t';
							break;
						case 'u':
						{
							string s = string_0.Substring(i + 1, 4);
							text += (char)int.Parse(s, NumberStyles.AllowHexSpecifier);
							i += 4;
							break;
						}
						}
					}
					break;
				case ']':
				case '}':
					if (flag)
					{
						text += string_0[i];
						break;
					}
					if (stack.Count != 0)
					{
						stack.Pop();
						if (text != string.Empty)
						{
							text2 = text2.Trim();
							if (jSONNode is JSONArray)
							{
								jSONNode.Add(text);
							}
							else if (text2 != string.Empty)
							{
								jSONNode.Add(text2, text);
							}
						}
						text2 = string.Empty;
						text = string.Empty;
						if (stack.Count > 0)
						{
							jSONNode = stack.Peek();
						}
						break;
					}
					throw new Exception("JSON Parse: Too many closing brackets");
				case '"':
					flag ^= true;
					break;
				case '\n':
				case '\r':
					break;
				}
			}
			if (flag)
			{
				throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
			}
			return jSONNode;
		}

		public virtual void Serialize(BinaryWriter binaryWriter_0)
		{
		}

		public void SaveToStream(Stream stream_0)
		{
			BinaryWriter binaryWriter_ = new BinaryWriter(stream_0);
			Serialize(binaryWriter_);
		}

		public void SaveToCompressedStream(Stream stream_0)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		public void SaveToCompressedFile(string string_0)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		public string SaveToCompressedBase64()
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		public void SaveToFile(string string_0)
		{
			Directory.CreateDirectory(new FileInfo(string_0).Directory.FullName);
			using (FileStream stream_ = File.OpenWrite(string_0))
			{
				SaveToStream(stream_);
			}
		}

		public string SaveToBase64()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				SaveToStream(memoryStream);
				memoryStream.Position = 0L;
				return Convert.ToBase64String(memoryStream.ToArray());
			}
		}

		public static JSONNode Deserialize(BinaryReader binaryReader_0)
		{
			JSONBinaryTag jSONBinaryTag = (JSONBinaryTag)binaryReader_0.ReadByte();
			switch (jSONBinaryTag)
			{
			default:
				throw new Exception("Error deserializing JSON. Unknown tag: " + jSONBinaryTag);
			case JSONBinaryTag.Array:
			{
				int num2 = binaryReader_0.ReadInt32();
				JSONArray jSONArray = new JSONArray();
				for (int j = 0; j < num2; j++)
				{
					jSONArray.Add(Deserialize(binaryReader_0));
				}
				return jSONArray;
			}
			case JSONBinaryTag.Class:
			{
				int num = binaryReader_0.ReadInt32();
				JSONClass jSONClass = new JSONClass();
				for (int i = 0; i < num; i++)
				{
					string string_ = binaryReader_0.ReadString();
					JSONNode jsonnode_ = Deserialize(binaryReader_0);
					jSONClass.Add(string_, jsonnode_);
				}
				return jSONClass;
			}
			case JSONBinaryTag.Value:
				return new JSONData(binaryReader_0.ReadString());
			case JSONBinaryTag.IntValue:
				return new JSONData(binaryReader_0.ReadInt32());
			case JSONBinaryTag.DoubleValue:
				return new JSONData(binaryReader_0.ReadDouble());
			case JSONBinaryTag.BoolValue:
				return new JSONData(binaryReader_0.ReadBoolean());
			case JSONBinaryTag.FloatValue:
				return new JSONData(binaryReader_0.ReadSingle());
			}
		}

		public static JSONNode LoadFromCompressedFile(string string_0)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		public static JSONNode LoadFromCompressedStream(Stream stream_0)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		public static JSONNode LoadFromCompressedBase64(string string_0)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		public static JSONNode LoadFromStream(Stream stream_0)
		{
			using (BinaryReader binaryReader_ = new BinaryReader(stream_0))
			{
				return Deserialize(binaryReader_);
			}
		}

		public static JSONNode LoadFromFile(string string_0)
		{
			using (FileStream stream_ = File.OpenRead(string_0))
			{
				return LoadFromStream(stream_);
			}
		}

		public static JSONNode LoadFromBase64(string string_0)
		{
			byte[] buffer = Convert.FromBase64String(string_0);
			MemoryStream memoryStream = new MemoryStream(buffer);
			memoryStream.Position = 0L;
			return LoadFromStream(memoryStream);
		}

		public static implicit operator JSONNode(string string_0)
		{
			return new JSONData(string_0);
		}

		public static implicit operator string(JSONNode jsonnode_0)
		{
			return (!(jsonnode_0 == null)) ? jsonnode_0.String_0 : null;
		}

		public static bool operator ==(JSONNode jsonnode_0, object object_0)
		{
			if (object_0 == null && jsonnode_0 is JSONLazyCreator)
			{
				return true;
			}
			return object.ReferenceEquals(jsonnode_0, object_0);
		}

		public static bool operator !=(JSONNode jsonnode_0, object object_0)
		{
			return !(jsonnode_0 == object_0);
		}
	}
}
