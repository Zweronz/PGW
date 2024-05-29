using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Rilisoft
{
	public sealed class ButtonHandler : MonoBehaviour
	{
		public bool noSound;

		private EventHandler eventHandler_0;

		public event EventHandler Clicked
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_0 = (EventHandler)Delegate.Combine(eventHandler_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_0 = (EventHandler)Delegate.Remove(eventHandler_0, value);
			}
		}

		private void OnClick()
		{
			if (ButtonClickSound.buttonClickSound_0 != null && !noSound)
			{
				ButtonClickSound.buttonClickSound_0.PlayClick();
			}
			EventHandler eventHandler = eventHandler_0;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		public void DoClick()
		{
			OnClick();
		}
	}
}
