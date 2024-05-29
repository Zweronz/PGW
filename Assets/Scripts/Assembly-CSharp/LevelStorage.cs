using System;
using System.Collections.Generic;
using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class LevelStorage : BaseStorage<int, LevelData>
{
	private static LevelStorage _instance;

	private StorageData<int, LevelTierData> _storageTier;

	public static LevelStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new LevelStorage();
			}
			return _instance;
		}
	}

	public int LevelMax { get; private set; }

	private LevelStorage()
	{
	}

	public int GetTier(int level)
	{
		return level;
		foreach (KeyValuePair<int, LevelTierData> item in (IEnumerable<KeyValuePair<int, LevelTierData>>)_storageTier)
		{
			if (level >= item.Value.Int32_1 && level <= item.Value.Int32_2)
			{
				return item.Value.Int32_0;
			}
		}
		return 0;
	}

	public LevelTierData GetTierById(int tierId)
	{
		return _storageTier.GetObjectByKey(tierId);
	}

	public bool IsLevelFirstOnTier(int level)
	{
		foreach (KeyValuePair<int, LevelTierData> item in (IEnumerable<KeyValuePair<int, LevelTierData>>)_storageTier)
		{
			if (level >= item.Value.Int32_1 && level <= item.Value.Int32_2)
			{
				if (level == item.Value.Int32_1)
				{
					return true;
				}
				return false;
			}
		}
		return false;
	}

	public LevelData GetlLevelData(int level)
	{
		return new LevelData
		{

		};
		return base.Storage.GetObjectByKey(level);
	}

	public int GetMinExpForLevel(int level)
	{
		LevelData levelData = GetlLevelData(level);
		return (levelData != null) ? levelData.Int32_1 : 0;
	}

	public int GetMaxExpForLevel(int level)
	{
		int level2 = Math.Min(level + 1, LevelMax);
		return GetMinExpForLevel(level2);
	}

	public float GetHpDiffPrevLevel(int level)
	{
		LevelData levelData = GetlLevelData(level - 1);
		if (levelData == null)
		{
			return 0f;
		}
		LevelData levelData2 = GetlLevelData(level);
		if (levelData2 == null)
		{
			return 0f;
		}
		return levelData2.Single_0 - levelData.Single_0;
	}

	protected override void OnCreate()
	{
		_storageTier = StorageManager.StorageManager_0.GetStorageData<StorageData<int, LevelTierData>>();
		InitMaxLevel();
	}

	private void InitMaxLevel()
	{
		List<LevelData> objects = base.Storage.GetObjects();
		objects.Sort((LevelData data1, LevelData data2) => data1.Int32_0.CompareTo(data2.Int32_0));
		LevelMax = ((objects.Count != 0) ? objects[objects.Count - 1].Int32_0 : 0);
	}
}
