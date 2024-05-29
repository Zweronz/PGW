using System;
using System.Runtime.CompilerServices;
using UnityEngine;

internal sealed class ControlSizeSlider : MonoBehaviour
{
	public class EnabledChangedEventArgs : EventArgs
	{
		[CompilerGenerated]
		private bool bool_0;

		public bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_0;
			}
			[CompilerGenerated]
			set
			{
				bool_0 = value;
			}
		}
	}

	public UISlider slider;

	private EventHandler<EnabledChangedEventArgs> eventHandler_0;

	public event EventHandler<EnabledChangedEventArgs> EnabledChanged
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			eventHandler_0 = (EventHandler<EnabledChangedEventArgs>)Delegate.Combine(eventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			eventHandler_0 = (EventHandler<EnabledChangedEventArgs>)Delegate.Remove(eventHandler_0, value);
		}
	}

	private void OnEnable()
	{
		EventHandler<EnabledChangedEventArgs> eventHandler = eventHandler_0;
		if (eventHandler != null)
		{
			eventHandler(slider, new EnabledChangedEventArgs
			{
				Boolean_0 = true
			});
		}
	}

	private void OnDisable()
	{
		EventHandler<EnabledChangedEventArgs> eventHandler = eventHandler_0;
		if (eventHandler != null)
		{
			eventHandler(slider, new EnabledChangedEventArgs
			{
				Boolean_0 = false
			});
		}
	}
}
