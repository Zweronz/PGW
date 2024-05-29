using UnityEngine;

public class ConsoleCloseAction : ConsoleAction
{
	public GameObject gameObject_0;

	public override void Activate()
	{
		gameObject_0.SetActive(false);
	}
}
