using System.Collections.Generic;
using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public class LikesAndComplaintsCommand : AbstractNetworkCommand
{
	public const int int_1 = 1;

	public const int int_2 = 2;

	public const int int_3 = 4;

	public const int int_4 = 8;

	public const int int_5 = 16;

	public const int int_6 = 32;

	public const int int_7 = 64;

	public const int int_8 = 128;

	public const int int_9 = 256;

	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public Dictionary<int, int> dictionary_0;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("LikesAndComplaintsCommand::HandleError > Error! Code = {0}, Message = {1} ", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0));
	}

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine("LikesAndComplaintsCommand::Answered > OK. Code = " + abstractNetworkCommand_0.NetworkCommandInfo_0.int_0);
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(LikesAndComplaintsCommand), 122);
	}
}
