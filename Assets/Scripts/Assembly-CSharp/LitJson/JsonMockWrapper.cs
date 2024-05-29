using System;
using System.Collections;

namespace LitJson
{
	public class JsonMockWrapper : IDictionary, IList, ICollection, IEnumerable, IOrderedDictionary, IJsonWrapper
	{
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		object IList.this[int index]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		int ICollection.Count
		{
			get
			{
				return 0;
			}
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		ICollection IDictionary.Keys
		{
			get
			{
				return null;
			}
		}

		ICollection IDictionary.Values
		{
			get
			{
				return null;
			}
		}

		object IDictionary.this[object key]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		object IOrderedDictionary.this[int idx]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public bool Boolean_0
		{
			get
			{
				return false;
			}
		}

		public bool Boolean_1
		{
			get
			{
				return false;
			}
		}

		public bool Boolean_2
		{
			get
			{
				return false;
			}
		}

		public bool Boolean_3
		{
			get
			{
				return false;
			}
		}

		public bool Boolean_4
		{
			get
			{
				return false;
			}
		}

		public bool Boolean_5
		{
			get
			{
				return false;
			}
		}

		public bool Boolean_6
		{
			get
			{
				return false;
			}
		}

		int IList.Add(object value)
		{
			return 0;
		}

		void IList.Clear()
		{
		}

		bool IList.Contains(object value)
		{
			return false;
		}

		int IList.IndexOf(object value)
		{
			return -1;
		}

		void IList.Insert(int index, object value)
		{
		}

		void IList.Remove(object value)
		{
		}

		void IList.RemoveAt(int index)
		{
		}

		void ICollection.CopyTo(Array array, int index)
		{
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}

		void IDictionary.Add(object key, object value)
		{
		}

		void IDictionary.Clear()
		{
		}

		bool IDictionary.Contains(object key)
		{
			return false;
		}

		void IDictionary.Remove(object key)
		{
		}

		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return null;
		}

		IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
		{
			return null;
		}

		void IOrderedDictionary.Insert(int index, object key, object value)
		{
		}

		void IOrderedDictionary.RemoveAt(int index)
		{
		}

		public bool GetBoolean()
		{
			return false;
		}

		public double GetDouble()
		{
			return 0.0;
		}

		public int GetInt()
		{
			return 0;
		}

		public JsonType GetJsonType()
		{
			return JsonType.None;
		}

		public long GetLong()
		{
			return 0L;
		}

		public string GetString()
		{
			return string.Empty;
		}

		public void SetBoolean(bool val)
		{
		}

		public void SetDouble(double val)
		{
		}

		public void SetInt(int val)
		{
		}

		public void SetJsonType(JsonType type)
		{
		}

		public void SetLong(long val)
		{
		}

		public void SetString(string val)
		{
		}

		public string ToJson()
		{
			return string.Empty;
		}

		public void ToJson(JsonWriter writer)
		{
		}
	}
}
