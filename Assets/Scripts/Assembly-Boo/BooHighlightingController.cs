using System;
using UnityEngine;

[Serializable]
public class BooHighlightingController : MonoBehaviour
{
	protected HighlightableObject highlightableObject_0;

	public void Awake()
	{
		highlightableObject_0 = gameObject.AddComponent<HighlightableObject>() as HighlightableObject;
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			highlightableObject_0.ConstantSwitch();
		}
		else if (Input.GetKeyDown(KeyCode.Q))
		{
			highlightableObject_0.ConstantSwitchImmediate();
		}
		if (Input.GetKeyDown(KeyCode.Z))
		{
			highlightableObject_0.Off();
		}
	}
}
