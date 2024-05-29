using System;
using System.Collections;
using System.Collections.Generic;

namespace Rilisoft
{
	internal abstract class Preferences : IEnumerable, ICollection<KeyValuePair<string, string>>, IDictionary<string, string>, IEnumerable<KeyValuePair<string, string>>
	{
		public abstract ICollection<string> Keys { get; }

		public abstract ICollection<string> Values { get; }

		public string this[string key]
		{
			get
			{
				string value;
				if (!TryGetValue(key, out value))
				{
					throw new KeyNotFoundException();
				}
				return value;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				AddCore(key, value);
			}
		}

		public abstract int Count { get; }

		public abstract bool IsReadOnly { get; }

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		protected abstract void AddCore(string string_0, string string_1);

		protected abstract bool ContainsKeyCore(string string_0);

		protected abstract void CopyToCore(KeyValuePair<string, string>[] keyValuePair_0, int int_0);

		protected abstract bool RemoveCore(string string_0);

		protected abstract bool TryGetValueCore(string string_0, out string string_1);

		public abstract void Save();

		public void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			AddCore(key, value);
		}

		public bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return ContainsKeyCore(key);
		}

		public bool Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return RemoveCore(key);
		}

		public bool TryGetValue(string key, out string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return TryGetValueCore(key, out value);
		}

		public void Add(KeyValuePair<string, string> item)
		{
			AddCore(item.Key, item.Value);
		}

		public abstract void Clear();

		public bool Contains(KeyValuePair<string, string> item)
		{
			if (item.Key == null)
			{
				throw new ArgumentException("Key is null.", "item");
			}
			string string_;
			if (!TryGetValueCore(item.Key, out string_))
			{
				return false;
			}
			return EqualityComparer<string>.Default.Equals(item.Value, string_);
		}

		public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (arrayIndex > array.Length)
			{
				throw new ArgumentException("Index larger than largest valid index of array.");
			}
			if (array.Length - arrayIndex < Count)
			{
				throw new ArgumentException("Destination array cannot hold the requested elements!");
			}
			CopyToCore(array, arrayIndex);
		}

		public bool Remove(KeyValuePair<string, string> item)
		{
			if (!Contains(item))
			{
				return false;
			}
			return Remove(item.Key);
		}

		public abstract IEnumerator<KeyValuePair<string, string>> GetEnumerator();
	}
}
