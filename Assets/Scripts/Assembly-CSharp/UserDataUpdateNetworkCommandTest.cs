using System.Collections.Generic;
using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class UserDataUpdateNetworkCommandTest : AbstractNetworkCommand
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
	public Dictionary<int, int> dictionary_6 = new Dictionary<int, int>();

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void Run()
	{
		base.Run();
		Log.AddLine("[UserDataUpdateNetworkCommandTest::Run] Update user data complete!");
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("[UserDataUpdateNetworkCommandTest::HandleError] Update user data fail! {0}", (abstractNetworkCommand_0 != null) ? string.Format("error code = {0}, message = {1}", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0) : string.Empty));
	}

	private new static void Init()
	{
	}

	private new static void InitTest()
	{
		NetworkCommands.RegisterTest(typeof(UserDataUpdateNetworkCommandTest), 101);
	}
}
