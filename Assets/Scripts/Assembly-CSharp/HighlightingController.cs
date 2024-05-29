using UnityEngine;

public class HighlightingController : MonoBehaviour
{
	protected HighlightableObject highlightableObject_0;

	private void Awake()
	{
		highlightableObject_0 = base.gameObject.AddComponent<HighlightableObject>();
	}

	private void Update()
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
		AfterUpdate();
	}

	protected virtual void AfterUpdate()
	{
	}
}
