using System;
using System.Diagnostics;
using UnityEngine;

public class UIWidget : UIRect
{
	public enum Pivot
	{
		TopLeft = 0,
		Top = 1,
		TopRight = 2,
		Left = 3,
		Center = 4,
		Right = 5,
		BottomLeft = 6,
		Bottom = 7,
		BottomRight = 8
	}

	public enum AspectRatioSource
	{
		Free = 0,
		BasedOnWidth = 1,
		BasedOnHeight = 2
	}

	public delegate void OnDimensionsChanged();

	public delegate bool HitCheck(Vector3 vector3_0);

	[SerializeField]
	protected Color color_0 = Color.white;

	[SerializeField]
	protected Pivot pivot_0 = Pivot.Center;

	[SerializeField]
	protected int int_1 = 100;

	[SerializeField]
	protected int int_2 = 100;

	[SerializeField]
	protected int int_3;

	public OnDimensionsChanged onDimensionsChanged_0;

	public bool bool_6;

	public bool bool_7;

	public AspectRatioSource aspectRatioSource_0;

	public float float_0 = 1f;

	public HitCheck hitCheck_0;

	[NonSerialized]
	public UIPanel uipanel_0;

	[NonSerialized]
	public UIGeometry uigeometry_0 = new UIGeometry();

	[NonSerialized]
	public bool bool_8 = true;

	[NonSerialized]
	protected bool bool_9 = true;

	[NonSerialized]
	protected Vector4 vector4_0 = new Vector4(0f, 0f, 1f, 1f);

	[NonSerialized]
	private Matrix4x4 matrix4x4_0;

	[NonSerialized]
	private bool bool_10 = true;

	[NonSerialized]
	private bool bool_11 = true;

	[NonSerialized]
	private bool bool_12 = true;

	[NonSerialized]
	private float float_1;

	[NonSerialized]
	private bool bool_13;

	[NonSerialized]
	public UIDrawCall uidrawCall_0;

	[NonSerialized]
	protected Vector3[] vector3_1 = new Vector3[4];

	[NonSerialized]
	private int int_4 = -1;

	private int int_5 = -1;

	private Vector3 vector3_2;

	private Vector3 vector3_3;

	public Vector4 Vector4_0
	{
		get
		{
			return vector4_0;
		}
		set
		{
			if (vector4_0 != value)
			{
				vector4_0 = value;
				if (bool_6)
				{
					ResizeCollider();
				}
				MarkAsChanged();
			}
		}
	}

	public Vector2 Vector2_0
	{
		get
		{
			return NGUIMath.GetPivotOffset(Pivot_1);
		}
	}

	public int Int32_0
	{
		get
		{
			return int_1;
		}
		set
		{
			int int32_ = Int32_4;
			if (value < int32_)
			{
				value = int32_;
			}
			if (int_1 == value || aspectRatioSource_0 == AspectRatioSource.BasedOnHeight)
			{
				return;
			}
			if (Boolean_12)
			{
				if (leftAnchor.target != null && rightAnchor.target != null)
				{
					if (pivot_0 != Pivot.BottomLeft && pivot_0 != Pivot.Left && pivot_0 != 0)
					{
						if (pivot_0 != Pivot.BottomRight && pivot_0 != Pivot.Right && pivot_0 != Pivot.TopRight)
						{
							int num = value - int_1;
							num -= num & 1;
							if (num != 0)
							{
								NGUIMath.AdjustWidget(this, (float)(-num) * 0.5f, 0f, (float)num * 0.5f, 0f);
							}
						}
						else
						{
							NGUIMath.AdjustWidget(this, int_1 - value, 0f, 0f, 0f);
						}
					}
					else
					{
						NGUIMath.AdjustWidget(this, 0f, 0f, value - int_1, 0f);
					}
				}
				else if (leftAnchor.target != null)
				{
					NGUIMath.AdjustWidget(this, 0f, 0f, value - int_1, 0f);
				}
				else
				{
					NGUIMath.AdjustWidget(this, int_1 - value, 0f, 0f, 0f);
				}
			}
			else
			{
				SetDimensions(value, int_2);
			}
		}
	}

	public int Int32_1
	{
		get
		{
			return int_2;
		}
		set
		{
			int int32_ = Int32_5;
			if (value < int32_)
			{
				value = int32_;
			}
			if (int_2 == value || aspectRatioSource_0 == AspectRatioSource.BasedOnWidth)
			{
				return;
			}
			if (Boolean_13)
			{
				if (bottomAnchor.target != null && topAnchor.target != null)
				{
					if (pivot_0 != Pivot.BottomLeft && pivot_0 != Pivot.Bottom && pivot_0 != Pivot.BottomRight)
					{
						if (pivot_0 != 0 && pivot_0 != Pivot.Top && pivot_0 != Pivot.TopRight)
						{
							int num = value - int_2;
							num -= num & 1;
							if (num != 0)
							{
								NGUIMath.AdjustWidget(this, 0f, (float)(-num) * 0.5f, 0f, (float)num * 0.5f);
							}
						}
						else
						{
							NGUIMath.AdjustWidget(this, 0f, int_2 - value, 0f, 0f);
						}
					}
					else
					{
						NGUIMath.AdjustWidget(this, 0f, 0f, 0f, value - int_2);
					}
				}
				else if (bottomAnchor.target != null)
				{
					NGUIMath.AdjustWidget(this, 0f, 0f, 0f, value - int_2);
				}
				else
				{
					NGUIMath.AdjustWidget(this, 0f, int_2 - value, 0f, 0f);
				}
			}
			else
			{
				SetDimensions(int_1, value);
			}
		}
	}

	public Color Color_0
	{
		get
		{
			return color_0;
		}
		set
		{
			if (color_0 != value)
			{
				bool bool_ = color_0.a != value.a;
				color_0 = value;
				Invalidate(bool_);
			}
		}
	}

	public override float Single_2
	{
		get
		{
			return color_0.a;
		}
		set
		{
			if (color_0.a != value)
			{
				color_0.a = value;
				Invalidate(true);
			}
		}
	}

	public bool Boolean_2
	{
		get
		{
			return bool_11 && bool_10 && bool_12 && finalAlpha > 0.001f && NGUITools.GetActive(this);
		}
	}

	public bool Boolean_3
	{
		get
		{
			return uigeometry_0 != null && uigeometry_0.Boolean_0;
		}
	}

	public Pivot Pivot_0
	{
		get
		{
			return pivot_0;
		}
		set
		{
			if (pivot_0 != value)
			{
				pivot_0 = value;
				if (bool_6)
				{
					ResizeCollider();
				}
				MarkAsChanged();
			}
		}
	}

	public Pivot Pivot_1
	{
		get
		{
			return pivot_0;
		}
		set
		{
			if (pivot_0 != value)
			{
				Vector3 vector = Vector3_3[0];
				pivot_0 = value;
				bool_0 = true;
				Vector3 vector2 = Vector3_3[0];
				Transform transform = base.Transform_0;
				Vector3 position = transform.position;
				float z = transform.localPosition.z;
				position.x += vector.x - vector2.x;
				position.y += vector.y - vector2.y;
				base.Transform_0.position = position;
				position = base.Transform_0.localPosition;
				position.x = Mathf.Round(position.x);
				position.y = Mathf.Round(position.y);
				position.z = z;
				base.Transform_0.localPosition = position;
			}
		}
	}

	public int Int32_2
	{
		get
		{
			return int_3;
		}
		set
		{
			if (int_3 == value)
			{
				return;
			}
			if (uipanel_0 != null)
			{
				uipanel_0.RemoveWidget(this);
			}
			int_3 = value;
			if (uipanel_0 != null)
			{
				uipanel_0.AddWidget(this);
				if (!Application.isPlaying)
				{
					uipanel_0.SortWidgets();
					uipanel_0.RebuildAllDrawCalls();
				}
			}
		}
	}

	public int Int32_3
	{
		get
		{
			if (uipanel_0 == null)
			{
				CreatePanel();
			}
			return (!(uipanel_0 != null)) ? int_3 : (int_3 + uipanel_0.Int32_1 * 1000);
		}
	}

	public override Vector3[] Vector3_2
	{
		get
		{
			Vector2 vector2_ = Vector2_0;
			float num = (0f - vector2_.x) * (float)int_1;
			float num2 = (0f - vector2_.y) * (float)int_2;
			float x = num + (float)int_1;
			float y = num2 + (float)int_2;
			vector3_1[0] = new Vector3(num, num2);
			vector3_1[1] = new Vector3(num, y);
			vector3_1[2] = new Vector3(x, y);
			vector3_1[3] = new Vector3(x, num2);
			return vector3_1;
		}
	}

	public virtual Vector2 Vector2_4
	{
		get
		{
			Vector3[] array = Vector3_2;
			return array[2] - array[0];
		}
	}

	public Vector3 Vector3_0
	{
		get
		{
			Vector3[] array = Vector3_2;
			return Vector3.Lerp(array[0], array[2], 0.5f);
		}
	}

	public override Vector3[] Vector3_3
	{
		get
		{
			Vector2 vector2_ = Vector2_0;
			float num = (0f - vector2_.x) * (float)int_1;
			float num2 = (0f - vector2_.y) * (float)int_2;
			float x = num + (float)int_1;
			float y = num2 + (float)int_2;
			Transform transform = base.Transform_0;
			vector3_1[0] = transform.TransformPoint(num, num2, 0f);
			vector3_1[1] = transform.TransformPoint(num, y, 0f);
			vector3_1[2] = transform.TransformPoint(x, y, 0f);
			vector3_1[3] = transform.TransformPoint(x, num2, 0f);
			return vector3_1;
		}
	}

	public Vector3 Vector3_1
	{
		get
		{
			return base.Transform_0.TransformPoint(Vector3_0);
		}
	}

	public virtual Vector4 Vector4_2
	{
		get
		{
			Vector2 vector2_ = Vector2_0;
			float num = (0f - vector2_.x) * (float)int_1;
			float num2 = (0f - vector2_.y) * (float)int_2;
			float num3 = num + (float)int_1;
			float num4 = num2 + (float)int_2;
			return new Vector4((vector4_0.x != 0f) ? Mathf.Lerp(num, num3, vector4_0.x) : num, (vector4_0.y != 0f) ? Mathf.Lerp(num2, num4, vector4_0.y) : num2, (vector4_0.z != 1f) ? Mathf.Lerp(num, num3, vector4_0.z) : num3, (vector4_0.w != 1f) ? Mathf.Lerp(num2, num4, vector4_0.w) : num4);
		}
	}

	public virtual Material Material_0
	{
		get
		{
			return null;
		}
		set
		{
			throw new NotImplementedException(string.Concat(GetType(), " has no material setter"));
		}
	}

	public virtual Texture Texture_0
	{
		get
		{
			Material material_ = Material_0;
			return (!(material_ != null)) ? null : material_.mainTexture;
		}
		set
		{
			throw new NotImplementedException(string.Concat(GetType(), " has no mainTexture setter"));
		}
	}

	public virtual Shader Shader_0
	{
		get
		{
			Material material_ = Material_0;
			return (!(material_ != null)) ? null : material_.shader;
		}
		set
		{
			throw new NotImplementedException(string.Concat(GetType(), " has no shader setter"));
		}
	}

	[Obsolete("There is no relative scale anymore. Widgets now have width and height instead")]
	public Vector2 Vector2_1
	{
		get
		{
			return Vector2.one;
		}
	}

	public bool Boolean_4
	{
		get
		{
			BoxCollider boxCollider = base.GetComponent<Collider>() as BoxCollider;
			if (boxCollider != null)
			{
				return true;
			}
			return GetComponent<BoxCollider2D>() != null;
		}
	}

	public virtual int Int32_4
	{
		get
		{
			return 2;
		}
	}

	public virtual int Int32_5
	{
		get
		{
			return 2;
		}
	}

	public virtual Vector4 Vector4_3
	{
		get
		{
			return Vector4.zero;
		}
		set
		{
		}
	}

	public void SetDimensions(int int_6, int int_7)
	{
		if (int_1 != int_6 || int_2 != int_7)
		{
			int_1 = int_6;
			int_2 = int_7;
			if (aspectRatioSource_0 == AspectRatioSource.BasedOnWidth)
			{
				int_2 = Mathf.RoundToInt((float)int_1 / float_0);
			}
			else if (aspectRatioSource_0 == AspectRatioSource.BasedOnHeight)
			{
				int_1 = Mathf.RoundToInt((float)int_2 * float_0);
			}
			else if (aspectRatioSource_0 == AspectRatioSource.Free)
			{
				float_0 = (float)int_1 / (float)int_2;
			}
			bool_13 = true;
			if (bool_6)
			{
				ResizeCollider();
			}
			MarkAsChanged();
		}
	}

	public override Vector3[] GetSides(Transform transform_1)
	{
		Vector2 vector2_ = Vector2_0;
		float num = (0f - vector2_.x) * (float)int_1;
		float num2 = (0f - vector2_.y) * (float)int_2;
		float num3 = num + (float)int_1;
		float num4 = num2 + (float)int_2;
		float x = (num + num3) * 0.5f;
		float y = (num2 + num4) * 0.5f;
		Transform transform = base.Transform_0;
		vector3_1[0] = transform.TransformPoint(num, y, 0f);
		vector3_1[1] = transform.TransformPoint(x, num4, 0f);
		vector3_1[2] = transform.TransformPoint(num3, y, 0f);
		vector3_1[3] = transform.TransformPoint(x, num2, 0f);
		if (transform_1 != null)
		{
			for (int i = 0; i < 4; i++)
			{
				vector3_1[i] = transform_1.InverseTransformPoint(vector3_1[i]);
			}
		}
		return vector3_1;
	}

	public override float CalculateFinalAlpha(int int_6)
	{
		if (int_4 != int_6)
		{
			int_4 = int_6;
			UpdateFinalAlpha(int_6);
		}
		return finalAlpha;
	}

	protected void UpdateFinalAlpha(int int_6)
	{
		if (bool_10 && bool_12)
		{
			UIRect uIRect_ = base.UIRect_0;
			finalAlpha = ((!(base.UIRect_0 != null)) ? color_0.a : (uIRect_.CalculateFinalAlpha(int_6) * color_0.a));
		}
		else
		{
			finalAlpha = 0f;
		}
	}

	public override void Invalidate(bool bool_14)
	{
		bool_0 = true;
		int_4 = -1;
		if (uipanel_0 != null)
		{
			bool bool_15 = (!bool_7 && !uipanel_0.Boolean_5) || uipanel_0.IsVisible(this);
			UpdateVisibility(CalculateCumulativeAlpha(Time.frameCount) > 0.001f, bool_15);
			UpdateFinalAlpha(Time.frameCount);
			if (bool_14)
			{
				base.Invalidate(true);
			}
		}
	}

	public float CalculateCumulativeAlpha(int int_6)
	{
		UIRect uIRect_ = base.UIRect_0;
		return (!(uIRect_ != null)) ? color_0.a : (uIRect_.CalculateFinalAlpha(int_6) * color_0.a);
	}

	public override void SetRect(float float_2, float float_3, float float_4, float float_5)
	{
		Vector2 vector2_ = Vector2_0;
		float num = Mathf.Lerp(float_2, float_2 + float_4, vector2_.x);
		float num2 = Mathf.Lerp(float_3, float_3 + float_5, vector2_.y);
		int num3 = Mathf.FloorToInt(float_4 + 0.5f);
		int num4 = Mathf.FloorToInt(float_5 + 0.5f);
		if (vector2_.x == 0.5f)
		{
			num3 = num3 >> 1 << 1;
		}
		if (vector2_.y == 0.5f)
		{
			num4 = num4 >> 1 << 1;
		}
		Transform transform = base.Transform_0;
		Vector3 localPosition = transform.localPosition;
		localPosition.x = Mathf.Floor(num + 0.5f);
		localPosition.y = Mathf.Floor(num2 + 0.5f);
		if (num3 < Int32_4)
		{
			num3 = Int32_4;
		}
		if (num4 < Int32_5)
		{
			num4 = Int32_5;
		}
		transform.localPosition = localPosition;
		Int32_0 = num3;
		Int32_1 = num4;
		if (base.Boolean_1)
		{
			transform = transform.parent;
			if ((bool)leftAnchor.target)
			{
				leftAnchor.SetHorizontal(transform, float_2);
			}
			if ((bool)rightAnchor.target)
			{
				rightAnchor.SetHorizontal(transform, float_2 + float_4);
			}
			if ((bool)bottomAnchor.target)
			{
				bottomAnchor.SetVertical(transform, float_3);
			}
			if ((bool)topAnchor.target)
			{
				topAnchor.SetVertical(transform, float_3 + float_5);
			}
		}
	}

	public void ResizeCollider()
	{
		if (NGUITools.GetActive(this))
		{
			NGUITools.UpdateWidgetCollider(base.gameObject);
		}
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static int FullCompareFunc(UIWidget uiwidget_0, UIWidget uiwidget_1)
	{
		int num = UIPanel.CompareFunc(uiwidget_0.uipanel_0, uiwidget_1.uipanel_0);
		return (num != 0) ? num : PanelCompareFunc(uiwidget_0, uiwidget_1);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static int PanelCompareFunc(UIWidget uiwidget_0, UIWidget uiwidget_1)
	{
		if (uiwidget_0.int_3 < uiwidget_1.int_3)
		{
			return -1;
		}
		if (uiwidget_0.int_3 > uiwidget_1.int_3)
		{
			return 1;
		}
		Material material_ = uiwidget_0.Material_0;
		Material material_2 = uiwidget_1.Material_0;
		if (material_ == material_2)
		{
			return 0;
		}
		if (material_ != null)
		{
			return -1;
		}
		if (material_2 != null)
		{
			return 1;
		}
		return (material_.GetInstanceID() >= material_2.GetInstanceID()) ? 1 : (-1);
	}

	public Bounds CalculateBounds()
	{
		return CalculateBounds(null);
	}

	public Bounds CalculateBounds(Transform transform_1)
	{
		if (transform_1 == null)
		{
			Vector3[] array = Vector3_2;
			Bounds result = new Bounds(array[0], Vector3.zero);
			for (int i = 1; i < 4; i++)
			{
				result.Encapsulate(array[i]);
			}
			return result;
		}
		Matrix4x4 worldToLocalMatrix = transform_1.worldToLocalMatrix;
		Vector3[] array2 = Vector3_3;
		Bounds result2 = new Bounds(worldToLocalMatrix.MultiplyPoint3x4(array2[0]), Vector3.zero);
		for (int j = 1; j < 4; j++)
		{
			result2.Encapsulate(worldToLocalMatrix.MultiplyPoint3x4(array2[j]));
		}
		return result2;
	}

	public void SetDirty()
	{
		if (uidrawCall_0 != null)
		{
			uidrawCall_0.isDirty = true;
		}
		else if (Boolean_2 && Boolean_3)
		{
			CreatePanel();
		}
	}

	protected void RemoveFromPanel()
	{
		if (uipanel_0 != null)
		{
			uipanel_0.RemoveWidget(this);
			uipanel_0 = null;
		}
	}

	public virtual void MarkAsChanged()
	{
		if (NGUITools.GetActive(this))
		{
			bool_0 = true;
			if (uipanel_0 != null && base.enabled && NGUITools.GetActive(base.gameObject) && !bool_9)
			{
				SetDirty();
				CheckLayer();
			}
		}
	}

	public UIPanel CreatePanel()
	{
		if (bool_1 && uipanel_0 == null && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			uipanel_0 = UIPanel.Find(base.Transform_0, true, base.GameObject_0.layer);
			if (uipanel_0 != null)
			{
				bool_2 = false;
				uipanel_0.AddWidget(this);
				CheckLayer();
				Invalidate(true);
			}
		}
		return uipanel_0;
	}

	public void CheckLayer()
	{
		if (uipanel_0 != null && uipanel_0.gameObject.layer != base.gameObject.layer)
		{
			UnityEngine.Debug.LogWarning("You can't place widgets on a layer different than the UIPanel that manages them.\nIf you want to move widgets to a different layer, parent them to a new panel instead.", this);
			base.gameObject.layer = uipanel_0.gameObject.layer;
		}
	}

	public override void ParentHasChanged()
	{
		base.ParentHasChanged();
		if (uipanel_0 != null)
		{
			UIPanel uIPanel = UIPanel.Find(base.Transform_0, true, base.GameObject_0.layer);
			if (uipanel_0 != uIPanel)
			{
				RemoveFromPanel();
				CreatePanel();
			}
		}
	}

	protected virtual void Awake()
	{
		gameObject_0 = base.gameObject;
		bool_9 = Application.isPlaying;
	}

	protected override void OnInit()
	{
		base.OnInit();
		RemoveFromPanel();
		bool_13 = true;
		if (int_1 == 100 && int_2 == 100 && base.Transform_0.localScale.magnitude > 8f)
		{
			UpgradeFrom265();
			base.Transform_0.localScale = Vector3.one;
		}
		Update();
	}

	protected virtual void UpgradeFrom265()
	{
		Vector3 localScale = base.Transform_0.localScale;
		int_1 = Mathf.Abs(Mathf.RoundToInt(localScale.x));
		int_2 = Mathf.Abs(Mathf.RoundToInt(localScale.y));
		NGUITools.UpdateWidgetCollider(base.gameObject, true);
	}

	protected override void OnStart()
	{
		CreatePanel();
	}

	protected override void OnAnchor()
	{
		Transform transform = base.Transform_0;
		Transform parent = transform.parent;
		Vector3 localPosition = transform.localPosition;
		Vector2 vector2_ = Vector2_0;
		float num;
		float num2;
		float num3;
		float num4;
		if (leftAnchor.target == bottomAnchor.target && leftAnchor.target == rightAnchor.target && leftAnchor.target == topAnchor.target)
		{
			Vector3[] sides = leftAnchor.GetSides(parent);
			if (sides != null)
			{
				num = NGUIMath.Lerp(sides[0].x, sides[2].x, leftAnchor.relative) + (float)leftAnchor.absolute;
				num2 = NGUIMath.Lerp(sides[0].x, sides[2].x, rightAnchor.relative) + (float)rightAnchor.absolute;
				num3 = NGUIMath.Lerp(sides[3].y, sides[1].y, bottomAnchor.relative) + (float)bottomAnchor.absolute;
				num4 = NGUIMath.Lerp(sides[3].y, sides[1].y, topAnchor.relative) + (float)topAnchor.absolute;
				bool_12 = true;
			}
			else
			{
				Vector3 localPos = GetLocalPos(leftAnchor, parent);
				num = localPos.x + (float)leftAnchor.absolute;
				num3 = localPos.y + (float)bottomAnchor.absolute;
				num2 = localPos.x + (float)rightAnchor.absolute;
				num4 = localPos.y + (float)topAnchor.absolute;
				bool_12 = !bool_7 || localPos.z >= 0f;
			}
		}
		else
		{
			bool_12 = true;
			if ((bool)leftAnchor.target)
			{
				Vector3[] sides2 = leftAnchor.GetSides(parent);
				num = ((sides2 == null) ? (GetLocalPos(leftAnchor, parent).x + (float)leftAnchor.absolute) : (NGUIMath.Lerp(sides2[0].x, sides2[2].x, leftAnchor.relative) + (float)leftAnchor.absolute));
			}
			else
			{
				num = localPosition.x - vector2_.x * (float)int_1;
			}
			if ((bool)rightAnchor.target)
			{
				Vector3[] sides3 = rightAnchor.GetSides(parent);
				num2 = ((sides3 == null) ? (GetLocalPos(rightAnchor, parent).x + (float)rightAnchor.absolute) : (NGUIMath.Lerp(sides3[0].x, sides3[2].x, rightAnchor.relative) + (float)rightAnchor.absolute));
			}
			else
			{
				num2 = localPosition.x - vector2_.x * (float)int_1 + (float)int_1;
			}
			if ((bool)bottomAnchor.target)
			{
				Vector3[] sides4 = bottomAnchor.GetSides(parent);
				num3 = ((sides4 == null) ? (GetLocalPos(bottomAnchor, parent).y + (float)bottomAnchor.absolute) : (NGUIMath.Lerp(sides4[3].y, sides4[1].y, bottomAnchor.relative) + (float)bottomAnchor.absolute));
			}
			else
			{
				num3 = localPosition.y - vector2_.y * (float)int_2;
			}
			if ((bool)topAnchor.target)
			{
				Vector3[] sides5 = topAnchor.GetSides(parent);
				num4 = ((sides5 == null) ? (GetLocalPos(topAnchor, parent).y + (float)topAnchor.absolute) : (NGUIMath.Lerp(sides5[3].y, sides5[1].y, topAnchor.relative) + (float)topAnchor.absolute));
			}
			else
			{
				num4 = localPosition.y - vector2_.y * (float)int_2 + (float)int_2;
			}
		}
		Vector3 vector = new Vector3(Mathf.Lerp(num, num2, vector2_.x), Mathf.Lerp(num3, num4, vector2_.y), localPosition.z);
		int num5 = Mathf.FloorToInt(num2 - num + 0.5f);
		int num6 = Mathf.FloorToInt(num4 - num3 + 0.5f);
		if (aspectRatioSource_0 != 0 && float_0 != 0f)
		{
			if (aspectRatioSource_0 == AspectRatioSource.BasedOnHeight)
			{
				num5 = Mathf.RoundToInt((float)num6 * float_0);
			}
			else
			{
				num6 = Mathf.RoundToInt((float)num5 / float_0);
			}
		}
		if (num5 < Int32_4)
		{
			num5 = Int32_4;
		}
		if (num6 < Int32_5)
		{
			num6 = Int32_5;
		}
		if (Vector3.SqrMagnitude(localPosition - vector) > 0.001f)
		{
			base.Transform_0.localPosition = vector;
			if (bool_12)
			{
				bool_0 = true;
			}
		}
		if (int_1 != num5 || int_2 != num6)
		{
			int_1 = num5;
			int_2 = num6;
			if (bool_12)
			{
				bool_0 = true;
			}
			if (bool_6)
			{
				ResizeCollider();
			}
		}
	}

	protected override void OnUpdate()
	{
		if (uipanel_0 == null)
		{
			CreatePanel();
		}
	}

	private void OnApplicationPause(bool bool_14)
	{
		if (!bool_14)
		{
			MarkAsChanged();
		}
	}

	protected override void OnDisable()
	{
		RemoveFromPanel();
		base.OnDisable();
	}

	private void OnDestroy()
	{
		RemoveFromPanel();
	}

	public bool UpdateVisibility(bool bool_14, bool bool_15)
	{
		if (bool_10 == bool_14 && bool_11 == bool_15)
		{
			return false;
		}
		bool_0 = true;
		bool_10 = bool_14;
		bool_11 = bool_15;
		return true;
	}

	public bool UpdateTransform(int int_6)
	{
		if (!bool_13 && !uipanel_0.bool_8 && base.Transform_0.hasChanged)
		{
			transform_0.hasChanged = false;
			matrix4x4_0 = uipanel_0.matrix4x4_0 * base.Transform_0.localToWorldMatrix;
			int_5 = int_6;
			Vector2 vector2_ = Vector2_0;
			float num = (0f - vector2_.x) * (float)int_1;
			float num2 = (0f - vector2_.y) * (float)int_2;
			float x = num + (float)int_1;
			float y = num2 + (float)int_2;
			Transform transform = base.Transform_0;
			Vector3 v = transform.TransformPoint(num, num2, 0f);
			Vector3 v2 = transform.TransformPoint(x, y, 0f);
			v = uipanel_0.matrix4x4_0.MultiplyPoint3x4(v);
			v2 = uipanel_0.matrix4x4_0.MultiplyPoint3x4(v2);
			if (Vector3.SqrMagnitude(vector3_2 - v) > 1E-06f || Vector3.SqrMagnitude(vector3_3 - v2) > 1E-06f)
			{
				bool_13 = true;
				vector3_2 = v;
				vector3_3 = v2;
			}
		}
		if (bool_13 && onDimensionsChanged_0 != null)
		{
			onDimensionsChanged_0();
		}
		return bool_13 || bool_0;
	}

	public bool UpdateGeometry(int int_6)
	{
		float num = CalculateFinalAlpha(int_6);
		if (bool_10 && float_1 != num)
		{
			bool_0 = true;
		}
		float_1 = num;
		if (bool_0)
		{
			bool_0 = false;
			if (bool_10 && num > 0.001f && Shader_0 != null)
			{
				bool boolean_ = uigeometry_0.Boolean_0;
				if (bool_8)
				{
					uigeometry_0.Clear();
					OnFill(uigeometry_0.betterList_0, uigeometry_0.betterList_1, uigeometry_0.betterList_2);
				}
				if (uigeometry_0.Boolean_0)
				{
					if (int_5 != int_6)
					{
						matrix4x4_0 = uipanel_0.matrix4x4_0 * base.Transform_0.localToWorldMatrix;
						int_5 = int_6;
					}
					uigeometry_0.ApplyTransform(matrix4x4_0);
					bool_13 = false;
					return true;
				}
				return boolean_;
			}
			if (uigeometry_0.Boolean_0)
			{
				if (bool_8)
				{
					uigeometry_0.Clear();
				}
				bool_13 = false;
				return true;
			}
		}
		else if (bool_13 && uigeometry_0.Boolean_0)
		{
			if (int_5 != int_6)
			{
				matrix4x4_0 = uipanel_0.matrix4x4_0 * base.Transform_0.localToWorldMatrix;
				int_5 = int_6;
			}
			uigeometry_0.ApplyTransform(matrix4x4_0);
			bool_13 = false;
			return true;
		}
		bool_13 = false;
		return false;
	}

	public void WriteToBuffers(BetterList<Vector3> betterList_1, BetterList<Vector2> betterList_2, BetterList<Color32> betterList_3, BetterList<Vector3> betterList_4, BetterList<Vector4> betterList_5)
	{
		uigeometry_0.WriteToBuffers(betterList_1, betterList_2, betterList_3, betterList_4, betterList_5);
	}

	public virtual void MakePixelPerfect()
	{
		Vector3 localPosition = base.Transform_0.localPosition;
		localPosition.z = Mathf.Round(localPosition.z);
		localPosition.x = Mathf.Round(localPosition.x);
		localPosition.y = Mathf.Round(localPosition.y);
		base.Transform_0.localPosition = localPosition;
		Vector3 localScale = base.Transform_0.localScale;
		base.Transform_0.localScale = new Vector3(Mathf.Sign(localScale.x), Mathf.Sign(localScale.y), 1f);
	}

	public virtual void OnFill(BetterList<Vector3> betterList_1, BetterList<Vector2> betterList_2, BetterList<Color32> betterList_3)
	{
	}
}
