using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

public class UIPlayTween : MonoBehaviour
{
	public static UIPlayTween uiplayTween_0;

	public GameObject tweenTarget;

	public int tweenGroup;

	public Trigger trigger;

	public Direction playDirection = Direction.Forward;

	public bool resetOnPlay;

	public bool resetIfDisabled;

	public EnableCondition ifDisabledOnPlay;

	public DisableCondition disableWhenFinished;

	public bool includeChildren;

	public List<EventDelegate> onFinished = new List<EventDelegate>();

	[SerializeField]
	private GameObject gameObject_0;

	[SerializeField]
	private string string_0;

	private UITweener[] uitweener_0;

	private bool bool_0;

	private int int_0;

	private bool bool_1;

	private void Awake()
	{
		if (gameObject_0 != null && EventDelegate.IsValid(onFinished))
		{
			gameObject_0 = null;
			string_0 = null;
		}
	}

	private void Start()
	{
		bool_0 = true;
		if (tweenTarget == null)
		{
			tweenTarget = base.gameObject;
		}
	}

	private void OnEnable()
	{
		if (bool_0)
		{
			OnHover(UICamera.IsHighlighted(base.gameObject));
		}
		if (UICamera.mouseOrTouch_0 != null)
		{
			if (trigger == Trigger.OnPress || trigger == Trigger.OnPressTrue)
			{
				bool_1 = UICamera.mouseOrTouch_0.gameObject_2 == base.gameObject;
			}
			if (trigger == Trigger.OnHover || trigger == Trigger.OnHoverTrue)
			{
				bool_1 = UICamera.mouseOrTouch_0.gameObject_1 == base.gameObject;
			}
		}
		UIToggle component = GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Add(component.list_0, OnToggle);
		}
	}

	private void OnDisable()
	{
		UIToggle component = GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Remove(component.list_0, OnToggle);
		}
	}

	private void OnHover(bool bool_2)
	{
		if (base.enabled && (trigger == Trigger.OnHover || (trigger == Trigger.OnHoverTrue && bool_2) || (trigger == Trigger.OnHoverFalse && !bool_2)))
		{
			bool_1 = bool_2 && trigger == Trigger.OnHover;
			Play(bool_2);
		}
	}

	private void OnDragOut()
	{
		if (base.enabled && bool_1)
		{
			bool_1 = false;
			Play(false);
		}
	}

	private void OnPress(bool bool_2)
	{
		if (base.enabled && (trigger == Trigger.OnPress || (trigger == Trigger.OnPressTrue && bool_2) || (trigger == Trigger.OnPressFalse && !bool_2)))
		{
			bool_1 = bool_2 && trigger == Trigger.OnPress;
			Play(bool_2);
		}
	}

	private void OnClick()
	{
		if (base.enabled && trigger == Trigger.OnClick)
		{
			Play(true);
		}
	}

	private void OnDoubleClick()
	{
		if (base.enabled && trigger == Trigger.OnDoubleClick)
		{
			Play(true);
		}
	}

	private void OnSelect(bool bool_2)
	{
		if (base.enabled && (trigger == Trigger.OnSelect || (trigger == Trigger.OnSelectTrue && bool_2) || (trigger == Trigger.OnSelectFalse && !bool_2)))
		{
			bool_1 = bool_2 && trigger == Trigger.OnSelect;
			Play(bool_2);
		}
	}

	private void OnToggle()
	{
		if (base.enabled && !(UIToggle.uitoggle_0 == null) && (trigger == Trigger.OnActivate || (trigger == Trigger.OnActivateTrue && UIToggle.uitoggle_0.Boolean_0) || (trigger == Trigger.OnActivateFalse && !UIToggle.uitoggle_0.Boolean_0)))
		{
			Play(UIToggle.uitoggle_0.Boolean_0);
		}
	}

	private void Update()
	{
		if (disableWhenFinished == DisableCondition.DoNotDisable || uitweener_0 == null)
		{
			return;
		}
		bool flag = true;
		bool flag2 = true;
		int i = 0;
		for (int num = uitweener_0.Length; i < num; i++)
		{
			UITweener uITweener = uitweener_0[i];
			if (uITweener.int_0 == tweenGroup)
			{
				if (uITweener.enabled)
				{
					flag = false;
					break;
				}
				if (uITweener.Direction_0 != (Direction)disableWhenFinished)
				{
					flag2 = false;
				}
			}
		}
		if (flag)
		{
			if (flag2)
			{
				NGUITools.SetActive(tweenTarget, false);
			}
			uitweener_0 = null;
		}
	}

	public void Play(bool bool_2)
	{
		int_0 = 0;
		GameObject gameObject = ((!(tweenTarget == null)) ? tweenTarget : base.gameObject);
		if (!NGUITools.GetActive(gameObject))
		{
			if (ifDisabledOnPlay != EnableCondition.EnableThenPlay)
			{
				return;
			}
			NGUITools.SetActive(gameObject, true);
		}
		uitweener_0 = ((!includeChildren) ? gameObject.GetComponents<UITweener>() : gameObject.GetComponentsInChildren<UITweener>());
		if (uitweener_0.Length == 0)
		{
			if (disableWhenFinished != 0)
			{
				NGUITools.SetActive(tweenTarget, false);
			}
			return;
		}
		bool flag = false;
		if (playDirection == Direction.Reverse)
		{
			bool_2 = !bool_2;
		}
		int i = 0;
		for (int num = uitweener_0.Length; i < num; i++)
		{
			UITweener uITweener = uitweener_0[i];
			if (uITweener.int_0 != tweenGroup)
			{
				continue;
			}
			if (!flag && !NGUITools.GetActive(gameObject))
			{
				flag = true;
				NGUITools.SetActive(gameObject, true);
			}
			int_0++;
			if (playDirection == Direction.Toggle)
			{
				EventDelegate.Add(uITweener.list_0, OnFinished, true);
				uITweener.Toggle();
				continue;
			}
			if (resetOnPlay || (resetIfDisabled && !uITweener.enabled))
			{
				uITweener.ResetToBeginning();
			}
			EventDelegate.Add(uITweener.list_0, OnFinished, true);
			uITweener.Play(bool_2);
		}
	}

	private void OnFinished()
	{
		if (--int_0 == 0 && uiplayTween_0 == null)
		{
			uiplayTween_0 = this;
			EventDelegate.Execute(onFinished);
			if (gameObject_0 != null && !string.IsNullOrEmpty(string_0))
			{
				gameObject_0.SendMessage(string_0, SendMessageOptions.DontRequireReceiver);
			}
			gameObject_0 = null;
			uiplayTween_0 = null;
		}
	}
}
