using System.Collections.Generic;
using UnityEngine;

public class UIEventTrigger : MonoBehaviour
{
	public static UIEventTrigger uieventTrigger_0;

	public List<EventDelegate> onHoverOver = new List<EventDelegate>();

	public List<EventDelegate> onHoverOut = new List<EventDelegate>();

	public List<EventDelegate> onPress = new List<EventDelegate>();

	public List<EventDelegate> onRelease = new List<EventDelegate>();

	public List<EventDelegate> onSelect = new List<EventDelegate>();

	public List<EventDelegate> onDeselect = new List<EventDelegate>();

	public List<EventDelegate> onClick = new List<EventDelegate>();

	public List<EventDelegate> onDoubleClick = new List<EventDelegate>();

	public List<EventDelegate> onDragOver = new List<EventDelegate>();

	public List<EventDelegate> onDragOut = new List<EventDelegate>();

	private void OnHover(bool bool_0)
	{
		if (!(uieventTrigger_0 != null))
		{
			uieventTrigger_0 = this;
			if (bool_0)
			{
				EventDelegate.Execute(onHoverOver);
			}
			else
			{
				EventDelegate.Execute(onHoverOut);
			}
			uieventTrigger_0 = null;
		}
	}

	private void OnPress(bool bool_0)
	{
		if (!(uieventTrigger_0 != null))
		{
			uieventTrigger_0 = this;
			if (bool_0)
			{
				EventDelegate.Execute(onPress);
			}
			else
			{
				EventDelegate.Execute(onRelease);
			}
			uieventTrigger_0 = null;
		}
	}

	private void OnSelect(bool bool_0)
	{
		if (!(uieventTrigger_0 != null))
		{
			uieventTrigger_0 = this;
			if (bool_0)
			{
				EventDelegate.Execute(onSelect);
			}
			else
			{
				EventDelegate.Execute(onDeselect);
			}
			uieventTrigger_0 = null;
		}
	}

	private void OnClick()
	{
		if (!(uieventTrigger_0 != null))
		{
			uieventTrigger_0 = this;
			EventDelegate.Execute(onClick);
			uieventTrigger_0 = null;
		}
	}

	private void OnDoubleClick()
	{
		if (!(uieventTrigger_0 != null))
		{
			uieventTrigger_0 = this;
			EventDelegate.Execute(onDoubleClick);
			uieventTrigger_0 = null;
		}
	}

	private void OnDragOver(GameObject gameObject_0)
	{
		if (!(uieventTrigger_0 != null))
		{
			uieventTrigger_0 = this;
			EventDelegate.Execute(onDragOver);
			uieventTrigger_0 = null;
		}
	}

	private void OnDragOut(GameObject gameObject_0)
	{
		if (!(uieventTrigger_0 != null))
		{
			uieventTrigger_0 = this;
			EventDelegate.Execute(onDragOut);
			uieventTrigger_0 = null;
		}
	}
}
