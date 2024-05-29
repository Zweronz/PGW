using System;
using UnityEngine;

public class UIScrollBar : UISlider
{
	private enum Direction
	{
		Horizontal = 0,
		Vertical = 1,
		Upgraded = 2
	}

	[SerializeField]
	protected float float_3 = 1f;

	[SerializeField]
	private float float_4;

	[SerializeField]
	private Direction direction_1 = Direction.Upgraded;

	[Obsolete("Use 'value' instead")]
	public float Single_3
	{
		get
		{
			return base.Single_0;
		}
		set
		{
			base.Single_0 = value;
		}
	}

	public float Single_4
	{
		get
		{
			return float_3;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (float_3 == num)
			{
				return;
			}
			float_3 = num;
			bool_0 = true;
			if (NGUITools.GetActive(this))
			{
				if (UIProgressBar.uiprogressBar_0 == null && list_0 != null)
				{
					UIProgressBar.uiprogressBar_0 = this;
					EventDelegate.Execute(list_0);
					UIProgressBar.uiprogressBar_0 = null;
				}
				ForceUpdate();
			}
		}
	}

	protected override void Upgrade()
	{
		if (direction_1 != Direction.Upgraded)
		{
			float_0 = float_4;
			if (direction_1 == Direction.Horizontal)
			{
				fillDirection_0 = (bool_1 ? FillDirection.RightToLeft : FillDirection.LeftToRight);
			}
			else
			{
				fillDirection_0 = ((!bool_1) ? FillDirection.TopToBottom : FillDirection.BottomToTop);
			}
			direction_1 = Direction.Upgraded;
		}
	}

	protected override void OnStart()
	{
		base.OnStart();
		if (uiwidget_1 != null && uiwidget_1.gameObject != base.gameObject && (uiwidget_1.GetComponent<Collider>() != null || uiwidget_1.GetComponent<Collider2D>() != null))
		{
			UIEventListener uIEventListener = UIEventListener.Get(uiwidget_1.gameObject);
			uIEventListener.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uIEventListener.onPress, new UIEventListener.BoolDelegate(base.OnPressForeground));
			uIEventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uIEventListener.onDrag, new UIEventListener.VectorDelegate(base.OnDragForeground));
			uiwidget_1.bool_6 = true;
		}
	}

	protected override float LocalToValue(Vector2 vector2_0)
	{
		if (uiwidget_1 != null)
		{
			float num = Mathf.Clamp01(float_3) * 0.5f;
			float t = num;
			float t2 = 1f - num;
			Vector3[] vector3_ = uiwidget_1.Vector3_2;
			if (base.Boolean_0)
			{
				t = Mathf.Lerp(vector3_[0].x, vector3_[2].x, t);
				t2 = Mathf.Lerp(vector3_[0].x, vector3_[2].x, t2);
				float num2 = t2 - t;
				if (num2 == 0f)
				{
					return base.Single_0;
				}
				return (!base.Boolean_1) ? ((vector2_0.x - t) / num2) : ((t2 - vector2_0.x) / num2);
			}
			t = Mathf.Lerp(vector3_[0].y, vector3_[1].y, t);
			t2 = Mathf.Lerp(vector3_[3].y, vector3_[2].y, t2);
			float num3 = t2 - t;
			if (num3 == 0f)
			{
				return base.Single_0;
			}
			return (!base.Boolean_1) ? ((vector2_0.y - t) / num3) : ((t2 - vector2_0.y) / num3);
		}
		return base.LocalToValue(vector2_0);
	}

	public override void ForceUpdate()
	{
		if (uiwidget_1 != null)
		{
			bool_0 = false;
			float num = Mathf.Clamp01(float_3) * 0.5f;
			float num2 = Mathf.Lerp(num, 1f - num, base.Single_0);
			float num3 = num2 - num;
			float num4 = num2 + num;
			if (base.Boolean_0)
			{
				uiwidget_1.Vector4_0 = ((!base.Boolean_1) ? new Vector4(num3, 0f, num4, 1f) : new Vector4(1f - num4, 0f, 1f - num3, 1f));
			}
			else
			{
				uiwidget_1.Vector4_0 = ((!base.Boolean_1) ? new Vector4(0f, num3, 1f, num4) : new Vector4(0f, 1f - num4, 1f, 1f - num3));
			}
			if (transform_0 != null)
			{
				Vector4 vector4_ = uiwidget_1.Vector4_2;
				Vector3 position = new Vector3(Mathf.Lerp(vector4_.x, vector4_.z, 0.5f), Mathf.Lerp(vector4_.y, vector4_.w, 0.5f));
				SetThumbPosition(uiwidget_1.Transform_0.TransformPoint(position));
			}
		}
		else
		{
			base.ForceUpdate();
		}
	}
}
