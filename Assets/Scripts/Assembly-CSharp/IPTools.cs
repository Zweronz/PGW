public static class IPTools
{
	public static int GetSelectedIndexAfterRemoveElement(int int_0, int int_1, int int_2)
	{
		int num = int_1;
		if (int_0 <= int_1)
		{
			num = ((int_1 != 0) ? (int_1 - 1) : (int_2 - 1));
			if (num < 0)
			{
				num = 0;
			}
		}
		return num;
	}

	public static void NormalizeWidget(UIWidget uiwidget_0, float float_0)
	{
		uiwidget_0.MakePixelPerfect();
		float num = ((uiwidget_0.Int32_1 < uiwidget_0.Int32_0) ? (float_0 / (float)uiwidget_0.Int32_0) : (float_0 / (float)uiwidget_0.Int32_1));
		uiwidget_0.Int32_0 = (int)((float)uiwidget_0.Int32_0 * num);
		uiwidget_0.Int32_1 = (int)((float)uiwidget_0.Int32_1 * num);
	}
}
