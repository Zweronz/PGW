using System;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using engine.network;
using engine.unity;

public class LobbyUserInfoController : MonoBehaviour
{
	public UIWidget common;

	public UILabel nickLabel;

	public UILabel levelValue;

	public UILabel expLabel;

	public UILabel expValue;

	public UISprite expProgress;

	public UIButton pen;

	public UIWidget maximum;

	public UILabel maxlevelValue;

	public UILabel maxNickLabel;

	public UIButton maxpen;

	public UILabel clanNameLabel;

	public UILabel maxLevelClanNameLabel;

	private UILabel uilabel_0;

	private int int_0;

	private bool bool_0;

	private void OnEnable()
	{
		NGUITools.SetActive(pen.gameObject, false);
		NGUITools.SetActive(maxpen.gameObject, false);
		UpdateInfo();
		UsersData.Subscribe(UsersData.EventType.USER_CHANGED, UpdateInfo);
		BaseGameWindow.Subscribe(GameWindowType.Profile, OnOpenProfile);
		BaseGameWindow.Subscribe(GameWindowType.Shop, OnOpenShop);
		ClanController.ClanController_0.Subscribe(OnUpdateClan, ClanController.EventType.UPDATE_CLAN);
	}

	private void OnOpenProfile(GameWindowEventArg gameWindowEventArg_0)
	{
		NGUITools.SetActive(pen.gameObject, gameWindowEventArg_0.Boolean_0);
		NGUITools.SetActive(maxpen.gameObject, gameWindowEventArg_0.Boolean_0);
	}

	private void OnOpenShop(GameWindowEventArg gameWindowEventArg_0)
	{
		expProgress.transform.parent.gameObject.SetActive(!gameWindowEventArg_0.Boolean_0);
		nickLabel.enabled = !gameWindowEventArg_0.Boolean_0;
		expValue.enabled = !gameWindowEventArg_0.Boolean_0;
		expLabel.enabled = !gameWindowEventArg_0.Boolean_0;
		NGUITools.SetActive(uilabel_0.gameObject, !gameWindowEventArg_0.Boolean_0);
		Vector3 localPosition = levelValue.transform.parent.transform.localPosition;
		localPosition.x = ((!gameWindowEventArg_0.Boolean_0) ? 0f : 125f);
		levelValue.transform.parent.transform.localPosition = localPosition;
		UpdateInfo();
	}

	private void OnDisable()
	{
		UsersData.Unsubscribe(UsersData.EventType.USER_CHANGED, UpdateInfo);
		BaseGameWindow.Unsubscribe(GameWindowType.Profile, OnOpenProfile);
		BaseGameWindow.Unsubscribe(GameWindowType.Shop, OnOpenShop);
		ClanController.ClanController_0.Unsubscribe(OnUpdateClan, ClanController.EventType.UPDATE_CLAN);
		ClanController.ClanController_0.Unsubscribe(OnGetClan, ClanController.EventType.GET_CLAN_BY_USER_SUCCESS);
		bool_0 = false;
	}

	public void ReinitToAnotherUser(int int_1)
	{
		int_0 = int_1;
		NGUITools.SetActive(pen.gameObject, false);
		NGUITools.SetActive(maxpen.gameObject, false);
		UpdateInfo();
	}

	private void OnUpdateClan(ClanController.EventData eventData_0)
	{
		UpdateInfo();
	}

	private void UpdateInfo(UsersData.EventData eventData_0 = null)
	{
		UserController userController_ = UserController.UserController_0;
		if (int_0 == 0)
		{
			nickLabel.String_0 = Defs.GetPlayerNameOrDefault();
			maxNickLabel.String_0 = Defs.GetPlayerNameOrDefault();
		}
		else
		{
			nickLabel.String_0 = userController_.GetUser(int_0).user_0.string_0;
			maxNickLabel.String_0 = userController_.GetUser(int_0).user_0.string_0;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		if (int_0 == 0)
		{
			num = userController_.GetUserExp();
			num4 = userController_.GetUserLevel();
			num2 = userController_.GetUserMinExpForCurrentLevel();
			num3 = userController_.GetUserMaxExpForCurrentLevel();
		}
		else
		{
			UserData user = userController_.GetUser(int_0);
			num = user.user_0.int_1;
			num4 = user.user_0.int_2;
			num2 = LevelStorage.Get.GetMinExpForLevel(num4);
			num3 = LevelStorage.Get.GetMaxExpForLevel(num4);
		}
		float single_ = (float)(num - num2) / (float)(num3 - num2);
		int maxLevel = 20;//CommonParamsSettings.Get.MaxLevel;
		expValue.String_0 = string.Format("{0}/{1}", num, num3);
		expProgress.Single_0 = single_;
		bool flag = num4 >= maxLevel;
		NGUITools.SetActive(common.gameObject, !flag);
		NGUITools.SetActive(maximum.gameObject, flag);
		SetClanName(flag);
		num4 = Math.Min(maxLevel, num4);
		levelValue.String_0 = num4.ToString();
		maxlevelValue.String_0 = num4.ToString();
	}

	public void SetVisible(bool bool_1)
	{
		NGUITools.SetActive(base.gameObject, bool_1);
	}

	public void OnPenClick()
	{
		if (ProfileWindow.ProfileWindow_0 != null)
		{
			ProfileWindowParams profileWindowParams = ProfileWindow.ProfileWindow_0.WindowShowParameters_0 as ProfileWindowParams;
			if (!profileWindowParams.int_0.Equals(UserController.UserController_0.UserData_0.user_0.int_0))
			{
				return;
			}
		}
		UserNickController.UserNickController_0.ShowRenameWindow(true);
	}

	private void SetClanName(bool bool_1)
	{
		uilabel_0 = ((!bool_1) ? clanNameLabel : maxLevelClanNameLabel);
		string empty = string.Empty;
		if (int_0 == 0)
		{
			empty = ((ClanController.ClanController_0.UserClanData_0 != null) ? ClanController.ClanController_0.UserClanData_0.string_2 : string.Empty);
		}
		else
		{
			empty = ClanController.ClanController_0.GetClanTitleByUser(int_0);
			if (string.IsNullOrEmpty(empty) && !bool_0)
			{
				GetUserClanDataNetworkCommand getUserClanDataNetworkCommand = new GetUserClanDataNetworkCommand();
				getUserClanDataNetworkCommand.int_1 = int_0;
				AbstractNetworkCommand.Send(getUserClanDataNetworkCommand);
				ClanController.ClanController_0.Subscribe(OnGetClan, ClanController.EventType.GET_CLAN_BY_USER_SUCCESS);
				bool_0 = true;
			}
		}
		uilabel_0.String_0 = empty;
	}

	public void GoToClanWnd()
	{
		if (!ClanController.ClanController_0.Boolean_0)
		{
			MessageWindow.Show(new MessageWindowParams(Localizer.Get("clan.message.clan_not_available")));
		}
		else if (!uilabel_0.String_0.IsNullOrEmpty())
		{
			if (ClanController.ClanController_0.UserClanData_0 == null)
			{
				ClanTopWindowParams clanTopWindowParams = new ClanTopWindowParams();
				clanTopWindowParams.Boolean_0 = true;
				ClanTopWindow.Show(clanTopWindowParams);
			}
			else if (int_0 > 0)
			{
				UserClanData clanDataByUser = ClanController.ClanController_0.GetClanDataByUser(int_0);
				ClanWindowParams clanWindowParams = new ClanWindowParams();
				clanWindowParams.String_0 = clanDataByUser.string_0;
				clanWindowParams.UserClanData_0 = clanDataByUser;
				ClanWindow.Show(clanWindowParams);
			}
			else
			{
				ClanWindow.Show();
			}
			if (ProfileWindow.Boolean_1)
			{
				ProfileWindow.ProfileWindow_0.Hide();
			}
		}
	}

	private void OnGetClan(ClanController.EventData eventData_0)
	{
		if (eventData_0 == null || eventData_0.Dictionary_0 == null || eventData_0.Dictionary_0.Count <= 0 || int_0 == 0)
		{
			return;
		}
		foreach (KeyValuePair<string, UserClanData> item in eventData_0.Dictionary_0)
		{
			UserClanData value = item.Value;
			if (value == null || value.list_0 == null || value.list_0.Count <= 0)
			{
				continue;
			}
			for (int i = 0; i < value.list_0.Count; i++)
			{
				if (value.list_0[i].int_0 == int_0)
				{
					uilabel_0.String_0 = value.string_2;
					break;
				}
			}
		}
	}

	private void OnDestroy()
	{
		ClanController.ClanController_0.Unsubscribe(OnGetClan, ClanController.EventType.GET_CLAN_BY_USER_SUCCESS);
	}
}
