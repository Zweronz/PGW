using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class ArtikulStorage : BaseStorage<int, ArtikulData>
{
	public const int INDEX_SLOT_TYPE = 0;

	public const int INDEX_PREFAB_NAME = 1;

	public const int INDEX_PREFAB_TAG = 2;

	private static ArtikulStorage _instance;

	public static ArtikulStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new ArtikulStorage();
			}
			return _instance;
		}
	}

	private ArtikulStorage()
	{
	}

	protected override void OnAddIndexes()
	{
		Get.Storage.AddIndex(new HashCallbackIndex<SlotType, ArtikulData>((ArtikulData data, HashIndex<SlotType, ArtikulData> index) => data.SlotType_0));
		Get.Storage.AddIndex(new HashUniqueCallbackIndex<string, ArtikulData>((ArtikulData data, HashUniqueIndex<string, ArtikulData> index) => data.String_3));
		Get.Storage.AddIndex(new HashUniqueCallbackIndex<string, ArtikulData>((ArtikulData data, HashUniqueIndex<string, ArtikulData> index) => data.String_2));
	}
}
