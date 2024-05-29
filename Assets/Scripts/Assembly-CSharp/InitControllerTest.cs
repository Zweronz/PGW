using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.network;

public class InitControllerTest : MonoBehaviour
{
	[CompilerGenerated]
	private static InitControllerTest initControllerTest_0;

	public static InitControllerTest InitControllerTest_0
	{
		[CompilerGenerated]
		get
		{
			return initControllerTest_0;
		}
		[CompilerGenerated]
		private set
		{
			initControllerTest_0 = value;
		}
	}

	private void Awake()
	{
		InitControllerTest_0 = this;
	}

	private void Start()
	{
		UnityThreadHelper.EnsureHelper();
		AppControllerTest.AppControllerTest_0.Init();
		ConsoleCommandsRepository consoleCommandsRepository_ = ConsoleCommandsRepository.ConsoleCommandsRepository_0;
		consoleCommandsRepository_.RegisterCommand("set_server", ProcessSetServer);
		consoleCommandsRepository_.RegisterCommand("set_version", ProcessSetVersion);
		consoleCommandsRepository_.RegisterCommand("auth", ProcessAuth);
		consoleCommandsRepository_.RegisterCommand("connect", ProcessConnect);
		consoleCommandsRepository_.RegisterCommand("get_user_data", ProcessGetUserData);
		consoleCommandsRepository_.RegisterCommand("send_data", ProcessSendData);
	}

	private void OnDestroy()
	{
		InitControllerTest_0 = null;
	}

	public void ProcessSetServer(params string[] string_0)
	{
		if (string_0 != null && string_0.Length != 0)
		{
			AppControllerTest.Server server_ = AppControllerTest.Server.Dev;
			if (string_0[0] == AppControllerTest.Server.Test.ToString())
			{
				server_ = AppControllerTest.Server.Test;
			}
			else if (string_0[0] == AppControllerTest.Server.Battle.ToString())
			{
				server_ = AppControllerTest.Server.Battle;
			}
			AppControllerTest.AppControllerTest_0.Server_0 = server_;
		}
	}

	public void ProcessSetVersion(params string[] string_0)
	{
		string string_ = string.Empty;
		if (string_0 != null && string_0.Length != 0)
		{
			string_ = string_0[0];
		}
		AppControllerTest.AppControllerTest_0.String_1 = string_;
	}

	public void ProcessAuth(params string[] string_0)
	{
		string string_ = ((string_0 == null || string_0.Length == 0) ? string.Empty : string_0[0]);
		ProcessEnterTest.ProcessEnterTest_0.ProcessEnter(string_);
	}

	public void ProcessConnect(params string[] string_0)
	{
		string string_ = ((string_0 == null || string_0.Length == 0) ? string.Empty : string_0[0]);
		string string_2 = ((string_0 == null || string_0.Length == 1) ? string.Empty : string_0[1]);
		ProcessWebSocketConnect.ProcessWebSocketConnect_0.ProcessConnect(string_, string_2);
	}

	public void ProcessGetUserData(params string[] string_0)
	{
		AbstractNetworkCommand.Send<GetUserDataNetworkCommandTest>();
	}

	public void ProcessSendData(params string[] string_0)
	{
		string text = ((string_0 == null || string_0.Length == 0) ? string.Empty : string_0[0]);
		byte[] array = new byte[text.Length * 2];
		Buffer.BlockCopy(text.ToCharArray(), 0, array, 0, array.Length);
		BaseConnection.BaseConnection_0.Send(array, new SimpleResponseNetworkCommand());
	}
}
