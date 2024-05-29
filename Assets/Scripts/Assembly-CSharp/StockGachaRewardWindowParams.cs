using engine.unity;

public sealed class StockGachaRewardWindowParams : WindowShowParameters
{
	public ApplyBonusNetworkCommand applyBonusNetworkCommand_0;

	public ActionData actionData_0;

	public StockGachaRewardWindowParams(ApplyBonusNetworkCommand applyBonusNetworkCommand_1, ActionData actionData_1)
	{
		applyBonusNetworkCommand_0 = applyBonusNetworkCommand_1;
		actionData_0 = actionData_1;
	}
}
