using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class MapStorage : BaseStorage<int, MapData>
{
	private static MapStorage _instance;

	public static MapStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new MapStorage();
			}
			return _instance;
		}
	}

	private MapStorage()
	{
	}
}
