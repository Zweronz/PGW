using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public class BankUpdateNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void Run()
	{
		Log.AddLine("BankUpdateNetworkCommand::Run");
		BankController.BankController_0.CleanBankPositions();
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(BankUpdateNetworkCommand), 6);
	}
}
