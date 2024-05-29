using System.Collections.Generic;
using ProtoBuf;
using engine.controllers;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class UserDataUpdateNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public User user_0;

	[ProtoMember(3)]
	public Dictionary<string, UserArtikul> dictionary_0;

	[ProtoMember(4)]
	public string[] string_0;

	[ProtoMember(5)]
	public Dictionary<string, UserFriend> dictionary_1;

	[ProtoMember(6)]
	public string[] string_1;

	[ProtoMember(7)]
	public Dictionary<SkillId, SkillData> dictionary_2;

	[ProtoMember(8)]
	public SkillId[] skillId_0;

	[ProtoMember(9)]
	public Dictionary<SlotType, int> dictionary_3;

	[ProtoMember(10)]
	public SlotType[] slotType_0;

	[ProtoMember(11)]
	public Dictionary<string, UserTextureSkinData> dictionary_4;

	[ProtoMember(12)]
	public string[] string_2;

	[ProtoMember(13)]
	public UserProfileStatData userProfileStatData_0;

	[ProtoMember(14)]
	public int int_1;

	[ProtoMember(15)]
	public UserRankData userRankData_0;

	[ProtoMember(16)]
	public Dictionary<UserTimerData.UserTimerType, List<UserTimerData>> dictionary_5;

	[ProtoMember(19)]
	public Dictionary<int, int> dictionary_6;

	private bool bool_0;

	private bool bool_1;

	private UserData userData_0;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public UserDataUpdateNetworkCommand()
	{
		hashSet_0 = new HashSet<AppStateController.States>
		{
			AppStateController.States.GAME_LOADING,
			AppStateController.States.GAME_LOADED,
			AppStateController.States.MAIN_MENU,
			AppStateController.States.JOINED_ROOM,
			AppStateController.States.IN_BATTLE,
			AppStateController.States.IN_BATTLE_OVER_WINDOW,
			AppStateController.States.LEAVING_ROOM
		};
	}

	public override void DeferredRun()
	{
		base.DeferredRun();
		bool_0 = false;
		userData_0 = null;
		if (InitCurrentUser())
		{
			UpdateUser();
			UpdateArtikuls();
			UpdateFriends();
			UpdateSkills();
			UpdateSlots();
			UpdateSkins();
			UpdateProfileStat();
			UpdateUserRankData();
			UpdateUserTimers();
			UpdatePlaceSeason();
			if (bool_0)
			{
				UsersData.Dispatch(UsersData.EventType.INVENTORY_UPDATE);
			}
		}
	}

	private bool InitCurrentUser()
	{
		userData_0 = UsersData.UsersData_0.usersStorage_0.GetObjectByKey(int_1);
		if (userData_0 == null)
		{
			Log.AddLine(string.Format("[UserDataUpdateNetworkCommand::InitCurrentUser] ERROR UID {0}", int_1), Log.LogLevel.ERROR);
			return false;
		}
		return true;
	}

	private void UpdateUser()
	{
		if (user_0 != null)
		{
			User user = userData_0.user_0;
			if (user != null && user.int_0 != user_0.int_0)
			{
				Log.AddLineError("[UserDataUpdateNetworkCommand::UpdateUser] ERROR UID CHANGE oldUID={0} newUID={1}", user.int_0, user_0.int_0);
			}
			if (user != null && user.int_7 != user_0.int_7)
			{
				bool_1 = true;
			}
			userData_0.user_0 = user_0;
			UsersData.Dispatch(UsersData.EventType.USER_CHANGED);
		}
	}

	private void UpdateArtikuls()
	{
		if (userData_0 == null)
		{
			return;
		}
		bool flag = false;
		if (string_0 != null && string_0.Length > 0)
		{
			flag = true;
			for (int i = 0; i < string_0.Length; i++)
			{
				UserArtikul userArtikulById = UserController.UserController_0.GetUserArtikulById(string_0[i]);
				userData_0.userArtikulStorage_0.RemoveObject(string_0[i]);
				UsersData.Dispatch(UsersData.EventType.ARTIKUL_CHANGED, new UsersData.EventData
				{
					string_0 = string_0[i],
					int_0 = userArtikulById.ArtikulData_0.Int32_0
				});
			}
		}
		if (dictionary_0 != null && dictionary_0.Count > 0)
		{
			flag = true;
			foreach (KeyValuePair<string, UserArtikul> item in dictionary_0)
			{
				userData_0.userArtikulStorage_0.UpdateObject(item.Key, item.Value);
				UsersData.Dispatch(UsersData.EventType.ARTIKUL_CHANGED, new UsersData.EventData
				{
					string_0 = item.Key,
					int_0 = item.Value.int_0
				});
			}
		}
		if (flag)
		{
			bool_0 = true;
			UsersData.Dispatch(UsersData.EventType.ARTIKULS_CHANGED);
		}
	}

	private void UpdateFriends()
	{
		if (userData_0 == null)
		{
			return;
		}
		bool flag = false;
		if (string_1 != null && string_1.Length > 0)
		{
			flag = true;
			for (int i = 0; i < string_1.Length; i++)
			{
				userData_0.userFriendStorage_0.RemoveObject(string_1[i]);
				UsersData.Dispatch(UsersData.EventType.FRIEND_CHANGED, new UsersData.EventData
				{
					string_0 = string_1[i]
				});
			}
		}
		if (dictionary_1 != null && dictionary_1.Count > 0)
		{
			flag = true;
			foreach (KeyValuePair<string, UserFriend> item in dictionary_1)
			{
				userData_0.userFriendStorage_0.UpdateObject(item.Key, item.Value);
				UsersData.Dispatch(UsersData.EventType.FRIEND_CHANGED, new UsersData.EventData
				{
					string_0 = item.Key
				});
			}
		}
		if (flag)
		{
			UsersData.Dispatch(UsersData.EventType.FRIENDS_CHANGED);
		}
	}

	private void UpdateSkills()
	{
		if (userData_0 == null)
		{
			return;
		}
		bool flag = false;
		if (skillId_0 != null && skillId_0.Length > 0)
		{
			flag = true;
			for (int i = 0; i < skillId_0.Length; i++)
			{
				if (userData_0.dictionary_2.ContainsKey(skillId_0[i]))
				{
					userData_0.dictionary_2.Remove(skillId_0[i]);
				}
				UsersData.Dispatch(UsersData.EventType.SKILL_CHANGED, new UsersData.EventData
				{
					int_0 = (int)skillId_0[i]
				});
			}
		}
		if (dictionary_2 != null && dictionary_2.Count > 0)
		{
			flag = true;
			foreach (KeyValuePair<SkillId, SkillData> item in dictionary_2)
			{
				if (userData_0.dictionary_2.ContainsKey(item.Key))
				{
					userData_0.dictionary_2[item.Key] = item.Value;
				}
				else
				{
					userData_0.dictionary_2.Add(item.Key, item.Value);
				}
				UsersData.Dispatch(UsersData.EventType.SKILL_CHANGED, new UsersData.EventData
				{
					int_0 = (int)item.Key
				});
			}
		}
		if (flag)
		{
			UsersData.Dispatch(UsersData.EventType.SKILLS_CHANGED);
		}
	}

	private void UpdateSlots()
	{
		if (userData_0 == null)
		{
			return;
		}
		bool flag = false;
		if (slotType_0 != null && slotType_0.Length > 0)
		{
			flag = true;
			for (int i = 0; i < slotType_0.Length; i++)
			{
				if (userData_0.dictionary_3.ContainsKey(slotType_0[i]))
				{
					userData_0.dictionary_3.Remove(slotType_0[i]);
				}
				UsersData.Dispatch(UsersData.EventType.SLOT_CHANGED, new UsersData.EventData
				{
					int_0 = (int)slotType_0[i]
				});
			}
		}
		if (dictionary_3 != null && dictionary_3.Count > 0)
		{
			flag = true;
			foreach (KeyValuePair<SlotType, int> item in dictionary_3)
			{
				if (userData_0.dictionary_3.ContainsKey(item.Key))
				{
					userData_0.dictionary_3[item.Key] = item.Value;
				}
				else
				{
					userData_0.dictionary_3.Add(item.Key, item.Value);
				}
				UsersData.Dispatch(UsersData.EventType.SLOT_CHANGED, new UsersData.EventData
				{
					int_0 = (int)item.Key
				});
			}
		}
		if (flag)
		{
			bool_0 = true;
			UsersData.Dispatch(UsersData.EventType.SLOTS_CHANGED);
		}
	}

	private void UpdateSkins()
	{
		if (userData_0 == null)
		{
			return;
		}
		bool flag = false;
		if (string_2 != null && string_2.Length > 0)
		{
			flag = true;
			for (int i = 0; i < string_2.Length; i++)
			{
				if (userData_0.dictionary_4.ContainsKey(string_2[i]))
				{
					userData_0.dictionary_4.Remove(string_2[i]);
				}
				UsersData.Dispatch(UsersData.EventType.SKIN_CHANGED, new UsersData.EventData
				{
					string_0 = string_2[i]
				});
			}
		}
		if (dictionary_4 != null && dictionary_4.Count > 0)
		{
			flag = true;
			foreach (KeyValuePair<string, UserTextureSkinData> item in dictionary_4)
			{
				if (userData_0.dictionary_4.ContainsKey(item.Key))
				{
					userData_0.dictionary_4[item.Key] = item.Value;
				}
				else
				{
					userData_0.dictionary_4.Add(item.Key, item.Value);
				}
				UsersData.Dispatch(UsersData.EventType.SKIN_CHANGED, new UsersData.EventData
				{
					string_0 = item.Key
				});
			}
		}
		if (flag)
		{
			bool_0 = true;
			UsersData.Dispatch(UsersData.EventType.SKINS_CHANGED);
		}
	}

	private void UpdateProfileStat()
	{
		if (userProfileStatData_0 != null && userData_0 != null)
		{
			userData_0.userProfileStatData_0 = userProfileStatData_0;
		}
	}

	private void UpdateUserRankData()
	{
		if (userData_0 == null)
		{
			return;
		}
		if (userRankData_0 != null)
		{
			int num = userData_0.userRankData_0.int_1;
			int num2 = userData_0.userRankData_0.int_0;
			userData_0.userRankData_0 = userRankData_0;
			UsersData.EventData eventData = new UsersData.EventData();
			eventData.int_0 = num;
			eventData.double_0 = num2;
			UsersData.EventData eventData_ = eventData;
			if (num > userRankData_0.int_1)
			{
				UsersData.Dispatch(UsersData.EventType.RANK_LEVEL_UP, eventData_);
			}
			else if (num < userRankData_0.int_1)
			{
				UsersData.Dispatch(UsersData.EventType.RANK_LEVEL_DOWN, eventData_);
			}
			else if (num2 != userRankData_0.int_0)
			{
				UsersData.Dispatch(UsersData.EventType.RANK_MEDALS_CHANGED, eventData_);
			}
			else if (bool_1 && userData_0.user_0.int_7 > 0)
			{
				UsersData.Dispatch(UsersData.EventType.RANK_FIRST_RANK_BATTLE_END);
			}
		}
		else if (bool_1 && userData_0.user_0.int_7 > 0)
		{
			UsersData.Dispatch(UsersData.EventType.RANK_FIRST_RANK_BATTLE_END);
		}
	}

	private void UpdatePlaceSeason()
	{
		if (userData_0 != null && dictionary_6 != null)
		{
			userData_0.dictionary_6 = dictionary_6;
		}
	}

	private void UpdateUserTimers()
	{
		if (dictionary_5 == null || userData_0 == null)
		{
			return;
		}
		bool flag = false;
		if (dictionary_5 != null && dictionary_5.Count > 0)
		{
			flag = true;
			foreach (KeyValuePair<UserTimerData.UserTimerType, List<UserTimerData>> item in dictionary_5)
			{
				List<UserTimerData> value = item.Value;
				foreach (UserTimerData item2 in value)
				{
					UsersData.Dispatch(UsersData.EventType.USER_TIMER_UPDATE, new UsersData.EventData
					{
						int_0 = item2.int_0,
						double_0 = item2.double_0
					});
				}
			}
		}
		userData_0.dictionary_5 = dictionary_5;
		if (flag)
		{
			UsersData.Dispatch(UsersData.EventType.USER_TIMERS_UPDATE);
		}
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(UserDataUpdateNetworkCommand), 101);
	}
}
