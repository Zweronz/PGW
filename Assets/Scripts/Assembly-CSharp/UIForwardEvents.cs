using UnityEngine;

public class UIForwardEvents : MonoBehaviour
{
	public GameObject target;

	public bool onHover;

	public bool onPress;

	public bool onClick;

	public bool onDoubleClick;

	public bool onSelect;

	public bool onDrag;

	public bool onDrop;

	public bool onSubmit;

	public bool onScroll;

	private void OnHover(bool bool_0)
	{
		if (onHover && target != null)
		{
			target.SendMessage("OnHover", bool_0, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnPress(bool bool_0)
	{
		if (onPress && target != null)
		{
			target.SendMessage("OnPress", bool_0, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnClick()
	{
		if (onClick && target != null)
		{
			target.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnDoubleClick()
	{
		if (onDoubleClick && target != null)
		{
			target.SendMessage("OnDoubleClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnSelect(bool bool_0)
	{
		if (onSelect && target != null)
		{
			target.SendMessage("OnSelect", bool_0, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnDrag(Vector2 vector2_0)
	{
		if (onDrag && target != null)
		{
			target.SendMessage("OnDrag", vector2_0, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnDrop(GameObject gameObject_0)
	{
		if (onDrop && target != null)
		{
			target.SendMessage("OnDrop", gameObject_0, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnSubmit()
	{
		if (onSubmit && target != null)
		{
			target.SendMessage("OnSubmit", SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnScroll(float float_0)
	{
		if (onScroll && target != null)
		{
			target.SendMessage("OnScroll", float_0, SendMessageOptions.DontRequireReceiver);
		}
	}
}
