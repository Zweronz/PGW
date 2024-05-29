using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LitJson
{
	public class JsonMapper
	{
		private static int int_0;

		private static IFormatProvider iformatProvider_0;

		private static IDictionary<Type, ExporterFunc> idictionary_0;

		private static IDictionary<Type, ExporterFunc> idictionary_1;

		private static IDictionary<Type, IDictionary<Type, ImporterFunc>> idictionary_2;

		private static IDictionary<Type, IDictionary<Type, ImporterFunc>> idictionary_3;

		private static IDictionary<Type, ArrayMetadata> idictionary_4;

		private static readonly object object_0;

		private static IDictionary<Type, IDictionary<Type, MethodInfo>> idictionary_5;

		private static readonly object object_1;

		private static IDictionary<Type, ObjectMetadata> idictionary_6;

		private static readonly object object_2;

		private static IDictionary<Type, IList<PropertyMetadata>> idictionary_7;

		private static readonly object object_3;

		private static JsonWriter jsonWriter_0;

		private static readonly object object_4;

		[CompilerGenerated]
		private static WrapperFactory wrapperFactory_0;

		[CompilerGenerated]
		private static ExporterFunc exporterFunc_0;

		[CompilerGenerated]
		private static ExporterFunc exporterFunc_1;

		[CompilerGenerated]
		private static ExporterFunc exporterFunc_2;

		[CompilerGenerated]
		private static ExporterFunc exporterFunc_3;

		[CompilerGenerated]
		private static ExporterFunc exporterFunc_4;

		[CompilerGenerated]
		private static ExporterFunc exporterFunc_5;

		[CompilerGenerated]
		private static ExporterFunc exporterFunc_6;

		[CompilerGenerated]
		private static ExporterFunc exporterFunc_7;

		[CompilerGenerated]
		private static ExporterFunc exporterFunc_8;

		[CompilerGenerated]
		private static ImporterFunc importerFunc_0;

		[CompilerGenerated]
		private static ImporterFunc importerFunc_1;

		[CompilerGenerated]
		private static ImporterFunc importerFunc_2;

		[CompilerGenerated]
		private static ImporterFunc importerFunc_3;

		[CompilerGenerated]
		private static ImporterFunc importerFunc_4;

		[CompilerGenerated]
		private static ImporterFunc importerFunc_5;

		[CompilerGenerated]
		private static ImporterFunc importerFunc_6;

		[CompilerGenerated]
		private static ImporterFunc importerFunc_7;

		[CompilerGenerated]
		private static ImporterFunc importerFunc_8;

		[CompilerGenerated]
		private static ImporterFunc importerFunc_9;

		[CompilerGenerated]
		private static ImporterFunc importerFunc_10;

		[CompilerGenerated]
		private static ImporterFunc importerFunc_11;

		[CompilerGenerated]
		private static WrapperFactory wrapperFactory_1;

		[CompilerGenerated]
		private static WrapperFactory wrapperFactory_2;

		[CompilerGenerated]
		private static WrapperFactory wrapperFactory_3;

		static JsonMapper()
		{
			object_0 = new object();
			object_1 = new object();
			object_2 = new object();
			object_3 = new object();
			object_4 = new object();
			int_0 = 100;
			idictionary_4 = new Dictionary<Type, ArrayMetadata>();
			idictionary_5 = new Dictionary<Type, IDictionary<Type, MethodInfo>>();
			idictionary_6 = new Dictionary<Type, ObjectMetadata>();
			idictionary_7 = new Dictionary<Type, IList<PropertyMetadata>>();
			jsonWriter_0 = new JsonWriter();
			iformatProvider_0 = DateTimeFormatInfo.InvariantInfo;
			idictionary_0 = new Dictionary<Type, ExporterFunc>();
			idictionary_1 = new Dictionary<Type, ExporterFunc>();
			idictionary_2 = new Dictionary<Type, IDictionary<Type, ImporterFunc>>();
			idictionary_3 = new Dictionary<Type, IDictionary<Type, ImporterFunc>>();
			RegisterBaseExporters();
			RegisterBaseImporters();
		}

		private static bool HasInterface(Type type_0, string string_0)
		{
			return type_0.GetInterface(string_0, true) != null;
		}

		public static PropertyInfo[] GetPublicInstanceProperties(Type type_0)
		{
			return type_0.GetProperties();
		}

		private static void AddArrayMetadata(Type type_0)
		{
			if (idictionary_4.ContainsKey(type_0))
			{
				return;
			}
			ArrayMetadata value = default(ArrayMetadata);
			value.Boolean_0 = type_0.IsArray;
			if (HasInterface(type_0, "System.Collections.IList"))
			{
				value.Boolean_1 = true;
			}
			PropertyInfo[] publicInstanceProperties = GetPublicInstanceProperties(type_0);
			foreach (PropertyInfo propertyInfo in publicInstanceProperties)
			{
				if (!(propertyInfo.Name != "Item"))
				{
					ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
					if (indexParameters.Length == 1 && indexParameters[0].ParameterType == typeof(int))
					{
						value.Type_0 = propertyInfo.PropertyType;
					}
				}
			}
			lock (object_0)
			{
				try
				{
					idictionary_4.Add(type_0, value);
				}
				catch (ArgumentException)
				{
				}
			}
		}

		private static void AddObjectMetadata(Type type_0)
		{
			if (idictionary_6.ContainsKey(type_0))
			{
				return;
			}
			ObjectMetadata value = default(ObjectMetadata);
			if (HasInterface(type_0, "System.Collections.IDictionary"))
			{
				value.Boolean_0 = true;
			}
			value.IDictionary_0 = new Dictionary<string, PropertyMetadata>();
			PropertyInfo[] publicInstanceProperties = GetPublicInstanceProperties(type_0);
			foreach (PropertyInfo propertyInfo in publicInstanceProperties)
			{
				if (propertyInfo.Name == "Item")
				{
					ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
					if (indexParameters.Length == 1 && indexParameters[0].ParameterType == typeof(string))
					{
						value.Type_0 = propertyInfo.PropertyType;
					}
				}
				else
				{
					PropertyMetadata value2 = default(PropertyMetadata);
					value2.memberInfo_0 = propertyInfo;
					value2.type_0 = propertyInfo.PropertyType;
					value.IDictionary_0.Add(propertyInfo.Name, value2);
				}
			}
			FieldInfo[] fields = type_0.GetFields();
			foreach (FieldInfo fieldInfo in fields)
			{
				PropertyMetadata value3 = default(PropertyMetadata);
				value3.memberInfo_0 = fieldInfo;
				value3.bool_0 = true;
				value3.type_0 = fieldInfo.FieldType;
				value.IDictionary_0.Add(fieldInfo.Name, value3);
			}
			lock (object_2)
			{
				try
				{
					idictionary_6.Add(type_0, value);
				}
				catch (ArgumentException)
				{
				}
			}
		}

		private static void AddTypeProperties(Type type_0)
		{
			if (idictionary_7.ContainsKey(type_0))
			{
				return;
			}
			IList<PropertyMetadata> list = new List<PropertyMetadata>();
			PropertyInfo[] publicInstanceProperties = GetPublicInstanceProperties(type_0);
			foreach (PropertyInfo propertyInfo in publicInstanceProperties)
			{
				if (!(propertyInfo.Name == "Item"))
				{
					PropertyMetadata item = default(PropertyMetadata);
					item.memberInfo_0 = propertyInfo;
					item.bool_0 = false;
					list.Add(item);
				}
			}
			FieldInfo[] fields = type_0.GetFields();
			foreach (FieldInfo memberInfo_ in fields)
			{
				PropertyMetadata item2 = default(PropertyMetadata);
				item2.memberInfo_0 = memberInfo_;
				item2.bool_0 = true;
				list.Add(item2);
			}
			lock (object_3)
			{
				try
				{
					idictionary_7.Add(type_0, list);
				}
				catch (ArgumentException)
				{
				}
			}
		}

		private static MethodInfo GetConvOp(Type type_0, Type type_1)
		{
			lock (object_1)
			{
				if (!idictionary_5.ContainsKey(type_0))
				{
					idictionary_5.Add(type_0, new Dictionary<Type, MethodInfo>());
				}
			}
			if (idictionary_5[type_0].ContainsKey(type_1))
			{
				return idictionary_5[type_0][type_1];
			}
			MethodInfo method = type_0.GetMethod("op_Implicit", new Type[1] { type_1 });
			lock (object_1)
			{
				try
				{
					idictionary_5[type_0].Add(type_1, method);
					return method;
				}
				catch (ArgumentException)
				{
					return idictionary_5[type_0][type_1];
				}
			}
		}

		private static object ReadValue(Type type_0, JsonReader jsonReader_0)
		{
			jsonReader_0.Read();
			if (jsonReader_0.JsonToken_0 == JsonToken.ArrayEnd)
			{
				return null;
			}
			if (jsonReader_0.JsonToken_0 == JsonToken.Null)
			{
				if (!type_0.IsClass)
				{
					throw new JsonException(string.Format("Can't assign null to an instance of type {0}", type_0));
				}
				return null;
			}
			if (jsonReader_0.JsonToken_0 != JsonToken.Double && jsonReader_0.JsonToken_0 != JsonToken.Int && jsonReader_0.JsonToken_0 != JsonToken.Long && jsonReader_0.JsonToken_0 != JsonToken.String && jsonReader_0.JsonToken_0 != JsonToken.Boolean)
			{
				object obj = null;
				if (jsonReader_0.JsonToken_0 == JsonToken.ArrayStart)
				{
					if (type_0.FullName == "System.Object")
					{
						type_0 = typeof(object[]);
					}
					AddArrayMetadata(type_0);
					ArrayMetadata arrayMetadata = idictionary_4[type_0];
					if (!arrayMetadata.Boolean_0 && !arrayMetadata.Boolean_1)
					{
						throw new JsonException(string.Format("Type {0} can't act as an array", type_0));
					}
					IList list;
					Type type;
					if (!arrayMetadata.Boolean_0)
					{
						list = (IList)Activator.CreateInstance(type_0);
						type = arrayMetadata.Type_0;
					}
					else
					{
						list = new ArrayList();
						type = type_0.GetElementType();
					}
					while (true)
					{
						object obj2 = ReadValue(type, jsonReader_0);
						if (obj2 != null || jsonReader_0.JsonToken_0 != JsonToken.ArrayEnd)
						{
							list.Add(obj2);
							continue;
						}
						break;
					}
					if (arrayMetadata.Boolean_0)
					{
						int count = list.Count;
						obj = Array.CreateInstance(type, count);
						for (int i = 0; i < count; i++)
						{
							((Array)obj).SetValue(list[i], i);
						}
					}
					else
					{
						obj = list;
					}
				}
				else if (jsonReader_0.JsonToken_0 == JsonToken.ObjectStart)
				{
					if (type_0 == typeof(object))
					{
						type_0 = typeof(Dictionary<string, object>);
					}
					AddObjectMetadata(type_0);
					ObjectMetadata objectMetadata = idictionary_6[type_0];
					obj = Activator.CreateInstance(type_0);
					while (true)
					{
						jsonReader_0.Read();
						if (jsonReader_0.JsonToken_0 == JsonToken.ObjectEnd)
						{
							break;
						}
						string text = (string)jsonReader_0.Object_0;
						if (objectMetadata.IDictionary_0.ContainsKey(text))
						{
							PropertyMetadata propertyMetadata = objectMetadata.IDictionary_0[text];
							if (propertyMetadata.bool_0)
							{
								((FieldInfo)propertyMetadata.memberInfo_0).SetValue(obj, ReadValue(propertyMetadata.type_0, jsonReader_0));
								continue;
							}
							PropertyInfo propertyInfo = (PropertyInfo)propertyMetadata.memberInfo_0;
							if (propertyInfo.CanWrite)
							{
								propertyInfo.SetValue(obj, ReadValue(propertyMetadata.type_0, jsonReader_0), null);
							}
							else
							{
								ReadValue(propertyMetadata.type_0, jsonReader_0);
							}
						}
						else if (!objectMetadata.Boolean_0)
						{
							if (!jsonReader_0.Boolean_2)
							{
								throw new JsonException(string.Format("The type {0} doesn't have the property '{1}'", type_0, text));
							}
							ReadSkip(jsonReader_0);
						}
						else
						{
							((IDictionary)obj).Add(text, ReadValue(objectMetadata.Type_0, jsonReader_0));
						}
					}
				}
				return obj;
			}
			Type type2 = jsonReader_0.Object_0.GetType();
			if (type_0.IsAssignableFrom(type2))
			{
				return jsonReader_0.Object_0;
			}
			if (idictionary_3.ContainsKey(type2) && idictionary_3[type2].ContainsKey(type_0))
			{
				ImporterFunc importerFunc = idictionary_3[type2][type_0];
				return importerFunc(jsonReader_0.Object_0);
			}
			if (idictionary_2.ContainsKey(type2) && idictionary_2[type2].ContainsKey(type_0))
			{
				ImporterFunc importerFunc2 = idictionary_2[type2][type_0];
				return importerFunc2(jsonReader_0.Object_0);
			}
			if (type_0.IsEnum)
			{
				return Enum.ToObject(type_0, jsonReader_0.Object_0);
			}
			MethodInfo convOp = GetConvOp(type_0, type2);
			if (convOp == null)
			{
				throw new JsonException(string.Format("Can't assign value '{0}' (type {1}) to type {2}", jsonReader_0.Object_0, type2, type_0));
			}
			return convOp.Invoke(null, new object[1] { jsonReader_0.Object_0 });
		}

		private static IJsonWrapper ReadValue(WrapperFactory wrapperFactory_4, JsonReader jsonReader_0)
		{
			jsonReader_0.Read();
			if (jsonReader_0.JsonToken_0 != JsonToken.ArrayEnd && jsonReader_0.JsonToken_0 != JsonToken.Null)
			{
				IJsonWrapper jsonWrapper = wrapperFactory_4();
				if (jsonReader_0.JsonToken_0 == JsonToken.String)
				{
					jsonWrapper.SetString((string)jsonReader_0.Object_0);
					return jsonWrapper;
				}
				if (jsonReader_0.JsonToken_0 == JsonToken.Double)
				{
					jsonWrapper.SetDouble((double)jsonReader_0.Object_0);
					return jsonWrapper;
				}
				if (jsonReader_0.JsonToken_0 == JsonToken.Int)
				{
					jsonWrapper.SetInt((int)jsonReader_0.Object_0);
					return jsonWrapper;
				}
				if (jsonReader_0.JsonToken_0 == JsonToken.Long)
				{
					jsonWrapper.SetLong((long)jsonReader_0.Object_0);
					return jsonWrapper;
				}
				if (jsonReader_0.JsonToken_0 == JsonToken.Boolean)
				{
					jsonWrapper.SetBoolean((bool)jsonReader_0.Object_0);
					return jsonWrapper;
				}
				if (jsonReader_0.JsonToken_0 == JsonToken.ArrayStart)
				{
					jsonWrapper.SetJsonType(JsonType.Array);
					while (true)
					{
						IJsonWrapper jsonWrapper2 = ReadValue(wrapperFactory_4, jsonReader_0);
						if (jsonWrapper2 != null || jsonReader_0.JsonToken_0 != JsonToken.ArrayEnd)
						{
							jsonWrapper.Add(jsonWrapper2);
							continue;
						}
						break;
					}
				}
				else if (jsonReader_0.JsonToken_0 == JsonToken.ObjectStart)
				{
					jsonWrapper.SetJsonType(JsonType.Object);
					while (true)
					{
						jsonReader_0.Read();
						if (jsonReader_0.JsonToken_0 == JsonToken.ObjectEnd)
						{
							break;
						}
						string key = (string)jsonReader_0.Object_0;
						jsonWrapper[key] = ReadValue(wrapperFactory_4, jsonReader_0);
					}
				}
				return jsonWrapper;
			}
			return null;
		}

		private static void ReadSkip(JsonReader jsonReader_0)
		{
			ToWrapper(() => new JsonMockWrapper(), jsonReader_0);
		}

		private static void RegisterBaseExporters()
		{
			idictionary_0[typeof(byte)] = delegate(object object_5, JsonWriter jsonWriter_1)
			{
				jsonWriter_1.Write(Convert.ToInt32((byte)object_5));
			};
			idictionary_0[typeof(char)] = delegate(object object_5, JsonWriter jsonWriter_1)
			{
				jsonWriter_1.Write(Convert.ToString((char)object_5));
			};
			idictionary_0[typeof(DateTime)] = delegate(object object_5, JsonWriter jsonWriter_1)
			{
				jsonWriter_1.Write(Convert.ToString((DateTime)object_5, iformatProvider_0));
			};
			idictionary_0[typeof(decimal)] = delegate(object object_5, JsonWriter jsonWriter_1)
			{
				jsonWriter_1.Write((decimal)object_5);
			};
			idictionary_0[typeof(sbyte)] = delegate(object object_5, JsonWriter jsonWriter_1)
			{
				jsonWriter_1.Write(Convert.ToInt32((sbyte)object_5));
			};
			idictionary_0[typeof(short)] = delegate(object object_5, JsonWriter jsonWriter_1)
			{
				jsonWriter_1.Write(Convert.ToInt32((short)object_5));
			};
			idictionary_0[typeof(ushort)] = delegate(object object_5, JsonWriter jsonWriter_1)
			{
				jsonWriter_1.Write(Convert.ToInt32((ushort)object_5));
			};
			idictionary_0[typeof(uint)] = delegate(object object_5, JsonWriter jsonWriter_1)
			{
				jsonWriter_1.Write(Convert.ToUInt64((uint)object_5));
			};
			idictionary_0[typeof(ulong)] = delegate(object object_5, JsonWriter jsonWriter_1)
			{
				jsonWriter_1.Write((ulong)object_5);
			};
		}

		private static void RegisterBaseImporters()
		{
			ImporterFunc importerFunc_ = (object object_5) => Convert.ToByte((int)object_5);
			RegisterImporter(idictionary_2, typeof(int), typeof(byte), importerFunc_);
			importerFunc_ = (object object_5) => Convert.ToUInt64((int)object_5);
			RegisterImporter(idictionary_2, typeof(int), typeof(ulong), importerFunc_);
			importerFunc_ = (object object_5) => Convert.ToSByte((int)object_5);
			RegisterImporter(idictionary_2, typeof(int), typeof(sbyte), importerFunc_);
			importerFunc_ = (object object_5) => Convert.ToInt16((int)object_5);
			RegisterImporter(idictionary_2, typeof(int), typeof(short), importerFunc_);
			importerFunc_ = (object object_5) => Convert.ToUInt16((int)object_5);
			RegisterImporter(idictionary_2, typeof(int), typeof(ushort), importerFunc_);
			importerFunc_ = (object object_5) => Convert.ToUInt32((int)object_5);
			RegisterImporter(idictionary_2, typeof(int), typeof(uint), importerFunc_);
			importerFunc_ = (object object_5) => Convert.ToSingle((int)object_5);
			RegisterImporter(idictionary_2, typeof(int), typeof(float), importerFunc_);
			importerFunc_ = (object object_5) => Convert.ToDouble((int)object_5);
			RegisterImporter(idictionary_2, typeof(int), typeof(double), importerFunc_);
			importerFunc_ = (object object_5) => Convert.ToDecimal((double)object_5);
			RegisterImporter(idictionary_2, typeof(double), typeof(decimal), importerFunc_);
			importerFunc_ = (object object_5) => Convert.ToUInt32((long)object_5);
			RegisterImporter(idictionary_2, typeof(long), typeof(uint), importerFunc_);
			importerFunc_ = (object object_5) => Convert.ToChar((string)object_5);
			RegisterImporter(idictionary_2, typeof(string), typeof(char), importerFunc_);
			importerFunc_ = (object object_5) => Convert.ToDateTime((string)object_5, iformatProvider_0);
			RegisterImporter(idictionary_2, typeof(string), typeof(DateTime), importerFunc_);
		}

		private static void RegisterImporter(IDictionary<Type, IDictionary<Type, ImporterFunc>> idictionary_8, Type type_0, Type type_1, ImporterFunc importerFunc_12)
		{
			if (!idictionary_8.ContainsKey(type_0))
			{
				idictionary_8.Add(type_0, new Dictionary<Type, ImporterFunc>());
			}
			idictionary_8[type_0][type_1] = importerFunc_12;
		}

		private static void WriteValue(object object_5, JsonWriter jsonWriter_1, bool bool_0, int int_1)
		{
			if (int_1 > int_0)
			{
				throw new JsonException(string.Format("Max allowed object depth reached while trying to export from type {0}", object_5.GetType()));
			}
			if (object_5 == null)
			{
				jsonWriter_1.Write(null);
				return;
			}
			if (object_5 is IJsonWrapper)
			{
				if (bool_0)
				{
					jsonWriter_1.TextWriter_0.Write(((IJsonWrapper)object_5).ToJson());
				}
				else
				{
					((IJsonWrapper)object_5).ToJson(jsonWriter_1);
				}
				return;
			}
			if (object_5 is string)
			{
				jsonWriter_1.Write((string)object_5);
				return;
			}
			if (object_5 is double)
			{
				jsonWriter_1.Write((double)object_5);
				return;
			}
			if (object_5 is int)
			{
				jsonWriter_1.Write((int)object_5);
				return;
			}
			if (object_5 is bool)
			{
				jsonWriter_1.Write((bool)object_5);
				return;
			}
			if (object_5 is long)
			{
				jsonWriter_1.Write((long)object_5);
				return;
			}
			if (object_5 is Array)
			{
				jsonWriter_1.WriteArrayStart();
				foreach (object item in (Array)object_5)
				{
					WriteValue(item, jsonWriter_1, bool_0, int_1 + 1);
				}
				jsonWriter_1.WriteArrayEnd();
				return;
			}
			if (object_5 is IList)
			{
				jsonWriter_1.WriteArrayStart();
				foreach (object item2 in (IList)object_5)
				{
					WriteValue(item2, jsonWriter_1, bool_0, int_1 + 1);
				}
				jsonWriter_1.WriteArrayEnd();
				return;
			}
			if (object_5 is IDictionary)
			{
				jsonWriter_1.WriteObjectStart();
				foreach (DictionaryEntry item3 in (IDictionary)object_5)
				{
					jsonWriter_1.WritePropertyName((string)item3.Key);
					WriteValue(item3.Value, jsonWriter_1, bool_0, int_1 + 1);
				}
				jsonWriter_1.WriteObjectEnd();
				return;
			}
			Type type = object_5.GetType();
			if (idictionary_1.ContainsKey(type))
			{
				ExporterFunc exporterFunc = idictionary_1[type];
				exporterFunc(object_5, jsonWriter_1);
				return;
			}
			if (idictionary_0.ContainsKey(type))
			{
				ExporterFunc exporterFunc2 = idictionary_0[type];
				exporterFunc2(object_5, jsonWriter_1);
				return;
			}
			if (object_5 is Enum)
			{
				Type underlyingType = Enum.GetUnderlyingType(type);
				if (underlyingType != typeof(long) && underlyingType != typeof(uint) && underlyingType != typeof(ulong))
				{
					jsonWriter_1.Write((int)object_5);
				}
				else
				{
					jsonWriter_1.Write((ulong)object_5);
				}
				return;
			}
			AddTypeProperties(type);
			IList<PropertyMetadata> list = idictionary_7[type];
			jsonWriter_1.WriteObjectStart();
			foreach (PropertyMetadata item4 in list)
			{
				if (item4.bool_0)
				{
					jsonWriter_1.WritePropertyName(item4.memberInfo_0.Name);
					WriteValue(((FieldInfo)item4.memberInfo_0).GetValue(object_5), jsonWriter_1, bool_0, int_1 + 1);
					continue;
				}
				PropertyInfo propertyInfo = (PropertyInfo)item4.memberInfo_0;
				if (propertyInfo.CanRead)
				{
					jsonWriter_1.WritePropertyName(item4.memberInfo_0.Name);
					WriteValue(propertyInfo.GetValue(object_5, null), jsonWriter_1, bool_0, int_1 + 1);
				}
			}
			jsonWriter_1.WriteObjectEnd();
		}

		public static string ToJson(object object_5)
		{
			lock (object_4)
			{
				jsonWriter_0.Reset();
				WriteValue(object_5, jsonWriter_0, true, 0);
				return jsonWriter_0.ToString();
			}
		}

		public static void ToJson(object object_5, JsonWriter jsonWriter_1)
		{
			WriteValue(object_5, jsonWriter_1, false, 0);
		}

		public static JsonData ToObject(JsonReader jsonReader_0)
		{
			return (JsonData)ToWrapper(() => new JsonData(), jsonReader_0);
		}

		public static JsonData ToObject(TextReader textReader_0)
		{
			JsonReader jsonReader_ = new JsonReader(textReader_0);
			return (JsonData)ToWrapper(() => new JsonData(), jsonReader_);
		}

		public static JsonData ToObject(string string_0)
		{
			return (JsonData)ToWrapper(() => new JsonData(), string_0);
		}

		public static T ToObject<T>(JsonReader jsonReader_0)
		{
			return (T)ReadValue(typeof(T), jsonReader_0);
		}

		public static T ToObject<T>(TextReader textReader_0)
		{
			JsonReader jsonReader_ = new JsonReader(textReader_0);
			return (T)ReadValue(typeof(T), jsonReader_);
		}

		public static T ToObject<T>(string string_0)
		{
			JsonReader jsonReader_ = new JsonReader(string_0);
			return (T)ReadValue(typeof(T), jsonReader_);
		}

		public static IJsonWrapper ToWrapper(WrapperFactory wrapperFactory_4, JsonReader jsonReader_0)
		{
			return ReadValue(wrapperFactory_4, jsonReader_0);
		}

		public static IJsonWrapper ToWrapper(WrapperFactory wrapperFactory_4, string string_0)
		{
			JsonReader jsonReader_ = new JsonReader(string_0);
			return ReadValue(wrapperFactory_4, jsonReader_);
		}

		public static void RegisterExporter<T>(ExporterFunc<T> exporterFunc_9)
		{
			ExporterFunc value = delegate(object object_0, JsonWriter jsonWriter_0)
			{
				exporterFunc_9((T)object_0, jsonWriter_0);
			};
			idictionary_1[typeof(T)] = value;
		}

		public static void RegisterImporter<TJson, TValue>(ImporterFunc<TJson, TValue> importerFunc_12)
		{
			ImporterFunc importerFunc_13 = (object object_0) => importerFunc_12((TJson)object_0);
			RegisterImporter(idictionary_3, typeof(TJson), typeof(TValue), importerFunc_13);
		}

		public static void UnregisterExporters()
		{
			idictionary_1.Clear();
		}

		public static void UnregisterImporters()
		{
			idictionary_3.Clear();
		}
	}
}
