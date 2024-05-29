using System.Collections.Generic;
using UnityEngine;
using engine.events;
using engine.helpers;
using engine.unity;

public class TooltipWeaponComparer : MonoBehaviour
{
	public GameObject WeaponContainer;

	public ShopItemSkillsPanel skillsPanel;

	public UILabel letalityTitle;

	public UILabel letality;

	public UILabel letalityDiff;

	public UISprite letalityDiffSprite;

	public UILabel fireRateTitle;

	public UILabel fireRate;

	public UILabel fireRateDiff;

	public UISprite fireRateDiffSprite;

	public UILabel capacityTitle;

	public UILabel capacity;

	public UILabel capacityDiff;

	public UISprite capacityDiffSprite;

	public UILabel mobilityTitle;

	public UILabel mobility;

	public UILabel mobilityDiff;

	public UISprite mobilityDiffSprite;

	public UILabel liveTimeTitleLabel;

	public UILabel liveTimeLabel;

	public GameObject WearContainer;

	public ShopItemSkillsBigPanel wearPanel;

	private UserArtikul userArtikul_0;

	private string string_0;

	private string string_1;

	private string string_2;

	private string string_3;

	private ArtikulData artikulData_0;

	public void SetWeaponContent(int int_0, int int_1)
	{
		WeaponContainer.gameObject.SetActive(true);
		WearContainer.gameObject.SetActive(false);
		WeaponData weapon = WeaponController.WeaponController_0.GetWeapon(int_0);
		WeaponData weaponData = ((int_0 != int_1) ? WeaponController.WeaponController_0.GetWeapon(int_1) : null);
		int num = 0;
		int num2 = 0;
		num = Mathf.CeilToInt(weapon.Single_17);
		num2 = ((weaponData == null) ? Mathf.CeilToInt(weapon.Single_17) : Mathf.CeilToInt(weaponData.Single_17));
		letalityTitle.String_0 = LocalizationStorage.Get.Term("ui.shop.weapon_dps");
		letality.String_0 = num.ToString();
		int num3 = num - num2;
		letalityDiffSprite.String_0 = ((num3 > 0) ? "up" : ((num3 >= 0) ? string.Empty : "down"));
		letalityDiff.String_0 = ((num3 != 0) ? (((num3 <= 0) ? string.Empty : "+") + Mathf.CeilToInt(num3)) : string.Empty);
		num = Mathf.CeilToInt(weapon.Single_19);
		num2 = ((weaponData == null) ? Mathf.CeilToInt(weapon.Single_19) : Mathf.CeilToInt(weaponData.Single_19));
		fireRateTitle.String_0 = LocalizationStorage.Get.Term("ui.shop.weapon_rate");
		fireRate.String_0 = num.ToString();
		num3 = num - num2;
		fireRateDiffSprite.String_0 = ((num3 > 0) ? "up" : ((num3 >= 0) ? string.Empty : "down"));
		fireRateDiff.String_0 = ((num3 != 0) ? (((num3 <= 0) ? string.Empty : "+") + Mathf.CeilToInt(num3)) : string.Empty);
		num = Mathf.CeilToInt(weapon.Int32_2);
		num2 = ((weaponData == null) ? Mathf.CeilToInt(weapon.Int32_2) : Mathf.CeilToInt(weaponData.Int32_2));
		capacityTitle.String_0 = LocalizationStorage.Get.Term("ui.shop.weapon_capacity");
		capacity.String_0 = num.ToString();
		num3 = num - num2;
		capacityDiffSprite.String_0 = ((num3 > 0) ? "up" : ((num3 >= 0) ? string.Empty : "down"));
		capacityDiff.String_0 = ((num3 != 0) ? (((num3 <= 0) ? string.Empty : "+") + Mathf.CeilToInt(num3)) : string.Empty);
		num = Mathf.CeilToInt(weapon.Int32_5);
		num2 = ((weaponData == null) ? Mathf.CeilToInt(weapon.Int32_5) : Mathf.CeilToInt(weaponData.Int32_5));
		mobilityTitle.String_0 = LocalizationStorage.Get.Term("ui.shop.weapon_mobility");
		mobility.String_0 = num.ToString();
		num3 = num - num2;
		mobilityDiffSprite.String_0 = ((num3 > 0) ? "up" : ((num3 >= 0) ? string.Empty : "down"));
		mobilityDiff.String_0 = ((num3 != 0) ? (((num3 <= 0) ? string.Empty : "+") + Mathf.CeilToInt(num3)) : string.Empty);
		int num4 = 0;
		if (weapon.ArtikulData_0.Int32_5 > 0)
		{
			num4 = 16;
			userArtikul_0 = UserController.UserController_0.GetUserArtikulByArtikulId(weapon.ArtikulData_0.Int32_0);
			artikulData_0 = weapon.ArtikulData_0;
			liveTimeTitleLabel.gameObject.transform.parent.gameObject.SetActive(true);
			UpdateLiveTime();
		}
		else
		{
			liveTimeTitleLabel.gameObject.transform.parent.gameObject.SetActive(false);
		}
		RepositionStatAndSkills(num4);
		int num5 = skillsPanel.Init(ArtikulController.ArtikulController_0.GetArtikul(int_0));
		SetPosition(209, 88 + num5 * 16 + num4);
		WeaponContainer.transform.GetChild(1).GetComponent<UISprite>().Int32_0 = ((weaponData != null || weapon.ArtikulData_0.Int32_5 != 0) ? 206 : 170);
		WeaponContainer.transform.GetChild(1).GetComponent<UISprite>().Int32_1 = 81 + num5 * 16 + num4;
	}

	private void RepositionStatAndSkills(int int_0)
	{
		Vector3 localPosition = letalityDiffSprite.gameObject.transform.parent.transform.localPosition;
		localPosition.y = -int_0;
		letalityDiffSprite.gameObject.transform.parent.transform.localPosition = localPosition;
		localPosition = fireRateDiffSprite.gameObject.transform.parent.transform.localPosition;
		localPosition.y = -20 - int_0;
		fireRateDiffSprite.gameObject.transform.parent.transform.localPosition = localPosition;
		localPosition = capacityDiffSprite.gameObject.transform.parent.transform.localPosition;
		localPosition.y = -40 - int_0;
		capacityDiffSprite.gameObject.transform.parent.transform.localPosition = localPosition;
		localPosition = mobilityDiffSprite.gameObject.transform.parent.transform.localPosition;
		localPosition.y = -60 - int_0;
		mobilityDiffSprite.gameObject.transform.parent.transform.localPosition = localPosition;
		localPosition = skillsPanel.gameObject.transform.localPosition;
		localPosition.y = -81 - int_0;
		skillsPanel.gameObject.transform.localPosition = localPosition;
	}

	private void UpdateLiveTime()
	{
		string_0 = Localizer.Get("ui.day.mini");
		string_1 = Localizer.Get("ui.hour.mini");
		string_2 = Localizer.Get("ui.min.mini");
		string_3 = Localizer.Get("ui.sec.mini");
		if (userArtikul_0 != null && (userArtikul_0 == null || userArtikul_0.int_1 != 0))
		{
			if (userArtikul_0.double_0 > 0.0 && userArtikul_0.ArtikulData_0.Int32_5 > 0)
			{
				liveTimeTitleLabel.String_0 = Localizer.Get("ui.shop.left");
				liveTimeLabel.String_0 = Utility.GetLocalizedTime((int)(userArtikul_0.double_0 + (double)userArtikul_0.int_3 + (double)userArtikul_0.ArtikulData_0.Int32_5 - Utility.Double_0), string_0, string_1, string_2, string_3);
				if (!DependSceneEvent<MainUpdateOneSecond>.Contains(TickOneSecond))
				{
					DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(TickOneSecond);
				}
			}
		}
		else if (artikulData_0.Int32_5 > 0)
		{
			liveTimeTitleLabel.String_0 = Localizer.Get("ui.shop.rent_for");
			liveTimeLabel.String_0 = Utility.GetLocalizedTime(artikulData_0.Int32_5, string_0, string_1, string_2, string_3, false);
			if (DependSceneEvent<MainUpdateOneSecond>.Contains(TickOneSecond))
			{
				DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(TickOneSecond);
			}
		}
	}

	private void TickOneSecond()
	{
		if (userArtikul_0 != null)
		{
			liveTimeLabel.String_0 = Utility.GetLocalizedTime((int)(userArtikul_0.double_0 + (double)userArtikul_0.int_3 + (double)userArtikul_0.ArtikulData_0.Int32_5 - Utility.Double_0), string_0, string_1, string_2, string_3);
		}
	}

	public void SetWearContent(int int_0)
	{
		int num = 0;
		if (WearController.WearController_0.GetWear(int_0).ArtikulData_0.Dictionary_0 != null)
		{
			foreach (KeyValuePair<SkillId, SkillData> item in WearController.WearController_0.GetWear(int_0).ArtikulData_0.Dictionary_0)
			{
				if ((item.Key != SkillId.SKILL_ARMOR_ABSORB || WearController.WearController_0.GetWear(int_0).ArtikulData_0.SlotType_0 == SlotType.SLOT_WEAR_ARMOR) && ShopItemSkillsBigPanel.dictionary_0.ContainsKey(item.Key))
				{
					num++;
				}
			}
		}
		wearPanel.GetComponent<UIWidget>().Int32_0 = 120 * num;
		wearPanel.Init(WearController.WearController_0.GetWear(int_0).ArtikulData_0);
		if (num > 0)
		{
			SetPosition(120 * num, 50);
		}
		WeaponContainer.gameObject.SetActive(false);
		WearContainer.gameObject.SetActive(num > 0);
	}

	private void SetPosition(int int_0, int int_1)
	{
		Vector2 cursorSize = CursorPGW.CursorPGW_0.GetCursorSize();
		Vector3 localPosition = Input.mousePosition * ScreenController.ScreenController_0.Single_0;
		localPosition.x += cursorSize.x + 10f;
		int num = (int)((float)Screen.width * ScreenController.ScreenController_0.Single_0);
		if ((int)(localPosition.x + (float)int_0 + 10f) >= num)
		{
			localPosition.x -= (float)int_0 + cursorSize.x + 20f;
		}
		if ((int)(localPosition.y - (float)int_1) <= 0)
		{
			localPosition.y += int_1;
		}
		base.transform.localPosition = localPosition;
		Invoke("ActivateTooltip", 0.5f);
	}

	public void DeActivateTooltip()
	{
		CancelInvoke("ActivateTooltip");
		NGUITools.SetActive(base.gameObject, false);
		DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(TickOneSecond);
	}

	public void ActivateTooltip()
	{
		if (!base.gameObject.activeSelf)
		{
			NGUITools.SetActive(base.gameObject, true);
		}
	}
}
