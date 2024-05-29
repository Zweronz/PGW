using System.Collections.Generic;
using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class SendClanMessageNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public List<ClanMessageData> list_0 = new List<ClanMessageData>();

	[ProtoMember(3)]
	public int int_1;

	[ProtoMember(4)]
	public int int_2;

	[ProtoMember(5)]
	public string string_0;

	[ProtoMember(6)]
	public string string_1;

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
		Log.AddLine(string.Format("[SendClanMessageNetworkCommand   HandleError!] {0}", (abstractNetworkCommand_0 != null) ? string.Format("error code = {0}, message = {1}", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0) : string.Empty));
		switch (abstractNetworkCommand_0.NetworkCommandInfo_0.int_0)
		{
		case 17:
			Log.AddLine("already in clan");
			MessageWindow.Show(new MessageWindowParams(Localizer.Get("window.clan_info.message_already_in_clan")));
			break;
		case 18:
			Log.AddLine("have no clan");
			break;
		case 19:
			Log.AddLine("clan not found");
			break;
		case 20:
			Log.AddLine("wrong sended mesage type");
			break;
		case 21:
			Log.AddLine("message not found");
			break;
		case 22:
			Log.AddLine("message already sended");
			MessageWindow.Show(new MessageWindowParams(Localizer.Get("window.clan_info.message_to_many_members")));
			break;
		case 23:
			Log.AddLine("clan leader can't change current clan");
			break;
		case 26:
			Log.AddLine("message already sended");
			if (ClanWindow.Boolean_1)
			{
				ClanWindow.ClanWindow_0.ShowRequestSendedLabel();
			}
			break;
		case 24:
		case 25:
			break;
		}
	}

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("[SendClanMessageNetworkCommand   Answered!] {0}", (abstractNetworkCommand_0 != null) ? string.Format("error code = {0}, message = {1}", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0) : string.Empty));
		SendClanMessageNetworkCommand sendClanMessageNetworkCommand = abstractNetworkCommand_0 as SendClanMessageNetworkCommand;
		if (sendClanMessageNetworkCommand != null)
		{
			ClanController.ClanController_0.list_1 = sendClanMessageNetworkCommand.list_0;
			ClanController.ClanController_0.UpdateClanMessageList();
		}
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(SendClanMessageNetworkCommand), 304);
	}
}
