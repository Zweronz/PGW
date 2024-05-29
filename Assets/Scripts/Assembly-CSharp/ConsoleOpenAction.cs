using UnityEngine;

public class ConsoleOpenAction : ConsoleAction
{
	public GameObject gameObject_0;

	public override void Activate()
	{
		gameObject_0.SetActive(true);
	}
}
