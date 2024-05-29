using System;
using System.Collections.Generic;
using UnityEngine;

public class UIFont : MonoBehaviour
{
	[SerializeField]
	private Material material_0;

	[SerializeField]
	private Rect rect_0 = new Rect(0f, 0f, 1f, 1f);

	[SerializeField]
	private BMFont bmfont_0 = new BMFont();

	[SerializeField]
	private UIAtlas uiatlas_0;

	[SerializeField]
	private UIFont uifont_0;

	[SerializeField]
	private List<BMSymbol> list_0 = new List<BMSymbol>();

	[SerializeField]
	private Font font_0;

	[SerializeField]
	private int int_0 = 16;

	[SerializeField]
	private FontStyle fontStyle_0;

	[NonSerialized]
	private UISpriteData uispriteData_0;

	private int int_1 = -1;

	private int int_2 = -1;

	public BMFont BMFont_0
	{
		get
		{
			return (!(uifont_0 != null)) ? bmfont_0 : uifont_0.BMFont_0;
		}
		set
		{
			if (uifont_0 != null)
			{
				uifont_0.BMFont_0 = value;
			}
			else
			{
				bmfont_0 = value;
			}
		}
	}

	public int Int32_0
	{
		get
		{
			return (uifont_0 != null) ? uifont_0.Int32_0 : ((bmfont_0 == null) ? 1 : bmfont_0.Int32_2);
		}
		set
		{
			if (uifont_0 != null)
			{
				uifont_0.Int32_0 = value;
			}
			else if (bmfont_0 != null)
			{
				bmfont_0.Int32_2 = value;
			}
		}
	}

	public int Int32_1
	{
		get
		{
			return (uifont_0 != null) ? uifont_0.Int32_1 : ((bmfont_0 == null) ? 1 : bmfont_0.Int32_3);
		}
		set
		{
			if (uifont_0 != null)
			{
				uifont_0.Int32_1 = value;
			}
			else if (bmfont_0 != null)
			{
				bmfont_0.Int32_3 = value;
			}
		}
	}

	public bool Boolean_0
	{
		get
		{
			return (uifont_0 != null) ? uifont_0.Boolean_0 : (list_0 != null && list_0.Count != 0);
		}
	}

	public List<BMSymbol> List_0
	{
		get
		{
			return (!(uifont_0 != null)) ? list_0 : uifont_0.List_0;
		}
	}

	public UIAtlas UIAtlas_0
	{
		get
		{
			return (!(uifont_0 != null)) ? uiatlas_0 : uifont_0.UIAtlas_0;
		}
		set
		{
			if (uifont_0 != null)
			{
				uifont_0.UIAtlas_0 = value;
			}
			else
			{
				if (!(uiatlas_0 != value))
				{
					return;
				}
				if (value == null)
				{
					if (uiatlas_0 != null)
					{
						material_0 = uiatlas_0.Material_0;
					}
					if (UISpriteData_0 != null)
					{
						rect_0 = Rect_0;
					}
				}
				int_1 = -1;
				uiatlas_0 = value;
				MarkAsChanged();
			}
		}
	}

	public Material Material_0
	{
		get
		{
			if (uifont_0 != null)
			{
				return uifont_0.Material_0;
			}
			if (uiatlas_0 != null)
			{
				return uiatlas_0.Material_0;
			}
			if (material_0 != null)
			{
				if (font_0 != null && material_0 != font_0.material)
				{
					material_0.mainTexture = font_0.material.mainTexture;
				}
				return material_0;
			}
			if (font_0 != null)
			{
				return font_0.material;
			}
			return null;
		}
		set
		{
			if (uifont_0 != null)
			{
				uifont_0.Material_0 = value;
			}
			else if (material_0 != value)
			{
				int_1 = -1;
				material_0 = value;
				MarkAsChanged();
			}
		}
	}

	public bool Boolean_1
	{
		get
		{
			if (uifont_0 != null)
			{
				return uifont_0.Boolean_1;
			}
			if (uiatlas_0 != null)
			{
				return uiatlas_0.Boolean_0;
			}
			if (int_1 == -1)
			{
				Material material = Material_0;
				int_1 = ((material != null && material.shader != null && material.shader.name.Contains("Premultiplied")) ? 1 : 0);
			}
			return int_1 == 1;
		}
	}

	public bool Boolean_2
	{
		get
		{
			if (uifont_0 != null)
			{
				return uifont_0.Boolean_2;
			}
			if (uiatlas_0 != null)
			{
				return false;
			}
			if (int_2 == -1)
			{
				Material material = Material_0;
				int_2 = ((material != null && material.shader != null && material.shader.name.Contains("Packed")) ? 1 : 0);
			}
			return int_2 == 1;
		}
	}

	public Texture2D Texture2D_0
	{
		get
		{
			if (uifont_0 != null)
			{
				return uifont_0.Texture2D_0;
			}
			Material material = Material_0;
			return (!(material != null)) ? null : (material.mainTexture as Texture2D);
		}
	}

	public Rect Rect_0
	{
		get
		{
			if (uifont_0 != null)
			{
				return uifont_0.Rect_0;
			}
			return (!(uiatlas_0 != null) || UISpriteData_0 == null) ? new Rect(0f, 0f, 1f, 1f) : rect_0;
		}
		set
		{
			if (uifont_0 != null)
			{
				uifont_0.Rect_0 = value;
			}
			else if (UISpriteData_0 == null && rect_0 != value)
			{
				rect_0 = value;
				MarkAsChanged();
			}
		}
	}

	public string String_0
	{
		get
		{
			return (!(uifont_0 != null)) ? bmfont_0.String_0 : uifont_0.String_0;
		}
		set
		{
			if (uifont_0 != null)
			{
				uifont_0.String_0 = value;
			}
			else if (bmfont_0.String_0 != value)
			{
				bmfont_0.String_0 = value;
				MarkAsChanged();
			}
		}
	}

	public bool Boolean_3
	{
		get
		{
			return font_0 != null || bmfont_0.Boolean_0;
		}
	}

	[Obsolete("Use UIFont.defaultSize instead")]
	public int Int32_2
	{
		get
		{
			return Int32_3;
		}
		set
		{
			Int32_3 = value;
		}
	}

	public int Int32_3
	{
		get
		{
			if (uifont_0 != null)
			{
				return uifont_0.Int32_3;
			}
			if (!Boolean_4 && bmfont_0 != null)
			{
				return bmfont_0.Int32_0;
			}
			return int_0;
		}
		set
		{
			if (uifont_0 != null)
			{
				uifont_0.Int32_3 = value;
			}
			else
			{
				int_0 = value;
			}
		}
	}

	public UISpriteData UISpriteData_0
	{
		get
		{
			if (uifont_0 != null)
			{
				return uifont_0.UISpriteData_0;
			}
			if (uispriteData_0 == null && uiatlas_0 != null && !string.IsNullOrEmpty(bmfont_0.String_0))
			{
				uispriteData_0 = uiatlas_0.GetSprite(bmfont_0.String_0);
				if (uispriteData_0 == null)
				{
					uispriteData_0 = uiatlas_0.GetSprite(base.name);
				}
				if (uispriteData_0 == null)
				{
					bmfont_0.String_0 = null;
				}
				else
				{
					UpdateUVRect();
				}
				int i = 0;
				for (int count = list_0.Count; i < count; i++)
				{
					List_0[i].MarkAsChanged();
				}
			}
			return uispriteData_0;
		}
	}

	public UIFont UIFont_0
	{
		get
		{
			return uifont_0;
		}
		set
		{
			UIFont uIFont = value;
			if (uIFont == this)
			{
				uIFont = null;
			}
			if (uifont_0 != uIFont)
			{
				if (uIFont != null && uIFont.UIFont_0 == this)
				{
					uIFont.UIFont_0 = null;
				}
				if (uifont_0 != null)
				{
					MarkAsChanged();
				}
				uifont_0 = uIFont;
				if (uIFont != null)
				{
					int_1 = -1;
					material_0 = null;
					bmfont_0 = null;
					font_0 = null;
				}
				MarkAsChanged();
			}
		}
	}

	public bool Boolean_4
	{
		get
		{
			return (!(uifont_0 != null)) ? (font_0 != null) : uifont_0.Boolean_4;
		}
	}

	public Font Font_0
	{
		get
		{
			return (!(uifont_0 != null)) ? font_0 : uifont_0.Font_0;
		}
		set
		{
			if (uifont_0 != null)
			{
				uifont_0.Font_0 = value;
			}
			else if (font_0 != value)
			{
				if (font_0 != null)
				{
					Material_0 = null;
				}
				font_0 = value;
				MarkAsChanged();
			}
		}
	}

	public FontStyle FontStyle_0
	{
		get
		{
			return (!(uifont_0 != null)) ? fontStyle_0 : uifont_0.FontStyle_0;
		}
		set
		{
			if (uifont_0 != null)
			{
				uifont_0.FontStyle_0 = value;
			}
			else if (fontStyle_0 != value)
			{
				fontStyle_0 = value;
				MarkAsChanged();
			}
		}
	}

	private Texture Texture_0
	{
		get
		{
			if ((bool)uifont_0)
			{
				return uifont_0.Texture_0;
			}
			if (Boolean_4)
			{
				return font_0.material.mainTexture;
			}
			return null;
		}
	}

	private void Trim()
	{
		Texture texture_ = uiatlas_0.Texture_0;
		if (texture_ != null && uispriteData_0 != null)
		{
			Rect rect = NGUIMath.ConvertToPixels(rect_0, Texture2D_0.width, Texture2D_0.height, true);
			Rect rect2 = new Rect(uispriteData_0.x, uispriteData_0.y, uispriteData_0.width, uispriteData_0.height);
			int int_ = Mathf.RoundToInt(rect2.xMin - rect.xMin);
			int int_2 = Mathf.RoundToInt(rect2.yMin - rect.yMin);
			int int_3 = Mathf.RoundToInt(rect2.xMax - rect.xMin);
			int int_4 = Mathf.RoundToInt(rect2.yMax - rect.yMin);
			bmfont_0.Trim(int_, int_2, int_3, int_4);
		}
	}

	private bool References(UIFont uifont_1)
	{
		if (uifont_1 == null)
		{
			return false;
		}
		if (uifont_1 == this)
		{
			return true;
		}
		return uifont_0 != null && uifont_0.References(uifont_1);
	}

	public static bool CheckIfRelated(UIFont uifont_1, UIFont uifont_2)
	{
		if (!(uifont_1 == null) && !(uifont_2 == null))
		{
			if (uifont_1.Boolean_4 && uifont_2.Boolean_4 && uifont_1.Font_0.fontNames[0] == uifont_2.Font_0.fontNames[0])
			{
				return true;
			}
			return uifont_1 == uifont_2 || uifont_1.References(uifont_2) || uifont_2.References(uifont_1);
		}
		return false;
	}

	public void MarkAsChanged()
	{
		if (uifont_0 != null)
		{
			uifont_0.MarkAsChanged();
		}
		uispriteData_0 = null;
		UILabel[] array = NGUITools.FindActive<UILabel>();
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			UILabel uILabel = array[i];
			if (uILabel.enabled && NGUITools.GetActive(uILabel.gameObject) && CheckIfRelated(this, uILabel.UIFont_1))
			{
				UIFont uIFont_ = uILabel.UIFont_1;
				uILabel.UIFont_1 = null;
				uILabel.UIFont_1 = uIFont_;
			}
		}
		int j = 0;
		for (int count = List_0.Count; j < count; j++)
		{
			List_0[j].MarkAsChanged();
		}
	}

	public void UpdateUVRect()
	{
		if (uiatlas_0 == null)
		{
			return;
		}
		Texture texture_ = uiatlas_0.Texture_0;
		if (texture_ != null)
		{
			rect_0 = new Rect(uispriteData_0.x - uispriteData_0.paddingLeft, uispriteData_0.y - uispriteData_0.paddingTop, uispriteData_0.width + uispriteData_0.paddingLeft + uispriteData_0.paddingRight, uispriteData_0.height + uispriteData_0.paddingTop + uispriteData_0.paddingBottom);
			rect_0 = NGUIMath.ConvertToTexCoords(rect_0, texture_.width, texture_.height);
			if (uispriteData_0.Boolean_1)
			{
				Trim();
			}
		}
	}

	private BMSymbol GetSymbol(string string_0, bool bool_0)
	{
		int num = 0;
		int count = list_0.Count;
		BMSymbol bMSymbol;
		while (true)
		{
			if (num < count)
			{
				bMSymbol = list_0[num];
				if (bMSymbol.sequence == string_0)
				{
					break;
				}
				num++;
				continue;
			}
			if (bool_0)
			{
				BMSymbol bMSymbol2 = new BMSymbol();
				bMSymbol2.sequence = string_0;
				list_0.Add(bMSymbol2);
				return bMSymbol2;
			}
			return null;
		}
		return bMSymbol;
	}

	public BMSymbol MatchSymbol(string string_0, int int_3, int int_4)
	{
		int count = list_0.Count;
		if (count == 0)
		{
			return null;
		}
		int_4 -= int_3;
		int num = 0;
		BMSymbol bMSymbol;
		while (true)
		{
			if (num < count)
			{
				bMSymbol = list_0[num];
				int int32_ = bMSymbol.Int32_0;
				if (int32_ != 0 && int_4 >= int32_)
				{
					bool flag = true;
					for (int i = 0; i < int32_; i++)
					{
						if (string_0[int_3 + i] != bMSymbol.sequence[i])
						{
							flag = false;
							break;
						}
					}
					if (flag && bMSymbol.Validate(UIAtlas_0))
					{
						break;
					}
				}
				num++;
				continue;
			}
			return null;
		}
		return bMSymbol;
	}

	public void AddSymbol(string string_0, string string_1)
	{
		BMSymbol symbol = GetSymbol(string_0, true);
		symbol.spriteName = string_1;
		MarkAsChanged();
	}

	public void RemoveSymbol(string string_0)
	{
		BMSymbol symbol = GetSymbol(string_0, false);
		if (symbol != null)
		{
			List_0.Remove(symbol);
		}
		MarkAsChanged();
	}

	public void RenameSymbol(string string_0, string string_1)
	{
		BMSymbol symbol = GetSymbol(string_0, false);
		if (symbol != null)
		{
			symbol.sequence = string_1;
		}
		MarkAsChanged();
	}

	public bool UsesSprite(string string_0)
	{
		if (!string.IsNullOrEmpty(string_0))
		{
			if (string_0.Equals(String_0))
			{
				return true;
			}
			int i = 0;
			for (int count = List_0.Count; i < count; i++)
			{
				BMSymbol bMSymbol = List_0[i];
				if (string_0.Equals(bMSymbol.spriteName))
				{
					return true;
				}
			}
		}
		return false;
	}
}
