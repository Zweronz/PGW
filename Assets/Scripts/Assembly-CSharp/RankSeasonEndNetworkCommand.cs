using System.Collections.Generic;
using ProtoBuf;
using engine.controllers;
using engine.network;

[ProtoContract]
public class RankSeasonEndNetworkCommand : AbstractNetworkCommand
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

	public RankSeasonEndNetworkCommand()
	{
		RankController.RankController_0.Boolean_1 = true;
		hashSet_0 = new HashSet<AppStateController.States>
		{
			AppStateController.States.GAME_LOADING,
			AppStateController.States.GAME_LOADED,
			AppStateController.States.MAIN_MENU,
			AppStateController.States.JOINED_ROOM,
			AppStateController.States.IN_BATTLE,
			AppStateController.States.IN_BATTLE_OVER_WINDOW,
			AppStateController.States.LEAVING_ROOM
		};
	}

	public override void DeferredRun()
	{
		base.DeferredRun();
		RankController.RankController_0.OnRankSeasonEnd(int_1, userRankData_0);
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(RankSeasonEndNetworkCommand), 132);
	}
}
