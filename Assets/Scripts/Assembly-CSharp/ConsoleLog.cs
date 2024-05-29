using System.Collections.Generic;
using UnityEngine;

public class ConsoleLog
{
	private static ConsoleLog consoleLog_0;

	private List<string> list_0;

	private int int_0 = 512;

	private bool bool_0;

	public string string_0 = string.Empty;

	public static ConsoleLog ConsoleLog_0
	{
		get
		{
			if (consoleLog_0 == null)
			{
				consoleLog_0 = new ConsoleLog();
				Application.RegisterLogCallback(ConsoleLog_0.HandleLog);
			}
			return consoleLog_0;
		}
	}

	public bool Boolean_0
	{
		get
		{
			bool result = bool_0;
			bool_0 = false;
			return result;
		}
	}

	public ConsoleLog()
	{
		bool_0 = false;
		list_0 = new List<string>();
	}

	public void Log(string string_1)
	{
		list_0.Add(string_1);
		if (list_0.Count > int_0)
		{
			int count = list_0.Count - int_0;
			list_0.RemoveRange(0, count);
		}
		string_0 = string.Join("\n", list_0.ToArray());
		bool_0 = true;
	}

	public void HandleLog(string string_1, string string_2, LogType logType_0)
	{
		ConsoleLog_0.Log(string.Format("[{0}] {1}", logType_0.ToString(), string_1));
	}
}
