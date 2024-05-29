using System.Collections;
using System.Collections.Generic;

namespace engine.data
{
	public abstract class HashMultiIndex<K, T> : AbstractIndex<K, T> where T : class
	{
		private Dictionary<K, List<T>> _index = new Dictionary<K, List<T>>();

		public override void AddObject(T gparam_0)
		{
			HashSet<K> indexSet = GetIndexSet(gparam_0);
			if (indexSet == null)
			{
				return;
			}
			IEnumerator enumerator = indexSet.GetEnumerator();
			while (enumerator.MoveNext())
			{
				K key = (K)enumerator.Current;
				if (!_index.ContainsKey(key))
				{
					_index[key] = new List<T>();
				}
				_index[key].Add((T)gparam_0);
			}
		}

		public override void RemoveObject(T gparam_0)
		{
			List<K> list = new List<K>(_index.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				K key = list[i];
				List<T> list2 = _index[key];
				IEnumerator enumerator = list2.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == gparam_0)
					{
						list2.Remove(enumerator.Current as T);
						if (list2.Count == 0)
						{
							_index.Remove(key);
						}
						break;
					}
				}
			}
		}

		public override void UpdateObject(T gparam_0)
		{
			RemoveObject(gparam_0);
			AddObject(gparam_0);
		}

		public abstract HashSet<K> GetIndexSet(T gparam_0);

		public override List<T> Search(K gparam_0)
		{
			if (!_index.ContainsKey(gparam_0))
			{
				return _emptyList;
			}
			return _index[gparam_0];
		}

		public override void Clear()
		{
			_index.Clear();
		}
	}
}
