using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class ChatMsgSendNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public ChatMessageData chatMessageData_0;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("[ChatMessageSendNetworkCommand::Error! Code:{0}, message:{1}]", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0));
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(ChatMsgSendNetworkCommand), 111);
	}
}
