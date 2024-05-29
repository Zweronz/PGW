using System;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using engine.network;

public class ClanMessageBodySlot : MonoBehaviour
{
	public UILabel FromPlayerLabel;

	public UILabel FromClanNameLabel;

	public UILabel DateLabel;

	public UILabel MainTextLabel;

	public UIButton AcceptButton;

	public UIButton RejectButton;

	public UIButton DeleteMessageButton;

	private ClanMessageData clanMessageData_0;

	public void SetData(ClanMessageData clanMessageData_1)
	{
		NGUITools.SetActive(base.gameObject, true);
		clanMessageData_0 = clanMessageData_1;
		UserClanData clan = ClanController.ClanController_0.GetClan(clanMessageData_0.string_1);
		FromPlayerLabel.String_0 = ((clanMessageData_0.int_18 > 0) ? clanMessageData_0.string_2 : ((clan == null) ? string.Empty : clan.string_1));
		FromClanNameLabel.String_0 = ((clan == null) ? string.Empty : clan.string_2);
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(clanMessageData_0.int_19).ToLocalTime();
		DateLabel.String_0 = dateTime.ToString("M") + " " + dateTime.ToString("T");
		if (!clanMessageData_0.string_1.IsNullOrEmpty())
		{
			MainTextLabel.String_0 = string.Format(Localizer.Get("clan.message." + clanMessageData_0.int_16 + ".clan"), (clan == null) ? string.Empty : clan.string_2);
			if (clan != null && !MainTextLabel.String_0.Contains(clan.string_2))
			{
				BoxCollider component = MainTextLabel.gameObject.GetComponent<BoxCollider>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
		}
		else if (clanMessageData_0.int_18 > 0)
		{
			MainTextLabel.String_0 = string.Format(Localizer.Get("clan.message." + clanMessageData_0.int_16 + ".user"), clanMessageData_0.string_2);
			BoxCollider component2 = MainTextLabel.gameObject.GetComponent<BoxCollider>();
			if (component2 != null)
			{
				component2.enabled = false;
			}
		}
		if (clanMessageData_0.int_16 >= 1000)
		{
			MainTextLabel.String_0 = Localizer.Get("clan.message." + clanMessageData_0.int_16 + ".clan");
			FromPlayerLabel.String_0 = string.Empty;
			FromClanNameLabel.String_0 = string.Empty;
		}
		NGUITools.SetActive(AcceptButton.gameObject, clanMessageData_0.int_16 == 0);
		NGUITools.SetActive(RejectButton.gameObject, clanMessageData_0.int_16 == 0);
	}

	public void OnAccept()
	{
		if (ClanController.ClanController_0.UserClanData_0 != null && ClanController.ClanController_0.UserClanData_0.int_0 != UserController.UserController_0.UserData_0.user_0.int_0)
		{
			MessageWindow.Show(new MessageWindowParams(LocalizationStorage.Get.Term("ui.clan_wnd.you_alredy_in_clan")));
			return;
		}
		bool flag;
		if ((flag = ClanController.ClanController_0.UserClanData_0 != null && ClanController.ClanController_0.UserClanData_0.int_0 == UserController.UserController_0.UserData_0.user_0.int_0) && ClanController.ClanController_0.UserClanData_0.list_0.Count >= ClanController.ClanController_0.GetClanMembersMaxByLevel(ClanController.ClanController_0.UserClanData_0.int_2))
		{
			MessageWindow.Show(new MessageWindowParams(LocalizationStorage.Get.Term("ui.clan_wnd.to_many_mambers")));
			return;
		}
		ClanController.ClanController_0.SendClanMessage(2, clanMessageData_0.string_0, 0, string.Empty);
		ClanController.ClanController_0.GetClanMessageList();
		OnDeleteMessage();
		if (!flag)
		{
			ClanMessageWindow.ClanMessageWindow_0.Hide();
		}
	}

	public void OnReject()
	{
		ClanController.ClanController_0.SendClanMessage(4, clanMessageData_0.string_0, 0, string.Empty);
		ClanController.ClanController_0.GetClanMessageList();
		OnDeleteMessage();
	}

	public void OnDeleteMessage()
	{
		ClanController.ClanController_0.ClanMessageDelete(clanMessageData_0.string_0);
		ClanMessageWindow.ClanMessageWindow_0.DeleteMessage(clanMessageData_0.string_0);
	}

	public void OnNickClick()
	{
		if (clanMessageData_0.int_18 > 0)
		{
			ProfileWindowController.ProfileWindowController_0.ShowProfileWindowForPlayer(clanMessageData_0.int_18);
			return;
		}
		UserClanData clan = ClanController.ClanController_0.GetClan(clanMessageData_0.string_1);
		if (clan != null)
		{
			ProfileWindowController.ProfileWindowController_0.ShowProfileWindowForPlayer(clan.int_0);
		}
	}

	public void OnMessageClick()
	{
		string urlAtPosition = MainTextLabel.GetUrlAtPosition(UICamera.vector3_0);
		if (!string.IsNullOrEmpty(urlAtPosition))
		{
			OnSearchClanClick();
		}
	}

	public void OnSearchClanClick()
	{
		GetUserClanDataNetworkCommand getUserClanDataNetworkCommand = new GetUserClanDataNetworkCommand();
		getUserClanDataNetworkCommand.string_0 = clanMessageData_0.string_1;
		AbstractNetworkCommand.Send(getUserClanDataNetworkCommand);
		ClanController.ClanController_0.Subscribe(OnGetClan, ClanController.EventType.SEARCH_SUCCESS);
	}

	private void OnGetClan(ClanController.EventData eventData_0)
	{
		if (eventData_0 != null && eventData_0.Dictionary_0 != null && eventData_0.Dictionary_0.Count > 0)
		{
			foreach (KeyValuePair<string, UserClanData> item in eventData_0.Dictionary_0)
			{
				UserClanData value = item.Value;
				if (value != null && value.string_0 == clanMessageData_0.string_1)
				{
					ClanWindowParams clanWindowParams = new ClanWindowParams();
					clanWindowParams.String_0 = value.string_0;
					clanWindowParams.UserClanData_0 = value;
					ClanWindow.Show(clanWindowParams);
					if (ClanMessageWindow.ClanMessageWindow_0 != null)
					{
						ClanMessageWindow.ClanMessageWindow_0.Hide();
					}
					return;
				}
			}
		}
		MessageWindow.Show(new MessageWindowParams(Localizer.Get("msg.clan_not_found")));
	}
}
