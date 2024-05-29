using System;
using UnityEngine;

public abstract class UIBasicSprite : UIWidget
{
	public enum Type
	{
		Simple = 0,
		Sliced = 1,
		Tiled = 2,
		Filled = 3,
		Advanced = 4
	}

	public enum FillDirection
	{
		Horizontal = 0,
		Vertical = 1,
		Radial90 = 2,
		Radial180 = 3,
		Radial360 = 4
	}

	public enum AdvancedType
	{
		Invisible = 0,
		Sliced = 1,
		Tiled = 2
	}

	public enum Flip
	{
		Nothing = 0,
		Horizontally = 1,
		Vertically = 2,
		Both = 3
	}

	[SerializeField]
	protected Type type_0;

	[SerializeField]
	protected FillDirection fillDirection_0 = FillDirection.Radial360;

	[Range(0f, 1f)]
	[SerializeField]
	protected float float_2 = 1f;

	[SerializeField]
	protected bool bool_14;

	[SerializeField]
	protected Flip flip_0;

	[NonSerialized]
	private Rect rect_0 = default(Rect);

	[NonSerialized]
	private Rect rect_1 = default(Rect);

	public AdvancedType advancedType_0 = AdvancedType.Sliced;

	public AdvancedType advancedType_1 = AdvancedType.Sliced;

	public AdvancedType advancedType_2 = AdvancedType.Sliced;

	public AdvancedType advancedType_3 = AdvancedType.Sliced;

	public AdvancedType advancedType_4 = AdvancedType.Sliced;

	protected static Vector2[] vector2_0 = new Vector2[4];

	protected static Vector2[] vector2_1 = new Vector2[4];

	public virtual Type Type_0
	{
		get
		{
			return type_0;
		}
		set
		{
			if (type_0 != value)
			{
				type_0 = value;
				MarkAsChanged();
			}
		}
	}

	public Flip Flip_0
	{
		get
		{
			return flip_0;
		}
		set
		{
			if (flip_0 != value)
			{
				flip_0 = value;
				MarkAsChanged();
			}
		}
	}

	public FillDirection FillDirection_0
	{
		get
		{
			return fillDirection_0;
		}
		set
		{
			if (fillDirection_0 != value)
			{
				fillDirection_0 = value;
				bool_0 = true;
			}
		}
	}

	public float Single_0
	{
		get
		{
			return float_2;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (float_2 != num)
			{
				float_2 = num;
				bool_0 = true;
			}
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
			return Mathf.Max(base.Int32_5, ((num & 1) != 1) ? num : (num + 1));
		}
	}

	public bool Boolean_5
	{
		get
		{
			return bool_14;
		}
		set
		{
			if (bool_14 != value)
			{
				bool_14 = value;
				bool_0 = true;
			}
		}
	}

	public bool Boolean_6
	{
		get
		{
			Vector4 vector4_ = Vector4_3;
			return vector4_.x != 0f || vector4_.y != 0f || vector4_.z != 0f || vector4_.w != 0f;
		}
	}

	public virtual bool Boolean_9
	{
		get
		{
			return false;
		}
	}

	public virtual float Single_1
	{
		get
		{
			return 1f;
		}
	}

	private Vector4 Vector4_1
	{
		get
		{
			switch (flip_0)
			{
			default:
				return new Vector4(rect_1.xMin, rect_1.yMin, rect_1.xMax, rect_1.yMax);
			case Flip.Horizontally:
				return new Vector4(rect_1.xMax, rect_1.yMin, rect_1.xMin, rect_1.yMax);
			case Flip.Vertically:
				return new Vector4(rect_1.xMin, rect_1.yMax, rect_1.xMax, rect_1.yMin);
			case Flip.Both:
				return new Vector4(rect_1.xMax, rect_1.yMax, rect_1.xMin, rect_1.yMin);
			}
		}
	}

	private Color32 Color32_0
	{
		get
		{
			Color color = base.Color_0;
			color.a = finalAlpha;
			return (!Boolean_9) ? color : NGUITools.ApplyPMA(color);
		}
	}

	protected void Fill(BetterList<Vector3> betterList_1, BetterList<Vector2> betterList_2, BetterList<Color32> betterList_3, Rect rect_2, Rect rect_3)
	{
		rect_1 = rect_2;
		rect_0 = rect_3;
		switch (Type_0)
		{
		case Type.Simple:
			SimpleFill(betterList_1, betterList_2, betterList_3);
			break;
		case Type.Sliced:
			SlicedFill(betterList_1, betterList_2, betterList_3);
			break;
		case Type.Tiled:
			TiledFill(betterList_1, betterList_2, betterList_3);
			break;
		case Type.Filled:
			FilledFill(betterList_1, betterList_2, betterList_3);
			break;
		case Type.Advanced:
			AdvancedFill(betterList_1, betterList_2, betterList_3);
			break;
		}
	}

	private void SimpleFill(BetterList<Vector3> betterList_1, BetterList<Vector2> betterList_2, BetterList<Color32> betterList_3)
	{
		Vector4 vector4_ = Vector4_2;
		Vector4 vector4_2 = Vector4_1;
		Color32 color32_ = Color32_0;
		betterList_1.Add(new Vector3(vector4_.x, vector4_.y));
		betterList_1.Add(new Vector3(vector4_.x, vector4_.w));
		betterList_1.Add(new Vector3(vector4_.z, vector4_.w));
		betterList_1.Add(new Vector3(vector4_.z, vector4_.y));
		betterList_2.Add(new Vector2(vector4_2.x, vector4_2.y));
		betterList_2.Add(new Vector2(vector4_2.x, vector4_2.w));
		betterList_2.Add(new Vector2(vector4_2.z, vector4_2.w));
		betterList_2.Add(new Vector2(vector4_2.z, vector4_2.y));
		betterList_3.Add(color32_);
		betterList_3.Add(color32_);
		betterList_3.Add(color32_);
		betterList_3.Add(color32_);
	}

	private void SlicedFill(BetterList<Vector3> betterList_1, BetterList<Vector2> betterList_2, BetterList<Color32> betterList_3)
	{
		Vector4 vector = Vector4_3 * Single_1;
		if (vector.x == 0f && vector.y == 0f && vector.z == 0f && vector.w == 0f)
		{
			SimpleFill(betterList_1, betterList_2, betterList_3);
			return;
		}
		Color32 color32_ = Color32_0;
		Vector4 vector4_ = Vector4_2;
		vector2_0[0].x = vector4_.x;
		vector2_0[0].y = vector4_.y;
		vector2_0[3].x = vector4_.z;
		vector2_0[3].y = vector4_.w;
		if (flip_0 != Flip.Horizontally && flip_0 != Flip.Both)
		{
			vector2_0[1].x = vector2_0[0].x + vector.x;
			vector2_0[2].x = vector2_0[3].x - vector.z;
			vector2_1[0].x = rect_1.xMin;
			vector2_1[1].x = rect_0.xMin;
			vector2_1[2].x = rect_0.xMax;
			vector2_1[3].x = rect_1.xMax;
		}
		else
		{
			vector2_0[1].x = vector2_0[0].x + vector.z;
			vector2_0[2].x = vector2_0[3].x - vector.x;
			vector2_1[3].x = rect_1.xMin;
			vector2_1[2].x = rect_0.xMin;
			vector2_1[1].x = rect_0.xMax;
			vector2_1[0].x = rect_1.xMax;
		}
		if (flip_0 != Flip.Vertically && flip_0 != Flip.Both)
		{
			vector2_0[1].y = vector2_0[0].y + vector.y;
			vector2_0[2].y = vector2_0[3].y - vector.w;
			vector2_1[0].y = rect_1.yMin;
			vector2_1[1].y = rect_0.yMin;
			vector2_1[2].y = rect_0.yMax;
			vector2_1[3].y = rect_1.yMax;
		}
		else
		{
			vector2_0[1].y = vector2_0[0].y + vector.w;
			vector2_0[2].y = vector2_0[3].y - vector.y;
			vector2_1[3].y = rect_1.yMin;
			vector2_1[2].y = rect_0.yMin;
			vector2_1[1].y = rect_0.yMax;
			vector2_1[0].y = rect_1.yMax;
		}
		for (int i = 0; i < 3; i++)
		{
			int num = i + 1;
			for (int j = 0; j < 3; j++)
			{
				if (advancedType_0 != 0 || i != 1 || j != 1)
				{
					int num2 = j + 1;
					betterList_1.Add(new Vector3(vector2_0[i].x, vector2_0[j].y));
					betterList_1.Add(new Vector3(vector2_0[i].x, vector2_0[num2].y));
					betterList_1.Add(new Vector3(vector2_0[num].x, vector2_0[num2].y));
					betterList_1.Add(new Vector3(vector2_0[num].x, vector2_0[j].y));
					betterList_2.Add(new Vector2(vector2_1[i].x, vector2_1[j].y));
					betterList_2.Add(new Vector2(vector2_1[i].x, vector2_1[num2].y));
					betterList_2.Add(new Vector2(vector2_1[num].x, vector2_1[num2].y));
					betterList_2.Add(new Vector2(vector2_1[num].x, vector2_1[j].y));
					betterList_3.Add(color32_);
					betterList_3.Add(color32_);
					betterList_3.Add(color32_);
					betterList_3.Add(color32_);
				}
			}
		}
	}

	private void TiledFill(BetterList<Vector3> betterList_1, BetterList<Vector2> betterList_2, BetterList<Color32> betterList_3)
	{
		Texture texture_ = Texture_0;
		if (texture_ == null)
		{
			return;
		}
		Vector2 vector = new Vector2(rect_0.width * (float)texture_.width, rect_0.height * (float)texture_.height);
		vector *= Single_1;
		if (texture_ == null || vector.x < 2f || !(vector.y >= 2f))
		{
			return;
		}
		Color32 color32_ = Color32_0;
		Vector4 vector4_ = Vector4_2;
		Vector4 vector2 = default(Vector4);
		if (flip_0 != Flip.Horizontally && flip_0 != Flip.Both)
		{
			vector2.x = rect_0.xMin;
			vector2.z = rect_0.xMax;
		}
		else
		{
			vector2.x = rect_0.xMax;
			vector2.z = rect_0.xMin;
		}
		if (flip_0 != Flip.Vertically && flip_0 != Flip.Both)
		{
			vector2.y = rect_0.yMin;
			vector2.w = rect_0.yMax;
		}
		else
		{
			vector2.y = rect_0.yMax;
			vector2.w = rect_0.yMin;
		}
		float x = vector4_.x;
		float num = vector4_.y;
		float x2 = vector2.x;
		float y = vector2.y;
		for (; num < vector4_.w; num += vector.y)
		{
			x = vector4_.x;
			float num2 = num + vector.y;
			float y2 = vector2.w;
			if (num2 > vector4_.w)
			{
				y2 = Mathf.Lerp(vector2.y, vector2.w, (vector4_.w - num) / vector.y);
				num2 = vector4_.w;
			}
			for (; x < vector4_.z; x += vector.x)
			{
				float num3 = x + vector.x;
				float x3 = vector2.z;
				if (num3 > vector4_.z)
				{
					x3 = Mathf.Lerp(vector2.x, vector2.z, (vector4_.z - x) / vector.x);
					num3 = vector4_.z;
				}
				betterList_1.Add(new Vector3(x, num));
				betterList_1.Add(new Vector3(x, num2));
				betterList_1.Add(new Vector3(num3, num2));
				betterList_1.Add(new Vector3(num3, num));
				betterList_2.Add(new Vector2(x2, y));
				betterList_2.Add(new Vector2(x2, y2));
				betterList_2.Add(new Vector2(x3, y2));
				betterList_2.Add(new Vector2(x3, y));
				betterList_3.Add(color32_);
				betterList_3.Add(color32_);
				betterList_3.Add(color32_);
				betterList_3.Add(color32_);
			}
		}
	}

	private void FilledFill(BetterList<Vector3> betterList_1, BetterList<Vector2> betterList_2, BetterList<Color32> betterList_3)
	{
		if (float_2 < 0.001f)
		{
			return;
		}
		Vector4 vector4_ = Vector4_2;
		Vector4 vector4_2 = Vector4_1;
		Color32 color32_ = Color32_0;
		if (fillDirection_0 == FillDirection.Horizontal || fillDirection_0 == FillDirection.Vertical)
		{
			if (fillDirection_0 == FillDirection.Horizontal)
			{
				float num = (vector4_2.z - vector4_2.x) * float_2;
				if (bool_14)
				{
					vector4_.x = vector4_.z - (vector4_.z - vector4_.x) * float_2;
					vector4_2.x = vector4_2.z - num;
				}
				else
				{
					vector4_.z = vector4_.x + (vector4_.z - vector4_.x) * float_2;
					vector4_2.z = vector4_2.x + num;
				}
			}
			else if (fillDirection_0 == FillDirection.Vertical)
			{
				float num2 = (vector4_2.w - vector4_2.y) * float_2;
				if (bool_14)
				{
					vector4_.y = vector4_.w - (vector4_.w - vector4_.y) * float_2;
					vector4_2.y = vector4_2.w - num2;
				}
				else
				{
					vector4_.w = vector4_.y + (vector4_.w - vector4_.y) * float_2;
					vector4_2.w = vector4_2.y + num2;
				}
			}
		}
		vector2_0[0] = new Vector2(vector4_.x, vector4_.y);
		vector2_0[1] = new Vector2(vector4_.x, vector4_.w);
		vector2_0[2] = new Vector2(vector4_.z, vector4_.w);
		vector2_0[3] = new Vector2(vector4_.z, vector4_.y);
		vector2_1[0] = new Vector2(vector4_2.x, vector4_2.y);
		vector2_1[1] = new Vector2(vector4_2.x, vector4_2.w);
		vector2_1[2] = new Vector2(vector4_2.z, vector4_2.w);
		vector2_1[3] = new Vector2(vector4_2.z, vector4_2.y);
		if (float_2 < 1f)
		{
			if (fillDirection_0 == FillDirection.Radial90)
			{
				if (RadialCut(vector2_0, vector2_1, float_2, bool_14, 0))
				{
					for (int i = 0; i < 4; i++)
					{
						betterList_1.Add(vector2_0[i]);
						betterList_2.Add(vector2_1[i]);
						betterList_3.Add(color32_);
					}
				}
				return;
			}
			if (fillDirection_0 == FillDirection.Radial180)
			{
				for (int j = 0; j < 2; j++)
				{
					float t = 0f;
					float t2 = 1f;
					float t3;
					float t4;
					if (j == 0)
					{
						t3 = 0f;
						t4 = 0.5f;
					}
					else
					{
						t3 = 0.5f;
						t4 = 1f;
					}
					vector2_0[0].x = Mathf.Lerp(vector4_.x, vector4_.z, t3);
					vector2_0[1].x = vector2_0[0].x;
					vector2_0[2].x = Mathf.Lerp(vector4_.x, vector4_.z, t4);
					vector2_0[3].x = vector2_0[2].x;
					vector2_0[0].y = Mathf.Lerp(vector4_.y, vector4_.w, t);
					vector2_0[1].y = Mathf.Lerp(vector4_.y, vector4_.w, t2);
					vector2_0[2].y = vector2_0[1].y;
					vector2_0[3].y = vector2_0[0].y;
					vector2_1[0].x = Mathf.Lerp(vector4_2.x, vector4_2.z, t3);
					vector2_1[1].x = vector2_1[0].x;
					vector2_1[2].x = Mathf.Lerp(vector4_2.x, vector4_2.z, t4);
					vector2_1[3].x = vector2_1[2].x;
					vector2_1[0].y = Mathf.Lerp(vector4_2.y, vector4_2.w, t);
					vector2_1[1].y = Mathf.Lerp(vector4_2.y, vector4_2.w, t2);
					vector2_1[2].y = vector2_1[1].y;
					vector2_1[3].y = vector2_1[0].y;
					float value = (bool_14 ? (float_2 * 2f - (float)(1 - j)) : (Single_0 * 2f - (float)j));
					if (RadialCut(vector2_0, vector2_1, Mathf.Clamp01(value), !bool_14, NGUIMath.RepeatIndex(j + 3, 4)))
					{
						for (int k = 0; k < 4; k++)
						{
							betterList_1.Add(vector2_0[k]);
							betterList_2.Add(vector2_1[k]);
							betterList_3.Add(color32_);
						}
					}
				}
				return;
			}
			if (fillDirection_0 == FillDirection.Radial360)
			{
				for (int l = 0; l < 4; l++)
				{
					float t5;
					float t6;
					if (l < 2)
					{
						t5 = 0f;
						t6 = 0.5f;
					}
					else
					{
						t5 = 0.5f;
						t6 = 1f;
					}
					float t7;
					float t8;
					if (l != 0 && l != 3)
					{
						t7 = 0.5f;
						t8 = 1f;
					}
					else
					{
						t7 = 0f;
						t8 = 0.5f;
					}
					vector2_0[0].x = Mathf.Lerp(vector4_.x, vector4_.z, t5);
					vector2_0[1].x = vector2_0[0].x;
					vector2_0[2].x = Mathf.Lerp(vector4_.x, vector4_.z, t6);
					vector2_0[3].x = vector2_0[2].x;
					vector2_0[0].y = Mathf.Lerp(vector4_.y, vector4_.w, t7);
					vector2_0[1].y = Mathf.Lerp(vector4_.y, vector4_.w, t8);
					vector2_0[2].y = vector2_0[1].y;
					vector2_0[3].y = vector2_0[0].y;
					vector2_1[0].x = Mathf.Lerp(vector4_2.x, vector4_2.z, t5);
					vector2_1[1].x = vector2_1[0].x;
					vector2_1[2].x = Mathf.Lerp(vector4_2.x, vector4_2.z, t6);
					vector2_1[3].x = vector2_1[2].x;
					vector2_1[0].y = Mathf.Lerp(vector4_2.y, vector4_2.w, t7);
					vector2_1[1].y = Mathf.Lerp(vector4_2.y, vector4_2.w, t8);
					vector2_1[2].y = vector2_1[1].y;
					vector2_1[3].y = vector2_1[0].y;
					float value2 = ((!bool_14) ? (float_2 * 4f - (float)(3 - NGUIMath.RepeatIndex(l + 2, 4))) : (float_2 * 4f - (float)NGUIMath.RepeatIndex(l + 2, 4)));
					if (RadialCut(vector2_0, vector2_1, Mathf.Clamp01(value2), bool_14, NGUIMath.RepeatIndex(l + 2, 4)))
					{
						for (int m = 0; m < 4; m++)
						{
							betterList_1.Add(vector2_0[m]);
							betterList_2.Add(vector2_1[m]);
							betterList_3.Add(color32_);
						}
					}
				}
				return;
			}
		}
		for (int n = 0; n < 4; n++)
		{
			betterList_1.Add(vector2_0[n]);
			betterList_2.Add(vector2_1[n]);
			betterList_3.Add(color32_);
		}
	}

	private void AdvancedFill(BetterList<Vector3> betterList_1, BetterList<Vector2> betterList_2, BetterList<Color32> betterList_3)
	{
		Texture texture_ = Texture_0;
		if (texture_ == null)
		{
			return;
		}
		Vector4 vector = Vector4_3 * Single_1;
		if (vector.x == 0f && vector.y == 0f && vector.z == 0f && vector.w == 0f)
		{
			SimpleFill(betterList_1, betterList_2, betterList_3);
			return;
		}
		Color32 color32_ = Color32_0;
		Vector4 vector4_ = Vector4_2;
		Vector2 vector2 = new Vector2(rect_0.width * (float)texture_.width, rect_0.height * (float)texture_.height);
		vector2 *= Single_1;
		if (vector2.x < 1f)
		{
			vector2.x = 1f;
		}
		if (vector2.y < 1f)
		{
			vector2.y = 1f;
		}
		vector2_0[0].x = vector4_.x;
		vector2_0[0].y = vector4_.y;
		vector2_0[3].x = vector4_.z;
		vector2_0[3].y = vector4_.w;
		if (flip_0 != Flip.Horizontally && flip_0 != Flip.Both)
		{
			vector2_0[1].x = vector2_0[0].x + vector.x;
			vector2_0[2].x = vector2_0[3].x - vector.z;
			vector2_1[0].x = rect_1.xMin;
			vector2_1[1].x = rect_0.xMin;
			vector2_1[2].x = rect_0.xMax;
			vector2_1[3].x = rect_1.xMax;
		}
		else
		{
			vector2_0[1].x = vector2_0[0].x + vector.z;
			vector2_0[2].x = vector2_0[3].x - vector.x;
			vector2_1[3].x = rect_1.xMin;
			vector2_1[2].x = rect_0.xMin;
			vector2_1[1].x = rect_0.xMax;
			vector2_1[0].x = rect_1.xMax;
		}
		if (flip_0 != Flip.Vertically && flip_0 != Flip.Both)
		{
			vector2_0[1].y = vector2_0[0].y + vector.y;
			vector2_0[2].y = vector2_0[3].y - vector.w;
			vector2_1[0].y = rect_1.yMin;
			vector2_1[1].y = rect_0.yMin;
			vector2_1[2].y = rect_0.yMax;
			vector2_1[3].y = rect_1.yMax;
		}
		else
		{
			vector2_0[1].y = vector2_0[0].y + vector.w;
			vector2_0[2].y = vector2_0[3].y - vector.y;
			vector2_1[3].y = rect_1.yMin;
			vector2_1[2].y = rect_0.yMin;
			vector2_1[1].y = rect_0.yMax;
			vector2_1[0].y = rect_1.yMax;
		}
		for (int i = 0; i < 3; i++)
		{
			int num = i + 1;
			for (int j = 0; j < 3; j++)
			{
				if (advancedType_0 == AdvancedType.Invisible && i == 1 && j == 1)
				{
					continue;
				}
				int num2 = j + 1;
				if (i == 1 && j == 1)
				{
					if (advancedType_0 == AdvancedType.Tiled)
					{
						float x = vector2_0[i].x;
						float x2 = vector2_0[num].x;
						float y = vector2_0[j].y;
						float y2 = vector2_0[num2].y;
						float x3 = vector2_1[i].x;
						float y3 = vector2_1[j].y;
						for (float num3 = y; num3 < y2; num3 += vector2.y)
						{
							float num4 = x;
							float num5 = vector2_1[num2].y;
							float num6 = num3 + vector2.y;
							if (num6 > y2)
							{
								num5 = Mathf.Lerp(y3, num5, (y2 - num3) / vector2.y);
								num6 = y2;
							}
							for (; num4 < x2; num4 += vector2.x)
							{
								float num7 = num4 + vector2.x;
								float num8 = vector2_1[num].x;
								if (num7 > x2)
								{
									num8 = Mathf.Lerp(x3, num8, (x2 - num4) / vector2.x);
									num7 = x2;
								}
								Fill(betterList_1, betterList_2, betterList_3, num4, num7, num3, num6, x3, num8, y3, num5, color32_);
							}
						}
					}
					else if (advancedType_0 == AdvancedType.Sliced)
					{
						Fill(betterList_1, betterList_2, betterList_3, vector2_0[i].x, vector2_0[num].x, vector2_0[j].y, vector2_0[num2].y, vector2_1[i].x, vector2_1[num].x, vector2_1[j].y, vector2_1[num2].y, color32_);
					}
				}
				else if (i == 1)
				{
					if ((j == 0 && advancedType_3 == AdvancedType.Tiled) || (j == 2 && advancedType_4 == AdvancedType.Tiled))
					{
						float x4 = vector2_0[i].x;
						float x5 = vector2_0[num].x;
						float y4 = vector2_0[j].y;
						float y5 = vector2_0[num2].y;
						float x6 = vector2_1[i].x;
						float y6 = vector2_1[j].y;
						float y7 = vector2_1[num2].y;
						for (float num9 = x4; num9 < x5; num9 += vector2.x)
						{
							float num10 = num9 + vector2.x;
							float num11 = vector2_1[num].x;
							if (num10 > x5)
							{
								num11 = Mathf.Lerp(x6, num11, (x5 - num9) / vector2.x);
								num10 = x5;
							}
							Fill(betterList_1, betterList_2, betterList_3, num9, num10, y4, y5, x6, num11, y6, y7, color32_);
						}
					}
					else if ((j == 0 && advancedType_3 == AdvancedType.Sliced) || (j == 2 && advancedType_4 == AdvancedType.Sliced))
					{
						Fill(betterList_1, betterList_2, betterList_3, vector2_0[i].x, vector2_0[num].x, vector2_0[j].y, vector2_0[num2].y, vector2_1[i].x, vector2_1[num].x, vector2_1[j].y, vector2_1[num2].y, color32_);
					}
				}
				else if (j == 1)
				{
					if ((i == 0 && advancedType_1 == AdvancedType.Tiled) || (i == 2 && advancedType_2 == AdvancedType.Tiled))
					{
						float x7 = vector2_0[i].x;
						float x8 = vector2_0[num].x;
						float y8 = vector2_0[j].y;
						float y9 = vector2_0[num2].y;
						float x9 = vector2_1[i].x;
						float x10 = vector2_1[num].x;
						float y10 = vector2_1[j].y;
						for (float num12 = y8; num12 < y9; num12 += vector2.y)
						{
							float num13 = vector2_1[num2].y;
							float num14 = num12 + vector2.y;
							if (num14 > y9)
							{
								num13 = Mathf.Lerp(y10, num13, (y9 - num12) / vector2.y);
								num14 = y9;
							}
							Fill(betterList_1, betterList_2, betterList_3, x7, x8, num12, num14, x9, x10, y10, num13, color32_);
						}
					}
					else if ((i == 0 && advancedType_1 == AdvancedType.Sliced) || (i == 2 && advancedType_2 == AdvancedType.Sliced))
					{
						Fill(betterList_1, betterList_2, betterList_3, vector2_0[i].x, vector2_0[num].x, vector2_0[j].y, vector2_0[num2].y, vector2_1[i].x, vector2_1[num].x, vector2_1[j].y, vector2_1[num2].y, color32_);
					}
				}
				else
				{
					Fill(betterList_1, betterList_2, betterList_3, vector2_0[i].x, vector2_0[num].x, vector2_0[j].y, vector2_0[num2].y, vector2_1[i].x, vector2_1[num].x, vector2_1[j].y, vector2_1[num2].y, color32_);
				}
			}
		}
	}

	private static bool RadialCut(Vector2[] vector2_2, Vector2[] vector2_3, float float_3, bool bool_15, int int_6)
	{
		if (float_3 < 0.001f)
		{
			return false;
		}
		if ((int_6 & 1) == 1)
		{
			bool_15 = !bool_15;
		}
		if (!bool_15 && float_3 > 0.999f)
		{
			return true;
		}
		float num = Mathf.Clamp01(float_3);
		if (bool_15)
		{
			num = 1f - num;
		}
		num *= (float)Math.PI / 2f;
		float float_4 = Mathf.Cos(num);
		float float_5 = Mathf.Sin(num);
		RadialCut(vector2_2, float_4, float_5, bool_15, int_6);
		RadialCut(vector2_3, float_4, float_5, bool_15, int_6);
		return true;
	}

	private static void RadialCut(Vector2[] vector2_2, float float_3, float float_4, bool bool_15, int int_6)
	{
		int num = NGUIMath.RepeatIndex(int_6 + 1, 4);
		int num2 = NGUIMath.RepeatIndex(int_6 + 2, 4);
		int num3 = NGUIMath.RepeatIndex(int_6 + 3, 4);
		if ((int_6 & 1) == 1)
		{
			if (float_4 > float_3)
			{
				float_3 /= float_4;
				float_4 = 1f;
				if (bool_15)
				{
					vector2_2[num].x = Mathf.Lerp(vector2_2[int_6].x, vector2_2[num2].x, float_3);
					vector2_2[num2].x = vector2_2[num].x;
				}
			}
			else if (float_3 > float_4)
			{
				float_4 /= float_3;
				float_3 = 1f;
				if (!bool_15)
				{
					vector2_2[num2].y = Mathf.Lerp(vector2_2[int_6].y, vector2_2[num2].y, float_4);
					vector2_2[num3].y = vector2_2[num2].y;
				}
			}
			else
			{
				float_3 = 1f;
				float_4 = 1f;
			}
			if (!bool_15)
			{
				vector2_2[num3].x = Mathf.Lerp(vector2_2[int_6].x, vector2_2[num2].x, float_3);
			}
			else
			{
				vector2_2[num].y = Mathf.Lerp(vector2_2[int_6].y, vector2_2[num2].y, float_4);
			}
			return;
		}
		if (float_3 > float_4)
		{
			float_4 /= float_3;
			float_3 = 1f;
			if (!bool_15)
			{
				vector2_2[num].y = Mathf.Lerp(vector2_2[int_6].y, vector2_2[num2].y, float_4);
				vector2_2[num2].y = vector2_2[num].y;
			}
		}
		else if (float_4 > float_3)
		{
			float_3 /= float_4;
			float_4 = 1f;
			if (bool_15)
			{
				vector2_2[num2].x = Mathf.Lerp(vector2_2[int_6].x, vector2_2[num2].x, float_3);
				vector2_2[num3].x = vector2_2[num2].x;
			}
		}
		else
		{
			float_3 = 1f;
			float_4 = 1f;
		}
		if (bool_15)
		{
			vector2_2[num3].y = Mathf.Lerp(vector2_2[int_6].y, vector2_2[num2].y, float_4);
		}
		else
		{
			vector2_2[num].x = Mathf.Lerp(vector2_2[int_6].x, vector2_2[num2].x, float_3);
		}
	}

	private static void Fill(BetterList<Vector3> betterList_1, BetterList<Vector2> betterList_2, BetterList<Color32> betterList_3, float float_3, float float_4, float float_5, float float_6, float float_7, float float_8, float float_9, float float_10, Color color_1)
	{
		betterList_1.Add(new Vector3(float_3, float_5));
		betterList_1.Add(new Vector3(float_3, float_6));
		betterList_1.Add(new Vector3(float_4, float_6));
		betterList_1.Add(new Vector3(float_4, float_5));
		betterList_2.Add(new Vector2(float_7, float_9));
		betterList_2.Add(new Vector2(float_7, float_10));
		betterList_2.Add(new Vector2(float_8, float_10));
		betterList_2.Add(new Vector2(float_8, float_9));
		betterList_3.Add(color_1);
		betterList_3.Add(color_1);
		betterList_3.Add(color_1);
		betterList_3.Add(color_1);
	}
}
