using System;
using System.Reflection;

namespace engine.data
{
	[Obfuscation(Exclude = true)]
	public abstract class BaseStorage<K, T> where K : IComparable where T : class
	{
		public StorageData<K, T> Storage { get; private set; }

		protected virtual void OnAddIndexes()
		{
		}

		protected virtual void OnCreate()
		{
		}

		protected void InitInstance()
		{
			bool flag = !StorageManager.StorageManager_0.Contains(OnCreate, StorageManager.StatusEvent.LOADING_COMPLETE);
			Storage = StorageManager.StorageManager_0.GetStorageData<StorageData<K, T>>();
			if (Storage == null)
			{
				if (!flag)
				{
					StorageManager.StorageManager_0.Unsubscribe(OnCreate, StorageManager.StatusEvent.LOADING_COMPLETE);
				}
				return;
			}
			if (flag)
			{
				StorageManager.StorageManager_0.Subscribe(OnCreate, StorageManager.StatusEvent.LOADING_COMPLETE);
			}
			OnAddIndexes();
		}
	}
}
