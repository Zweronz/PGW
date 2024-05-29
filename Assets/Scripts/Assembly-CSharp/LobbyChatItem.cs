using System;
using System.Text;
using UnityEngine;

public class LobbyChatItem : MonoBehaviour
{
	public UIWidget widget;

	public UILabel msg;

	public int LINE_HEIGHT = 15;

	public int LINE_SYMBOLS = 47;

	public bool enableClick = true;

	public string TIME_FORMAT = "HH:mm:ss";

	private static string[] string_0 = new string[5] { "[818181]", "[FFFFFF]", "[03A9EC]", "[00DB0B]", "[EB0000]" };

	private static string string_1 = "[-]";

	private int int_0 = 22;

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
		stringBuilder.Append(string_0[0]);
		stringBuilder.Append(chatMessageData_1.dateTime_0.ToString(TIME_FORMAT));
		stringBuilder.Append("  ");
		stringBuilder.Append(string_1);
		switch (chatMessageData_1.int_0)
		{
		case 0:
			int_0 = 22;
			stringBuilder.Append(string_0[4]);
			stringBuilder.Append(chatMessageData_1.string_2);
			stringBuilder.Append(": ");
			stringBuilder.Append(chatMessageData_1.string_1);
			stringBuilder.Append(string_1);
			break;
		case 2:
			int_0 = 44;
			stringBuilder.Append((chatMessageData_0.int_1 != num) ? string_0[1] : string_0[2]);
			stringBuilder.Append(chatMessageData_0.string_2);
			stringBuilder.Append(" > ");
			stringBuilder.Append(string_1);
			stringBuilder.Append((chatMessageData_0.int_2 != num) ? string_0[1] : string_0[2]);
			stringBuilder.Append(chatMessageData_0.string_3);
			stringBuilder.Append(": ");
			stringBuilder.Append(string_1);
			stringBuilder.Append(string_0[3]);
			stringBuilder.Append(chatMessageData_1.string_1);
			stringBuilder.Append(string_1);
			break;
		case 1:
		case 3:
			int_0 = 22;
			stringBuilder.Append((chatMessageData_0.int_1 != num) ? string_0[1] : string_0[2]);
			stringBuilder.Append(chatMessageData_1.string_2);
			stringBuilder.Append(": ");
			stringBuilder.Append(chatMessageData_1.string_1);
			stringBuilder.Append(string_1);
			break;
		}
		msg.String_0 = stringBuilder.ToString();
		UpdateBounds();
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
		msg.Int32_1 = int32_;
		Vector3 zero = Vector3.zero;
		zero = msg.transform.localPosition;
		zero.x = 0f;
		zero.y = (float)msg.Int32_1 * 0.5f;
		msg.transform.localPosition = zero;
	}

	private void OnClick()
	{
		if (enableClick)
		{
			if (action_0 != null && chatMessageData_0 != null)
			{
				action_0(chatMessageData_0);
			}
			Debug.Log("LobbyChatItem::OnClick > nick: " + chatMessageData_0.string_2);
		}
	}
}
