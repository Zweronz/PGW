using UnityEngine;
using engine.events;
using engine.unity;
using pixelgun.tutorial;
using System.Collections.Generic;

[ScreenParams(GameScreenType.FightScreen)]
public class FightScreen : BaseGameScreen
{
	private static FightScreen fightScreen_0;

	public static FightScreen FightScreen_0
	{
		get
		{
			if (fightScreen_0 == null)
			{
				fightScreen_0 = new FightScreen();
			}
			return fightScreen_0;
		}
	}

	public void Show()
	{
		if (!base.Boolean_0)
		{
			ScreenController.ScreenController_0.ShowScreen(fightScreen_0);
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

	public void ShowLoadingWindow()
	{
		LoadingWindow.Show(new LoadingWindowParams(MonoSingleton<FightController>.Prop_0.ModeData_0, MonoSingleton<FightController>.Prop_0.ModeData_0.Boolean_0));
	}

	private static List<MapData> mapData
	{
		get
		{
			if (_mapData == null)
			{
				List<MapData> data = new List<MapData>();
				Maps maps = Resources.Load<Maps>("Maps");

				foreach (Maps.Map map in maps.maps)
				{
					data.Add(map.ToMapData());
				}

				_mapData = data;
			}

			return _mapData;
		}
	}

	private static List<MapData> _mapData;

	public void SwitchToBattle()
	{
		MapData objectByKey = mapData.Find(x => x.Int32_0 == MonoSingleton<FightController>.Prop_0.ModeData_0.Int32_1);//MapStorage.Get.Storage.GetObjectByKey(MonoSingleton<FightController>.Prop_0.ModeData_0.Int32_1);
		Application.LoadLevelAsync(objectByKey.String_0);
	}

	public void SwitchToMenu(bool bool_0 = false)
	{
		if (LobbyScreen.LobbyScreen_0.Boolean_0)
		{
			return;
		}
		LobbyScreen.LobbyScreen_0.Show();
		if (LoadingWindow.LoadingWindow_0 == null)
		{
			LoadingWindow.Show(new LoadingWindowParams(Defs.String_11));
		}
		else
		{
			LoadingWindowParams loadingWindowParams = (LoadingWindowParams)LoadingWindow.LoadingWindow_0.WindowShowParameters_0;
			if (loadingWindowParams != null && !loadingWindowParams.bool_0)
			{
				loadingWindowParams.bool_0 = true;
			}
		}
		if (bool_0 && !TutorialController.TutorialController_0.Boolean_0 && !RankController.RankController_0.Boolean_1)
		{
			SelectMapWindowParams selectMapWindowParams = new SelectMapWindowParams();
			selectMapWindowParams.gameEvent_0 = WindowController.GameEvent.IN_MAIN_MENU;
			SelectMapWindow.Show(selectMapWindowParams);
		}
		Application.LoadLevelAsync(Defs.String_11);
	}

	public override void Release()
	{
		DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Update);
		WindowController.WindowController_0.Unsubscribe(OnSetPauseGame, WindowController.EventType.SHOW_FIRST_WINDOW);
		WindowController.WindowController_0.Unsubscribe(OnResetPauseGame, WindowController.EventType.HIDE_LAST_WINDOW);
	}

	private void Update()
	{
		if (InputManager.GetButtonUp("ShowBugReport") && !KillCamWindow.KillCamWindow_0)
		{
			if (!WeaponManager.weaponManager_0.myPlayerMoveC.PlayerGrenadeController_0.Boolean_0)
			{
				WeaponManager.weaponManager_0.myPlayerMoveC.PlayerGrenadeController_0.GrenadeFire();
			}
			BugReportController.BugReportController_0.SwitchWindow();
		}
	}

	private void OnSetPauseGame()
	{
		if (!(WindowController.WindowController_0.BaseWindow_0 != null) || !WindowController.WindowController_0.BaseWindow_0.Parameters_0.bool_7)
		{
			SetPauseGame(true);
		}
	}

	private void OnResetPauseGame()
	{
		SetPauseGame(false);
	}

	private void SetPauseGame(bool bool_0)
	{
		if (Player_move_c.Boolean_0 != bool_0 && HeadUpDisplay.HeadUpDisplay_0 != null && (!(PauseBattleWindow.PauseBattleWindow_0 != null) || !bool_0) && HeadUpDisplay.GetPlayerMoveC() != null)
		{
			HeadUpDisplay.GetPlayerMoveC().SetPause(false);
		}
	}
}
