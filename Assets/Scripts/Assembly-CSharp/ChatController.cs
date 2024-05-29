using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.data;
using engine.events;
using engine.helpers;
using engine.network;
using engine.operations;

public sealed class ChatController : BaseEvent<ChatMessageData>
{
	public enum ChatEvents
	{
		MESSAGE_RECEIVED = 0
	}

	public enum LocalCommandType
	{
		FINAL_BATTLE = 0,
		KICK = 1,
		KICK_ALL = 2,
		GET_MASTER = 3,
		ADMIN_VOICE = 4
	}

	private const string string_0 = "chat_battle_not_in_lobby";

	private const string string_1 = "chat_system_not_in_lobby";

	private const int int_0 = 7;

	private const int int_1 = 30;

	public static string string_2 = "HH:mm:ss";

	private static BaseSharedSettings baseSharedSettings_0;

	private List<double> list_1 = new List<double>();

	private double double_0;

	private Dictionary<string, LocalCommandType> dictionary_1 = new Dictionary<string, LocalCommandType>
	{
		{
			"/final_battle",
			LocalCommandType.FINAL_BATTLE
		},
		{
			"/kick",
			LocalCommandType.KICK
		},
		{
			"/kickall",
			LocalCommandType.KICK_ALL
		},
		{
			"/get_master",
			LocalCommandType.GET_MASTER
		},
		{
			"/vm",
			LocalCommandType.ADMIN_VOICE
		}
	};

	private static ChatController chatController_0;

	private List<CircularBuffer<ChatMessageData>> list_2 = new List<CircularBuffer<ChatMessageData>>();

	private bool bool_0 = true;

	private bool bool_1 = true;

	[CompilerGenerated]
	private static Comparison<ChatMessageData> comparison_0;

	public static ChatController ChatController_0
	{
		get
		{
			if (chatController_0 == null)
			{
				chatController_0 = new ChatController();
			}
			return chatController_0;
		}
	}

	public bool Boolean_0
	{
		get
		{
			return bool_0;
		}
		set
		{
			bool_0 = value;
			baseSharedSettings_0.SetValue("chat_battle_not_in_lobby", bool_0 ? 1 : 0, true);
		}
	}

	public bool Boolean_1
	{
		get
		{
			return bool_1;
		}
		set
		{
			bool_1 = value;
			baseSharedSettings_0.SetValue("chat_system_not_in_lobby", bool_1 ? 1 : 0, true);
		}
	}

	private bool Boolean_2
	{
		get
		{
			User user_ = UsersData.UsersData_0.UserData_0.user_0;
			return user_.bool_0 || user_.bool_4;
		}
	}

	private ChatController()
	{
		for (int i = 0; i < 7; i++)
		{
			list_2.Add(new CircularBuffer<ChatMessageData>(30u));
		}
	}

	public void Init(BaseSharedSettings baseSharedSettings_1)
	{
		baseSharedSettings_0 = baseSharedSettings_1;
		if (baseSharedSettings_0 != null)
		{
			bool_0 = baseSharedSettings_0.GetValue("chat_battle_not_in_lobby", 1) == 1;
			bool_1 = baseSharedSettings_0.GetValue("chat_system_not_in_lobby", 0) == 1;
		}
	}

	public List<ChatMessageData> GetMessages(int int_2)
	{
		List<ChatMessageData> list = new List<ChatMessageData>();
		switch (int_2)
		{
		case 2:
			list.AddRange(list_2[0]);
			list.AddRange(list_2[6]);
			list.AddRange(list_2[2]);
			break;
		default:
			list.AddRange(list_2[0]);
			list.AddRange(list_2[6]);
			list.AddRange(list_2[1]);
			list.AddRange(list_2[2]);
			list.AddRange(list_2[5]);
			break;
		case 4:
			list.AddRange(list_2[4]);
			break;
		}
		list.Sort((ChatMessageData chatMessageData_0, ChatMessageData chatMessageData_1) => chatMessageData_0.dateTime_0.CompareTo(chatMessageData_1.dateTime_0));
		return list;
	}

	public void ClearMessages(int int_2)
	{
		int num = 0;
		switch (int_2)
		{
		case 2:
			num = Mathf.Max(list_2[0].List_0.Count - 10, 0);
			list_2[0].List_0.RemoveRange(0, num);
			num = Mathf.Max(list_2[6].List_0.Count - 10, 0);
			list_2[6].List_0.RemoveRange(0, num);
			num = Mathf.Max(list_2[2].List_0.Count - 10, 0);
			list_2[2].List_0.RemoveRange(0, num);
			break;
		default:
			num = Mathf.Max(list_2[0].List_0.Count - 10, 0);
			list_2[0].List_0.RemoveRange(0, num);
			num = Mathf.Max(list_2[6].List_0.Count - 10, 0);
			list_2[6].List_0.RemoveRange(0, num);
			num = Mathf.Max(list_2[2].List_0.Count - 10, 0);
			list_2[2].List_0.RemoveRange(0, num);
			num = Mathf.Max(list_2[1].List_0.Count - 10, 0);
			list_2[1].List_0.RemoveRange(0, num);
			num = Mathf.Max(list_2[5].List_0.Count - 10, 0);
			list_2[5].List_0.RemoveRange(0, num);
			break;
		case 4:
			num = Mathf.Max(list_2[4].List_0.Count - 10, 0);
			list_2[4].List_0.RemoveRange(0, num);
			break;
		}
	}

	public void AddMessage(ChatMessageData chatMessageData_0, bool bool_2 = true)
	{
		if (UsersData.UsersData_0 == null || UsersData.UsersData_0.UserData_0 == null)
		{
			return;
		}
		int num = UsersData.UsersData_0.UserData_0.user_0.int_0;
		if ((bool_2 && chatMessageData_0.int_1 == num) || (chatMessageData_0.string_4 != null && chatMessageData_0.string_4 != LocalizationStore.GetCurrentLanguageCode()))
		{
			return;
		}
		chatMessageData_0.dateTime_0 = DateTime.Now;
		int num2 = 0;
		int num3 = chatMessageData_0.int_0;
		num2 = ((num3 == 3) ? 1 : chatMessageData_0.int_0);
		if (num2 < list_2.Count)
		{
			if (chatMessageData_0.int_0 == 2 && chatMessageData_0.int_1 != num && chatMessageData_0.int_2 != num)
			{
				return;
			}
			list_2[num2].Add(chatMessageData_0);
		}
		Dispatch(chatMessageData_0, ChatEvents.MESSAGE_RECEIVED);
	}

	public void AddMessages(List<ChatMessageData> list_3, bool bool_2 = true)
	{
		foreach (ChatMessageData item in list_3)
		{
			if (item.int_0 != 4 || (ClanController.ClanController_0.UserClanData_0 != null && (ClanController.ClanController_0.UserClanData_0 == null || ClanController.ClanController_0.UserClanData_0.string_0.Equals(item.string_5))))
			{
				AddMessage(item, bool_2);
				continue;
			}
			break;
		}
	}

	public bool SendMessage(string string_3, int int_2 = -1)
	{
		if (!CheckSpecialWord(string_3) && !MessageWindow.MessageWindow_0)
		{
			double double_ = 0.0;
			if (checkSpam(out double_))
			{
				ChatMessageData chatMessageData = new ChatMessageData();
				chatMessageData.string_1 = string.Format(Localizer.Get("msg.chat.spam"), double_);
				chatMessageData.int_0 = 6;
				AddMessage(chatMessageData, false);
				return false;
			}
			string string_4 = string.Empty;
			int int_3 = 6;
			if (checkCommands(string_3, out string_4, out int_3))
			{
				ChatMessageData chatMessageData2 = new ChatMessageData();
				if (string.IsNullOrEmpty(string_4))
				{
					chatMessageData2.string_1 = string_3;
				}
				else
				{
					chatMessageData2.string_1 = string_4;
				}
				chatMessageData2.int_0 = int_3;
				if (int_3 == 5)
				{
					chatMessageData2.int_0 = 1;
					chatMessageData2.string_4 = LocalizationStore.GetCurrentLanguageCode();
					Send(chatMessageData2);
				}
				else
				{
					AddMessage(chatMessageData2, false);
				}
				return true;
			}
			ChatMessageData chatMessageData3 = new ChatMessageData();
			chatMessageData3.string_1 = Utility.DeleteBBCode(string_3);
			chatMessageData3.string_4 = LocalizationStore.GetCurrentLanguageCode();
			if (int_2 != -1)
			{
				chatMessageData3.int_0 = int_2;
			}
			Send(chatMessageData3);
			return true;
		}
		return false;
	}

	private bool checkSpam(out double double_1)
	{
		double_1 = 0.0;
		if (ChatParamSettings.Get != null && ChatParamSettings.Get.checkMessageTime > 0f)
		{
			double num = Time.realtimeSinceStartup;
			if (num - double_0 <= (double)ChatParamSettings.Get.checkMessageTime)
			{
				double_1 = (double)ChatParamSettings.Get.checkMessageTime - num + double_0;
				Log.AddLine("Mute for spam in chat", Log.LogLevel.WARNING);
				return true;
			}
			double_0 = num;
			return false;
		}
		return false;
	}

	private bool checkCommands(string string_3, out string string_4, out int int_2)
	{
		int_2 = 0;
		string_4 = string.Empty;
		string[] array = string_3.Split(' ');
		if (array != null && array.Length > 0 && dictionary_1.ContainsKey(array[0]))
		{
			return useCommand(array, dictionary_1[array[0]], out string_4, out int_2);
		}
		return false;
	}

	private bool useCommand(string[] string_3, LocalCommandType localCommandType_0, out string string_4, out int int_2)
	{
		int_2 = 6;
		string_4 = string.Empty;
		switch (localCommandType_0)
		{
		default:
			return false;
		case LocalCommandType.FINAL_BATTLE:
			return finalBattleCommand();
		case LocalCommandType.KICK:
			return kickCommand(string_3);
		case LocalCommandType.KICK_ALL:
			return kickAllCommand();
		case LocalCommandType.GET_MASTER:
			return getMasterCommand(out string_4);
		case LocalCommandType.ADMIN_VOICE:
		{
			int_2 = 5;
			User user_ = UsersData.UsersData_0.UserData_0.user_0;
			return user_.bool_0;
		}
		}
	}

	private bool finalBattleCommand()
	{
		if (!Boolean_2)
		{
			return false;
		}
		if (WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.myNetworkStartTable != null)
		{
			WeaponManager.weaponManager_0.myNetworkStartTable.ForceFinalBattle();
			return true;
		}
		return false;
	}

	private bool kickCommand(string[] string_3)
	{
		if (!Boolean_2)
		{
			return false;
		}
		if (string_3.Length < 2)
		{
			return false;
		}
		if (WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.myNetworkStartTable != null)
		{
			WeaponManager.weaponManager_0.myNetworkStartTable.Kick(string_3[1]);
			return true;
		}
		return false;
	}

	private bool kickAllCommand()
	{
		if (!Boolean_2)
		{
			return false;
		}
		if (WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.myNetworkStartTable != null)
		{
			WeaponManager.weaponManager_0.myNetworkStartTable.KickAll();
			return true;
		}
		return false;
	}

	private bool getMasterCommand(out string string_3)
	{
		string_3 = string.Empty;
		if (!UsersData.UsersData_0.UserData_0.user_0.bool_0)
		{
			return false;
		}
		if (WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.myNetworkStartTable != null)
		{
			PhotonPlayer[] photonPlayer_ = PhotonNetwork.PhotonPlayer_2;
			if (photonPlayer_ != null && photonPlayer_.Length > 0)
			{
				int num = 0;
				int num2 = 0;
				string empty = string.Empty;
				bool flag = false;
				bool flag2 = false;
				for (int i = 0; i < photonPlayer_.Length; i++)
				{
					if (photonPlayer_[i].Boolean_0)
					{
						num2 = photonPlayer_[i].Int32_0;
						num = (int)photonPlayer_[i].Hashtable_0["uid"];
						empty = photonPlayer_[i].String_0;
					}
				}
				GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
				GameObject[] array2 = array;
				foreach (GameObject gameObject in array2)
				{
					NetworkStartTable component = gameObject.GetComponent<NetworkStartTable>();
					if (component.PhotonView_0.PhotonPlayer_0.Int32_0 == num2)
					{
						flag = true;
						if (component.Player_move_c_0 != null)
						{
							flag2 = true;
						}
					}
				}
				string_3 = string.Format("name: {0} uid: {1} nst: {2} plr: {3}", empty, num, (!flag) ? "F" : "T", (!flag2) ? "F" : "T");
				return true;
			}
		}
		return false;
	}

	private void Send(ChatMessageData chatMessageData_0)
	{
		OperationsManager.OperationsManager_0.Add(delegate
		{
			AbstractNetworkCommand.Send(new ChatMsgSendNetworkCommand
			{
				chatMessageData_0 = chatMessageData_0
			});
		});
	}

	private bool CheckSpecialWord(string string_3)
	{
		return false;
	}
}
