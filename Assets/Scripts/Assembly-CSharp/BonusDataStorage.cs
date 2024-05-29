using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class BonusDataStorage : BaseStorage<int, BonusData>
{
	private static BonusDataStorage _instance;

	public static BonusDataStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new BonusDataStorage();
			}
			return _instance;
		}
	}

	private BonusDataStorage()
	{
	}
}
