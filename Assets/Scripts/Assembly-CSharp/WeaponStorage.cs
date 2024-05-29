using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class WeaponStorage : BaseStorage<int, WeaponData>
{
	private static WeaponStorage _instance;

	public static WeaponStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new WeaponStorage();
			}
			return _instance;
		}
	}

	private WeaponStorage()
	{
	}
}
