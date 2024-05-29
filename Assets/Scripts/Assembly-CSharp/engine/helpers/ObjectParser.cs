using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using engine.protobuf;

namespace engine.helpers
{
	public static class ObjectParser
	{
		public static string ObjectToString(object object_0)
		{
			Dictionary<string, object> dictionary_ = new Dictionary<string, object>();
			string[] value = Parse(object_0, 1, 0, ref dictionary_).ToArray();
			return string.Join("\n", value);
		}

		public static List<string> Parse(object object_0, int int_0, int int_1, ref Dictionary<string, object> dictionary_0)
		{
			List<string> list = new List<string>();
			if (object_0 == null)
			{
				return list;
			}
			string text = "   ";
			for (int i = 0; i < int_0; i++)
			{
				text += "   ";
			}
			Type type = object_0.GetType();
			if (type == typeof(DictionarySerialize))
			{
				DictionarySerialize dictionarySerialize = (DictionarySerialize)object_0;
				if (dictionarySerialize.dictionary_0.Count > 0)
				{
					foreach (KeyValuePair<string, object> item in dictionarySerialize.dictionary_0)
					{
						int_1++;
						if (dictionary_0 != null)
						{
							dictionary_0.Add("index" + (int_1 - 1), item.Value);
						}
						list.Add(text + item.Key + " = " + item.Value);
					}
				}
			}
			else if (type == typeof(DictionaryProtoSerialize))
			{
				DictionaryProtoSerialize dictionaryProtoSerialize = (DictionaryProtoSerialize)object_0;
				if (dictionaryProtoSerialize.dictionaryProtoData_0.dictionary_0.Count > 0)
				{
					foreach (KeyValuePair<string, WrapObjForProtobuf> item2 in dictionaryProtoSerialize.dictionaryProtoData_0.dictionary_0)
					{
						int_1++;
						if (dictionary_0 != null)
						{
							dictionary_0.Add("index" + (int_1 - 1), item2.Value);
						}
						list.Add(text + item2.Key + " = " + item2.Value);
					}
				}
			}
			else if (type.IsArray)
			{
				Array array = (Array)object_0;
				int length = array.Length;
				if (length > 0)
				{
					for (int j = 0; j < length; j++)
					{
						int_1++;
						if (dictionary_0 != null)
						{
							dictionary_0.Add("index" + (int_1 - 1), array.GetValue(j));
						}
						list.Add(text + j.ToString() + " = " + array.GetValue(j));
					}
				}
			}
			else if (type == typeof(string))
			{
				string[] array2 = ((string)object_0).Split('\n');
				for (int k = 0; k < array2.Length && (k != array2.Length - 1 || !(array2[k] == string.Empty)); k++)
				{
					list.Add(array2[k]);
					dictionary_0.Add("index" + k, array2[k]);
				}
			}
			else
			{
				MemberInfo[] members = type.GetMembers();
				if (members.Length > 0)
				{
					string empty = string.Empty;
					List<string> list2 = new List<string>();
					foreach (MemberInfo memberInfo in members)
					{
						if (memberInfo.MemberType == MemberTypes.Method)
						{
							if (empty.IndexOf(memberInfo.Name) == -1)
							{
								list2.Add(memberInfo.Name + "(); ");
							}
						}
						else
						{
							if (!(memberInfo.Name != ".ctor") || !(memberInfo.Name != "SyncRoot"))
							{
								continue;
							}
							object obj = null;
							switch (memberInfo.MemberType)
							{
							case MemberTypes.Property:
							{
								PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
								if (propertyInfo.GetIndexParameters().Length > 0)
								{
									if (type.IsArray)
									{
										Array array3 = (Array)object_0;
										int_1++;
										if (dictionary_0 != null)
										{
											dictionary_0.Add("index" + (int_1 - 1), memberInfo.Name);
										}
										list.Add(text + "[" + memberInfo.Name + "]");
										text += "   ";
										int m = 0;
										for (int length2 = array3.Length; m < length2; m++)
										{
											int_1++;
											if (dictionary_0 != null)
											{
												dictionary_0.Add("index" + (int_1 - 1), array3.GetValue(m));
											}
											list.Add(text + m.ToString() + " = " + array3.GetValue(m));
										}
										list.Add(text + "[/" + memberInfo.Name + "]");
										text = text.Substring(0, text.Length - 3);
										break;
									}
									Type[] interfaces = type.GetInterfaces();
									foreach (Type type2 in interfaces)
									{
										if (type2 == typeof(IEnumerable))
										{
											ParameterInfo parameterInfo = propertyInfo.GetIndexParameters()[0];
											if (parameterInfo.ParameterType != typeof(string))
											{
												continue;
											}
											int_1++;
											if (dictionary_0 != null)
											{
												dictionary_0.Add("index" + (int_1 - 1), memberInfo.Name);
											}
											list.Add(text + "[" + memberInfo.Name + "]");
											text += "   ";
											IEnumerable enumerable = (IEnumerable)object_0;
											foreach (object item3 in enumerable)
											{
												int_1++;
												if (dictionary_0 != null)
												{
													dictionary_0.Add("index" + (int_1 - 1), item3.ToString());
												}
												list.Add(text + item3.ToString());
											}
											list.Add(text + "[/" + memberInfo.Name + "]");
											text = text.Substring(0, text.Length - 3);
										}
										else
										{
											if (type2 != typeof(IList))
											{
												continue;
											}
											IList list3 = (IList)object_0;
											int_1++;
											if (dictionary_0 != null)
											{
												dictionary_0.Add("index" + (int_1 - 1), memberInfo.Name);
											}
											list.Add(text + "[" + memberInfo.Name + "]");
											text = text.Substring(0, text.Length - 3);
											int num = 0;
											for (int count = list3.Count; num < count; num++)
											{
												int_1++;
												if (dictionary_0 != null)
												{
													dictionary_0.Add("index" + (int_1 - 1), list3[num]);
												}
												list.Add(text + num.ToString() + " = " + list3[num]);
											}
											list.Add(text + "[/" + memberInfo.Name + "]");
											text = text.Substring(0, text.Length - 3);
										}
									}
								}
								else if (!propertyInfo.PropertyType.IsAbstract)
								{
									obj = propertyInfo.GetValue(object_0, new object[0]);
								}
								break;
							}
							case MemberTypes.Field:
							{
								FieldInfo fieldInfo = (FieldInfo)memberInfo;
								if (fieldInfo.IsStatic)
								{
									continue;
								}
								obj = fieldInfo.GetValue(object_0);
								break;
							}
							}
							if (obj != null)
							{
								int_1++;
								if (dictionary_0 != null)
								{
									dictionary_0.Add("index" + (int_1 - 1), obj.ToString());
								}
								list.Add(text + memberInfo.ToString() + " = " + obj.ToString());
							}
						}
					}
					if (list2.Count > 0)
					{
						int_1++;
						if (dictionary_0 != null)
						{
							dictionary_0.Add("index" + (int_1 - 1), "[methods]");
						}
						list.Add(text + "[methods]");
						text += "   ";
						for (int num2 = 0; num2 < list2.Count; num2++)
						{
							int_1++;
							if (dictionary_0 != null)
							{
								dictionary_0.Add("index" + (int_1 - 1), list2[num2]);
							}
							list.Add(text + list2[num2]);
						}
						int_1++;
						if (dictionary_0 != null)
						{
							dictionary_0.Add("index" + (int_1 - 1), "[/methods]");
						}
						text = text.Substring(0, text.Length - 3);
						list.Add(text + "[/methods]");
					}
				}
			}
			return list;
		}

		private static void PraseLine(ref List<string> list_0, object object_0, object object_1, int int_0, ref int int_1, ref Dictionary<string, object> dictionary_0, string string_0)
		{
			if (!(object_1 is DictionaryProtoSerialize) && !(object_1 is DictionarySerialize))
			{
				int_1++;
				list_0.Add(string.Concat(string_0, object_0, " = ", object_1));
				return;
			}
			int_1++;
			list_0.Add(string.Concat(string_0, " [", object_0, "]"));
			if (dictionary_0 != null)
			{
				dictionary_0.Add("index" + (int_1 - 1), object_1);
			}
			List<string> list = Parse(object_1, int_0 + 1, int_1, ref dictionary_0);
			int_1 += list.Count;
			if (list.Count > 0)
			{
				list_0.AddRange(list);
			}
			else
			{
				list_0.Add(string_0 + "[]");
			}
			if (list.Count == 0)
			{
				int_1++;
			}
			list_0.Add(string.Concat(string_0, " [/", object_0, "]"));
		}
	}
}
