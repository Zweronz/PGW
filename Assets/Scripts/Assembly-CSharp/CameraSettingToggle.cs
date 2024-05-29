using System;

public class CameraSettingToggle : BaseSettingToggle
{
	protected override bool isOn()
	{
		return Storager.GetInt(Defs.string_2) == 1;
	}

	protected override void onClick()
	{
		if (Storager.GetInt(Defs.string_2) == 1 != base.Boolean_0)
		{
			Storager.SetInt(Defs.string_2, Convert.ToInt32(base.Boolean_0));
			Storager.Save();
			if (WeaponManager.weaponManager_0.myPlayerMoveC != null)
			{
				WeaponManager.weaponManager_0.myPlayerMoveC.PlayerStateController_0.DispatchChangeCameraSN();
			}
		}
	}
}
