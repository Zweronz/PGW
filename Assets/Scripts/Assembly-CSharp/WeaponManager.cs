using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.events;
using engine.helpers;
using engine.unity;

public sealed class WeaponManager : MonoBehaviour
{
	public enum EventType
	{
		AMMO_IN_CLIP_CHANGE = 0,
		AMMO_IN_BACKPACK_CHANGE = 1
	}

	public static WeaponManager weaponManager_0;

	public GameObject myPlayer;

	public Player_move_c myPlayerMoveC;

	public GameObject myTable;

	public NetworkStartTable myNetworkStartTable;

	public PlayerScoreController myScoreController;

	public Dictionary<SlotType, Weapon> playerWeapons = new Dictionary<SlotType, Weapon>();

	public List<Weapon> allAvailablePlayerWeapons = new List<Weapon>();

	private WeaponSounds weaponSounds_0 = new WeaponSounds();

	private Dictionary<int, Weapon> dictionary_0 = new Dictionary<int, Weapon>();

	private BaseEvent<int> baseEvent_0 = new BaseEvent<int>();

	public SlotType nextWeaponSlot;

	private static Action<SlotType> action_0;

	[CompilerGenerated]
	private bool bool_0;

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		private set
		{
			bool_0 = value;
		}
	}

	public WeaponSounds WeaponSounds_0
	{
		get
		{
			if (!myPlayerMoveC.PlayerMechController_0.Boolean_1)
			{
				return myPlayerMoveC.PlayerMechController_0.mechWeaponSounds;
			}
			return weaponSounds_0;
		}
		set
		{
			if (weaponSounds_0 != null)
			{
				UserController.UserController_0.SetActiveSlot(weaponSounds_0.WeaponData_0.SlotType_0, false);
				if (myPlayerMoveC.PlayerGrenadeController_0.Boolean_0)
				{
					nextWeaponSlot = weaponSounds_0.WeaponData_0.SlotType_0;
				}
			}
			else if (nextWeaponSlot == SlotType.SLOT_NONE)
			{
				nextWeaponSlot = WeaponController.WeaponController_0.GetFirstUserWeaponFromSlots(2);
			}
			weaponSounds_0 = value;
			if (weaponSounds_0 != null)
			{
				UserController.UserController_0.SetActiveSlot(weaponSounds_0.WeaponData_0.SlotType_0, true);
			}
		}
	}

	public static event Action<SlotType> WeaponEquipped
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			action_0 = (Action<SlotType>)Delegate.Combine(action_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			action_0 = (Action<SlotType>)Delegate.Remove(action_0, value);
		}
	}

	static WeaponManager()
	{
	}

	public void Dispatch<EventType>(EventType gparam_0, int int_0)
	{
		baseEvent_0.Dispatch(int_0, gparam_0);
	}

	public void Subscribe(EventType eventType_0, Action<int> action_1)
	{
		if (!baseEvent_0.Contains(action_1, eventType_0))
		{
			baseEvent_0.Subscribe(action_1, eventType_0);
		}
	}

	public void Unsubscribe(EventType eventType_0, Action<int> action_1)
	{
		baseEvent_0.Unsubscribe(action_1, eventType_0);
	}

	public static GameObject AddRay(Vector3 vector3_0, Vector3 vector3_1, string string_0)
	{
		GameObject gameObject = Resources.Load(ResPath.Combine("Rays", string_0)) as GameObject;
		if (gameObject == null)
		{
			gameObject = Resources.Load(ResPath.Combine("Rays", "Weapon77")) as GameObject;
		}
		if (gameObject == null)
		{
			return null;
		}
		GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, vector3_0, Quaternion.identity) as GameObject;
		gameObject2.transform.forward = vector3_1;
		return gameObject2;
	}

	private void Start()
	{
		weaponManager_0 = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		UsersData.Subscribe(UsersData.EventType.INIT_COMPLETE, OnInitAllWeapons);
		UsersData.Subscribe(UsersData.EventType.SLOT_CHANGED, OnInitUserWeapons);
		Boolean_0 = true;
	}

	private void OnDestroy()
	{
		UsersData.Unsubscribe(UsersData.EventType.INIT_COMPLETE, OnInitAllWeapons);
		UsersData.Unsubscribe(UsersData.EventType.SLOT_CHANGED, OnInitUserWeapons);
	}

	public void ResetAmmoInAllWeapon()
	{
		foreach (Weapon allAvailablePlayerWeapon in allAvailablePlayerWeapons)
		{
			allAvailablePlayerWeapon.ResetAmmo();
		}
	}

	public Weapon GetWeaponFromSlot(SlotType slotType_0)
	{
		if (slotType_0 == SlotType.SLOT_NONE)
		{
			return null;
		}
		Weapon value = null;
		playerWeapons.TryGetValue(slotType_0, out value);
		return value;
	}

	public Weapon GetWeaponFromCurrentSlot()
	{
		if (WeaponSounds_0 == null)
		{
			return null;
		}
		return GetWeaponFromSlot(WeaponSounds_0.WeaponData_0.SlotType_0);
	}

	public Weapon GetPrevNextWeapon(bool bool_1 = false)
	{
		List<SlotType> sortedSlotTypes = WeaponController.WeaponController_0.GetSortedSlotTypes();
		int num = sortedSlotTypes.IndexOf(WeaponSounds_0.WeaponData_0.SlotType_0);
		if (num == -1)
		{
			return GetWeaponFromCurrentSlot();
		}
		Weapon weapon = null;
		do
		{
			if (!bool_1)
			{
				num++;
				num %= sortedSlotTypes.Count;
			}
			else
			{
				num--;
				num = ((num >= 0) ? num : (sortedSlotTypes.Count - 1));
			}
			if (playerWeapons.ContainsKey(sortedSlotTypes[num]))
			{
				weapon = playerWeapons[sortedSlotTypes[num]];
			}
		}
		while (weapon == null);
		return playerWeapons[sortedSlotTypes[num]];
	}

	public Weapon GetRandomWeaponFromAvailableWeapons()
	{
		return allAvailablePlayerWeapons[UnityEngine.Random.Range(0, allAvailablePlayerWeapons.Count)];
	}

	private void EquipWeapon(Weapon weapon_0)
	{
		if (weapon_0 == null)
		{
			Log.AddLine("[WeaponManager::EquipWeapon. Error eqipWeapon, weapon = null]");
			return;
		}
		SlotType slotType_ = weapon_0.WeaponSounds_0.WeaponData_0.SlotType_0;
		if (!playerWeapons.ContainsKey(slotType_))
		{
			playerWeapons.Add(slotType_, weapon_0);
		}
		else
		{
			playerWeapons[slotType_] = weapon_0;
		}
		if (action_0 != null)
		{
			action_0(slotType_);
		}
	}

	public void Reset()
	{
		playerWeapons.Clear();
		InitAllWeapons();
		InitUserWeapons();
	}

	public bool AddWeapon(int int_0)
	{
		if (MonoSingleton<FightController>.Prop_0.NetworkStateMode_0 != FightController.NetworkStateMode.Offline)
		{
			return false;
		}
		Weapon value = null;
		dictionary_0.TryGetValue(int_0, out value);
		if (value == null)
		{
			Log.AddLine("[WeaponManager::AddWeapon. Error added weapon = null, weaponId = ]" + int_0, Log.LogLevel.WARNING);
			return false;
		}
		Weapon weaponFromSlot = GetWeaponFromSlot(value.WeaponSounds_0.WeaponData_0.SlotType_0);
		if (weaponFromSlot != null && value.WeaponSounds_0.WeaponData_0.Single_3 < weaponFromSlot.WeaponSounds_0.WeaponData_0.Single_3)
		{
			Log.AddLine(string.Format("[WeaponManager::AddWeapon. Error added weapon id = {0} by DPS]", int_0), Log.LogLevel.WARNING);
			return false;
		}
		EquipWeapon(value);
		SlotType slotType_ = value.WeaponSounds_0.WeaponData_0.SlotType_0;
		if (AddAmmo(slotType_) == 0)
		{
		}
		return true;
	}

	public int AddAmmo(SlotType slotType_0 = SlotType.SLOT_NONE)
	{
		Weapon weapon = null;
		if (slotType_0 == SlotType.SLOT_NONE)
		{
			weapon = GetWeaponFromCurrentSlot();
			if (weapon != null)
			{
				slotType_0 = weapon.WeaponSounds_0.WeaponData_0.SlotType_0;
			}
		}
		if (slotType_0 == SlotType.SLOT_NONE)
		{
			return 0;
		}
		if (WeaponSounds_0.WeaponData_0.Boolean_2 && !WeaponSounds_0.WeaponData_0.Boolean_3)
		{
			return 0;
		}
		if (!playerWeapons.ContainsKey(slotType_0))
		{
			return 0;
		}
		weapon = playerWeapons[slotType_0];
		WeaponSounds component = weapon.GameObject_0.GetComponent<WeaponSounds>();
		if (weapon.Int32_0 >= component.Int32_0)
		{
			return 0;
		}
		int num = component.WeaponData_0.Int32_2;
		if (weapon.Int32_0 + num > component.Int32_0)
		{
			num = component.Int32_0 - weapon.Int32_0;
		}
		weapon.Int32_0 += num;
		return num;
	}

	public void SetMaxAmmoFrAllWeapons(bool bool_1 = false)
	{
		foreach (Weapon allAvailablePlayerWeapon in allAvailablePlayerWeapons)
		{
			allAvailablePlayerWeapon.Int32_1 = allAvailablePlayerWeapon.WeaponSounds_0.WeaponData_0.Int32_2;
			int int32_ = (bool_1 ? allAvailablePlayerWeapon.WeaponSounds_0.Int32_1 : allAvailablePlayerWeapon.WeaponSounds_0.Int32_0);
			allAvailablePlayerWeapon.Int32_0 = int32_;
		}
	}

	public void ReloadAmmo()
	{
		SlotType slotType_ = WeaponSounds_0.WeaponData_0.SlotType_0;
		if (!playerWeapons.ContainsKey(slotType_))
		{
			Log.AddLine("WeaponManager::ReloadAmmo. Reload ammo error, current weapon slot type = " + slotType_);
			return;
		}
		Weapon weapon = playerWeapons[slotType_];
		int num = WeaponSounds_0.WeaponData_0.Int32_2 - weapon.Int32_1;
		if (weapon.Int32_0 >= num)
		{
			weapon.Int32_1 += num;
			weapon.Int32_0 -= num;
		}
		else
		{
			weapon.Int32_1 += weapon.Int32_0;
			weapon.Int32_0 = 0;
		}
		if (myPlayerMoveC != null)
		{
			myPlayerMoveC.Boolean_10 = false;
		}
	}

	public void Reload()
	{
		if (!WeaponSounds_0.WeaponData_0.Boolean_3)
		{
			WeaponSounds_0.GameObject_0.GetComponent<Animation>().Stop("Empty");
			if (!WeaponSounds_0.WeaponData_0.Boolean_5)
			{
				WeaponSounds_0.GameObject_0.GetComponent<Animation>().CrossFade("Shoot");
			}
			float float_ = 0f;
			WeaponSounds_0.PlayReloadAnimation(out float_);
		}
	}

	public void StopReload()
	{
		WeaponSounds_0.GameObject_0.GetComponent<Animation>().Stop("Reload");
	}

	private void OnInitAllWeapons(UsersData.EventData eventData_0)
	{
		Reset();
	}

	private void OnInitUserWeapons(UsersData.EventData eventData_0)
	{
		SlotType int_ = (SlotType)eventData_0.int_0;
		if (int_ <= SlotType.SLOT_WEAPON_SNIPER && int_ >= SlotType.SLOT_WEAPON_PRIMARY)
		{
			InitUserWeapons();
			if (action_0 != null)
			{
				action_0(int_);
			}
		}
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

	private void InitAllWeapons()
	{
		dictionary_0.Clear();
		allAvailablePlayerWeapons.Clear();
		foreach (KeyValuePair<int, WeaponData> item in weapons)
		{
			if (item.Value.ArtikulData_0.Boolean_1)
			{
				bool flag;
				Weapon weapon = new Weapon(item.Key, out flag);
				if (flag)
				{
					allAvailablePlayerWeapons.Add(weapon);
					dictionary_0.Add(item.Key, weapon);
				}
			}
		}
	}

	public void InitUserWeapons()
	{
		Dictionary<SlotType, int> userWeaponsFromSlots = WeaponController.WeaponController_0.GetUserWeaponsFromSlots();
		foreach (KeyValuePair<SlotType, int> item in userWeaponsFromSlots)
		{
			Weapon value = null;
			if (dictionary_0.TryGetValue(item.Value, out value))
			{
				EquipWeapon(value);
			}
		}
	}
}
