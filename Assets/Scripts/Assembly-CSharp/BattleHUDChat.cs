using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleHUDChat : MonoBehaviour
{
	public GameObject chat;

	public ChatItem template;

	public static string string_0 = "HH:mm:ss";

	private static float float_0 = 410f;

	private static int int_0 = 3;

	private static float float_1 = 10f;

	private int int_1;

	private float float_2;

	private int int_2;

	private void Start()
	{
		ChatController.ChatController_0.Subscribe(OnMessageReceived, ChatController.ChatEvents.MESSAGE_RECEIVED);
	}

	private void OnDestroy()
	{
		ChatController.ChatController_0.Unsubscribe(OnMessageReceived, ChatController.ChatEvents.MESSAGE_RECEIVED);
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
		if (ChatController.ChatController_0.Boolean_1 || chatMessageData_0.int_0 != 0)
		{
			GameObject gameObject = NGUITools.AddChild(chat.gameObject, template.gameObject);
			gameObject.name = string.Format("{0:000}", int_1++);
			ChatItem component = gameObject.GetComponent<ChatItem>();
			component.SetMessage(chatMessageData_0);
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

	private void UpdateMessages(int int_3, string string_1)
	{
		int num = 0;
		foreach (Transform item in chat.transform)
		{
			num++;
			if (!item.gameObject.name.Equals(string_1))
			{
				Vector3 localPosition = item.localPosition;
				localPosition.y -= int_3;
				item.localPosition = localPosition;
			}
		}
	}

	private void OnMessageReceived(ChatMessageData chatMessageData_0)
	{
		CreateChatMessage(chatMessageData_0);
	}

	private void Update()
	{
		if (chat.transform.childCount > int_0)
		{
			UnityEngine.Object.Destroy(chat.transform.GetChild(0).gameObject);
			return;
		}
		DateTime now = DateTime.Now;
		foreach (Transform item in chat.transform)
		{
			ChatItem component = item.gameObject.GetComponent<ChatItem>();
			double totalSeconds = (now - component.ChatMessageData_0.dateTime_0).TotalSeconds;
			if (totalSeconds > (double)float_1)
			{
				UnityEngine.Object.Destroy(item.gameObject);
			}
		}
	}
}
