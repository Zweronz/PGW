using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BMFont
{
	[SerializeField]
	private int int_0 = 16;

	[SerializeField]
	private int int_1;

	[SerializeField]
	private int int_2;

	[SerializeField]
	private int int_3;

	[SerializeField]
	private string string_0;

	[SerializeField]
	private List<BMGlyph> list_0 = new List<BMGlyph>();

	private Dictionary<int, BMGlyph> dictionary_0 = new Dictionary<int, BMGlyph>();

	public bool Boolean_0
	{
		get
		{
			return list_0.Count > 0;
		}
	}

	public int Int32_0
	{
		get
		{
			return int_0;
		}
		set
		{
			int_0 = value;
		}
	}

	public int Int32_1
	{
		get
		{
			return int_1;
		}
		set
		{
			int_1 = value;
		}
	}

	public int Int32_2
	{
		get
		{
			return int_2;
		}
		set
		{
			int_2 = value;
		}
	}

	public int Int32_3
	{
		get
		{
			return int_3;
		}
		set
		{
			int_3 = value;
		}
	}

	public int Int32_4
	{
		get
		{
			return Boolean_0 ? list_0.Count : 0;
		}
	}

	public string String_0
	{
		get
		{
			return string_0;
		}
		set
		{
			string_0 = value;
		}
	}

	public List<BMGlyph> List_0
	{
		get
		{
			return list_0;
		}
	}

	public BMGlyph GetGlyph(int int_4, bool bool_0)
	{
		BMGlyph value = null;
		if (dictionary_0.Count == 0)
		{
			int i = 0;
			for (int count = list_0.Count; i < count; i++)
			{
				BMGlyph bMGlyph = list_0[i];
				dictionary_0.Add(bMGlyph.index, bMGlyph);
			}
		}
		if (!dictionary_0.TryGetValue(int_4, out value) && bool_0)
		{
			value = new BMGlyph();
			value.index = int_4;
			list_0.Add(value);
			dictionary_0.Add(int_4, value);
		}
		return value;
	}

	public BMGlyph GetGlyph(int int_4)
	{
		return GetGlyph(int_4, false);
	}

	public void Clear()
	{
		dictionary_0.Clear();
		list_0.Clear();
	}

	public void Trim(int int_4, int int_5, int int_6, int int_7)
	{
		if (!Boolean_0)
		{
			return;
		}
		int i = 0;
		for (int count = list_0.Count; i < count; i++)
		{
			BMGlyph bMGlyph = list_0[i];
			if (bMGlyph != null)
			{
				bMGlyph.Trim(int_4, int_5, int_6, int_7);
			}
		}
	}
}
