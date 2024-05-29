using UnityEngine;
using engine.events;

public class TooltipController
{
	private static TooltipController tooltipController_0;

	private TooltipWindow tooltipWindow_0;

	public static TooltipController TooltipController_0
	{
		get
		{
			return tooltipController_0 ?? (tooltipController_0 = new TooltipController());
		}
	}

	public void Init()
	{
		tooltipWindow_0 = TooltipWindow.TooltipWindow_0;
		if (!tooltipWindow_0.gameObject.activeSelf)
		{
			NGUITools.SetActive(tooltipWindow_0.gameObject, true);
		}
		if (!DependSceneEvent<MainUpdate>.Contains(Update))
		{
			DependSceneEvent<MainUpdate>.GlobalSubscribe(Update);
		}
	}

	private void OnDestroy()
	{
		if (!DependSceneEvent<MainUpdate>.Contains(Update))
		{
			DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Update);
		}
	}

	private void Update()
	{
		bool flag = Input.GetKeyUp(KeyCode.Mouse0);
		GameObject gameObject = ((CursorPGW.CursorPGW_0.Func_0 == null) ? UICamera.gameObject_6 : CursorPGW.CursorPGW_0.Func_0());
		if (gameObject != null)
		{
			TooltipInfo component = gameObject.GetComponent<TooltipInfo>();
			if (component != null)
			{
				switch (component.tooltipType)
				{
				case TooltipInfo.TooltipType.TOOLTIP_TYPE_COMPARER:
				{
					tooltipWindow_0.containers[0].GetComponent<TooltipOnlyText>().DeActivateTooltip();
					TooltipWeaponComparer component3 = tooltipWindow_0.containers[(int)component.tooltipType].GetComponent<TooltipWeaponComparer>();
					if (component.weaponID > 0)
					{
						WeaponData weapon = WeaponController.WeaponController_0.GetWeapon(component.weaponID);
						if (weapon == null || weapon.SlotType_0 > SlotType.SLOT_WEAPON_SNIPER)
						{
							flag = true;
							break;
						}
						Weapon weaponFromSlot = WeaponManager.weaponManager_0.GetWeaponFromSlot(weapon.SlotType_0);
						component3.SetWeaponContent(component.weaponID, (weaponFromSlot != null) ? WeaponManager.weaponManager_0.GetWeaponFromSlot(weapon.SlotType_0).WeaponSounds_0.WeaponData_0.Int32_0 : 0);
					}
					else
					{
						if (component.wearID <= 0)
						{
							flag = true;
							break;
						}
						WearData wear = WearController.WearController_0.GetWear(component.wearID);
						if (wear == null || wear.ArtikulData_0.SlotType_0 > SlotType.SLOT_WEAR_BOOTS)
						{
							flag = true;
							break;
						}
						component3.SetWearContent(component.wearID);
					}
					if (flag && component.ClicHandler != null)
					{
						component.ClicHandler.SendMessage("OnClick");
					}
					flag = false;
					break;
				}
				case TooltipInfo.TooltipType.TOOLTIP_TYPE_ONLYTEXT:
				{
					tooltipWindow_0.containers[1].GetComponent<TooltipWeaponComparer>().DeActivateTooltip();
					TooltipOnlyText component2 = tooltipWindow_0.containers[(int)component.tooltipType].GetComponent<TooltipOnlyText>();
					if (!component.isKarl)
					{
						component2.SetContent(component.text);
						if (flag && component.ClicHandler != null)
						{
							component.ClicHandler.SendMessage("OnClick");
						}
						flag = false;
					}
					break;
				}
				}
			}
			flag |= component == null;
		}
		if (flag |= gameObject == null)
		{
			HideAllContainers();
		}
	}

	public void HideAllContainers()
	{
		tooltipWindow_0.containers[0].GetComponent<TooltipOnlyText>().DeActivateTooltip();
		tooltipWindow_0.containers[1].GetComponent<TooltipWeaponComparer>().DeActivateTooltip();
	}
}
