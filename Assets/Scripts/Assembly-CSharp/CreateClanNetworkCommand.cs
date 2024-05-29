using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class CreateClanNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public UserClanData userClanData_0;

	[ProtoMember(3)]
	public string string_0;

	[ProtoMember(4)]
	public bool bool_0;

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
		if (userClanData_0 != null)
		{
			Log.AddLineFormat("CreateClanNetworkCommand::Run > created clan success: {0}, title: {1}", userClanData_0.string_0, userClanData_0.string_2);
		}
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("[CreateClanNetworkCommand failed!] {0}", (abstractNetworkCommand_0 != null) ? string.Format("error code = {0}, message = {1}", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0) : string.Empty));
		ClanController.ClanController_0.OnCreatedClanFailure(abstractNetworkCommand_0.NetworkCommandInfo_0.int_0);
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(CreateClanNetworkCommand), 301);
	}
}
