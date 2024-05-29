using UnityEngine;

public class ConsoleToggler : MonoBehaviour
{
	private bool bool_0;

	public ConsoleAction ConsoleOpenAction;

	public ConsoleAction ConsoleCloseAction;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			ToggleConsole();
		}
	}

	private void ToggleConsole()
	{
		bool_0 = !bool_0;
		if (bool_0)
		{
			ConsoleOpenAction.Activate();
		}
		else
		{
			ConsoleCloseAction.Activate();
		}
	}
}
