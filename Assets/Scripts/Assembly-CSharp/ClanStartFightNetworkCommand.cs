using System.Collections.Generic;
using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class ClanStartFightNetworkCommand : AbstractNetworkCommand
{
	public static int int_1 = 1;

	public static int int_2 = 2;

	public static int int_3 = 3;

	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public string string_0;

	[ProtoMember(3)]
	public string string_1;

	[ProtoMember(4)]
	public List<int> list_0;

	[ProtoMember(5)]
	public int int_4;

	[ProtoMember(6)]
	public int int_5;

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
		ClanBattleController.ClanBattleController_0.GetResponseToFight(string_1, string_0, int_4, int_5);
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("[ClanStartFightNetworkCommand   HandleError!] {0}", (abstractNetworkCommand_0 != null) ? string.Format("error code = {0}, message = {1}", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0) : string.Empty));
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(ClanStartFightNetworkCommand), 308);
	}
}
