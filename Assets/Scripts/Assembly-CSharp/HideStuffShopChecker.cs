using UnityEngine;

public class HideStuffShopChecker : MonoBehaviour
{
	public enum HideStuffType
	{
		ARMOR = 0,
		HAT = 1,
		BOOTS = 2,
		CAPE = 3
	}

	public HideStuffType stuffType;

	private UIPlaySound uiplaySound_0;

	private void Start()
	{
		uiplaySound_0 = GetComponent<UIPlaySound>();
	}

	public void SetVisible(bool bool_0)
	{
		NGUITools.SetActive(base.gameObject, bool_0);
	}

	public void OnClick()
	{
		if (uiplaySound_0 != null && Defs.Boolean_0)
		{
			uiplaySound_0.Play();
		}
		switch (stuffType)
		{
		case HideStuffType.ARMOR:
			HideStuffSettings.HideStuffSettings_0.SetShowArmor(!GetComponent<UIToggle>().Boolean_0);
			break;
		case HideStuffType.HAT:
			HideStuffSettings.HideStuffSettings_0.SetShowHat(!GetComponent<UIToggle>().Boolean_0);
			break;
		case HideStuffType.BOOTS:
			HideStuffSettings.HideStuffSettings_0.SetShowBoots(!GetComponent<UIToggle>().Boolean_0);
			break;
		case HideStuffType.CAPE:
			HideStuffSettings.HideStuffSettings_0.SetShowCape(!GetComponent<UIToggle>().Boolean_0);
			break;
		}
	}
}
