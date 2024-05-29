using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class WaveMonstersStorage : BaseStorage<int, WaveMonstersData>
{
	public const int INDEX_MODE_DATA = 0;

	private static WaveMonstersStorage _instance;

	public static WaveMonstersStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new WaveMonstersStorage();
			}
			return _instance;
		}
	}

	private WaveMonstersStorage()
	{
	}

	protected override void OnAddIndexes()
	{
		Get.Storage.AddIndex(new HashCallbackIndex<int, WaveMonstersData>((WaveMonstersData data, HashIndex<int, WaveMonstersData> index) => data.Int32_1));
	}
}
