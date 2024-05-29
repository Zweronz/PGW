using System;
using System.Text;
using UnityEngine;
using WebSocketSharp;
using engine.unity;

public class ChatItem : MonoBehaviour
{
	public UIWidget widget;

	public UILabel msg;

	public UIPanel actionWidget;

	public UIWidget actionWidgetMissClickHandler;

	public int LINE_HEIGHT = 15;

	public int LINE_SYMBOLS = 84;

	public bool enableClick = true;

	public bool printTime = true;

	public string TIME_FORMAT = "HH:mm:ss";

	private static string[] string_0 = new string[8] { "[818181]", "[FFFFFF]", "[a7ff1a]", "[00DB0B]", "[EB0000]", "[2af2ff]", "[ffca3a]", "[ff2d2d]" };

	private static string string_1 = "[-]";

	private int int_0 = 11;

	private ChatMessageData chatMessageData_0;

	private Action<ChatMessageData> action_0;

	public ChatMessageData ChatMessageData_0
	{
		get
		{
			return chatMessageData_0;
		}
	}

	public int Int32_0
	{
		get
		{
			return GetLineCount() * LINE_HEIGHT;
		}
	}

	private int GetLineCount()
	{
		return (int)Math.Ceiling((double)(msg.String_0.Length - int_0) / (double)LINE_SYMBOLS);
	}

	public void SetMessage(ChatMessageData chatMessageData_1)
	{
		chatMessageData_0 = chatMessageData_1;
		StringBuilder stringBuilder = new StringBuilder();
		int num = UsersData.UsersData_0.UserData_0.user_0.int_0;
		int_0 = 0;
		if (printTime)
		{
			stringBuilder.Append(string_0[0]);
			stringBuilder.Append(chatMessageData_1.dateTime_0.ToString(TIME_FORMAT));
			stringBuilder.Append("  ");
			stringBuilder.Append(string_1);
			int_0 = 11;
		}
		string value = chatMessageData_0.NormalizedSenderNick(13);
		switch (chatMessageData_1.int_0)
		{
		case 0:
			int_0 += 11;
			stringBuilder.Append(string_0[4]);
			stringBuilder.Append(value);
			stringBuilder.Append(": ");
			stringBuilder.Append(chatMessageData_1.string_1);
			stringBuilder.Append(string_1);
			break;
		case 2:
		{
			int_0 += 33;
			string value2 = chatMessageData_0.NormalizedReceiverNick(13);
			stringBuilder.Append((chatMessageData_0.int_1 != num) ? string_0[1] : string_0[2]);
			stringBuilder.Append(value);
			stringBuilder.Append(" > ");
			stringBuilder.Append(string_1);
			stringBuilder.Append((chatMessageData_0.int_2 != num) ? string_0[1] : string_0[2]);
			stringBuilder.Append(value2);
			stringBuilder.Append(": ");
			stringBuilder.Append(string_1);
			stringBuilder.Append(string_0[3]);
			stringBuilder.Append(chatMessageData_1.string_1);
			stringBuilder.Append(string_1);
			break;
		}
		case 1:
		case 3:
			int_0 += 11;
			stringBuilder.Append((chatMessageData_0.int_1 != num) ? string_0[1] : string_0[2]);
			stringBuilder.Append(value);
			stringBuilder.Append(": ");
			stringBuilder.Append(chatMessageData_1.string_1);
			stringBuilder.Append(string_1);
			break;
		case 4:
			int_0 += 11;
			stringBuilder.Append(string_0[5]);
			stringBuilder.Append(value);
			stringBuilder.Append(": ");
			stringBuilder.Append(chatMessageData_1.string_1);
			stringBuilder.Append(string_1);
			break;
		case 5:
			int_0 += 11;
			stringBuilder.Append(string_0[6]);
			stringBuilder.Append(Localizer.Get("ui.chat.admin_voice"));
			stringBuilder.Append(": ");
			stringBuilder.Append(chatMessageData_1.string_1);
			stringBuilder.Append(string_1);
			break;
		case 6:
			int_0 += 11;
			stringBuilder.Append(string_0[7]);
			stringBuilder.Append(value);
			stringBuilder.Append(": ");
			stringBuilder.Append(chatMessageData_1.string_1);
			stringBuilder.Append(string_1);
			break;
		}
		if (chatMessageData_1.int_0 != 0 && chatMessageData_1.int_0 != 6 && chatMessageData_1.int_0 != 5)
		{
			base.gameObject.AddComponent<CursorChanger>();
		}
		msg.String_0 = stringBuilder.ToString();
		UpdateBounds();
		if (chatMessageData_0 != null && (chatMessageData_0.int_1 == 0 || chatMessageData_0.int_1 == UserController.UserController_0.UserData_0.user_0.int_0 || string.Equals("System", chatMessageData_0.string_2)))
		{
			enableClick = false;
		}
		if (!enableClick)
		{
			CursorChanger component = base.gameObject.GetComponent<CursorChanger>();
			if (component != null)
			{
				component.enabled = false;
			}
		}
	}

	public void SetOnClickCallback(Action<ChatMessageData> action_1)
	{
		action_0 = action_1;
	}

	private void UpdateBounds()
	{
		GetLineCount();
		int int32_ = Int32_0;
		widget.Int32_1 = int32_;
		msg.Int32_1 = int32_ * 2;
		Vector3 zero = Vector3.zero;
		zero = msg.transform.localPosition;
		zero.x = 0f;
		zero.y = (float)widget.Int32_1 * 0.5f;
		msg.transform.localPosition = zero;
	}

	private void OnClick()
	{
		if (!enableClick)
		{
			OnCloseActionPanel();
			return;
		}
		bool bool_ = false;
		if (ClanController.ClanController_0.UserClanData_0 != null && ClanController.ClanController_0.UserClanData_0.int_0 == UserController.UserController_0.UserData_0.user_0.int_0 && !ClanController.ClanController_0.UserClanData_0.IsMemderOfClan(chatMessageData_0.int_1) && chatMessageData_0.string_5.IsNullOrEmpty())
		{
			bool_ = true;
		}
		NGUITools.SetActive(actionWidget.gameObject, true);
		NGUITools.SetActive(actionWidgetMissClickHandler.gameObject, true);
		ActionPlayerNick component = actionWidget.GetComponent<ActionPlayerNick>();
		component.Show(chatMessageData_0.int_1, bool_, true, true);
		component.SetMessageAction(action_0, chatMessageData_0);
		component.SetCloseAction(OnCloseActionPanel);
		Vector3 localPosition = Input.mousePosition * ScreenController.ScreenController_0.Single_0;
		float num = (float)Screen.height * ScreenController.ScreenController_0.Single_0;
		localPosition.y -= num;
		localPosition.y = Math.Max(localPosition.y, -670f);
		actionWidget.transform.localPosition = localPosition;
	}

	public void OnCloseActionPanel()
	{
		NGUITools.SetActive(actionWidget.gameObject, false);
		NGUITools.SetActive(actionWidgetMissClickHandler.gameObject, false);
		actionWidget.GetComponent<ActionPlayerNick>();
	}
}
