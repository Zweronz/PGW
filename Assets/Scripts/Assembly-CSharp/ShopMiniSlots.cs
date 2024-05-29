using System.Collections.Generic;
using UnityEngine;

public class ShopMiniSlots : MonoBehaviour
{
	public ShopMiniSlotGear[] gears;

	public ShopMiniSlotWeapon[] weapons;

	public void Init()
	{
		ShopMiniSlotGear[] array = gears;
		foreach (ShopMiniSlotGear shopMiniSlotGear in array)
		{
			shopMiniSlotGear.Setup();
		}
		Dictionary<KeyCode, SlotType> dictionary = new Dictionary<KeyCode, SlotType>();
		List<KeyCode> mapKeycodeSlotType = WeaponController.WeaponController_0.GetMapKeycodeSlotType(dictionary);
		int num = 0;
		ShopMiniSlotWeapon[] array2 = weapons;
		foreach (ShopMiniSlotWeapon shopMiniSlotWeapon in array2)
		{
			shopMiniSlotWeapon.slotType = dictionary[mapKeycodeSlotType[num]];
			shopMiniSlotWeapon.Setup();
			num++;
		}
	}
}
