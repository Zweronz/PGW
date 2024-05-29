using UnityEngine;

public sealed class GameStatHelper
{
	private static GameStatHelper gameStatHelper_0;

	public static GameStatHelper GameStatHelper_0
	{
		get
		{
			if (gameStatHelper_0 == null)
			{
				gameStatHelper_0 = new GameStatHelper();
			}
			return gameStatHelper_0;
		}
	}

	private GameStatHelper()
	{
	}

	public void DeviceStat()
	{
		IntegrationEventDeviceStat.IntegrationEventDeviceStat_0.String_0 = SystemInfo.processorType;
		IntegrationEventDeviceStat.IntegrationEventDeviceStat_0.Int32_0 = SystemInfo.systemMemorySize;
		IntegrationEventDeviceStat.IntegrationEventDeviceStat_0.String_1 = string.Format("{0} {1}", SystemInfo.graphicsDeviceName, SystemInfo.graphicsMemorySize);
		IntegrationEventDeviceStat.IntegrationEventDeviceStat_0.String_2 = SystemInfo.operatingSystem;
		IntegrationEventDeviceStat.IntegrationEventDeviceStat_0.String_3 = string.Format("{0}x{1}", Screen.width, Screen.height);
		IntegrationsManager.IntegrationsManager_0.SendEvent(IntegrationEventDeviceStat.IntegrationEventDeviceStat_0);
	}

	public void BattleStart()
	{
		IntegrationEventBattleStat.IntegrationEventBattleStat_0.Boolean_1 = true;
		IntegrationsManager.IntegrationsManager_0.SendEvent(IntegrationEventBattleStat.IntegrationEventBattleStat_0);
	}

	public void BattleStop()
	{
		IntegrationEventBattleStat.IntegrationEventBattleStat_0.Boolean_1 = false;
		IntegrationsManager.IntegrationsManager_0.SendEvent(IntegrationEventBattleStat.IntegrationEventBattleStat_0);
	}
}
