using UnityEngine;
using engine.helpers;

public class SkinSelectItemButton : MonoBehaviour
{
	public enum SkinSelectItemButtonType
	{
		BUY = 0
	}

	public UILabel label;

	public UISprite icon;

	public UILabel value;

	public SkinSelectItemButtonType buttonType;

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
		if (buttonType == SkinSelectItemButtonType.BUY)
		{
			icon.String_0 = ((shopArtikulData_1.MoneyType_0 != 0) ? "gems_ico" : "coin_ico");
			int num = ((shopArtikulData_1.GetArtikul() != null) ? shopArtikulData_1.GetPrice() : 0);
			value.String_0 = num.ToString();
			if (shopArtikulData_1.Int32_4 > 1)
			{
				value.String_0 += string.Format("   x{0}", shopArtikulData_1.Int32_4);
			}
			if (UserController.UserController_0.GetMoneyByType(shopArtikulData_1.MoneyType_0) >= num)
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
			Log.AddLine("SkinSelectItemButton::OnClick > failed. ShopArtikulData is null", Log.LogLevel.WARNING);
			return;
		}
		if (uiplaySound_0 != null && Defs.Boolean_0)
		{
			uiplaySound_0.Play();
		}
		if (buttonType == SkinSelectItemButtonType.BUY && !bool_0)
		{
			bool_0 = true;
			UsersData.Subscribe(UsersData.EventType.INVENTORY_UPDATE, OnInventoryUpdate);
			ShopArtikulController.ShopArtikulController_0.BuyArtikul(shopArtikulData_0.Int32_0, false, delegate
			{
				bool_0 = false;
			});
		}
	}

	private void OnInventoryUpdate(UsersData.EventData eventData_0)
	{
		bool_0 = false;
		UsersData.Unsubscribe(UsersData.EventType.INVENTORY_UPDATE, OnInventoryUpdate);
	}
}
