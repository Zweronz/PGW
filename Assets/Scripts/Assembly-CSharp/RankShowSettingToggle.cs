public class RankShowSettingToggle : BaseSettingToggle
{
	protected override bool isOn()
	{
		return RankCupController.Boolean_0;
	}

	protected override void onClick()
	{
		RankCupController.Boolean_0 = base.Boolean_0;
	}
}
