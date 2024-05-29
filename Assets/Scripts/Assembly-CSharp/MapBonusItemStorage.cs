using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class MapBonusItemStorage : BaseStorage<int, MapBonusItemData>
{
	private static MapBonusItemStorage _instance;

	public static MapBonusItemStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new MapBonusItemStorage();
			}
			return _instance;
		}
	}

	private MapBonusItemStorage()
	{
	}

	protected override void OnAddIndexes()
	{
	}
}
