using System;

namespace engine.data
{
	public class HashUniqueCallbackIndex<K, T> : HashUniqueIndex<K, T> where T : class
	{
		private Func<T, HashUniqueIndex<K, T>, K> _indexFieldCallback;

		public HashUniqueCallbackIndex(Func<T, HashUniqueIndex<K, T>, K> func_0)
		{
			if (func_0 == null)
			{
				throw new ArgumentException("HashUniqueCallbackIndex|Ctor. Index field callback must be not null!");
			}
			_indexFieldCallback = func_0;
		}

		public override K GetIndexField(T gparam_0, HashUniqueIndex<K, T> hashUniqueIndex_0)
		{
			return _indexFieldCallback(gparam_0, hashUniqueIndex_0);
		}
	}
}
