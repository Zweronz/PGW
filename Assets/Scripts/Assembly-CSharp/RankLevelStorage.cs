using System;
using System.Collections.Generic;
using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class RankLevelStorage : BaseStorage<int, RankLevelData>
{
	public const int INDEX_LEVEL = 0;

	private static RankLevelStorage _instance;

	public static RankLevelStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new RankLevelStorage();
			}
			return _instance;
		}
	}

	public int RankLevelMin { get; private set; }

	private RankLevelStorage()
	{
	}

	public int GetMinMedalsForLevel(int rankLevel)
	{
		RankLevelData objectByKey = base.Storage.GetObjectByKey(rankLevel);
		return (objectByKey != null) ? objectByKey.Int32_1 : 0;
	}

	public int GetMaxMedalsForLevel(int rankLevel)
	{
		int rankLevel2 = Math.Max(rankLevel - 1, RankLevelMin);
		return GetMinMedalsForLevel(rankLevel2);
	}

	protected override void OnAddIndexes()
	{
		Get.Storage.AddIndex(new HashUniqueCallbackIndex<int, RankLevelData>((RankLevelData data, HashUniqueIndex<int, RankLevelData> index) => data.Int32_0));
	}

	protected override void OnCreate()
	{
		InitMaxRankLevel();
	}

	private void InitMaxRankLevel()
	{
		List<RankLevelData> objects = base.Storage.GetObjects();
		objects.Sort((RankLevelData data1, RankLevelData data2) => data1.Int32_0.CompareTo(data2.Int32_0));
		RankLevelMin = ((objects.Count != 0) ? objects[0].Int32_0 : 0);
	}
}
