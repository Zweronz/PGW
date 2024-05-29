using System.Collections.Generic;
using engine.events;

public class BonusController
{
	private static BonusController bonusController_0;

	public static BonusController BonusController_0
	{
		get
		{
			return bonusController_0 ?? (bonusController_0 = new BonusController());
		}
	}

	public void Init()
	{
		if (!DependSceneEvent<EventApplyBonus, ApplyBonusNetworkCommand>.Contains(OnGachaBonusApplied))
		{
			DependSceneEvent<EventApplyBonus, ApplyBonusNetworkCommand>.GlobalSubscribe(OnGachaBonusApplied);
		}
	}

	private void OnGachaBonusApplied(ApplyBonusNetworkCommand applyBonusNetworkCommand_0)
	{
		if (applyBonusNetworkCommand_0.bonusType_0 == ApplyBonusNetworkCommand.BonusType.GACHA)
		{
			ActionData actionData_ = null;
			ContentGroupData contentGroupData_ = null;
			StocksController.StocksController_0.GetActiveStock(StockGachaWindow.StockGachaWindow_0.Int32_0, out actionData_, out contentGroupData_);
			StockGachaRewardWindow.Show(new StockGachaRewardWindowParams(applyBonusNetworkCommand_0, actionData_));
		}
	}

	public BonusData GetBonusById(int int_0)
	{
		return BonusDataStorage.Get.Storage.GetObjectByKey(int_0);
	}

	public void GetAllItemsFromBonus(int int_0, List<BonusItemData> list_0, bool bool_0 = true)
	{
		BonusData bonusById = GetBonusById(int_0);
		if (bonusById == null || (bonusById.needsData_0 != null && !bonusById.needsData_0.Check()))
		{
			return;
		}
		foreach (BonusItemData item in bonusById.list_0)
		{
			if (item.bonusItemType_0 == BonusItemData.BonusItemType.BONUS_ITEM_BONUS)
			{
				GetAllItemsFromBonus(item.int_1, list_0);
				continue;
			}
			bool flag = false;
			foreach (BonusItemData item2 in list_0)
			{
				if (item2.bonusItemType_0 == item.bonusItemType_0 && item2.int_1 == item.int_1)
				{
					flag = bool_0;
					break;
				}
			}
			if (flag)
			{
				continue;
			}
			if (item.bonusItemType_0 == BonusItemData.BonusItemType.BONUS_ITEM_ARTICUL)
			{
				int userArtikulCount = UserController.UserController_0.GetUserArtikulCount(item.int_1);
				ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(item.int_1);
				if (artikul != null && userArtikulCount >= artikul.Int32_3 && artikul.SlotType_0 < SlotType.SLOT_CONSUM_POTION && artikul.Int32_5 == 0)
				{
					continue;
				}
			}
			if ((item.needsData_0 != null && item.needsData_0.Check()) || item.needsData_0 == null)
			{
				list_0.Add(item);
			}
		}
	}
}
