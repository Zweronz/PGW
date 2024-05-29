using System;

public class ShopWeaponCompareParam : ShopWeaponParam
{
	public UILabel uilabel_0;

	public UIProgressBar uiprogressBar_0;

	public void Init(string string_0, string string_1, float float_0, float float_1, float float_2)
	{
		Init(string_0, string_1, float_0, float_2);
		uilabel_0.String_0 = ((int)Math.Ceiling(float_1)).ToString();
		if (float_1 > float_2)
		{
			float_1 = float_2;
		}
		uiprogressBar_0.Single_0 = float_1 / float_2;
	}
}
