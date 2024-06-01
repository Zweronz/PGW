using System.Collections.Generic;
using ProtoBuf;

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
		{SlotType.SLOT_WEAPON_BACKUP, 100}
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
