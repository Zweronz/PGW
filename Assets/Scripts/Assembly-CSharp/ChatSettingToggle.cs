public class ChatSettingToggle : BaseSettingToggle
{
	protected override bool isOn()
	{
		return Defs.Boolean_1;
	}

	protected override void onClick()
	{
		SettingsController.SwitchChatSetting(base.Boolean_0);
	}
}
