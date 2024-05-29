using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.events;
using engine.helpers;
using engine.unity;

[GameWindowParams(GameWindowType.Stock)]
public class StockWindow : BaseGameWindow
{
	private static StockWindow stockWindow_0;

	public GameObject[] gameObject_0;

	public UILabel uilabel_0;

	public UILabel uilabel_1;

	public UILabel uilabel_2;

	public UIGrid uigrid_0;

	public UIScrollView uiscrollView_0;

	public StockWindowMap stockWindowMap_0;

	public UIButton uibutton_0;

	public UIButton uibutton_1;

	public UIButton uibutton_2;

	public UIGrid uigrid_1;

	public UIScrollView uiscrollView_1;

	public StockWindowItem stockWindowItem_0;

	public UIButton uibutton_3;

	public UIButton uibutton_4;

	public UIWidget uiwidget_0;

	private bool bool_1 = true;

	private int int_0;

	private ActionData actionData_0;

	private int int_1;

	private SelectMapWindowItem selectMapWindowItem_0;

	private List<ModeData> list_0 = new List<ModeData>();

	private int int_2;

	private List<ShopArtikulData> list_1 = new List<ShopArtikulData>();

	private int int_3;

	private List<StockWindowItem> list_2 = new List<StockWindowItem>();

	private string string_0;

	private string string_1;

	private string string_2;

	private string string_3;

	[CompilerGenerated]
	private static Action action_0;

	public static StockWindow StockWindow_0
	{
		get
		{
			return stockWindow_0;
		}
	}

	public static void Show(StockWindowParams stockWindowParams_0)
	{
		if (!(stockWindow_0 != null))
		{
			stockWindow_0 = BaseWindow.Load("StockWindow") as StockWindow;
			stockWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			stockWindow_0.Parameters_0.bool_5 = true;
			stockWindow_0.Parameters_0.bool_0 = false;
			stockWindow_0.Parameters_0.bool_6 = true;
			stockWindow_0.InternalShow(stockWindowParams_0);
		}
	}

	public override void OnShow()
	{
		UserOverrideContentGroupStorage.Subscribe(OverrideContentGroupEventType.UPDATE_ALL, CloseWndOnFinishStock);
		UserOverrideContentGroupStorage.Subscribe(OverrideContentGroupEventType.REMOVE_ALL, CloseWndOnFinishStock);
		Init();
		base.OnShow();
	}

	public override void OnHide()
	{
		base.OnHide();
		stockWindow_0 = null;
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
		if (!StocksController.StocksController_0.IsStockActive(int_0))
		{
			Hide();
		}
	}

	private void Init()
	{
		string_0 = Localizer.Get("ui.day.mini");
		string_1 = Localizer.Get("ui.hour.mini");
		string_2 = Localizer.Get("ui.min.mini");
		string_3 = Localizer.Get("ui.sec.mini");
		base.transform.localPosition = new Vector3(0f, 0f, 300f);
		WindowController.WindowController_0.WindowFader_0.transform.localPosition = new Vector3(0f, 0f, 300f);
		StockWindowParams stockWindowParams = base.WindowShowParameters_0 as StockWindowParams;
		int_0 = stockWindowParams.int_0;
		ContentGroupData contentGroupData_ = null;
		UserOverrideContentGroupData activeStock = StocksController.StocksController_0.GetActiveStock(int_0, out actionData_0, out contentGroupData_);
		uilabel_0.String_0 = Localizer.Get(actionData_0.string_0);
		uilabel_1.String_0 = Localizer.Get(actionData_0.string_1);
		if (actionData_0.bool_0)
		{
			showTimer(false);
		}
		else
		{
			showTimer(true);
			int_1 = Mathf.FloorToInt((float)((double)activeStock.int_2 - Utility.Double_0)) + 1;
			UpdateOneS();
			if (!DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateOneS))
			{
				DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(UpdateOneS);
			}
		}
		GameObject[] array = gameObject_0;
		foreach (GameObject gameObject in array)
		{
			gameObject.SetActive(false);
		}
		switch (actionData_0.stockWndType_0)
		{
		case StockWndType.NEW_YEAR:
			gameObject_0[0].SetActive(true);
			break;
		case StockWndType.KUNG_FU_PANDA:
			gameObject_0[1].SetActive(true);
			break;
		case StockWndType.VALENTINE_DAY:
			gameObject_0[2].SetActive(true);
			break;
		case StockWndType.BATMAN:
			gameObject_0[3].SetActive(true);
			break;
		}
		list_0.Clear();
		List<ContentGroupDataItem> itemsByType = contentGroupData_.GetItemsByType(ContentGroupItemType.MODE);
		for (int j = 0; j < itemsByType.Count; j++)
		{
			ModeData mode = ModesController.ModesController_0.GetMode(itemsByType[j].int_0);
			if (mode != null && !mode.Boolean_3)
			{
				list_0.Add(mode);
			}
		}
		if (list_0.Count > 0)
		{
			UpdateMaps();
		}
		int_2 = 0;
		UpdateMapAroows();
		list_1.Clear();
		List<ContentGroupDataItem> itemsByType2 = contentGroupData_.GetItemsByType(ContentGroupItemType.SHOP);
		for (int k = 0; k < itemsByType2.Count; k++)
		{
			ShopArtikulData shopArtikul = ShopArtikulController.ShopArtikulController_0.GetShopArtikul(itemsByType2[k].int_0);
			if (shopArtikul != null)
			{
				list_1.Add(shopArtikul);
			}
		}
		if (list_1.Count > 0)
		{
			UpdateItems();
		}
		int_3 = 3;
		UpdateItemAroows();
	}

	private void UpdateOneS()
	{
		if (!(uilabel_2 == null))
		{
			int_1--;
			if (int_1 > 86400)
			{
				uilabel_2.String_0 = string.Format(Localizer.Get("ui.lobby.stock.timer_title"), Utility.GetLocalizedTime(int_1, string_0, string_1, string_2, string_3));
			}
			else
			{
				uilabel_2.String_0 = string.Format(Localizer.Get("ui.lobby.stock.timer_title"), Utility.GetLocalizedTime(int_1, string_0, string_1, string_2, string_3));
			}
		}
	}

	private void UpdateMaps()
	{
		ClearMapItems();
		int num = 0;
		for (int i = 0; i < list_0.Count; i++)
		{
			ModeData mode = list_0[i];
			GameObject gameObject = NGUITools.AddChild(uigrid_0.gameObject, stockWindowMap_0.gameObject);
			gameObject.name = string.Format("{0:0000}", num++);
			StockWindowMap component = gameObject.GetComponent<StockWindowMap>();
			component.mode = mode;
			NGUITools.SetActive(gameObject, true);
		}
		RepositionMapContent();
	}

	private void ClearMapItems()
	{
		selectMapWindowItem_0 = null;
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

	private void RepositionMapContent()
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

	public void ScrollMapLeft()
	{
		if (int_2 != 0)
		{
			int num = (int)uigrid_0.float_0;
			Vector3 zero = Vector3.zero;
			zero.x += num;
			uiscrollView_0.MoveRelative(zero);
			int_2--;
			UpdateMapAroows();
		}
	}

	public void ScrollMapRight()
	{
		if (int_2 != list_0.Count - 1)
		{
			int num = (int)uigrid_0.float_0;
			Vector3 zero = Vector3.zero;
			zero.x -= num;
			uiscrollView_0.MoveRelative(zero);
			int_2++;
			UpdateMapAroows();
		}
	}

	private void UpdateMapAroows()
	{
		uibutton_1.Boolean_0 = int_2 > 0;
		uibutton_2.Boolean_0 = int_2 < list_0.Count - 1;
		NGUITools.SetActive(uibutton_1.gameObject, list_0.Count > 1);
		NGUITools.SetActive(uibutton_2.gameObject, list_0.Count > 1);
	}

	private void UpdateItems()
	{
		ClearItems();
		int num = 0;
		for (int i = 0; i < list_1.Count; i++)
		{
			GameObject gameObject = NGUITools.AddChild(uigrid_1.gameObject, stockWindowItem_0.gameObject);
			gameObject.name = string.Format("{0:0000}", num++);
			StockWindowItem component = gameObject.GetComponent<StockWindowItem>();
			component.articul = list_1[i];
			component.wndType = actionData_0.stockWndType_0;
			TooltipInfo component2 = gameObject.GetComponent<TooltipInfo>();
			ArtikulData artikul = list_1[i].GetArtikul();
			if (artikul.SlotType_0 >= SlotType.SLOT_WEAPON_PRIMARY && artikul.SlotType_0 <= SlotType.SLOT_WEAPON_SNIPER)
			{
				component2.weaponID = artikul.Int32_0;
			}
			else if (artikul.SlotType_0 >= SlotType.SLOT_WEAR_HAT && artikul.SlotType_0 <= SlotType.SLOT_WEAR_BOOTS)
			{
				component2.wearID = artikul.Int32_0;
			}
			NGUITools.SetActive(gameObject, true);
			list_2.Add(component);
		}
		if (list_2.Count < 4)
		{
			int int32_ = stockWindowItem_0.gameObject.GetComponent<UIWidget>().Int32_0;
			Vector3 localPosition = uiscrollView_1.transform.localPosition;
			localPosition.x += ((float)uiscrollView_1.transform.parent.gameObject.GetComponent<UIWidget>().Int32_0 - (float)(list_2.Count * int32_)) * 0.5f;
			uiscrollView_1.transform.localPosition = localPosition;
		}
		RepositionContent();
	}

	private void ClearItems()
	{
		list_2.Clear();
		BetterList<Transform> childList = uigrid_1.GetChildList();
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
		uigrid_1.Reposition();
		if (bool_1)
		{
			bool_1 = false;
		}
		else
		{
			uiscrollView_1.ResetPosition();
		}
	}

	public void ScrollItemLeft()
	{
		if (int_3 != 3)
		{
			int num = (int)uigrid_1.float_0;
			Vector3 zero = Vector3.zero;
			zero.x += num;
			uiscrollView_1.MoveRelative(zero);
			int_3--;
			UpdateItemAroows();
		}
	}

	public void ScrollItemRight()
	{
		if (int_3 != list_1.Count - 1)
		{
			int num = (int)uigrid_1.float_0;
			Vector3 zero = Vector3.zero;
			zero.x -= num;
			uiscrollView_1.MoveRelative(zero);
			int_3++;
			UpdateItemAroows();
		}
	}

	private void UpdateItemAroows()
	{
		uibutton_3.Boolean_0 = int_3 > 3;
		uibutton_4.Boolean_0 = int_3 < list_1.Count - 1;
		NGUITools.SetActive(uibutton_3.gameObject, list_1.Count > 4);
		NGUITools.SetActive(uibutton_4.gameObject, list_1.Count > 4);
	}

	public void OnGoToShop()
	{
		StockWindowItem stockWindowItem = null;
		int num = 0;
		for (int i = 0; i < list_2.Count; i++)
		{
			if (!UserController.UserController_0.HasUserArtikul(list_2[i].articul.GetArtikul().Int32_0) && (list_2[i].articul.NeedsData_0 == null || list_2[i].articul.NeedsData_0.Check()) && (list_2[i].articul.GetArtikul() == null || list_2[i].articul.GetArtikul().NeedsData_0 == null || list_2[i].articul.GetArtikul().NeedsData_0.Check()) && list_2[i].articul.GetPrice() > num)
			{
				stockWindowItem = list_2[i];
				num = list_2[i].articul.GetPrice();
			}
		}
		if (stockWindowItem != null)
		{
			stockWindowItem.SendMessage("OnClick");
			return;
		}
		StockWindow_0.Hide();
		Lobby.Lobby_0.Hide();
		ShopWindowParams shopWindowParams = new ShopWindowParams();
		shopWindowParams.action_0 = delegate
		{
			Lobby.Lobby_0.Show();
		};
		shopWindowParams.openStyle_0 = ShopWindow.OpenStyle.ANIMATED;
		shopWindowParams.sourceBuyType_0 = ShopArtikulController.SourceBuyType.TYPE_SHOP_WND;
		ShopWindow.Show(shopWindowParams);
	}

	private void showTimer(bool bool_2)
	{
		if (uilabel_2 != null && uilabel_2.gameObject.activeSelf != bool_2)
		{
			uilabel_2.gameObject.SetActive(bool_2);
		}
		if (uiwidget_0 != null && uiwidget_0.gameObject.activeSelf != bool_2)
		{
			uiwidget_0.gameObject.SetActive(bool_2);
		}
	}
}
