using System.Collections.Generic;
using engine.helpers;
using engine.integrations;
using engine.network;

public sealed class GameStatIntegration : IIntegration
{
	private HashSet<IntegrationEventType> hashSet_0 = new HashSet<IntegrationEventType> { IntegrationEventType.DEVICE_STAT };

	public override HashSet<IntegrationEventType> ProcessingEvents()
	{
		return hashSet_0;
	}

	public override bool Init()
	{
		return true;
	}

	public override void OnEvent(IntegrationEvent integrationEvent_0)
	{
		IntegrationEventType integrationEventType_ = integrationEvent_0.IntegrationEventType_0;
		if (integrationEventType_ == IntegrationEventType.DEVICE_STAT)
		{
			OnDeviceStat(integrationEvent_0);
		}
	}

	private void OnDeviceStat(IntegrationEvent integrationEvent_0)
	{
		IntegrationEventDeviceStat integrationEventDeviceStat = integrationEvent_0 as IntegrationEventDeviceStat;
		if (integrationEventDeviceStat != null && integrationEventDeviceStat.Boolean_3)
		{
			Log.AddLine(string.Format("GameStatIntegration::OnEvent > DEVICE_STAT: {0} {1}", integrationEventDeviceStat.String_0, integrationEventDeviceStat.String_1));
			DeviceStatNetworkCommand deviceStatNetworkCommand = new DeviceStatNetworkCommand();
			deviceStatNetworkCommand.string_0 = integrationEventDeviceStat.String_0;
			deviceStatNetworkCommand.string_1 = integrationEventDeviceStat.String_1;
			deviceStatNetworkCommand.int_1 = integrationEventDeviceStat.Int32_0;
			deviceStatNetworkCommand.string_2 = integrationEventDeviceStat.String_2;
			deviceStatNetworkCommand.string_3 = integrationEventDeviceStat.String_3;
			AbstractNetworkCommand.Send(deviceStatNetworkCommand);
		}
	}

	public override void OnApplicationQuit()
	{
	}

	public override void OnApplicationPause(bool bool_0)
	{
	}

	public override void OnApplicationFocus(bool bool_0)
	{
	}
}
