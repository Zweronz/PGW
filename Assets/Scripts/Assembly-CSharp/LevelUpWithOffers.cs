using System.Collections.Generic;
using Rilisoft;
using UnityEngine;

public class LevelUpWithOffers : MonoBehaviour
{
	public struct ItemDesc
	{
		public string string_0;
	}

	public UIButton continueButton;

	public UIButton shopButton;

	public UILabel[] rewardGemsPriceLabel;

	public UILabel totalGemsLabel;

	public UILabel[] currentRankLabel;

	public UILabel[] rewardPriceLabel;

	public UILabel totalCoinsLabel;

	public NewAvailableItemInShop[] items;

	public UIGrid grid;

	public bool isTierLevelUp;

	public void SetCurrentRank(string string_0)
	{
		for (int i = 0; i < currentRankLabel.Length; i++)
		{
			currentRankLabel[i].String_0 = string_0;
		}
	}

	public void SetRewardPrice(string string_0)
	{
		for (int i = 0; i < rewardPriceLabel.Length; i++)
		{
			rewardPriceLabel[i].String_0 = string_0;
		}
	}

	public void SetGemsRewardPrice(string string_0)
	{
		for (int i = 0; i < rewardGemsPriceLabel.Length; i++)
		{
			rewardGemsPriceLabel[i].String_0 = string_0;
		}
	}

	public void SetItems(List<ArtikulData> list_0)
	{
		for (int i = 0; i < items.Length; i++)
		{
			items[i].gameObject.SetActive(false);
		}
		for (int j = 0; j < list_0.Count; j++)
		{
			ArtikulData artikulData = list_0[j];
			items[j].gameObject.SetActive(true);
			items[j].artikulId = artikulData.Int32_0;
			items[j].itemImage.Texture_0 = ImageLoader.LoadArtikulTexture(artikulData.Int32_0);
			items[j].itemName.String_0 = artikulData.String_4;
			items[j].GetComponent<UIButton>().Boolean_0 = true;
		}
		grid.transform.localPosition = new Vector3((0f - grid.float_0 * (float)list_0.Count) / 2f + grid.float_0 / 2f, grid.transform.localPosition.y, grid.transform.localPosition.z);
		grid.Reposition();
	}

	public void Init(int int_0, List<ArtikulData> list_0)
	{
		LevelData levelData = LevelStorage.Get.GetlLevelData(int_0);
		SetCurrentRank(int_0.ToString());
		SetRewardPrice("+" + levelData.Int32_2);
		totalCoinsLabel.String_0 = UserController.UserController_0.GetMoneyByType(MoneyType.MONEY_TYPE_COINS).ToString();
		if (list_0.Count > items.Length)
		{
			list_0.RemoveRange(items.Length, list_0.Count - items.Length);
		}
		SetItems(list_0);
	}
}
