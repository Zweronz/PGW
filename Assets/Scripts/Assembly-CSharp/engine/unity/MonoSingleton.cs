using UnityEngine;

namespace engine.unity
{
	public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
	{
		private static T _instance;

		public static T Prop_0
		{
			get
			{
				if ((Object)_instance == (Object)null)
				{
					_instance = Object.FindObjectOfType(typeof(T)) as T;
					if ((Object)_instance == (Object)null)
					{
						Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");
						_instance = new GameObject("Temp Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
						if ((Object)_instance == (Object)null)
						{
							Debug.LogError("Problem during the creation of " + typeof(T).ToString());
						}
					}
					_instance.Init();
				}
				return _instance;
			}
		}

		private void Awake()
		{
			if ((Object)_instance == (Object)null)
			{
				_instance = this as T;
				_instance.Init();
			}
			else
			{
				DublicateInstance();
			}
		}

		protected virtual void Init()
		{
		}

		protected virtual void DublicateInstance()
		{
		}

		private void OnApplicationQuit()
		{
			_instance = (T)null;
		}

		private void OnDestroy()
		{
			_instance = (T)null;
		}
	}
}
