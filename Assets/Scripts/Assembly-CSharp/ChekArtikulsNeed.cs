public static class ChekArtikulsNeed
{
	public static bool Check(NeedData needData_0)
	{
		if (needData_0.Int32_4 == 0)
		{
			return false;
		}
		UserArtikul userArtikul = UsersData.UsersData_0.UserData_0.userArtikulStorage_0.SearchUnique(0, needData_0.Int32_4);
		if (userArtikul == null)
		{
			return false;
		}
		return needData_0.Int32_5 == 0 || userArtikul.int_1 >= needData_0.Int32_5;
	}
}
