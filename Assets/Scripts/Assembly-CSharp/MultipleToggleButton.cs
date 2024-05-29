using System;
using System.Runtime.CompilerServices;
using Rilisoft;
using UnityEngine;

public class MultipleToggleButton : MonoBehaviour
{
	public ToggleButton[] buttons;

	private int int_0;

	private EventHandler<MultipleToggleEventArgs> eventHandler_0;

	public int Int32_0
	{
		get
		{
			return int_0;
		}
		set
		{
			if (buttons == null || value == -1)
			{
				return;
			}
			int_0 = value;
			for (int i = 0; i < buttons.Length; i++)
			{
				if (i != int_0)
				{
					buttons[i].Boolean_0 = false;
				}
			}
			EventHandler<MultipleToggleEventArgs> eventHandler = eventHandler_0;
			if (eventHandler != null)
			{
				eventHandler(this, new MultipleToggleEventArgs
				{
					Int32_0 = int_0
				});
			}
		}
	}

	public event EventHandler<MultipleToggleEventArgs> Clicked
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			eventHandler_0 = (EventHandler<MultipleToggleEventArgs>)Delegate.Combine(eventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			eventHandler_0 = (EventHandler<MultipleToggleEventArgs>)Delegate.Remove(eventHandler_0, value);
		}
	}

	private void Start()
	{
		if (buttons == null)
		{
			return;
		}
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].Clicked += delegate(object sender, ToggleButtonEventArgs e)
			{
				if (e.Boolean_0)
				{
					Int32_0 = Array.IndexOf(buttons, sender as ToggleButton);
				}
			};
		}
	}
}
