using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.helpers;
using engine.unity;

public sealed class FightOfflineController
{
	public enum DifficultyLevel
	{
		None = 0,
		Level1 = 1,
		Level2 = 2,
		Level3 = 3
	}

	private static FightOfflineController fightOfflineController_0;

	private DifficultyLevel difficultyLevel_0;

	private bool bool_0;

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private WaveMonstersData waveMonstersData_0;

	[CompilerGenerated]
	private ModeData modeData_0;

	[CompilerGenerated]
	private static Predicate<WaveMonstersData> predicate_0;

	public static FightOfflineController FightOfflineController_0
	{
		get
		{
			if (fightOfflineController_0 == null)
			{
				fightOfflineController_0 = new FightOfflineController();
			}
			return fightOfflineController_0;
		}
	}

	public bool Boolean_0
	{
		get
		{
			return bool_0;
		}
		set
		{
			bool_0 = false;
		}
	}

	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		private set
		{
			int_0 = value;
		}
	}

	public string String_0
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		private set
		{
			string_0 = value;
		}
	}

	public int Int32_1
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		private set
		{
			int_1 = value;
		}
	}

	public WaveMonstersData WaveMonstersData_0
	{
		[CompilerGenerated]
		get
		{
			return waveMonstersData_0;
		}
		[CompilerGenerated]
		private set
		{
			waveMonstersData_0 = value;
		}
	}

	public ModeData ModeData_0
	{
		[CompilerGenerated]
		get
		{
			return modeData_0;
		}
		[CompilerGenerated]
		private set
		{
			modeData_0 = value;
		}
	}

	public float Single_0
	{
		get
		{
			float num = 1f;
			if (WaveMonstersData_0 == null)
			{
				return num;
			}
			switch (difficultyLevel_0)
			{
			case DifficultyLevel.Level1:
				num = WaveMonstersData_0.Single_3;
				break;
			case DifficultyLevel.Level2:
				num = WaveMonstersData_0.Single_4;
				break;
			case DifficultyLevel.Level3:
				num = WaveMonstersData_0.Single_5;
				break;
			}
			return (num != 0f) ? num : 1f;
		}
	}

	private FightOfflineController()
	{
	}

	public void StartFight(int int_2 = 0, int int_3 = 0, DifficultyLevel difficultyLevel_1 = DifficultyLevel.None)
	{
		MenuBackgroundMusic.menuBackgroundMusic_0.StopMenuMusicImmediately();
		UserController.UserController_0.UserData_0.localUserData_0.ClearOfflineFightData();
		difficultyLevel_0 = difficultyLevel_1;
		ModeData_0 = null;
		WaveMonstersData_0 = null;
		UserController.UserController_0.ClearActiveSlots();
		WeaponManager.weaponManager_0.ResetAmmoInAllWeapon();
		StartWave(int_2, int_3);
		if (ModeData_0 != null)
		{
			switch (ModeData_0.ModeType_0)
			{
			default:
				Log.AddLine(string.Format("[FightOfflineController::StartFight. Mode type {0} not work in offline fight controller, wave data id = {1}, mode data id = {2}]", ModeData_0.ModeType_0.ToString(), WaveMonstersData_0.Int32_0, ModeData_0.Int32_0), Log.LogLevel.ERROR);
				return;
			case ModeType.ARENA:
				MonoSingleton<FightController>.Prop_0.CreateRoom(ModeData_0, 0, 0, string.Empty, string.Empty);
				break;
			case ModeType.TUTORIAL:
				MonoSingleton<FightController>.Prop_0.CreateRoom(ModeData_0, 0, 0, string.Empty, string.Empty);
				break;
			case ModeType.CAMPAIGN:
				break;
			}
			Log.AddLine(string.Format("[FightOfflineController::StartFight. Start offline fight with wave monster id = {0}, mode data id = {1}]", WaveMonstersData_0.Int32_0, ModeData_0.Int32_0));
		}
	}

	public void StartNextWave()
	{
		if (WaveMonstersData_0 != null && ModeData_0 != null)
		{
			UserController.UserController_0.UserData_0.localUserData_0.Int32_0++;
			int nextWaveId = GetNextWaveId();
			Log.AddLine(string.Format("[FightOfflineController::StartNextWave. Start next monster wave with wave monster id = {0}]", nextWaveId));
			StartWave(nextWaveId);
			if (BonusMapController.bonusMapController_0 != null)
			{
				BonusMapController.bonusMapController_0.CreateMapPoints();
			}
		}
		else
		{
			Log.AddLine(string.Format("[FightOfflineController::StartNextWave. Error start next monster wave. CurrentWaveMonsters == null?{0}, CurrentModeData == null?{1}]", WaveMonstersData_0 == null, ModeData_0 == null), Log.LogLevel.ERROR);
		}
	}

	public bool IsNextWaveBoss()
	{
		WaveMonstersData nextWave = GetNextWave(GetNextWaveId());
		if (nextWave == null)
		{
			return false;
		}
		return nextWave.Boolean_1;
	}

	private void StartWave(int int_2, int int_3 = 0)
	{
		if (InitWave(int_2) && InitModeData(int_3))
		{
			InitMonsters();
		}
	}

	private bool InitWave(int int_2)
	{
		WaveMonstersData_0 = GetNextWave(int_2);
		if (WaveMonstersData_0 == null)
		{
			Log.AddLine("[FightOfflineController::InitArenaWave. Error init monster waves data, id]: " + int_2, Log.LogLevel.ERROR);
			return false;
		}
		return true;
	}

	private int GetNextWaveId()
	{
		return (WaveMonstersData_0.Int32_3 != 0) ? WaveMonstersData_0.Int32_3 : WaveMonstersData_0.Int32_0;
	}

	private WaveMonstersData GetNextWave(int int_2)
	{
		WaveMonstersData waveMonstersData = null;
		if (int_2 == 0)
		{
			List<WaveMonstersData> list = WaveMonstersStorage.Get.Storage.Search(0, 0);
			return list.Find((WaveMonstersData waveMonstersData_1) => waveMonstersData_1.Boolean_0);
		}
		return WaveMonstersStorage.Get.Storage.GetObjectByKey(int_2);
	}

	private bool InitModeData(int int_2)
	{
		if (WaveMonstersData_0 == null)
		{
			return false;
		}
		if (int_2 != 0)
		{
			ModeData_0 = ModeStorage.Get.Storage.GetObjectByKey(int_2);
		}
		if (ModeData_0 != null)
		{
			return true;
		}
		if (WaveMonstersData_0.Int32_1 == 0)
		{
			List<ModeData> list = ModeStorage.Get.Storage.Search(0, ModeType.ARENA);
			if (list != null && list.Count != 0)
			{
				List<ModeData> list2 = new List<ModeData>();
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].Boolean_1 && !list[i].Boolean_4)
					{
						MapData objectByKey = MapStorage.Get.Storage.GetObjectByKey(list[i].Int32_1);
						if (objectByKey != null && objectByKey.Boolean_0 && !objectByKey.Boolean_3)
						{
							list2.Add(list[i]);
						}
					}
				}
				ModeData_0 = list2[UnityEngine.Random.Range(0, list2.Count)];
			}
		}
		else
		{
			ModeData_0 = ModeStorage.Get.Storage.GetObjectByKey(WaveMonstersData_0.Int32_1);
		}
		if (ModeData_0 == null)
		{
			Log.AddLine(string.Format("[FightOfflineController::InitArenaModeData. Error init mode data for monster wave data, mode data id]: ", WaveMonstersData_0.Int32_1), Log.LogLevel.ERROR);
			return false;
		}
		return true;
	}

	private void InitMonsters()
	{
		LocalMonsterData.Clear();
		List<CustomMonsterData> list_ = WaveMonstersData_0.List_0;
		if (list_ == null)
		{
			return;
		}
		for (int i = 0; i < list_.Count; i++)
		{
			CustomMonsterData customMonsterData = list_[i];
			if (customMonsterData.NeedsData_0 == null || customMonsterData.NeedsData_0.Check())
			{
				LocalMonsterData.LoadMonsterData(customMonsterData);
			}
		}
	}

	public int GetRandomWeaponFromWave()
	{
		if (WaveMonstersData_0 != null && WaveMonstersData_0.List_1 != null)
		{
			int index = UnityEngine.Random.Range(0, WaveMonstersData_0.List_1.Count);
			return WaveMonstersData_0.List_1[index];
		}
		return 0;
	}

	public void CompleteArenaBattle()
	{
		int int32_ = UserController.UserController_0.UserData_0.localUserData_0.Int32_0;
		int int32_2 = UserController.UserController_0.UserData_0.localUserData_0.ObscuredInt_1;
		int int32_3 = UserController.UserController_0.UserData_0.localUserData_0.ObscuredInt_0;
		int int_ = UserController.UserController_0.UserData_0.user_0.int_4;
		int int_2 = UserController.UserController_0.UserData_0.user_0.int_5;
		bool boolean_ = UserController.UserController_0.UserData_0.localUserData_0.Boolean_1;
		bool boolean_2 = UserController.UserController_0.UserData_0.localUserData_0.Boolean_2;
		ModeRewardData rewardAfterArena = ModesController.ModesController_0.GetRewardAfterArena(int32_3, int32_2, int32_);
		UserController.UserController_0.UserData_0.localUserData_0.ClearOfflineFightData();
		MenuBackgroundMusic.menuBackgroundMusic_0.Play();
		ScreenController.ScreenController_0.HideActiveScreen();
		ConsumablesController.ConsumablesController_0.OnArenaComplete();
		Application.LoadLevel("EmptyScene");
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.Hide();
		}
		ArenaCompleteWindowParams arenaCompleteWindowParams = new ArenaCompleteWindowParams();
		arenaCompleteWindowParams.ModeData_0 = MonoSingleton<FightController>.Prop_0.ModeData_0;
		arenaCompleteWindowParams.ModeRewardData_0 = rewardAfterArena;
		arenaCompleteWindowParams.Int32_0 = int32_;
		arenaCompleteWindowParams.Int32_1 = int32_2;
		arenaCompleteWindowParams.Int32_2 = int32_3;
		arenaCompleteWindowParams.Int32_3 = int_;
		arenaCompleteWindowParams.Int32_4 = int_2;
		arenaCompleteWindowParams.Boolean_0 = boolean_;
		arenaCompleteWindowParams.Boolean_1 = boolean_2;
		arenaCompleteWindowParams.Boolean_2 = UserController.UserController_0.UserData_0.localUserData_0.Boolean_0;
		ArenaCompleteWindow.Show(arenaCompleteWindowParams);
	}

	public void CompleteCompaignBattle()
	{
	}

	public void SetArenaTop(int int_2, string string_1, int int_3)
	{
		Int32_0 = int_2;
		String_0 = string_1;
		Int32_1 = int_3;
	}
}
