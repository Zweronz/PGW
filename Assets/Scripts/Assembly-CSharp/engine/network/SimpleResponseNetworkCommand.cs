using ProtoBuf;

namespace engine.network
{
	[ProtoContract]
	public sealed class SimpleResponseNetworkCommand : AbstractNetworkCommand
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
			NetworkCommands.Register(typeof(SimpleResponseNetworkCommand), 1);
		}
	}
}
