using UnityEngine;

public class SpectrumController : HighlightingController
{
	public float float_0 = 200f;

	private readonly int int_0 = 1530;

	private float float_1;

	protected override void AfterUpdate()
	{
		base.AfterUpdate();
		int int_ = (int)float_1;
		Color color_ = new Color(GetColorValue(1020, int_), GetColorValue(0, int_), GetColorValue(510, int_), 1f);
		highlightableObject_0.ConstantOnImmediate(color_);
		float_1 += Time.deltaTime * float_0;
		float_1 %= int_0;
	}

	private float GetColorValue(int int_1, int int_2)
	{
		int num = 0;
		int_2 = (int_2 - int_1) % int_0;
		if (int_2 < 0)
		{
			int_2 += int_0;
		}
		if (int_2 < 255)
		{
			num = int_2;
		}
		if (int_2 >= 255 && int_2 < 765)
		{
			num = 255;
		}
		if (int_2 >= 765 && int_2 < 1020)
		{
			num = 1020 - int_2;
		}
		return (float)num / 255f;
	}
}
