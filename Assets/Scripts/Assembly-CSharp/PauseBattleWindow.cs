using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.PauseBattle)]
public class PauseBattleWindow : BaseGameWindow
{
	private static PauseBattleWindow pauseBattleWindow_0;

	[CompilerGenerated]
	private static Action action_0;

	public static PauseBattleWindow PauseBattleWindow_0
	{
		get
		{
			return pauseBattleWindow_0;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return pauseBattleWindow_0 != null && pauseBattleWindow_0.Boolean_0;
		}
	}

	public static void Show(PauseBattleWindowParams pauseBattleWindowParams_0 = null)
	{
		if (!(pauseBattleWindow_0 != null))
		{
			pauseBattleWindow_0 = BaseWindow.Load("PauseInBattleWindow") as PauseBattleWindow;
			pauseBattleWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			pauseBattleWindow_0.Parameters_0.bool_5 = false;
			pauseBattleWindow_0.Parameters_0.bool_0 = false;
			pauseBattleWindow_0.Parameters_0.bool_6 = false;
			pauseBattleWindow_0.InternalShow(pauseBattleWindowParams_0);
		}
	}

	public override void OnShow()
	{
		base.OnShow();
	}

	public override void OnHide()
	{
		base.OnHide();
		pauseBattleWindow_0 = null;
		PresetGameSettings.PresetGameSettings_0.Save();
	}

	public void HandleQuitButton()
	{
		ButtonClickSound.buttonClickSound_0.PlayClick();
		if (PhotonNetwork.Room_0 == null)
		{
			return;
		}
		if (!WeaponManager.weaponManager_0.myPlayerMoveC.Boolean_4)
		{
			Hide();
			MonoSingleton<FightController>.Prop_0.LeaveRoom();
		}
		else if ((bool)PhotonNetwork.Room_0.Hashtable_0["IsRanked"])
		{
			MessageWindowConfirm.Show(new MessageWindowConfirmParams(LocalizationStorage.Get.Term("ui.fight.exit.text"), delegate
			{
				MonoSingleton<FightController>.Prop_0.LeaveRoom();
			}, LocalizationStorage.Get.Term("ui.fight.exit.yes"), KeyCode.None, null, LocalizationStorage.Get.Term("ui.fight.exit.no")));
		}
		else
		{
			MonoSingleton<FightController>.Prop_0.LeaveRoom();
		}
	}

	public void HandleResumeButton()
	{
		ButtonClickSound.buttonClickSound_0.PlayClick();
		if (HeadUpDisplay.GetPlayerMoveC() != null)
		{
			HeadUpDisplay.GetPlayerMoveC().SetPause();
		}
		else
		{
			Hide();
		}
	}

	public void HandleControlsButton()
	{
		ButtonClickSound.buttonClickSound_0.PlayClick();
		SettingsControlWindowParams settingsControlWindowParams = new SettingsControlWindowParams();
		settingsControlWindowParams.Boolean_0 = false;
		SettingsControlWindow.Show(settingsControlWindowParams);
	}
}
