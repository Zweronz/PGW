using System;
using System.Collections.Generic;
using System.Reflection;

namespace engine.data
{
	[Obfuscation(Exclude = true)]
	public class UserStorage<K, T> : StorageData<K, T> where K : IComparable where T : class
	{
		public T AddObject(K key, T item)
		{
			if (item == null)
			{
				return (T)null;
			}
			_objects.Add(key, item);
			_indexes.ForEach(delegate(AbstractStorageIndex<T> abstractStorageIndex_0)
			{
				abstractStorageIndex_0.AddObject(item);
			});
			return item;
		}

		public T RemoveObject(K key)
		{
			T item = (T)null;
			if (!_objects.TryGetValue(key, out item))
			{
				return (T)null;
			}
			_objects.Remove(key);
			_indexes.ForEach(delegate(AbstractStorageIndex<T> abstractStorageIndex_0)
			{
				abstractStorageIndex_0.RemoveObject(item);
			});
			return item;
		}

		public void UpdateObject(K key, T item)
		{
			RemoveObject(key);
			AddObject(key, item);
		}

		public new T GetObjectByKey(K key)
		{
			T value = (T)null;
			_objects.TryGetValue(key, out value);
			return value;
		}

		public void Clear()
		{
			_objects.Clear();
			_indexes.ForEach(delegate(AbstractStorageIndex<T> index)
			{
				index.Clear();
			});
		}

		public void InitUserStorage(Dictionary<K, T> data)
		{
			data = ((data != null) ? data : new Dictionary<K, T>());
			Clear();
			_objects = data;
			InitIndexes();
		}
	}
}
