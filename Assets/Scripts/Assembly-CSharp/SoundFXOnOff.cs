using UnityEngine;

public class SoundFXOnOff : MonoBehaviour
{
	private GameObject gameObject_0;

	private void Start()
	{
		gameObject_0 = base.transform.GetChild(0).gameObject;
		gameObject_0.SetActive(Defs.Boolean_0);
	}

	private void FixedUpdate()
	{
		if (gameObject_0.activeSelf != Defs.Boolean_0)
		{
			gameObject_0.SetActive(Defs.Boolean_0);
		}
	}

	private void OnApplicationFocus(bool bool_0)
	{
		if (Defs.Boolean_0)
		{
			gameObject_0.SetActive(bool_0);
		}
	}
}
