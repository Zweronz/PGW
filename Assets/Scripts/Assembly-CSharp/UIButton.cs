using System;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : UIButtonColor
{
	public static UIButton uibutton_0;

	public bool bool_1;

	public string string_0;

	public string string_1;

	public string string_2;

	public Sprite sprite_0;

	public Sprite sprite_1;

	public Sprite sprite_2;

	public bool bool_2;

	public List<EventDelegate> list_0 = new List<EventDelegate>();

	[NonSerialized]
	private UISprite uisprite_0;

	[NonSerialized]
	private UI2DSprite ui2DSprite_0;

	[NonSerialized]
	private string string_3;

	[NonSerialized]
	private Sprite sprite_3;

	public override bool Boolean_0
	{
		get
		{
			if (!base.enabled)
			{
				return false;
			}
			Collider collider = base.GetComponent<Collider>();
			if ((bool)collider && collider.enabled)
			{
				return true;
			}
			Collider2D component = GetComponent<Collider2D>();
			return (bool)component && component.enabled;
		}
		set
		{
			if (Boolean_0 == value)
			{
				return;
			}
			Collider collider = base.GetComponent<Collider>();
			if (collider != null)
			{
				collider.enabled = value;
				SetState((!value) ? State.Disabled : State.Normal, false);
				return;
			}
			Collider2D component = GetComponent<Collider2D>();
			if (component != null)
			{
				component.enabled = value;
				SetState((!value) ? State.Disabled : State.Normal, false);
			}
			else
			{
				base.enabled = value;
			}
		}
	}

	public string String_0
	{
		get
		{
			if (!bool_0)
			{
				OnInit();
			}
			return string_3;
		}
		set
		{
			if (uisprite_0 != null && !string.IsNullOrEmpty(string_3) && string_3 == uisprite_0.String_0)
			{
				string_3 = value;
				SetSprite(value);
				NGUITools.SetDirty(uisprite_0);
				return;
			}
			string_3 = value;
			if (state_0 == State.Normal)
			{
				SetSprite(value);
			}
		}
	}

	public Sprite Sprite_0
	{
		get
		{
			if (!bool_0)
			{
				OnInit();
			}
			return sprite_3;
		}
		set
		{
			if (ui2DSprite_0 != null && sprite_3 == ui2DSprite_0.Sprite_0)
			{
				sprite_3 = value;
				SetSprite(value);
				NGUITools.SetDirty(uisprite_0);
				return;
			}
			sprite_3 = value;
			if (state_0 == State.Normal)
			{
				SetSprite(value);
			}
		}
	}

	protected override void OnInit()
	{
		base.OnInit();
		uisprite_0 = uiwidget_0 as UISprite;
		ui2DSprite_0 = uiwidget_0 as UI2DSprite;
		if (uisprite_0 != null)
		{
			string_3 = uisprite_0.String_0;
		}
		if (ui2DSprite_0 != null)
		{
			sprite_3 = ui2DSprite_0.Sprite_0;
		}
	}

	protected override void OnEnable()
	{
		if (Boolean_0)
		{
			if (bool_0)
			{
				if (UICamera.controlScheme_0 == UICamera.ControlScheme.Controller)
				{
					OnHover(UICamera.GameObject_0 == base.gameObject);
				}
				else if (UICamera.controlScheme_0 == UICamera.ControlScheme.Mouse)
				{
					OnHover(UICamera.gameObject_6 == base.gameObject);
				}
				else
				{
					SetState(State.Normal, false);
				}
			}
		}
		else
		{
			SetState(State.Disabled, true);
		}
	}

	protected override void OnDragOver()
	{
		if (Boolean_0 && (bool_1 || UICamera.mouseOrTouch_0.gameObject_2 == base.gameObject))
		{
			base.OnDragOver();
		}
	}

	protected override void OnDragOut()
	{
		if (Boolean_0 && (bool_1 || UICamera.mouseOrTouch_0.gameObject_2 == base.gameObject))
		{
			base.OnDragOut();
		}
	}

	protected virtual void OnClick()
	{
		if (uibutton_0 == null && Boolean_0)
		{
			uibutton_0 = this;
			EventDelegate.Execute(list_0);
			uibutton_0 = null;
		}
	}

	public override void SetState(State state_1, bool bool_3)
	{
		base.SetState(state_1, bool_3);
		if (uisprite_0 != null)
		{
			switch (state_1)
			{
			case State.Normal:
				SetSprite(string_3);
				break;
			case State.Hover:
				SetSprite(string_0);
				break;
			case State.Pressed:
				SetSprite(string_1);
				break;
			case State.Disabled:
				SetSprite(string_2);
				break;
			}
		}
		else if (ui2DSprite_0 != null)
		{
			switch (state_1)
			{
			case State.Normal:
				SetSprite(sprite_3);
				break;
			case State.Hover:
				SetSprite(sprite_0);
				break;
			case State.Pressed:
				SetSprite(sprite_1);
				break;
			case State.Disabled:
				SetSprite(sprite_2);
				break;
			}
		}
	}

	protected void SetSprite(string string_4)
	{
		if (uisprite_0 != null && !string.IsNullOrEmpty(string_4) && uisprite_0.String_0 != string_4)
		{
			uisprite_0.String_0 = string_4;
			if (bool_2)
			{
				uisprite_0.MakePixelPerfect();
			}
		}
	}

	protected void SetSprite(Sprite sprite_4)
	{
		if (sprite_4 != null && ui2DSprite_0 != null && ui2DSprite_0.Sprite_0 != sprite_4)
		{
			ui2DSprite_0.Sprite_0 = sprite_4;
			if (bool_2)
			{
				ui2DSprite_0.MakePixelPerfect();
			}
		}
	}
}
