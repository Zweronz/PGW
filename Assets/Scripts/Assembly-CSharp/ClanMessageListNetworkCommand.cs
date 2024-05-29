using System.Collections.Generic;
using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class ClanMessageListNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public List<ClanMessageData> list_0 = new List<ClanMessageData>();

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
		ClanController.ClanController_0.UpdateClanMessageList();
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("[ClanMessageListNetworkCommand  failed!] {0}", (abstractNetworkCommand_0 != null) ? string.Format("error code = {0}, message = {1}", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0) : string.Empty));
	}

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		ClanMessageListNetworkCommand clanMessageListNetworkCommand = abstractNetworkCommand_0 as ClanMessageListNetworkCommand;
		if (clanMessageListNetworkCommand != null)
		{
			ClanController.ClanController_0.list_1 = clanMessageListNetworkCommand.list_0;
			ClanController.ClanController_0.UpdateClanMessageList();
		}
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(ClanMessageListNetworkCommand), 303);
	}
}
