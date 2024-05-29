using System;
using UnityEngine;

public class ActionPlayerNick : MonoBehaviour
{
	public UIWidget Body;

	public UITable table;

	public UIWidget InviteToClan;

	public UIWidget PrivateMessage;

	public UIWidget Profile;

	public int ElementHeight;

	private bool bool_0;

	private GameObject gameObject_0;

	private int int_0;

	private Action action_0;

	private Action<ChatMessageData> action_1;

	private Action action_2;

	private Action action_3;

	private ChatMessageData chatMessageData_0;

	public void SetInviteAction(Action action_4)
	{
		action_0 = action_4;
	}

	public void SetMessageAction(Action<ChatMessageData> action_4, ChatMessageData chatMessageData_1)
	{
		action_1 = action_4;
		chatMessageData_0 = chatMessageData_1;
	}

	public void SetProfileAction(Action action_4)
	{
		action_2 = action_4;
	}

	public void SetCloseAction(Action action_4)
	{
		action_3 = action_4;
	}

	public void Show(int int_1, bool bool_1, bool bool_2, bool bool_3)
	{
		int_0 = int_1;
		action_0 = null;
		action_1 = null;
		action_2 = null;
		action_3 = null;
		chatMessageData_0 = null;
		if (InviteToClan != null)
		{
			InviteToClan.gameObject.SetActive(false);
		}
		if (PrivateMessage != null)
		{
			PrivateMessage.gameObject.SetActive(false);
		}
		if (Profile != null)
		{
			Profile.gameObject.SetActive(false);
		}
		while (table.transform.childCount > 0)
		{
			Transform child = table.transform.GetChild(0);
			child.parent = null;
			child.gameObject.SetActive(false);
			UnityEngine.Object.Destroy(child.gameObject);
		}
		int num = 0;
		GameObject gameObject = null;
		if (bool_1 && InviteToClan != null)
		{
			bool_0 = false;
			gameObject = NGUITools.AddChild(table.gameObject, InviteToClan.gameObject);
			gameObject_0 = gameObject.transform.Find("label").gameObject;
			gameObject_0.GetComponent<UILabel>().String_0 = Localizer.Get("ui.chat.invite_clan");
			gameObject.SetActive(true);
			num++;
		}
		if (bool_2 && PrivateMessage != null)
		{
			gameObject = NGUITools.AddChild(table.gameObject, PrivateMessage.gameObject);
			gameObject.SetActive(true);
			num++;
		}
		if (bool_3 && Profile != null)
		{
			gameObject = NGUITools.AddChild(table.gameObject, Profile.gameObject);
			gameObject.SetActive(true);
			num++;
		}
		Body.Int32_1 = Convert.ToInt32((float)(ElementHeight * num) + table.vector2_0.y * (float)num + 4f);
		table.Boolean_0 = true;
	}

	public void OnInviteClick()
	{
		if (!bool_0)
		{
			ClanController.ClanController_0.SendClanMessage(0, string.Empty, int_0, string.Empty);
			gameObject_0.GetComponent<UILabel>().String_0 = Localizer.Get("ui.chat.invite_clan_sended");
			gameObject_0.GetComponent<UIButton>().Boolean_0 = false;
			bool_0 = true;
			if (action_0 != null)
			{
				action_0();
			}
		}
	}

	public void OnPrivateMessageClick()
	{
		if (chatMessageData_0 != null && action_1 != null)
		{
			action_1(chatMessageData_0);
		}
		if (action_3 != null)
		{
			action_3();
		}
	}

	public void OnProfileClick()
	{
		ProfileWindowController.ProfileWindowController_0.ShowProfileWindowForPlayer(int_0);
		if (action_2 != null)
		{
			action_2();
		}
		if (action_3 != null)
		{
			action_3();
		}
	}
}
