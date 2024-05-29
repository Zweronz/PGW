using System.Collections.Generic;
using UnityEngine;

public class AmmunitionUIController : MonoBehaviour
{
	public GameObject plate;

	public UILabel clip;

	public UILabel other;

	public UISlider progressBar;

	private bool bool_0 = true;

	private string string_0 = string.Empty;

	private string string_1 = string.Empty;

	private float float_0 = 1f;

	private void Start()
	{
		clip.String_0 = string_0;
		other.String_0 = string_1;
	}

	private void Update()
	{
		Dictionary<SlotType, int> activesSlotType = UserController.UserController_0.GetActivesSlotType(LocalUserData.ActiveSlotType.Weapon);
		if (activesSlotType != null && activesSlotType.Count != 0 && !(WeaponManager.weaponManager_0 == null))
		{
			WeaponData weaponData = null;
			foreach (KeyValuePair<SlotType, int> item in activesSlotType)
			{
				weaponData = WeaponController.WeaponController_0.GetWeapon(item.Value);
				if (weaponData != null)
				{
					break;
				}
			}
			if (weaponData != null && weaponData.SlotType_0 != SlotType.SLOT_WEAPON_MELEE && WeaponManager.weaponManager_0.playerWeapons.ContainsKey(weaponData.SlotType_0))
			{
				Weapon weapon = WeaponManager.weaponManager_0.playerWeapons[weaponData.SlotType_0];
				weapon.GameObject_0.GetComponent<WeaponSounds>();
				string empty = string.Empty;
				string otherText = string.Empty;
				if (weaponData.Boolean_3)
				{
					empty = (weapon.Int32_1 + weapon.Int32_0).ToString();
				}
				else
				{
					empty = weapon.Int32_1.ToString();
					otherText = string.Format("/{0}", weapon.Int32_0);
				}
				float num = (float)(weapon.Int32_1 + weapon.Int32_0) * 1f;
				float progress = num / ((weaponData.Int32_3 + weapon.Int32_1 <= 0) ? 1f : ((float)(weaponData.Int32_3 + weapon.Int32_1)));
				setShow(true);
				setClipText(empty);
				setOtherText(otherText);
				setProgress(progress);
				plate.GetComponent<Animation>().enabled = weapon.Int32_0 == 0;
				if (!plate.GetComponent<Animation>().enabled)
				{
					progressBar.GetComponent<AnimatedColor>().color = Color.white;
					clip.GetComponent<AnimatedColor>().color = Color.white;
				}
			}
			else
			{
				setShow(false);
			}
		}
		else
		{
			setShow(false);
		}
	}

	private void setShow(bool bool_1)
	{
		if (bool_1 != bool_0)
		{
			bool_0 = bool_1;
			NGUITools.SetActive(plate, bool_0);
		}
	}

	private void setClipText(string string_2)
	{
		if (!string.Equals(string_0, string_2))
		{
			string_0 = string_2;
			clip.String_0 = string_0;
		}
	}

	private void setOtherText(string string_2)
	{
		if (!string.Equals(string_1, string_2))
		{
			string_1 = string_2;
			other.String_0 = string_1;
		}
	}

	private void setProgress(float float_1)
	{
		if (float_0 == float_1)
		{
			return;
		}
		float_0 = float_1;
		if (progressBar != null)
		{
			progressBar.Single_0 = float_0;
			if (float_0 == 0f)
			{
				progressBar.gameObject.SetActive(false);
			}
			else
			{
				progressBar.gameObject.SetActive(true);
			}
		}
	}
}
