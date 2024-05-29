using System;
using System.Runtime.CompilerServices;
using engine.controllers;
using engine.events;
using engine.operations;
using engine.unity;
using pixelgun.tutorial;
using UnityEngine;

[ScreenParams(GameScreenType.LobbyScreen)]
public class LobbyScreen : BaseGameScreen
{
	private static LobbyScreen lobbyScreen_0;

	[CompilerGenerated]
	private static Action action_0;

	public static LobbyScreen LobbyScreen_0
	{
		get
		{
			if (lobbyScreen_0 == null)
			{
				lobbyScreen_0 = new LobbyScreen();
			}
			return lobbyScreen_0;
		}
	}

	public void Show()
	{
		if (!base.Boolean_0)
		{
			WaitLoadMainMenu();
			ScreenController.ScreenController_0.ShowScreen(lobbyScreen_0);
		}
	}

	public override void Init()
	{
		base.Init();
		WindowController.WindowController_0.HideAllWindow();
		DependSceneEvent<MainUpdate>.GlobalSubscribe(Update);
		WindowController.WindowController_0.Subscribe(OnSetPauseGame, WindowController.EventType.SHOW_FIRST_WINDOW);
		WindowController.WindowController_0.Subscribe(OnResetPauseGame, WindowController.EventType.HIDE_LAST_WINDOW);
	}

	public override void Release()
	{
		DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Update);
		WindowController.WindowController_0.Unsubscribe(OnSetPauseGame, WindowController.EventType.SHOW_FIRST_WINDOW);
		WindowController.WindowController_0.Unsubscribe(OnResetPauseGame, WindowController.EventType.HIDE_LAST_WINDOW);
	}

	private void Update()
	{
		if (InputManager.GetButtonUp("ShowBugReport"))
		{
			BugReportController.BugReportController_0.SwitchWindow();
		}
	}

	private void OnSetPauseGame()
	{
		SetPauseGame(true);
	}

	private void OnResetPauseGame()
	{
		SetPauseGame(false);
	}

	private void SetPauseGame(bool bool_0)
	{
		if (Player_move_c.Boolean_0 != bool_0 && HeadUpDisplay.HeadUpDisplay_0 != null && PauseBattleWindow.PauseBattleWindow_0 != null != bool_0 && HeadUpDisplay.GetPlayerMoveC() != null)
		{
			HeadUpDisplay.GetPlayerMoveC().SetPause(false);
		}
	}

	private void WaitLoadMainMenu()
	{
		SeveralOperations operation_ = new SeveralOperations(new WaitLoadSceneOperation(Defs.String_11), new ActionOperation(delegate
		{
			bool flag = UserNickController.UserNickController_0.CheckUserNick();
			Lobby.Lobby_0.Show();
			Loader.Loader_0.Hide();
			AppStateController.AppStateController_0.SetState(AppStateController.States.MAIN_MENU);
			WindowController.WindowController_0.DispatchEvent(WindowController.GameEvent.IN_MAIN_MENU);
			if (!UsersData.UsersData_0.UserData_0.user_0.bool_1 && flag && !TutorialController.TutorialController_0.Boolean_0)
			{
				TutorialController.TutorialController_0.Start();
			}
		}));
		OperationsManager.OperationsManager_0.Add(operation_);

	}
}
