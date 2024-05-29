using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public class AddMoneyBonusNetworkCommand : AbstractNetworkCommand
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

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine("AddMoneyBonusNetworkCommand::Answered > OK");
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("AddMoneyBonusNetworkCommand::HandleError > Error! Code = {0}, Message = {1} ", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0));
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(AddMoneyBonusNetworkCommand), 200);
	}
}
