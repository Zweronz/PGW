using DG.Tweening;
using engine.controllers;
using engine.unity;

public class Main : MainBase
{
	public override void Start()
	{
		base.Start();
		DOTween.Clear(true);
		DOTween.Init(true, true, LogBehaviour.ErrorsOnly);
		LobbyScreen.LobbyScreen_0.Show();
		InputManager.Init(SharedSettings.SharedSettings_0);
		GraphicsController.GraphicsController_0.Init(SharedSettings.SharedSettings_0);
		ChatController.ChatController_0.Init(SharedSettings.SharedSettings_0);
		IntegrationsManager.IntegrationsManager_0.Init();
		AntiCheatController.AntiCheatController_0.Init();
		BankController.BankController_0.Init();
		RankController.RankController_0.Init(SharedSettings.SharedSettings_0);
		SettingsController.CheckKeyboardConflict();
		AppStateController.AppStateController_0.SetState(AppStateController.States.GAME_LOADED);
		//GameStatHelper.GameStatHelper_0.DeviceStat();
		MonoSingleton<FightController>.Prop_0.Connect();
	}
}
