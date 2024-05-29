using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using engine.events;
using engine.helpers;

public class StocksBaseTableObject : MonoBehaviour
{
	public UILabel title;

	public UILabel title2;

	public UILabel shortDescription;

	public UITexture icon;

	public UITexture bannerBkg;

	public UILabel timerLabel;

	public UISprite freeSticker;

	public UIWidget RedStripe;

	private ActionData actionData_0;

	private UserOverrideContentGroupData userOverrideContentGroupData_0;

	private ContentGroupData contentGroupData_0;

	private int int_0;

	private ShopArtikulData shopArtikulData_0;

	private string string_0;

	private string string_1;

	private string string_2;

	private string string_3;

	public void SetData(int int_1)
	{
		string_0 = Localizer.Get("ui.day.mini");
		string_1 = Localizer.Get("ui.hour.mini");
		string_2 = Localizer.Get("ui.min.mini");
		string_3 = Localizer.Get("ui.sec.mini");
		userOverrideContentGroupData_0 = StocksController.StocksController_0.GetActiveStock(int_1, out actionData_0, out contentGroupData_0);
		title.String_0 = Localizer.Get(actionData_0.string_0);
		if (title2 != null)
		{
			title2.String_0 = Localizer.Get(actionData_0.string_0);
		}
		shortDescription.String_0 = Localizer.Get(actionData_0.string_2);
		if (!actionData_0.string_3.IsNullOrEmpty())
		{
			bannerBkg.Texture_0 = ImageLoader.LoadTexture(actionData_0.string_3);
		}
		string empty = string.Empty;
		if (icon != null)
		{
			icon.Texture_0 = Resources.Load(empty) as Texture;
		}
		if (actionData_0.stockWndType_0 == StockWndType.GACHA)
		{
			List<ContentGroupDataItem> itemsByType = contentGroupData_0.GetItemsByType(ContentGroupItemType.SHOP);
			shopArtikulData_0 = ShopArtikulController.ShopArtikulController_0.GetShopArtikul(itemsByType[0].int_0);
		}
		showTimer(!actionData_0.bool_0);
		int_0 = Mathf.FloorToInt((float)((double)userOverrideContentGroupData_0.int_2 - Utility.Double_0)) + 1;
		UpdateOneS();
		if (!DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateOneS))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(UpdateOneS);
		}
	}

	private void OnDestroy()
	{
		if (DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateOneS))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(UpdateOneS);
		}
	}

	private void UpdateOneS()
	{
		int_0--;
		if (int_0 > 86400)
		{
			timerLabel.String_0 = string.Format(Localizer.Get("ui.lobby.stock.timer_title"), Utility.GetLocalizedTime(int_0, string_0, string_1, string_2, string_3));
		}
		else
		{
			timerLabel.String_0 = string.Format(Localizer.Get("ui.lobby.stock.timer_title"), Utility.GetLocalizedTime(int_0, string_0, string_1, string_2, string_3));
		}
		if (actionData_0.stockWndType_0 == StockWndType.GACHA && shopArtikulData_0 != null && freeSticker != null)
		{
			freeSticker.gameObject.SetActive(Mathf.FloorToInt((float)(UserController.UserController_0.GetTimerForFreeBuy(UserTimerData.UserTimerType.USER_TIMER_GACHA, shopArtikulData_0.Int32_0) + (double)shopArtikulData_0.Int32_8 - Utility.Double_0)) <= 0);
		}
		else
		{
			freeSticker.gameObject.SetActive(false);
		}
	}

	private void OnClick()
	{
		if (actionData_0 == null)
		{
			return;
		}
		switch (actionData_0.stockWndType_0)
		{
		case StockWndType.BANK:
			if (Lobby.Lobby_0 != null)
			{
				Lobby.Lobby_0.OnMoneyButtonClick();
			}
			break;
		case StockWndType.CLANS:
			if (!ClanController.ClanController_0.Boolean_0)
			{
				MessageWindow.Show(new MessageWindowParams(Localizer.Get("clan.message.clan_not_available")));
			}
			else if (ClanController.ClanController_0.UserClanData_0 == null)
			{
				ClanTopWindowParams clanTopWindowParams = new ClanTopWindowParams();
				clanTopWindowParams.Boolean_0 = true;
				ClanTopWindow.Show(clanTopWindowParams);
			}
			else
			{
				ClanWindow.Show();
			}
			break;
		case StockWndType.GACHA:
			StockGachaWindow.Show(new StockWindowParams(actionData_0.int_0));
			break;
		default:
			StockWindow.Show(new StockWindowParams(actionData_0.int_0));
			break;
		case StockWndType.MAIL_VERIFICATION:
		{
			string arg = AppController.AppController_0.ProcessArguments_0.String_1;
			Application.OpenURL(string.Format("{0}verifyemail?email={1}", AppController.AppController_0.String_2, arg));
			break;
		}
		case StockWndType.WEAPON_SALE:
			StockWeaponSaleWindow.Show(new StockWindowParams(actionData_0.int_0));
			break;
		}
	}

	private void showTimer(bool bool_0)
	{
		if (timerLabel != null && timerLabel.gameObject.activeSelf != bool_0)
		{
			timerLabel.gameObject.SetActive(bool_0);
		}
		if (RedStripe != null && RedStripe.gameObject.activeSelf != bool_0)
		{
			RedStripe.gameObject.SetActive(bool_0);
		}
	}
}
