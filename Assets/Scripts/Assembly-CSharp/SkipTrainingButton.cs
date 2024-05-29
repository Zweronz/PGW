using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SkipTrainingButton : MonoBehaviour
{
	private static Action action_0;

	public static event Action SkipTrClosed
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			action_0 = (Action)Delegate.Combine(action_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			action_0 = (Action)Delegate.Remove(action_0, value);
		}
	}

	protected virtual void OnClick()
	{
		if (action_0 != null)
		{
			action_0();
		}
		Resources.UnloadUnusedAssets();
	}
}
