using System.Collections.Generic;
using ProtoBuf;
using engine.helpers;
using engine.network;
using engine.unity;

[ProtoContract]
public class EndFightNetworkCommand : AbstractNetworkCommand
{
	public enum IsWinState
	{
		None = 0,
		Win = 1,
		Lose = 2,
		Draw = 3
	}

	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public string string_0;

	[ProtoMember(3)]
	public int int_1;

	[ProtoMember(4)]
	public IsWinState isWinState_0;

	[ProtoMember(5)]
	public Dictionary<int, BattleStatData> dictionary_0;

	[ProtoMember(6)]
	public PhotonNetworkStatistics photonNetworkStatistics_0;

	[ProtoMember(7)]
	public int int_2;

	[ProtoMember(8)]
	public GameFPSStatistics gameFPSStatistics_0;

	[ProtoMember(9, IsRequired = true)]
	public bool bool_0;

	[ProtoMember(10)]
	public int int_3;

	[ProtoMember(11)]
	public bool bool_1;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLineError("EndFightNetworkCommand::HandleError > Error! Code = {0}, Message = {1}. Leaving in lobby!", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0);
		MonoSingleton<FightController>.Prop_0.Quit();
	}

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine("EndFightNetworkCommand::Answered > OK. Code = " + abstractNetworkCommand_0.NetworkCommandInfo_0.int_0);
		StartFightNetworkCommand startFightNetworkCommand = abstractNetworkCommand_0 as StartFightNetworkCommand;
		if (startFightNetworkCommand != null)
		{
			MonoSingleton<FightController>.Prop_0.OnFightAllowed(startFightNetworkCommand.string_0);
			MonoSingleton<FightController>.Prop_0.StartFightCommand();
		}
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(EndFightNetworkCommand), 117);
	}
}
