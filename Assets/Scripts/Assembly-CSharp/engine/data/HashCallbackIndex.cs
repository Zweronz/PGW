using System;

namespace engine.data
{
	public class HashCallbackIndex<K, T> : HashIndex<K, T> where T : class
	{
		private Func<T, HashIndex<K, T>, K> _indexFieldCallback;

		public HashCallbackIndex(Func<T, HashIndex<K, T>, K> func_0)
		{
			if (func_0 == null)
			{
				throw new ArgumentException("HashCallbackIndex|Ctor. Index field callback must be not null!");
			}
			_indexFieldCallback = func_0;
		}

		public override K GetIndexField(T gparam_0, HashIndex<K, T> hashIndex_0)
		{
			return _indexFieldCallback(gparam_0, hashIndex_0);
		}
	}
}
