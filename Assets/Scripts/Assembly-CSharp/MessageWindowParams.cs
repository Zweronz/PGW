using System;
using UnityEngine;
using engine.unity;

public class MessageWindowParams : WindowShowParameters
{
	public string string_0;

	public string string_1;

	public Action action_0;

	public KeyCode keyCode_0;

	public WindowController.GameEvent gameEvent_0;

	public MessageWindowParams(string string_2, Action action_1 = null, string string_3 = "OK", KeyCode keyCode_1 = KeyCode.None, WindowController.GameEvent gameEvent_1 = WindowController.GameEvent.UNUSED)
	{
		string_0 = string_2;
		action_0 = action_1;
		string_1 = string_3;
		keyCode_0 = keyCode_1;
		gameEvent_0 = gameEvent_1;
	}
}
