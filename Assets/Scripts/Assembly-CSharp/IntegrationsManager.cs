using System;
using System.Collections.Generic;
using engine.integrations;

public sealed class IntegrationsManager : IIntegrationCallback
{
	private List<Type> list_0 = new List<Type>
	{
		typeof(GameStatIntegration),
		typeof(PhotonNetworkStatIntegration),
		typeof(GameFPSStatIntegration)
	};

	private static IntegrationsManager integrationsManager_0;

	private Dictionary<IntegrationEventType, HashSet<Action<IIntegration, IntegrationEvent>>> dictionary_0 = new Dictionary<IntegrationEventType, HashSet<Action<IIntegration, IntegrationEvent>>>();

	public static IntegrationsManager IntegrationsManager_0
	{
		get
		{
			if (integrationsManager_0 == null)
			{
				integrationsManager_0 = new IntegrationsManager();
			}
			return integrationsManager_0;
		}
	}

	private IntegrationsManager()
	{
	}

	public void Init()
	{
		IntegrationManager.IntegrationManager_0.Init(list_0, this);
	}

	public void AddListener(IntegrationEventType integrationEventType_0, Action<IIntegration, IntegrationEvent> action_0)
	{
		if (!dictionary_0.ContainsKey(integrationEventType_0))
		{
			dictionary_0[integrationEventType_0] = new HashSet<Action<IIntegration, IntegrationEvent>>();
		}
		dictionary_0[integrationEventType_0].Add(action_0);
	}

	public void RemoveListener(IntegrationEventType integrationEventType_0, Action<IIntegration, IntegrationEvent> action_0)
	{
		if (dictionary_0.ContainsKey(integrationEventType_0))
		{
			dictionary_0[integrationEventType_0].Remove(action_0);
		}
	}

	public void SendEvent(IntegrationEvent integrationEvent_0)
	{
		IntegrationManager.IntegrationManager_0.Event(integrationEvent_0);
	}

	public override bool OnIntegrationEvent(IIntegration iintegration_0, IntegrationEvent integrationEvent_0)
	{
		if (dictionary_0.ContainsKey(integrationEvent_0.IntegrationEventType_0))
		{
			HashSet<Action<IIntegration, IntegrationEvent>> hashSet = dictionary_0[integrationEvent_0.IntegrationEventType_0];
			foreach (Action<IIntegration, IntegrationEvent> item in hashSet)
			{
				item(iintegration_0, integrationEvent_0);
			}
		}
		return true;
	}

	public override bool IsIntegrationActive(IIntegration iintegration_0)
	{
		return true;
	}
}
