using System.Diagnostics;
using engine.unity;

public sealed class FPDefaultActionProcess : FPBaseProcess
{
	public override void Update(FPProcessSharedData fpprocessSharedData_0)
	{
		if (!fpprocessSharedData_0.bool_0)
		{
			Player_move_c player_move_c_ = fpprocessSharedData_0.firstPersonPlayerController_0.Player_move_c_0;
			if (InputManager.GetButtonUp("Reload") && !player_move_c_.Boolean_10)
			{
				player_move_c_.ReloadPressed();
			}
			if (InputManager.GetButtonDown("Attack") && !player_move_c_.Boolean_20 && player_move_c_.Boolean_23 && player_move_c_.PlayerTurretController_0.Boolean_1)
			{
				fpprocessSharedData_0.bool_1 = true;
			}
			if (!InputManager.GetButton("Attack") && fpprocessSharedData_0.bool_1)
			{
				fpprocessSharedData_0.bool_1 = false;
				player_move_c_.PlayerStateController_0.DispatchStopShoot();
			}
			if (fpprocessSharedData_0.bool_1 && fpprocessSharedData_0.firstPersonPlayerController_0.State_0 != FirstPersonPlayerController.State.Hook)
			{
				player_move_c_.ShotPressed();
				player_move_c_.PlayerStateController_0.DispatchStartShoot();
			}
			if (InputManager.GetButtonUp("Zoom") && player_move_c_ != null && WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.WeaponSounds_0 != null && WeaponManager.weaponManager_0.WeaponSounds_0.WeaponData_0.Boolean_15 && (player_move_c_.Boolean_16 || BattleStatWindow.BattleStatWindow_0 == null || !BattleStatWindow.Boolean_1))
			{
				player_move_c_.ZoomPress();
			}
			if (fpprocessSharedData_0.bool_4)
			{
				fpprocessSharedData_0.bool_2 = InputManager.GetButtonDown("Jump");
			}
			fpprocessSharedData_0.bool_3 = InputManager.GetButton("Jump");
		}
	}
}
