public static class ChekUserTierNeed
{
	public static bool Check(NeedData needData_0)
	{
		int userTier = UserController.UserController_0.GetUserTier();
		return userTier == needData_0.Int32_2;
	}
}
