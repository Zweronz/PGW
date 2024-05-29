using ProtoBuf;
using UnityEngine;
using engine.helpers;

namespace engine.network
{
	[ProtoContract]
	public sealed class MaintenanceNetworkCommand : AbstractNetworkCommand
	{
		[ProtoMember(1)]
		public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

		[ProtoMember(2, DataFormat = DataFormat.ZigZag)]
		public int int_1;

		[ProtoMember(3, DataFormat = DataFormat.ZigZag)]
		public int int_2;

		public override NetworkCommandInfo NetworkCommandInfo_0
		{
			get
			{
				return networkCommandInfo_0;
			}
		}

		private new static void Init()
		{
			NetworkCommands.Register(typeof(MaintenanceNetworkCommand), 2);
		}

		public override void Run()
		{
			int num = int_1 - (int)Utility.Double_0;
			Log.AddLine(string.Format("Server going to maintenance after {0} seconds!", num), Log.LogLevel.WARNING);
			BaseConnection.BaseConnection_0.Boolean_0 = false;
			Screen.lockCursor = false;
			string format = Localizer.Get("window.msg.project_service_new");
			string localTime = Utility.GetLocalTime(int_1);
			string localTime2 = Utility.GetLocalTime(int_2);
			string localTime3 = Utility.GetLocalTime(int_2, "MM/dd/yy");
			string string_ = string.Format(format, localTime, localTime2, localTime3);
			AppController.AppController_0.NeedStartLauncher(AppController.LauncherStartReason.SERVER_GO_TO_MAINTENENCE, string_, int_1);
		}
	}
}
