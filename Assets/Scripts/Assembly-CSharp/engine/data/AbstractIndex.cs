using System.Collections.Generic;

namespace engine.data
{
	public abstract class AbstractIndex<K, T> : AbstractStorageIndex<T> where T : class
	{
		protected List<T> _emptyList = new List<T>();

		public bool Boolean_0 { get; set; }

		public K Prop_0 { get; set; }

		public abstract List<T> Search(K gparam_0);
	}
}
