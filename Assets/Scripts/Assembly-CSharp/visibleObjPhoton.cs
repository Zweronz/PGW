using UnityEngine;

public class visibleObjPhoton : MonoBehaviour
{
	public ThirdPersonNetwork1 lerpScript;

	public bool isVisible;

	private void Awake()
	{
		if (!Defs.bool_2 || !Defs.bool_3)
		{
			base.enabled = false;
		}
	}

	private void OnBecameVisible()
	{
		isVisible = true;
		if (lerpScript != null)
		{
			lerpScript.bool_0 = true;
		}
	}

	private void OnBecameInvisible()
	{
		isVisible = false;
		if (lerpScript != null)
		{
			lerpScript.bool_0 = false;
		}
	}
}
