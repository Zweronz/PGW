namespace engine.data
{
	public abstract class AbstractStorageIndex<T> where T : class
	{
		public abstract void AddObject(T gparam_0);

		public abstract void RemoveObject(T gparam_0);

		public abstract void UpdateObject(T gparam_0);

		public abstract void Clear();
	}
}
