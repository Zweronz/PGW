using ProtoBuf;
using engine.helpers;
using engine.network;
using engine.unity;

[ProtoContract]
public class StartFightNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public int int_1;

	[ProtoMember(3)]
	public string string_0;

	[ProtoMember(4)]
	public int int_2;

	[ProtoMember(5)]
	public int int_3;

	[ProtoMember(6)]
	public string string_1;

	[ProtoMember(7)]
	public int int_4;

	[ProtoMember(8)]
	public bool bool_0;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("StartFightNetworkCommand::HandleError > Error! Code = {0}, Message = {1} ", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0));
		MonoSingleton<FightController>.Prop_0.OnFightDisallowed();
	}

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine("StartFightNetworkCommand::Answered > OK. Code = " + abstractNetworkCommand_0.NetworkCommandInfo_0.int_0);
		FightController.ConnectionStatus connectionStatus_ = MonoSingleton<FightController>.Prop_0.ConnectionStatus_0;
		if (MonoSingleton<FightController>.Prop_0.ConnectionStatus_0 != FightController.ConnectionStatus.Exiting && MonoSingleton<FightController>.Prop_0.ConnectionStatus_0 != FightController.ConnectionStatus.InLobby)
		{
			StartFightNetworkCommand startFightNetworkCommand = abstractNetworkCommand_0 as StartFightNetworkCommand;
			if (startFightNetworkCommand != null)
			{
				MonoSingleton<FightController>.Prop_0.OnFightAllowed(startFightNetworkCommand.string_0);
				FightOfflineController.FightOfflineController_0.SetArenaTop(startFightNetworkCommand.int_3, startFightNetworkCommand.string_1, startFightNetworkCommand.int_4);
			}
			else
			{
				MonoSingleton<FightController>.Prop_0.OnFightAllowed(string_0);
			}
		}
		else
		{
			Log.AddLineWarning("[StartFightNetworkCommand::Answered] do not work, because FightController.Get.Status = {0}", MonoSingleton<FightController>.Prop_0.ConnectionStatus_0.ToString());
		}
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(StartFightNetworkCommand), 120);
	}
}
