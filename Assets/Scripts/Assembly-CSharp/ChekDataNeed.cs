using engine.helpers;

public static class ChekDataNeed
{
	public static bool Check(NeedData needData_0)
	{
		double double_ = Utility.Double_0;
		if (needData_0.Double_0 != 0.0 && double_ < needData_0.Double_0)
		{
			return false;
		}
		if (needData_0.Double_1 != 0.0 && double_ > needData_0.Double_1)
		{
			return false;
		}
		return true;
	}
}
