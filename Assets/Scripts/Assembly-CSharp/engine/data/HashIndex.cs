using System.Collections;
using System.Collections.Generic;

namespace engine.data
{
	public abstract class HashIndex<K, T> : AbstractIndex<K, T> where T : class
	{
		private Dictionary<K, List<T>> _index = new Dictionary<K, List<T>>();

		public override void AddObject(T gparam_0)
		{
			K indexField = GetIndexField(gparam_0, this);
			if (!base.Boolean_0 || !indexField.Equals(base.Prop_0))
			{
				if (!_index.ContainsKey(indexField))
				{
					_index[indexField] = new List<T>();
				}
				_index[indexField].Add(gparam_0);
			}
		}

		public override void UpdateObject(T gparam_0)
		{
			RemoveObject(gparam_0);
			AddObject(gparam_0);
		}

		public override void RemoveObject(T gparam_0)
		{
			K indexField = GetIndexField(gparam_0, this);
			if (_index.ContainsKey(indexField))
			{
				List<T> list = _index[indexField];
				IEnumerator enumerator = list.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == gparam_0)
					{
						list.Remove(enumerator.Current as T);
						if (list.Count == 0)
						{
							_index.Remove(indexField);
						}
						return;
					}
				}
			}
			IDictionaryEnumerator dictionaryEnumerator = _index.GetEnumerator();
			while (dictionaryEnumerator.MoveNext())
			{
				List<T> list2 = dictionaryEnumerator.Value as List<T>;
				IEnumerator enumerator2 = list2.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current == gparam_0)
					{
						list2.Remove(enumerator2.Current as T);
						if (list2.Count == 0)
						{
							_index.Remove((K)dictionaryEnumerator.Key);
						}
						return;
					}
				}
			}
		}

		public void Update(T gparam_0)
		{
			K indexField = GetIndexField(gparam_0, this);
			if (_index.ContainsKey(indexField))
			{
				IEnumerator enumerator = _index[indexField].GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == gparam_0)
					{
						return;
					}
				}
			}
			RemoveObject(gparam_0);
			AddObject(gparam_0);
		}

		public abstract K GetIndexField(T gparam_0, HashIndex<K, T> hashIndex_0);

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
