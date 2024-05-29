using System;
using UnityEngine;

public class MessageWindowConfirmParams : MessageWindowParams
{
	public string string_2;

	public Action action_1;

	public MessageWindowConfirmParams(string string_3, Action action_2 = null, string string_4 = "OK", KeyCode keyCode_1 = KeyCode.None, Action action_3 = null, string string_5 = "")
		: base(string_3, action_2, string_4, keyCode_1)
	{
		string_2 = string_5;
		action_1 = action_3;
	}
}
