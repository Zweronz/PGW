using System;
using UnityEngine;
using engine.events;
using engine.unity;

public class SurvivalUIController : MonoBehaviour
{
	public UILabel ScoreLabel;

	public UILabel WaveCountLablel;

	public UILabel WaveMonsterCount;

	public UILabel TimerLabel;

	public UISprite skullIcon;

	private int int_0;

	private int int_1;

	private void Start()
	{
		ScoreLabel.String_0 = UsersData.UsersData_0.UserData_0.localUserData_0.ObscuredInt_0.ToString();
		UpdateWaveCount();
		InitLeftTime();
	}

	private void OnEnable()
	{
		UsersData.Subscribe(UsersData.EventType.OFFLINE_WAVES_OVERCOME_CHANGED, UpdateWaveCount);
		UsersData.Subscribe(UsersData.EventType.OFFLINE_WAVES_COMPLETE, CompleteWave);
	}

	private void OnDisable()
	{
		UsersData.Unsubscribe(UsersData.EventType.OFFLINE_WAVES_OVERCOME_CHANGED, UpdateWaveCount);
		UsersData.Unsubscribe(UsersData.EventType.OFFLINE_WAVES_COMPLETE, CompleteWave);
		if (DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateTimeLeft))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(UpdateTimeLeft);
		}
	}

	private void Update()
	{
		UpdateScore();
		UpdateMonsterCount();
		UpdateTimer();
	}

	private void UpdateWaveCount()
	{
		if (!(WaveCountLablel == null))
		{
			WaveCountLablel.String_0 = UsersData.UsersData_0.UserData_0.localUserData_0.Int32_0.ToString();
		}
	}

	private void UpdateWaveCount(UsersData.EventData eventData_0)
	{
		if (!(WaveCountLablel == null))
		{
			WaveCountLablel.String_0 = eventData_0.int_0.ToString();
			InitLeftTime();
		}
	}

	private void CompleteWave(UsersData.EventData eventData_0)
	{
		if (DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateTimeLeft))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(UpdateTimeLeft);
		}
		int_0 = 0;
	}

	private void InitLeftTime()
	{
		if (FightOfflineController.FightOfflineController_0.WaveMonstersData_0 != null)
		{
			int_0 = FightOfflineController.FightOfflineController_0.WaveMonstersData_0.Int32_4;
			if (int_0 > 0 && !DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateTimeLeft))
			{
				DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(UpdateTimeLeft);
			}
		}
	}

	private void UpdateTimeLeft()
	{
		int_0--;
		if (HeadUpDisplay.HeadUpDisplay_0 != null && (int_0 == 30 || int_0 == 20 || int_0 <= 10))
		{
			skullIcon.GetComponent<Animation>().enabled = true;
			HeadUpDisplay.HeadUpDisplay_0.ShowArenaTimeAttention(int_0);
		}
		if (int_0 <= 0 && DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateTimeLeft))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(UpdateTimeLeft);
		}
		if (int_0 <= 0 && PhotonNetwork.Room_0 != null)
		{
			UserController.UserController_0.UserData_0.localUserData_0.Boolean_2 = true;
			MonoSingleton<FightController>.Prop_0.LeaveRoom();
		}
	}

	private void UpdateScore()
	{
		if (!(ScoreLabel == null))
		{
			int num = UsersData.UsersData_0.UserData_0.localUserData_0.ObscuredInt_0;
			if (num != int_1)
			{
				int_1 = num;
				ScoreLabel.String_0 = num.ToString();
				ScoreLabel.GetComponent<Animation>().Play("ScoreChangeSurv");
			}
		}
	}

	private void UpdateMonsterCount()
	{
		if (!(WaveMonsterCount == null) && !(ZombieCreator.zombieCreator_0 == null))
		{
			WaveMonsterCount.String_0 = (MonstersController.Int32_0 - ZombieCreator.zombieCreator_0.Int32_2).ToString();
		}
	}

	private void UpdateTimer()
	{
		if (!(TimerLabel == null))
		{
			int num = Math.Max(0, int_0);
			int num2 = num / 60;
			int num3 = num % 60;
			TimerLabel.String_0 = string.Format("{0}:{1:00}", num2, num3);
		}
	}
}
