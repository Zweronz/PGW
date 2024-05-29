using UnityEngine;
using engine.network.auth;

public sealed class ProcessEnterTest
{
	private static ProcessEnterTest processEnterTest_0;

	private string string_0 = string.Empty;

	public static ProcessEnterTest ProcessEnterTest_0
	{
		get
		{
			return processEnterTest_0 ?? (processEnterTest_0 = new ProcessEnterTest());
		}
	}

	public void ProcessEnter(string string_1)
	{
		if (string.IsNullOrEmpty(string_1))
		{
			Debug.Log("[ProcessEnterTest::ProcessEnter]  Auth key is null or empty!?");
			return;
		}
		string_0 = string_1;
		Debug.Log("[ProcessEnterTest::ProcessEnter]  Auth key: " + string_1);
		AuthManager.AuthManager_0.Subscribe(OnEnterSuccess, AuthManager.Event.EnterSuccess);
		AuthManager.AuthManager_0.Subscribe(OnEnterFail, AuthManager.Event.EnterFail);
		AuthManager.AuthManager_0.ProcessEnter(string_1);
	}

	private void OnEnterFail(AuthEventParams authEventParams_0)
	{
		Debug.Log("[ProcessEnterTest::OnEnterFail] Status code: " + authEventParams_0.int_0);
		AppController.LauncherStartReason launcherStartReason = AppController.LauncherStartReason.ENTER_OPERATION_FAILED;
		switch (authEventParams_0.int_0)
		{
		case 106:
			launcherStartReason = AppController.LauncherStartReason.ERROR_WRONG_VERSION;
			break;
		case 101:
		case 105:
			launcherStartReason = AppController.LauncherStartReason.PROJECT_STOPPED;
			break;
		case 109:
			launcherStartReason = AppController.LauncherStartReason.ERROR_WRONG_HASH0;
			break;
		}
		Debug.Log("[ProcessEnterTest::OnEnterFail] Reason: " + launcherStartReason);
	}

	private void OnEnterSuccess(AuthEventParams authEventParams_0)
	{
		Debug.Log(string.Format("[ProcessEnterTest::OnEnterSuccess] Socket interface: {0} Deploy version: {1}", authEventParams_0.string_2, authEventParams_0.string_3));
		ProcessWebSocketConnect.ProcessWebSocketConnect_0.ProcessConnect(authEventParams_0.string_2, string_0);
	}
}
