using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class ConsumablesStorage : BaseStorage<int, ConsumableData>
{
	private static ConsumablesStorage _instance;

	public static ConsumablesStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new ConsumablesStorage();
			}
			return _instance;
		}
	}

	private ConsumablesStorage()
	{
	}
}
