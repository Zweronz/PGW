using System;
using System.Collections;
using UnityEngine;

public class IPUserInteraction : MonoBehaviour
{
	public delegate void OnPickerClicked();

	public delegate void OnDragExit();

	public IPCycler cycler;

	public bool restrictWithinPicker;

	public float exitRecenterDelay = 0.1f;

	private bool bool_0;

	private bool bool_1;

	private bool bool_2;

	public OnPickerClicked onPickerClicked;

	public OnDragExit onDragExit;

	private void OnPress(bool bool_3)
	{
		if (bool_1 != bool_3)
		{
			cycler.OnPress(bool_3);
			bool_1 = bool_3;
		}
		if (bool_2 && !bool_3)
		{
			cycler.ipdragScrollView_0.enabled = true;
			bool_2 = false;
			StopAllCoroutines();
		}
	}

	private void OnScroll(float float_0)
	{
		if (!bool_0)
		{
			ScrollWheelEvents.scrollStartStopHandler_0 = (ScrollWheelEvents.ScrollStartStopHandler)Delegate.Combine(ScrollWheelEvents.scrollStartStopHandler_0, new ScrollWheelEvents.ScrollStartStopHandler(ScrollStartOrStop));
			bool_0 = true;
		}
	}

	private void ScrollStartOrStop(bool bool_3)
	{
		if (!bool_3)
		{
			ScrollWheelEvents.scrollStartStopHandler_0 = (ScrollWheelEvents.ScrollStartStopHandler)Delegate.Remove(ScrollWheelEvents.scrollStartStopHandler_0, new ScrollWheelEvents.ScrollStartStopHandler(ScrollStartOrStop));
			bool_0 = false;
		}
	}

	private void OnClick()
	{
		if (onPickerClicked != null)
		{
			onPickerClicked();
		}
	}

	private void OnDrag()
	{
		if (restrictWithinPicker && !bool_2 && UICamera.mouseOrTouch_0.gameObject_1 != base.gameObject)
		{
			bool_2 = true;
			if (onDragExit != null)
			{
				onDragExit();
			}
			StartCoroutine(DelayedDragExit());
		}
	}

	private IEnumerator DelayedDragExit()
	{
		yield return new WaitForSeconds(exitRecenterDelay);
		if (bool_2)
		{
			cycler.ipdragScrollView_0.enabled = false;
			cycler.OnPress(false);
		}
	}
}
