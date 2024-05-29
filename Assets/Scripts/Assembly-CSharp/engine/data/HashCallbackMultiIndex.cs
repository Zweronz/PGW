using System;
using System.Collections.Generic;

namespace engine.data
{
	public class HashCallbackMultiIndex<K, T> : HashMultiIndex<K, T> where T : class
	{
		private Func<T, HashSet<K>> _indexFieldCallback;

		public HashCallbackMultiIndex(Func<T, HashSet<K>> func_0)
		{
			if (func_0 == null)
			{
				throw new ArgumentException("HashCallbackMultiIndex|Ctor. Index field callback must be not null!");
			}
			_indexFieldCallback = func_0;
		}

		public override HashSet<K> GetIndexSet(T gparam_0)
		{
			return _indexFieldCallback(gparam_0);
		}
	}
}
