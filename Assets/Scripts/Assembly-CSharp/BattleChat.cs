using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class BattleChat : MonoBehaviour
{
	public UIInput input;

	public GameObject chat;

	public ChatItem template;

	public static string string_0 = "HH:mm:ss";

	private static float float_0 = 410f;

	private static float float_1 = 520f;

	private int int_0;

	private float float_2;

	private int int_1;

	private string string_1 = string.Empty;

	private void OnEnable()
	{
		InitChat();
		ChatController.ChatController_0.Subscribe(OnMessageReceived, ChatController.ChatEvents.MESSAGE_RECEIVED);
	}

	private void OnDestroy()
	{
		ChatController.ChatController_0.Unsubscribe(OnMessageReceived, ChatController.ChatEvents.MESSAGE_RECEIVED);
	}

	private void ClearChat()
	{
		foreach (Transform item in chat.transform)
		{
			Object.Destroy(item.gameObject);
		}
		int_0 = 0;
		float_2 = 0f;
		int_1 = 0;
	}

	private void InitChat()
	{
		List<ChatMessageData> messages = ChatController.ChatController_0.GetMessages(1);
		for (int i = 0; i < messages.Count; i++)
		{
			CreateChatMessage(messages[i]);
		}
	}

	private void CreateChatMessage(ChatMessageData chatMessageData_0)
	{
		if (int_0 > 60)
		{
			ChatController.ChatController_0.ClearMessages(1);
			ClearChat();
			InitChat();
		}
		else if (ChatController.ChatController_0.Boolean_1 || chatMessageData_0.int_0 != 0)
		{
			GameObject gameObject = NGUITools.AddChild(chat.gameObject, template.gameObject);
			gameObject.name = string.Format("{0:000}", int_0++);
			ChatItem component = gameObject.GetComponent<ChatItem>();
			component.SetMessage(chatMessageData_0);
			component.SetOnClickCallback(OnMessageClick);
			int int32_ = component.Int32_0;
			float_2 += int32_;
			Vector3 localPosition = gameObject.transform.localPosition;
			localPosition.x = float_0;
			localPosition.y = 0f;
			gameObject.transform.localPosition = localPosition;
			UpdateMessages(int32_, gameObject.name);
			NGUITools.SetActive(gameObject, true);
		}
	}

	private void UpdateMessages(int int_2, string string_2)
	{
		foreach (Transform item in chat.transform)
		{
			if (!item.gameObject.name.Equals(string_2))
			{
				Vector3 localPosition = item.localPosition;
				localPosition.y -= int_2;
				item.localPosition = localPosition;
			}
		}
		if (float_2 > float_1 && chat.transform.childCount > int_1)
		{
			GameObject obj = chat.transform.GetChild(int_1++).gameObject;
			Object.Destroy(obj);
		}
	}

	public void Send(string string_2)
	{
		print("sending " + string_2);
		string empty = string.Empty;
		if (string_2.Contains("/tell"))
		{
			string[] array = input.String_2.Split(' ');
			if (array.Length < 3)
			{
				return;
			}
			empty = array[0] + " " + array[1] + " ";
		}
		else
		{
			empty = string.Empty;
		}
		int length = empty.Length;
		int length2 = string_2.Length;
		string text = string_2.Substring((length <= length2) ? length : 0, (length <= length2) ? (length2 - length) : 0).Trim();
		if (!text.Equals(string.Empty))
		{
			ChatController.ChatController_0.SendMessage(empty + text, 3);
		}
	}

	public void OnSendButtonClick()
	{
		Send(input.String_2);
	}

	private void OnMessageReceived(ChatMessageData chatMessageData_0)
	{
		CreateChatMessage(chatMessageData_0);
	}

	private void OnMessageClick(ChatMessageData chatMessageData_0)
	{
		if (chatMessageData_0.int_1 != UserController.UserController_0.UserData_0.user_0.int_0 && !chatMessageData_0.string_2.IsNullOrEmpty())
		{
			string text = "/tell " + chatMessageData_0.string_2 + " ";
			input.String_2 = string.Empty;
			string_1 = text;
			input.Boolean_2 = true;
			Invoke("SetInput", 0.05f);
		}
	}

	private void SetInput()
	{
		input.String_2 = string_1;
		string_1 = string.Empty;
	}
}
