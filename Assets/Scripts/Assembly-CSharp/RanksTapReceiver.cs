using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RanksTapReceiver : MonoBehaviour
{
	private static Action action_0;

	public static event Action RanksClicked
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

	private void Start()
	{
		base.gameObject.SetActive(Defs.bool_2);
	}

	private void OnClick()
	{
		if (action_0 != null)
		{
			action_0();
		}
	}
}
