using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public class RankSeasonEndNetworkCommandTest : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public int int_1;

	[ProtoMember(3)]
	public UserRankData userRankData_0;

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
		Log.AddLineFormat("[RankSeasonEndNetworkCommandTest::Run. Rank season end complete!] Cup artikul id: {0}", int_1);
	}

	private new static void Init()
	{
	}

	private new static void InitTest()
	{
		NetworkCommands.RegisterTest(typeof(RankSeasonEndNetworkCommandTest), 132);
	}
}
