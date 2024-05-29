using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public class TutorialStepNetworkCommand : AbstractNetworkCommand
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

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine("TutorialStepNetworkCommand::Answered > OK, Code = " + abstractNetworkCommand_0.NetworkCommandInfo_0.int_0);
		ApplyBonusNetworkCommand applyBonusNetworkCommand = abstractNetworkCommand_0 as ApplyBonusNetworkCommand;
		if (applyBonusNetworkCommand.bonusType_0 == ApplyBonusNetworkCommand.BonusType.TUTORIAL_COMPLETE)
		{
		}
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine("TutorialStepNetworkCommand::Answered > Error, Code = " + abstractNetworkCommand_0.NetworkCommandInfo_0.int_0);
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(TutorialStepNetworkCommand), 121);
	}
}
