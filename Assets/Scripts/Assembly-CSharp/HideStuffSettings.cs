public sealed class HideStuffSettings
{
	public static string string_0 = "SHOW_ARMOR";

	public static string string_1 = "SHOW_BOOTS";

	public static string string_2 = "SHOW_HAT";

	public static string string_3 = "SHOW_CAPE";

	private static HideStuffSettings hideStuffSettings_0;

	public static HideStuffSettings HideStuffSettings_0
	{
		get
		{
			if (hideStuffSettings_0 == null)
			{
				hideStuffSettings_0 = new HideStuffSettings();
			}
			return hideStuffSettings_0;
		}
	}

	private HideStuffSettings()
	{
	}

	public bool GetShowArmor()
	{
		return SharedSettings.SharedSettings_0.GetValue(string_0, 1) == 1;
	}

	public void SetShowArmor(bool bool_0)
	{
		SharedSettings.SharedSettings_0.SetValue(string_0, bool_0 ? 1 : 0, true);
		UsersData.Dispatch(UsersData.EventType.RESET_VISIBLE_ARMOR);
	}

	public bool GetShowBoots()
	{
		return SharedSettings.SharedSettings_0.GetValue(string_1, 1) == 1;
	}

	public void SetShowBoots(bool bool_0)
	{
		SharedSettings.SharedSettings_0.SetValue(string_1, bool_0 ? 1 : 0, true);
		UsersData.Dispatch(UsersData.EventType.RESET_VISIBLE_BOOTS);
	}

	public bool GetShowHat()
	{
		return SharedSettings.SharedSettings_0.GetValue(string_2, 1) == 1;
	}

	public void SetShowHat(bool bool_0)
	{
		SharedSettings.SharedSettings_0.SetValue(string_2, bool_0 ? 1 : 0, true);
		UsersData.Dispatch(UsersData.EventType.RESET_VISIBLE_HAT);
	}

	public bool GetShowCape()
	{
		return SharedSettings.SharedSettings_0.GetValue(string_3, 1) == 1;
	}

	public void SetShowCape(bool bool_0)
	{
		SharedSettings.SharedSettings_0.SetValue(string_3, bool_0 ? 1 : 0, true);
		UsersData.Dispatch(UsersData.EventType.RESET_VISIBLE_CAPE);
	}

	public void Clear()
	{
		SetShowArmor(true);
		SetShowHat(true);
		SetShowBoots(true);
		SetShowBoots(true);
	}
}
