using ProtoBuf;

namespace engine.network
{
	[ProtoContract]
	public sealed class SimpleResponseNetworkCommandTest : AbstractNetworkCommand
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

		private new static void Init()
		{
		}

		private new static void InitTest()
		{
			NetworkCommands.RegisterTest(typeof(SimpleResponseNetworkCommandTest), 1);
		}
	}
}
