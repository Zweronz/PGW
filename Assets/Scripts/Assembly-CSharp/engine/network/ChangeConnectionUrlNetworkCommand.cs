using ProtoBuf;
using engine.helpers;

namespace engine.network
{
	[ProtoContract]
	public sealed class ChangeConnectionUrlNetworkCommand : AbstractNetworkCommand
	{
		[ProtoMember(1)]
		public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

		[ProtoMember(2, IsRequired = true)]
		public string string_0;

		public override NetworkCommandInfo NetworkCommandInfo_0
		{
			get
			{
				return networkCommandInfo_0;
			}
		}

		public override void Run()
		{
			if (!string.IsNullOrEmpty(string_0))
			{
				Log.AddLineWarning("[ChangeConnectionUrlNetworkCommand::Run. Reconnect from url={0} to url={1}]", BaseConnection.BaseConnection_0.String_0, string_0);
				BaseConnection.BaseConnection_0.Connect(string_0);
			}
		}

		private new static void Init()
		{
			NetworkCommands.Register(typeof(ChangeConnectionUrlNetworkCommand), 7);
		}
	}
}
