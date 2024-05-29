using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace LitJson
{
	public class JsonData : IEquatable<JsonData>, IDictionary, IList, ICollection, IEnumerable, IOrderedDictionary, IJsonWrapper
	{
		private IList<JsonData> ilist_0;

		private bool bool_0;

		private double double_0;

		private int int_0;

		private long long_0;

		private IDictionary<string, JsonData> idictionary_0;

		private string string_0;

		private string string_1;

		private JsonType jsonType_0;

		private IList<KeyValuePair<string, JsonData>> ilist_1;

		int ICollection.Count
		{
			get
			{
				return Int32_0;
			}
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return EnsureCollection().IsSynchronized;
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				return EnsureCollection().SyncRoot;
			}
		}

		bool IDictionary.IsFixedSize
		{
			get
			{
				return EnsureDictionary().IsFixedSize;
			}
		}

		bool IDictionary.IsReadOnly
		{
			get
			{
				return EnsureDictionary().IsReadOnly;
			}
		}

		ICollection IDictionary.Keys
		{
			get
			{
				EnsureDictionary();
				IList<string> list = new List<string>();
				foreach (KeyValuePair<string, JsonData> item in ilist_1)
				{
					list.Add(item.Key);
				}
				return (ICollection)list;
			}
		}

		ICollection IDictionary.Values
		{
			get
			{
				EnsureDictionary();
				IList<JsonData> list = new List<JsonData>();
				foreach (KeyValuePair<string, JsonData> item in ilist_1)
				{
					list.Add(item.Value);
				}
				return (ICollection)list;
			}
		}

		bool IJsonWrapper.Boolean_0
		{
			get
			{
				return Boolean_7;
			}
		}

		bool IJsonWrapper.Boolean_1
		{
			get
			{
				return Boolean_8;
			}
		}

		bool IJsonWrapper.Boolean_2
		{
			get
			{
				return Boolean_9;
			}
		}

		bool IJsonWrapper.Boolean_3
		{
			get
			{
				return Boolean_10;
			}
		}

		bool IJsonWrapper.Boolean_4
		{
			get
			{
				return Boolean_11;
			}
		}

		bool IJsonWrapper.Boolean_5
		{
			get
			{
				return Boolean_12;
			}
		}

		bool IJsonWrapper.Boolean_6
		{
			get
			{
				return Boolean_13;
			}
		}

		bool IList.IsFixedSize
		{
			get
			{
				return EnsureList().IsFixedSize;
			}
		}

		bool IList.IsReadOnly
		{
			get
			{
				return EnsureList().IsReadOnly;
			}
		}

		object IDictionary.this[object key]
		{
			get
			{
				return EnsureDictionary()[key];
			}
			set
			{
				if (!(key is string))
				{
					throw new ArgumentException("The key has to be a string");
				}
				JsonData value2 = ToJsonData(value);
				this[(string)key] = value2;
			}
		}

		object IOrderedDictionary.this[int idx]
		{
			get
			{
				EnsureDictionary();
				return ilist_1[idx].Value;
			}
			set
			{
				EnsureDictionary();
				JsonData value2 = ToJsonData(value);
				KeyValuePair<string, JsonData> keyValuePair = ilist_1[idx];
				idictionary_0[keyValuePair.Key] = value2;
				KeyValuePair<string, JsonData> value3 = new KeyValuePair<string, JsonData>(keyValuePair.Key, value2);
				ilist_1[idx] = value3;
			}
		}

		object IList.this[int index]
		{
			get
			{
				return EnsureList()[index];
			}
			set
			{
				EnsureList();
				JsonData value2 = ToJsonData(value);
				this[index] = value2;
			}
		}

		public int Int32_0
		{
			get
			{
				return EnsureCollection().Count;
			}
		}

		public bool Boolean_7
		{
			get
			{
				return jsonType_0 == JsonType.Array;
			}
		}

		public bool Boolean_8
		{
			get
			{
				return jsonType_0 == JsonType.Boolean;
			}
		}

		public bool Boolean_9
		{
			get
			{
				return jsonType_0 == JsonType.Double;
			}
		}

		public bool Boolean_10
		{
			get
			{
				return jsonType_0 == JsonType.Int;
			}
		}

		public bool Boolean_11
		{
			get
			{
				return jsonType_0 == JsonType.Long;
			}
		}

		public bool Boolean_12
		{
			get
			{
				return jsonType_0 == JsonType.Object;
			}
		}

		public bool Boolean_13
		{
			get
			{
				return jsonType_0 == JsonType.String;
			}
		}

		public ICollection<string> ICollection_0
		{
			get
			{
				EnsureDictionary();
				return idictionary_0.Keys;
			}
		}

		public JsonData this[string prop_name]
		{
			get
			{
				EnsureDictionary();
				return idictionary_0[prop_name];
			}
			set
			{
				EnsureDictionary();
				KeyValuePair<string, JsonData> keyValuePair = new KeyValuePair<string, JsonData>(prop_name, value);
				if (idictionary_0.ContainsKey(prop_name))
				{
					for (int i = 0; i < ilist_1.Count; i++)
					{
						if (ilist_1[i].Key == prop_name)
						{
							ilist_1[i] = keyValuePair;
							break;
						}
					}
				}
				else
				{
					ilist_1.Add(keyValuePair);
				}
				idictionary_0[prop_name] = value;
				string_1 = null;
			}
		}

		public JsonData this[int index]
		{
			get
			{
				EnsureCollection();
				if (jsonType_0 == JsonType.Array)
				{
					return ilist_0[index];
				}
				return ilist_1[index].Value;
			}
			set
			{
				EnsureCollection();
				if (jsonType_0 == JsonType.Array)
				{
					ilist_0[index] = value;
				}
				else
				{
					KeyValuePair<string, JsonData> keyValuePair = ilist_1[index];
					KeyValuePair<string, JsonData> value2 = new KeyValuePair<string, JsonData>(keyValuePair.Key, value);
					ilist_1[index] = value2;
					idictionary_0[keyValuePair.Key] = value;
				}
				string_1 = null;
			}
		}

		public JsonData()
		{
		}

		public JsonData(bool bool_1)
		{
			jsonType_0 = JsonType.Boolean;
			bool_0 = bool_1;
		}

		public JsonData(double double_1)
		{
			jsonType_0 = JsonType.Double;
			double_0 = double_1;
		}

		public JsonData(int int_1)
		{
			jsonType_0 = JsonType.Int;
			int_0 = int_1;
		}

		public JsonData(long long_1)
		{
			jsonType_0 = JsonType.Long;
			long_0 = long_1;
		}

		public JsonData(object object_0)
		{
			if (object_0 is bool)
			{
				jsonType_0 = JsonType.Boolean;
				bool_0 = (bool)object_0;
				return;
			}
			if (object_0 is double)
			{
				jsonType_0 = JsonType.Double;
				double_0 = (double)object_0;
				return;
			}
			if (object_0 is int)
			{
				jsonType_0 = JsonType.Int;
				int_0 = (int)object_0;
				return;
			}
			if (object_0 is long)
			{
				jsonType_0 = JsonType.Long;
				long_0 = (long)object_0;
				return;
			}
			if (!(object_0 is string))
			{
				throw new ArgumentException("Unable to wrap the given object with JsonData");
			}
			jsonType_0 = JsonType.String;
			string_0 = (string)object_0;
		}

		public JsonData(string string_2)
		{
			jsonType_0 = JsonType.String;
			string_0 = string_2;
		}

		void ICollection.CopyTo(Array array, int index)
		{
			EnsureCollection().CopyTo(array, index);
		}

		void IDictionary.Add(object key, object value)
		{
			JsonData value2 = ToJsonData(value);
			EnsureDictionary().Add(key, value2);
			KeyValuePair<string, JsonData> item = new KeyValuePair<string, JsonData>((string)key, value2);
			ilist_1.Add(item);
			string_1 = null;
		}

		void IDictionary.Clear()
		{
			EnsureDictionary().Clear();
			ilist_1.Clear();
			string_1 = null;
		}

		bool IDictionary.Contains(object key)
		{
			return EnsureDictionary().Contains(key);
		}

		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return ((IOrderedDictionary)this).GetEnumerator();
		}

		void IDictionary.Remove(object key)
		{
			EnsureDictionary().Remove(key);
			for (int i = 0; i < ilist_1.Count; i++)
			{
				if (ilist_1[i].Key == (string)key)
				{
					ilist_1.RemoveAt(i);
					break;
				}
			}
			string_1 = null;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return EnsureCollection().GetEnumerator();
		}

		bool IJsonWrapper.GetBoolean()
		{
			if (jsonType_0 != JsonType.Boolean)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a boolean");
			}
			return bool_0;
		}

		double IJsonWrapper.GetDouble()
		{
			if (jsonType_0 != JsonType.Double)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a double");
			}
			return double_0;
		}

		int IJsonWrapper.GetInt()
		{
			if (jsonType_0 != JsonType.Int)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold an int");
			}
			return int_0;
		}

		long IJsonWrapper.GetLong()
		{
			if (jsonType_0 != JsonType.Long)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a long");
			}
			return long_0;
		}

		string IJsonWrapper.GetString()
		{
			if (jsonType_0 != JsonType.String)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a string");
			}
			return string_0;
		}

		void IJsonWrapper.SetBoolean(bool val)
		{
			jsonType_0 = JsonType.Boolean;
			bool_0 = val;
			string_1 = null;
		}

		void IJsonWrapper.SetDouble(double val)
		{
			jsonType_0 = JsonType.Double;
			double_0 = val;
			string_1 = null;
		}

		void IJsonWrapper.SetInt(int val)
		{
			jsonType_0 = JsonType.Int;
			int_0 = val;
			string_1 = null;
		}

		void IJsonWrapper.SetLong(long val)
		{
			jsonType_0 = JsonType.Long;
			long_0 = val;
			string_1 = null;
		}

		void IJsonWrapper.SetString(string val)
		{
			jsonType_0 = JsonType.String;
			string_0 = val;
			string_1 = null;
		}

		string IJsonWrapper.ToJson()
		{
			return ToJson();
		}

		void IJsonWrapper.ToJson(JsonWriter writer)
		{
			ToJson(writer);
		}

		int IList.Add(object value)
		{
			return Add(value);
		}

		void IList.Clear()
		{
			EnsureList().Clear();
			string_1 = null;
		}

		bool IList.Contains(object value)
		{
			return EnsureList().Contains(value);
		}

		int IList.IndexOf(object value)
		{
			return EnsureList().IndexOf(value);
		}

		void IList.Insert(int index, object value)
		{
			EnsureList().Insert(index, value);
			string_1 = null;
		}

		void IList.Remove(object value)
		{
			EnsureList().Remove(value);
			string_1 = null;
		}

		void IList.RemoveAt(int index)
		{
			EnsureList().RemoveAt(index);
			string_1 = null;
		}

		IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
		{
			EnsureDictionary();
			return new OrderedDictionaryEnumerator(ilist_1.GetEnumerator());
		}

		void IOrderedDictionary.Insert(int index, object key, object value)
		{
			string text = (string)key;
			JsonData value2 = (this[text] = ToJsonData(value));
			KeyValuePair<string, JsonData> item = new KeyValuePair<string, JsonData>(text, value2);
			ilist_1.Insert(index, item);
		}

		void IOrderedDictionary.RemoveAt(int index)
		{
			EnsureDictionary();
			idictionary_0.Remove(ilist_1[index].Key);
			ilist_1.RemoveAt(index);
		}

		private ICollection EnsureCollection()
		{
			if (jsonType_0 == JsonType.Array)
			{
				return (ICollection)ilist_0;
			}
			if (jsonType_0 != JsonType.Object)
			{
				throw new InvalidOperationException("The JsonData instance has to be initialized first");
			}
			return (ICollection)idictionary_0;
		}

		private IDictionary EnsureDictionary()
		{
			if (jsonType_0 == JsonType.Object)
			{
				return (IDictionary)idictionary_0;
			}
			if (jsonType_0 != 0)
			{
				throw new InvalidOperationException("Instance of JsonData is not a dictionary");
			}
			jsonType_0 = JsonType.Object;
			idictionary_0 = new Dictionary<string, JsonData>();
			ilist_1 = new List<KeyValuePair<string, JsonData>>();
			return (IDictionary)idictionary_0;
		}

		private IList EnsureList()
		{
			if (jsonType_0 == JsonType.Array)
			{
				return (IList)ilist_0;
			}
			if (jsonType_0 != 0)
			{
				throw new InvalidOperationException("Instance of JsonData is not a list");
			}
			jsonType_0 = JsonType.Array;
			ilist_0 = new List<JsonData>();
			return (IList)ilist_0;
		}

		private JsonData ToJsonData(object object_0)
		{
			if (object_0 == null)
			{
				return null;
			}
			if (object_0 is JsonData)
			{
				return (JsonData)object_0;
			}
			return new JsonData(object_0);
		}

		private static void WriteJson(IJsonWrapper ijsonWrapper_0, JsonWriter jsonWriter_0)
		{
			if (ijsonWrapper_0 == null)
			{
				jsonWriter_0.Write(null);
			}
			else if (ijsonWrapper_0.Boolean_6)
			{
				jsonWriter_0.Write(ijsonWrapper_0.GetString());
			}
			else if (ijsonWrapper_0.Boolean_1)
			{
				jsonWriter_0.Write(ijsonWrapper_0.GetBoolean());
			}
			else if (ijsonWrapper_0.Boolean_2)
			{
				jsonWriter_0.Write(ijsonWrapper_0.GetDouble());
			}
			else if (ijsonWrapper_0.Boolean_3)
			{
				jsonWriter_0.Write(ijsonWrapper_0.GetInt());
			}
			else if (ijsonWrapper_0.Boolean_4)
			{
				jsonWriter_0.Write(ijsonWrapper_0.GetLong());
			}
			else if (ijsonWrapper_0.Boolean_0)
			{
				jsonWriter_0.WriteArrayStart();
				foreach (object item in (IEnumerable)ijsonWrapper_0)
				{
					WriteJson((JsonData)item, jsonWriter_0);
				}
				jsonWriter_0.WriteArrayEnd();
			}
			else
			{
				if (!ijsonWrapper_0.Boolean_5)
				{
					return;
				}
				jsonWriter_0.WriteObjectStart();
				foreach (DictionaryEntry item2 in (IDictionary)ijsonWrapper_0)
				{
					jsonWriter_0.WritePropertyName((string)item2.Key);
					WriteJson((JsonData)item2.Value, jsonWriter_0);
				}
				jsonWriter_0.WriteObjectEnd();
			}
		}

		public int Add(object object_0)
		{
			JsonData value = ToJsonData(object_0);
			string_1 = null;
			return EnsureList().Add(value);
		}

		public void Clear()
		{
			if (Boolean_12)
			{
				((IDictionary)this).Clear();
			}
			else if (Boolean_7)
			{
				((IList)this).Clear();
			}
		}

		public bool Equals(JsonData other)
		{
			if (other == null)
			{
				return false;
			}
			if (other.jsonType_0 != jsonType_0)
			{
				return false;
			}
			switch (jsonType_0)
			{
			default:
				return false;
			case JsonType.None:
				return true;
			case JsonType.Object:
				return idictionary_0.Equals(other.idictionary_0);
			case JsonType.Array:
				return ilist_0.Equals(other.ilist_0);
			case JsonType.String:
				return string_0.Equals(other.string_0);
			case JsonType.Int:
				return int_0.Equals(other.int_0);
			case JsonType.Long:
				return long_0.Equals(other.long_0);
			case JsonType.Double:
				return double_0.Equals(other.double_0);
			case JsonType.Boolean:
				return bool_0.Equals(other.bool_0);
			}
		}

		public JsonType GetJsonType()
		{
			return jsonType_0;
		}

		public void SetJsonType(JsonType type)
		{
			if (jsonType_0 != type)
			{
				switch (type)
				{
				case JsonType.Object:
					idictionary_0 = new Dictionary<string, JsonData>();
					ilist_1 = new List<KeyValuePair<string, JsonData>>();
					break;
				case JsonType.Array:
					ilist_0 = new List<JsonData>();
					break;
				case JsonType.String:
					string_0 = null;
					break;
				case JsonType.Int:
					int_0 = 0;
					break;
				case JsonType.Long:
					long_0 = 0L;
					break;
				case JsonType.Double:
					double_0 = 0.0;
					break;
				case JsonType.Boolean:
					bool_0 = false;
					break;
				}
				jsonType_0 = type;
			}
		}

		public string ToJson()
		{
			if (string_1 != null)
			{
				return string_1;
			}
			StringWriter stringWriter = new StringWriter();
			JsonWriter jsonWriter = new JsonWriter(stringWriter);
			jsonWriter.Boolean_1 = false;
			WriteJson(this, jsonWriter);
			string_1 = stringWriter.ToString();
			return string_1;
		}

		public void ToJson(JsonWriter jsonWriter_0)
		{
			bool boolean_ = jsonWriter_0.Boolean_1;
			jsonWriter_0.Boolean_1 = false;
			WriteJson(this, jsonWriter_0);
			jsonWriter_0.Boolean_1 = boolean_;
		}

		public override string ToString()
		{
			switch (jsonType_0)
			{
			default:
				return "Uninitialized JsonData";
			case JsonType.Object:
				return "JsonData object";
			case JsonType.Array:
				return "JsonData array";
			case JsonType.String:
				return string_0;
			case JsonType.Int:
				return int_0.ToString();
			case JsonType.Long:
				return long_0.ToString();
			case JsonType.Double:
				return double_0.ToString();
			case JsonType.Boolean:
				return bool_0.ToString();
			}
		}

		public static implicit operator JsonData(bool bool_1)
		{
			return new JsonData(bool_1);
		}

		public static implicit operator JsonData(double double_1)
		{
			return new JsonData(double_1);
		}

		public static implicit operator JsonData(int int_1)
		{
			return new JsonData(int_1);
		}

		public static implicit operator JsonData(long long_1)
		{
			return new JsonData(long_1);
		}

		public static implicit operator JsonData(string string_2)
		{
			return new JsonData(string_2);
		}

		public static explicit operator bool(JsonData jsonData_0)
		{
			if (jsonData_0.jsonType_0 != JsonType.Boolean)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a double");
			}
			return jsonData_0.bool_0;
		}

		public static explicit operator double(JsonData jsonData_0)
		{
			if (jsonData_0.jsonType_0 != JsonType.Double)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a double");
			}
			return jsonData_0.double_0;
		}

		public static explicit operator int(JsonData jsonData_0)
		{
			if (jsonData_0.jsonType_0 != JsonType.Int)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold an int");
			}
			return jsonData_0.int_0;
		}

		public static explicit operator long(JsonData jsonData_0)
		{
			if (jsonData_0.jsonType_0 != JsonType.Long)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold an int");
			}
			return jsonData_0.long_0;
		}

		public static explicit operator string(JsonData jsonData_0)
		{
			if (jsonData_0.jsonType_0 != JsonType.String)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a string");
			}
			return jsonData_0.string_0;
		}
	}
}
