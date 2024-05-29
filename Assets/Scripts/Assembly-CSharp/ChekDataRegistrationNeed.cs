public static class ChekDataRegistrationNeed
{
	public static bool Check(NeedData needData_0)
	{
		double double_ = UsersData.UsersData_0.UserData_0.user_0.double_0;
		if (needData_0.Double_2 != 0.0 && double_ < needData_0.Double_2)
		{
			return false;
		}
		if (needData_0.Double_3 != 0.0 && double_ > needData_0.Double_3)
		{
			return false;
		}
		return true;
	}
}
