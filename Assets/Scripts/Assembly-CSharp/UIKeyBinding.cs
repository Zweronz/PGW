using UnityEngine;

public class UIKeyBinding : MonoBehaviour
{
	public enum Action
	{
		PressAndClick = 0,
		Select = 1
	}

	public enum Modifier
	{
		None = 0,
		Shift = 1,
		Control = 2,
		Alt = 3
	}

	public KeyCode keyCode;

	public Modifier modifier;

	public Action action;

	private bool bool_0;

	private bool bool_1;

	private bool bool_2;

	private void Start()
	{
		UIInput component = GetComponent<UIInput>();
		bool_1 = component != null;
		if (component != null)
		{
			EventDelegate.Add(component.onSubmit, OnSubmit);
		}
	}

	private void OnSubmit()
	{
		if (UICamera.keyCode_0 == keyCode && IsModifierActive())
		{
			bool_0 = true;
		}
	}

	private bool IsModifierActive()
	{
		if (modifier == Modifier.None)
		{
			return true;
		}
		if (modifier == Modifier.Alt)
		{
			if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
			{
				return true;
			}
		}
		else if (modifier == Modifier.Control)
		{
			if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
			{
				return true;
			}
		}
		else if (modifier == Modifier.Shift && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
		{
			return true;
		}
		return false;
	}

	private void Update()
	{
		if (keyCode == KeyCode.None || !IsModifierActive())
		{
			return;
		}
		if (action == Action.PressAndClick)
		{
			if (UICamera.bool_1)
			{
				return;
			}
			UICamera.mouseOrTouch_0 = UICamera.mouseOrTouch_2;
			UICamera.controlScheme_0 = UICamera.ControlScheme.Mouse;
			UICamera.mouseOrTouch_0.gameObject_1 = base.gameObject;
			if (Input.GetKeyDown(keyCode))
			{
				bool_2 = true;
				UICamera.Notify(base.gameObject, "OnPress", true);
			}
			if (Input.GetKeyUp(keyCode))
			{
				UICamera.Notify(base.gameObject, "OnPress", false);
				if (bool_2)
				{
					UICamera.Notify(base.gameObject, "OnClick", null);
					bool_2 = false;
				}
			}
			UICamera.mouseOrTouch_0.gameObject_1 = null;
		}
		else
		{
			if (action != Action.Select || !Input.GetKeyUp(keyCode))
			{
				return;
			}
			if (bool_1)
			{
				if (!bool_0 && !UICamera.bool_1)
				{
					UICamera.GameObject_0 = base.gameObject;
				}
				bool_0 = false;
			}
			else
			{
				UICamera.GameObject_0 = base.gameObject;
			}
		}
	}
}
