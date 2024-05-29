using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using engine.unity;

public class SelectMapChat : MonoBehaviour
{
	public enum ChatTabs
	{
		TAB_PUBLIC = 0,
		TAB_PRIVATE = 1,
		TAB_CLANS = 2
	}

	public UIInput input;

	public UIScrollView scroll;

	public UIWidget scrollBar;

	public ChatItem template;

	public UITabsContentController tabController;

	public ChatSettings chatSettings;

	public static string string_0 = "HH:mm:ss";

	private static float float_0 = 185f;

	private static float float_1 = 5f;

	private static float float_2 = 1f;

	private static float float_3 = 0.01f;

	private static float float_4 = -128f;

	private static float float_5 = 0f;

	private static int int_0 = 90;

	private static int int_1 = 1000;

	private static Dictionary<ChatTabs, int> dictionary_0 = new Dictionary<ChatTabs, int>
	{
		{
			ChatTabs.TAB_PUBLIC,
			1
		},
		{
			ChatTabs.TAB_PRIVATE,
			2
		},
		{
			ChatTabs.TAB_CLANS,
			4
		}
	};

	private ChatTabs chatTabs_0;

	private int int_2;

	private float float_6 = float_0;

	private float float_7;

	private bool bool_0;

	private bool bool_1;

	private bool bool_2;

	private string string_1 = string.Empty;

	private string string_2 = string.Empty;

	private void Awake()
	{
		scroll.verticalScrollBar.list_0.Add(new EventDelegate(OnScrollBarChange));
		tabController.onTabActive = SetTabContent;
	}

	private void Start()
	{
		ResetTemplate();
		StartCoroutine(TryResetScroll());
	}

	private void OnEnable()
	{
		ClearChat();
		InitChat();
		ChatController.ChatController_0.Subscribe(OnMessageReceived, ChatController.ChatEvents.MESSAGE_RECEIVED);
	}

	private void OnDisable()
	{
		ChatController.ChatController_0.Unsubscribe(OnMessageReceived, ChatController.ChatEvents.MESSAGE_RECEIVED);
	}

	private void ResetTemplate()
	{
		Vector3 localPosition = template.transform.localPosition;
		localPosition.x = -4f;
		localPosition.y = 175f;
		localPosition.z = 0f;
		template.transform.localPosition = localPosition;
		NGUITools.SetActive(template.gameObject, false);
	}

	private void ResetScroll()
	{
		Vector2 vector2_ = scroll.UIPanel_0.Vector2_0;
		vector2_.y = float_5;
		scroll.UIPanel_0.Vector2_0 = vector2_;
		Vector3 localPosition = scroll.transform.localPosition;
		localPosition.y = float_4;
		scroll.transform.localPosition = localPosition;
	}

	private IEnumerator TryResetScroll()
	{
		yield return null;
		ResetScroll();
		ScrollDown();
	}

	private void ClearChat()
	{
		foreach (Transform item in scroll.transform)
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}
		bool_0 = false;
		bool_1 = false;
		int_2 = 0;
		float_6 = float_0;
		float_7 = 0f;
	}

	private void SetTabContent(int int_3)
	{
		chatTabs_0 = (ChatTabs)int_3;
		ClearChat();
		InitChat();
		StartCoroutine(TryResetScroll());
	}

	private void InitChat()
	{
		List<ChatMessageData> messages = ChatController.ChatController_0.GetMessages(dictionary_0[chatTabs_0]);
		for (int i = 0; i < messages.Count; i++)
		{
			CreateChatMessage(messages[i]);
		}
	}

	private void CreateChatMessage(ChatMessageData chatMessageData_0)
	{
		if (int_2 > 60)
		{
			ChatController.ChatController_0.ClearMessages(dictionary_0[chatTabs_0]);
			ClearChat();
			InitChat();
			StartCoroutine(TryResetScroll());
		}
		else if ((ChatController.ChatController_0.Boolean_0 || chatMessageData_0.int_0 != 3) && (ChatController.ChatController_0.Boolean_1 || chatMessageData_0.int_0 != 0) && (chatMessageData_0.int_0 != 1 || chatTabs_0 != ChatTabs.TAB_PRIVATE) && (chatMessageData_0.int_0 != 3 || chatTabs_0 == ChatTabs.TAB_PUBLIC) && (chatMessageData_0.int_0 != 4 || chatTabs_0 == ChatTabs.TAB_CLANS) && (chatMessageData_0.int_0 == 4 || chatTabs_0 != ChatTabs.TAB_CLANS))
		{
			GameObject gameObject = NGUITools.AddChild(scroll.gameObject, template.gameObject);
			gameObject.name = string.Format("{0:000}", int_2++);
			ChatItem component = gameObject.GetComponent<ChatItem>();
			component.SetMessage(chatMessageData_0);
			component.SetOnClickCallback(OnMessageClick);
			int int32_ = component.Int32_0;
			float_6 = ((int_2 != 1) ? (float_6 - (float)int32_ * 0.5f - float_7 * 0.5f) : float_0);
			float_7 = int32_;
			Vector3 localPosition = gameObject.transform.localPosition;
			localPosition.y = float_6;
			gameObject.transform.localPosition = localPosition;
			NGUITools.SetActive(scrollBar.gameObject, int_2 > 7);
			NGUITools.SetActive(gameObject, true);
		}
	}

	private void OnMessageReceived(ChatMessageData chatMessageData_0)
	{
		if (scroll.transform.childCount >= int_0)
		{
			SetTabContent((int)chatTabs_0);
			return;
		}
		CreateChatMessage(chatMessageData_0);
		ScrollDown();
	}

	private void ScrollDown()
	{
		if (scroll.verticalScrollBar.Single_1 != 0f && bool_0)
		{
			scroll.verticalScrollBar.Single_0 = 1f;
			scroll.UpdatePosition();
		}
	}

	private void OnScrollBarChange()
	{
		bool_0 = Math.Abs(1f - scroll.verticalScrollBar.Single_0) < float_3;
	}

	private void Update()
	{
		if (InputManager.GetButtonUp("Accept") && input != null)
		{
			input.Boolean_2 = true;
		}
		if (scroll.verticalScrollBar.Single_1 != 0f && scroll.verticalScrollBar.Single_1 == 1f && !bool_1)
		{
			scroll.verticalScrollBar.Single_0 = 1f;
			scroll.UpdatePosition();
			bool_1 = true;
		}
	}

	public void OnSendButtonClick()
	{
		if (!input.Boolean_2)
		{
			input.Boolean_2 = true;
		}
		string text = input.String_2;
		if (text.Contains("/tell"))
		{
			string[] array = input.String_2.Split(' ');
			if (array.Length < 3)
			{
				return;
			}
			bool_2 = true;
			string_1 = array[0] + " " + array[1] + " ";
		}
		else
		{
			bool_2 = false;
			string_1 = string.Empty;
		}
		int length = string_1.Length;
		int length2 = text.Length;
		string text2 = text.Substring((length <= length2) ? length : 0, (length <= length2) ? (length2 - length) : 0).Trim();
		if (!text2.Equals(string.Empty))
		{
			bool flag = ChatController.ChatController_0.SendMessage(string_1 + text2, dictionary_0[chatTabs_0]);
			input.String_2 = (bool_2 ? ((!flag) ? text : string_1) : ((!flag) ? text : input.String_0));
		}
	}

	private void OnMessageClick(ChatMessageData chatMessageData_0)
	{
		if (chatMessageData_0.int_1 != UserController.UserController_0.UserData_0.user_0.int_0 && !chatMessageData_0.string_2.IsNullOrEmpty())
		{
			string text = "/tell " + chatMessageData_0.string_2 + " ";
			input.String_2 = string.Empty;
			string_2 = text;
			input.Boolean_2 = true;
			Invoke("SetInput", 0.05f);
			string_1 = text;
			bool_2 = true;
		}
	}

	private void SetInput()
	{
		input.String_2 = string_2;
		string_2 = string.Empty;
	}

	public void OnChatSettingsClick()
	{
		chatSettings.Show();
	}
}
