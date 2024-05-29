namespace engine.data
{
	public abstract class AbstractUniqueIndex<K, T> : AbstractStorageIndex<T> where T : class
	{
		public bool Boolean_0 { get; set; }

		public K Prop_0 { get; set; }

		public abstract T Search(K gparam_0);
	}
}
