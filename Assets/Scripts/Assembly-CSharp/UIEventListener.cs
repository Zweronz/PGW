using UnityEngine;

public class UIEventListener : MonoBehaviour
{
	public delegate void VoidDelegate(GameObject gameObject_0);

	public delegate void BoolDelegate(GameObject gameObject_0, bool bool_0);

	public delegate void FloatDelegate(GameObject gameObject_0, float float_0);

	public delegate void VectorDelegate(GameObject gameObject_0, Vector2 vector2_0);

	public delegate void ObjectDelegate(GameObject gameObject_0, GameObject gameObject_1);

	public delegate void KeyCodeDelegate(GameObject gameObject_0, KeyCode keyCode_0);

	public object parameter;

	public VoidDelegate onSubmit;

	public VoidDelegate onClick;

	public VoidDelegate onDoubleClick;

	public BoolDelegate onHover;

	public BoolDelegate onPress;

	public BoolDelegate onSelect;

	public FloatDelegate onScroll;

	public VectorDelegate onDrag;

	public VoidDelegate onDragOver;

	public VoidDelegate onDragOut;

	public ObjectDelegate onDrop;

	public KeyCodeDelegate onKey;

	private void OnSubmit()
	{
		if (onSubmit != null)
		{
			onSubmit(base.gameObject);
		}
	}

	private void OnClick()
	{
		if (onClick != null)
		{
			onClick(base.gameObject);
		}
	}

	private void OnDoubleClick()
	{
		if (onDoubleClick != null)
		{
			onDoubleClick(base.gameObject);
		}
	}

	private void OnHover(bool bool_0)
	{
		if (onHover != null)
		{
			onHover(base.gameObject, bool_0);
		}
	}

	private void OnPress(bool bool_0)
	{
		if (onPress != null)
		{
			onPress(base.gameObject, bool_0);
		}
	}

	private void OnSelect(bool bool_0)
	{
		if (onSelect != null)
		{
			onSelect(base.gameObject, bool_0);
		}
	}

	private void OnScroll(float float_0)
	{
		if (onScroll != null)
		{
			onScroll(base.gameObject, float_0);
		}
	}

	private void OnDrag(Vector2 vector2_0)
	{
		if (onDrag != null)
		{
			onDrag(base.gameObject, vector2_0);
		}
	}

	private void OnDragOver()
	{
		if (onDragOver != null)
		{
			onDragOver(base.gameObject);
		}
	}

	private void OnDragOut()
	{
		if (onDragOut != null)
		{
			onDragOut(base.gameObject);
		}
	}

	private void OnDrop(GameObject gameObject_0)
	{
		if (onDrop != null)
		{
			onDrop(base.gameObject, gameObject_0);
		}
	}

	private void OnKey(KeyCode keyCode_0)
	{
		if (onKey != null)
		{
			onKey(base.gameObject, keyCode_0);
		}
	}

	public static UIEventListener Get(GameObject gameObject_0)
	{
		UIEventListener uIEventListener = gameObject_0.GetComponent<UIEventListener>();
		if (uIEventListener == null)
		{
			uIEventListener = gameObject_0.AddComponent<UIEventListener>();
		}
		return uIEventListener;
	}
}
