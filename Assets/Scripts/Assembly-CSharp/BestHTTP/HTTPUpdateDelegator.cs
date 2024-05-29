using BestHTTP.Caching;
using BestHTTP.Cookies;
using UnityEngine;

namespace BestHTTP
{
	internal sealed class HTTPUpdateDelegator : MonoBehaviour
	{
		private static HTTPUpdateDelegator httpupdateDelegator_0;

		private static bool bool_0;

		public static void CheckInstance()
		{
			try
			{
				if (!bool_0)
				{
					httpupdateDelegator_0 = Object.FindObjectOfType(typeof(HTTPUpdateDelegator)) as HTTPUpdateDelegator;
					if (httpupdateDelegator_0 == null)
					{
						GameObject gameObject = new GameObject("HTTP Update Delegator");
						gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
						Object.DontDestroyOnLoad(gameObject);
						httpupdateDelegator_0 = gameObject.AddComponent<HTTPUpdateDelegator>();
					}
					bool_0 = true;
				}
			}
			catch
			{
				HTTPManager.ILogger_0.Error("HTTPUpdateDelegator", "Please call the BestHTTP.HTTPManager.Setup() from one of Unity's event(eg. awake, start) before you send any request!");
			}
		}

		private void Awake()
		{
			HTTPCacheService.SetupCacheFolder();
			CookieJar.SetupFolder();
			CookieJar.Load();
		}

		private void Update()
		{
			HTTPManager.OnUpdate();
		}

		private void OnDestroy()
		{
			bool_0 = false;
		}

		private void OnApplicationQuit()
		{
			HTTPManager.OnQuit();
		}
	}
}
