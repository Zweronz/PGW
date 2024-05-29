using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class AdditionalPurchasesDataStorage : BaseStorage<AdditionalPurchaseType, AdditionalPurchaseData>
{
	public const int INDEX_BASE = 0;

	private static AdditionalPurchasesDataStorage _instance;

	public static AdditionalPurchasesDataStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new AdditionalPurchasesDataStorage();
			}
			return _instance;
		}
	}

	private AdditionalPurchasesDataStorage()
	{
	}

	public AdditionalPurchaseData GetAddPurchaseDataByKey(AdditionalPurchaseType key)
	{
		return Get.Storage.GetObjectByKey(key);
	}
}
