using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using engine.controllers;
using engine.events;
using engine.helpers;
using engine.integrations;
using engine.unity;

public class PhotonNetworkStatIntegration : IIntegration
{
	public const int int_0 = 300;

	private PhotonNetworkStatGui photonNetworkStatGui_0;

	private float float_0;

	private bool bool_0;

	private HashSet<IntegrationEventType> hashSet_0 = new HashSet<IntegrationEventType> { IntegrationEventType.BATTLE_STAT };

	[CompilerGenerated]
	private PhotonNetworkStatistics photonNetworkStatistics_0;

	public PhotonNetworkStatistics PhotonNetworkStatistics_0
	{
		[CompilerGenerated]
		get
		{
			return photonNetworkStatistics_0;
		}
		[CompilerGenerated]
		private set
		{
			photonNetworkStatistics_0 = value;
		}
	}

	public override HashSet<IntegrationEventType> ProcessingEvents()
	{
		return hashSet_0;
	}

	public override bool Init()
	{
		InitStatistics();
		return true;
	}

	public override void OnEvent(IntegrationEvent integrationEvent_0)
	{
		IntegrationEventType integrationEventType_ = integrationEvent_0.IntegrationEventType_0;
		if (integrationEventType_ == IntegrationEventType.BATTLE_STAT)
		{
			OnBattleStat(integrationEvent_0);
		}
	}

	private void OnBattleStat(IntegrationEvent integrationEvent_0)
	{
		IntegrationEventBattleStat integrationEventBattleStat = integrationEvent_0 as IntegrationEventBattleStat;
		if (integrationEventBattleStat != null && integrationEventBattleStat.Boolean_3 && Defs.bool_2)
		{
			PhotonNetwork.Boolean_12 = integrationEventBattleStat.Boolean_1;
			StringBuilder stringBuilder = new StringBuilder();
			if (!integrationEventBattleStat.Boolean_1)
			{
				stringBuilder.AppendLine("[PhotonNetworkStatIntegration. Final collect network stats.]:");
				stringBuilder.AppendLine("---------- Photon network statistics 0 ----------");
				stringBuilder.AppendLine(PhotonNetwork.NetworkStatisticsToString());
				stringBuilder.AppendLine("---------- Photon network statistics 1 ----------");
				stringBuilder.AppendLine(photonNetworkStatGui_0.ToString());
				stringBuilder.AppendLine("-----------------------------");
				stringBuilder.AppendLine("---------- Photon ping statistics ----------");
				stringBuilder.AppendLine(PhotonNetworkStatistics_0.ToString());
				stringBuilder.AppendLine("-----------------------------");
				ShowGUI(false);
			}
			else
			{
				stringBuilder.AppendLine("[PhotonNetworkStatIntegration. Start collect network stats.]");
				ShowGUI(true);
			}
			PhotonNetwork.NetworkStatisticsReset();
		}
	}

	private void ShowGUI(bool bool_1)
	{
		if (bool_1)
		{
			if (!(photonNetworkStatGui_0 != null))
			{
				photonNetworkStatGui_0 = new GameObject("PhotonNetworkTemp", typeof(PhotonNetworkStatGui)).GetComponent<PhotonNetworkStatGui>();
			}
		}
		else if (!(photonNetworkStatGui_0 == null))
		{
			Object.Destroy(photonNetworkStatGui_0.gameObject);
		}
	}

	private void InitStatistics()
	{
		PhotonNetworkStatistics_0 = new PhotonNetworkStatistics();
		MonoSingleton<FightController>.Prop_0.PhotonNetworkStatistics_0 = PhotonNetworkStatistics_0;
		if (!AppStateController.AppStateController_0.Contains(StartCollectStatistic, AppStateController.States.JOINED_ROOM))
		{
			AppStateController.AppStateController_0.Subscribe(StartCollectStatistic, AppStateController.States.JOINED_ROOM);
		}
		if (!AppStateController.AppStateController_0.Contains(StopCollectStatistic, AppStateController.States.LEAVING_ROOM))
		{
			AppStateController.AppStateController_0.Subscribe(StopCollectStatistic, AppStateController.States.LEAVING_ROOM);
		}
	}

	private void StartCollectStatistic()
	{
		if (Defs.bool_2)
		{
			Log.AddLine("[PhotonNetworkStatIntegration. Start collect photon ping stats.]");
			PhotonNetworkStatistics_0.Reset();
			float_0 = Time.time + 10f;
			bool_0 = false;
			if (!DependSceneEvent<MainUpdateOneSecond>.Contains(Update))
			{
				DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(Update);
			}
		}
	}

	private void StopCollectStatistic()
	{
		Log.AddLine("[PhotonNetworkStatIntegration. End collect photon ping stats.]");
		if (DependSceneEvent<MainUpdateOneSecond>.Contains(Update))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(Update);
		}
	}

	private void Update()
	{
		if (!(float_0 > Time.time))
		{
			if (!bool_0 && photonNetworkStatGui_0 != null)
			{
				photonNetworkStatGui_0.Reset();
			}
			bool_0 = true;
			int int_ = ((!(photonNetworkStatGui_0 == null)) ? photonNetworkStatGui_0.Int32_0 : 0);
			int int_2 = ((!(photonNetworkStatGui_0 == null)) ? photonNetworkStatGui_0.Int32_1 : 0);
			PhotonNetworkStatistics_0.SetPing(int_, int_2);
			if (PhotonNetworkStatistics_0.Int32_0 > 300)
			{
				IntegrationEventGameStat integrationEventGameStat_ = IntegrationEventGameStat.IntegrationEventGameStat_0;
				integrationEventGameStat_.Boolean_2 = true;
				SendCallback(integrationEventGameStat_);
			}
		}
	}
}
