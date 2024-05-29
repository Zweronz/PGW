using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.data;
using engine.helpers;

public sealed class LocalShopData
{
	public const string string_0 = "WeaponSystem/Inner/";

	private const string string_1 = "_Inner";

	[CompilerGenerated]
	private Dictionary<string, GameObject> dictionary_0;

	[CompilerGenerated]
	private Dictionary<int, string> dictionary_1;

	[CompilerGenerated]
	private Dictionary<int, GameObject> dictionary_2;

	private Dictionary<string, GameObject> Dictionary_0
	{
		[CompilerGenerated]
		get
		{
			return dictionary_0;
		}
		[CompilerGenerated]
		set
		{
			dictionary_0 = value;
		}
	}

	private Dictionary<int, string> Dictionary_1
	{
		[CompilerGenerated]
		get
		{
			return dictionary_1;
		}
		[CompilerGenerated]
		set
		{
			dictionary_1 = value;
		}
	}

	private Dictionary<int, GameObject> Dictionary_2
	{
		[CompilerGenerated]
		get
		{
			return dictionary_2;
		}
		[CompilerGenerated]
		set
		{
			dictionary_2 = value;
		}
	}

	public void Init()
	{
		Dictionary_0 = new Dictionary<string, GameObject>();
		Dictionary_1 = new Dictionary<int, string>();
		Dictionary_2 = new Dictionary<int, GameObject>();
		OnStoragesDataComplete();
		StorageManager.StorageManager_0.Subscribe(OnStoragesDataComplete, StorageManager.StatusEvent.LOADING_COMPLETE);
	}

	private List<ArtikulData> LoadLocalArtikuls()
	{
		List<ArtikulData> artikuls = new List<ArtikulData>();
		Article article = Resources.Load<Article>("Article");

		foreach (Article.ArticleData data in article.articleData)
		{
			artikuls.Add(data.ToArtikul());
		}
		
		return artikuls;
	}

	private void OnStoragesDataComplete()
	{
		UnloadCache();
		List<ArtikulData> objects = LoadLocalArtikuls();//ArtikulStorage.Get.Storage.GetObjects();
		foreach (ArtikulData item in objects)
		{
			WeaponController.WeaponController_0.checkAddToMax(item.Int32_0);
			if (item.String_3.Equals(string.Empty))
			{
				continue;
			}
			GameObject gameObject = Resources.Load<GameObject>(item.String_3);
			if (gameObject == null)
			{
				Log.AddLine("[LocalShopData::OnStoragesDataComplete. Add prefab in cache error! Prefab name]: " + item.String_3, Log.LogLevel.WARNING);
				continue;
			}
			if (!Dictionary_0.ContainsKey(item.String_3))
			{
				Dictionary_0.Add(item.String_3, gameObject);
			}
			Dictionary_1.Add(item.Int32_0, item.String_3);
			if (item.Boolean_1)
			{
				WeaponSounds component = gameObject.GetComponent<WeaponSounds>();
				component.ResetWeaponData();
				InitInnerPrefab("WeaponSystem/Inner/" + gameObject.name + "_Inner", item);
			}
			else if (item.Boolean_2)
			{
				InitWearParams(gameObject, item);
			}
			else if (item.Boolean_3)
			{
				InitConsumableParams(gameObject, item);
			}
		}
	}

	private void UnloadCache()
	{
		Dictionary_0.Clear();
		Dictionary_1.Clear();
		Dictionary_2.Clear();
	}

	public GameObject GetGameObject(int int_0)
	{
		GameObject value = null;
		string text = string.Empty;
		if (Dictionary_1.ContainsKey(int_0))
		{
			text = Dictionary_1[int_0];
		}
		if (string.IsNullOrEmpty(text))
		{
			return value;
		}
		Dictionary_0.TryGetValue(text, out value);
		if (value != null)
		{
			WearData wear = WearController.WearController_0.GetWear(int_0);
			if (wear != null && wear.Boolean_2 && wear.Boolean_0)
			{
				CustomCapePicker component = value.GetComponent<CustomCapePicker>();
				if (component != null)
				{
					component.artikulId = int_0;
				}
			}
		}
		return value;
	}

	public GameObject GetInnerGameObject(int int_0)
	{
		GameObject value = null;
		Dictionary_2.TryGetValue(int_0, out value);
		return value;
	}

	private void InitInnerPrefab(string string_2, ArtikulData artikulData_0)
	{
		GameObject gameObject = Resources.Load<GameObject>(string_2);
		if (gameObject != null)
		{
			Dictionary_2.Add(artikulData_0.Int32_0, gameObject);
		}
	}

	private void InitWearParams(GameObject gameObject_0, ArtikulData artikulData_0)
	{
		ShopPositionParams component = gameObject_0.GetComponent<ShopPositionParams>();
		if (!(component == null))
		{
			component.Boolean_0 = true;
			component.prefabName = artikulData_0.String_3;
		}
	}

	private void InitConsumableParams(GameObject gameObject_0, ArtikulData artikulData_0)
	{
		ShopPositionParams component = gameObject_0.GetComponent<ShopPositionParams>();
		if (!(component == null))
		{
			List<ArtikulData> downgrades = ArtikulController.ArtikulController_0.GetDowngrades(artikulData_0.Int32_0);
			string prefabName = string.Format("{0}_Up_{1}", artikulData_0.String_3, downgrades.Count);
			component.Boolean_0 = false;
			component.prefabName = prefabName;
		}
	}
}
