using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using WebSocketSharp;
using engine.events;
using engine.network;

public sealed class ClanController : BaseEvent<ClanController.EventData>
{
	public sealed class EventData
	{
		[CompilerGenerated]
		private int int_0;

		[CompilerGenerated]
		private int int_1;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private string string_2;

		[CompilerGenerated]
		private UserClanData userClanData_0;

		[CompilerGenerated]
		private Dictionary<string, UserClanData> dictionary_0;

		public int Int32_0
		{
			[CompilerGenerated]
			get
			{
				return int_0;
			}
			[CompilerGenerated]
			set
			{
				int_0 = value;
			}
		}

		public int Int32_1
		{
			[CompilerGenerated]
			get
			{
				return int_1;
			}
			[CompilerGenerated]
			set
			{
				int_1 = value;
			}
		}

		public string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			set
			{
				string_0 = value;
			}
		}

		public string String_1
		{
			[CompilerGenerated]
			get
			{
				return string_1;
			}
			[CompilerGenerated]
			set
			{
				string_1 = value;
			}
		}

		public string String_2
		{
			[CompilerGenerated]
			get
			{
				return string_2;
			}
			[CompilerGenerated]
			set
			{
				string_2 = value;
			}
		}

		public UserClanData UserClanData_0
		{
			[CompilerGenerated]
			get
			{
				return userClanData_0;
			}
			[CompilerGenerated]
			set
			{
				userClanData_0 = value;
			}
		}

		public Dictionary<string, UserClanData> Dictionary_0
		{
			[CompilerGenerated]
			get
			{
				return dictionary_0;
			}
			[CompilerGenerated]
			set
			{
				dictionary_0 = value;
			}
		}
	}

	public enum EventType
	{
		CREATE_SUCCESS = 0,
		CREATE_FAILURE = 1,
		CLOSE_SUCCESS = 2,
		CLOSE_FAILURE = 3,
		EXIT_SUCCESS = 4,
		EXIT_FAILURE = 5,
		GET_CLAN_SUCCESS = 6,
		GET_CLAN_FAILURE = 7,
		GET_TOP_SUCCESS = 8,
		GET_TOP_FAILURE = 9,
		SEARCH_SUCCESS = 10,
		SEARCH_FAILURE = 11,
		UPDATE_CLAN_MESSAGES = 12,
		UPDATE_CLAN = 13,
		GET_CLAN_BY_USER_SUCCESS = 14
	}

	private static ClanController clanController_0;

	public List<ClanMessageData> list_1 = new List<ClanMessageData>();

	private List<UserClanData> list_2 = new List<UserClanData>();

	private float float_0;

	private float float_1 = 60f;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private double double_0;

	[CompilerGenerated]
	private static Comparison<UserClanData> comparison_0;

	public static ClanController ClanController_0
	{
		get
		{
			if (clanController_0 == null)
			{
				clanController_0 = new ClanController();
			}
			return clanController_0;
		}
	}

	public string String_0
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		private set
		{
			string_0 = value;
		}
	}

	public double Double_0
	{
		[CompilerGenerated]
		get
		{
			return double_0;
		}
		[CompilerGenerated]
		private set
		{
			double_0 = value;
		}
	}

	public bool Boolean_0
	{
		get
		{
			return UsersData.UsersData_0.UserData_0.user_0.int_2 > 2;
		}
	}

	public UserClanData UserClanData_0
	{
		get
		{
			return (UserClansData.UserClansData_0 != null) ? UserClansData.UserClansData_0.UserClanData_0 : null;
		}
	}

	public List<UserClanData> List_1
	{
		get
		{
			return list_2;
		}
	}

	private ClanController()
	{
	}

	public UserClanData GetClan(string string_1)
	{
		if (string_1.IsNullOrEmpty())
		{
			return null;
		}
		return UserClansData.UserClansData_0.userClanStorage_0.GetObjectByKey(string_1);
	}

	public ClanLevelData GetClanLevelData(string string_1)
	{
		UserClanData clan = GetClan(string_1);
		if (clan == null)
		{
			return null;
		}
		return GetClanLevelDataByLevel(clan.int_2);
	}

	public ClanLevelData GetClanLevelDataByLevel(int int_0)
	{
		return ClanLevelStorage.Get.Storage.SearchUnique(0, int_0);
	}

	public int GetClanMembersMaxByLevel(int int_0)
	{
		ClanLevelData clanLevelData = ClanLevelStorage.Get.Storage.SearchUnique(0, int_0);
		return (clanLevelData != null) ? clanLevelData.int_2 : 0;
	}

	public bool IsCalnInTop(string string_1)
	{
		UserClanData userClanData = list_2.Find((UserClanData userClanData_0) => userClanData_0.string_0.Equals(string_1));
		return userClanData != null;
	}

	public void UpdateClanTop()
	{
		AbstractNetworkCommand.Send<GetUserClanDataNetworkCommand>();
	}

	public void CreateClan(string string_1)
	{
		CreateClanNetworkCommand createClanNetworkCommand = new CreateClanNetworkCommand();
		createClanNetworkCommand.string_0 = string_1;
		AbstractNetworkCommand.Send(createClanNetworkCommand);
	}

	public void CloseClan()
	{
		AbstractNetworkCommand.Send<CloseClanNetworkCommand>();
		GetClanMessageList();
	}

	public void ExitClan()
	{
		UserClanData userClanData_ = ClanController_0.UserClanData_0;
		if (userClanData_ != null)
		{
			ClanController_0.SendClanMessage(6, string.Empty, 0, userClanData_.string_0);
			GetClanMessageList();
		}
	}

	public void SearchClan(string string_1)
	{
		GetUserClanDataNetworkCommand getUserClanDataNetworkCommand = new GetUserClanDataNetworkCommand();
		getUserClanDataNetworkCommand.string_1 = string_1;
		GetUserClanDataNetworkCommand gparam_ = getUserClanDataNetworkCommand;
		AbstractNetworkCommand.Send(gparam_);
	}

	public void ClanMessageDelete(string string_1)
	{
		ClanMessageStatusNetworkCommand clanMessageStatusNetworkCommand = new ClanMessageStatusNetworkCommand();
		clanMessageStatusNetworkCommand.string_0 = string_1;
		clanMessageStatusNetworkCommand.int_1 = 3;
		AbstractNetworkCommand.Send(clanMessageStatusNetworkCommand);
		for (int i = 0; i < list_1.Count; i++)
		{
			if (list_1[i].string_0.Equals(string_1))
			{
				list_1.Remove(list_1[i]);
				break;
			}
		}
		ClanController_0.UpdateClanMessageList();
	}

	public void ClanMessageSeen(string string_1)
	{
		for (int i = 0; i < list_1.Count; i++)
		{
			if (list_1[i].string_0 == string_1)
			{
				list_1[i].int_20 = 1;
			}
		}
		ClanMessageStatusNetworkCommand clanMessageStatusNetworkCommand = new ClanMessageStatusNetworkCommand();
		clanMessageStatusNetworkCommand.string_0 = string_1;
		clanMessageStatusNetworkCommand.int_1 = 1;
		AbstractNetworkCommand.Send(clanMessageStatusNetworkCommand);
		ClanController_0.UpdateClanMessageList();
	}

	public void SendClanMessage(int int_0, string string_1 = "", int int_1 = 0, string string_2 = "")
	{
		SendClanMessageNetworkCommand sendClanMessageNetworkCommand = new SendClanMessageNetworkCommand();
		sendClanMessageNetworkCommand.string_1 = string_1;
		sendClanMessageNetworkCommand.int_2 = int_1;
		sendClanMessageNetworkCommand.string_0 = string_2;
		sendClanMessageNetworkCommand.int_1 = int_0;
		AbstractNetworkCommand.Send(sendClanMessageNetworkCommand);
	}

	public void GetClanMessageList()
	{
		ClanMessageListNetworkCommand gparam_ = new ClanMessageListNetworkCommand();
		AbstractNetworkCommand.Send(gparam_);
	}

	public string GetClanTitleByUser(int int_0)
	{
		for (int i = 0; i < list_2.Count; i++)
		{
			UserClanData userClanData = list_2[i];
			for (int j = 0; j < userClanData.list_0.Count; j++)
			{
				UserClanMemberData userClanMemberData = userClanData.list_0[j];
				if (userClanMemberData.int_0 == int_0)
				{
					return userClanData.string_2;
				}
			}
		}
		return string.Empty;
	}

	public UserClanData GetClanDataByUser(int int_0)
	{
		for (int i = 0; i < list_2.Count; i++)
		{
			UserClanData userClanData = list_2[i];
			for (int j = 0; j < userClanData.list_0.Count; j++)
			{
				UserClanMemberData userClanMemberData = userClanData.list_0[j];
				if (userClanMemberData.int_0 == int_0)
				{
					return userClanData;
				}
			}
		}
		return null;
	}

	public void OnUpdateClanTopSuccess(string string_1, double double_1)
	{
		list_2 = UserClansData.UserClansData_0.userClanStorage_0.GetObjects();
		SortTop();
		String_0 = string_1;
		Double_0 = double_1;
		Dispatch(null, EventType.GET_TOP_SUCCESS);
	}

	public void OnSearchSuccess(Dictionary<string, UserClanData> dictionary_1)
	{
		EventData eventData = new EventData();
		eventData.Dictionary_0 = dictionary_1;
		EventData gparam_ = eventData;
		Dispatch(gparam_, EventType.SEARCH_SUCCESS);
	}

	public void OnSearchByUserSuccess(Dictionary<string, UserClanData> dictionary_1)
	{
		EventData eventData = new EventData();
		eventData.Dictionary_0 = dictionary_1;
		EventData gparam_ = eventData;
		Dispatch(gparam_, EventType.GET_CLAN_BY_USER_SUCCESS);
	}

	public void OnCreatedClanSuccess(UserClanData userClanData_0)
	{
		ClanController_0.GetClanMessageList();
		EventData eventData = new EventData();
		eventData.UserClanData_0 = userClanData_0;
		EventData gparam_ = eventData;
		Dispatch(gparam_, EventType.CREATE_SUCCESS);
	}

	public void OnCreatedClanFailure(int int_0)
	{
		EventData eventData = new EventData();
		eventData.Int32_0 = int_0;
		EventData gparam_ = eventData;
		Dispatch(gparam_, EventType.CREATE_FAILURE);
	}

	public void OnClosedClanSuccess()
	{
		UpdateClanTop();
		Dispatch(null, EventType.CLOSE_SUCCESS);
	}

	public void OnClosedClanFailure(int int_0)
	{
		EventData eventData = new EventData();
		eventData.Int32_0 = int_0;
		EventData gparam_ = eventData;
		Dispatch(gparam_, EventType.CLOSE_FAILURE);
		if (int_0 == 18)
		{
			MessageWindow.Show(new MessageWindowParams(Localizer.Get("window.clan_info.warn.have_no_clan")));
		}
	}

	public void OnExitClanSuccess()
	{
		UpdateClanTop();
		Dispatch(null, EventType.EXIT_SUCCESS);
	}

	public void OnExitClanFailure(int int_0)
	{
		EventData eventData = new EventData();
		eventData.Int32_0 = int_0;
		EventData gparam_ = eventData;
		Dispatch(gparam_, EventType.EXIT_FAILURE);
	}

	public void UpdateClanMessageList()
	{
		Dispatch(null, EventType.UPDATE_CLAN_MESSAGES);
	}

	public void OnUpdateClan()
	{
		Dispatch(null, EventType.UPDATE_CLAN);
	}

	public void Init()
	{
		if (!DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateOneSec))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(UpdateOneSec);
		}
		UpdateClanTop();
		float_0 = 0f;
		UpdateOneSec();
	}

	private void UpdateOneSec()
	{
		float_0 -= 1f;
		if (float_0 <= 0f)
		{
			GetClanMessageList();
			float_0 = float_1;
		}
	}

	private void SortTop()
	{
		list_2.Sort((UserClanData userClanData_0, UserClanData userClanData_1) => userClanData_0.int_4.CompareTo(userClanData_1.int_4));
	}

	public int GetNewClanMessagesCount()
	{
		int num = 0;
		for (int i = 0; i < list_1.Count; i++)
		{
			if (list_1[i].int_20 == 0 && list_1[i].int_16 != 1)
			{
				num++;
			}
		}
		return num;
	}
}
