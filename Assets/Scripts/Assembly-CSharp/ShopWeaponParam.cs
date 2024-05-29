using System;
using UnityEngine;

public class ShopWeaponParam : MonoBehaviour
{
	public UISprite paramIcon;

	public UILabel paramTitle;

	public UILabel paramValue;

	public UIProgressBar paramValueBar;

	public void Init(string string_0, string string_1, float float_0, float float_1)
	{
		paramIcon.String_0 = string_0;
		paramTitle.String_0 = string_1;
		paramValue.String_0 = ((int)Math.Ceiling(float_0)).ToString();
		if (float_0 > float_1)
		{
			float_0 = float_1;
		}
		paramValueBar.Single_0 = float_0 / float_1;
	}
}
