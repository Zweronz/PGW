using System.Collections.Generic;
using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public class EquippedStatNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public string string_0;

	[ProtoMember(3)]
	public string string_1;

	[ProtoMember(4)]
	public List<EquippedArtikul> list_0 = new List<EquippedArtikul>();

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine("EquippedStatNetworkCommand::Answered > OK");
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(EquippedStatNetworkCommand), 106);
	}
}
