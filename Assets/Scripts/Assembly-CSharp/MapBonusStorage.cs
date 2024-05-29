using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class MapBonusStorage : BaseStorage<int, MapBonusData>
{
	private static MapBonusStorage _instance;

	public static MapBonusStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new MapBonusStorage();
			}
			return _instance;
		}
	}

	private MapBonusStorage()
	{
	}

	protected override void OnAddIndexes()
	{
	}
}
