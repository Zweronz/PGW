using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public class DeviceStatNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public string string_0;

	[ProtoMember(3)]
	public string string_1;

	[ProtoMember(4)]
	public int int_1;

	[ProtoMember(5)]
	public string string_2;

	[ProtoMember(6)]
	public string string_3;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine("DeviceStatNetworkCommand::Answered > OK");
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(DeviceStatNetworkCommand), 109);
	}
}
