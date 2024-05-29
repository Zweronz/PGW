using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InvGameItem
{
	public enum Quality
	{
		Broken = 0,
		Cursed = 1,
		Damaged = 2,
		Worn = 3,
		Sturdy = 4,
		Polished = 5,
		Improved = 6,
		Crafted = 7,
		Superior = 8,
		Enchanted = 9,
		Epic = 10,
		Legendary = 11,
		_LastDoNotUse = 12
	}

	[SerializeField]
	private int int_0;

	public Quality quality = Quality.Sturdy;

	public int itemLevel = 1;

	private InvBaseItem invBaseItem_0;

	public int Int32_0
	{
		get
		{
			return int_0;
		}
	}

	public InvBaseItem InvBaseItem_0
	{
		get
		{
			if (invBaseItem_0 == null)
			{
				invBaseItem_0 = InvDatabase.FindByID(Int32_0);
			}
			return invBaseItem_0;
		}
	}

	public string String_0
	{
		get
		{
			if (InvBaseItem_0 == null)
			{
				return null;
			}
			return quality.ToString() + " " + InvBaseItem_0.name;
		}
	}

	public float Single_0
	{
		get
		{
			float num = 0f;
			switch (quality)
			{
			case Quality.Broken:
				num = 0f;
				break;
			case Quality.Cursed:
				num = -1f;
				break;
			case Quality.Damaged:
				num = 0.25f;
				break;
			case Quality.Worn:
				num = 0.9f;
				break;
			case Quality.Sturdy:
				num = 1f;
				break;
			case Quality.Polished:
				num = 1.1f;
				break;
			case Quality.Improved:
				num = 1.25f;
				break;
			case Quality.Crafted:
				num = 1.5f;
				break;
			case Quality.Superior:
				num = 1.75f;
				break;
			case Quality.Enchanted:
				num = 2f;
				break;
			case Quality.Epic:
				num = 2.5f;
				break;
			case Quality.Legendary:
				num = 3f;
				break;
			}
			float num2 = (float)itemLevel / 50f;
			return num * Mathf.Lerp(num2, num2 * num2, 0.5f);
		}
	}

	public Color Color_0
	{
		get
		{
			Color result = Color.white;
			switch (quality)
			{
			case Quality.Broken:
				result = new Color(0.4f, 0.2f, 0.2f);
				break;
			case Quality.Cursed:
				result = Color.red;
				break;
			case Quality.Damaged:
				result = new Color(0.4f, 0.4f, 0.4f);
				break;
			case Quality.Worn:
				result = new Color(0.7f, 0.7f, 0.7f);
				break;
			case Quality.Sturdy:
				result = new Color(1f, 1f, 1f);
				break;
			case Quality.Polished:
				result = NGUIMath.HexToColor(3774856959u);
				break;
			case Quality.Improved:
				result = NGUIMath.HexToColor(2480359935u);
				break;
			case Quality.Crafted:
				result = NGUIMath.HexToColor(1325334783u);
				break;
			case Quality.Superior:
				result = NGUIMath.HexToColor(12255231u);
				break;
			case Quality.Enchanted:
				result = NGUIMath.HexToColor(1937178111u);
				break;
			case Quality.Epic:
				result = NGUIMath.HexToColor(2516647935u);
				break;
			case Quality.Legendary:
				result = NGUIMath.HexToColor(4287627519u);
				break;
			}
			return result;
		}
	}

	public InvGameItem(int int_1)
	{
		int_0 = int_1;
	}

	public InvGameItem(int int_1, InvBaseItem invBaseItem_1)
	{
		int_0 = int_1;
		invBaseItem_0 = invBaseItem_1;
	}

	public List<InvStat> CalculateStats()
	{
		List<InvStat> list = new List<InvStat>();
		if (InvBaseItem_0 != null)
		{
			float single_ = Single_0;
			List<InvStat> stats = InvBaseItem_0.stats;
			int i = 0;
			for (int count = stats.Count; i < count; i++)
			{
				InvStat invStat = stats[i];
				int num = Mathf.RoundToInt(single_ * (float)invStat.amount);
				if (num == 0)
				{
					continue;
				}
				bool flag = false;
				int j = 0;
				for (int count2 = list.Count; j < count2; j++)
				{
					InvStat invStat2 = list[j];
					if (invStat2.id == invStat.id && invStat2.modifier == invStat.modifier)
					{
						invStat2.amount += num;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					InvStat invStat3 = new InvStat();
					invStat3.id = invStat.id;
					invStat3.amount = num;
					invStat3.modifier = invStat.modifier;
					list.Add(invStat3);
				}
			}
			list.Sort(InvStat.CompareArmor);
		}
		return list;
	}
}
