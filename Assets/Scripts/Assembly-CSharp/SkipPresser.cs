using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SkipPresser : MonoBehaviour
{
	public GameObject windowAnchor;

	private static Action action_0;

	public static event Action SkipPressed
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
	}

	private void OnClick()
	{
		base.gameObject.SetActive(false);
		windowAnchor.SetActive(true);
		if (action_0 != null)
		{
			action_0();
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("PlayerGun");
		if ((bool)gameObject && gameObject != null)
		{
			Transform child = gameObject.transform.GetChild(0);
			if ((bool)child && child != null)
			{
				child.gameObject.SetActive(false);
			}
		}
	}

	private void Update()
	{
	}
}
