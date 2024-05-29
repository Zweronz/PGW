using UnityEngine;
using engine.helpers;

public class ShopItemButton : MonoBehaviour
{
	public enum ShopItemButtonType
	{
		BUY = 0,
		EQUIP = 1,
		UNEQUIP = 2,
		EDIT_CUSTOM = 3,
		TESTING = 4
	}

	public UILabel label;

	public UISprite icon;

	public UILabel value;

	public ShopItemButtonType buttonType;

	public string localization;

	private bool bool_0;

	private ShopArtikulData shopArtikulData_0;

	private UIPlaySound uiplaySound_0;

	private void Start()
	{
		uiplaySound_0 = GetComponent<UIPlaySound>();
	}

	public void SetVisible(bool bool_1)
	{
		NGUITools.SetActive(base.gameObject, bool_1);
	}

	public void Init(ShopArtikulData shopArtikulData_1)
	{
		shopArtikulData_0 = shopArtikulData_1;
		if (buttonType == ShopItemButtonType.BUY)
		{
			icon.String_0 = ((shopArtikulData_1.MoneyType_0 != 0) ? "gems_ico" : "coin_ico");
			value.String_0 = shopArtikulData_1.GetPrice().ToString();
			if (shopArtikulData_1.Int32_4 > 1)
			{
				value.String_0 += string.Format("   x{0}", shopArtikulData_1.Int32_4);
			}
			if (UserController.UserController_0.GetMoneyByType(shopArtikulData_1.MoneyType_0) >= shopArtikulData_1.GetPrice())
			{
			}
		}
		if (!string.IsNullOrEmpty(localization))
		{
			label.String_0 = Localizer.Get(localization);
		}
		bool_0 = false;
	}

	private void OnClick()
	{
		if (shopArtikulData_0 == null)
		{
			Log.AddLine("ShopItemButton::OnClick > failed. ShopArtikulData is null", Log.LogLevel.WARNING);
			return;
		}
		if (uiplaySound_0 != null && Defs.Boolean_0)
		{
			uiplaySound_0.Play();
		}
		switch (buttonType)
		{
		case ShopItemButtonType.EQUIP:
			UserController.UserController_0.EquipArtikul(shopArtikulData_0.Int32_1);
			break;
		case ShopItemButtonType.UNEQUIP:
			UserController.UserController_0.UnequipArtikul(shopArtikulData_0.Int32_1);
			break;
		case ShopItemButtonType.EDIT_CUSTOM:
			SkinEditController.SkinEditController_0.EditCustomWear(shopArtikulData_0.Int32_1);
			break;
		case ShopItemButtonType.BUY:
		case ShopItemButtonType.TESTING:
			if (!bool_0)
			{
				bool_0 = true;
				UsersData.Subscribe(UsersData.EventType.INVENTORY_UPDATE, OnInventoryUpdate);
				bool flag = true;
				ShopArtikulController.SourceBuyType sourceBuyType_ = ShopArtikulController.SourceBuyType.TYPE_SHOP_WND;
				if (ShopWindow.ShopWindow_0 != null)
				{
					sourceBuyType_ = ShopWindow.ShopWindow_0.sourceBuyType;
				}
				else if (RebuyArticulWindow.Boolean_1)
				{
					sourceBuyType_ = ShopArtikulController.SourceBuyType.TYPE_REBUY_WND;
				}
				ShopArtikulController.ShopArtikulController_0.BuyArtikul(shopArtikulData_0.Int32_0, flag, delegate
				{
					bool_0 = false;
				}, sourceBuyType_);
			}
			break;
		}
	}

	private void OnInventoryUpdate(UsersData.EventData eventData_0)
	{
		bool_0 = false;
		UsersData.Unsubscribe(UsersData.EventType.INVENTORY_UPDATE, OnInventoryUpdate);
	}
}
