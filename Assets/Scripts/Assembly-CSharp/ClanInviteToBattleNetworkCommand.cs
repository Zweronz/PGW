using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class ClanInviteToBattleNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public int int_1;

	[ProtoMember(3)]
	public int int_2;

	[ProtoMember(4)]
	public int int_3;

	[ProtoMember(5)]
	public int int_4;

	[ProtoMember(6)]
	public string string_0;

	[ProtoMember(7)]
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
		ClanBattleController.ClanBattleController_0.GetResponseFromUser(int_4, int_2, string_0, int_3, bool_0);
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("[ClanInviteToBattleNetworkCommand   HandleError!] {0}", (abstractNetworkCommand_0 != null) ? string.Format("error code = {0}, message = {1}", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0) : string.Empty));
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(ClanInviteToBattleNetworkCommand), 307);
	}
}
