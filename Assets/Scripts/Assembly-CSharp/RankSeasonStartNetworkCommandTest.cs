using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public class RankSeasonStartNetworkCommandTest : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public double double_0;

	[ProtoMember(3)]
	public double double_1;

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
		Log.AddLineFormat("[RankSeasonStartNetworkCommandTest::Run. Rank season start complete!] time start: {0}, time end: {1}", double_0, double_1);
	}

	private new static void Init()
	{
	}

	private new static void InitTest()
	{
		NetworkCommands.RegisterTest(typeof(RankSeasonStartNetworkCommandTest), 131);
	}
}
