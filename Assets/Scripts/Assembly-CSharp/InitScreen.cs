using System.Text;
using UnityEngine;
using engine.controllers;
using engine.data;
using engine.events;
using engine.helpers;
using engine.network;
using engine.network.auth;
using engine.operations;
using engine.unity;

[ScreenParams(GameScreenType.InitScreen)]
public class InitScreen : BaseGameScreen
{
	private static InitScreen initScreen_0;

	private string string_0;

	private string string_1;

	public static InitScreen InitScreen_0
	{
		get
		{
			if (initScreen_0 == null)
			{
				initScreen_0 = new InitScreen();
			}
			return initScreen_0;
		}
	}

	public void Show()
	{
		if (!base.Boolean_0)
		{
			ScreenController.ScreenController_0.ShowScreen(initScreen_0);
		}
	}

	public override void Init()
	{
		base.Init();
		Loader.Loader_0.Show(true);
		ContentGroupController.ContentGroupController_0.Init();
		TooltipController.TooltipController_0.Init();
		NotificationController.NotificationController_0.Init();
		BonusController.BonusController_0.Init();
		ProcessEnter();
	}

	public override void Release()
	{
	}

	public void StartGameLoading()
	{
		AppStateController.AppStateController_0.SetState(AppStateController.States.GAME_LOADING);
		Application.LoadLevel("AppCenter");
	}

	public void LoadInitScreen()
	{
		if (!base.Boolean_0)
		{
			Log.AddLineFormat("[InitScreen::LoadInitScreen] Game Restart!");
			WindowController.WindowController_0.ForceHideAllWindow();
			DependSceneEvent<ReloadGameEvent>.GlobalDispatch();
			Application.LoadLevel("Init");
		}
	}

	private void ProcessEnter()
	{
		Log.AddLine("[InitScreen. Start enter operation...]");
		AppStateController.AppStateController_0.SetState(AppStateController.States.ENTER);
		StartGameLoading();
		//AuthManager.AuthManager_0.Subscribe(OnEnterSuccess, AuthManager.Event.EnterSuccess);
		//AuthManager.AuthManager_0.Subscribe(OnEnterFail, AuthManager.Event.EnterFail);
		//AuthManager.AuthManager_0.ProcessEnter(AppController.AppController_0.ProcessArguments_0.String_0);
	}

	private void OnEnterFail(AuthEventParams authEventParams_0)
	{
		Log.AddLine("[InitScreen. Enter operation failed! Status code]: " + authEventParams_0.int_0, Log.LogLevel.FATAL);
		AppController.LauncherStartReason launcherStartReason_ = AppController.LauncherStartReason.ENTER_OPERATION_FAILED;
		switch (authEventParams_0.int_0)
		{
		case 106:
			launcherStartReason_ = AppController.LauncherStartReason.ERROR_WRONG_VERSION;
			break;
		case 101:
		case 105:
			launcherStartReason_ = AppController.LauncherStartReason.PROJECT_STOPPED;
			break;
		case 109:
			launcherStartReason_ = AppController.LauncherStartReason.ERROR_WRONG_HASH0;
			break;
		}
		AppController.AppController_0.NeedStartLauncher(launcherStartReason_);
		Loader.Loader_0.Hide();
	}

	private void OnEnterSuccess(AuthEventParams authEventParams_0)
	{
		Log.AddLine(string.Format("[InitScreen. Enter success! Socket interface: {0} Deploy version: {1}]", authEventParams_0.string_2, authEventParams_0.string_3));
		string_0 = authEventParams_0.string_3;
		string_1 = authEventParams_0.string_2;
		CheatsChecker.CheatsChecker_0.Dictionary_0 = authEventParams_0.dictionary_0;
		InitDataStorages();
	}

	private void OnConnectComplete(Operation operation_0)
	{
		operation_0.UnsubscribeAll();
		ConnectOperation connectOperation = operation_0 as ConnectOperation;
		if (connectOperation.Boolean_1)
		{
			Log.AddLine("InitScreen. Connect operation failed!", Log.LogLevel.FATAL);
			AppController.AppController_0.NeedStartLauncher(AppController.LauncherStartReason.WEBSOCKET_CONNECTION_CMD_ERROR);
		}
		else
		{
			Log.AddLine("InitScreen. Connect operation success!");
			InitUserDataStorages();
		}
	}

	private void InitDataStorages()
	{
		Log.AddLine("[InitScreen. Init data storages...]");
		StorageManager.StorageManager_0.Subscribe(GetStoragesDataError, StorageManager.StatusEvent.LOADING_ERROR);
		StorageManager.StorageManager_0.Subscribe(GetStoragesDataComplete, StorageManager.StatusEvent.LOADING_COMPLETE);
		StorageManager.StorageManager_0.Init("static/data", "DataScheme", string_0, InitStorages);
	}

	private void GetStoragesDataError()
	{
		StorageManager.StorageManager_0.Unsubscribe(GetStoragesDataError, StorageManager.StatusEvent.LOADING_ERROR);
		StorageManager.StorageManager_0.Unsubscribe(GetStoragesDataComplete, StorageManager.StatusEvent.LOADING_COMPLETE);
		Log.AddLine("[InitScreen. Init data storages error!]");
		AppController.AppController_0.NeedStartLauncher(AppController.LauncherStartReason.ERROR_LOADING_STORAGES_DATA);
		Loader.Loader_0.Hide();
	}

	private void GetStoragesDataComplete()
	{
		StorageManager.StorageManager_0.Unsubscribe(GetStoragesDataError, StorageManager.StatusEvent.LOADING_ERROR);
		StorageManager.StorageManager_0.Unsubscribe(GetStoragesDataComplete, StorageManager.StatusEvent.LOADING_COMPLETE);
		Log.AddLine("[InitScreen. Init data storages complete!]");
		ConnectOperation connectOperation = new ConnectOperation(string_1, AppController.AppController_0.ProcessArguments_0.String_0, BaseConnection.ConnectionType.WEB_SOCKET);
		connectOperation.Subscribe(OnConnectComplete, Operation.StatusEvent.Complete);
		OperationsManager.OperationsManager_0.Add(connectOperation);
	}

	private void InitStorages()
	{
		BaseStorageHelper.Init();
	}

	private void InitUserDataStorages()
	{
		UsersData.Subscribe(UsersData.EventType.INIT_COMPLETE, GetUserDataComplete);
		UsersData.Subscribe(UsersData.EventType.INIT_ERROR, GetUserDataError);
		AbstractNetworkCommand.Send<GetUserDataNetworkCommand>();
	}

	private void GetUserDataError(UsersData.EventData eventData_0)
	{
		UsersData.Unsubscribe(UsersData.EventType.INIT_COMPLETE, GetUserDataComplete);
		UsersData.Unsubscribe(UsersData.EventType.INIT_ERROR, GetUserDataError);
		Log.AddLine("[InitScreen. Init user data error!]");
		AppController.AppController_0.NeedStartLauncher(AppController.LauncherStartReason.ERROR_LOADING_USER_DATA);
		Loader.Loader_0.Hide();
	}

	private void GetUserDataComplete(UsersData.EventData eventData_0)
	{
		UsersData.Unsubscribe(UsersData.EventType.INIT_COMPLETE, GetUserDataComplete);
		UsersData.Unsubscribe(UsersData.EventType.INIT_ERROR, GetUserDataError);
		ClanController.ClanController_0.Init();
		AppStateController.AppStateController_0.SetState(AppStateController.States.DATA_INIT);
		UserData userData_ = UsersData.UsersData_0.UserData_0;
		if (userData_ == null)
		{
			Log.AddLine("USER NOT FOUND");
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("======= USER INFO =======");
		stringBuilder.AppendLine("User ID = " + userData_.user_0.int_0);
		stringBuilder.AppendLine("User Nick = " + userData_.user_0.string_0);
		stringBuilder.AppendLine("User Email = " + userData_.user_0.string_1);
		stringBuilder.AppendLine("User IsAdmin = " + userData_.user_0.bool_0);
		Log.AddLine(stringBuilder.ToString());
		CrashController.CrashController_0.SaveUser(userData_.user_0.int_0, userData_.user_0.string_1);
		StartGameLoading();
	}
}
