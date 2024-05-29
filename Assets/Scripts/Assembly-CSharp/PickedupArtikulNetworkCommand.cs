using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public class PickedupArtikulNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public int int_1;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine("PickedupArtikulNetworkCommand::Answered > OK");
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(PickedupArtikulNetworkCommand), 119);
	}
}
