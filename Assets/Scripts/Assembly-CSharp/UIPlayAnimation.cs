using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

public class UIPlayAnimation : MonoBehaviour
{
	public static UIPlayAnimation uiplayAnimation_0;

	public Animation target;

	public Animator animator;

	public string clipName;

	public Trigger trigger;

	public Direction playDirection = Direction.Forward;

	public bool resetOnPlay;

	public bool clearSelection;

	public EnableCondition ifDisabledOnPlay;

	public DisableCondition disableWhenFinished;

	public List<EventDelegate> onFinished = new List<EventDelegate>();

	[SerializeField]
	private GameObject gameObject_0;

	[SerializeField]
	private string string_0;

	private bool bool_0;

	private bool bool_1;

	private bool bool_2;

	private bool Boolean_0
	{
		get
		{
			return trigger == Trigger.OnPress || trigger == Trigger.OnHover;
		}
	}

	private void Awake()
	{
		UIButton component = GetComponent<UIButton>();
		if (component != null)
		{
			bool_2 = component.bool_1;
		}
		if (gameObject_0 != null && EventDelegate.IsValid(onFinished))
		{
			gameObject_0 = null;
			string_0 = null;
		}
	}

	private void Start()
	{
		bool_0 = true;
		if (target == null && animator == null)
		{
			animator = GetComponentInChildren<Animator>();
		}
		if (animator != null)
		{
			if (animator.enabled)
			{
				animator.enabled = false;
			}
			return;
		}
		if (target == null)
		{
			target = GetComponentInChildren<Animation>();
		}
		if (target != null && target.enabled)
		{
			target.enabled = false;
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

	private void OnHover(bool bool_3)
	{
		if (base.enabled && (trigger == Trigger.OnHover || (trigger == Trigger.OnHoverTrue && bool_3) || (trigger == Trigger.OnHoverFalse && !bool_3)))
		{
			Play(bool_3, Boolean_0);
		}
	}

	private void OnPress(bool bool_3)
	{
		if (base.enabled && (trigger == Trigger.OnPress || (trigger == Trigger.OnPressTrue && bool_3) || (trigger == Trigger.OnPressFalse && !bool_3)))
		{
			Play(bool_3, Boolean_0);
		}
	}

	private void OnClick()
	{
		if (base.enabled && trigger == Trigger.OnClick)
		{
			Play(true, false);
		}
	}

	private void OnDoubleClick()
	{
		if (base.enabled && trigger == Trigger.OnDoubleClick)
		{
			Play(true, false);
		}
	}

	private void OnSelect(bool bool_3)
	{
		if (base.enabled && (trigger == Trigger.OnSelect || (trigger == Trigger.OnSelectTrue && bool_3) || (trigger == Trigger.OnSelectFalse && !bool_3)))
		{
			Play(bool_3, Boolean_0);
		}
	}

	private void OnToggle()
	{
		if (base.enabled && !(UIToggle.uitoggle_0 == null) && (trigger == Trigger.OnActivate || (trigger == Trigger.OnActivateTrue && UIToggle.uitoggle_0.Boolean_0) || (trigger == Trigger.OnActivateFalse && !UIToggle.uitoggle_0.Boolean_0)))
		{
			Play(UIToggle.uitoggle_0.Boolean_0, Boolean_0);
		}
	}

	private void OnDragOver()
	{
		if (base.enabled && Boolean_0)
		{
			if (UICamera.mouseOrTouch_0.gameObject_3 == base.gameObject)
			{
				Play(true, true);
			}
			else if (bool_2 && trigger == Trigger.OnPress)
			{
				Play(true, true);
			}
		}
	}

	private void OnDragOut()
	{
		if (base.enabled && Boolean_0 && UICamera.gameObject_6 != base.gameObject)
		{
			Play(false, true);
		}
	}

	private void OnDrop(GameObject gameObject_1)
	{
		if (base.enabled && trigger == Trigger.OnPress && UICamera.mouseOrTouch_0.gameObject_3 != base.gameObject)
		{
			Play(false, true);
		}
	}

	public void Play(bool bool_3)
	{
		Play(bool_3, true);
	}

	public void Play(bool bool_3, bool bool_4)
	{
		if (!target && !animator)
		{
			return;
		}
		if (bool_4)
		{
			if (bool_1 == bool_3)
			{
				return;
			}
			bool_1 = bool_3;
		}
		if (clearSelection && UICamera.GameObject_0 == base.gameObject)
		{
			UICamera.GameObject_0 = null;
		}
		int num = 0 - playDirection;
		Direction direction_ = ((!bool_3) ? ((Direction)num) : playDirection);
		ActiveAnimation activeAnimation = ((!target) ? ActiveAnimation.Play(animator, clipName, direction_, ifDisabledOnPlay, disableWhenFinished) : ActiveAnimation.Play(target, clipName, direction_, ifDisabledOnPlay, disableWhenFinished));
		if (activeAnimation != null)
		{
			if (resetOnPlay)
			{
				activeAnimation.Reset();
			}
			for (int i = 0; i < onFinished.Count; i++)
			{
				EventDelegate.Add(activeAnimation.onFinished, OnFinished, true);
			}
		}
	}

	private void OnFinished()
	{
		if (uiplayAnimation_0 == null)
		{
			uiplayAnimation_0 = this;
			EventDelegate.Execute(onFinished);
			if (gameObject_0 != null && !string.IsNullOrEmpty(string_0))
			{
				gameObject_0.SendMessage(string_0, SendMessageOptions.DontRequireReceiver);
			}
			gameObject_0 = null;
			uiplayAnimation_0 = null;
		}
	}
}
