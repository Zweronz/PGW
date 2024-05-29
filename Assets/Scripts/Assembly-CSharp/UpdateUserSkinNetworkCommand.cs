using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public class UpdateUserSkinNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public UserTextureSkinData userTextureSkinData_0;

	[ProtoMember(3)]
	public bool bool_0;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine("UpdateUserSkinNetworkCommand::Answered > OK, Code = " + abstractNetworkCommand_0.NetworkCommandInfo_0.int_0);
		SkinEditorController.Dispatch(SkinEditorController.EventType.SAVE_OK);
		SkinEditController.SkinEditController_0.Dispatch(0, SkinEditController.SkinEditorEvent.SAVE_OK);
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine("UpdateUserSkinNetworkCommand::Answered > Error, Code = " + abstractNetworkCommand_0.NetworkCommandInfo_0.int_0);
		SkinEditorController.Dispatch(SkinEditorController.EventType.SAVE_ERROR);
		SkinEditController.SkinEditController_0.Dispatch(0, SkinEditController.SkinEditorEvent.SAVE_ERROR);
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(UpdateUserSkinNetworkCommand), 116);
	}
}
