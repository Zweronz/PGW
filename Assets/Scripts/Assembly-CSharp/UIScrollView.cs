using UnityEngine;

[RequireComponent(typeof(UIPanel))]
public class UIScrollView : MonoBehaviour
{
	public enum Movement
	{
		Horizontal = 0,
		Vertical = 1,
		Unrestricted = 2,
		Custom = 3
	}

	public enum DragEffect
	{
		None = 0,
		Momentum = 1,
		MomentumAndSpring = 2
	}

	public enum ShowCondition
	{
		Always = 0,
		OnlyIfNeeded = 1,
		WhenDragging = 2
	}

	public delegate void OnDragNotification();

	public static BetterList<UIScrollView> betterList_0 = new BetterList<UIScrollView>();

	public Movement movement;

	public DragEffect dragEffect = DragEffect.MomentumAndSpring;

	public bool restrictWithinPanel = true;

	public bool disableDragIfFits;

	public bool smoothDragStart = true;

	public bool iOSDragEmulation = true;

	public float scrollWheelFactor = 0.25f;

	public float momentumAmount = 35f;

	public UIProgressBar horizontalScrollBar;

	public UIProgressBar verticalScrollBar;

	public ShowCondition showScrollBars = ShowCondition.OnlyIfNeeded;

	public Vector2 customMovement = new Vector2(1f, 0f);

	public UIWidget.Pivot contentPivot;

	public OnDragNotification onDragStarted;

	public OnDragNotification onDragFinished;

	[SerializeField]
	private Vector3 vector3_0 = new Vector3(1f, 0f, 0f);

	[SerializeField]
	private Vector2 vector2_0 = Vector2.zero;

	protected Transform transform_0;

	protected UIPanel uipanel_0;

	protected Plane plane_0;

	protected Vector3 vector3_1;

	protected bool bool_0;

	protected Vector3 vector3_2 = Vector3.zero;

	protected float float_0;

	protected Bounds bounds_0;

	protected bool bool_1;

	protected bool bool_2;

	protected bool bool_3;

	protected int int_0 = -10;

	protected Vector2 vector2_1 = Vector2.zero;

	protected bool bool_4;

	public UIPanel UIPanel_0
	{
		get
		{
			return uipanel_0;
		}
	}

	public bool Boolean_0
	{
		get
		{
			return bool_0 && bool_4;
		}
	}

	public virtual Bounds Bounds_0
	{
		get
		{
			if (!bool_1)
			{
				bool_1 = true;
				transform_0 = base.transform;
				bounds_0 = NGUIMath.CalculateRelativeWidgetBounds(transform_0, transform_0);
			}
			return bounds_0;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return movement == Movement.Horizontal || movement == Movement.Unrestricted || (movement == Movement.Custom && customMovement.x != 0f);
		}
	}

	public bool Boolean_2
	{
		get
		{
			return movement == Movement.Vertical || movement == Movement.Unrestricted || (movement == Movement.Custom && customMovement.y != 0f);
		}
	}

	public virtual bool Boolean_3
	{
		get
		{
			float num = Bounds_0.size.x;
			if (uipanel_0.Clipping_0 == UIDrawCall.Clipping.SoftClip)
			{
				num += uipanel_0.Vector2_1.x * 2f;
			}
			return Mathf.RoundToInt(num - uipanel_0.Single_0) > 0;
		}
	}

	public virtual bool Boolean_4
	{
		get
		{
			float num = Bounds_0.size.y;
			if (uipanel_0.Clipping_0 == UIDrawCall.Clipping.SoftClip)
			{
				num += uipanel_0.Vector2_1.y * 2f;
			}
			return Mathf.RoundToInt(num - uipanel_0.Single_1) > 0;
		}
	}

	protected virtual bool Boolean_5
	{
		get
		{
			if (!disableDragIfFits)
			{
				return true;
			}
			if (uipanel_0 == null)
			{
				uipanel_0 = GetComponent<UIPanel>();
			}
			Vector4 vector4_ = uipanel_0.Vector4_2;
			Bounds bounds = Bounds_0;
			float num = ((vector4_.z != 0f) ? (vector4_.z * 0.5f) : ((float)Screen.width));
			float num2 = ((vector4_.w != 0f) ? (vector4_.w * 0.5f) : ((float)Screen.height));
			if (Boolean_1)
			{
				if (bounds.min.x < vector4_.x - num)
				{
					return true;
				}
				if (bounds.max.x > vector4_.x + num)
				{
					return true;
				}
			}
			if (Boolean_2)
			{
				if (bounds.min.y < vector4_.y - num2)
				{
					return true;
				}
				if (bounds.max.y > vector4_.y + num2)
				{
					return true;
				}
			}
			return false;
		}
	}

	public Vector3 Vector3_0
	{
		get
		{
			return vector3_2;
		}
		set
		{
			vector3_2 = value;
			bool_2 = true;
		}
	}

	private void Awake()
	{
		transform_0 = base.transform;
		uipanel_0 = GetComponent<UIPanel>();
		if (uipanel_0.Clipping_0 == UIDrawCall.Clipping.None)
		{
			uipanel_0.Clipping_0 = UIDrawCall.Clipping.ConstrainButDontClip;
		}
		if (movement != Movement.Custom && vector3_0.sqrMagnitude > 0.001f)
		{
			if (vector3_0.x == 1f && vector3_0.y == 0f)
			{
				movement = Movement.Horizontal;
			}
			else if (vector3_0.x == 0f && vector3_0.y == 1f)
			{
				movement = Movement.Vertical;
			}
			else if (vector3_0.x == 1f && vector3_0.y == 1f)
			{
				movement = Movement.Unrestricted;
			}
			else
			{
				movement = Movement.Custom;
				customMovement.x = vector3_0.x;
				customMovement.y = vector3_0.y;
			}
			vector3_0 = Vector3.zero;
		}
		if (contentPivot == UIWidget.Pivot.TopLeft && vector2_0 != Vector2.zero)
		{
			contentPivot = NGUIMath.GetPivot(new Vector2(vector2_0.x, 1f - vector2_0.y));
			vector2_0 = Vector2.zero;
		}
	}

	private void OnEnable()
	{
		betterList_0.Add(this);
	}

	private void OnDisable()
	{
		betterList_0.Remove(this);
	}

	protected virtual void Start()
	{
		if (Application.isPlaying)
		{
			if (horizontalScrollBar != null)
			{
				EventDelegate.Add(horizontalScrollBar.list_0, OnScrollBar);
				horizontalScrollBar.Single_1 = ((showScrollBars == ShowCondition.Always || Boolean_3) ? 1f : 0f);
			}
			if (verticalScrollBar != null)
			{
				EventDelegate.Add(verticalScrollBar.list_0, OnScrollBar);
				verticalScrollBar.Single_1 = ((showScrollBars == ShowCondition.Always || Boolean_4) ? 1f : 0f);
			}
		}
	}

	public bool RestrictWithinBounds(bool bool_5)
	{
		return RestrictWithinBounds(bool_5, true, true);
	}

	public bool RestrictWithinBounds(bool bool_5, bool bool_6, bool bool_7)
	{
		Bounds bounds = Bounds_0;
		Vector3 vector = uipanel_0.CalculateConstrainOffset(bounds.min, bounds.max);
		if (!bool_6)
		{
			vector.x = 0f;
		}
		if (!bool_7)
		{
			vector.y = 0f;
		}
		if (vector.sqrMagnitude > 0.1f)
		{
			if (!bool_5 && dragEffect == DragEffect.MomentumAndSpring)
			{
				Vector3 vector2 = transform_0.localPosition + vector;
				vector2.x = Mathf.Round(vector2.x);
				vector2.y = Mathf.Round(vector2.y);
				SpringPanel.Begin(uipanel_0.gameObject, vector2, 13f);
			}
			else
			{
				MoveRelative(vector);
				if (Mathf.Abs(vector.x) > 0.01f)
				{
					vector3_2.x = 0f;
				}
				if (Mathf.Abs(vector.y) > 0.01f)
				{
					vector3_2.y = 0f;
				}
				if (Mathf.Abs(vector.z) > 0.01f)
				{
					vector3_2.z = 0f;
				}
				float_0 = 0f;
			}
			return true;
		}
		return false;
	}

	public void DisableSpring()
	{
		SpringPanel component = GetComponent<SpringPanel>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	public void UpdateScrollbars()
	{
		UpdateScrollbars(true);
	}

	public virtual void UpdateScrollbars(bool bool_5)
	{
		if (uipanel_0 == null)
		{
			return;
		}
		if (!(horizontalScrollBar != null) && !(verticalScrollBar != null))
		{
			if (bool_5)
			{
				bool_1 = false;
			}
			return;
		}
		if (bool_5)
		{
			bool_1 = false;
			bool_2 = Boolean_5;
		}
		Bounds bounds = Bounds_0;
		Vector2 vector = bounds.min;
		Vector2 vector2 = bounds.max;
		if (horizontalScrollBar != null && vector2.x > vector.x)
		{
			Vector4 vector4_ = uipanel_0.Vector4_2;
			int num = Mathf.RoundToInt(vector4_.z);
			if (((uint)num & (true ? 1u : 0u)) != 0)
			{
				num--;
			}
			float f = (float)num * 0.5f;
			f = Mathf.Round(f);
			if (uipanel_0.Clipping_0 == UIDrawCall.Clipping.SoftClip)
			{
				f -= uipanel_0.Vector2_1.x;
			}
			float float_ = vector2.x - vector.x;
			float float_2 = f * 2f;
			float x = vector.x;
			float x2 = vector2.x;
			float num2 = vector4_.x - f;
			float num3 = vector4_.x + f;
			x = num2 - x;
			x2 -= num3;
			UpdateScrollbars(horizontalScrollBar, x, x2, float_, float_2, false);
		}
		if (verticalScrollBar != null && vector2.y > vector.y)
		{
			Vector4 vector4_2 = uipanel_0.Vector4_2;
			int num4 = Mathf.RoundToInt(vector4_2.w);
			if (((uint)num4 & (true ? 1u : 0u)) != 0)
			{
				num4--;
			}
			float f2 = (float)num4 * 0.5f;
			f2 = Mathf.Round(f2);
			if (uipanel_0.Clipping_0 == UIDrawCall.Clipping.SoftClip)
			{
				f2 -= uipanel_0.Vector2_1.y;
			}
			float float_3 = vector2.y - vector.y;
			float float_4 = f2 * 2f;
			float y = vector.y;
			float y2 = vector2.y;
			float num5 = vector4_2.y - f2;
			float num6 = vector4_2.y + f2;
			y = num5 - y;
			y2 -= num6;
			UpdateScrollbars(verticalScrollBar, y, y2, float_3, float_4, true);
		}
	}

	protected void UpdateScrollbars(UIProgressBar uiprogressBar_0, float float_1, float float_2, float float_3, float float_4, bool bool_5)
	{
		if (uiprogressBar_0 == null)
		{
			return;
		}
		bool_3 = true;
		float num;
		if (float_4 < float_3)
		{
			float_1 = Mathf.Clamp01(float_1 / float_3);
			float_2 = Mathf.Clamp01(float_2 / float_3);
			num = float_1 + float_2;
			uiprogressBar_0.Single_0 = (bool_5 ? ((!(num > 0.001f)) ? 0f : (1f - float_1 / num)) : ((!(num > 0.001f)) ? 1f : (float_1 / num)));
		}
		else
		{
			float_1 = Mathf.Clamp01((0f - float_1) / float_3);
			float_2 = Mathf.Clamp01((0f - float_2) / float_3);
			num = float_1 + float_2;
			uiprogressBar_0.Single_0 = (bool_5 ? ((!(num > 0.001f)) ? 0f : (1f - float_1 / num)) : ((!(num > 0.001f)) ? 1f : (float_1 / num)));
			if (float_3 > 0f)
			{
				float_1 = Mathf.Clamp01(float_1 / float_3);
				float_2 = Mathf.Clamp01(float_2 / float_3);
				num = float_1 + float_2;
			}
		}
		UIScrollBar uIScrollBar = uiprogressBar_0 as UIScrollBar;
		if (uIScrollBar != null)
		{
			uIScrollBar.Single_4 = 1f - num;
		}
		bool_3 = false;
	}

	public virtual void SetDragAmount(float float_1, float float_2, bool bool_5)
	{
		if (uipanel_0 == null)
		{
			uipanel_0 = GetComponent<UIPanel>();
		}
		DisableSpring();
		Bounds bounds = Bounds_0;
		if (bounds.min.x == bounds.max.x || bounds.min.y == bounds.max.y)
		{
			return;
		}
		Vector4 vector4_ = uipanel_0.Vector4_2;
		float num = vector4_.z * 0.5f;
		float num2 = vector4_.w * 0.5f;
		float num3 = bounds.min.x + num;
		float num4 = bounds.max.x - num;
		float num5 = bounds.min.y + num2;
		float num6 = bounds.max.y - num2;
		if (uipanel_0.Clipping_0 == UIDrawCall.Clipping.SoftClip)
		{
			num3 -= uipanel_0.Vector2_1.x;
			num4 += uipanel_0.Vector2_1.x;
			num5 -= uipanel_0.Vector2_1.y;
			num6 += uipanel_0.Vector2_1.y;
		}
		float num7 = Mathf.Lerp(num3, num4, float_1);
		float num8 = Mathf.Lerp(num6, num5, float_2);
		if (!bool_5)
		{
			Vector3 localPosition = transform_0.localPosition;
			if (Boolean_1)
			{
				localPosition.x += vector4_.x - num7;
			}
			if (Boolean_2)
			{
				localPosition.y += vector4_.y - num8;
			}
			transform_0.localPosition = localPosition;
		}
		if (Boolean_1)
		{
			vector4_.x = num7;
		}
		if (Boolean_2)
		{
			vector4_.y = num8;
		}
		Vector4 vector4_2 = uipanel_0.Vector4_1;
		uipanel_0.Vector2_0 = new Vector2(vector4_.x - vector4_2.x, vector4_.y - vector4_2.y);
		if (bool_5)
		{
			UpdateScrollbars(int_0 == -10);
		}
	}

	public void InvalidateBounds()
	{
		bool_1 = false;
	}

	[ContextMenu("Reset Clipping Position")]
	public void ResetPosition()
	{
		if (NGUITools.GetActive(this))
		{
			bool_1 = false;
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(contentPivot);
			SetDragAmount(pivotOffset.x, 1f - pivotOffset.y, false);
			SetDragAmount(pivotOffset.x, 1f - pivotOffset.y, true);
		}
	}

	public void UpdatePosition()
	{
		if (!bool_3 && (horizontalScrollBar != null || verticalScrollBar != null))
		{
			bool_3 = true;
			bool_1 = false;
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(contentPivot);
			float float_ = ((!(horizontalScrollBar != null)) ? pivotOffset.x : horizontalScrollBar.Single_0);
			float float_2 = ((!(verticalScrollBar != null)) ? (1f - pivotOffset.y) : verticalScrollBar.Single_0);
			SetDragAmount(float_, float_2, false);
			UpdateScrollbars(true);
			bool_3 = false;
		}
	}

	public void OnScrollBar()
	{
		if (!bool_3)
		{
			bool_3 = true;
			float float_ = ((!(horizontalScrollBar != null)) ? 0f : horizontalScrollBar.Single_0);
			float float_2 = ((!(verticalScrollBar != null)) ? 0f : verticalScrollBar.Single_0);
			SetDragAmount(float_, float_2, false);
			bool_3 = false;
		}
	}

	public virtual void MoveRelative(Vector3 vector3_3)
	{
		transform_0.localPosition += vector3_3;
		Vector2 vector = uipanel_0.Vector2_0;
		vector.x -= vector3_3.x;
		vector.y -= vector3_3.y;
		uipanel_0.Vector2_0 = vector;
		UpdateScrollbars(false);
	}

	public void MoveAbsolute(Vector3 vector3_3)
	{
		Vector3 vector = transform_0.InverseTransformPoint(vector3_3);
		Vector3 vector2 = transform_0.InverseTransformPoint(Vector3.zero);
		MoveRelative(vector - vector2);
	}

	public void Press(bool bool_5)
	{
		if (smoothDragStart && bool_5)
		{
			bool_4 = false;
			vector2_1 = Vector2.zero;
		}
		if (!base.enabled || !NGUITools.GetActive(base.gameObject))
		{
			return;
		}
		if (!bool_5 && int_0 == UICamera.int_0)
		{
			int_0 = -10;
		}
		bool_1 = false;
		bool_2 = Boolean_5;
		if (!bool_2)
		{
			return;
		}
		bool_0 = bool_5;
		if (bool_5)
		{
			vector3_2 = Vector3.zero;
			float_0 = 0f;
			DisableSpring();
			vector3_1 = UICamera.vector3_0;
			plane_0 = new Plane(transform_0.rotation * Vector3.back, vector3_1);
			Vector2 vector = uipanel_0.Vector2_0;
			vector.x = Mathf.Round(vector.x);
			vector.y = Mathf.Round(vector.y);
			uipanel_0.Vector2_0 = vector;
			Vector3 localPosition = transform_0.localPosition;
			localPosition.x = Mathf.Round(localPosition.x);
			localPosition.y = Mathf.Round(localPosition.y);
			transform_0.localPosition = localPosition;
		}
		else
		{
			if (restrictWithinPanel && uipanel_0.Clipping_0 != 0 && dragEffect == DragEffect.MomentumAndSpring)
			{
				RestrictWithinBounds(false, Boolean_1, Boolean_2);
			}
			if (onDragFinished != null)
			{
				onDragFinished();
			}
		}
	}

	public void Drag()
	{
		if (!base.enabled || !NGUITools.GetActive(base.gameObject) || !bool_2)
		{
			return;
		}
		if (int_0 == -10)
		{
			int_0 = UICamera.int_0;
		}
		UICamera.mouseOrTouch_0.clickNotification_0 = UICamera.ClickNotification.BasedOnDelta;
		if (smoothDragStart && !bool_4)
		{
			bool_4 = true;
			vector2_1 = UICamera.mouseOrTouch_0.vector2_3;
			if (onDragStarted != null)
			{
				onDragStarted();
			}
		}
		Ray ray = ((!smoothDragStart) ? UICamera.camera_0.ScreenPointToRay(UICamera.mouseOrTouch_0.vector2_0) : UICamera.camera_0.ScreenPointToRay(UICamera.mouseOrTouch_0.vector2_0 - vector2_1));
		float enter = 0f;
		if (!plane_0.Raycast(ray, out enter))
		{
			return;
		}
		Vector3 point = ray.GetPoint(enter);
		Vector3 vector = point - vector3_1;
		vector3_1 = point;
		if (vector.x != 0f || vector.y != 0f || vector.z != 0f)
		{
			vector = transform_0.InverseTransformDirection(vector);
			if (movement == Movement.Horizontal)
			{
				vector.y = 0f;
				vector.z = 0f;
			}
			else if (movement == Movement.Vertical)
			{
				vector.x = 0f;
				vector.z = 0f;
			}
			else if (movement == Movement.Unrestricted)
			{
				vector.z = 0f;
			}
			else
			{
				vector.Scale(customMovement);
			}
			vector = transform_0.TransformDirection(vector);
		}
		vector3_2 = Vector3.Lerp(vector3_2, vector3_2 + vector * (0.01f * momentumAmount), 0.67f);
		if (iOSDragEmulation && dragEffect == DragEffect.MomentumAndSpring)
		{
			if (uipanel_0.CalculateConstrainOffset(Bounds_0.min, Bounds_0.max).magnitude > 1f)
			{
				MoveAbsolute(vector * 0.5f);
				vector3_2 *= 0.5f;
			}
			else
			{
				MoveAbsolute(vector);
			}
		}
		else
		{
			MoveAbsolute(vector);
		}
		if (restrictWithinPanel && uipanel_0.Clipping_0 != 0 && dragEffect != DragEffect.MomentumAndSpring)
		{
			RestrictWithinBounds(true, Boolean_1, Boolean_2);
		}
	}

	public void Scroll(float float_1)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && scrollWheelFactor != 0f)
		{
			DisableSpring();
			bool_2 = Boolean_5;
			if (Mathf.Sign(float_0) != Mathf.Sign(float_1))
			{
				float_0 = 0f;
			}
			float_0 += float_1 * scrollWheelFactor;
		}
	}

	private void LateUpdate()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		float single_ = RealTime.Single_1;
		if (showScrollBars != 0 && ((bool)verticalScrollBar || (bool)horizontalScrollBar))
		{
			bool flag = false;
			bool flag2 = false;
			if (showScrollBars != ShowCondition.WhenDragging || int_0 != -10 || vector3_2.magnitude > 0.01f)
			{
				flag = Boolean_4;
				flag2 = Boolean_3;
			}
			if ((bool)verticalScrollBar)
			{
				float single_2 = verticalScrollBar.Single_1;
				single_2 += ((!flag) ? ((0f - single_) * 3f) : (single_ * 6f));
				single_2 = Mathf.Clamp01(single_2);
				if (verticalScrollBar.Single_1 != single_2)
				{
					verticalScrollBar.Single_1 = single_2;
				}
			}
			if ((bool)horizontalScrollBar)
			{
				float single_3 = horizontalScrollBar.Single_1;
				single_3 += ((!flag2) ? ((0f - single_) * 3f) : (single_ * 6f));
				single_3 = Mathf.Clamp01(single_3);
				if (horizontalScrollBar.Single_1 != single_3)
				{
					horizontalScrollBar.Single_1 = single_3;
				}
			}
		}
		if (bool_2 && !bool_0)
		{
			if (movement == Movement.Horizontal)
			{
				vector3_2 -= transform_0.TransformDirection(new Vector3(float_0 * 0.05f, 0f, 0f));
			}
			else if (movement == Movement.Vertical)
			{
				vector3_2 -= transform_0.TransformDirection(new Vector3(0f, float_0 * 0.05f, 0f));
			}
			else if (movement == Movement.Unrestricted)
			{
				vector3_2 -= transform_0.TransformDirection(new Vector3(float_0 * 0.05f, float_0 * 0.05f, 0f));
			}
			else
			{
				vector3_2 -= transform_0.TransformDirection(new Vector3(float_0 * customMovement.x * 0.05f, float_0 * customMovement.y * 0.05f, 0f));
			}
			if (vector3_2.magnitude > 0.0001f)
			{
				float_0 = NGUIMath.SpringLerp(float_0, 0f, 20f, single_);
				Vector3 vector3_ = NGUIMath.SpringDampen(ref vector3_2, 9f, single_);
				MoveAbsolute(vector3_);
				if (restrictWithinPanel && uipanel_0.Clipping_0 != 0)
				{
					RestrictWithinBounds(false, Boolean_1, Boolean_2);
				}
				if (vector3_2.magnitude < 0.0001f && onDragFinished != null)
				{
					onDragFinished();
				}
				return;
			}
			float_0 = 0f;
			vector3_2 = Vector3.zero;
		}
		else
		{
			float_0 = 0f;
		}
		NGUIMath.SpringDampen(ref vector3_2, 9f, single_);
	}
}
