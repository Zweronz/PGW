using System;
using System.Runtime.CompilerServices;
using UnityEngine;

internal sealed class Pauser : MonoBehaviour
{
	private Action action_0;

	public bool pausedVar;

	[CompilerGenerated]
	private static Action action_1;

	public bool Boolean_0
	{
		get
		{
			return pausedVar;
		}
		set
		{
			pausedVar = value;
		}
	}

	private void Start()
	{
		action_0 = delegate
		{
		};
		Initializer.PlayerAddedEvent += action_0;
	}

	private void OnDestroy()
	{
		Initializer.PlayerAddedEvent -= action_0;
	}
}
