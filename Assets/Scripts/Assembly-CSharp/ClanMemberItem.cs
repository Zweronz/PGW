using UnityEngine;
using engine.helpers;

public sealed class ClanMemberItem : MonoBehaviour
{
	public UILabel placeValue;

	public UISprite placeIcon;

	public UILabel nickLabel;

	public UILabel levelLabel;

	public UILabel scoreLabel;

	public UILabel statusLabel;

	public UISprite[] backs;

	public Color[] statusColors;

	private UserClanMemberData userClanMemberData_0;

	private int int_0;

	public void SetData(UserClanMemberData userClanMemberData_1, int int_1)
	{
		userClanMemberData_0 = userClanMemberData_1;
		int_0 = int_1;
		UpdateItem();
	}

	public void UpdateItem()
	{
		placeValue.String_0 = int_0.ToString();
		placeIcon.String_0 = string.Format("icon_place_{0}", int_0);
		NGUITools.SetActive(placeIcon.gameObject, int_0 > 0 && int_0 < 4);
		nickLabel.String_0 = userClanMemberData_0.string_0;
		levelLabel.String_0 = userClanMemberData_0.int_1.ToString();
		scoreLabel.String_0 = userClanMemberData_0.int_2.ToString();
		statusLabel.Color_0 = statusColors[0];
		statusLabel.String_0 = ((!userClanMemberData_0.bool_0) ? LastTimeToText() : Localizer.Get("window.clan_info.status_online"));
		int num = UserController.UserController_0.UserData_0.user_0.int_0;
		for (int i = 0; i < backs.Length; i++)
		{
			backs[i].String_0 = string.Format("clan_item_bg_{0}_{1}", (userClanMemberData_0.int_0 != num) ? "gray" : "blue", (i % 2 != 0) ? "light" : "dark");
		}
	}

	private string LastTimeToText()
	{
		statusLabel.Color_0 = statusColors[1];
		int num = (int)(Utility.Double_0 - (double)userClanMemberData_0.int_3);
		int num2 = num / 86400;
		int num3 = num % 86400;
		int num4 = num3 / 3600;
		int num5 = num3 % 3600;
		int num6 = num5 / 60;
		if (num2 > 7)
		{
			return Localizer.Get("window.clan_info.status_week");
		}
		if (num2 > 1)
		{
			return Localizer.Get("window.clan_info.status_days");
		}
		if (num2 > 0)
		{
			return Localizer.Get("window.clan_info.status_day");
		}
		if (num4 > 1)
		{
			return string.Format(Localizer.Get("window.clan_info.status_hours"), num4);
		}
		if (num4 > 0)
		{
			return string.Format(Localizer.Get("window.clan_info.status_hour"), num4);
		}
		if (num6 > 10)
		{
			return Localizer.Get("window.clan_info.status_min");
		}
		if (num6 >= 0)
		{
			return Localizer.Get("window.clan_info.status_sec");
		}
		return string.Empty;
	}

	public void OnClick()
	{
		ClanWindow.ClanWindow_0.OnMemberClick(base.gameObject, userClanMemberData_0);
	}

	public void OnNickClick()
	{
		ClanWindow.ClanWindow_0.OnNickClick(base.gameObject, userClanMemberData_0);
	}
}
