using ProtoBuf;
using engine.helpers;
using engine.network;
using engine.unity;

[ProtoContract]
public class FightPingNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public string string_0;

	[ProtoMember(3)]
	public double double_0;

	[ProtoMember(4)]
	public int int_1;

	[ProtoMember(5)]
	public int int_2;

	[ProtoMember(6)]
	public int int_3;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("FightPingNetworkCommand::HandleError > Error! Code = {0}, Message = {1} ", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0));
	}

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		FightPingNetworkCommand fightPingNetworkCommand = abstractNetworkCommand_0 as FightPingNetworkCommand;
		if (fightPingNetworkCommand != null)
		{
			MonoSingleton<FightController>.Prop_0.OnSyncFightGlobalParameters(fightPingNetworkCommand.string_0, fightPingNetworkCommand.double_0, fightPingNetworkCommand.int_2, fightPingNetworkCommand.int_3);
		}
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(FightPingNetworkCommand), 124);
	}
}
