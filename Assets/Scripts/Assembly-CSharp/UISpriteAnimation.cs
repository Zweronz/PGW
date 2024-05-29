using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UISprite))]
public class UISpriteAnimation : MonoBehaviour
{
	[SerializeField]
	protected int int_0 = 30;

	[SerializeField]
	protected string string_0 = string.Empty;

	[SerializeField]
	protected bool bool_0 = true;

	[SerializeField]
	protected bool bool_1 = true;

	protected UISprite uisprite_0;

	protected float float_0;

	protected int int_1;

	protected bool bool_2 = true;

	protected List<string> list_0 = new List<string>();

	public int Int32_0
	{
		get
		{
			return list_0.Count;
		}
	}

	public int Int32_1
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

	public string String_0
	{
		get
		{
			return string_0;
		}
		set
		{
			if (string_0 != value)
			{
				string_0 = value;
				RebuildSpriteList();
			}
		}
	}

	public bool Boolean_0
	{
		get
		{
			return bool_0;
		}
		set
		{
			bool_0 = value;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return bool_2;
		}
	}

	protected virtual void Start()
	{
		RebuildSpriteList();
	}

	protected virtual void Update()
	{
		if (!bool_2 || list_0.Count <= 1 || !Application.isPlaying || !((float)int_0 > 0f))
		{
			return;
		}
		float_0 += RealTime.Single_1;
		float num = 1f / (float)int_0;
		if (!(num < float_0))
		{
			return;
		}
		float_0 = ((!(num > 0f)) ? 0f : (float_0 - num));
		if (++int_1 >= list_0.Count)
		{
			int_1 = 0;
			bool_2 = Boolean_0;
		}
		if (bool_2)
		{
			uisprite_0.String_0 = list_0[int_1];
			if (bool_1)
			{
				uisprite_0.MakePixelPerfect();
			}
		}
	}

	public void RebuildSpriteList()
	{
		if (uisprite_0 == null)
		{
			uisprite_0 = GetComponent<UISprite>();
		}
		list_0.Clear();
		if (!(uisprite_0 != null) || !(uisprite_0.UIAtlas_0 != null))
		{
			return;
		}
		List<UISpriteData> list = uisprite_0.UIAtlas_0.List_0;
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			UISpriteData uISpriteData = list[i];
			if (string.IsNullOrEmpty(string_0) || uISpriteData.name.StartsWith(string_0))
			{
				list_0.Add(uISpriteData.name);
			}
		}
		list_0.Sort();
	}

	public void Reset()
	{
		bool_2 = true;
		int_1 = 0;
		if (uisprite_0 != null && list_0.Count > 0)
		{
			uisprite_0.String_0 = list_0[int_1];
			if (bool_1)
			{
				uisprite_0.MakePixelPerfect();
			}
		}
	}
}
