using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using engine.controllers;
using engine.events;
using engine.helpers;
using engine.integrations;
using engine.unity;

public sealed class GameFPSStatIntegration : IIntegration
{
	private HashSet<IntegrationEventType> hashSet_0 = new HashSet<IntegrationEventType> { IntegrationEventType.BATTLE_STAT };

	private float float_0;

	private float float_1;

	private int int_0;

	private int int_1;

	private int int_2;

	[CompilerGenerated]
	private GameFPSStatistics gameFPSStatistics_0;

	public GameFPSStatistics GameFPSStatistics_0
	{
		[CompilerGenerated]
		get
		{
			return gameFPSStatistics_0;
		}
		[CompilerGenerated]
		private set
		{
			gameFPSStatistics_0 = value;
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
		if (integrationEventBattleStat != null && integrationEventBattleStat.Boolean_3)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!integrationEventBattleStat.Boolean_1)
			{
				stringBuilder.AppendLine("[GameFPSStatIntegration. Final collect game fps stats.]:");
				stringBuilder.AppendLine(GameFPSStatistics_0.ToString());
			}
			else
			{
				stringBuilder.AppendLine("[GameFPSStatIntegration. Start collect game fps stats.]");
			}
		}
	}

	private void InitStatistics()
	{
		GameFPSStatistics_0 = new GameFPSStatistics();
		MonoSingleton<FightController>.Prop_0.GameFPSStatistics_0 = GameFPSStatistics_0;
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
		Log.AddLine("[GameFPSStatIntegration. Start collect galme fps stats.]");
		GameFPSStatistics_0.Reset();
		float_0 = Time.time + 15f;
		if (!DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateOneSecond))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(UpdateOneSecond);
		}
		if (!DependSceneEvent<MainUpdate>.Contains(Update))
		{
			DependSceneEvent<MainUpdate>.GlobalSubscribe(Update);
		}
	}

	private void StopCollectStatistic()
	{
		Log.AddLine("[GameFPSStatIntegration. End collect game fps stats.]");
		if (DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateOneSecond))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(UpdateOneSecond);
		}
		if (DependSceneEvent<MainUpdate>.Contains(Update))
		{
			DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Update);
		}
	}

	private void UpdateOneSecond()
	{
		if (!(float_0 > Time.time))
		{
			if (!CalculateFPS())
			{
				IntegrationEventGameStat integrationEventGameStat_ = IntegrationEventGameStat.IntegrationEventGameStat_0;
				integrationEventGameStat_.Boolean_1 = true;
				SendCallback(integrationEventGameStat_);
			}
			GameFPSStatistics_0.SetFPS(int_1);
		}
	}

	private void Update()
	{
		if (!(float_0 > Time.time))
		{
			float_1 += Time.timeScale / Time.deltaTime;
			int_0++;
		}
	}

	private bool CalculateFPS()
	{
		int_1 = (int)(float_1 / (float)int_0);
		float_1 = 0f;
		int_0 = 0;
		int_2 = ((int_1 <= 50) ? (++int_2) : 0);
		return int_2 <= 10;
	}
}
