using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class GetUserDataNetworkCommandTest : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public UsersData usersData_0;

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
		Log.AddLine("[GetUserDataNetworkCommandTest::Run] Get user data complete!");
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("[GetUserDataNetworkCommandTest::HandleError]. Get user data fail! {0}", (abstractNetworkCommand_0 != null) ? string.Format("error code = {0}, message = {1}", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0) : string.Empty));
	}

	private new static void Init()
	{
	}

	private new static void InitTest()
	{
		NetworkCommands.RegisterTest(typeof(GetUserDataNetworkCommandTest), 100);
	}
}
