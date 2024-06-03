using System.Collections.Generic;
using ProtoBuf;
using UnityEngine;

[ProtoContract]
public sealed class UserData
{
	[ProtoMember(1)]
	public User user_0;

	[ProtoMember(2)]
	internal Dictionary<string, UserArtikul> dictionary_0;

	public UserArtikulStorage userArtikulStorage_0 = new UserArtikulStorage();

	[ProtoMember(3)]
	internal Dictionary<string, UserFriend> dictionary_1;

	public UserFriendStorage userFriendStorage_0 = new UserFriendStorage();

	[ProtoMember(4)]
	public Dictionary<SkillId, SkillData> dictionary_2 = new Dictionary<SkillId, SkillData>();

	//USER CATEGORIES
	[ProtoMember(5)]
	public Dictionary<SlotType, int> dictionary_3 = new Dictionary<SlotType, int>()
	{
		{SlotType.SLOT_WEAPON_PRIMARY, 1},
		{SlotType.SLOT_WEAPON_BACKUP, 0}
	};

	[ProtoMember(6)]
	public Dictionary<string, UserTextureSkinData> dictionary_4 = new Dictionary<string, UserTextureSkinData>();

	[ProtoMember(7)]
	public UserProfileStatData userProfileStatData_0;

	[ProtoMember(8)]
	public UserRankData userRankData_0;

	[ProtoMember(9)]
	public Dictionary<UserTimerData.UserTimerType, List<UserTimerData>> dictionary_5;

	[ProtoMember(10)]
	public Dictionary<int, int> dictionary_6 = new Dictionary<int, int>();

	public LocalUserData localUserData_0 = new LocalUserData();

	public LocalShopData localShopData_0 = new LocalShopData();

	private static UserData _cachedLocalUserData;

	public static UserData LocalUserData
	{
		get
		{
			if (_cachedLocalUserData == null)
			{
				_cachedLocalUserData = new UserData();
				_cachedLocalUserData.user_0 = User.LocalUser;
				_cachedLocalUserData.userProfileStatData_0 = new UserProfileStatData();
				_cachedLocalUserData.userProfileStatData_0.dictionary_0 = new Dictionary<ModeType, int>
				{
					{ ModeType.DEATH_MATCH, PlayerPrefs.GetInt("KillsDeathMatch", 0) },
					{ ModeType.TEAM_FIGHT, PlayerPrefs.GetInt("KillsTeamFight", 0) },
					{ ModeType.FLAG_CAPTURE, PlayerPrefs.GetInt("KillsFlagCapture", 0) }
				};
				_cachedLocalUserData.userProfileStatData_0.int_0 = PlayerPrefs.GetInt("KillSeries", 0);
				_cachedLocalUserData.userProfileStatData_0.int_1 = PlayerPrefs.GetInt("Headshots", 0);
				_cachedLocalUserData.userProfileStatData_0.float_0 = PlayerPrefs.GetFloat("KillRate", 0.0f);
				_cachedLocalUserData.userProfileStatData_0.float_1 = PlayerPrefs.GetFloat("Accuracy", 0.0f);

				_cachedLocalUserData.userProfileStatData_0.dictionary_1 = new Dictionary<ModeType, int>
				{
					{ ModeType.DEATH_MATCH, PlayerPrefs.GetInt("WinsDeathMatch", 0) },
					{ ModeType.TEAM_FIGHT, PlayerPrefs.GetInt("WinsTeamFight", 0) },
					{ ModeType.FLAG_CAPTURE, PlayerPrefs.GetInt("WinsFlagCapture", 0) }
				};
				_cachedLocalUserData.userProfileStatData_0.float_2 = PlayerPrefs.GetFloat("WinRate", 0.0f);
				_cachedLocalUserData.userProfileStatData_0.int_2 = PlayerPrefs.GetInt("WeeklyWins", 0);
				_cachedLocalUserData.userProfileStatData_0.int_3 = PlayerPrefs.GetInt("FirstPlaces", 0);
				_cachedLocalUserData.userProfileStatData_0.int_4 = PlayerPrefs.GetInt("FlagCaptures", 0);
			}
			return _cachedLocalUserData;
		}
	}

	internal void Init()
	{
		localUserData_0.Init();
		localShopData_0.Init();
		InitStorages();
	}

	internal void InitStorages()
	{
		userArtikulStorage_0.InitUserStorage(dictionary_0);
		userFriendStorage_0.InitUserStorage(dictionary_1);
		ShopArtikulStorage.Get.InitBestIndex();
	}
}
