using System;
using UnityEngine;

public class UI2DSprite : UIBasicSprite
{
	[SerializeField]
	private Sprite sprite_0;

	[SerializeField]
	private Material material_0;

	[SerializeField]
	private Shader shader_0;

	[SerializeField]
	private Vector4 vector4_1 = Vector4.zero;

	public Sprite sprite_1;

	[NonSerialized]
	private int int_6 = -1;

	public Sprite Sprite_0
	{
		get
		{
			return sprite_0;
		}
		set
		{
			if (sprite_0 != value)
			{
				RemoveFromPanel();
				sprite_0 = value;
				sprite_1 = null;
				MarkAsChanged();
			}
		}
	}

	public override Material Material_0
	{
		get
		{
			return material_0;
		}
		set
		{
			if (material_0 != value)
			{
				RemoveFromPanel();
				material_0 = value;
				int_6 = -1;
				MarkAsChanged();
			}
		}
	}

	public override Shader Shader_0
	{
		get
		{
			if (material_0 != null)
			{
				return material_0.shader;
			}
			if (shader_0 == null)
			{
				shader_0 = Shader.Find("Unlit/Transparent Colored");
			}
			return shader_0;
		}
		set
		{
			if (shader_0 != value)
			{
				RemoveFromPanel();
				shader_0 = value;
				if (material_0 == null)
				{
					int_6 = -1;
					MarkAsChanged();
				}
			}
		}
	}

	public override Texture Texture_0
	{
		get
		{
			if (sprite_0 != null)
			{
				return sprite_0.texture;
			}
			if (material_0 != null)
			{
				return material_0.mainTexture;
			}
			return null;
		}
	}

	public override bool Boolean_9
	{
		get
		{
			if (int_6 == -1)
			{
				Shader shader = Shader_0;
				int_6 = ((shader != null && shader.name.Contains("Premultiplied")) ? 1 : 0);
			}
			return int_6 == 1;
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
			if (sprite_0 != null && type_0 != Type.Tiled)
			{
				int num5 = Mathf.RoundToInt(sprite_0.rect.width);
				int num6 = Mathf.RoundToInt(sprite_0.rect.height);
				int num7 = Mathf.RoundToInt(sprite_0.textureRectOffset.x);
				int num8 = Mathf.RoundToInt(sprite_0.textureRectOffset.y);
				int num9 = Mathf.RoundToInt(sprite_0.rect.width - sprite_0.textureRect.width - sprite_0.textureRectOffset.x);
				int num10 = Mathf.RoundToInt(sprite_0.rect.height - sprite_0.textureRect.height - sprite_0.textureRectOffset.y);
				float num11 = 1f;
				float num12 = 1f;
				if (num5 > 0 && num6 > 0 && (type_0 == Type.Simple || type_0 == Type.Filled))
				{
					if (((uint)num5 & (true ? 1u : 0u)) != 0)
					{
						num9++;
					}
					if (((uint)num6 & (true ? 1u : 0u)) != 0)
					{
						num10++;
					}
					num11 = 1f / (float)num5 * (float)int_1;
					num12 = 1f / (float)num6 * (float)int_2;
				}
				if (flip_0 != Flip.Horizontally && flip_0 != Flip.Both)
				{
					num += (float)num7 * num11;
					num3 -= (float)num9 * num11;
				}
				else
				{
					num += (float)num9 * num11;
					num3 -= (float)num7 * num11;
				}
				if (flip_0 != Flip.Vertically && flip_0 != Flip.Both)
				{
					num2 += (float)num8 * num12;
					num4 -= (float)num10 * num12;
				}
				else
				{
					num2 += (float)num10 * num12;
					num4 -= (float)num8 * num12;
				}
			}
			Vector4 vector4_ = Vector4_3;
			float num13 = vector4_.x + vector4_.z;
			float num14 = vector4_.y + vector4_.w;
			float x = Mathf.Lerp(num, num3 - num13, vector4_0.x);
			float y = Mathf.Lerp(num2, num4 - num14, vector4_0.y);
			float z = Mathf.Lerp(num + num13, num3, vector4_0.z);
			float w = Mathf.Lerp(num2 + num14, num4, vector4_0.w);
			return new Vector4(x, y, z, w);
		}
	}

	public override Vector4 Vector4_3
	{
		get
		{
			return vector4_1;
		}
		set
		{
			if (vector4_1 != value)
			{
				vector4_1 = value;
				MarkAsChanged();
			}
		}
	}

	protected override void OnUpdate()
	{
		if (sprite_1 != null)
		{
			if (sprite_1 != sprite_0)
			{
				Sprite_0 = sprite_1;
			}
			sprite_1 = null;
		}
		base.OnUpdate();
	}

	public override void MakePixelPerfect()
	{
		base.MakePixelPerfect();
		if (type_0 == Type.Tiled)
		{
			return;
		}
		Texture texture_ = Texture_0;
		if (!(texture_ == null) && (type_0 == Type.Simple || type_0 == Type.Filled || !base.Boolean_6) && texture_ != null)
		{
			Rect rect = sprite_0.rect;
			int num = Mathf.RoundToInt(rect.width);
			int num2 = Mathf.RoundToInt(rect.height);
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

	public override void OnFill(BetterList<Vector3> betterList_1, BetterList<Vector2> betterList_2, BetterList<Color32> betterList_3)
	{
		Texture texture_ = Texture_0;
		if (!(texture_ == null))
		{
			Rect textureRect = sprite_0.textureRect;
			Rect rect_ = textureRect;
			Vector4 vector4_ = Vector4_3;
			rect_.xMin += vector4_.x;
			rect_.yMin += vector4_.y;
			rect_.xMax -= vector4_.z;
			rect_.yMax -= vector4_.w;
			float num = 1f / (float)texture_.width;
			float num2 = 1f / (float)texture_.height;
			textureRect.xMin *= num;
			textureRect.xMax *= num;
			textureRect.yMin *= num2;
			textureRect.yMax *= num2;
			rect_.xMin *= num;
			rect_.xMax *= num;
			rect_.yMin *= num2;
			rect_.yMax *= num2;
			Fill(betterList_1, betterList_2, betterList_3, textureRect, rect_);
		}
	}
}
