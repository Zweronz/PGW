using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.events;
using engine.helpers;
using engine.unity;

[GameWindowParams(GameWindowType.ClanTopWindow)]
public class ClanTopWindow : BaseGameWindow
{
	private static ClanTopWindow clanTopWindow_0;

	public UIScrollView uiscrollView_0;

	public UIGrid uigrid_0;

	public ClanTopItem clanTopItem_0;

	public UIWidget uiwidget_0;

	public UILabel uilabel_0;

	public UILabel uilabel_1;

	public UILabel uilabel_2;

	public UIInput uiinput_0;

	public UIButton uibutton_0;

	public UIButton uibutton_1;

	public UIWidget uiwidget_1;

	public ClanTopItem clanTopItem_1;

	public UIButton uibutton_2;

	public UIButton uibutton_3;

	public UILabel uilabel_3;

	public UISprite uisprite_0;

	public UIButton uibutton_4;

	public UIButton uibutton_5;

	private List<UserClanData> list_0;

	private string string_0 = string.Empty;

	private bool bool_1;

	private string string_1;

	private string string_2;

	private string string_3;

	private string string_4;

	private float float_0;

	private float float_1 = 60f;

	[CompilerGenerated]
	private static Comparison<UserClanData> comparison_0;

	public static ClanTopWindow ClanTopWindow_0
	{
		get
		{
			return clanTopWindow_0;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return ClanTopWindow_0 != null && ClanTopWindow_0.Boolean_0;
		}
	}

	public static void Show(ClanTopWindowParams clanTopWindowParams_0 = null)
	{
		if (!(clanTopWindow_0 != null))
		{
			clanTopWindow_0 = BaseWindow.Load("ClanTopWindow") as ClanTopWindow;
			clanTopWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			clanTopWindow_0.Parameters_0.bool_5 = false;
			clanTopWindow_0.Parameters_0.bool_0 = false;
			clanTopWindow_0.Parameters_0.bool_6 = true;
			clanTopWindow_0.InternalShow(clanTopWindowParams_0);
		}
	}

	public override void OnShow()
	{
		base.OnShow();
		Init();
		UpdateOneSec();
	}

	public override void OnHide()
	{
		base.OnHide();
		clanTopWindow_0 = null;
		ClanTopWindowParams clanTopWindowParams = base.WindowShowParameters_0 as ClanTopWindowParams;
		if (clanTopWindowParams != null && !clanTopWindowParams.Boolean_0 && ClanController.ClanController_0.UserClanData_0 != null)
		{
			ClanWindow.Show();
		}
	}

	private void OnEnable()
	{
		ClanController.ClanController_0.Subscribe(OnSearchSuccess, ClanController.EventType.SEARCH_SUCCESS);
		ClanController.ClanController_0.Subscribe(OnClanTopUpdateSuccess, ClanController.EventType.GET_TOP_SUCCESS);
		ClanController.ClanController_0.Subscribe(OnUpdateClan, ClanController.EventType.UPDATE_CLAN);
		ClanController.ClanController_0.Subscribe(InitMessageCount, ClanController.EventType.UPDATE_CLAN_MESSAGES);
		ClanController.ClanController_0.Subscribe(OnCreateClanSuccess, ClanController.EventType.CREATE_SUCCESS);
		if (!DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateOneSec))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(UpdateOneSec);
		}
	}

	private void OnDisable()
	{
		ClanController.ClanController_0.Unsubscribe(OnSearchSuccess, ClanController.EventType.SEARCH_SUCCESS);
		ClanController.ClanController_0.Unsubscribe(OnClanTopUpdateSuccess, ClanController.EventType.GET_TOP_SUCCESS);
		ClanController.ClanController_0.Unsubscribe(OnUpdateClan, ClanController.EventType.UPDATE_CLAN);
		ClanController.ClanController_0.Unsubscribe(InitMessageCount, ClanController.EventType.UPDATE_CLAN_MESSAGES);
		ClanController.ClanController_0.Unsubscribe(OnCreateClanSuccess, ClanController.EventType.CREATE_SUCCESS);
		if (DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateOneSec))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(UpdateOneSec);
		}
	}

	private void Init()
	{
		string_1 = Localizer.Get("ui.day.mini");
		string_2 = Localizer.Get("ui.hour.mini");
		string_3 = Localizer.Get("ui.min.mini");
		string_4 = Localizer.Get("ui.sec.mini");
		NGUITools.SetActive(clanTopItem_0.gameObject, false);
		InitMyClan();
		InitState();
		InitTop();
		uigrid_0.onReposition_0 = delegate
		{
			uiscrollView_0.UpdateScrollbars(true);
		};
		InitMessageCount();
	}

	private void InitMessageCount(ClanController.EventData eventData_0 = null)
	{
		int newClanMessagesCount = ClanController.ClanController_0.GetNewClanMessagesCount();
		uilabel_3.String_0 = newClanMessagesCount.ToString();
		NGUITools.SetActive(uisprite_0.gameObject, newClanMessagesCount > 0);
		NGUITools.SetActive(uibutton_4.gameObject, newClanMessagesCount > 0);
		NGUITools.SetActive(uibutton_5.gameObject, !uibutton_4.gameObject.activeSelf);
	}

	private void InitMyClan()
	{
		UserClanData userClanData_ = ClanController.ClanController_0.UserClanData_0;
		bool flag = userClanData_ == null || (userClanData_ != null && ClanController.ClanController_0.IsCalnInTop(userClanData_.string_0));
		NGUITools.SetActive(clanTopItem_1.gameObject, !flag);
		uiwidget_1.Int32_1 = ((!flag) ? 506 : 461);
		if (userClanData_ != null)
		{
			clanTopItem_1.SetData(userClanData_, 0, false);
		}
	}

	private void InitState()
	{
		UserClanData userClanData_ = ClanController.ClanController_0.UserClanData_0;
		NGUITools.SetActive(uibutton_2.gameObject, userClanData_ != null);
		NGUITools.SetActive(uibutton_3.gameObject, userClanData_ == null);
	}

	private void InitTop(bool bool_2 = true)
	{
		list_0 = new List<UserClanData>(ClanController.ClanController_0.List_1);
		InflateItems(bool_2);
		InitLeader();
	}

	private void InitLeader()
	{
		NGUITools.SetActive(uiwidget_0.gameObject, !string.IsNullOrEmpty(ClanController.ClanController_0.String_0));
		uilabel_0.String_0 = ((!string.IsNullOrEmpty(ClanController.ClanController_0.String_0)) ? ClanController.ClanController_0.String_0 : string.Empty);
	}

	private void UpdateOneSec()
	{
		int int_ = Math.Max(0, (int)(ClanController.ClanController_0.Double_0 - Utility.Double_0));
		string localizedTime = Utility.GetLocalizedTime(int_, string_1, string_2, string_3, string_4);
		uilabel_1.String_0 = localizedTime;
		uilabel_2.String_0 = localizedTime;
		float_0 -= 1f;
		if (float_0 <= 0f)
		{
			ClanController.ClanController_0.UpdateClanTop();
			float_0 = float_1;
		}
	}

	private void InflateItems(bool bool_2 = true)
	{
		ClearGrid();
		for (int i = 0; i < list_0.Count; i++)
		{
			GameObject gameObject = NGUITools.AddChild(uigrid_0.gameObject, clanTopItem_0.gameObject);
			gameObject.name = string.Format("{0:000}", i);
			ClanTopItem component = gameObject.GetComponent<ClanTopItem>();
			component.SetData(list_0[i], i + 1, bool_1);
			NGUITools.SetActive(gameObject, true);
		}
		if (bool_2)
		{
			uiscrollView_0.ResetPosition();
		}
		uigrid_0.Reposition();
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

	private void SortByScores()
	{
		if (list_0 != null)
		{
			list_0.Sort((UserClanData userClanData_0, UserClanData userClanData_1) => userClanData_1.int_3.CompareTo(userClanData_0.int_3));
		}
	}

	private void OnClanTopUpdateSuccess(ClanController.EventData eventData_0)
	{
		if (string.IsNullOrEmpty(string_0))
		{
			InitState();
			InitTop(false);
		}
	}

	private void OnSearchSuccess(ClanController.EventData eventData_0)
	{
		if (eventData_0.Dictionary_0 != null)
		{
			list_0 = new List<UserClanData>(eventData_0.Dictionary_0.Values);
			SortByScores();
			InflateItems();
		}
		else
		{
			list_0.Clear();
			InflateItems();
		}
	}

	public void OnUpdateClan(ClanController.EventData eventData_0)
	{
		UserClanData userClanData_ = ClanController.ClanController_0.UserClanData_0;
		if (userClanData_ != null && string.IsNullOrEmpty(string_0))
		{
			InitState();
			InitTop(false);
		}
	}

	public void OnCreateClanSuccess(ClanController.EventData eventData_0)
	{
		ClanWindow.Show();
	}

	public void OnSearchButtonClick()
	{
		if (string.IsNullOrEmpty(uiinput_0.String_2) && !string.IsNullOrEmpty(string_0))
		{
			InitTop();
		}
		else if ((string.IsNullOrEmpty(uiinput_0.String_2) && string.IsNullOrEmpty(string_0)) || uiinput_0.String_2.Equals(string_0))
		{
			return;
		}
		ClanController.ClanController_0.SearchClan(uiinput_0.String_2.Trim());
		string_0 = uiinput_0.String_2;
		bool_1 = true;
		NGUITools.SetActive(uibutton_0.gameObject, false);
		NGUITools.SetActive(uibutton_1.gameObject, true);
	}

	public void OnClearButtonClick()
	{
		uiinput_0.String_2 = string.Empty;
		uiinput_0.Boolean_2 = false;
		string_0 = string.Empty;
		bool_1 = false;
		NGUITools.SetActive(uibutton_0.gameObject, true);
		NGUITools.SetActive(uibutton_1.gameObject, false);
		InitTop();
	}

	public void OnCreateClanButtonClick()
	{
		ClanCreateWindow.Show();
	}

	public void OnMyClanButtonClick()
	{
		ClanWindow.Show();
	}

	public void OnMessageButtonClick()
	{
		ClanMessageWindow.Show();
	}
}
