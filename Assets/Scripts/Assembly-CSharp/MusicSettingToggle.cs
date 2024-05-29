public class MusicSettingToggle : BaseSettingToggle
{
	protected override bool isOn()
	{
		return MenuBackgroundMusic.menuBackgroundMusic_0.Boolean_0;
	}

	protected override void onClick()
	{
		if (MenuBackgroundMusic.menuBackgroundMusic_0.Boolean_0 != base.Boolean_0)
		{
			Storager.SetBool(Defs.String_1, base.Boolean_0);
			Storager.Save();
			MenuBackgroundMusic.menuBackgroundMusic_0.ChangePlayMusicState(base.Boolean_0);
		}
	}
}
