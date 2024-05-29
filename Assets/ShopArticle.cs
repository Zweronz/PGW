using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopArticle")]
public class ShopArticle : ScriptableObject
{
	[System.Serializable]
	public class ShopArticleData
	{
		public int id, otherID;

		public SlotType slotType;

		public MoneyType moneyType;

		public int price;

		public ShopArtikulData ToShopArtikul()
		{
			return new ShopArtikulData
			{
				Int32_0 = id,
				Int32_1 = otherID,

				SlotType_0 = slotType,
				MoneyType_0 = moneyType,

				Int32_2 = price,
			};
		}
	}

	public List<ShopArticleData> shopArticleData = new List<ShopArticleData>();
}
