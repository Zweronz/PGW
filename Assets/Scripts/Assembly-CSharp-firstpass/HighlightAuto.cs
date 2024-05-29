using UnityEngine;

public class HighlightAuto : MonoBehaviour
{
	private GameObject gameObject_0;

	public Color colorHL;

	private void Start()
	{
		gameObject_0 = base.gameObject;
	}

	private void Update()
	{
		Objectlight();
	}

	public void Objectlight()
	{
		HighlightableObject componentInChildren = gameObject_0.transform.root.GetComponentInChildren<HighlightableObject>();
		if (componentInChildren != null)
		{
			componentInChildren.On(colorHL);
		}
	}
}
