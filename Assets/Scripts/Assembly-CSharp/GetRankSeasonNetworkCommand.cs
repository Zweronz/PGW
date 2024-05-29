using ProtoBuf;
using engine.network;

[ProtoContract]
public class GetRankSeasonNetworkCommand : AbstractNetworkCommand
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
		RankController.RankController_0.UpdateRankSeason(rankSeasonData_0);
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(GetRankSeasonNetworkCommand), 130);
	}
}
