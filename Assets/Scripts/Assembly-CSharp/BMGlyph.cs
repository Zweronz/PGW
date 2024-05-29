using System;
using System.Collections.Generic;

[Serializable]
public class BMGlyph
{
	public int index;

	public int x;

	public int y;

	public int width;

	public int height;

	public int offsetX;

	public int offsetY;

	public int advance;

	public int channel;

	public List<int> kerning;

	public int GetKerning(int int_0)
	{
		if (kerning != null && int_0 != 0)
		{
			int i = 0;
			for (int count = kerning.Count; i < count; i += 2)
			{
				if (kerning[i] == int_0)
				{
					return kerning[i + 1];
				}
			}
		}
		return 0;
	}

	public void SetKerning(int int_0, int int_1)
	{
		if (kerning == null)
		{
			kerning = new List<int>();
		}
		int num = 0;
		while (true)
		{
			if (num < kerning.Count)
			{
				if (kerning[num] == int_0)
				{
					break;
				}
				num += 2;
				continue;
			}
			kerning.Add(int_0);
			kerning.Add(int_1);
			return;
		}
		kerning[num + 1] = int_1;
	}

	public void Trim(int int_0, int int_1, int int_2, int int_3)
	{
		int num = x + width;
		int num2 = y + height;
		if (x < int_0)
		{
			int num3 = int_0 - x;
			x += num3;
			width -= num3;
			offsetX += num3;
		}
		if (y < int_1)
		{
			int num4 = int_1 - y;
			y += num4;
			height -= num4;
			offsetY += num4;
		}
		if (num > int_2)
		{
			width -= num - int_2;
		}
		if (num2 > int_3)
		{
			height -= num2 - int_3;
		}
	}
}
