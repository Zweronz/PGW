using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class ModeTypeStorage : BaseStorage<ModeType, ModeTypeData>
{
	private static ModeTypeStorage _instance;

	public static ModeTypeStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new ModeTypeStorage();
			}
			return _instance;
		}
	}

	private ModeTypeStorage()
	{
	}
}
