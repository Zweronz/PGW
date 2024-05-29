using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class ShopArtikulStorage : BaseStorage<int, ShopArtikulData>
{
	public const int INDEX_ARTIKUL_ID = 0;

	public const int INDEX_ARTIKUL_SLOT_TYPE = 1;

	public const int INDEX_ARTIKUL_NEED_LEVEL = 2;

	private static ShopArtikulStorage _instance;

	public List<ShopArtikulData> Bests = new List<ShopArtikulData>();

	public static ShopArtikulStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new ShopArtikulStorage();
			}
			return _instance;
		}
	}

	private ShopArtikulStorage()
	{
	}

	protected override void OnAddIndexes()
	{
		Get.Storage.AddIndex(new HashCallbackIndex<int, ShopArtikulData>(delegate(ShopArtikulData data, HashIndex<int, ShopArtikulData> index)
		{
			index.Boolean_0 = data.Int32_1 == 0;
			if (index.Boolean_0)
			{
				index.Prop_0 = data.Int32_1;
			}
			return data.Int32_1;
		}));
		Get.Storage.AddIndex(new HashCallbackIndex<SlotType, ShopArtikulData>((ShopArtikulData data, HashIndex<SlotType, ShopArtikulData> index) => data.SlotType_0));
		Get.Storage.AddIndex(new HashCallbackIndex<int, ShopArtikulData>(delegate(ShopArtikulData data, HashIndex<int, ShopArtikulData> index)
		{
			int num = 0;
			if (data.NeedsData_0 != null && data.NeedsData_0.List_0 != null && data.NeedsData_0.List_0.Count > 0)
			{
				for (int i = 0; i < data.NeedsData_0.List_0.Count; i++)
				{
					NeedData needData = data.NeedsData_0.List_0[i];
					if (needData.NeedTypes_0 == NeedTypes.LEVEL && needData.Int32_0 > 0)
					{
						num = Mathf.Max(num, needData.Int32_0);
					}
					if (needData.NeedTypes_0 == NeedTypes.TIER && needData.Int32_2 > 0)
					{
						LevelTierData tierById = LevelStorage.Get.GetTierById(needData.Int32_2);
						if (tierById != null)
						{
							num = Mathf.Max(num, tierById.Int32_1);
						}
					}
				}
			}
			ArtikulData artikul = data.GetArtikul();
			if (artikul != null && artikul.NeedsData_0 != null && artikul.NeedsData_0.List_0 != null && artikul.NeedsData_0.List_0.Count > 0)
			{
				for (int j = 0; j < artikul.NeedsData_0.List_0.Count; j++)
				{
					NeedData needData2 = artikul.NeedsData_0.List_0[j];
					if (needData2.NeedTypes_0 == NeedTypes.LEVEL && needData2.Int32_0 > 0)
					{
						num = Mathf.Max(num, needData2.Int32_0);
					}
					if (needData2.NeedTypes_0 == NeedTypes.TIER && needData2.Int32_2 > 0)
					{
						LevelTierData tierById2 = LevelStorage.Get.GetTierById(needData2.Int32_2);
						if (tierById2 != null)
						{
							num = Mathf.Max(num, tierById2.Int32_1);
						}
					}
				}
			}
			return num;
		}));
	}

	public Dictionary<int, ShopArtikulData> shopArtikuls
	{
		get
		{
			if (_shopArtikuls == null)
			{
				Dictionary<int, ShopArtikulData> shopArtikulData = new Dictionary<int, ShopArtikulData>();
				ShopArticle shopArticle = Resources.Load<ShopArticle>("ShopArticle");

				foreach (ShopArticle.ShopArticleData articleData in shopArticle.shopArticleData)
				{
					shopArtikulData.Add(articleData.id, articleData.ToShopArtikul());
				}

				_shopArtikuls = shopArtikulData;
			}

			return _shopArtikuls;
		}
	}

	private Dictionary<int, ShopArtikulData> _shopArtikuls;

	public void InitBestIndex()
	{
		Bests.Clear();
		foreach (KeyValuePair<int, ShopArtikulData> item in shopArtikuls)//(IEnumerable<KeyValuePair<int, ShopArtikulData>>)Get.Storage)
		{
			if (item.Value.Boolean_7)
			{
				Bests.Add(item.Value);
			}
		}
	}
}
