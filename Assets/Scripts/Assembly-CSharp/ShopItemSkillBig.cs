using System;

public class ShopItemSkillBig : ShopItemSkill
{
	public UILabel uilabel_0;

	public void Init(string string_0, string string_1, float float_0, bool bool_0)
	{
		Init(string_0, string_1);
		uilabel_0.String_0 = ((float_0 != 0f) ? string.Format("{0}{1}{2}", (!(float_0 > 0f)) ? "-" : "+", (int)Math.Floor(float_0 * (float)((!bool_0) ? 1 : 100)), (!bool_0) ? string.Empty : "%") : string.Empty);
	}
}
