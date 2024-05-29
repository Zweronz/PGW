using UnityEngine;

public class KillCamConsShopItem : MonoBehaviour
{
	public UITexture icon;

	public UILabel count;

	public UILabel countBuy;

	public UILabel price;

	public UIButton button;

	private ShopArtikulData shopArtikulData_0;

	private bool bool_0;

	public void SetData(ShopArtikulData shopArtikulData_1)
	{
		shopArtikulData_0 = shopArtikulData_1;
		icon.Texture_0 = ImageLoader.LoadArtikulTexture(shopArtikulData_1.Int32_1);
		price.String_0 = shopArtikulData_1.GetPrice().ToString();
		if (shopArtikulData_0.Int32_4 == 1)
		{
			countBuy.String_0 = string.Empty;
		}
		else
		{
			countBuy.String_0 = string.Format("x{0}", shopArtikulData_0.Int32_4);
		}
		UpdateCount();
	}

	private void UpdateCount()
	{
		if (shopArtikulData_0.GetArtikul().Int32_3 > 0)
		{
			count.String_0 = string.Format("{0}/{1}", UserController.UserController_0.GetUserArtikulCount(shopArtikulData_0.Int32_1), shopArtikulData_0.GetArtikul().Int32_3);
		}
		else
		{
			count.String_0 = string.Format("{0}", UserController.UserController_0.GetUserArtikulCount(shopArtikulData_0.Int32_1));
		}
	}

	public void OnBuyClick()
	{
		if (!bool_0)
		{
			bool_0 = true;
			UsersData.Subscribe(UsersData.EventType.INVENTORY_UPDATE, OnInventoryUpdate);
			ShopArtikulController.ShopArtikulController_0.BuyArtikul(shopArtikulData_0.Int32_0, true, delegate
			{
				bool_0 = false;
			}, ShopArtikulController.SourceBuyType.TYPE_KILLCAM_WND);
		}
	}

	private void OnInventoryUpdate(UsersData.EventData eventData_0)
	{
		bool_0 = false;
		UsersData.Unsubscribe(UsersData.EventType.INVENTORY_UPDATE, OnInventoryUpdate);
		if (!shopArtikulData_0.CanBuy())
		{
			button.gameObject.SetActive(false);
		}
		UpdateCount();
	}

	private void OnDestroy()
	{
		UsersData.Unsubscribe(UsersData.EventType.INVENTORY_UPDATE, OnInventoryUpdate);
	}
}
