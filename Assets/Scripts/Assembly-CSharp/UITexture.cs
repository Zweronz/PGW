using System;
using UnityEngine;

public class UITexture : UIBasicSprite
{
	[SerializeField]
	private Rect rect_2 = new Rect(0f, 0f, 1f, 1f);

	[SerializeField]
	private Texture texture_0;

	[SerializeField]
	private Material material_0;

	[SerializeField]
	private Shader shader_0;

	[SerializeField]
	private Vector4 vector4_1 = Vector4.zero;

	[NonSerialized]
	private int int_6 = -1;

	public override Texture Texture_0
	{
		get
		{
			if (texture_0 != null)
			{
				return texture_0;
			}
			if (material_0 != null)
			{
				return material_0.mainTexture;
			}
			return null;
		}
		set
		{
			if (texture_0 != value)
			{
				RemoveFromPanel();
				texture_0 = value;
				int_6 = -1;
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
				shader_0 = null;
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
				int_6 = -1;
				material_0 = null;
				MarkAsChanged();
			}
		}
	}

	public override bool Boolean_9
	{
		get
		{
			if (int_6 == -1)
			{
				Material material = Material_0;
				int_6 = ((material != null && material.shader != null && material.shader.name.Contains("Premultiplied")) ? 1 : 0);
			}
			return int_6 == 1;
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

	public Rect Rect_0
	{
		get
		{
			return rect_2;
		}
		set
		{
			if (rect_2 != value)
			{
				rect_2 = value;
				MarkAsChanged();
			}
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
			if (texture_0 != null && type_0 != Type.Tiled)
			{
				int width = texture_0.width;
				int height = texture_0.height;
				int num5 = 0;
				int num6 = 0;
				float num7 = 1f;
				float num8 = 1f;
				if (width > 0 && height > 0 && (type_0 == Type.Simple || type_0 == Type.Filled))
				{
					if (((uint)width & (true ? 1u : 0u)) != 0)
					{
						num5++;
					}
					if (((uint)height & (true ? 1u : 0u)) != 0)
					{
						num6++;
					}
					num7 = 1f / (float)width * (float)int_1;
					num8 = 1f / (float)height * (float)int_2;
				}
				if (flip_0 != Flip.Horizontally && flip_0 != Flip.Both)
				{
					num3 -= (float)num5 * num7;
				}
				else
				{
					num += (float)num5 * num7;
				}
				if (flip_0 != Flip.Vertically && flip_0 != Flip.Both)
				{
					num4 -= (float)num6 * num8;
				}
				else
				{
					num2 += (float)num6 * num8;
				}
			}
			Vector4 vector4_ = Vector4_3;
			float num9 = vector4_.x + vector4_.z;
			float num10 = vector4_.y + vector4_.w;
			float x = Mathf.Lerp(num, num3 - num9, vector4_0.x);
			float y = Mathf.Lerp(num2, num4 - num10, vector4_0.y);
			float z = Mathf.Lerp(num + num9, num3, vector4_0.z);
			float w = Mathf.Lerp(num2 + num10, num4, vector4_0.w);
			return new Vector4(x, y, z, w);
		}
	}

	public override void MakePixelPerfect()
	{
		base.MakePixelPerfect();
		if (type_0 == Type.Tiled)
		{
			return;
		}
		Texture texture = Texture_0;
		if (!(texture == null) && (type_0 == Type.Simple || type_0 == Type.Filled || !base.Boolean_6) && texture != null)
		{
			int num = texture.width;
			int num2 = texture.height;
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
		Texture texture = Texture_0;
		if (!(texture == null))
		{
			Rect rect = new Rect(rect_2.x * (float)texture.width, rect_2.y * (float)texture.height, (float)texture.width * rect_2.width, (float)texture.height * rect_2.height);
			Rect rect_ = rect;
			Vector4 vector4_ = Vector4_3;
			rect_.xMin += vector4_.x;
			rect_.yMin += vector4_.y;
			rect_.xMax -= vector4_.z;
			rect_.yMax -= vector4_.w;
			float num = 1f / (float)texture.width;
			float num2 = 1f / (float)texture.height;
			rect.xMin *= num;
			rect.xMax *= num;
			rect.yMin *= num2;
			rect.yMax *= num2;
			rect_.xMin *= num;
			rect_.xMax *= num;
			rect_.yMin *= num2;
			rect_.yMax *= num2;
			Fill(betterList_1, betterList_2, betterList_3, rect, rect_);
		}
	}
}
