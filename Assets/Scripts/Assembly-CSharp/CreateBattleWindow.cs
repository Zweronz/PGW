using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.CreateBattle)]
public class CreateBattleWindow : BaseGameWindow
{
	private static CreateBattleWindow createBattleWindow_0 = null;

	public UITable uitable_0;

	public UIScrollView uiscrollView_0;

	public UIWidget uiwidget_0;

	public CreateBattleWindowItem createBattleWindowItem_0;

	public UIInput uiinput_0;

	public UIPopupList uipopupList_0;

	public UILabel uilabel_0;

	public UITabsContentController uitabsContentController_0;

	public UILabel uilabel_1;

	public UIInput uiinput_1;

	public GameObject[] gameObject_0;

	public GameObject[] gameObject_1;

	public UISprite uisprite_0;

	public UISprite uisprite_1;

	public UIWidget uiwidget_1;

	private int int_0 = 3;

	private int int_1;

	private ModeType modeType_0 = ModeType.TEAM_FIGHT;

	private bool bool_1 = true;

	private CreateBattleWindowItem createBattleWindowItem_1;

	private int int_2;

	private static List<string> list_0 = new List<string>();

	[CompilerGenerated]
	private static Comparison<ModeData> comparison_0;

	public static CreateBattleWindow CreateBattleWindow_0
	{
		get
		{
			return createBattleWindow_0;
		}
	}

	public static void Show(CreateBattleWindowParams createBattleWindowParams_0 = null)
	{
		if (!(createBattleWindow_0 != null))
		{
			createBattleWindow_0 = BaseWindow.Load("CreateBattleWindow") as CreateBattleWindow;
			createBattleWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			createBattleWindow_0.Parameters_0.bool_5 = false;
			createBattleWindow_0.Parameters_0.bool_0 = false;
			createBattleWindow_0.Parameters_0.bool_6 = true;
			createBattleWindow_0.InternalShow(createBattleWindowParams_0);
		}
	}

	public override void OnShow()
	{
		Init();
		UserOverrideContentGroupStorage.Subscribe(OverrideContentGroupEventType.UPDATE_ALL, ForceUpdateTabContent);
		UserOverrideContentGroupStorage.Subscribe(OverrideContentGroupEventType.REMOVE_ALL, ForceUpdateTabContent);
		base.OnShow();
	}

	public override void OnHide()
	{
		UserOverrideContentGroupStorage.Unsubscribe(OverrideContentGroupEventType.UPDATE_ALL, ForceUpdateTabContent);
		UserOverrideContentGroupStorage.Unsubscribe(OverrideContentGroupEventType.REMOVE_ALL, ForceUpdateTabContent);
		base.OnHide();
		createBattleWindow_0 = null;
	}

	private void Init()
	{
		NGUITools.SetActive(uiwidget_1.gameObject, ClanController.ClanController_0.UserClanData_0 != null);
		if (ClanController.ClanController_0.UserClanData_0 == null)
		{
			uisprite_0.String_0 = "chat_big_bkg1-2";
			uisprite_1.String_0 = "chat_big_bkg2-2";
		}
		uitable_0.onReposition_0 = delegate
		{
			uiscrollView_0.UpdateScrollbars(true);
		};
		OnPlusBtnClick();
		fillModeTypes();
		for (int i = 0; i < list_0.Count; i++)
		{
			uipopupList_0.items.Add(list_0[i]);
		}
		uipopupList_0.String_0 = list_0[0];
		uitabsContentController_0.onTabActive = SetTabContent;
		setTeamNums(true);
		UpdateTable();
		createBattleWindowItem_0.gameObject.SetActive(false);
	}

	private void ForceUpdateTabContent(OverrideContentGroupEventData overrideContentGroupEventData_0)
	{
		UpdateTable();
	}

	private void UpdateTable()
	{
		ClearItems();
		List<ModeData> modesByType = ModesController.ModesController_0.GetModesByType(modeType_0);
		List<ModeData> list = new List<ModeData>(modesByType.ToArray());
		list.Sort((ModeData modeData_0, ModeData modeData_1) => modeData_0.Int32_5.CompareTo(modeData_1.Int32_5));
		HashSet<MapData> hashSet = new HashSet<MapData>();
		for (int i = 0; i < list.Count; i++)
		{
			MapData objectByKey = MapStorage.Get.Storage.GetObjectByKey(list[i].Int32_1);
			hashSet.Add(objectByKey);
		}
		int_1 = 0;
		foreach (MapData item in hashSet)
		{
			GameObject gameObject = NGUITools.AddChild(uitable_0.gameObject, createBattleWindowItem_0.gameObject);
			gameObject.name = string.Format("{0:0000}", int_1++);
			CreateBattleWindowItem component = gameObject.GetComponent<CreateBattleWindowItem>();
			component.map = item;
			component.initSelected = int_1 == 1;
			component.SetCallbacks(OnSelectedItem, OnStartRoom);
			NGUITools.SetActive(gameObject, true);
		}
		NGUITools.SetActive(uiwidget_0.gameObject, (float)int_1 / (float)uitable_0.int_0 > 2f);
		RepositionContent();
	}

	private void SetTabContent(int int_3)
	{
		if (int_0 != int_3)
		{
			int_0 = int_3;
		}
	}

	private void ClearItems()
	{
		List<Transform> list = uitable_0.List_0;
		foreach (Transform item in list)
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
		uitable_0.Reposition();
		if (bool_1)
		{
			bool_1 = false;
		}
		else
		{
			uiscrollView_0.ResetPosition();
		}
	}

	private void fillModeTypes()
	{
		list_0.Clear();
		list_0.Add(Localizer.Get("ui.map_list_window.teamfight"));
		list_0.Add(Localizer.Get("ui.map_list_window.flagcapture"));
		list_0.Add(Localizer.Get("ui.map_list_window.dethmatch"));
	}

	public void OnPopupBtnClick()
	{
		uipopupList_0.gameObject.SendMessage("OnClick");
	}

	private void OnSelectedItem(CreateBattleWindowItem createBattleWindowItem_2)
	{
		if (createBattleWindowItem_1 != null)
		{
			createBattleWindowItem_1.SetSelected(false);
		}
		createBattleWindowItem_1 = createBattleWindowItem_2;
	}

	private void OnStartRoom(int int_3)
	{
		ModeData modeDataByTypeAndMap = ModeStorage.Get.GetModeDataByTypeAndMap(modeType_0, int_3);
		if (modeDataByTypeAndMap == null)
		{
			return;
		}
		int num = 16;
		if (int_0 == 0)
		{
			num = 4;
		}
		else if (int_0 == 1)
		{
			num = 6;
		}
		else if (int_0 == 2)
		{
			num = 8;
		}
		if (ClanController.ClanController_0.UserClanData_0 != null && ClanController.ClanController_0.UserClanData_0.list_0.Count > 1)
		{
			ClanInviteBattleWindowParams clanInviteBattleWindowParams = new ClanInviteBattleWindowParams();
			clanInviteBattleWindowParams.modeData_0 = modeDataByTypeAndMap;
			clanInviteBattleWindowParams.int_3 = num;
			clanInviteBattleWindowParams.int_4 = int_2;
			clanInviteBattleWindowParams.string_0 = uiinput_0.String_2;
			clanInviteBattleWindowParams.string_1 = uiinput_1.String_2;
			clanInviteBattleWindowParams.bool_0 = false;
			clanInviteBattleWindowParams.int_5 = 1;
			ClanInviteBattleWindow.Show(clanInviteBattleWindowParams);
		}
		else
		{
			Hide();
			if (SelectMapWindow.SelectMapWindow_0 != null)
			{
				SelectMapWindow.SelectMapWindow_0.Hide();
			}
			if (MapListWindow.MapListWindow_0 != null)
			{
				MapListWindow.MapListWindow_0.Hide();
			}
			MonoSingleton<FightController>.Prop_0.CreateRoom(modeDataByTypeAndMap, num, int_2, uiinput_0.String_2, uiinput_1.String_2);
		}
	}

	private void getDurationData(out int int_3, out int int_4, out int int_5)
	{
		int_3 = 5;
		int_4 = 15;
		int_5 = 5;
		ModeTypeData objectByKey = ModeTypeStorage.Get.Storage.GetObjectByKey(modeType_0);
		if (objectByKey != null)
		{
			if (objectByKey.Int32_0 > 0)
			{
				int_3 = objectByKey.Int32_0;
			}
			if (objectByKey.Int32_1 > 0)
			{
				int_4 = objectByKey.Int32_1;
			}
			if (objectByKey.Int32_2 > 0)
			{
				int_5 = objectByKey.Int32_2;
			}
		}
		if (createBattleWindowItem_1 != null && createBattleWindowItem_1.map != null)
		{
			ModeData modeDataByTypeAndMap = ModeStorage.Get.GetModeDataByTypeAndMap(modeType_0, createBattleWindowItem_1.map.Int32_0);
			if (modeDataByTypeAndMap != null && modeDataByTypeAndMap.Int32_2 >= int_3)
			{
				int_4 = modeDataByTypeAndMap.Int32_2;
			}
		}
	}

	public void OnPlusBtnClick()
	{
		int int_;
		int int_2;
		int int_3;
		getDurationData(out int_, out int_2, out int_3);
		this.int_2 += int_3;
		this.int_2 = Mathf.Max(this.int_2, int_);
		this.int_2 = Mathf.Min(this.int_2, int_2);
		uilabel_1.String_0 = this.int_2.ToString();
	}

	public void OnMinusBtnClick()
	{
		int int_;
		int int_2;
		int int_3;
		getDurationData(out int_, out int_2, out int_3);
		this.int_2 -= int_3;
		this.int_2 = Mathf.Max(this.int_2, int_);
		this.int_2 = Mathf.Min(this.int_2, int_2);
		uilabel_1.String_0 = this.int_2.ToString();
	}

	public void OnModePopupValueChange()
	{
		string string_ = uipopupList_0.String_0;
		int num = 0;
		for (int i = 0; i < list_0.Count; i++)
		{
			if (string.Equals(string_, list_0[i]))
			{
				num = i;
				break;
			}
		}
		uilabel_0.String_0 = string_;
		ModeType modeType = ModeType.TEAM_FIGHT;
		switch (num)
		{
		case 0:
			setTeamNums(true);
			break;
		case 1:
			modeType = ModeType.FLAG_CAPTURE;
			setTeamNums(true);
			break;
		case 2:
			modeType = ModeType.DEATH_MATCH;
			setTeamNums(false);
			break;
		}
		if (modeType_0 != modeType)
		{
			modeType_0 = modeType;
			UpdateTable();
		}
	}

	private void setTeamNums(bool bool_2)
	{
		for (int i = 0; i < gameObject_0.Length; i++)
		{
			gameObject_0[i].SetActive(bool_2);
		}
		for (int j = 0; j < gameObject_1.Length; j++)
		{
			gameObject_1[j].SetActive(!bool_2);
		}
	}

	public void OnDeletePassword()
	{
		uiinput_1.String_2 = string.Empty;
	}

	public void OnFightClick()
	{
		if (createBattleWindowItem_1 != null && createBattleWindowItem_1.map != null)
		{
			OnStartRoom(createBattleWindowItem_1.map.Int32_0);
		}
	}
}
