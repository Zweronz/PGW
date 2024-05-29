using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class ClanLevelStorage : BaseStorage<int, ClanLevelData>
{
	public const int INDEX_LEVEL = 0;

	private static ClanLevelStorage _instance;

	public static ClanLevelStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new ClanLevelStorage();
			}
			return _instance;
		}
	}

	private ClanLevelStorage()
	{
	}

	protected override void OnAddIndexes()
	{
		Get.Storage.AddIndex(new HashUniqueCallbackIndex<int, ClanLevelData>((ClanLevelData data, HashUniqueIndex<int, ClanLevelData> index) => data.int_0));
	}
}
