using System;
using UnityEngine;

[Serializable]
public class BMSymbol
{
	public string sequence;

	public string spriteName;

	private UISpriteData uispriteData_0;

	private bool bool_0;

	private int int_0;

	private int int_1;

	private int int_2;

	private int int_3;

	private int int_4;

	private int int_5;

	private Rect rect_0;

	public int Int32_0
	{
		get
		{
			if (int_0 == 0)
			{
				int_0 = sequence.Length;
			}
			return int_0;
		}
	}

	public int Int32_1
	{
		get
		{
			return int_1;
		}
	}

	public int Int32_2
	{
		get
		{
			return int_2;
		}
	}

	public int Int32_3
	{
		get
		{
			return int_3;
		}
	}

	public int Int32_4
	{
		get
		{
			return int_4;
		}
	}

	public int Int32_5
	{
		get
		{
			return int_5;
		}
	}

	public Rect Rect_0
	{
		get
		{
			return rect_0;
		}
	}

	public void MarkAsChanged()
	{
		bool_0 = false;
	}

	public bool Validate(UIAtlas uiatlas_0)
	{
		if (uiatlas_0 == null)
		{
			return false;
		}
		if (!bool_0)
		{
			if (string.IsNullOrEmpty(spriteName))
			{
				return false;
			}
			uispriteData_0 = ((!(uiatlas_0 != null)) ? null : uiatlas_0.GetSprite(spriteName));
			if (uispriteData_0 != null)
			{
				Texture texture_ = uiatlas_0.Texture_0;
				if (texture_ == null)
				{
					uispriteData_0 = null;
				}
				else
				{
					rect_0 = new Rect(uispriteData_0.x, uispriteData_0.y, uispriteData_0.width, uispriteData_0.height);
					rect_0 = NGUIMath.ConvertToTexCoords(rect_0, texture_.width, texture_.height);
					int_1 = uispriteData_0.paddingLeft;
					int_2 = uispriteData_0.paddingTop;
					int_3 = uispriteData_0.width;
					int_4 = uispriteData_0.height;
					int_5 = uispriteData_0.width + (uispriteData_0.paddingLeft + uispriteData_0.paddingRight);
					bool_0 = true;
				}
			}
		}
		return uispriteData_0 != null;
	}
}
