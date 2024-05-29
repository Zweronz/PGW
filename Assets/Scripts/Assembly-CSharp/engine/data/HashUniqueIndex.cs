using System.Collections;
using System.Collections.Generic;

namespace engine.data
{
	public abstract class HashUniqueIndex<K, T> : AbstractUniqueIndex<K, T> where T : class
	{
		private Dictionary<K, T> _index = new Dictionary<K, T>();

		public override void AddObject(T gparam_0)
		{
			K indexField = GetIndexField(gparam_0, this);
			if (!base.Boolean_0 || !indexField.Equals(base.Prop_0))
			{
				_index[indexField] = gparam_0;
			}
		}

		public override void RemoveObject(T gparam_0)
		{
			IDictionaryEnumerator dictionaryEnumerator = _index.GetEnumerator();
			do
			{
				if (!dictionaryEnumerator.MoveNext())
				{
					return;
				}
			}
			while (dictionaryEnumerator.Value != gparam_0);
			_index.Remove((K)dictionaryEnumerator.Key);
		}

		public override void UpdateObject(T gparam_0)
		{
			K indexField = GetIndexField(gparam_0, this);
			if (!_index.ContainsKey(indexField) || _index.Values != gparam_0)
			{
				RemoveObject(gparam_0);
				AddObject(gparam_0);
			}
		}

		public abstract K GetIndexField(T gparam_0, HashUniqueIndex<K, T> hashUniqueIndex_0);

		public override T Search(K gparam_0)
		{
			if (!_index.ContainsKey(gparam_0))
			{
				return (T)null;
			}
			return _index[gparam_0];
		}

		public override void Clear()
		{
			_index.Clear();
		}
	}
}
