using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public class HackDetectedNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public AntiCheatController.CheatType cheatType_0;

	[ProtoMember(3)]
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
		Log.AddLine("HackDetectedNetworkCommand::Answered > OK");
	}

	public override void Run()
	{
		base.Run();
		AppController.AppController_0.NeedStartLauncher(AppController.LauncherStartReason.HACK_DETECTED, LocalizationStore.Get(string_0));
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(HackDetectedNetworkCommand), 150);
	}
}
