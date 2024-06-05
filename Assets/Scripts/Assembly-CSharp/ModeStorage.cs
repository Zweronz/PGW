using System.Collections.Generic;
using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class ModeStorage : BaseStorage<int, ModeData>
{
	public const int INDEX_MODE_TYPE = 0;

	private static ModeStorage _instance;

	public static ModeStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new ModeStorage();
				//_instance.Storage = new StorageData<int, ModeData>();
			}
			return _instance;
		}
	}

	private ModeStorage()
	{
	}

	protected override void OnAddIndexes()
	{
		Get.Storage.AddIndex(new HashCallbackIndex<ModeType, ModeData>((ModeData data, HashIndex<ModeType, ModeData> index) => data.ModeType_0));
	}

	public ModeData GetModeDataByTypeAndMap(ModeType modeType, int mapId)
	{
		List<ModeData> list = Get.Storage.Search(0, modeType);
		if (list != null && list.Count != 0)
		{
			int num = 0;
			while (true)
			{
				if (num < list.Count)
				{
					if (list[num].Int32_1 == mapId)
					{
						break;
					}
					num++;
					continue;
				}
				return null;
			}
			return list[num];
		}
		return null;
	}
}
