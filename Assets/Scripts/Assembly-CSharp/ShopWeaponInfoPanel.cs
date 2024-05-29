public class ShopWeaponInfoPanel : ShopItemInfoPanel
{
	public ShopWeaponParam[] shopWeaponParam_0;

	protected override void InitComponents()
	{
		WeaponData weapon = WeaponController.WeaponController_0.GetWeapon(artikulData_0.Int32_0);
		if (weapon != null)
		{
			shopWeaponParam_0[0].Init("icon1", LocalizationStorage.Get.Term("ui.shop.weapon_dps"), weapon.Single_17, WeaponController.WeaponController_0.GetMaxWeaponSkill("damage"));
			shopWeaponParam_0[1].Init("icon2", LocalizationStorage.Get.Term("ui.shop.weapon_rate"), weapon.Single_19, WeaponController.WeaponController_0.GetMaxWeaponSkill("rate"));
			shopWeaponParam_0[2].Init("icon3", LocalizationStorage.Get.Term("ui.shop.weapon_capacity"), weapon.Int32_2, WeaponController.WeaponController_0.GetMaxWeaponSkill("capacity"));
			shopWeaponParam_0[3].Init("icon4", LocalizationStorage.Get.Term("ui.shop.weapon_mobility"), weapon.Int32_5, WeaponController.WeaponController_0.GetMaxWeaponSkill("mobility"));
		}
	}
}
