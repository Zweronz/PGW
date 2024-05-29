using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class GetUserClanDataNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public UserClansData userClansData_0;

	[ProtoMember(3)]
	public string string_0;

	[ProtoMember(4)]
	public int int_1;

	[ProtoMember(5)]
	public string string_1;

	[ProtoMember(6)]
	public string string_2;

	[ProtoMember(7)]
	public int int_2;

	[ProtoMember(8)]
	public int int_3;

	[ProtoMember(9)]
	public string string_3;

	[ProtoMember(10)]
	public double double_0;

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
		if (string.IsNullOrEmpty(string_0) && string.IsNullOrEmpty(string_1) && int_1 == 0 && userClansData_0 != null)
		{
			userClansData_0.Init();
			ClanController.ClanController_0.OnUpdateClanTopSuccess(string_3, double_0);
		}
		else if (string.IsNullOrEmpty(string_1) && string.IsNullOrEmpty(string_0))
		{
			if (int_1 != 0)
			{
				ClanController.ClanController_0.OnSearchByUserSuccess(userClansData_0.dictionary_0);
			}
		}
		else
		{
			ClanController.ClanController_0.OnSearchSuccess(userClansData_0.dictionary_0);
		}
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("[UserClansData. Get user clans data fail!] {0}", (abstractNetworkCommand_0 != null) ? string.Format("error code = {0}, message = {1}", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0) : string.Empty));
		UserClansData.Dispatch(UserClansData.EventType.INIT_ERROR);
		if (abstractNetworkCommand_0.NetworkCommandInfo_0.int_0 == 15)
		{
			Log.AddLine("GetUserClanDataNetworkCommand::HandleError > wrong top: " + string_2);
		}
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(GetUserClanDataNetworkCommand), 300);
	}
}
