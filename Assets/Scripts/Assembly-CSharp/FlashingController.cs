using System.Collections;
using UnityEngine;

public class FlashingController : HighlightingController
{
	public Color flashingStartColor = Color.blue;

	public Color flashingEndColor = Color.cyan;

	public float flashingDelay = 2.5f;

	public float flashingFrequency = 2f;

	private void Start()
	{
		StartCoroutine(DelayFlashing());
	}

	protected IEnumerator DelayFlashing()
	{
		yield return new WaitForSeconds(flashingDelay);
		highlightableObject_0.FlashingOn(flashingStartColor, flashingEndColor, flashingFrequency);
	}
}
