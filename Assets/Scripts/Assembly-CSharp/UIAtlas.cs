using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UIAtlas : MonoBehaviour
{
	[Serializable]
	private class Sprite
	{
		public string name = "Unity Bug";

		public Rect outer = new Rect(0f, 0f, 1f, 1f);

		public Rect inner = new Rect(0f, 0f, 1f, 1f);

		public bool rotated;

		public float paddingLeft;

		public float paddingRight;

		public float paddingTop;

		public float paddingBottom;

		public bool Boolean_0
		{
			get
			{
				return paddingLeft != 0f || paddingRight != 0f || paddingTop != 0f || paddingBottom != 0f;
			}
		}
	}

	private enum Coordinates
	{
		Pixels = 0,
		TexCoords = 1
	}

	[SerializeField]
	private Material material_0;

	[SerializeField]
	private List<UISpriteData> list_0 = new List<UISpriteData>();

	[SerializeField]
	private float float_0 = 1f;

	[SerializeField]
	private UIAtlas uiatlas_0;

	[SerializeField]
	private Coordinates coordinates_0;

	[SerializeField]
	private List<Sprite> list_1 = new List<Sprite>();

	private int int_0 = -1;

	[CompilerGenerated]
	private static Comparison<UISpriteData> comparison_0;

	public Material Material_0
	{
		get
		{
			return (!(uiatlas_0 != null)) ? material_0 : uiatlas_0.Material_0;
		}
		set
		{
			if (uiatlas_0 != null)
			{
				uiatlas_0.Material_0 = value;
				return;
			}
			if (material_0 == null)
			{
				int_0 = 0;
				material_0 = value;
				return;
			}
			MarkAsChanged();
			int_0 = -1;
			material_0 = value;
			MarkAsChanged();
		}
	}

	public bool Boolean_0
	{
		get
		{
			if (uiatlas_0 != null)
			{
				return uiatlas_0.Boolean_0;
			}
			if (int_0 == -1)
			{
				Material material = Material_0;
				int_0 = ((material != null && material.shader != null && material.shader.name.Contains("Premultiplied")) ? 1 : 0);
			}
			return int_0 == 1;
		}
	}

	public List<UISpriteData> List_0
	{
		get
		{
			if (uiatlas_0 != null)
			{
				return uiatlas_0.List_0;
			}
			if (list_0.Count == 0)
			{
				Upgrade();
			}
			return list_0;
		}
		set
		{
			if (uiatlas_0 != null)
			{
				uiatlas_0.List_0 = value;
			}
			else
			{
				list_0 = value;
			}
		}
	}

	public Texture Texture_0
	{
		get
		{
			return (uiatlas_0 != null) ? uiatlas_0.Texture_0 : ((!(material_0 != null)) ? null : material_0.mainTexture);
		}
	}

	public float Single_0
	{
		get
		{
			return (!(uiatlas_0 != null)) ? float_0 : uiatlas_0.Single_0;
		}
		set
		{
			if (uiatlas_0 != null)
			{
				uiatlas_0.Single_0 = value;
				return;
			}
			float num = Mathf.Clamp(value, 0.25f, 4f);
			if (float_0 != num)
			{
				float_0 = num;
				MarkAsChanged();
			}
		}
	}

	public UIAtlas UIAtlas_0
	{
		get
		{
			return uiatlas_0;
		}
		set
		{
			UIAtlas uIAtlas = value;
			if (uIAtlas == this)
			{
				uIAtlas = null;
			}
			if (uiatlas_0 != uIAtlas)
			{
				if (uIAtlas != null && uIAtlas.UIAtlas_0 == this)
				{
					uIAtlas.UIAtlas_0 = null;
				}
				if (uiatlas_0 != null)
				{
					MarkAsChanged();
				}
				uiatlas_0 = uIAtlas;
				if (uIAtlas != null)
				{
					material_0 = null;
				}
				MarkAsChanged();
			}
		}
	}

	public UISpriteData GetSprite(string string_0)
	{
		if (uiatlas_0 != null)
		{
			return uiatlas_0.GetSprite(string_0);
		}
		if (!string.IsNullOrEmpty(string_0))
		{
			if (list_0.Count == 0)
			{
				Upgrade();
			}
			int i = 0;
			for (int count = list_0.Count; i < count; i++)
			{
				UISpriteData uISpriteData = list_0[i];
				if (!string.IsNullOrEmpty(uISpriteData.name) && string_0 == uISpriteData.name)
				{
					return uISpriteData;
				}
			}
		}
		return null;
	}

	public void SortAlphabetically()
	{
		list_0.Sort((UISpriteData uispriteData_0, UISpriteData uispriteData_1) => uispriteData_0.name.CompareTo(uispriteData_1.name));
	}

	public BetterList<string> GetListOfSprites()
	{
		if (uiatlas_0 != null)
		{
			return uiatlas_0.GetListOfSprites();
		}
		if (list_0.Count == 0)
		{
			Upgrade();
		}
		BetterList<string> betterList = new BetterList<string>();
		int i = 0;
		for (int count = list_0.Count; i < count; i++)
		{
			UISpriteData uISpriteData = list_0[i];
			if (uISpriteData != null && !string.IsNullOrEmpty(uISpriteData.name))
			{
				betterList.Add(uISpriteData.name);
			}
		}
		return betterList;
	}

	public BetterList<string> GetListOfSprites(string string_0)
	{
		if ((bool)uiatlas_0)
		{
			return uiatlas_0.GetListOfSprites(string_0);
		}
		if (string.IsNullOrEmpty(string_0))
		{
			return GetListOfSprites();
		}
		if (list_0.Count == 0)
		{
			Upgrade();
		}
		BetterList<string> betterList = new BetterList<string>();
		int num = 0;
		int count = list_0.Count;
		UISpriteData uISpriteData;
		while (true)
		{
			if (num < count)
			{
				uISpriteData = list_0[num];
				if (uISpriteData != null && !string.IsNullOrEmpty(uISpriteData.name) && string.Equals(string_0, uISpriteData.name, StringComparison.OrdinalIgnoreCase))
				{
					break;
				}
				num++;
				continue;
			}
			string[] array = string_0.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].ToLower();
			}
			int j = 0;
			for (int count2 = list_0.Count; j < count2; j++)
			{
				UISpriteData uISpriteData2 = list_0[j];
				if (uISpriteData2 == null || string.IsNullOrEmpty(uISpriteData2.name))
				{
					continue;
				}
				string text = uISpriteData2.name.ToLower();
				int num2 = 0;
				for (int k = 0; k < array.Length; k++)
				{
					if (text.Contains(array[k]))
					{
						num2++;
					}
				}
				if (num2 == array.Length)
				{
					betterList.Add(uISpriteData2.name);
				}
			}
			return betterList;
		}
		betterList.Add(uISpriteData.name);
		return betterList;
	}

	private bool References(UIAtlas uiatlas_1)
	{
		if (uiatlas_1 == null)
		{
			return false;
		}
		if (uiatlas_1 == this)
		{
			return true;
		}
		return uiatlas_0 != null && uiatlas_0.References(uiatlas_1);
	}

	public static bool CheckIfRelated(UIAtlas uiatlas_1, UIAtlas uiatlas_2)
	{
		if (!(uiatlas_1 == null) && !(uiatlas_2 == null))
		{
			return uiatlas_1 == uiatlas_2 || uiatlas_1.References(uiatlas_2) || uiatlas_2.References(uiatlas_1);
		}
		return false;
	}

	public void MarkAsChanged()
	{
		if (uiatlas_0 != null)
		{
			uiatlas_0.MarkAsChanged();
		}
		UISprite[] array = NGUITools.FindActive<UISprite>();
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			UISprite uISprite = array[i];
			if (CheckIfRelated(this, uISprite.UIAtlas_0))
			{
				UIAtlas uIAtlas_ = uISprite.UIAtlas_0;
				uISprite.UIAtlas_0 = null;
				uISprite.UIAtlas_0 = uIAtlas_;
			}
		}
		UIFont[] array2 = Resources.FindObjectsOfTypeAll(typeof(UIFont)) as UIFont[];
		int j = 0;
		for (int num2 = array2.Length; j < num2; j++)
		{
			UIFont uIFont = array2[j];
			if (CheckIfRelated(this, uIFont.UIAtlas_0))
			{
				UIAtlas uIAtlas_2 = uIFont.UIAtlas_0;
				uIFont.UIAtlas_0 = null;
				uIFont.UIAtlas_0 = uIAtlas_2;
			}
		}
		UILabel[] array3 = NGUITools.FindActive<UILabel>();
		int k = 0;
		for (int num3 = array3.Length; k < num3; k++)
		{
			UILabel uILabel = array3[k];
			if (uILabel.UIFont_1 != null && CheckIfRelated(this, uILabel.UIFont_1.UIAtlas_0))
			{
				UIFont uIFont_ = uILabel.UIFont_1;
				uILabel.UIFont_1 = null;
				uILabel.UIFont_1 = uIFont_;
			}
		}
	}

	private bool Upgrade()
	{
		if ((bool)uiatlas_0)
		{
			return uiatlas_0.Upgrade();
		}
		if (list_0.Count == 0 && list_1.Count > 0 && (bool)material_0)
		{
			Texture mainTexture = material_0.mainTexture;
			int num = ((!(mainTexture != null)) ? 512 : mainTexture.width);
			int int_ = ((!(mainTexture != null)) ? 512 : mainTexture.height);
			for (int i = 0; i < list_1.Count; i++)
			{
				Sprite sprite = list_1[i];
				Rect outer = sprite.outer;
				Rect inner = sprite.inner;
				if (coordinates_0 == Coordinates.TexCoords)
				{
					NGUIMath.ConvertToPixels(outer, num, int_, true);
					NGUIMath.ConvertToPixels(inner, num, int_, true);
				}
				UISpriteData uISpriteData = new UISpriteData();
				uISpriteData.name = sprite.name;
				uISpriteData.x = Mathf.RoundToInt(outer.xMin);
				uISpriteData.y = Mathf.RoundToInt(outer.yMin);
				uISpriteData.width = Mathf.RoundToInt(outer.width);
				uISpriteData.height = Mathf.RoundToInt(outer.height);
				uISpriteData.paddingLeft = Mathf.RoundToInt(sprite.paddingLeft * outer.width);
				uISpriteData.paddingRight = Mathf.RoundToInt(sprite.paddingRight * outer.width);
				uISpriteData.paddingBottom = Mathf.RoundToInt(sprite.paddingBottom * outer.height);
				uISpriteData.paddingTop = Mathf.RoundToInt(sprite.paddingTop * outer.height);
				uISpriteData.borderLeft = Mathf.RoundToInt(inner.xMin - outer.xMin);
				uISpriteData.borderRight = Mathf.RoundToInt(outer.xMax - inner.xMax);
				uISpriteData.borderBottom = Mathf.RoundToInt(outer.yMax - inner.yMax);
				uISpriteData.borderTop = Mathf.RoundToInt(inner.yMin - outer.yMin);
				list_0.Add(uISpriteData);
			}
			list_1.Clear();
			return true;
		}
		return false;
	}
}
