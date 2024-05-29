using System;
using System.Collections.Generic;
using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.ClanWindow)]
public class ClanWindow : BaseGameWindow
{
	private static ClanWindow clanWindow_0;

	public UISprite uisprite_0;

	public UILabel uilabel_0;

	public UILabel uilabel_1;

	public UITexture uitexture_0;

	public UITexture uitexture_1;

	public UILabel uilabel_2;

	public UISprite uisprite_1;

	public UILabel uilabel_3;

	public UILabel uilabel_4;

	public UIProgressBar uiprogressBar_0;

	public UILabel uilabel_5;

	public UILabel uilabel_6;

	public UIScrollView uiscrollView_0;

	public UIGrid uigrid_0;

	public UISprite uisprite_2;

	public ClanMemberItem clanMemberItem_0;

	public ClanMembersSortHandler[] clanMembersSortHandler_0;

	public UIWidget uiwidget_0;

	public UIWidget uiwidget_1;

	public UILabel uilabel_7;

	public UIButton uibutton_0;

	public UIButton uibutton_1;

	public UIWidget uiwidget_2;

	public UILabel uilabel_8;

	public UILabel uilabel_9;

	public UIPanel uipanel_0;

	public UILabel uilabel_10;

	public UISprite uisprite_3;

	public UIButton uibutton_2;

	public UIButton uibutton_3;

	private List<UserClanMemberData> list_0 = new List<UserClanMemberData>();

	private UserClanData userClanData_0;

	private int int_0;

	private Texture texture_0;

	private UserClanMemberData userClanMemberData_0;

	public static ClanWindow ClanWindow_0
	{
		get
		{
			return clanWindow_0;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return ClanWindow_0 != null && ClanWindow_0.Boolean_0;
		}
	}

	public static void Show(ClanWindowParams clanWindowParams_0 = null)
	{
		if (!(clanWindow_0 != null))
		{
			clanWindow_0 = BaseWindow.Load("ClanWindow") as ClanWindow;
			clanWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			clanWindow_0.Parameters_0.bool_5 = false;
			clanWindow_0.Parameters_0.bool_0 = false;
			clanWindow_0.Parameters_0.bool_6 = true;
			clanWindow_0.InternalShow(clanWindowParams_0);
		}
	}

	public override void OnShow()
	{
		base.OnShow();
		Init();
	}

	public override void OnHide()
	{
		base.OnHide();
		clanWindow_0 = null;
		ClanController.ClanController_0.Unsubscribe(OnUpdateClan, ClanController.EventType.UPDATE_CLAN);
		ClanController.ClanController_0.Unsubscribe(InitMessageCount, ClanController.EventType.UPDATE_CLAN_MESSAGES);
	}

	private void Init()
	{
		ClanController.ClanController_0.Subscribe(OnUpdateClan, ClanController.EventType.UPDATE_CLAN);
		ClanController.ClanController_0.Subscribe(InitMessageCount, ClanController.EventType.UPDATE_CLAN_MESSAGES);
		NGUITools.SetActive(uipanel_0.gameObject, false);
		NGUITools.SetActive(clanMemberItem_0.gameObject, false);
		texture_0 = Resources.Load<Texture>("UI/images/Clan/clan_logo");
		InitData();
		InitClan();
		InitState();
		InitMembers();
		InflateItems();
		InitMessageCount();
	}

	private void InitMessageCount(ClanController.EventData eventData_0 = null)
	{
		int newClanMessagesCount = ClanController.ClanController_0.GetNewClanMessagesCount();
		uilabel_10.String_0 = newClanMessagesCount.ToString();
		NGUITools.SetActive(uisprite_3.gameObject, newClanMessagesCount > 0);
		NGUITools.SetActive(uibutton_2.gameObject, newClanMessagesCount > 0);
		NGUITools.SetActive(uibutton_3.gameObject, !uibutton_2.gameObject.activeSelf);
	}

	private void InitData()
	{
		ClanWindowParams clanWindowParams = base.WindowShowParameters_0 as ClanWindowParams;
		if (clanWindowParams != null && clanWindowParams.UserClanData_0 != null)
		{
			userClanData_0 = clanWindowParams.UserClanData_0;
		}
		else
		{
			userClanData_0 = ((clanWindowParams == null || string.IsNullOrEmpty(clanWindowParams.String_0)) ? ClanController.ClanController_0.UserClanData_0 : ClanController.ClanController_0.GetClan(clanWindowParams.String_0));
		}
		int_0 = ((userClanData_0 != null) ? userClanData_0.int_4 : 0);
	}

	private void InitClan()
	{
		UILabel uILabel = uilabel_0;
		string string_ = userClanData_0.string_2;
		uilabel_1.String_0 = string_;
		uILabel.String_0 = string_;
		uilabel_3.String_0 = userClanData_0.int_2.ToString();
		uilabel_4.String_0 = userClanData_0.int_1.ToString();
		ClanLevelData clanLevelDataByLevel = ClanController.ClanController_0.GetClanLevelDataByLevel(userClanData_0.int_2);
		ClanLevelData clanLevelDataByLevel2 = ClanController.ClanController_0.GetClanLevelDataByLevel(userClanData_0.int_2 + 1);
		if (clanLevelDataByLevel2 != null)
		{
			uiprogressBar_0.Single_0 = userClanData_0.int_1 / clanLevelDataByLevel2.int_1;
		}
		uilabel_5.String_0 = userClanData_0.string_1;
		bool flag = ClanController.ClanController_0.IsCalnInTop(userClanData_0.string_0);
		NGUITools.SetActive(uisprite_1.gameObject, flag);
		uisprite_1.String_0 = string.Format("clan_place_{0}", (int_0 >= 4) ? "universal" : int_0.ToString());
		uilabel_2.String_0 = int_0.ToString();
		NGUITools.SetActive(uilabel_7.gameObject, !flag && userClanData_0.int_4 > 0);
		uilabel_7.String_0 = userClanData_0.int_4.ToString();
		if (clanLevelDataByLevel != null)
		{
			uilabel_6.String_0 = string.Format("{0}/{1}", userClanData_0.list_0.Count, clanLevelDataByLevel.int_2);
		}
	}

	private void InitState()
	{
		bool flag = ClanController.ClanController_0.UserClanData_0 != null && userClanData_0.string_0 == ClanController.ClanController_0.UserClanData_0.string_0;
		bool flag2 = userClanData_0.bool_0;
		NGUITools.SetActive(uibutton_1.gameObject, ClanController.ClanController_0.UserClanData_0 == null && !flag2 && userClanData_0.list_0.Count < ClanController.ClanController_0.GetClanMembersMaxByLevel(userClanData_0.int_2));
		NGUITools.SetActive(uibutton_0.gameObject, !uibutton_1.gameObject.activeSelf);
		NGUITools.SetActive(uiwidget_2.gameObject, flag);
		uisprite_0.String_0 = ((!flag) ? "clan_header_other_bg" : "clan_header_bg");
		if (flag)
		{
			bool flag3 = ClanController.ClanController_0.UserClanData_0 != null && ClanController.ClanController_0.UserClanData_0.int_0 == UserController.UserController_0.UserData_0.user_0.int_0;
			uilabel_8.String_0 = Localizer.Get((!flag3) ? "window.clan_info.exit_clan" : "window.clan_info.close_clan");
		}
	}

	private void InitMembers()
	{
		list_0 = userClanData_0.list_0;
		NGUITools.SetActive(uiwidget_0.gameObject, list_0.Count > 0);
		int num = Mathf.Min(10, list_0.Count);
		uiwidget_1.Int32_1 = ((num <= 0) ? 130 : (150 + num * 28));
		Vector3 localPosition = uigrid_0.transform.localPosition;
		localPosition.y = (float)Math.Max(0, list_0.Count - 1) * uigrid_0.float_1 * 0.5f;
		uigrid_0.transform.localPosition = localPosition;
		uisprite_2.gameObject.SetActive(list_0.Count > 10);
		SortByScores(false);
	}

	private void InflateItems()
	{
		ClearGrid();
		if (ClanController.ClanController_0.UserClanData_0 != null)
		{
			bool flag = ClanController.ClanController_0.UserClanData_0.int_0 == UserController.UserController_0.UserData_0.user_0.int_0;
			bool flag2 = ClanController.ClanController_0.UserClanData_0.string_0 == userClanData_0.string_0;
		}
		for (int i = 0; i < list_0.Count; i++)
		{
			GameObject gameObject = NGUITools.AddChild(uigrid_0.gameObject, clanMemberItem_0.gameObject);
			gameObject.name = string.Format("{0:000}", i);
			ClanMemberItem component = gameObject.GetComponent<ClanMemberItem>();
			component.SetData(list_0[i], i + 1);
			NGUITools.SetActive(gameObject, true);
		}
		uigrid_0.Reposition();
		uiscrollView_0.ResetPosition();
	}

	private void ClearGrid()
	{
		BetterList<Transform> childList = uigrid_0.GetChildList();
		foreach (Transform item in childList)
		{
			item.parent = null;
			UnityEngine.Object.Destroy(item.gameObject);
		}
	}

	public void SortMembers(ClanMembersSortHandler.SortType sortType_0, bool bool_1)
	{
		ClanMembersSortHandler[] array = clanMembersSortHandler_0;
		foreach (ClanMembersSortHandler clanMembersSortHandler in array)
		{
			clanMembersSortHandler.ResetFlip();
		}
		switch (sortType_0)
		{
		case ClanMembersSortHandler.SortType.Nick:
			SortByNick(bool_1);
			break;
		case ClanMembersSortHandler.SortType.Level:
			SortByLevel(bool_1);
			break;
		case ClanMembersSortHandler.SortType.Score:
			SortByScores(bool_1);
			break;
		case ClanMembersSortHandler.SortType.Status:
			SortByStatus(bool_1);
			break;
		}
		InflateItems();
	}

	private void SortByScores(bool bool_1)
	{
		list_0.Sort((UserClanMemberData userClanMemberData_0, UserClanMemberData userClanMemberData_1) => (!bool_1) ? userClanMemberData_1.int_2.CompareTo(userClanMemberData_0.int_2) : userClanMemberData_0.int_2.CompareTo(userClanMemberData_1.int_2));
	}

	private void SortByNick(bool bool_1)
	{
		list_0.Sort((UserClanMemberData userClanMemberData_0, UserClanMemberData userClanMemberData_1) => (!bool_1) ? userClanMemberData_0.string_0.CompareTo(userClanMemberData_1.string_0) : userClanMemberData_1.string_0.CompareTo(userClanMemberData_0.string_0));
	}

	private void SortByLevel(bool bool_1)
	{
		list_0.Sort((UserClanMemberData userClanMemberData_0, UserClanMemberData userClanMemberData_1) => (!bool_1) ? userClanMemberData_1.int_1.CompareTo(userClanMemberData_0.int_1) : userClanMemberData_0.int_1.CompareTo(userClanMemberData_1.int_1));
	}

	private void SortByStatus(bool bool_1)
	{
		list_0.Sort(delegate(UserClanMemberData userClanMemberData_0, UserClanMemberData userClanMemberData_1)
		{
			if (userClanMemberData_0.bool_0 && userClanMemberData_1.bool_0)
			{
				return 0;
			}
			if (userClanMemberData_0.bool_0 && !userClanMemberData_1.bool_0)
			{
				return bool_1 ? 1 : (-1);
			}
			return (userClanMemberData_1.bool_0 && !userClanMemberData_0.bool_0) ? ((!bool_1) ? 1 : (-1)) : userClanMemberData_0.int_3.CompareTo(userClanMemberData_1.int_3);
		});
	}

	public void OnMessageButtonClick()
	{
		ClanMessageWindow.Show();
		OnMissClick();
	}

	public void OnCloseClanButtonClick()
	{
		OnMissClick();
		bool bool_0 = ClanController.ClanController_0.UserClanData_0 != null && ClanController.ClanController_0.UserClanData_0.int_0 == UserController.UserController_0.UserData_0.user_0.int_0;
		MessageWindowConfirm.Show(new MessageWindowConfirmParams(Localizer.Get((!bool_0) ? "window.clan_info.confirm.exit_clan" : "window.clan_info.confirm.close_clan"), delegate
		{
			if (bool_0)
			{
				ClanController.ClanController_0.CloseClan();
			}
			else
			{
				ClanController.ClanController_0.ExitClan();
			}
			Hide();
		}, "OK", KeyCode.None, null, string.Empty));
	}

	public void OnRequestButtonClick()
	{
		if (userClanData_0.list_0.Count >= ClanController.ClanController_0.GetClanMembersMaxByLevel(userClanData_0.int_2))
		{
			MessageWindow.Show(new MessageWindowParams(LocalizationStorage.Get.Term("ui.clan_wnd.to_many_mambers")));
		}
		else if (ClanController.ClanController_0.UserClanData_0 == null)
		{
			ClanController.ClanController_0.SendClanMessage(0, string.Empty, 0, userClanData_0.string_0);
			userClanData_0.bool_0 = true;
			NGUITools.SetActive(uibutton_1.gameObject, false);
			NGUITools.SetActive(uibutton_0.gameObject, true);
			ClanController.ClanController_0.UpdateClanTop();
			ShowRequestSendedLabel();
		}
		else
		{
			MessageWindow.Show(new MessageWindowParams(LocalizationStorage.Get.Term("ui.clan_wnd.you_alredy_in_clan")));
		}
	}

	public void OnClanTopButtonClick()
	{
		Hide();
		if (ClanTopWindow.ClanTopWindow_0 == null)
		{
			ClanTopWindowParams clanTopWindowParams = new ClanTopWindowParams();
			clanTopWindowParams.Boolean_0 = false;
			ClanTopWindow.Show(clanTopWindowParams);
		}
	}

	public void OnMemberClick(GameObject gameObject_0, UserClanMemberData userClanMemberData_1)
	{
		OnMissClick();
		UserClanData userClanData = ClanController.ClanController_0.UserClanData_0;
		bool flag = userClanData != null && userClanData.int_0 == UserController.UserController_0.UserData_0.user_0.int_0;
		bool flag2 = userClanData != null && userClanData.string_0 == userClanData_0.string_0;
		if (flag && UserController.UserController_0.UserData_0.user_0.int_0 != userClanMemberData_1.int_0 && flag2)
		{
			userClanMemberData_0 = userClanMemberData_1;
			NGUITools.SetActive(uipanel_0.gameObject, true);
			Vector3 localPosition = Input.mousePosition * ScreenController.ScreenController_0.Single_0;
			float num = (float)Screen.width * ScreenController.ScreenController_0.Single_0;
			float num2 = (float)Screen.height * ScreenController.ScreenController_0.Single_0;
			localPosition.x -= num * 0.5f;
			localPosition.y -= num2 * 0.5f;
			uipanel_0.transform.localPosition = localPosition;
		}
	}

	public void OnNickClick(GameObject gameObject_0, UserClanMemberData userClanMemberData_1)
	{
		OnMissClick();
		ProfileWindowController.ProfileWindowController_0.ShowProfileWindowForPlayer(userClanMemberData_1.int_0);
	}

	public void OnRemoveMember()
	{
		NGUITools.SetActive(uipanel_0.gameObject, false);
		MessageWindowConfirm.Show(new MessageWindowConfirmParams(string.Format(LocalizationStorage.Get.Term("ui.clan_wnd.send_kick"), userClanMemberData_0.string_0), OkButtonCallback, "OK", KeyCode.None, null, string.Empty));
	}

	private void OkButtonCallback()
	{
		ClanController.ClanController_0.SendClanMessage(6, string.Empty, userClanMemberData_0.int_0, string.Empty);
		userClanMemberData_0 = null;
	}

	public void OnMissClick()
	{
		NGUITools.SetActive(uipanel_0.gameObject, false);
		userClanMemberData_0 = null;
	}

	public void OnUpdateClan(ClanController.EventData eventData_0)
	{
		UserClanData userClanData = ClanController.ClanController_0.UserClanData_0;
		if (userClanData != null && userClanData_0 != null && userClanData_0.string_0.Equals(userClanData.string_0))
		{
			userClanData_0 = userClanData;
			InitClan();
			InitState();
			InitMembers();
			InflateItems();
		}
	}

	public void ShowRequestSendedLabel()
	{
		NGUITools.SetActive(uilabel_9.gameObject, true);
	}
}
