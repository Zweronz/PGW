using System.Collections.Generic;
using ProtoBuf;
using engine.controllers;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class ChatMsgReciveNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public List<ChatMessageData> list_0;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public ChatMsgReciveNetworkCommand()
	{
		hashSet_0 = new HashSet<AppStateController.States>
		{
			AppStateController.States.MAIN_MENU,
			AppStateController.States.JOINED_ROOM,
			AppStateController.States.IN_BATTLE,
			AppStateController.States.IN_BATTLE_OVER_WINDOW,
			AppStateController.States.LEAVING_ROOM
		};
	}

	public override void DeferredRun()
	{
		ChatController.ChatController_0.AddMessages(list_0, false);
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("[ChatMsgReciveNetworkCommand::Error! Code:{0}, message:{1}]", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0));
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(ChatMsgReciveNetworkCommand), 112);
	}
}
