using System;
using UnityEngine;

public class MessageRenameWindowParams : MessageWindowParams
{
	public bool bool_0;

	public MessageRenameWindowParams(string string_2, bool bool_1, Action action_1 = null, string string_3 = "OK", KeyCode keyCode_1 = KeyCode.None)
		: base(string_2, action_1, string_3, keyCode_1)
	{
		bool_0 = bool_1;
	}
}
