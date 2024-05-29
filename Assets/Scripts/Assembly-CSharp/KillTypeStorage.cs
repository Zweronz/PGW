using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class KillTypeStorage : BaseStorage<int, KillTypeData>
{
	private static KillTypeStorage _instance;

	public static KillTypeStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new KillTypeStorage();
			}
			return _instance;
		}
	}

	private KillTypeStorage()
	{
	}

	protected override void OnAddIndexes()
	{
	}
}
