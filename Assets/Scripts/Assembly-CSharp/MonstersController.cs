using UnityEngine;
using engine.helpers;

public static class MonstersController
{
	public enum MonsterAudioType
	{
		HURT = 0,
		VOICE = 1,
		BITE = 2,
		DEATH = 3
	}

	public static bool Boolean_0
	{
		get
		{
			WaveMonstersData waveMonstersData_ = FightOfflineController.FightOfflineController_0.WaveMonstersData_0;
			if (waveMonstersData_ == null)
			{
				return false;
			}
			return waveMonstersData_.Int32_3 != 0;
		}
	}

	public static int Int32_0
	{
		get
		{
			WaveMonstersData waveMonstersData_ = FightOfflineController.FightOfflineController_0.WaveMonstersData_0;
			if (waveMonstersData_ == null)
			{
				return 0;
			}
			return waveMonstersData_.Int32_2;
		}
	}

	public static float Single_0
	{
		get
		{
			WaveMonstersData waveMonstersData_ = FightOfflineController.FightOfflineController_0.WaveMonstersData_0;
			if (waveMonstersData_ == null)
			{
				return 0f;
			}
			return waveMonstersData_.Single_1 * FightOfflineController.FightOfflineController_0.Single_0;
		}
	}

	public static float Single_1
	{
		get
		{
			WaveMonstersData waveMonstersData_ = FightOfflineController.FightOfflineController_0.WaveMonstersData_0;
			if (waveMonstersData_ == null)
			{
				return 0f;
			}
			return waveMonstersData_.Single_0 * FightOfflineController.FightOfflineController_0.Single_0;
		}
	}

	public static float Single_2
	{
		get
		{
			WaveMonstersData waveMonstersData_ = FightOfflineController.FightOfflineController_0.WaveMonstersData_0;
			if (waveMonstersData_ == null)
			{
				return 0f;
			}
			return waveMonstersData_.Single_2 * FightOfflineController.FightOfflineController_0.Single_0;
		}
	}

	public static int GetMonsterCountInWave()
	{
		WaveMonstersData waveMonstersData_ = FightOfflineController.FightOfflineController_0.WaveMonstersData_0;
		if (waveMonstersData_ != null && waveMonstersData_.List_0 != null)
		{
			return LocalMonsterData.Int32_0;
		}
		return 0;
	}

	public static int GetMonsterByNumberInWave(int int_0)
	{
		WaveMonstersData waveMonstersData_ = FightOfflineController.FightOfflineController_0.WaveMonstersData_0;
		if (waveMonstersData_ != null && waveMonstersData_.List_0 != null)
		{
			int_0 = Mathf.Min(LocalMonsterData.Int32_0 - 1, int_0);
			return LocalMonsterData.List_0[int_0];
		}
		return 0;
	}

	public static int GetRandomMonster()
	{
		WaveMonstersData waveMonstersData_ = FightOfflineController.FightOfflineController_0.WaveMonstersData_0;
		if (waveMonstersData_ != null && waveMonstersData_.List_0 != null)
		{
			return LocalMonsterData.List_0[Random.Range(0, LocalMonsterData.Int32_0)];
		}
		return 0;
	}

	public static GameObject GetMonsterObject(int int_0)
	{
		GameObject mosnter = LocalMonsterData.GetMosnter(int_0);
		if (mosnter == null)
		{
			return null;
		}
		MonsterParams component = mosnter.transform.GetChild(0).gameObject.GetComponent<MonsterParams>();
		if (component == null)
		{
			Log.AddLine(string.Format("[MonstersController::GetMonsterObject ERROR prefab = {0} no have MonsterParams component]", mosnter.name), Log.LogLevel.ERROR);
			return null;
		}
		component.monsterId = int_0;
		return mosnter;
	}

	public static AudioClip GetMonsterAudioClip(int int_0, MonsterAudioType monsterAudioType_0)
	{
		MonsterData objectByKey = MonsterStorage.Get.Storage.GetObjectByKey(int_0);
		if (objectByKey == null)
		{
			return null;
		}
		string string_ = string.Empty;
		switch (monsterAudioType_0)
		{
		case MonsterAudioType.HURT:
			string_ = objectByKey.String_1;
			break;
		case MonsterAudioType.VOICE:
			string_ = objectByKey.String_2;
			break;
		case MonsterAudioType.BITE:
			string_ = objectByKey.String_3;
			break;
		case MonsterAudioType.DEATH:
			string_ = objectByKey.String_4;
			break;
		}
		return LocalMonsterData.GetAudio(string_);
	}

	public static Texture2D GetMonsterSkinTexture(int int_0)
	{
		WaveMonstersData waveMonstersData_ = FightOfflineController.FightOfflineController_0.WaveMonstersData_0;
		if (waveMonstersData_ == null)
		{
			return null;
		}
		CustomMonsterData customMonsterData = waveMonstersData_.List_0.Find((CustomMonsterData customMonsterData_0) => customMonsterData_0.Int32_0 == int_0);
		if (customMonsterData == null)
		{
			return null;
		}
		return LocalMonsterData.GetSkin(customMonsterData.String_0);
	}
}
