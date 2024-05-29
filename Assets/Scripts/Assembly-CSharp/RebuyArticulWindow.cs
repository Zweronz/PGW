using System;
using System.Collections.Generic;
using UnityEngine;
using engine.controllers;
using engine.helpers;
using engine.unity;

[GameWindowParams(GameWindowType.RebuyArticulWindow)]
public class RebuyArticulWindow : BaseGameWindow
{
	public PersController persController_0;

	public PersRotator persRotator_0;

	public Transform transform_0;

	public ShopWeaponInfoPanel shopWeaponInfoPanel_0;

	public ShopItemButton shopItemButton_0;

	public ShopItemButton shopItemButton_1;

	public UILabel uilabel_0;

	public UILabel uilabel_1;

	public GameObject gameObject_0;

	public UILabel uilabel_2;

	public UILabel uilabel_3;

	private static HashSet<RebuyArticulWindow> hashSet_0 = new HashSet<RebuyArticulWindow>();

	private ShopArtikulData shopArtikulData_0;

	private RebuyArticulWindowParams.RebuyWndType rebuyWndType_0;

	private ArtikulData artikulData_0;

	private Action action_0;

	private string string_0 = Localizer.Get("ui.day.mini");

	private string string_1 = Localizer.Get("ui.hour.mini");

	private string string_2 = Localizer.Get("ui.min.mini");

	private string string_3 = Localizer.Get("ui.sec.mini");

	private List<GameObject> list_0 = new List<GameObject>();

	public static bool Boolean_1
	{
		get
		{
			return hashSet_0.Count > 0;
		}
	}

	public static void Show(RebuyArticulWindowParams rebuyArticulWindowParams_0 = null)
	{
		bool flag = false;
		if (Boolean_1 || RankTrophyChangeWindow.RankTrophyChangeWindow_0 != null || RankSeasonCompleteWindow.RankSeasonCompleteWindow_0 != null)
		{
			flag = true;
		}
		RebuyArticulWindow rebuyArticulWindow = BaseWindow.Load("RebuyArticulWindow") as RebuyArticulWindow;
		rebuyArticulWindow.Parameters_0.type_0 = ((!flag) ? WindowQueue.Type.New : WindowQueue.Type.Top);
		rebuyArticulWindow.Parameters_0.bool_5 = false;
		rebuyArticulWindow.Parameters_0.bool_0 = false;
		rebuyArticulWindow.Parameters_0.bool_6 = false;
		if ((AppStateController.AppStateController_0.States_0 == AppStateController.States.IN_BATTLE || AppStateController.AppStateController_0.States_0 == AppStateController.States.IN_BATTLE_OVER_WINDOW) && (BattleOverWindow.BattleOverWindow_0 == null || !BattleOverWindow.BattleOverWindow_0.Boolean_0) && rebuyArticulWindowParams_0.rebuyWndType_0 != RebuyArticulWindowParams.RebuyWndType.NEW_ITEM_WND)
		{
			rebuyArticulWindow.Parameters_0.gameEvent_0 = WindowController.GameEvent.BATTLE_OVER_WINDOW_SHOW;
			rebuyArticulWindow.Parameters_0.bool_4 = true;
		}
		hashSet_0.Add(rebuyArticulWindow);
		rebuyArticulWindow.InternalShow(rebuyArticulWindowParams_0);
	}

	public override void OnShow()
	{
		GameObject gameObject = ScreenController.ScreenController_0.GameObject_0;
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			GameObject gameObject2 = gameObject.transform.GetChild(i).gameObject;
			if (gameObject2.activeSelf && needDisableUIRootObject(gameObject2))
			{
				Vector3 localPosition = gameObject2.transform.localPosition;
				localPosition.z += 1000f;
				gameObject2.transform.localPosition = localPosition;
				list_0.Add(gameObject2);
			}
		}
		base.transform.localPosition = new Vector3(0f, 0f, 0f);
		Init();
		base.OnShow();
	}

	public override void OnHide()
	{
		for (int i = 0; i < list_0.Count; i++)
		{
			if (list_0[i] != null && list_0[i].activeSelf)
			{
				Vector3 localPosition = list_0[i].transform.localPosition;
				localPosition.z -= 1000f;
				list_0[i].transform.localPosition = localPosition;
			}
		}
		list_0.Clear();
		hashSet_0.Remove(this);
		UsersData.Unsubscribe(UsersData.EventType.ARTIKUL_CHANGED, OnInventoryUpdate);
		base.OnHide();
		if (action_0 != null)
		{
			action_0();
		}
	}

	private bool needDisableUIRootObject(GameObject gameObject_1)
	{
		if (string.Equals(gameObject_1.name, "NGUICamera"))
		{
			return false;
		}
		if (string.Equals(gameObject_1.name, "Cursor"))
		{
			return false;
		}
		if (string.Equals(gameObject_1.name, "TooltipWindow@Common"))
		{
			return false;
		}
		if (string.Equals(gameObject_1.name, "Fader@Common"))
		{
			return false;
		}
		return true;
	}

	private void Init()
	{
		persRotator_0.pers = transform_0;
		RebuyArticulWindowParams rebuyArticulWindowParams = base.WindowShowParameters_0 as RebuyArticulWindowParams;
		action_0 = rebuyArticulWindowParams.action_0;
		rebuyWndType_0 = rebuyArticulWindowParams.rebuyWndType_0;
		shopArtikulData_0 = ShopArtikulController.ShopArtikulController_0.GetShopArtikul(rebuyArticulWindowParams.int_0);
		artikulData_0 = ((rebuyWndType_0 == RebuyArticulWindowParams.RebuyWndType.NEW_ITEM_WND || rebuyWndType_0 == RebuyArticulWindowParams.RebuyWndType.COMMON) ? ArtikulController.ArtikulController_0.GetArtikul(rebuyArticulWindowParams.int_0) : shopArtikulData_0.GetArtikul());
		shopWeaponInfoPanel_0.SetData(artikulData_0);
		ResetPers();
		Invoke("SetWeapon", 0.1f);
		bool flag = UserController.UserController_0.GetUserArtikulByArtikulId(artikulData_0.Int32_0) != null && UserController.UserController_0.GetUserArtikulByArtikulId(artikulData_0.Int32_0).int_1 > 0;
		shopItemButton_0.SetVisible(artikulData_0.Int32_5 == 0 && !flag);
		shopItemButton_1.SetVisible(artikulData_0.Int32_5 > 0);
		if (rebuyWndType_0 == RebuyArticulWindowParams.RebuyWndType.NEW_ITEM_WND)
		{
			shopItemButton_0.SetVisible(false);
			shopItemButton_1.SetVisible(false);
			uilabel_0.String_0 = Localizer.Get("ui.rebuy_wnd.main_title_new");
			uilabel_2.String_0 = Localizer.Get("ui.shop.new_item");
		}
		else if (rebuyWndType_0 == RebuyArticulWindowParams.RebuyWndType.REBUY_WND)
		{
			shopItemButton_0.Init(shopArtikulData_0);
			shopItemButton_1.Init(shopArtikulData_0);
			uilabel_0.String_0 = Localizer.Get("ui.rebuy_wnd.main_title");
			uilabel_2.String_0 = Localizer.Get("ui.shop.buy_forever");
		}
		else if (rebuyWndType_0 == RebuyArticulWindowParams.RebuyWndType.COMMON)
		{
			shopItemButton_0.SetVisible(false);
			shopItemButton_1.SetVisible(false);
			uilabel_0.String_0 = Localizer.Get("ui.rebuy_wnd.main_title_common");
			uilabel_2.String_0 = Localizer.Get("ui.shop.common");
		}
		gameObject_0.gameObject.SetActive(artikulData_0.Int32_5 > 0 && rebuyWndType_0 != RebuyArticulWindowParams.RebuyWndType.COMMON);
		uilabel_2.gameObject.SetActive(!gameObject_0.gameObject.activeSelf);
		uilabel_3.gameObject.gameObject.SetActive(rebuyWndType_0 == RebuyArticulWindowParams.RebuyWndType.COMMON);
		uilabel_3.String_0 = Localizer.Get("ui.rebuy_wnd.buy_gacha");
		if (!flag)
		{
			UsersData.Subscribe(UsersData.EventType.ARTIKUL_CHANGED, OnInventoryUpdate);
		}
		uilabel_1.String_0 = Utility.GetLocalizedTime(artikulData_0.Int32_5, string_0, string_1, string_2, string_3, false);
	}

	private new void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Escape))
		{
			Hide();
		}
	}

	public void ResetPers()
	{
		if (persController_0 != null)
		{
			persController_0.ReinitLobbyToProfile(UserController.UserController_0.UserData_0.user_0.int_0);
		}
	}

	private void SetWeapon()
	{
		persController_0.SetItem(artikulData_0.Int32_0);
	}

	private void OnInventoryUpdate(UsersData.EventData eventData_0)
	{
		if (eventData_0.int_0 == artikulData_0.Int32_0)
		{
			NGUITools.SetActive(shopItemButton_0.gameObject, false);
			NGUITools.SetActive(shopItemButton_1.gameObject, false);
			NGUITools.SetActive(uilabel_3.gameObject, true);
			uilabel_3.String_0 = Localizer.Get("ui.shop.already_buyed");
			UsersData.Unsubscribe(UsersData.EventType.ARTIKUL_CHANGED, OnInventoryUpdate);
		}
	}

	public void OpenBank()
	{
		BankController.BankController_0.TryOpenBank(BankController.BankSourceType.BANK_SHOP);
	}
}
