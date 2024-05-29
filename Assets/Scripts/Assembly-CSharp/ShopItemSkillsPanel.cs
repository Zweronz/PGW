using System.Collections.Generic;
using UnityEngine;

public class ShopItemSkillsPanel : MonoBehaviour
{
	private static readonly Dictionary<SkillId, string> dictionary_0 = new Dictionary<SkillId, string>
	{
		{
			SkillId.SKILL_WEAPON_AREA_DAMAGE,
			"icon5"
		},
		{
			SkillId.SKILL_WEAPON_BULLET_COUNT,
			"icon5"
		},
		{
			SkillId.SKILL_WEAPON_WALLS_BREAK,
			"icon6"
		},
		{
			SkillId.SKILL_WEAPON_ZOOM,
			"icon7"
		},
		{
			SkillId.SKILL_WEAPON_DAMAGE_CRIT_CHANCE,
			"icon8"
		},
		{
			SkillId.SKILL_JUMP_MODIFIER,
			"icon9"
		},
		{
			SkillId.SKILL_ARMOR_ABSORB,
			"icon14"
		},
		{
			SkillId.SKILL_WEAPON_REBOUND,
			"icon16"
		},
		{
			SkillId.SKILL_WEAPON_THROUGH_ENEMIES,
			"icon15"
		}
	};

	private static readonly Dictionary<WeaponType, string> dictionary_1 = new Dictionary<WeaponType, string>
	{
		{
			WeaponType.FLAME_THROWER,
			"icon5"
		},
		{
			WeaponType.FREEZER,
			"icon15"
		},
		{
			WeaponType.HOOK,
			"icon17"
		}
	};

	public UIWidget skillsContainer;

	public ShopItemSkill skillTemplate;

	private int int_0;

	private int int_1;

	public int Init(ArtikulData artikulData_0)
	{
		foreach (Transform item in skillsContainer.transform)
		{
			Object.Destroy(item.gameObject);
		}
		if (artikulData_0.Dictionary_0 == null)
		{
			return 0;
		}
		int_0 = skillTemplate.GetComponent<UIWidget>().Int32_1;
		NGUITools.SetActive(skillTemplate.gameObject, false);
		int_1 = 0;
		if (artikulData_0.Boolean_1)
		{
			foreach (KeyValuePair<SkillId, SkillData> item2 in artikulData_0.Dictionary_0)
			{
				if (dictionary_0.ContainsKey(item2.Key))
				{
					string string_ = string.Format(LocalizationStorage.Get.Term(string.Format("ui.skill.{0}.title", item2.Key.ToString())), item2.Value.Int32_0);
					CreateSkillView(string_, dictionary_0[item2.Key]);
				}
			}
			WeaponData weapon = WeaponController.WeaponController_0.GetWeapon(artikulData_0.Int32_0);
			if (weapon != null && dictionary_1.ContainsKey(weapon.WeaponType_0))
			{
				string string_2 = string.Format(LocalizationStorage.Get.Term(string.Format("ui.weapon_type.{0}.title", weapon.WeaponType_0.ToString())));
				CreateSkillView(string_2, dictionary_1[weapon.WeaponType_0]);
			}
		}
		else if (artikulData_0.Boolean_2 && artikulData_0.SlotType_0 != SlotType.SLOT_WEAR_ARMOR)
		{
			foreach (KeyValuePair<SkillId, SkillData> item3 in artikulData_0.Dictionary_0)
			{
				if (dictionary_0.ContainsKey(item3.Key) && item3.Key == SkillId.SKILL_ARMOR_ABSORB)
				{
					string modifierTextKey = ShopItemSkillsBigPanel.GetModifierTextKey(item3.Key, 0, item3.Value.Single_0);
					string string_3 = Localizer.Get(string.Format("ui.skill.{0}.slot.{1}.{2}", item3.Key.ToString(), artikulData_0.SlotType_0.ToString(), modifierTextKey));
					CreateSkillView(string_3, dictionary_0[item3.Key]);
				}
			}
		}
		return int_1;
	}

	private void CreateSkillView(string string_0, string string_1)
	{
		GameObject gameObject = NGUITools.AddChild(skillsContainer.gameObject, skillTemplate.gameObject);
		gameObject.name = string.Format("{0:00}", int_1);
		ShopItemSkill component = gameObject.GetComponent<ShopItemSkill>();
		component.Init(string_0, string_1);
		Vector3 localPosition = gameObject.transform.localPosition;
		localPosition.y -= int_0 * int_1;
		gameObject.transform.localPosition = localPosition;
		int_1++;
		NGUITools.SetActive(gameObject, true);
	}
}
