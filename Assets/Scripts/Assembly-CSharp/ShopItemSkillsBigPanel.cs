using System.Collections.Generic;
using UnityEngine;

public class ShopItemSkillsBigPanel : MonoBehaviour
{
	public static readonly Dictionary<SkillId, string> dictionary_0 = new Dictionary<SkillId, string>
	{
		{
			SkillId.SKILL_SPEED_MODIFIER,
			"icon4"
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
			SkillId.SKILL_NINJA_JUMP,
			"icon10"
		},
		{
			SkillId.SKILL_HEADSHOT_DAMAG_IGNORE_MODIFIER,
			"icon11"
		},
		{
			SkillId.SKILL_HEADSHOT_DAMAG_MODIFIER,
			"icon12"
		},
		{
			SkillId.SKILL_ARMOR,
			"icon13"
		},
		{
			SkillId.SKILL_ARMOR_ABSORB,
			"icon14"
		},
		{
			SkillId.SKILL_WEAPON_AREA_DAMAGE,
			"icon5"
		},
		{
			SkillId.SKILL_EXPLOSION_DAMAGE_MODIFIER,
			"icon5"
		},
		{
			SkillId.SKILL_EXPLOSION_RADIUS_MODIFIER,
			"icon5"
		},
		{
			SkillId.SKILL_SELF_EXPLOSION_RADIUS_MODIFIER,
			"icon5"
		},
		{
			SkillId.SKILL_SELF_EXPLOSION_DAMAGE_MODIFICATOR,
			"icon5"
		},
		{
			SkillId.SKILL_WEAPON_DAMAGE_MODIFIER,
			"icon_weapon_all"
		},
		{
			SkillId.SKILL_RELOAD_WEAPON_TIME_MODIFIER,
			"icon_weapon_all"
		},
		{
			SkillId.SKILL_AMMO_COUNT_MODIFIER_SLOT_PRIMARY,
			"icon_weapon_1"
		},
		{
			SkillId.SKILL_AMMO_COUNT_MODIFIER_SLOT_BACKUP,
			"icon_weapon_2"
		},
		{
			SkillId.SKILL_AMMO_COUNT_MODIFIER_SLOT_SPECIAL,
			"icon_weapon_3"
		},
		{
			SkillId.SKILL_AMMO_COUNT_MODIFIER_SLOT_PREMIUM,
			"icon_weapon_4"
		},
		{
			SkillId.SKILL_AMMO_COUNT_MODIFIER_SLOT_SNIPER,
			"icon_weapon_5"
		}
	};

	private static readonly Dictionary<SlotType, string> dictionary_1 = new Dictionary<SlotType, string>
	{
		{
			SlotType.SLOT_WEAPON_PRIMARY,
			"icon_weapon_1"
		},
		{
			SlotType.SLOT_WEAPON_BACKUP,
			"icon_weapon_2"
		},
		{
			SlotType.SLOT_WEAPON_SPECIAL,
			"icon_weapon_3"
		},
		{
			SlotType.SLOT_WEAPON_PREMIUM,
			"icon_weapon_4"
		},
		{
			SlotType.SLOT_WEAPON_SNIPER,
			"icon_weapon_5"
		},
		{
			SlotType.SLOT_WEAPON_MELEE,
			"icon_weapon_6"
		}
	};

	private static readonly Dictionary<SkillId, bool> dictionary_2 = new Dictionary<SkillId, bool>
	{
		{
			SkillId.SKILL_ARMOR,
			false
		},
		{
			SkillId.SKILL_WEAPON_DAMAGE_MODIFIER,
			true
		},
		{
			SkillId.SKILL_NINJA_JUMP,
			false
		}
	};

	public UIWidget skillsContainer;

	public ShopItemSkillBig skillBigTemplate;

	public ShopItemSkillBig skillBigTemplateVal;

	private int int_0;

	private int int_1;

	private float float_0;

	public void Init(ArtikulData artikulData_0)
	{
		foreach (Transform item in skillsContainer.transform)
		{
			Object.Destroy(item.gameObject);
		}
		NGUITools.SetActive(skillBigTemplate.gameObject, false);
		if (skillBigTemplateVal != null)
		{
			NGUITools.SetActive(skillBigTemplateVal.gameObject, false);
		}
		int_0 = skillBigTemplate.GetComponent<UIWidget>().Int32_0;
		if (artikulData_0.Dictionary_0 == null)
		{
			return;
		}
		int num = 0;
		Dictionary<SkillId, SkillData> dictionary = new Dictionary<SkillId, SkillData>();
		foreach (KeyValuePair<SkillId, SkillData> item2 in artikulData_0.Dictionary_0)
		{
			if ((item2.Key != SkillId.SKILL_ARMOR_ABSORB || artikulData_0.SlotType_0 == SlotType.SLOT_WEAR_ARMOR) && dictionary_0.ContainsKey(item2.Key))
			{
				dictionary.Add(item2.Key, item2.Value);
				num++;
			}
		}
		if (num == 0)
		{
			return;
		}
		float_0 = skillsContainer.Int32_0 / num;
		int_1 = 0;
		foreach (KeyValuePair<SkillId, SkillData> item3 in dictionary)
		{
			string string_ = string.Empty;
			string string_2 = dictionary_0[item3.Key];
			if (item3.Value.NeedsData_0 != null && item3.Value.NeedsData_0.List_0 != null && item3.Value.NeedsData_0.List_0.Count != 0)
			{
				if (item3.Value.NeedsData_0 != null && item3.Value.NeedsData_0.List_0 != null && item3.Value.NeedsData_0.List_0.Count > 0)
				{
					NeedData needData = item3.Value.NeedsData_0.List_0[0];
					if (needData.NeedTypes_0 == NeedTypes.ACTIVE_SLOT_TYPE)
					{
						string_ = LocalizationStorage.Get.Term(string.Format("ui.skill.{0}.{1}.title", item3.Key.ToString(), needData.SlotType_0.ToString()));
						if (item3.Key == SkillId.SKILL_WEAPON_DAMAGE_MODIFIER || item3.Key == SkillId.SKILL_RELOAD_WEAPON_TIME_MODIFIER)
						{
							string_2 = dictionary_1[needData.SlotType_0];
						}
					}
				}
			}
			else
			{
				string_ = LocalizationStorage.Get.Term(string.Format("ui.skill.{0}.title", item3.Key.ToString()));
			}
			if (item3.Key == SkillId.SKILL_ARMOR_ABSORB)
			{
				string modifierTextKey = GetModifierTextKey(item3.Key, 0, item3.Value.Single_0);
				string_ = Localizer.Get(string.Format("ui.skill.{0}.slot.{1}.{2}", item3.Key.ToString(), artikulData_0.SlotType_0.ToString(), modifierTextKey));
			}
			float float_ = 0f;
			if (item3.Key == SkillId.SKILL_ARMOR)
			{
				float_ = item3.Value.Single_0;
			}
			CreateSkillView(string_, string_2, float_, dictionary_2.ContainsKey(item3.Key) && dictionary_2[item3.Key]);
		}
	}

	private void CreateSkillView(string string_0, string string_1, float float_1, bool bool_0)
	{
		GameObject gameObject = NGUITools.AddChild(skillsContainer.gameObject, (float_1 == 0f) ? skillBigTemplate.gameObject : skillBigTemplateVal.gameObject);
		gameObject.name = string.Format("{0:00}", int_1);
		ShopItemSkillBig component = gameObject.GetComponent<ShopItemSkillBig>();
		component.Init(string_0, string_1, float_1, bool_0);
		Vector3 localPosition = gameObject.transform.localPosition;
		localPosition.x += float_0 * (float)int_1 + (float_0 - (float)int_0) * 0.5f;
		gameObject.transform.localPosition = localPosition;
		int_1++;
		NGUITools.SetActive(gameObject, true);
	}

	public static string GetModifierTextKey(SkillId skillId_0, int int_2, float float_1)
	{
		if (skillId_0 == SkillId.SKILL_ARMOR_ABSORB)
		{
			if (float_1 >= 0.01f && float_1 <= 0.3f)
			{
				return "LOW";
			}
			if (float_1 >= 0.31f && float_1 <= 0.45f)
			{
				return "MIDDLE";
			}
			if (float_1 >= 0.46f && float_1 <= 0.6f)
			{
				return "HIGH";
			}
			if (float_1 > 0.61f && float_1 <= 0.85f)
			{
				return "GREAT";
			}
			if (float_1 > 0.86f && float_1 <= 1f)
			{
				return "LEGENDARY";
			}
		}
		return string.Empty;
	}
}
