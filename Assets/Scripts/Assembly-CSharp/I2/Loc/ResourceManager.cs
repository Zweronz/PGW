using System;
using System.Collections.Generic;
using UnityEngine;

namespace I2.Loc
{
	public class ResourceManager : MonoBehaviour
	{
		private static ResourceManager resourceManager_0;

		public UnityEngine.Object[] Assets;

		private Dictionary<string, UnityEngine.Object> dictionary_0 = new Dictionary<string, UnityEngine.Object>();

		private bool bool_0;

		public static ResourceManager ResourceManager_0
		{
			get
			{
				if (resourceManager_0 == null)
				{
					resourceManager_0 = (ResourceManager)UnityEngine.Object.FindObjectOfType(typeof(ResourceManager));
				}
				if (resourceManager_0 == null)
				{
					GameObject gameObject = new GameObject("I2ResourceManager", typeof(ResourceManager));
					gameObject.hideFlags |= HideFlags.HideAndDontSave;
					resourceManager_0 = gameObject.GetComponent<ResourceManager>();
				}
				UnityEngine.Object.DontDestroyOnLoad(resourceManager_0.gameObject);
				return resourceManager_0;
			}
		}

		public T GetAsset<T>(string string_0) where T : UnityEngine.Object
		{
			T val = FindAsset(string_0) as T;
			if ((UnityEngine.Object)val != (UnityEngine.Object)null)
			{
				return val;
			}
			return LoadFromResources<T>(string_0);
		}

		private UnityEngine.Object FindAsset(string string_0)
		{
			if (Assets != null)
			{
				int i = 0;
				for (int num = Assets.Length; i < num; i++)
				{
					if (Assets[i] != null && Assets[i].name == string_0)
					{
						return Assets[i];
					}
				}
			}
			return null;
		}

		public bool HasAsset(UnityEngine.Object object_0)
		{
			if (Assets == null)
			{
				return false;
			}
			return Array.IndexOf(Assets, object_0) >= 0;
		}

		public T LoadFromResources<T>(string string_0) where T : UnityEngine.Object
		{
			UnityEngine.Object value;
			if (dictionary_0.TryGetValue(string_0, out value) && value != null)
			{
				return value as T;
			}
			T val = Resources.Load<T>(string_0);
			dictionary_0[string_0] = val;
			if (!bool_0)
			{
				Invoke("CleanResourceCache", 0.1f);
				bool_0 = true;
			}
			return val;
		}

		public void CleanResourceCache()
		{
			dictionary_0.Clear();
			CancelInvoke();
			bool_0 = false;
		}
	}
}
