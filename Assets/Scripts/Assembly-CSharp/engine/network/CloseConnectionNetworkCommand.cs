using ProtoBuf;
using UnityEngine;
using engine.helpers;
using engine.unity;

namespace engine.network
{
	[ProtoContract]
	public sealed class CloseConnectionNetworkCommand : AbstractNetworkCommand
	{
		private enum Reason
		{
			GAME_SESSION_ALREADY_RUNNING = 1,
			USER_BANNED = 2
		}

		[ProtoMember(1)]
		public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

		[ProtoMember(2)]
		public int int_1;

		[ProtoMember(3)]
		public string string_0;

		public override NetworkCommandInfo NetworkCommandInfo_0
		{
			get
			{
				return networkCommandInfo_0;
			}
		}

		private new static void Init()
		{
			NetworkCommands.Register(typeof(CloseConnectionNetworkCommand), 4);
		}

		public override void Run()
		{
			Log.AddLine("Close connection event! Closing connection.", Log.LogLevel.WARNING);
			WindowController.WindowController_0.ForceHideAllWindow();
			BaseConnection.BaseConnection_0.CloseConnect();
			Screen.lockCursor = false;
			int num = int_1;
			if (num != 2)
			{
				AppController.AppController_0.NeedStartLauncher(AppController.LauncherStartReason.GAME_SESSION_ALREADY_RUNNING);
				return;
			}
			string string_ = ParseArgsForTimeLimitBan();
			AppController.AppController_0.NeedStartLauncher(AppController.LauncherStartReason.USER_BANNED, string_);
		}

		private string ParseArgsForTimeLimitBan()
		{
			string text = "0";
			if (!string.IsNullOrEmpty(string_0))
			{
				string[] array = string_0.Split('\n');
				text = ((array.Length <= 0) ? text : array[0]);
			}
			return text;
		}
	}
}
