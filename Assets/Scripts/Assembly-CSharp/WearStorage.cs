using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class WearStorage : BaseStorage<int, WearData>
{
	private static WearStorage _instance;

	public static WearStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new WearStorage();
			}
			return _instance;
		}
	}

	private WearStorage()
	{
	}
}
