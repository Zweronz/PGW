using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.helpers;
using engine.unity;

public sealed class WeaponController
{
	private static WeaponController weaponController_0;

	public List<SlotType> list_0 = new List<SlotType>
	{
		SlotType.SLOT_WEAPON_PRIMARY,
		SlotType.SLOT_WEAPON_BACKUP,
		SlotType.SLOT_WEAPON_SPECIAL,
		SlotType.SLOT_WEAPON_PREMIUM,
		SlotType.SLOT_WEAPON_SNIPER,
		SlotType.SLOT_WEAPON_MELEE
	};

	private Dictionary<string, float> dictionary_0 = new Dictionary<string, float>
	{
		{ "damage", 0f },
		{ "rate", 0f },
		{ "capacity", 0f },
		{ "mobility", 0f }
	};

	[CompilerGenerated]
	private static Comparison<KeyCode> comparison_0;

	public static WeaponController WeaponController_0
	{
		get
		{
			if (weaponController_0 == null)
			{
				weaponController_0 = new WeaponController();
			}
			return weaponController_0;
		}
	}

	private WeaponController()
	{
	}

	public float GetMaxWeaponSkill(string string_0)
	{
		if (dictionary_0.ContainsKey(string_0))
		{
			return dictionary_0[string_0];
		}
		return 0f;
	}

	public void checkAddToMax(int int_0)
	{
		WeaponData weapon = GetWeapon(int_0);
		if (weapon != null)
		{
			float single_ = weapon.Single_17;
			if (single_ > dictionary_0["damage"])
			{
				dictionary_0["damage"] = single_;
			}
			float single_2 = weapon.Single_19;
			if (single_2 > dictionary_0["rate"])
			{
				dictionary_0["rate"] = single_2;
			}
			if ((float)weapon.Int32_2 > dictionary_0["capacity"])
			{
				dictionary_0["capacity"] = weapon.Int32_2;
			}
			if ((float)weapon.Int32_5 > dictionary_0["mobility"])
			{
				dictionary_0["mobility"] = weapon.Int32_5;
			}
		}
	}

	public List<UserArtikul> GetAllWeaponsFormIventory()
	{
		List<UserArtikul> list = new List<UserArtikul>();
		foreach (SlotType item in list_0)
		{
			list.AddRange(UserController.UserController_0.GetUserArtikulsBySlotType(item));
		}
		return list;
	}

	public List<UserArtikul> GetAllWeaponsFormIventoryOtherUser(int int_0)
	{
		List<UserArtikul> list = new List<UserArtikul>();
		foreach (SlotType item in list_0)
		{
			list.AddRange(UserController.UserController_0.GetAnyUserArtikulsBySlotType(item, int_0));
		}
		return list;
	}

	public Dictionary<int, WeaponData> weapons
	{
		get
		{
			if (_weapons == null)
			{
				Dictionary<int, WeaponData> weaponData = new Dictionary<int, WeaponData>();
				Weapons weaponsObject = Resources.Load<Weapons>("Weapons");

				foreach (Weapons.Weapon weapon in weaponsObject.weapons)
				{
					weaponData.Add(weapon.id, weapon.ToWeaponData());
				}

				_weapons = weaponData;
			}

			return _weapons;
		}
	}

	private Dictionary<int, WeaponData> _weapons;

	public WeaponData GetWeapon(int int_0)
	{
		return weapons[int_0];//WeaponStorage.Get.Storage.GetObjectByKey(int_0);
	}

	public void EquipWeapon(int int_0)
	{
		WeaponData weapon = GetWeapon(int_0);
		if (weapon == null)
		{
			Log.AddLine(string.Format("WeaponController::EquipWeapon > weapon {0} is null", int_0));
		}
		else
		{
			EquipWeaponByArtikulId(weapon.Int32_0);
		}
	}

	public void UnequipWeapon(int int_0)
	{
		WeaponData weapon = GetWeapon(int_0);
		if (weapon == null)
		{
			Log.AddLine(string.Format("WeaponController::UnequipWeapon > weapon {0} is null", int_0));
		}
		else
		{
			UnequipWeaponByArtikulId(weapon.Int32_0);
		}
	}

	public void EquipWeaponByArtikulId(int int_0)
	{
		UserController.UserController_0.EquipArtikul(int_0);
	}

	public void UnequipWeaponByArtikulId(int int_0)
	{
		UserController.UserController_0.UnequipArtikul(int_0);
	}

	public List<KeyCode> GetMapKeycodeSlotType(Dictionary<KeyCode, SlotType> dictionary_1)
	{
		List<KeyCode> list = new List<KeyCode>();
		InputManager.ButtonState value = null;
		InputManager.dictionary_0.TryGetValue("Weapon1", out value);
		dictionary_1[value.keyCode_0] = SlotType.SLOT_WEAPON_PRIMARY;
		InputManager.dictionary_0.TryGetValue("Weapon2", out value);
		dictionary_1[value.keyCode_0] = SlotType.SLOT_WEAPON_BACKUP;
		InputManager.dictionary_0.TryGetValue("Weapon3", out value);
		dictionary_1[value.keyCode_0] = SlotType.SLOT_WEAPON_SPECIAL;
		InputManager.dictionary_0.TryGetValue("Weapon4", out value);
		dictionary_1[value.keyCode_0] = SlotType.SLOT_WEAPON_PREMIUM;
		InputManager.dictionary_0.TryGetValue("Weapon5", out value);
		dictionary_1[value.keyCode_0] = SlotType.SLOT_WEAPON_SNIPER;
		InputManager.dictionary_0.TryGetValue("Weapon6", out value);
		dictionary_1[value.keyCode_0] = SlotType.SLOT_WEAPON_MELEE;
		list = dictionary_1.Keys.ToList();
		list.Sort((KeyCode keyCode_0, KeyCode keyCode_1) => keyCode_0.CompareTo(keyCode_1));
		return list;
	}

	public List<SlotType> GetSortedSlotTypes()
	{
		Dictionary<KeyCode, SlotType> dictionary = new Dictionary<KeyCode, SlotType>();
		List<KeyCode> mapKeycodeSlotType = GetMapKeycodeSlotType(dictionary);
		list_0.Clear();
		for (int i = 0; i < mapKeycodeSlotType.Count; i++)
		{
			list_0.Add(dictionary[mapKeycodeSlotType[i]]);
		}
		return list_0;
	}

	public Dictionary<SlotType, int> GetUserWeaponsFromSlots()
	{
		Dictionary<SlotType, int> dictionary = new Dictionary<SlotType, int>();
		GetSortedSlotTypes();
		foreach (SlotType item in list_0)
		{
			UserArtikul userArtikulDataFromSlot = UserController.UserController_0.GetUserArtikulDataFromSlot(item);
			if (userArtikulDataFromSlot != null && userArtikulDataFromSlot.int_1 > 0)
			{
				dictionary.Add(item, userArtikulDataFromSlot.int_0);
			}
		}
		return dictionary;
	}

	public SlotType GetFirstUserWeaponFromSlots(int int_0 = 1)
	{
		GetSortedSlotTypes();
		int num = 0;
		foreach (SlotType item in list_0)
		{
			UserArtikul userArtikulDataFromSlot = UserController.UserController_0.GetUserArtikulDataFromSlot(item);
			if (userArtikulDataFromSlot != null && userArtikulDataFromSlot.int_1 > 0 && ++num >= int_0)
			{
				return item;
			}
		}
		return SlotType.SLOT_NONE;
	}

	public SlotType GetActiveWeaponSlotType()
	{
		Dictionary<SlotType, int> activesSlotType = UserController.UserController_0.GetActivesSlotType(LocalUserData.ActiveSlotType.Weapon);
		if (activesSlotType != null && activesSlotType.Count != 0)
		{
			foreach (KeyValuePair<SlotType, int> item in activesSlotType)
			{
				if (item.Value > 0)
				{
					return item.Key;
				}
			}
			return SlotType.SLOT_NONE;
		}
		return SlotType.SLOT_NONE;
	}
}
