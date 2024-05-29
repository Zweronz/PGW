using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class CloseClanNetworkCommand : AbstractNetworkCommand
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
		base.Run();
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("[CloseClanNetworkCommand failed!] {0}", (abstractNetworkCommand_0 != null) ? string.Format("error code = {0}, message = {1}", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0) : string.Empty));
		ClanController.ClanController_0.OnClosedClanFailure(abstractNetworkCommand_0.NetworkCommandInfo_0.int_0);
	}

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		ClanController.ClanController_0.OnClosedClanSuccess();
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(CloseClanNetworkCommand), 302);
	}
}
