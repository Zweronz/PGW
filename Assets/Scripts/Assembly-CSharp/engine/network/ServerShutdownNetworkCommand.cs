using ProtoBuf;
using engine.helpers;
using engine.unity;

namespace engine.network
{
	[ProtoContract]
	public sealed class ServerShutdownNetworkCommand : AbstractNetworkCommand
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
			NetworkCommands.Register(typeof(ServerShutdownNetworkCommand), 3);
		}

		public override void Run()
		{
			WindowController.WindowController_0.ForceHideAllWindow();
			Log.AddLine("Server shutdown! Closing connection.", Log.LogLevel.WARNING);
			BaseConnection.BaseConnection_0.CloseConnect();
			AppController.AppController_0.NeedStartLauncher(AppController.LauncherStartReason.PROJECT_STOPPED);
		}
	}
}
