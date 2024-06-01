using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.events;
using engine.helpers;
using engine.unity;

[ProtoContract]
public sealed class UsersData
{
	public sealed class EventData
	{
		public int int_0;

		public string string_0;

		public double double_0;

		public EventData()
		{
		}

		public EventData(int int_1)
		{
			int_0 = int_1;
		}

		public EventData(string string_1)
		{
			string_0 = string_1;
		}

		public EventData(int int_1, string string_1)
		{
			int_0 = int_1;
			string_0 = string_1;
		}
	}

	public enum EventType
	{
		INIT_COMPLETE = 0,
		INIT_ERROR = 1,
		USER_CHANGED = 2,
		ARTIKUL_CHANGED = 3,
		FRIEND_CHANGED = 4,
		SKILL_CHANGED = 5,
		SLOT_CHANGED = 6,
		SKIN_CHANGED = 7,
		ARTIKULS_CHANGED = 8,
		FRIENDS_CHANGED = 9,
		SKILLS_CHANGED = 10,
		SLOTS_CHANGED = 11,
		SKINS_CHANGED = 12,
		INVENTORY_UPDATE = 13,
		CAMPAIGN_CHANGED = 14,
		RESET_VISIBLE_ARMOR = 15,
		RESET_VISIBLE_HAT = 16,
		RESET_VISIBLE_BOOTS = 17,
		RESET_VISIBLE_CAPE = 18,
		OFFLINE_WAVES_OVERCOME_CHANGED = 19,
		OFFLINE_WAVES_COMPLETE = 20,
		GET_ANY_USER_CMD_COMPLETE = 21,
		GET_ANY_USER_CMD_ERROR = 22,
		RANK_MEDALS_CHANGED = 23,
		RANK_LEVEL_UP = 24,
		RANK_LEVEL_DOWN = 25,
		RANK_FIRST_RANK_BATTLE_END = 26,
		USER_TIMER_UPDATE = 27,
		USER_TIMERS_UPDATE = 28
	}

	[ProtoMember(1)]
	public int int_0;

	[ProtoMember(2)]
	public Dictionary<int, UserData> dictionary_0;

	public UsersStorage usersStorage_0 = new UsersStorage();

	private static readonly BaseEvent<EventData> baseEvent_0 = new BaseEvent<EventData>();

	[CompilerGenerated]
	private static UsersData usersData_0;

	public static UsersData UsersData_0
	{
		[CompilerGenerated]
		get
		{
			if (usersData_0 == null)
			{
				usersData_0 = new UsersData();
				usersData_0.Init();
			}
			return usersData_0;
		}
		[CompilerGenerated]
		private set
		{
			usersData_0 = value;
		}
	}

	public UserData UserData_0
	{
		get
		{
			if (userData == null)
			{
				userData = UserData.LocalUserData;
			}
			
			return userData;
		}
	}

	private UserData userData;

	public static void Dispatch<EventType>(EventType gparam_0, EventData eventData_0 = null)
	{
		baseEvent_0.Dispatch(eventData_0, gparam_0);
	}

	public static void Subscribe<EventType>(EventType gparam_0, Action<EventData> action_0)
	{
		if (!baseEvent_0.Contains(action_0, gparam_0))
		{
			baseEvent_0.Subscribe(action_0, gparam_0);
		}
	}

	public static void Unsubscribe<EventType>(EventType gparam_0, Action<EventData> action_0)
	{
		if (baseEvent_0.Contains(action_0, gparam_0))
		{
			baseEvent_0.Unsubscribe(action_0, gparam_0);
		}
	}

	public void Init()
	{
		UsersData_0 = this;
		usersStorage_0.InitUserStorage(dictionary_0);
		UserData_0.Init();
		baseEvent_0.Dispatch(null, EventType.INIT_COMPLETE);
	}
}
