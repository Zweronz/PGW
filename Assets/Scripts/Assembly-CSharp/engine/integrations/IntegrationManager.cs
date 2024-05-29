using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using engine.events;

namespace engine.integrations
{
	public class IntegrationManager
	{
		private static IntegrationManager integrationManager_0;

		private IIntegrationCallback iintegrationCallback_0;

		private Dictionary<IntegrationEventType, HashSet<IIntegration>> dictionary_0 = new Dictionary<IntegrationEventType, HashSet<IIntegration>>();

		private HashSet<IIntegration> hashSet_0 = new HashSet<IIntegration>();

		private List<Type> list_0;

		public static IntegrationManager IntegrationManager_0
		{
			get
			{
				if (integrationManager_0 == null)
				{
					integrationManager_0 = new IntegrationManager();
				}
				return integrationManager_0;
			}
		}

		private IntegrationManager()
		{
		}

		private void Clear()
		{
			DependSceneEvent<ApplicationQuitUnityEvent>.GlobalUnsubscribe(OnApplicationQuitCallback);
			DependSceneEvent<ApplicationPauseUnityEvent, bool>.GlobalUnsubscribe(OnApplicationPauseCallback);
			DependSceneEvent<ApplicationFocusUnityEvent, bool>.GlobalUnsubscribe(OnApplicationFocusCallback);
			dictionary_0.Clear();
			hashSet_0.Clear();
		}

		public void Init(List<Type> list_1, IIntegrationCallback iintegrationCallback_1)
		{
			Clear();
			list_0 = list_1;
			iintegrationCallback_0 = iintegrationCallback_1;
			LoadIntegrations();
			SetIntegrationsCallback(iintegrationCallback_0);
			DependSceneEvent<ApplicationQuitUnityEvent>.GlobalSubscribe(OnApplicationQuitCallback);
			DependSceneEvent<ApplicationPauseUnityEvent, bool>.GlobalSubscribe(OnApplicationPauseCallback);
			DependSceneEvent<ApplicationFocusUnityEvent, bool>.GlobalSubscribe(OnApplicationFocusCallback);
		}

		private void OnApplicationQuitCallback()
		{
			foreach (IIntegration item in hashSet_0)
			{
				item.OnApplicationQuit();
			}
		}

		private void OnApplicationPauseCallback(bool bool_0)
		{
			foreach (IIntegration item in hashSet_0)
			{
				item.OnApplicationPause(bool_0);
			}
		}

		private void OnApplicationFocusCallback(bool bool_0)
		{
			foreach (IIntegration item in hashSet_0)
			{
				item.OnApplicationFocus(bool_0);
			}
		}

		public void Event(IntegrationEvent integrationEvent_0)
		{
			if (!dictionary_0.ContainsKey(integrationEvent_0.IntegrationEventType_0))
			{
				return;
			}
			foreach (IIntegration item in hashSet_0)
			{
				item.OnEvent(integrationEvent_0);
			}
		}

		public HashSet<ISocialNetwork> SocialNetworks()
		{
			HashSet<ISocialNetwork> hashSet = new HashSet<ISocialNetwork>();
			foreach (IIntegration item in hashSet_0)
			{
				if (item.IsSocial())
				{
					hashSet.Add((ISocialNetwork)item);
				}
			}
			return hashSet;
		}

		private void LoadIntegrations()
		{
			for (int i = 0; i < list_0.Count; i++)
			{
				Type type = list_0[i];
				if (type.IsSubclassOf(typeof(IIntegration)))
				{
					ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
					if (constructor == null)
					{
						break;
					}
					IIntegration integration = (IIntegration)constructor.Invoke(new object[0]);
					if (integration != null)
					{
						CheckIntegration(integration);
					}
					else
					{
						Debug.LogWarning(string.Format("Integration type:{0} create fail!!", type.ToString()));
					}
				}
			}
		}

		private void CheckIntegration(IIntegration iintegration_0)
		{
			if (iintegrationCallback_0 != null && !iintegrationCallback_0.IsIntegrationActive(iintegration_0))
			{
				return;
			}
			if (iintegration_0.Init())
			{
				HashSet<IntegrationEventType> hashSet = iintegration_0.ProcessingEvents();
				if (hashSet.Count <= 0)
				{
					return;
				}
				hashSet_0.Add(iintegration_0);
				{
					foreach (IntegrationEventType item in hashSet)
					{
						if (!dictionary_0.ContainsKey(item))
						{
							dictionary_0[item] = new HashSet<IIntegration>();
						}
						dictionary_0[item].Add(iintegration_0);
					}
					return;
				}
			}
			Debug.LogWarning(string.Format("Integration type:{0} init fail!!", iintegration_0.GetType().ToString()));
		}

		private void SetIntegrationsCallback(IIntegrationCallback iintegrationCallback_1)
		{
			foreach (IIntegration item in hashSet_0)
			{
				item.SetCallback(iintegrationCallback_1);
			}
		}
	}
}
