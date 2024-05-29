using System.Collections.Generic;
using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class KillStreakStorage : BaseStorage<int, KillstreakData>
{
	private static KillStreakStorage _instance;

	public static KillStreakStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new KillStreakStorage();
			}
			return _instance;
		}
	}

	private KillStreakStorage()
	{
	}

	protected override void OnAddIndexes()
	{
	}

	public int GetScoreForKillStreak(KillStreakType kType)
	{
		foreach (KeyValuePair<int, KillstreakData> item in (IEnumerable<KeyValuePair<int, KillstreakData>>)base.Storage)
		{
			if (item.Value.KillStreakType_0 == kType)
			{
				return item.Value.Int32_1;
			}
		}
		return 0;
	}

	public MapBonusItemData GetBonusKillStreak(KillStreakType kType)
	{
		KillstreakData killstreakData = null;
		foreach (KeyValuePair<int, KillstreakData> item in (IEnumerable<KeyValuePair<int, KillstreakData>>)base.Storage)
		{
			if (item.Value.KillStreakType_0 == kType)
			{
				killstreakData = item.Value;
				break;
			}
		}
		if (killstreakData == null)
		{
			return null;
		}
		MapBonusData objectByKey = MapBonusStorage.Get.Storage.GetObjectByKey(killstreakData.Int32_2);
		if (objectByKey == null)
		{
			return null;
		}
		return objectByKey.GetItem();
	}
}
