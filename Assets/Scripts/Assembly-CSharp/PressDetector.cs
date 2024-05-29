using System;
using UnityEngine;

internal sealed class PressDetector : MonoBehaviour
{
	public static EventHandler<EventArgs> eventHandler_0;

	private void OnPress(bool bool_0)
	{
		EventHandler<EventArgs> eventHandler = eventHandler_0;
		if (eventHandler != null)
		{
			eventHandler(base.gameObject, EventArgs.Empty);
		}
	}
}
