using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public class EquipArtikulNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public int int_1;

	[ProtoMember(3)]
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
		Log.AddLine(string.Format("EquipArtikulNetworkCommand::HandleError > Error! Code = {0}, Message = {1} ", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0));
	}

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine("EquipArtikulNetworkCommand::Answered > OK");
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(EquipArtikulNetworkCommand), 115);
	}
}