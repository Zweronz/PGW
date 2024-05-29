using System;
using UnityEngine;

public class UISprite : UIBasicSprite
{
	[SerializeField]
	private UIAtlas uiatlas_0;

	[SerializeField]
	private string string_0;

	[SerializeField]
	private bool bool_15 = true;

	[NonSerialized]
	protected UISpriteData uispriteData_0;

	[NonSerialized]
	private bool bool_16;

	public override Material Material_0
	{
		get
		{
			return (!(uiatlas_0 != null)) ? null : uiatlas_0.Material_0;
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
			if (uiatlas_0 != value)
			{
				RemoveFromPanel();
				uiatlas_0 = value;
				bool_16 = false;
				uispriteData_0 = null;
				if (string.IsNullOrEmpty(string_0) && uiatlas_0 != null && uiatlas_0.List_0.Count > 0)
				{
					SetAtlasSprite(uiatlas_0.List_0[0]);
					string_0 = uispriteData_0.name;
				}
				if (!string.IsNullOrEmpty(string_0))
				{
					string text = string_0;
					string_0 = string.Empty;
					String_0 = text;
					MarkAsChanged();
				}
			}
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
			if (string.IsNullOrEmpty(value))
			{
				if (!string.IsNullOrEmpty(string_0))
				{
					string_0 = string.Empty;
					uispriteData_0 = null;
					bool_0 = true;
					bool_16 = false;
				}
			}
			else if (string_0 != value)
			{
				string_0 = value;
				uispriteData_0 = null;
				bool_0 = true;
				bool_16 = false;
			}
		}
	}

	public new bool Boolean_7
	{
		get
		{
			return GetAtlasSprite() != null;
		}
	}

	[Obsolete("Use 'centerType' instead")]
	public bool Boolean_8
	{
		get
		{
			return advancedType_0 != AdvancedType.Invisible;
		}
		set
		{
			if (value != (advancedType_0 != AdvancedType.Invisible))
			{
				advancedType_0 = (value ? AdvancedType.Sliced : AdvancedType.Invisible);
				MarkAsChanged();
			}
		}
	}

	public override Vector4 Vector4_3
	{
		get
		{
			UISpriteData atlasSprite = GetAtlasSprite();
			if (atlasSprite == null)
			{
				return base.Vector4_3;
			}
			return new Vector4(atlasSprite.borderLeft, atlasSprite.borderBottom, atlasSprite.borderRight, atlasSprite.borderTop);
		}
	}

	public override float Single_1
	{
		get
		{
			return (!(uiatlas_0 != null)) ? 1f : uiatlas_0.Single_0;
		}
	}

	public override int Int32_4
	{
		get
		{
			if (Type_0 != Type.Sliced && Type_0 != Type.Advanced)
			{
				return base.Int32_4;
			}
			Vector4 vector = Vector4_3 * Single_1;
			int num = Mathf.RoundToInt(vector.x + vector.z);
			UISpriteData atlasSprite = GetAtlasSprite();
			if (atlasSprite != null)
			{
				num += atlasSprite.paddingLeft + atlasSprite.paddingRight;
			}
			return Mathf.Max(base.Int32_4, ((num & 1) != 1) ? num : (num + 1));
		}
	}

	public override int Int32_5
	{
		get
		{
			if (Type_0 != Type.Sliced && Type_0 != Type.Advanced)
			{
				return base.Int32_5;
			}
			Vector4 vector = Vector4_3 * Single_1;
			int num = Mathf.RoundToInt(vector.y + vector.w);
			UISpriteData atlasSprite = GetAtlasSprite();
			if (atlasSprite != null)
			{
				num += atlasSprite.paddingTop + atlasSprite.paddingBottom;
			}
			return Mathf.Max(base.Int32_5, ((num & 1) != 1) ? num : (num + 1));
		}
	}

	public override Vector4 Vector4_2
	{
		get
		{
			Vector2 vector = base.Vector2_0;
			float num = (0f - vector.x) * (float)int_1;
			float num2 = (0f - vector.y) * (float)int_2;
			float num3 = num + (float)int_1;
			float num4 = num2 + (float)int_2;
			if (GetAtlasSprite() != null && type_0 != Type.Tiled)
			{
				int paddingLeft = uispriteData_0.paddingLeft;
				int paddingBottom = uispriteData_0.paddingBottom;
				int num5 = uispriteData_0.paddingRight;
				int num6 = uispriteData_0.paddingTop;
				int num7 = uispriteData_0.width + paddingLeft + num5;
				int num8 = uispriteData_0.height + paddingBottom + num6;
				float num9 = 1f;
				float num10 = 1f;
				if (num7 > 0 && num8 > 0 && (type_0 == Type.Simple || type_0 == Type.Filled))
				{
					if (((uint)num7 & (true ? 1u : 0u)) != 0)
					{
						num5++;
					}
					if (((uint)num8 & (true ? 1u : 0u)) != 0)
					{
						num6++;
					}
					num9 = 1f / (float)num7 * (float)int_1;
					num10 = 1f / (float)num8 * (float)int_2;
				}
				if (flip_0 != Flip.Horizontally && flip_0 != Flip.Both)
				{
					num += (float)paddingLeft * num9;
					num3 -= (float)num5 * num9;
				}
				else
				{
					num += (float)num5 * num9;
					num3 -= (float)paddingLeft * num9;
				}
				if (flip_0 != Flip.Vertically && flip_0 != Flip.Both)
				{
					num2 += (float)paddingBottom * num10;
					num4 -= (float)num6 * num10;
				}
				else
				{
					num2 += (float)num6 * num10;
					num4 -= (float)paddingBottom * num10;
				}
			}
			Vector4 vector2 = ((!(uiatlas_0 != null)) ? Vector4.zero : (Vector4_3 * Single_1));
			float num11 = vector2.x + vector2.z;
			float num12 = vector2.y + vector2.w;
			float x = Mathf.Lerp(num, num3 - num11, vector4_0.x);
			float y = Mathf.Lerp(num2, num4 - num12, vector4_0.y);
			float z = Mathf.Lerp(num + num11, num3, vector4_0.z);
			float w = Mathf.Lerp(num2 + num12, num4, vector4_0.w);
			return new Vector4(x, y, z, w);
		}
	}

	public override bool Boolean_9
	{
		get
		{
			return uiatlas_0 != null && uiatlas_0.Boolean_0;
		}
	}

	public UISpriteData GetAtlasSprite()
	{
		if (!bool_16)
		{
			uispriteData_0 = null;
		}
		if (uispriteData_0 == null && uiatlas_0 != null)
		{
			if (!string.IsNullOrEmpty(string_0))
			{
				UISpriteData sprite = uiatlas_0.GetSprite(string_0);
				if (sprite == null)
				{
					return null;
				}
				SetAtlasSprite(sprite);
			}
			if (uispriteData_0 == null && uiatlas_0.List_0.Count > 0)
			{
				UISpriteData uISpriteData = uiatlas_0.List_0[0];
				if (uISpriteData == null)
				{
					return null;
				}
				SetAtlasSprite(uISpriteData);
				if (uispriteData_0 == null)
				{
					Debug.LogError(uiatlas_0.name + " seems to have a null sprite!");
					return null;
				}
				string_0 = uispriteData_0.name;
			}
		}
		return uispriteData_0;
	}

	protected void SetAtlasSprite(UISpriteData uispriteData_1)
	{
		bool_0 = true;
		bool_16 = true;
		if (uispriteData_1 != null)
		{
			uispriteData_0 = uispriteData_1;
			string_0 = uispriteData_0.name;
		}
		else
		{
			string_0 = ((uispriteData_0 == null) ? string.Empty : uispriteData_0.name);
			uispriteData_0 = uispriteData_1;
		}
	}

	public override void MakePixelPerfect()
	{
		if (!Boolean_7)
		{
			return;
		}
		base.MakePixelPerfect();
		if (type_0 == Type.Tiled)
		{
			return;
		}
		UISpriteData atlasSprite = GetAtlasSprite();
		if (atlasSprite == null)
		{
			return;
		}
		Texture texture_ = Texture_0;
		if (!(texture_ == null) && (type_0 == Type.Simple || type_0 == Type.Filled || !atlasSprite.Boolean_0) && texture_ != null)
		{
			int num = Mathf.RoundToInt(Single_1 * (float)(atlasSprite.width + atlasSprite.paddingLeft + atlasSprite.paddingRight));
			int num2 = Mathf.RoundToInt(Single_1 * (float)(atlasSprite.height + atlasSprite.paddingTop + atlasSprite.paddingBottom));
			if ((num & 1) == 1)
			{
				num++;
			}
			if ((num2 & 1) == 1)
			{
				num2++;
			}
			base.Int32_0 = num;
			base.Int32_1 = num2;
		}
	}

	protected override void OnInit()
	{
		if (!bool_15)
		{
			bool_15 = true;
			advancedType_0 = AdvancedType.Invisible;
		}
		base.OnInit();
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (bool_0 || !bool_16)
		{
			bool_16 = true;
			uispriteData_0 = null;
			bool_0 = true;
		}
	}

	public override void OnFill(BetterList<Vector3> betterList_1, BetterList<Vector2> betterList_2, BetterList<Color32> betterList_3)
	{
		Texture texture_ = Texture_0;
		if (!(texture_ == null))
		{
			if (uispriteData_0 == null)
			{
				uispriteData_0 = UIAtlas_0.GetSprite(String_0);
			}
			if (uispriteData_0 != null)
			{
				Rect rect = new Rect(uispriteData_0.x, uispriteData_0.y, uispriteData_0.width, uispriteData_0.height);
				Rect rect2 = new Rect(uispriteData_0.x + uispriteData_0.borderLeft, uispriteData_0.y + uispriteData_0.borderTop, uispriteData_0.width - uispriteData_0.borderLeft - uispriteData_0.borderRight, uispriteData_0.height - uispriteData_0.borderBottom - uispriteData_0.borderTop);
				rect = NGUIMath.ConvertToTexCoords(rect, texture_.width, texture_.height);
				rect2 = NGUIMath.ConvertToTexCoords(rect2, texture_.width, texture_.height);
				Fill(betterList_1, betterList_2, betterList_3, rect, rect2);
			}
		}
	}
}
