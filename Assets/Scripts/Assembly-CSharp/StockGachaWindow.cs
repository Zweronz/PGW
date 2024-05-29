using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.events;
using engine.helpers;
using engine.unity;

[GameWindowParams(GameWindowType.GachaWindow)]
public class StockGachaWindow : BaseGameWindow
{
	private static StockGachaWindow stockGachaWindow_0;

	public UILabel uilabel_0;

	public UILabel uilabel_1;

	public UILabel uilabel_2;

	public ShopItemButton shopItemButton_0;

	public ShopItemButton shopItemButton_1;

	public UIGrid uigrid_0;

	public UIScrollView uiscrollView_0;

	public StockWindowItem stockWindowItem_0;

	public UIButton uibutton_0;

	public UIButton uibutton_1;

	public UILabel uilabel_3;

	public UIWidget uiwidget_0;

	private bool bool_1 = true;

	private List<BonusItemData> list_0 = new List<BonusItemData>();

	private List<StockWindowItem> list_1 = new List<StockWindowItem>();

	private int int_0;

	private int int_1;

	private int int_2;

	private ShopArtikulData shopArtikulData_0;

	private bool bool_2;

	private UIPlaySound uiplaySound_0;

	private string string_0;

	private string string_1;

	private string string_2;

	private string string_3;

	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private static Comparison<BonusItemData> comparison_0;

	public static StockGachaWindow StockGachaWindow_0
	{
		get
		{
			return stockGachaWindow_0;
		}
	}

	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		set
		{
			int_3 = value;
		}
	}

	public static void Show(StockWindowParams stockWindowParams_0)
	{
		if (!(stockGachaWindow_0 != null))
		{
			stockGachaWindow_0 = BaseWindow.Load("StockGachaWindow") as StockGachaWindow;
			stockGachaWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			stockGachaWindow_0.Parameters_0.bool_5 = true;
			stockGachaWindow_0.Parameters_0.bool_0 = false;
			stockGachaWindow_0.Parameters_0.bool_6 = false;
			stockGachaWindow_0.InternalShow(stockWindowParams_0);
		}
	}

	public override void OnShow()
	{
		UserOverrideContentGroupStorage.Subscribe(OverrideContentGroupEventType.UPDATE_ALL, CloseWndOnFinishStock);
		UserOverrideContentGroupStorage.Subscribe(OverrideContentGroupEventType.REMOVE_ALL, CloseWndOnFinishStock);
		Init();
		base.OnShow();
		AddInputKey(KeyCode.Escape, delegate
		{
			if (base.Boolean_0)
			{
				Hide();
			}
		});
	}

	public override void OnHide()
	{
		base.OnHide();
		stockGachaWindow_0 = null;
		OnAnimComplete();
	}

	private void OnDestroy()
	{
		UserOverrideContentGroupStorage.Unsubscribe(OverrideContentGroupEventType.UPDATE_ALL, CloseWndOnFinishStock);
		UserOverrideContentGroupStorage.Unsubscribe(OverrideContentGroupEventType.REMOVE_ALL, CloseWndOnFinishStock);
		if (DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateOneS))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(UpdateOneS);
		}
	}

	private void CloseWndOnFinishStock(OverrideContentGroupEventData overrideContentGroupEventData_0)
	{
		if (!StocksController.StocksController_0.IsStockActive(Int32_0))
		{
			Hide();
		}
	}

	private void Start()
	{
		uiplaySound_0 = GetComponent<UIPlaySound>();
	}

	private void Init()
	{
		string_0 = Localizer.Get("ui.day.mini");
		string_1 = Localizer.Get("ui.hour.mini");
		string_2 = Localizer.Get("ui.min.mini");
		string_3 = Localizer.Get("ui.sec.mini");
		base.transform.localPosition = new Vector3(0f, 0f, 360f);
		WindowController.WindowController_0.WindowFader_0.transform.localPosition = new Vector3(0f, 0f, 360f);
		StockWindowParams stockWindowParams = base.WindowShowParameters_0 as StockWindowParams;
		Int32_0 = stockWindowParams.int_0;
		ActionData actionData_ = null;
		ContentGroupData contentGroupData_ = null;
		UserOverrideContentGroupData activeStock = StocksController.StocksController_0.GetActiveStock(Int32_0, out actionData_, out contentGroupData_);
		List<ContentGroupDataItem> itemsByType = contentGroupData_.GetItemsByType(ContentGroupItemType.SHOP);
		shopArtikulData_0 = ShopArtikulController.ShopArtikulController_0.GetShopArtikul(itemsByType[0].int_0);
		uilabel_0.String_0 = Localizer.Get(actionData_.string_0);
		uilabel_1.String_0 = Localizer.Get(actionData_.string_1);
		showTimer(!actionData_.bool_0);
		int_0 = Mathf.FloorToInt((float)((double)activeStock.int_2 - Utility.Double_0)) + 1;
		int_1 = Mathf.FloorToInt((float)(UserController.UserController_0.GetTimerForFreeBuy(UserTimerData.UserTimerType.USER_TIMER_GACHA, shopArtikulData_0.Int32_0) + (double)shopArtikulData_0.Int32_8 - Utility.Double_0));
		UpdateOneS();
		if (!DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateOneS))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(UpdateOneS);
		}
		shopItemButton_0.Init(shopArtikulData_0);
		shopItemButton_1.Init(shopArtikulData_0);
		list_0 = new List<BonusItemData>();
		BonusController.BonusController_0.GetAllItemsFromBonus(shopArtikulData_0.Int32_7, list_0);
		list_0.Sort((BonusItemData bonusItemData_0, BonusItemData bonusItemData_1) => bonusItemData_0.int_6.CompareTo(bonusItemData_1.int_6));
		if (list_0.Count > 0)
		{
			UpdateItems();
		}
		int_2 = 5;
		UpdateItemAroows();
		bool_2 = false;
		base.GetComponent<Animation>().Play("GachaChestIdle");
	}

	private void UpdateOneS()
	{
		int_0--;
		if (int_0 > 86400)
		{
			uilabel_2.String_0 = string.Format(Localizer.Get("ui.lobby.stock.timer_title"), Utility.GetLocalizedTime(int_0, string_0, string_1, string_2, string_3));
		}
		else
		{
			uilabel_2.String_0 = string.Format(Localizer.Get("ui.lobby.stock.timer_title"), Utility.GetLocalizedTime(int_0, string_0, string_1, string_2, string_3));
		}
		int_1--;
		if (int_1 > 86400)
		{
			uilabel_3.String_0 = string.Format(Localizer.Get("ui.lobby.stock.timer_to_free"), Utility.GetLocalizedTime(int_1, string_0, string_1, string_2, string_3));
		}
		else
		{
			uilabel_3.String_0 = string.Format(Localizer.Get("ui.lobby.stock.timer_to_free"), Utility.GetLocalizedTime(int_1, string_0, string_1, string_2, string_3));
		}
		if (int_1 <= 0)
		{
			uilabel_3.String_0 = Localizer.Get("ui.lobby.stock.timer_free");
		}
		if (!bool_2)
		{
			shopItemButton_1.SetVisible(int_1 <= 0);
			shopItemButton_0.SetVisible(int_1 > 0);
		}
	}

	private void UpdateItems()
	{
		ClearItems();
		int num = 0;
		for (int i = 0; i < list_0.Count; i++)
		{
			GameObject gameObject = NGUITools.AddChild(uigrid_0.gameObject, stockWindowItem_0.gameObject);
			gameObject.name = string.Format("{0:0000}", num++);
			StockWindowItem component = gameObject.GetComponent<StockWindowItem>();
			component.bonusArticul = list_0[i];
			TooltipInfo component2 = gameObject.GetComponent<TooltipInfo>();
			ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(list_0[i].int_1);
			if (list_0[i].bonusItemType_0 == BonusItemData.BonusItemType.BONUS_ITEM_ARTICUL)
			{
				if (artikul.SlotType_0 >= SlotType.SLOT_WEAPON_PRIMARY && artikul.SlotType_0 <= SlotType.SLOT_WEAPON_SNIPER)
				{
					component2.weaponID = list_0[i].int_1;
				}
				else if (artikul.SlotType_0 >= SlotType.SLOT_WEAR_HAT && artikul.SlotType_0 <= SlotType.SLOT_WEAR_BOOTS && artikul.SlotType_0 != SlotType.SLOT_WEAR_SKIN)
				{
					component2.wearID = list_0[i].int_1;
				}
				else if (artikul.SlotType_0 == SlotType.SLOT_WEAR_SKIN || (artikul.SlotType_0 >= SlotType.SLOT_CONSUM_POTION && artikul.SlotType_0 <= SlotType.SLOT_CONSUM_GRENADE))
				{
					component2.tooltipType = TooltipInfo.TooltipType.TOOLTIP_TYPE_ONLYTEXT;
					component2.text = "tooltip.slot." + artikul.SlotType_0;
				}
			}
			else if (list_0[i].bonusItemType_0 == BonusItemData.BonusItemType.BONUS_ITEM_MONEY)
			{
				component2.tooltipType = TooltipInfo.TooltipType.TOOLTIP_TYPE_ONLYTEXT;
				component2.text = "tooltip.slot.money";
			}
			else if (list_0[i].bonusItemType_0 == BonusItemData.BonusItemType.BONUS_ITEM_EXPIRIENCE)
			{
				component2.tooltipType = TooltipInfo.TooltipType.TOOLTIP_TYPE_ONLYTEXT;
				component2.text = "tooltip.slot.exp";
			}
			NGUITools.SetActive(gameObject, true);
			list_1.Add(component);
		}
		if (list_1.Count < 6)
		{
			int int32_ = stockWindowItem_0.gameObject.GetComponent<UIWidget>().Int32_0;
			Vector3 localPosition = uiscrollView_0.transform.localPosition;
			localPosition.x += ((float)uiscrollView_0.transform.parent.gameObject.GetComponent<UIWidget>().Int32_0 - (float)(list_1.Count * int32_)) * 0.5f;
			uiscrollView_0.transform.localPosition = localPosition;
		}
		RepositionContent();
	}

	private void ClearItems()
	{
		list_1.Clear();
		BetterList<Transform> childList = uigrid_0.GetChildList();
		foreach (Transform item in childList)
		{
			if (!(item == null))
			{
				item.parent = null;
				UnityEngine.Object.Destroy(item.gameObject);
			}
		}
	}

	private void RepositionContent()
	{
		uigrid_0.Reposition();
		if (bool_1)
		{
			bool_1 = false;
		}
		else
		{
			uiscrollView_0.ResetPosition();
		}
	}

	public void ScrollItemLeft()
	{
		if (int_2 != 5)
		{
			int num = (int)uigrid_0.float_0;
			Vector3 zero = Vector3.zero;
			zero.x += num;
			uiscrollView_0.MoveRelative(zero);
			int_2--;
			UpdateItemAroows();
		}
	}

	public void ScrollItemRight()
	{
		if (int_2 != list_0.Count - 1)
		{
			int num = (int)uigrid_0.float_0;
			Vector3 zero = Vector3.zero;
			zero.x -= num;
			uiscrollView_0.MoveRelative(zero);
			int_2++;
			UpdateItemAroows();
		}
	}

	private void UpdateItemAroows()
	{
		uibutton_0.Boolean_0 = int_2 > 5;
		uibutton_1.Boolean_0 = int_2 < list_0.Count - 1;
		NGUITools.SetActive(uibutton_0.gameObject, list_0.Count > 6);
		NGUITools.SetActive(uibutton_1.gameObject, list_0.Count > 6);
	}

	public void OnBuyClick()
	{
		if (bool_2)
		{
			return;
		}
		if (BankController.BankController_0.CanBuy(shopArtikulData_0.Int32_0))
		{
			if (uiplaySound_0 != null && Defs.Boolean_0)
			{
				uiplaySound_0.Play();
			}
			base.GetComponent<Animation>().Stop("GachaChestIdle");
			base.GetComponent<Animation>().Play("GachaChestOpen");
			uigrid_0.gameObject.SetActive(false);
			bool_2 = true;
			shopItemButton_0.SetVisible(false);
			shopItemButton_1.SetVisible(false);
			Invoke("OnAnimComplete", 1.1f);
		}
		else
		{
			Hide();
			OnAnimComplete();
		}
	}

	public void OnAnimComplete()
	{
		WindowController.WindowController_0.DispatchEvent(WindowController.GameEvent.STOCK_GACHA_WND_HIDE);
	}

	private void showTimer(bool bool_3)
	{
		if (uilabel_2 != null && uilabel_2.gameObject.activeSelf != bool_3)
		{
			uilabel_2.gameObject.SetActive(bool_3);
		}
		if (uiwidget_0 != null && uiwidget_0.gameObject.activeSelf != bool_3)
		{
			uiwidget_0.gameObject.SetActive(bool_3);
		}
	}
}
