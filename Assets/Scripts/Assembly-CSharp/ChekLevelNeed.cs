public static class ChekLevelNeed
{
	public static bool Check(NeedData needData_0)
	{
		int userLevel = UserController.UserController_0.GetUserLevel();
		if (needData_0.Int32_0 != 0 && userLevel < needData_0.Int32_0)
		{
			return false;
		}
		if (needData_0.Int32_1 != 0 && userLevel > needData_0.Int32_1)
		{
			return false;
		}
		return true;
	}
}
