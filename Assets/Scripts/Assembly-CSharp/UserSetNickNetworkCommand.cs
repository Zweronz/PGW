using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class UserSetNickNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public string string_0;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine("UserSetNickNetworkCommand::Answered > OK");
		UserNickController.UserNickController_0.Dispatch(null, UserNickController.Event.USER_SET_NICK_SUCCESS);
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		int num = abstractNetworkCommand_0.NetworkCommandInfo_0.int_0;
		UserNickEventParams userNickEventParams = new UserNickEventParams();
		userNickEventParams.int_0 = num;
		UserNickEventParams gparam_ = userNickEventParams;
		UserNickController.UserNickController_0.Dispatch(gparam_, UserNickController.Event.USER_SET_NICK_FAILED);
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(UserSetNickNetworkCommand), 104);
	}
}
