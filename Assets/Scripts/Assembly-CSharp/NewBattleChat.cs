using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using engine.unity;
using pixelgun.tutorial;

public class NewBattleChat : MonoBehaviour
{
	public UIInput input;

	public UIScrollView scroll;

	public ChatItem template;

	public UIWidget chatOffLabel;

	private static float float_0 = -95f;

	private static float float_1 = 15f;

	private static float float_2 = 19f;

	private static float float_3;

	private static int int_0 = 90;

	private static float float_4 = 8.5f;

	private int int_1;

	private float float_5 = float_0;

	private float float_6;

	private bool bool_0;

	private string string_0 = string.Empty;

	private string string_1 = string.Empty;

	private void Start()
	{
		scroll.verticalScrollBar.list_0.Add(new EventDelegate(OnScrollBarChange));
	}

	private void OnEnable()
	{
		ClearChat();
		InitChat();
		if (!ChatController.ChatController_0.Contains(OnMessgaeReceived))
		{
			ChatController.ChatController_0.Subscribe(OnMessgaeReceived, ChatController.ChatEvents.MESSAGE_RECEIVED);
		}
		PlayerStateController playerStateController_ = WeaponManager.weaponManager_0.myPlayerMoveC.PlayerStateController_0;
		if (!playerStateController_.Contains(OnStartPlayerRespawn, PlayerEvents.StartRespawn))
		{
			playerStateController_.Subscribe(OnStartPlayerRespawn, PlayerEvents.StartRespawn);
		}
	}

	private void OnDisable()
	{
		if (ChatController.ChatController_0.Contains(OnMessgaeReceived))
		{
			ChatController.ChatController_0.Unsubscribe(OnMessgaeReceived, ChatController.ChatEvents.MESSAGE_RECEIVED);
		}
		PlayerStateController playerStateController_ = WeaponManager.weaponManager_0.myPlayerMoveC.PlayerStateController_0;
		if (playerStateController_.Contains(OnStartPlayerRespawn, PlayerEvents.StartRespawn))
		{
			playerStateController_.Unsubscribe(OnStartPlayerRespawn, PlayerEvents.StartRespawn);
		}
	}

	private void ResetScroll()
	{
		Vector2 vector2_ = scroll.UIPanel_0.Vector2_0;
		vector2_.y = float_3;
		scroll.UIPanel_0.Vector2_0 = vector2_;
		Vector3 localPosition = scroll.transform.localPosition;
		localPosition.y = float_2;
		scroll.transform.localPosition = localPosition;
	}

	private IEnumerator TryResetScroll()
	{
		while (scroll.UIPanel_0.Vector2_0.y != 0f)
		{
			ResetScroll();
			ScrollDown();
			yield return null;
		}
	}

	private void ClearChat()
	{
		foreach (Transform item in scroll.transform)
		{
			Object.Destroy(item.gameObject);
		}
		int_1 = 0;
		float_5 = float_0;
		float_6 = 0f;
	}

	private void SetContent()
	{
		ClearChat();
		InitChat();
		StartCoroutine(TryResetScroll());
	}

	private void InitChat()
	{
		List<ChatMessageData> messages = ChatController.ChatController_0.GetMessages(1);
		for (int i = 0; i < messages.Count; i++)
		{
			CreateChatMessage(messages[i]);
			ScrollDown();
		}
	}

	private void CreateChatMessage(ChatMessageData chatMessageData_0)
	{
		if (int_1 > 60)
		{
			ChatController.ChatController_0.ClearMessages(1);
			ClearChat();
			InitChat();
			StartCoroutine(TryResetScroll());
		}
		else if ((ChatController.ChatController_0.Boolean_1 || chatMessageData_0.int_0 != 0) && Defs.Boolean_1 && !TutorialController.TutorialController_0.Boolean_0)
		{
			GameObject gameObject = NGUITools.AddChild(scroll.gameObject, template.gameObject);
			gameObject.name = string.Format("{0:000}", int_1++);
			ChatItem component = gameObject.GetComponent<ChatItem>();
			scroll.gameObject.GetComponent<UIPanel>();
			component.SetMessage(chatMessageData_0);
			int int32_ = component.Int32_0;
			float_5 = ((int_1 != 1) ? (float_5 - (float)int32_ * 0.5f - float_6 * 0.5f) : float_0);
			float_6 = int32_;
			Vector3 localPosition = gameObject.transform.localPosition;
			localPosition.y = float_5;
			gameObject.transform.localPosition = localPosition;
			NGUITools.SetActive(gameObject, true);
		}
	}

	private void OnMessgaeReceived(ChatMessageData chatMessageData_0)
	{
		if (scroll.transform.childCount >= int_0)
		{
			SetContent();
			return;
		}
		CreateChatMessage(chatMessageData_0);
		ScrollDown();
	}

	private void ScrollDown()
	{
		scroll.verticalScrollBar.Single_0 = 1f;
		scroll.UpdatePosition();
	}

	private void OnScrollBarChange()
	{
	}

	private void OnStartPlayerRespawn()
	{
		input.String_2 = string.Empty;
		input.Boolean_2 = false;
		input.enabled = false;
	}

	private void Update()
	{
		if (InputManager.GetButtonDown("Attack"))
		{
			Player_move_c.SetBlockKeyboardControl(false, true);
			input.Boolean_2 = false;
			input.enabled = false;
		}
		if (InputManager.GetButtonUp("Accept") && !TutorialController.TutorialController_0.Boolean_0)
		{
			if (input.isActiveAndEnabled && input.Boolean_2)
			{
				input.Boolean_2 = false;
				input.enabled = false;
				Player_move_c.SetBlockKeyboardControl(false, false);
			}
			else
			{
				if (!Defs.Boolean_1)
				{
					input.String_2 = string.Empty;
				}
				input.enabled = true;
				input.Boolean_2 = true;
				Player_move_c.SetBlockKeyboardControl(true, false);
			}
		}
		if (InputManager.GetButtonUp("Chat") && !input.Boolean_2)
		{
			input.enabled = true;
			input.Boolean_2 = true;
			Player_move_c.SetBlockKeyboardControl(true, false);
		}
	}

	private void DisableChatOff()
	{
		NGUITools.SetActive(chatOffLabel.gameObject, false);
	}

	public void OnSendButtonClick()
	{
		if (!Defs.Boolean_1)
		{
			ClearChat();
			input.String_2 = string.Empty;
			if (!TutorialController.TutorialController_0.Boolean_0 && !chatOffLabel.gameObject.activeSelf)
			{
				NGUITools.SetActive(chatOffLabel.gameObject, true);
				Invoke("DisableChatOff", 3f);
			}
			return;
		}
		string string_ = input.String_2;
		print("real sending " + string_);
		if (string_.Contains("/tell"))
		{
			string[] array = input.String_2.Split(' ');
			if (array.Length < 3)
			{
				return;
			}
			bool_0 = true;
			string_0 = array[0] + " " + array[1] + " ";
		}
		else
		{
			bool_0 = false;
			string_0 = string.Empty;
		}
		int length = string_0.Length;
		int length2 = string_.Length;
		string text = string_.Substring((length <= length2) ? length : 0, (length <= length2) ? (length2 - length) : 0).Trim();
		if (!text.Equals(string.Empty))
		{
			ChatMessageData myChatMessageData = new ChatMessageData();
			myChatMessageData.string_1 = text;
			myChatMessageData.string_2 = Defs.GetPlayerNameOrDefault();
			myChatMessageData.string_3 = Defs.GetPlayerNameOrDefault();
			ChatController.ChatController_0.AddMessage(myChatMessageData);
			WeaponManager.weaponManager_0.myPlayerMoveC.PhotonView_0.RPC("SendChatRPC", PhotonTargets.All, Defs.GetPlayerNameOrDefault(), text);
			input.String_2 = input.String_0;
			/*print("is chat controller null? " + System.Convert.ToString(ChatController.ChatController_0 == null));
			bool flag = ChatController.ChatController_0.SendMessage(string_0 + text, 3);
			input.String_2 = (bool_0 ? string_0 : ((!flag) ? string_ : input.String_0));*/
		}
	}

	private void SetInput()
	{
		input.String_2 = string_1;
		string_1 = string.Empty;
	}
}
