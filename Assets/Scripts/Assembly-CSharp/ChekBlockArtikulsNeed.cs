public static class ChekBlockArtikulsNeed
{
	public static bool Check(NeedData needData_0)
	{
		if (needData_0.Int32_4 == 0)
		{
			return false;
		}
		UserArtikul userArtikul = UsersData.UsersData_0.UserData_0.userArtikulStorage_0.SearchUnique(0, needData_0.Int32_4);
		return userArtikul == null || userArtikul.int_1 == 0;
	}
}
