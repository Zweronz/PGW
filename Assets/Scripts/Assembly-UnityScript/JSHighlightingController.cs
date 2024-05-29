using System;
using UnityEngine;

[Serializable]
public class JSHighlightingController : MonoBehaviour
{
	protected HighlightableObject highlightableObject_0;

	public virtual void Awake()
	{
		highlightableObject_0 = (HighlightableObject)gameObject.AddComponent(typeof(HighlightableObject));
	}

	public virtual void Update()
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

	public virtual void Main()
	{
	}
}
