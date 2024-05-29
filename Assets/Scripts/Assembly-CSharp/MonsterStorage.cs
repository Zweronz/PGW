using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class MonsterStorage : BaseStorage<int, MonsterData>
{
	private static MonsterStorage _instance;

	public static MonsterStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new MonsterStorage();
			}
			return _instance;
		}
	}

	private MonsterStorage()
	{
	}

	protected override void OnAddIndexes()
	{
		Get.Storage.AddIndex(new HashCallbackIndex<string, MonsterData>((MonsterData data, HashIndex<string, MonsterData> index) => data.String_0));
	}
}
