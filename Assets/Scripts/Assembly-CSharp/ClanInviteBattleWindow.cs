using System;
using System.Collections.Generic;
using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.ClanMessageWindow)]
public class ClanInviteBattleWindow : BaseGameWindow
{
	public UISprite uisprite_0;

	public UITable uitable_0;

	public UIButton uibutton_0;

	public ClanInviteMemberSlot clanInviteMemberSlot_0;

	private static ClanInviteBattleWindow clanInviteBattleWindow_0;

	private ModeData modeData_0;

	private List<ClanInviteMemberSlot> list_0 = new List<ClanInviteMemberSlot>();

	private bool bool_1 = true;

	public static ClanInviteBattleWindow ClanInviteBattleWindow_0
	{
		get
		{
			return clanInviteBattleWindow_0;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return ClanInviteBattleWindow_0 != null && ClanInviteBattleWindow_0.Boolean_0;
		}
	}

	public static void Show(ClanInviteBattleWindowParams clanInviteBattleWindowParams_0 = null)
	{
		if (!(clanInviteBattleWindow_0 != null))
		{
			clanInviteBattleWindow_0 = BaseWindow.Load("ClanInviteBattleWindow") as ClanInviteBattleWindow;
			clanInviteBattleWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			clanInviteBattleWindow_0.Parameters_0.bool_5 = true;
			clanInviteBattleWindow_0.Parameters_0.bool_0 = false;
			clanInviteBattleWindow_0.Parameters_0.bool_6 = true;
			clanInviteBattleWindow_0.InternalShow(clanInviteBattleWindowParams_0);
		}
	}

	public override void OnShow()
	{
		ClanBattleController.ClanBattleController_0.Subscribe(OnUpdateInviteResponse, ClanBattleController.EventType.INVITE_RESPONSE);
		ClanController.ClanController_0.Subscribe(OnUpdateClan, ClanController.EventType.UPDATE_CLAN);
		base.OnShow();
		ClanInviteBattleWindowParams clanInviteBattleWindowParams = ClanInviteBattleWindow_0.WindowShowParameters_0 as ClanInviteBattleWindowParams;
		modeData_0 = clanInviteBattleWindowParams.modeData_0;
		InflateItems();
	}

	public override void OnHide()
	{
		base.OnHide();
		if (bool_1)
		{
			ClanBattleController.ClanBattleController_0.SetInviteList(GetListInvited(true));
			ClanBattleController.ClanBattleController_0.SendInviteToFight(string.Empty, string.Empty, ClanStartFightNetworkCommand.int_2);
		}
		ClanBattleController.ClanBattleController_0.Unsubscribe(OnUpdateInviteResponse, ClanBattleController.EventType.INVITE_RESPONSE);
		ClanController.ClanController_0.Unsubscribe(OnUpdateClan, ClanController.EventType.UPDATE_CLAN);
	}

	private void InflateItems()
	{
		ClearGrid();
		NGUITools.SetActive(clanInviteMemberSlot_0.gameObject, false);
		List<UserClanMemberData> list = new List<UserClanMemberData>();
		if (ClanController.ClanController_0.UserClanData_0 != null)
		{
			list = ClanController.ClanController_0.UserClanData_0.list_0;
		}
		int int_ = UserController.UserController_0.UserData_0.user_0.int_0;
		ClanInviteBattleWindowParams clanInviteBattleWindowParams = base.WindowShowParameters_0 as ClanInviteBattleWindowParams;
		bool flag = clanInviteBattleWindowParams != null && clanInviteBattleWindowParams.bool_0;
		for (int i = 0; i < list.Count; i++)
		{
			if (!list[i].int_0.Equals(int_))
			{
				GameObject gameObject = NGUITools.AddChild(uitable_0.gameObject, clanInviteMemberSlot_0.gameObject);
				gameObject.name = string.Format("{0:000}", i);
				ClanInviteMemberSlot component = gameObject.GetComponent<ClanInviteMemberSlot>();
				component.SetData(list[i], modeData_0.Int32_0, flag);
				NGUITools.SetActive(gameObject, true);
				list_0.Add(component);
			}
		}
		uisprite_0.gameObject.SetActive(list_0.Count > 14);
		uitable_0.Reposition();
	}

	private void ClearGrid()
	{
		list_0.Clear();
		List<Transform> list = uitable_0.List_0;
		foreach (Transform item in list)
		{
			item.parent = null;
			UnityEngine.Object.Destroy(item.gameObject);
		}
	}

	public List<int> GetListInvited(bool bool_2 = false)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < list_0.Count; i++)
		{
			if (list_0[i].status == 2 || list_0[i].status == 1 || bool_2)
			{
				list.Add(list_0[i].UserClanMemberData_0.int_0);
			}
		}
		return list;
	}

	public void StarFight()
	{
		bool_1 = false;
		Hide();
		if (CreateBattleWindow.CreateBattleWindow_0 != null)
		{
			CreateBattleWindow.CreateBattleWindow_0.Hide();
		}
		if (SelectMapWindow.SelectMapWindow_0 != null)
		{
			SelectMapWindow.SelectMapWindow_0.Hide();
		}
		if (MapListWindow.MapListWindow_0 != null)
		{
			MapListWindow.MapListWindow_0.Hide();
		}
		ClanInviteBattleWindowParams clanInviteBattleWindowParams = ClanInviteBattleWindow_0.WindowShowParameters_0 as ClanInviteBattleWindowParams;
		int int_3 = GetListInvited().Count;
		ClanBattleController.ClanBattleController_0.SetInviteList(GetListInvited());
		int int_4 = clanInviteBattleWindowParams.int_5;
		if (int_3 >= MatchMakingSettings.Get.PlayerLimitInClanBattle)
		{
			int_4 = 1;
			int num = 1;
		}
		else
		{
			switch (int_4)
			{
			default:
				return;
			case 1:
				break;
			case 2:
				MonoSingleton<FightController>.Prop_0.JoinRoom(clanInviteBattleWindowParams.string_0);
				return;
			case 3:
			{
				Func<int, int, bool> func = (int int_1, int int_2) => int_1 + int_3 < int_2;
				ModeData randomModeForModeType = clanInviteBattleWindowParams.modeData_0;
				if (MonoSingleton<FightController>.Prop_0.FightMatchMakingController_0.State_0 == FightMatchMakingController.State.JoinRandomRoomModeType)
				{
					randomModeForModeType = MonoSingleton<FightController>.Prop_0.FightMatchMakingController_0.GetRandomModeForModeType(clanInviteBattleWindowParams.modeData_0.ModeType_0, func);
				}
				MonoSingleton<FightController>.Prop_0.JoinRandomRoom(randomModeForModeType, func);
				return;
			}
			}
		}
		MonoSingleton<FightController>.Prop_0.CreateRoom(clanInviteBattleWindowParams.modeData_0, clanInviteBattleWindowParams.int_3, clanInviteBattleWindowParams.int_4, clanInviteBattleWindowParams.string_0, clanInviteBattleWindowParams.string_1);
	}

	private void OnUpdateInviteResponse(ClanBattleController.EventData eventData_0)
	{
		for (int i = 0; i < list_0.Count; i++)
		{
			if (eventData_0 != null && eventData_0.Int32_1 == list_0[i].UserClanMemberData_0.int_0)
			{
				list_0[i].HideStatuses();
				list_0[i].SetStatus(eventData_0.Int32_0);
			}
		}
	}

	private void OnUpdateClan(ClanController.EventData eventData_0)
	{
		List<UserClanMemberData> list = new List<UserClanMemberData>();
		if (ClanController.ClanController_0.UserClanData_0 != null)
		{
			list = ClanController.ClanController_0.UserClanData_0.list_0;
		}
		for (int i = 0; i < list_0.Count; i++)
		{
			for (int j = 0; j < list.Count; j++)
			{
				if (list[j].int_0 == list_0[i].UserClanMemberData_0.int_0 && list[j].bool_0 != list_0[i].UserClanMemberData_0.bool_0)
				{
					list_0[i].HideStatuses();
					NGUITools.SetActive(list_0[i].statuses[1].gameObject, list[j].bool_0);
					NGUITools.SetActive(list_0[i].statuses[0].gameObject, !list[j].bool_0);
					list_0[i].UserClanMemberData_0.bool_0 = list[j].bool_0;
				}
			}
		}
	}

	public void CloseWnd()
	{
		Hide();
	}
}
