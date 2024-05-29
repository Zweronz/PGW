using System.Collections.Generic;
using ProtoBuf;
using engine.controllers;
using engine.helpers;
using engine.network;

[ProtoContract]
public class LevelUpNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public int int_1;

	[ProtoMember(3)]
	public int int_2;

	[ProtoMember(4)]
	public int int_3;

	[ProtoMember(5)]
	public int int_4;

	[ProtoMember(6)]
	public bool bool_0;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public LevelUpNetworkCommand()
	{
		hashSet_0 = new HashSet<AppStateController.States>
		{
			AppStateController.States.MAIN_MENU,
			AppStateController.States.JOINED_ROOM,
			AppStateController.States.IN_BATTLE,
			AppStateController.States.IN_BATTLE_OVER_WINDOW,
			AppStateController.States.LEAVING_ROOM
		};
	}

	public override void DeferredRun()
	{
		Log.AddLine(string.Format("[LevelUpNetworkCommand. Level up received!]: level: {0}, exp:{1}, moneyType:{2}, amountMoney:{3}", int_1, int_2, int_3, int_4));
		FightReconnectController.FightReconnectController_0.SetNeedReconnect();
		ShopArtikulController.ShopArtikulController_0.OnLevelUp(int_1);
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(LevelUpNetworkCommand), 113);
	}
}
