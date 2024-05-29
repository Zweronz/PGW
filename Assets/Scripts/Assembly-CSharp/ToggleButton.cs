using System;
using System.Runtime.CompilerServices;
using Rilisoft;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
	public UIButton offButton;

	public UIButton onButton;

	public bool useForMultipleToggle = true;

	private bool bool_0;

	private EventHandler<ToggleButtonEventArgs> eventHandler_0;

	public bool Boolean_0
	{
		get
		{
			return bool_0;
		}
		set
		{
			bool_0 = value;
			offButton.gameObject.SetActive(!bool_0);
			onButton.gameObject.SetActive(bool_0);
			if (useForMultipleToggle)
			{
				onButton.Boolean_0 = !onButton.gameObject.activeSelf;
			}
			EventHandler<ToggleButtonEventArgs> eventHandler = eventHandler_0;
			if (eventHandler != null)
			{
				eventHandler(this, new ToggleButtonEventArgs
				{
					Boolean_0 = bool_0
				});
			}
		}
	}

	public event EventHandler<ToggleButtonEventArgs> Clicked
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			eventHandler_0 = (EventHandler<ToggleButtonEventArgs>)Delegate.Combine(eventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			eventHandler_0 = (EventHandler<ToggleButtonEventArgs>)Delegate.Remove(eventHandler_0, value);
		}
	}

	public void SetCheckedImage(bool bool_1)
	{
		offButton.gameObject.SetActive(!bool_1);
		onButton.gameObject.SetActive(bool_1);
		if (useForMultipleToggle)
		{
			onButton.Boolean_0 = !onButton.gameObject.activeSelf;
		}
	}

	private void Start()
	{
		onButton.GetComponent<ButtonHandler>().Clicked += delegate
		{
			Boolean_0 = false;
		};
		offButton.GetComponent<ButtonHandler>().Clicked += delegate
		{
			Boolean_0 = true;
		};
	}
}
