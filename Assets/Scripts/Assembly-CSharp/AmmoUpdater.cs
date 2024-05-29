using UnityEngine;

internal sealed class AmmoUpdater : MonoBehaviour
{
	private UILabel uilabel_0;

	public GameObject ammoSprite;

	private void Start()
	{
		uilabel_0 = GetComponent<UILabel>();
	}

	private void Update()
	{
		WeaponManager weaponManager_ = WeaponManager.weaponManager_0;
		if (weaponManager_ == null || uilabel_0 == null)
		{
			return;
		}
		Weapon weaponFromCurrentSlot = weaponManager_.GetWeaponFromCurrentSlot();
		if (weaponFromCurrentSlot != null && (!weaponFromCurrentSlot.WeaponSounds_0.WeaponData_0.Boolean_2 || weaponFromCurrentSlot.WeaponSounds_0.WeaponData_0.Boolean_3))
		{
			uilabel_0.String_0 = ((!weaponFromCurrentSlot.WeaponSounds_0.WeaponData_0.Boolean_3) ? string.Format("{0}/{1}", weaponFromCurrentSlot.Int32_1, weaponFromCurrentSlot.Int32_0) : (weaponFromCurrentSlot.Int32_1 + weaponFromCurrentSlot.Int32_0).ToString());
			if (ammoSprite != null && !ammoSprite.activeSelf)
			{
				ammoSprite.SetActive(true);
			}
		}
		else
		{
			uilabel_0.String_0 = string.Empty;
			if (ammoSprite != null && ammoSprite.activeSelf)
			{
				ammoSprite.SetActive(false);
			}
		}
	}
}
