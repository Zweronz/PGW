using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public class UseArtikulStatNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public string string_0;

	[ProtoMember(3)]
	public string string_1;

	[ProtoMember(4)]
	public string string_2;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine("UseArtikulStatNetworkCommand::Answered > OK, Code = " + abstractNetworkCommand_0.NetworkCommandInfo_0.int_0);
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine("UseArtikulStatNetworkCommand::Answered > Error, Code = " + abstractNetworkCommand_0.NetworkCommandInfo_0.int_0);
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(UseArtikulStatNetworkCommand), 108);
	}
}
