using System;
using System.Collections.Generic;
using System.Linq;
using engine.helpers;
using UnityEngine;

public sealed class ShopArtikulController
{
	public enum SourceBuyType
	{
		TYPE_SHOP_WND = 0,
		TYPE_LVLUP_WND = 1,
		TYPE_KILLCAM_WND = 2,
		TYPE_REBUY_WND = 3
	}

	private const string string_0 = "new_artikuls_list";

	private static ShopArtikulController shopArtikulController_0;

	private HashSet<int> hashSet_0 = new HashSet<int>();

	public static ShopArtikulController ShopArtikulController_0
	{
		get
		{
			if (shopArtikulController_0 == null)
			{
				shopArtikulController_0 = new ShopArtikulController();
			}
			return shopArtikulController_0;
		}
	}

	private ShopArtikulController()
	{
	}

	public List<ShopArtikulData> shopArtikuls
	{
		get
		{
			if (_shopArtikuls == null)
			{
				List<ShopArtikulData> shopArtikulData = new List<ShopArtikulData>();
				ShopArticle shopArticle = Resources.Load<ShopArticle>("ShopArticle");

				foreach (ShopArticle.ShopArticleData articleData in shopArticle.shopArticleData)
				{
					shopArtikulData.Add(articleData.ToShopArtikul());
				}

				_shopArtikuls = shopArtikulData;
			}

			return _shopArtikuls;
		}
	}

	private List<ShopArtikulData> _shopArtikuls;

	public ShopArtikulData GetShopArtikul(int int_0)
	{
		return shopArtikuls.Find(x => x.Int32_0 == int_0);
	}

	public List<ShopArtikulData> GetShopArtikulsByArtikulId(int int_0)
	{
		return ShopArtikulStorage.Get.Storage.Search(0, int_0);
	}

	public List<ShopArtikulData> GetShopArtikulsBySlot(SlotType slotType_0)
	{
		Debug.LogError(shopArtikuls.FindAll(x => x.SlotType_0 == slotType_0).Count);
		return shopArtikuls.FindAll(x => x.SlotType_0 == slotType_0);
	}

	public List<ShopArtikulData> GetShopArtikulsBest()
	{
		return ShopArtikulStorage.Get.Bests;
	}

	public List<ShopArtikulData> GetShopArtikulsConsumable()
	{
		List<ShopArtikulData> list = new List<ShopArtikulData>();
		for (SlotType slotType = SlotType.SLOT_CONSUM_POTION; slotType <= SlotType.SLOT_CONSUM_GRENADE; slotType++)
		{
			List<ShopArtikulData> shopArtikulsBySlot = GetShopArtikulsBySlot(slotType);
			if (shopArtikulsBySlot != null && shopArtikulsBySlot.Count > 0)
			{
				list.AddRange(shopArtikulsBySlot);
			}
		}
		return list;
	}

	public List<ShopArtikulData> GetValidShopListBySlot(SlotType slotType_0)
	{
		List<ShopArtikulData> shopArtikulsBySlot = GetShopArtikulsBySlot(slotType_0);
		return GetValidShopList(shopArtikulsBySlot);
	}

	public List<ShopArtikulData> GetValidShopList(List<ShopArtikulData> list_0)
	{
		List<ShopArtikulData> list = new List<ShopArtikulData>();
		if (list_0 != null && list_0.Count > 0)
		{
			NeedData needData_ = null;
			foreach (ShopArtikulData item in list_0)
			{
				bool flag2;
				bool flag;
				if (flag2 = (flag = UserController.UserController_0.HasUserArtikul(item.Int32_1)) || (item.Boolean_3 && (item.NeedsData_0 == null || item.NeedsData_0.Check(out needData_)) && !UserController.UserController_0.HasAnyUpgrade(item.Int32_1)))
				{
					flag2 = (flag && ArtikulController.ArtikulController_0.CheckNeeds(item.Int32_1, out needData_)) || !flag;
				}
				bool flag3 = item.Boolean_5 || item.GetArtikul().Boolean_0;
				//if ((flag2 && !flag3) || flag)
				//{
					list.Add(item);
				//}
			}
		}
		return list;
	}

	public List<ShopArtikulData> GetValidShopListConsumable()
	{
		List<ShopArtikulData> list = new List<ShopArtikulData>();
		List<ShopArtikulData> shopArtikulsConsumable = GetShopArtikulsConsumable();
		if (shopArtikulsConsumable != null && shopArtikulsConsumable.Count > 0)
		{
			NeedData needData_ = null;
			foreach (ShopArtikulData item in shopArtikulsConsumable)
			{
				if (UserController.UserController_0.HasUserArtikul(item.Int32_1) || ((item.NeedsData_0 == null || item.NeedsData_0.Check(out needData_)) && !UserController.UserController_0.HasAnyUpgrade(item.Int32_1)))
				{
					list.Add(item);
				}
			}
		}
		return list;
	}

	public bool CheckNeeds(int int_0, NeedData needData_0)
	{
		ShopArtikulData shopArtikul = GetShopArtikul(int_0);
		if (shopArtikul == null)
		{
			Log.AddLine(string.Format("ShopArtikulController::CheckNeeds > shop artikul {0} is null", int_0));
			needData_0 = null;
			return false;
		}
		if (shopArtikul.NeedsData_0 == null)
		{
			Log.AddLine(string.Format("ShopArtikulController::CheckNeeds > needs for shop artikul {0} is null", int_0));
			needData_0 = null;
			return false;
		}
		return shopArtikul.NeedsData_0.Check(out needData_0);
	}

	public bool CanShopArtikulBuy(int int_0)
	{
		ShopArtikulData shopArtikul = GetShopArtikul(int_0);
		if (shopArtikul == null)
		{
			Log.AddLine(string.Format("ShopArtikulController::CanShopArtikulBuy > shop artikul {0} is null", int_0));
			return false;
		}
		return shopArtikul.CanBuy();
	}

	public void BuyArtikul(int int_0, bool bool_0 = true, Action action_0 = null, SourceBuyType sourceBuyType_0 = SourceBuyType.TYPE_SHOP_WND)
	{
		ShopArtikulData shopArtikul = GetShopArtikul(int_0);
		if (shopArtikul == null)
		{
			Log.AddLine(string.Format("ShopArtikulController::BuyArtikul > shop artikul {0} is null", int_0));
		}
		else if (BankController.BankController_0.CanBuy(int_0))
		{
			shopArtikul.Buy(bool_0, sourceBuyType_0);
		}
		else if (action_0 != null)
		{
			action_0();
		}
	}

	public void BuyArtikulUpgrade(int int_0)
	{
		ShopArtikulData shopArtikul = GetShopArtikul(int_0);
		if (shopArtikul == null)
		{
			Log.AddLine(string.Format("ShopArtikulController::BuyArtikulUpgrade > shop artikul {0} is null", int_0));
		}
		else
		{
			shopArtikul.BuyUpgrade();
		}
	}

	public bool isArtikulNew(int int_0)
	{
		return hashSet_0.Contains(int_0);
	}

	public void NotNew(int int_0)
	{
		if (hashSet_0.Contains(int_0))
		{
			hashSet_0.Remove(int_0);
			Save();
		}
	}

	public void Init()
	{
		hashSet_0.Clear();
		string value = SharedSettings.SharedSettings_0.GetValue("new_artikuls_list", string.Empty);
		if (string.IsNullOrEmpty(value))
		{
			return;
		}
		string[] array = value.Split(',');
		if (array == null || array.Length == 0)
		{
			return;
		}
		for (int i = 0; i < array.Length; i++)
		{
			int result = 0;
			int.TryParse(array[i], out result);
			if (ArtikulController.ArtikulController_0.GetArtikul(result) != null)
			{
				hashSet_0.Add(result);
			}
		}
	}

	public void OnLevelUp(int int_0)
	{
		List<int> newLevelArtikuls = getNewLevelArtikuls(int_0);
		if (newLevelArtikuls.Count != 0)
		{
			for (int i = 0; i < newLevelArtikuls.Count; i++)
			{
				hashSet_0.Add(newLevelArtikuls[i]);
			}
			Save();
			ShopArtikulStorage.Get.InitBestIndex();
		}
	}

	private void Save()
	{
		string text = string.Empty;
		foreach (int item in hashSet_0)
		{
			text = text + item + ",";
		}
		SharedSettings.SharedSettings_0.SetValue("new_artikuls_list", text, true);
	}

	private List<int> getNewLevelArtikuls(int int_0)
	{
		List<int> list = new List<int>();
		List<ShopArtikulData> list2 = ShopArtikulStorage.Get.Storage.Search(2, int_0);
		if (list2 != null && list2.Count > 0)
		{
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < list2.Count; i++)
			{
				hashSet.Add(list2[i].Int32_1);
			}
			foreach (int item in hashSet)
			{
				list.Add(item);
			}
		}
		return list;
	}
}
