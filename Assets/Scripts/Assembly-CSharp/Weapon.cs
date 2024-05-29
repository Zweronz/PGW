using Rilisoft;
using UnityEngine;
using engine.helpers;

public class Weapon
{
	private GameObject gameObject_0;

	private WeaponSounds weaponSounds_0;

	private int int_0;

	private SaltedInt saltedInt_0 = new SaltedInt(901269156);

	private SaltedInt saltedInt_1 = new SaltedInt(384354114);

	public GameObject GameObject_0
	{
		get
		{
			return gameObject_0;
		}
	}

	public WeaponSounds WeaponSounds_0
	{
		get
		{
			return weaponSounds_0;
		}
	}

	public int Int32_0
	{
		get
		{
			return saltedInt_0.Int32_0;
		}
		set
		{
			bool flag = saltedInt_0.Int32_0 != value;
			saltedInt_0.Int32_0 = value;
			if (flag && WeaponManager.weaponManager_0 != null && weaponSounds_0 != null && weaponSounds_0.WeaponData_0 != null)
			{
				WeaponManager.weaponManager_0.Dispatch(WeaponManager.EventType.AMMO_IN_BACKPACK_CHANGE, (int)weaponSounds_0.WeaponData_0.SlotType_0);
			}
		}
	}

	public int Int32_1
	{
		get
		{
			return saltedInt_1.Int32_0;
		}
		set
		{
			bool flag = saltedInt_1.Int32_0 != value;
			saltedInt_1.Int32_0 = value;
			if (flag && WeaponManager.weaponManager_0 != null && weaponSounds_0 != null && weaponSounds_0.WeaponData_0 != null)
			{
				WeaponManager.weaponManager_0.Dispatch(WeaponManager.EventType.AMMO_IN_CLIP_CHANGE, (int)weaponSounds_0.WeaponData_0.SlotType_0);
			}
		}
	}

	public Weapon(int int_1, out bool bool_0)
	{
		bool_0 = true;
		gameObject_0 = UserController.UserController_0.GetGameObject(int_1);
		if (gameObject_0 == null)
		{
			Log.AddLine("Weapon::Weapon. Error get prefab for weapon, id = " + int_1, Log.LogLevel.ERROR);
			bool_0 = false;
			return;
		}
		int_0 = int_1;
		weaponSounds_0 = gameObject_0.GetComponent<WeaponSounds>();
		InitModelData();
		ResetAmmo();
	}

	public void ResetAmmo()
	{
		InitModelData();
		Int32_1 = weaponSounds_0.WeaponData_0.Int32_2;
		Int32_0 = weaponSounds_0.Int32_1;
	}

	private void InitModelData()
	{
		if (weaponSounds_0.WeaponData_0 == null || int_0 != weaponSounds_0.WeaponData_0.Int32_0)
		{
			weaponSounds_0.Init(int_0);
		}
	}
}
