using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public class GetRankSeasonNetworkCommandTest : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public RankSeasonData rankSeasonData_0;

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
		Log.AddLine("[GetRankSeasonNetworkCommandTest::Run] Get rank season complete!]");
	}

	private new static void Init()
	{
	}

	private new static void InitTest()
	{
		NetworkCommands.RegisterTest(typeof(GetRankSeasonNetworkCommandTest), 130);
	}
}
