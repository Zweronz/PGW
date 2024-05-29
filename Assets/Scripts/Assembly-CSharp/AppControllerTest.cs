using System.Reflection;
using UnityEngine;
using engine.launcher;
using engine.network;
using engine.system;

public sealed class AppControllerTest : BaseAppController
{
	public enum Server
	{
		Dev = 0,
		Test = 1,
		Battle = 2
	}

	public enum AppStopReason
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

	private Server server_0;

	private string string_0 = string.Empty;

	public static AppControllerTest AppControllerTest_0
	{
		get
		{
			if (BaseAppController.BaseAppController_0 == null)
			{
				BaseAppController.BaseAppController_0 = new AppControllerTest();
			}
			return BaseAppController.BaseAppController_0 as AppControllerTest;
		}
	}

	public Server Server_0
	{
		get
		{
			return server_0;
		}
		set
		{
			server_0 = value;
			Init();
		}
	}

	public string String_1
	{
		get
		{
			return string_0;
		}
		set
		{
			string_0 = value;
			Init();
		}
	}

	public override void InitParams()
	{
		base.VersionInfo_0 = ((!string.IsNullOrEmpty(string_0)) ? new VersionInfo(String_1) : new VersionInfo(Assembly.GetExecutingAssembly().GetName().Version));
		Debug.Log("[AppControllerTest::InitParams] Aplication version: " + base.VersionInfo_0.ToString());
		switch (server_0)
		{
		default:
			serverInfo_0 = new ServerInfo("http://cdn-test.pixelgun3d.com/", "https://pgun-dev.rilisoft.info/", "login", "register", "enter", "JksHjkl2");
			break;
		case Server.Battle:
			serverInfo_0 = new ServerInfo("http://cdn.pixelgun3d.com/", "http://pgun.rilisoft.info/", "login", "register", "enter", "JksHjkl2");
			break;
		case Server.Test:
			serverInfo_0 = new ServerInfo("http://cdn-test.pixelgun3d.com/", "http://pgun-test.rilisoft.info/", "login", "register", "enter", "JksHjkl2");
			break;
		}
		Debug.Log(string.Format("[AppControllerTest::InitParams] Server URL = {0}", serverInfo_0.String_0));
	}

	public override void Init()
	{
		InitParams();
	}

	public void NeedStopApp(AppStopReason appStopReason_0)
	{
		NeedStopApp(appStopReason_0, string.Empty);
	}

	public void NeedStopApp(AppStopReason appStopReason_0, string string_1)
	{
		Debug.Log("[AppControllerTest::NeedStopApp] Need close app! Reason: " + appStopReason_0);
	}
}
