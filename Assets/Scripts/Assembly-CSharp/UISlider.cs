using System;
using UnityEngine;

public class UISlider : UIProgressBar
{
	private enum Direction
	{
		Horizontal = 0,
		Vertical = 1,
		Upgraded = 2
	}

	[SerializeField]
	private Transform transform_2;

	[SerializeField]
	private float float_2 = 1f;

	[SerializeField]
	private Direction direction_0 = Direction.Upgraded;

	[SerializeField]
	protected bool bool_1;

	[Obsolete("Use 'value' instead")]
	public float Single_2
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

	[Obsolete("Use 'fillDirection' instead")]
	public bool Boolean_2
	{
		get
		{
			return base.Boolean_1;
		}
		set
		{
		}
	}

	protected override void Upgrade()
	{
		if (direction_0 != Direction.Upgraded)
		{
			float_0 = float_2;
			if (transform_2 != null)
			{
				uiwidget_1 = transform_2.GetComponent<UIWidget>();
			}
			if (direction_0 == Direction.Horizontal)
			{
				fillDirection_0 = (bool_1 ? FillDirection.RightToLeft : FillDirection.LeftToRight);
			}
			else
			{
				fillDirection_0 = ((!bool_1) ? FillDirection.BottomToTop : FillDirection.TopToBottom);
			}
			direction_0 = Direction.Upgraded;
		}
	}

	protected override void OnStart()
	{
		GameObject gameObject_ = ((!(uiwidget_0 != null) || (!(uiwidget_0.GetComponent<Collider>() != null) && !(uiwidget_0.GetComponent<Collider2D>() != null))) ? base.gameObject : uiwidget_0.gameObject);
		UIEventListener uIEventListener = UIEventListener.Get(gameObject_);
		uIEventListener.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uIEventListener.onPress, new UIEventListener.BoolDelegate(OnPressBackground));
		uIEventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uIEventListener.onDrag, new UIEventListener.VectorDelegate(OnDragBackground));
		if (transform_0 != null && (transform_0.GetComponent<Collider>() != null || transform_0.GetComponent<Collider2D>() != null) && (uiwidget_1 == null || transform_0 != uiwidget_1.Transform_0))
		{
			UIEventListener uIEventListener2 = UIEventListener.Get(transform_0.gameObject);
			uIEventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uIEventListener2.onPress, new UIEventListener.BoolDelegate(OnPressForeground));
			uIEventListener2.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uIEventListener2.onDrag, new UIEventListener.VectorDelegate(OnDragForeground));
		}
	}

	protected void OnPressBackground(GameObject gameObject_0, bool bool_2)
	{
		if (UICamera.controlScheme_0 != UICamera.ControlScheme.Controller)
		{
			camera_0 = UICamera.camera_0;
			base.Single_0 = ScreenToValue(UICamera.vector2_0);
			if (!bool_2 && onDragFinished_0 != null)
			{
				onDragFinished_0();
			}
		}
	}

	protected void OnDragBackground(GameObject gameObject_0, Vector2 vector2_0)
	{
		if (UICamera.controlScheme_0 != UICamera.ControlScheme.Controller)
		{
			camera_0 = UICamera.camera_0;
			base.Single_0 = ScreenToValue(UICamera.vector2_0);
		}
	}

	protected void OnPressForeground(GameObject gameObject_0, bool bool_2)
	{
		if (UICamera.controlScheme_0 != UICamera.ControlScheme.Controller)
		{
			if (bool_2)
			{
				float_1 = ((!(uiwidget_1 == null)) ? (base.Single_0 - ScreenToValue(UICamera.vector2_0)) : 0f);
			}
			else if (onDragFinished_0 != null)
			{
				onDragFinished_0();
			}
		}
	}

	protected void OnDragForeground(GameObject gameObject_0, Vector2 vector2_0)
	{
		if (UICamera.controlScheme_0 != UICamera.ControlScheme.Controller)
		{
			camera_0 = UICamera.camera_0;
			base.Single_0 = float_1 + ScreenToValue(UICamera.vector2_0);
		}
	}

	protected void OnKey(KeyCode keyCode_0)
	{
		if (!base.enabled)
		{
			return;
		}
		float num = ((!((float)int_0 > 1f)) ? 0.125f : (1f / (float)(int_0 - 1)));
		if (base.FillDirection_0 != 0 && base.FillDirection_0 != FillDirection.RightToLeft)
		{
			switch (keyCode_0)
			{
			case KeyCode.DownArrow:
				base.Single_0 = float_0 - num;
				break;
			case KeyCode.UpArrow:
				base.Single_0 = float_0 + num;
				break;
			}
		}
		else
		{
			switch (keyCode_0)
			{
			case KeyCode.LeftArrow:
				base.Single_0 = float_0 - num;
				break;
			case KeyCode.RightArrow:
				base.Single_0 = float_0 + num;
				break;
			}
		}
	}
}
