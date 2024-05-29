using engine.unity;

public static class CheckModeId
{
	public static bool Check(NeedData needData_0)
	{
		ModeData modeData_ = FightOfflineController.FightOfflineController_0.ModeData_0;
		if (modeData_ == null)
		{
			modeData_ = MonoSingleton<FightController>.Prop_0.ModeData_0;
		}
		if (modeData_ == null)
		{
			return true;
		}
		return modeData_.Int32_0 == needData_0.Int32_6;
	}
}
