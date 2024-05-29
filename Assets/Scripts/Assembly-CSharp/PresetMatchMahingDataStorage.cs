using System.Collections.Generic;
using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class PresetMatchMahingDataStorage : BaseStorage<int, PresetMatchMakingData>
{
	public const int INDEX_BASE = 0;

	private static PresetMatchMahingDataStorage _instance;

	public static PresetMatchMahingDataStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new PresetMatchMahingDataStorage();
			}
			return _instance;
		}
	}

	private PresetMatchMahingDataStorage()
	{
	}

	public List<PresetMatchMakingData> GetPresetsData()
	{
		return Get.Storage.GetObjects();
	}
}
