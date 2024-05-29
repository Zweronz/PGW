using System.Collections.Generic;
using UnityEngine;

public class DominationController : MonoBehaviour
{
	public enum TypeDomination
	{
		NONE = 0,
		VILLIAN3 = 1,
		VILLIAN4 = 2,
		VILLIAN5 = 3,
		FIRST_BLOOD = 4,
		MULTY_KILL2 = 5,
		MULTY_KILL3 = 6,
		MULTY_KILL4 = 7,
		MULTY_KILL5 = 8,
		MULTY_KILL6 = 9,
		MULTY_KILL10 = 10,
		MULTY_KILL20 = 11,
		MULTY_KILL50 = 12,
		FLAG_KILL = 13,
		TOUCHDOWN1 = 14,
		TOUCHDOWN2 = 15,
		TOUCHDOWN3 = 16,
		NOVILLIAN = 17
	}

	private const int int_0 = 4;

	public UILabel KillerNickLabel;

	public UILabel VictimNickLabel;

	public UISprite activeSprite;

	public UILabel dominationLabel;

	public UIWidget domination;

	private TypeDomination typeDomination_0;

	private TypeDomination typeDomination_1;

	public void SetItem(string string_0, Color color_0, string string_1, Color color_1, TypeDomination typeDomination_2)
	{
		switch (typeDomination_2)
		{
		case TypeDomination.NONE:
			return;
		case TypeDomination.MULTY_KILL2:
		case TypeDomination.MULTY_KILL3:
		case TypeDomination.MULTY_KILL4:
		case TypeDomination.MULTY_KILL5:
		case TypeDomination.MULTY_KILL6:
			if (typeDomination_0 >= typeDomination_2)
			{
				return;
			}
			typeDomination_0 = typeDomination_2;
			break;
		}
		if (typeDomination_2 >= TypeDomination.TOUCHDOWN1 && typeDomination_2 <= TypeDomination.TOUCHDOWN3)
		{
			if (typeDomination_1 >= typeDomination_2)
			{
				return;
			}
			typeDomination_1 = typeDomination_2;
		}
		NGUITools.SetActive(domination.gameObject, true);
		int num = ((!GraphicsController.GraphicsController_0.Boolean_1) ? 7 : 16);
		if (string_0.Length > num)
		{
			string_0 = string_0.Substring(0, num);
		}
		KillerNickLabel.String_0 = string_0;
		KillerNickLabel.Color_0 = color_0;
		VictimNickLabel.String_0 = string_1;
		VictimNickLabel.Color_0 = color_1;
		switch (typeDomination_2)
		{
		case TypeDomination.VILLIAN3:
			activeSprite.String_0 = "villain3";
			dominationLabel.String_0 = LocalizationStorage.Get.Term("list_of_kills.domination.type_3");
			break;
		case TypeDomination.VILLIAN4:
			activeSprite.String_0 = "villain4";
			dominationLabel.String_0 = LocalizationStorage.Get.Term("list_of_kills.domination.type_2");
			break;
		case TypeDomination.VILLIAN5:
			activeSprite.String_0 = "villain5";
			dominationLabel.String_0 = LocalizationStorage.Get.Term("list_of_kills.domination.type_1");
			break;
		case TypeDomination.FIRST_BLOOD:
			activeSprite.String_0 = "badge_fblood_d";
			dominationLabel.String_0 = LocalizationStorage.Get.Term("hud.message.first_blood");
			break;
		case TypeDomination.MULTY_KILL2:
			activeSprite.String_0 = "badge_1_d";
			dominationLabel.String_0 = LocalizationStorage.Get.Term("hud.message.multykill2");
			break;
		case TypeDomination.MULTY_KILL3:
			activeSprite.String_0 = "badge_2_d";
			dominationLabel.String_0 = LocalizationStorage.Get.Term("hud.message.multykill3");
			break;
		case TypeDomination.MULTY_KILL4:
			activeSprite.String_0 = "badge_3_d";
			dominationLabel.String_0 = LocalizationStorage.Get.Term("hud.message.multykill4");
			break;
		case TypeDomination.MULTY_KILL5:
			activeSprite.String_0 = "badge_4_d";
			dominationLabel.String_0 = LocalizationStorage.Get.Term("hud.message.multykill5");
			break;
		case TypeDomination.MULTY_KILL6:
			activeSprite.String_0 = "badge_5_d";
			dominationLabel.String_0 = LocalizationStorage.Get.Term("hud.message.multykill6");
			break;
		case TypeDomination.MULTY_KILL10:
			activeSprite.String_0 = "badge_9_d";
			dominationLabel.String_0 = LocalizationStorage.Get.Term("hud.message.multykill10");
			break;
		case TypeDomination.MULTY_KILL20:
			activeSprite.String_0 = "badge_19_d";
			dominationLabel.String_0 = LocalizationStorage.Get.Term("hud.message.multykill20");
			break;
		case TypeDomination.MULTY_KILL50:
			activeSprite.String_0 = "badge_49_d";
			dominationLabel.String_0 = LocalizationStorage.Get.Term("hud.message.multykill50");
			break;
		case TypeDomination.FLAG_KILL:
			activeSprite.String_0 = "ksrflagkill_badge_d";
			dominationLabel.String_0 = LocalizationStorage.Get.Term("hud.message.ksflagkill");
			break;
		case TypeDomination.TOUCHDOWN1:
			activeSprite.String_0 = "kstouchdown_badge_d";
			dominationLabel.String_0 = LocalizationStorage.Get.Term("hud.message.kstouchdown");
			break;
		case TypeDomination.TOUCHDOWN2:
			activeSprite.String_0 = "kstouchdown2_badge_d";
			dominationLabel.String_0 = LocalizationStorage.Get.Term("hud.message.kstouchdown2");
			break;
		case TypeDomination.TOUCHDOWN3:
			activeSprite.String_0 = "kstouchdown3_badge_d";
			dominationLabel.String_0 = LocalizationStorage.Get.Term("hud.message.kstouchdown3");
			break;
		case TypeDomination.NOVILLIAN:
			activeSprite.String_0 = string.Empty;
			dominationLabel.String_0 = string.Empty;
			break;
		}
		activeSprite.Int32_0 = 50;
		int num2 = (int)KillerNickLabel.Vector2_3.x;
		int num3 = Mathf.RoundToInt(KillerNickLabel.gameObject.transform.localPosition.x);
		Vector3 position = activeSprite.transform.position;
		position.x = num3 + num2 + 4;
		activeSprite.transform.position = position;
		num2 += (int)((float)activeSprite.Int32_0 * activeSprite.transform.localScale.x) + 4;
		position = dominationLabel.gameObject.transform.localPosition;
		position.x = num3 + num2 + 4;
		dominationLabel.transform.localPosition = position;
		num2 += (int)dominationLabel.Vector2_3.x + 4;
		position = VictimNickLabel.gameObject.transform.localPosition;
		position.x = num3 + num2 + 4;
		VictimNickLabel.transform.localPosition = position;
	}

	public void HideItem()
	{
		NGUITools.SetActive(domination.gameObject, false);
	}

	public static TypeDomination GetDominationLevel(int int_1)
	{
		if (int_1 >= 20)
		{
			return TypeDomination.VILLIAN5;
		}
		if (int_1 >= 10)
		{
			return TypeDomination.VILLIAN4;
		}
		if (int_1 >= 7)
		{
			return TypeDomination.VILLIAN3;
		}
		return TypeDomination.NONE;
	}

	public static void GetDominationColor(string string_0, out Color color_0, out Color color_1)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		List<string> list3 = new List<string>();
		List<string> list4 = new List<string>();
		TypeCommand typeCommand = TypeCommand.None;
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			NetworkStartTable component = gameObject.GetComponent<NetworkStartTable>();
			string string_ = component.String_5;
			if (gameObject.Equals(WeaponManager.weaponManager_0.myTable))
			{
				typeCommand = component.PlayerCommandController_0.TypeCommand_1;
			}
			if (component.PlayerCommandController_0.TypeCommand_1 == TypeCommand.Diggers)
			{
				list.Add(string_);
			}
			else if (component.PlayerCommandController_0.TypeCommand_1 == TypeCommand.Kritters)
			{
				list2.Add(string_);
			}
		}
		list3 = ((typeCommand != TypeCommand.Diggers) ? list2 : list);
		list4 = ((typeCommand != TypeCommand.Diggers) ? list : list2);
		color_0 = ((!list3.Contains(string_0)) ? KillChatUIController.color_1 : KillChatUIController.color_0);
		color_1 = ((!list4.Contains(string_0)) ? KillChatUIController.color_1 : KillChatUIController.color_0);
	}
}
