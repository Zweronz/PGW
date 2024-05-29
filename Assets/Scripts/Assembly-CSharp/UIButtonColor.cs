using System;
using UnityEngine;

public class UIButtonColor : UIWidgetContainer
{
	public enum State
	{
		Normal = 0,
		Hover = 1,
		Pressed = 2,
		Disabled = 3
	}

	public GameObject gameObject_0;

	public Color color_0 = new Color(0.88235295f, 40f / 51f, 0.5882353f, 1f);

	public Color color_1 = new Color(61f / 85f, 0.6392157f, 41f / 85f, 1f);

	public Color color_2 = Color.grey;

	public float float_0 = 0.2f;

	[NonSerialized]
	protected Color color_3;

	[NonSerialized]
	protected Color color_4;

	[NonSerialized]
	protected bool bool_0;

	[NonSerialized]
	protected UIWidget uiwidget_0;

	[NonSerialized]
	protected State state_0;

	public State State_0
	{
		get
		{
			return state_0;
		}
		set
		{
			SetState(value, false);
		}
	}

	public Color Color_0
	{
		get
		{
			if (!bool_0)
			{
				OnInit();
			}
			return color_4;
		}
		set
		{
			if (!bool_0)
			{
				OnInit();
			}
			color_4 = value;
			State state_ = state_0;
			state_0 = State.Disabled;
			SetState(state_, false);
		}
	}

	public virtual bool Boolean_0
	{
		get
		{
			return base.enabled;
		}
		set
		{
			base.enabled = value;
		}
	}

	public void ResetDefaultColor()
	{
		Color_0 = color_3;
	}

	private void Awake()
	{
		if (!bool_0)
		{
			OnInit();
		}
	}

	private void Start()
	{
		if (!Boolean_0)
		{
			SetState(State.Disabled, true);
		}
	}

	protected virtual void OnInit()
	{
		bool_0 = true;
		if (gameObject_0 == null)
		{
			gameObject_0 = base.gameObject;
		}
		uiwidget_0 = gameObject_0.GetComponent<UIWidget>();
		if (uiwidget_0 != null)
		{
			color_4 = uiwidget_0.Color_0;
			color_3 = color_4;
			return;
		}
		Renderer renderer = gameObject_0.GetComponent<Renderer>();
		if (renderer != null)
		{
			color_4 = ((!Application.isPlaying) ? renderer.sharedMaterial.color : renderer.material.color);
			color_3 = color_4;
			return;
		}
		Light light = gameObject_0.GetComponent<Light>();
		if (light != null)
		{
			color_4 = light.color;
			color_3 = color_4;
		}
		else
		{
			gameObject_0 = null;
			bool_0 = false;
		}
	}

	protected virtual void OnEnable()
	{
		if (bool_0)
		{
			OnHover(UICamera.IsHighlighted(base.gameObject));
		}
		if (UICamera.mouseOrTouch_0 != null)
		{
			if (UICamera.mouseOrTouch_0.gameObject_2 == base.gameObject)
			{
				OnPress(true);
			}
			else if (UICamera.mouseOrTouch_0.gameObject_1 == base.gameObject)
			{
				OnHover(true);
			}
		}
	}

	protected virtual void OnDisable()
	{
		if (bool_0 && gameObject_0 != null)
		{
			SetState(State.Normal, true);
			TweenColor component = gameObject_0.GetComponent<TweenColor>();
			if (component != null)
			{
				component.Color_1 = color_4;
				component.enabled = false;
			}
		}
	}

	protected virtual void OnHover(bool bool_1)
	{
		if (Boolean_0)
		{
			if (!bool_0)
			{
				OnInit();
			}
			if (gameObject_0 != null)
			{
				SetState(bool_1 ? State.Hover : State.Normal, false);
			}
		}
	}

	protected virtual void OnPress(bool bool_1)
	{
		if (!Boolean_0 || UICamera.mouseOrTouch_0 == null)
		{
			return;
		}
		if (!bool_0)
		{
			OnInit();
		}
		if (!(gameObject_0 != null))
		{
			return;
		}
		if (bool_1)
		{
			SetState(State.Pressed, false);
		}
		else if (UICamera.mouseOrTouch_0.gameObject_1 == base.gameObject)
		{
			if (UICamera.controlScheme_0 == UICamera.ControlScheme.Controller)
			{
				SetState(State.Hover, false);
			}
			else if (UICamera.controlScheme_0 == UICamera.ControlScheme.Mouse && UICamera.gameObject_6 == base.gameObject)
			{
				SetState(State.Hover, false);
			}
			else
			{
				SetState(State.Normal, false);
			}
		}
		else
		{
			SetState(State.Normal, false);
		}
	}

	protected virtual void OnDragOver()
	{
		if (Boolean_0)
		{
			if (!bool_0)
			{
				OnInit();
			}
			if (gameObject_0 != null)
			{
				SetState(State.Pressed, false);
			}
		}
	}

	protected virtual void OnDragOut()
	{
		if (Boolean_0)
		{
			if (!bool_0)
			{
				OnInit();
			}
			if (gameObject_0 != null)
			{
				SetState(State.Normal, false);
			}
		}
	}

	protected virtual void OnSelect(bool bool_1)
	{
		if (Boolean_0 && (!bool_1 || UICamera.controlScheme_0 == UICamera.ControlScheme.Controller) && gameObject_0 != null)
		{
			OnHover(bool_1);
		}
	}

	public virtual void SetState(State state_1, bool bool_1)
	{
		if (!bool_0)
		{
			bool_0 = true;
			OnInit();
		}
		if (state_0 != state_1)
		{
			state_0 = state_1;
			UpdateColor(bool_1);
		}
	}

	public void UpdateColor(bool bool_1)
	{
		TweenColor tweenColor;
		switch (state_0)
		{
		default:
			tweenColor = TweenColor.Begin(gameObject_0, float_0, color_4);
			break;
		case State.Hover:
			tweenColor = TweenColor.Begin(gameObject_0, float_0, color_0);
			break;
		case State.Pressed:
			tweenColor = TweenColor.Begin(gameObject_0, float_0, color_1);
			break;
		case State.Disabled:
			tweenColor = TweenColor.Begin(gameObject_0, float_0, color_2);
			break;
		}
		if (bool_1 && tweenColor != null)
		{
			tweenColor.Color_1 = tweenColor.color_1;
			tweenColor.enabled = false;
		}
	}
}
