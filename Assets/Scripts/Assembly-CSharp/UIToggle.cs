using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

public class UIToggle : UIWidgetContainer
{
	public static BetterList<UIToggle> betterList_0 = new BetterList<UIToggle>();

	public static UIToggle uitoggle_0;

	public int int_0;

	public UIWidget uiwidget_0;

	public Animation animation_0;

	public bool bool_0;

	public bool bool_1;

	public bool bool_2;

	public List<EventDelegate> list_0 = new List<EventDelegate>();

	[SerializeField]
	private UISprite uisprite_0;

	[SerializeField]
	private Animation animation_1;

	[SerializeField]
	private GameObject gameObject_0;

	[SerializeField]
	private string string_0 = "OnActivate";

	[SerializeField]
	private bool bool_3;

	private bool bool_4 = true;

	private bool bool_5;

	public bool Boolean_0
	{
		get
		{
			return (!bool_5) ? bool_0 : bool_4;
		}
		set
		{
			if (!bool_5)
			{
				bool_0 = value;
			}
			else if (int_0 == 0 || value || bool_2 || !bool_5)
			{
				Set(value);
			}
		}
	}

	[Obsolete("Use 'value' instead")]
	public bool Boolean_1
	{
		get
		{
			return Boolean_0;
		}
		set
		{
			Boolean_0 = value;
		}
	}

	public static UIToggle GetActiveToggle(int int_1)
	{
		int num = 0;
		UIToggle uIToggle;
		while (true)
		{
			if (num < betterList_0.size)
			{
				uIToggle = betterList_0[num];
				if (uIToggle != null && uIToggle.int_0 == int_1 && uIToggle.bool_4)
				{
					break;
				}
				num++;
				continue;
			}
			return null;
		}
		return uIToggle;
	}

	private void OnEnable()
	{
		betterList_0.Add(this);
	}

	private void OnDisable()
	{
		betterList_0.Remove(this);
	}

	private void Start()
	{
		if (bool_3)
		{
			bool_3 = false;
			bool_0 = true;
		}
		if (!Application.isPlaying)
		{
			if (uisprite_0 != null && uiwidget_0 == null)
			{
				uiwidget_0 = uisprite_0;
				uisprite_0 = null;
			}
			if (animation_1 != null && animation_0 == null)
			{
				animation_0 = animation_1;
				animation_1 = null;
			}
			if (Application.isPlaying && uiwidget_0 != null)
			{
				uiwidget_0.Single_2 = ((!bool_0) ? 0f : 1f);
			}
			if (EventDelegate.IsValid(list_0))
			{
				gameObject_0 = null;
				string_0 = null;
			}
		}
		else
		{
			bool_4 = !bool_0;
			bool_5 = true;
			bool flag = bool_1;
			bool_1 = true;
			Set(bool_0);
			bool_1 = flag;
		}
	}

	private void OnClick()
	{
		if (base.enabled)
		{
			Boolean_0 = !Boolean_0;
		}
	}

	private void Set(bool bool_6)
	{
		if (!bool_5)
		{
			bool_4 = bool_6;
			bool_0 = bool_6;
			if (uiwidget_0 != null)
			{
				uiwidget_0.Single_2 = ((!bool_6) ? 0f : 1f);
			}
		}
		else
		{
			if (bool_4 == bool_6)
			{
				return;
			}
			if (int_0 != 0 && bool_6)
			{
				int num = 0;
				int size = betterList_0.size;
				while (num < size)
				{
					UIToggle uIToggle = betterList_0[num];
					if (uIToggle != this && uIToggle.int_0 == int_0)
					{
						uIToggle.Set(false);
					}
					if (betterList_0.size != size)
					{
						size = betterList_0.size;
						num = 0;
					}
					else
					{
						num++;
					}
				}
			}
			bool_4 = bool_6;
			if (uiwidget_0 != null)
			{
				if (bool_1)
				{
					uiwidget_0.Single_2 = ((!bool_4) ? 0f : 1f);
				}
				else
				{
					TweenAlpha.Begin(uiwidget_0.gameObject, 0.15f, (!bool_4) ? 0f : 1f);
				}
			}
			if (uitoggle_0 == null)
			{
				uitoggle_0 = this;
				if (EventDelegate.IsValid(list_0))
				{
					EventDelegate.Execute(list_0);
				}
				else if (gameObject_0 != null && !string.IsNullOrEmpty(string_0))
				{
					gameObject_0.SendMessage(string_0, bool_4, SendMessageOptions.DontRequireReceiver);
				}
				uitoggle_0 = null;
			}
			if (animation_0 != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(animation_0, bool_6 ? Direction.Forward : Direction.Reverse);
				if (bool_1)
				{
					activeAnimation.Finish();
				}
			}
		}
	}
}
