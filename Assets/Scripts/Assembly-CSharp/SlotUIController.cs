using System;
using System.Collections.Generic;
using UnityEngine;
using engine.unity;
using pixelgun.tutorial;

public class SlotUIController : MonoBehaviour
{
	public class TimingAction
	{
		private float float_0;

		private float float_1;

		private Action action_0;

		public TimingAction(Action action_1, float float_2, float float_3 = 0f)
		{
			action_0 = action_1;
			float_1 = float_2;
			float_0 = float_3;
		}

		public void Update()
		{
			float_0 -= Time.deltaTime;
			if (float_0 < 0f)
			{
				float_0 = float_1;
				action_0();
			}
		}
	}

	public new GameObject active;

	public GameObject unactive;

	public UILabel[] count;

	public UILabel[] num;

	public UITexture[] img;

	public UILabel timer;

	public UILabel CooldownLabel;

	public SlotType slot;

	public string inputAction = string.Empty;

	public int indx = -1;

	private KeyCode keyCode_0;

	private bool bool_0;

	private int int_0;

	private int int_1;

	private bool bool_1;

	private List<TimingAction> list_0 = new List<TimingAction>();

	private void Start()
	{
		if (slot == SlotType.SLOT_NONE)
		{
			if (indx < 0)
			{
				return;
			}
			InitCustomKeySlot();
		}
		updateActive(true);
		updateAmmoCount();
		updateTexture();
		if (timer != null)
		{
			list_0.Add(new TimingAction(updateTime, 0.1f, 0f));
		}
		list_0.Add(new TimingAction(updateActionText, 1f, 0f));
		Subscribe();
	}

	private void OnDestroy()
	{
		Unsubscribe();
	}

	private void Update()
	{
		if (slot != 0)
		{
			Subscribe();
			updateKeyboard();
			updateActive();
			for (int i = 0; i < list_0.Count; i++)
			{
				list_0[i].Update();
			}
		}
	}

	private void updateKeyboard()
	{
		if (HeadUpDisplay.HeadUpDisplay_0 != null && HeadUpDisplay.HeadUpDisplay_0.Boolean_0)
		{
			return;
		}
		if (string.IsNullOrEmpty(inputAction) && !TutorialController.TutorialController_0.Boolean_0)
		{
			if (!Defs.bool_11)
			{
				if (Input.GetKeyDown(keyCode_0) && (!bool_0 || (slot >= SlotType.SLOT_CONSUM_POTION && slot <= SlotType.SLOT_CONSUM_TURRET)))
				{
					HeadUpDisplay.HeadUpDisplay_0.SlotButtonDown(slot);
				}
				if (Input.GetKeyUp(keyCode_0))
				{
					HeadUpDisplay.HeadUpDisplay_0.SlotButtonUp(slot);
				}
			}
		}
		else if (!Defs.bool_11)
		{
			if (InputManager.GetButtonDown(inputAction) && (!bool_0 || (slot >= SlotType.SLOT_CONSUM_POTION && slot <= SlotType.SLOT_CONSUM_TURRET)))
			{
				HeadUpDisplay.HeadUpDisplay_0.SlotButtonDown(slot);
			}
			if (InputManager.GetButtonUp(inputAction))
			{
				HeadUpDisplay.HeadUpDisplay_0.SlotButtonUp(slot);
			}
		}
	}

	private void updateActive(bool bool_2 = false)
	{
		bool flag = false;
		flag = ((slot != SlotType.SLOT_CONSUM_TURRET) ? UserController.UserController_0.IsActiveSlot(slot) : (ConsumablesController.ConsumablesController_0.GetTimeForSlot(slot) > 0));
		if (flag != bool_0 || bool_2)
		{
			setActive(flag);
		}
		if (!flag)
		{
			updateCooldown();
		}
	}

	private void updateCooldown()
	{
		if (!(CooldownLabel == null))
		{
			float cooldownForSlot = ConsumablesController.ConsumablesController_0.GetCooldownForSlot(slot);
			if (cooldownForSlot > 0f)
			{
				TimeSpan timeSpan = TimeSpan.FromSeconds(Mathf.CeilToInt(cooldownForSlot));
				CooldownLabel.String_0 = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
			}
			else
			{
				CooldownLabel.String_0 = string.Empty;
			}
		}
	}

	private void Subscribe()
	{
		if (!bool_1 && !(WeaponManager.weaponManager_0 == null))
		{
			if (slot >= SlotType.SLOT_WEAPON_PRIMARY && slot <= SlotType.SLOT_WEAPON_SNIPER)
			{
				WeaponManager.weaponManager_0.Subscribe(WeaponManager.EventType.AMMO_IN_CLIP_CHANGE, updateAmmoCount);
				WeaponManager.weaponManager_0.Subscribe(WeaponManager.EventType.AMMO_IN_BACKPACK_CHANGE, updateAmmoCount);
				KeyboardController.KeyboardController_0.Subscribe(InitCustomKeySlot, KeyboardControllerEvent.SETTINGS_CONTROL_CHANGED);
			}
			else if (slot >= SlotType.SLOT_CONSUM_POTION && slot <= SlotType.SLOT_CONSUM_GRENADE)
			{
				UsersData.Subscribe(UsersData.EventType.ARTIKUL_CHANGED, OnArtChange);
			}
			UsersData.Subscribe(UsersData.EventType.SLOT_CHANGED, OnSlotChange);
			bool_1 = true;
		}
	}

	private void Unsubscribe()
	{
		if (bool_1)
		{
			if (WeaponManager.weaponManager_0 != null && slot >= SlotType.SLOT_WEAPON_PRIMARY && slot <= SlotType.SLOT_WEAPON_SNIPER)
			{
				WeaponManager.weaponManager_0.Unsubscribe(WeaponManager.EventType.AMMO_IN_CLIP_CHANGE, updateAmmoCount);
				WeaponManager.weaponManager_0.Unsubscribe(WeaponManager.EventType.AMMO_IN_BACKPACK_CHANGE, updateAmmoCount);
			}
			if (slot >= SlotType.SLOT_CONSUM_POTION && slot <= SlotType.SLOT_CONSUM_GRENADE)
			{
				UsersData.Unsubscribe(UsersData.EventType.ARTIKUL_CHANGED, OnArtChange);
			}
			UsersData.Unsubscribe(UsersData.EventType.SLOT_CHANGED, OnSlotChange);
			KeyboardController.KeyboardController_0.Unsubscribe(InitCustomKeySlot);
		}
	}

	private void updateAmmoCount(int int_2)
	{
		if (int_2 == (int)slot)
		{
			updateAmmoCount();
		}
	}

	private void OnArtChange(UsersData.EventData eventData_0)
	{
		UserArtikul userArtikulById = UserController.UserController_0.GetUserArtikulById(eventData_0.string_0);
		if (userArtikulById == null)
		{
			updateAmmoCount();
		}
		else if (userArtikulById.ArtikulData_0.SlotType_0 == slot)
		{
			updateAmmoCount();
		}
	}

	private void updateAmmoCount()
	{
		string text = string.Empty;
		if (slot >= SlotType.SLOT_WEAPON_PRIMARY && slot <= SlotType.SLOT_WEAPON_SNIPER && slot != SlotType.SLOT_WEAPON_MELEE)
		{
			if (WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.playerWeapons.ContainsKey(slot))
			{
				Weapon weapon = WeaponManager.weaponManager_0.playerWeapons[slot];
				WeaponSounds weaponSounds_ = weapon.WeaponSounds_0;
				WeaponData weaponData_ = weaponSounds_.WeaponData_0;
				text = (weapon.Int32_1 + weapon.Int32_0).ToString();
			}
		}
		else if (slot >= SlotType.SLOT_CONSUM_POTION && slot <= SlotType.SLOT_CONSUM_GRENADE)
		{
			text = UserController.UserController_0.GetUserArtikulCount(UserController.UserController_0.GetArtikulIdFromSlot(slot)).ToString();
		}
		if (UserController.UserController_0.GetArtikulIdFromSlot(slot) == 0)
		{
			text = string.Empty;
		}
		setCount(text);
	}

	private void updateTime()
	{
		int timeForSlot = ConsumablesController.ConsumablesController_0.GetTimeForSlot(slot);
		if (timeForSlot > 0 && timeForSlot != int_0)
		{
			int_0 = timeForSlot;
			setTimer(int_0);
		}
	}

	private void updateActionText()
	{
		if (num == null || num.Length == 0)
		{
			return;
		}
		string string_ = string.Empty;
		if (!string.IsNullOrEmpty(inputAction))
		{
			InputManager.ButtonState value = null;
			InputManager.dictionary_0.TryGetValue(inputAction, out value);
			if (value != null)
			{
				if (value.keyCode_0 != 0)
				{
					string_ = SettingsControlItemSlot.GetKeyText(value.keyCode_0);
				}
				else if (value.keyCode_1 != 0)
				{
					string_ = SettingsControlItemSlot.GetKeyText(value.keyCode_1);
				}
			}
		}
		else if (keyCode_0 != 0)
		{
			string_ = SettingsControlItemSlot.GetKeyText(keyCode_0);
		}
		for (int i = 0; i < num.Length; i++)
		{
			num[i].String_0 = string_;
		}
	}

	private void InitCustomKeySlot()
	{
		if (!TutorialController.TutorialController_0.Boolean_0)
		{
			inputAction = string.Empty;
		}
		Dictionary<KeyCode, SlotType> dictionary = new Dictionary<KeyCode, SlotType>();
		List<KeyCode> mapKeycodeSlotType = WeaponController.WeaponController_0.GetMapKeycodeSlotType(dictionary);
		keyCode_0 = mapKeycodeSlotType[indx];
		slot = dictionary[keyCode_0];
		updateActive(true);
		updateAmmoCount();
		updateTexture();
		if (timer != null)
		{
			list_0.Add(new TimingAction(updateTime, 0.1f, 0f));
		}
		list_0.Add(new TimingAction(updateActionText, 1f, 0f));
	}

	private void setActive(bool bool_2)
	{
		bool_0 = bool_2;
		if (active != null)
		{
			NGUITools.SetActive(active, bool_0);
		}
		if (unactive != null)
		{
			NGUITools.SetActive(unactive, !bool_0);
		}
		if (timer != null)
		{
			NGUITools.SetActive(timer.gameObject, bool_0);
			if (!bool_0)
			{
				timer.String_0 = string.Empty;
				int_0 = 0;
			}
		}
	}

	private void setCount(string string_0)
	{
		if (count != null && count.Length != 0)
		{
			for (int i = 0; i < count.Length; i++)
			{
				count[i].String_0 = string_0;
			}
		}
	}

	private void setTimer(int int_2)
	{
		if (!(timer == null))
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds(int_2);
			timer.String_0 = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
			if (int_2 <= 5)
			{
				timer.Color_0 = new Color(1f, 0f, 0f);
			}
			else
			{
				timer.Color_0 = new Color(0f, 0f, 0f);
			}
		}
	}

	private void OnSlotChange(UsersData.EventData eventData_0)
	{
		if (eventData_0.int_0 == (int)slot)
		{
			updateTexture();
			updateAmmoCount();
		}
	}

	private void updateTexture()
	{
		int artikulIdFromSlot = UserController.UserController_0.GetArtikulIdFromSlot(slot);
		if (artikulIdFromSlot == int_1)
		{
			return;
		}
		int_1 = artikulIdFromSlot;
		Texture texture = ImageLoader.LoadArtikulTexture(int_1);
		if (texture == null)
		{
			switch (slot)
			{
			case SlotType.SLOT_WEAPON_PRIMARY:
				texture = ImageLoader.LoadTexture("UI/images/shop_icons_primary");
				break;
			case SlotType.SLOT_WEAPON_BACKUP:
				texture = ImageLoader.LoadTexture("UI/images/shop_icons_backup");
				break;
			case SlotType.SLOT_WEAPON_MELEE:
				texture = ImageLoader.LoadTexture("UI/images/shop_icons_melee");
				break;
			case SlotType.SLOT_WEAPON_PREMIUM:
				texture = ImageLoader.LoadTexture("UI/images/shop_icons_premium");
				break;
			case SlotType.SLOT_WEAPON_SPECIAL:
			case SlotType.SLOT_WEAPON_SNIPER:
				texture = ImageLoader.LoadTexture("UI/images/shop_icons_spec");
				break;
			}
		}
		if (texture != null)
		{
			setImage(texture);
		}
	}

	private void setImage(Texture texture_0)
	{
		if (img != null && img.Length != 0)
		{
			for (int i = 0; i < img.Length; i++)
			{
				img[i].Texture_0 = texture_0;
			}
		}
	}
}
