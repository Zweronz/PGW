using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ProtoBuf;

namespace engine.data
{
	[Obfuscation(Exclude = true)]
	[ProtoContract(IgnoreListHandling = true)]
	public class StorageData<K, T> : IEnumerable, IEnumerable<KeyValuePair<K, T>> where K : IComparable where T : class
	{
		[ProtoMember(1)]
		protected Dictionary<K, T> _objects = new Dictionary<K, T>();

		protected List<AbstractStorageIndex<T>> _indexes = new List<AbstractStorageIndex<T>>();

		IEnumerator<KeyValuePair<K, T>> IEnumerable<KeyValuePair<K, T>>.GetEnumerator()
		{
			Dictionary<K, T>.Enumerator enumerator = _objects.GetEnumerator();
			/*Error near IL_0039: Could not find block for branch target IL_0047*/;
			yield break;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<K, T>>)this).GetEnumerator();
		}

		public T GetObjectByKey(K key)
		{
			T value = (T)null;
			_objects.TryGetValue(key, out value);
			return value;
		}

		public List<T> GetObjects()
		{
			return new List<T>(_objects.Values);
		}

		public void AddIndex(AbstractStorageIndex<T> index)
		{
			_indexes.Add(index);
		}

		public T SearchUnique<Key>(int numberIndex, Key key)
		{
			if (!IsCheckIndex<AbstractUniqueIndex<Key, T>>(numberIndex))
			{
				return (T)null;
			}
			AbstractUniqueIndex<Key, T> abstractUniqueIndex = _indexes[numberIndex] as AbstractUniqueIndex<Key, T>;
			return abstractUniqueIndex.Search(key);
		}

		public List<T> Search<Key>(int numberIndex, Key key)
		{
			if (!IsCheckIndex<AbstractIndex<Key, T>>(numberIndex))
			{
				return null;
			}
			AbstractIndex<Key, T> abstractIndex = _indexes[numberIndex] as AbstractIndex<Key, T>;
			return abstractIndex.Search(key);
		}

		private bool IsCheckIndex<IndexType>(int numberIndex)
		{
			return _indexes.Count > 0 && numberIndex >= 0 && numberIndex < _indexes.Count && _indexes[numberIndex] is IndexType;
		}

		protected void InitIndexes()
		{
			if (_indexes.Count == 0)
			{
				return;
			}
			IDictionaryEnumerator dictionaryEnumerator = _objects.GetEnumerator();
			while (dictionaryEnumerator.MoveNext())
			{
				T gparam_ = dictionaryEnumerator.Value as T;
				for (int i = 0; i < _indexes.Count; i++)
				{
					_indexes[i].AddObject(gparam_);
				}
			}
		}
	}
}
