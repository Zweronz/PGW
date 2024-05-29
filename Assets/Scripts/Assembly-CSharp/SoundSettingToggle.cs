public class SoundSettingToggle : BaseSettingToggle
{
	protected override bool isOn()
	{
		return Defs.Boolean_0;
	}

	protected override void onClick()
	{
		if (Defs.Boolean_0 != base.Boolean_0)
		{
			Defs.Boolean_0 = base.Boolean_0;
			Storager.SetBool(Defs.String_2, Defs.Boolean_0);
			Storager.Save();
		}
	}
}
