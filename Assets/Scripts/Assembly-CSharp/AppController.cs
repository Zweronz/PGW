using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using engine.events;
using engine.helpers;
using engine.launcher;
using engine.network;
using engine.system;

public sealed class AppController : BaseAppController
{
	public enum LauncherStartReason
	{
		NONE = 0,
		AUTH_KEY_NOT_VALID = 1,
		WEBSOCKET_CONNECTION_CMD_ERROR = 2,
		WEBSOCKET_CONNECTION_BROKEN = 3,
		ENTER_OPERATION_FAILED = 4,
		PROJECT_STOPPED = 5,
		ERROR_SERVER_MAINTENANCE = 6,
		ERROR_WRONG_VERSION = 7,
		ERROR_LOADING_STORAGES_DATA = 8,
		ERROR_LOADING_USER_DATA = 9,
		GAME_SESSION_ALREADY_RUNNING = 10,
		USER_BANNED = 11,
		HACK_DETECTED = 12,
		SERVER_GO_TO_MAINTENENCE = 13,
		ERROR_WRONG_HASH0 = 14
	}

	public sealed class ProcessArguments
	{
		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private string string_2;

		[CompilerGenerated]
		private string string_3;

		[CompilerGenerated]
		private string string_4;

		public string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			private set
			{
				string_0 = value;
			}
		}

		public string String_1
		{
			[CompilerGenerated]
			get
			{
				return string_1;
			}
			[CompilerGenerated]
			private set
			{
				string_1 = value;
			}
		}

		public string String_2
		{
			[CompilerGenerated]
			get
			{
				return string_2;
			}
			[CompilerGenerated]
			private set
			{
				string_2 = value;
			}
		}

		public string String_3
		{
			[CompilerGenerated]
			get
			{
				return string_3;
			}
			[CompilerGenerated]
			private set
			{
				string_3 = value;
			}
		}

		public string String_4
		{
			[CompilerGenerated]
			get
			{
				return string_4;
			}
			[CompilerGenerated]
			private set
			{
				string_4 = value;
			}
		}

		public bool Boolean_0
		{
			get
			{
				if (!string.IsNullOrEmpty(String_0))
				{
					return true;
				}
				if (AppController_0.Boolean_0)
				{
					String_0 = "testkey4";
					Log.AddLine("[ProcessArguments. Auth key not valid! In debug mode use hardcoded auth key]");
					return true;
				}
				return false;
			}
		}

		public bool Boolean_1
		{
			get
			{
				return !string.IsNullOrEmpty(String_2) && !string.IsNullOrEmpty(String_3);
			}
		}

		public ProcessArguments()
		{
			String_0 = CommandLineReader.GetCustomArgument("key");
			String_1 = CommandLineReader.GetCustomArgument("email");
			String_2 = CommandLineReader.GetCustomArgument("process");
			String_3 = CommandLineReader.GetCustomArgument("name");
			String_4 = CommandLineReader.GetCustomArgument("version");
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("----------- Auth info ------------ \n");
			stringBuilder.AppendLine(string.Format("Auth key = {0}", String_0));
			stringBuilder.AppendLine(string.Format("Launcher process name = {0}", String_2));
			stringBuilder.AppendLine(string.Format("Launcher file name = {0}", String_3));
			stringBuilder.AppendLine(string.Format("Game version info = {0}", String_4));
			stringBuilder.AppendLine("------------------------------");
			return stringBuilder.ToString();
		}
	}

	private double double_0;

	private string string_0 = string.Empty;

	[CompilerGenerated]
	private ProcessArguments processArguments_0;

	public ProcessArguments ProcessArguments_0
	{
		[CompilerGenerated]
		get
		{
			return processArguments_0;
		}
		[CompilerGenerated]
		private set
		{
			processArguments_0 = value;
		}
	}

	public string String_1
	{
		get
		{
			return "https://admin-pgun.rilisoft.info";
		}
	}

	public string String_2
	{
		get
		{
			return "http://pixelgun3d.com/en/";
		}
	}

	public string String_3
	{
		get
		{
			return string.Format("{0}bank", String_2);
		}
	}

	public bool Boolean_0
	{
		get
		{
			bool flag = false;
			return Application.isEditor || flag;
		}
	}

	public static AppController AppController_0
	{
		get
		{
			if (BaseAppController.BaseAppController_0 == null)
			{
				BaseAppController.BaseAppController_0 = new AppController();
			}
			return BaseAppController.BaseAppController_0 as AppController;
		}
	}

	public override void InitParams()
	{
		base.VersionInfo_0 = new VersionInfo(Assembly.GetExecutingAssembly().GetName().Version);
		Log.AddLine("[AppContriller. Aplication version]: " + base.VersionInfo_0.ToString());
		ProcessArguments_0 = new ProcessArguments();
		serverInfo_0 = new ServerInfo("http://cdn.pixelgun3d.com/", "http://pgun.rilisoft.info/", "login", "register", "enter", "JksHjkl2");
		Log.AddLine(string.Format("Server URL = {0}", serverInfo_0.String_0));
	}

	public override void Init()
	{
		if (!ProcessArguments_0.Boolean_0)
		{
			Log.AddLine("[AppController. Auth key not valid, start launcher!]");
			NeedStartLauncher(LauncherStartReason.AUTH_KEY_NOT_VALID);
			return;
		}
		Log.AddLine(ProcessArguments_0.ToString());
		ConnectionStatusEvent @event = EventManager.EventManager_0.GetEvent<ConnectionStatusEvent>();
		if (!@event.Contains(OnNetworkFailure))
		{
			@event.Subscribe(OnNetworkFailure, BaseConnection.ConnectionStatus.CONNECT_FAILURE);
		}
		ConnectionResponseEvent event2 = EventManager.EventManager_0.GetEvent<ConnectionResponseEvent>();
		if (!event2.Contains(OnNetworkFailure))
		{
			event2.Subscribe(OnNetworkFailure, BaseConnection.ConnectionStatus.ERROR);
		}
	}

	private void OnNetworkFailure(ConnectionStatusEventArg connectionStatusEventArg_0)
	{
		Log.AddLine("[AppController. Network websocket connectopn error, start launcher!]: " + connectionStatusEventArg_0.string_0);
		NeedStartLauncher(LauncherStartReason.WEBSOCKET_CONNECTION_BROKEN);
	}

	public void NeedStartLauncher(LauncherStartReason launcherStartReason_0)
	{
		NeedStartLauncher(launcherStartReason_0, string.Empty);
	}

	public void NeedStartLauncher(LauncherStartReason launcherStartReason_0, string string_1, int int_0 = 0)
	{
		Loader.Loader_0.Hide();
		string text = ParseLocalizationKey(launcherStartReason_0, string_1);
		Action action_ = null;
		if (int_0 == 0)
		{
			action_ = StartLauncher;
		}
		MessageWindow.Show(new MessageWindowParams(Localizer.Get(text), action_));
		if (!DependSceneEvent<MainUpdateOneSecond>.Contains(OnCountDownStartLauncher))
		{
			double_0 = ((int_0 <= 0) ? (Utility.Double_0 + 10.0) : ((double)int_0));
			DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(OnCountDownStartLauncher);
		}
	}

	private string ParseLocalizationKey(LauncherStartReason launcherStartReason_0, string string_1)
	{
		string string_2 = string.Format("[AppController. Need start launcher! Reason]: {0} {1}", launcherStartReason_0.ToString(), string_1);
		Log.AddLine(string_2, Log.LogLevel.WARNING);
		string result = "window.msg.error.launcher";
		switch (launcherStartReason_0)
		{
		case LauncherStartReason.WEBSOCKET_CONNECTION_BROKEN:
			result = "window.msg.error.socket";
			break;
		case LauncherStartReason.ENTER_OPERATION_FAILED:
			result = "window.msg.error.enter";
			break;
		case LauncherStartReason.PROJECT_STOPPED:
			result = "window.msg.error.project_stopped";
			break;
		case LauncherStartReason.ERROR_WRONG_VERSION:
			result = "window.msg.error.new_version";
			break;
		case LauncherStartReason.GAME_SESSION_ALREADY_RUNNING:
			result = "close_connection.event.game_sesseion_already_running";
			break;
		case LauncherStartReason.USER_BANNED:
		{
			int result2 = 0;
			if (int.TryParse(string_1, out result2))
			{
				if (result2 == 0)
				{
					result = "close_connection.event.user_banned_perm";
					break;
				}
				result = Localizer.Get("close_connection.event.user_banned_time_limit");
				string localTime = Utility.GetLocalTime(result2);
				string localTime2 = Utility.GetLocalTime(result2, "MM/dd/yy");
				result = string.Format(result, localTime, localTime2);
			}
			break;
		}
		case LauncherStartReason.ERROR_WRONG_HASH0:
			string_0 += "reload=1,";
			break;
		case LauncherStartReason.HACK_DETECTED:
		case LauncherStartReason.SERVER_GO_TO_MAINTENENCE:
			result = Localizer.Get(string_1);
			break;
		}
		return result;
	}

	private void OnCountDownStartLauncher()
	{
		if (Utility.Double_0 >= double_0)
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(OnCountDownStartLauncher);
			StartLauncher();
		}
	}

	public override void StartLauncher()
	{
		if (ProcessArguments_0.Boolean_1)
		{
			string directoryName = Path.GetDirectoryName(BaseAppController.String_0);
			string text = Path.Combine(string.Format("{0}/../../", directoryName), ProcessArguments_0.String_3);
			string text2 = ((!string.IsNullOrEmpty(string_0)) ? string.Format("-CustomArgs:{0}", string_0) : string_0);
			if (!string.IsNullOrEmpty(text2))
			{
				Utility.KillAllProcess(text);
				Utility.StartApplicationProcess(text, text2);
			}
			else
			{
				Process[] processByName = Utility.GetProcessByName(ProcessArguments_0.String_2);
				if (processByName.Length == 0)
				{
					Utility.StartApplicationProcess(text, text2);
				}
			}
		}
		Process.GetCurrentProcess().Kill();
	}
}
