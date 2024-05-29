using System;
using System.Collections.Generic;
using engine.events;

namespace engine.data
{
	public class StorageManager : BaseEvent
	{
		public enum StatusEvent
		{
			LOADING_COMPLETE = 0,
			LOADING_ERROR = 1
		}

		protected static StorageManager storageManager_0;

		private StorageSchemeLoader storageSchemeLoader_0 = new StorageSchemeLoader();

		internal Dictionary<Type, object> dictionary_1 = new Dictionary<Type, object>();

		private Action action_0;

		public static StorageManager StorageManager_0
		{
			get
			{
				if (storageManager_0 == null)
				{
					storageManager_0 = new StorageManager();
				}
				return storageManager_0;
			}
		}

		public void Init(string string_0, string string_1, string string_2, Action action_1)
		{
			dictionary_1.Clear();
			storageSchemeLoader_0.Init(string_0, string_1, string_2, action_1);
		}

		public T GetStorageData<T>() where T : class
		{
			Type typeFromHandle = typeof(T);
			if (!typeFromHandle.IsGenericType)
			{
				return (T)null;
			}
			Type[] genericArguments = typeFromHandle.GetGenericArguments();
			if (genericArguments.Length < 2)
			{
				return (T)null;
			}
			Type key = genericArguments[1];
			if (!dictionary_1.ContainsKey(key))
			{
				return (T)null;
			}
			return dictionary_1[key] as T;
		}
	}
}
