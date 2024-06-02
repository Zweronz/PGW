using System.Reflection;
using ProtoBuf;

[ProtoContract]
[Obfuscation(Exclude = true)]
public class MatchMakingSettings
{
	[ProtoMember(1)]
	public int CountTryJointRandomRoom;

	[ProtoMember(2)]
	public int PlayerLimitInClanBattle;

	[ProtoMember(3)]
	public bool ShowRankBattleInMapList;

	[ProtoMember(4)]
	public int TimeBlockToEnterBattle;

	[ProtoMember(5)]
	public bool IsSummPlayersCountForMapMode;

	static MatchMakingSettings()
	{
		Get = new MatchMakingSettings
		{
			CountTryJointRandomRoom = 1,
			PlayerLimitInClanBattle = 1,
			ShowRankBattleInMapList = true,
			TimeBlockToEnterBattle = 1,
			IsSummPlayersCountForMapMode = true
		};
	}

	public static MatchMakingSettings Get { get; private set; }
}
