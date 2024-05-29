using System.Collections.Generic;

public class ConsoleCommandsRepository
{
	private static ConsoleCommandsRepository consoleCommandsRepository_0;

	private Dictionary<string, ConsoleCommandCallback> dictionary_0;

	public static ConsoleCommandsRepository ConsoleCommandsRepository_0
	{
		get
		{
			if (consoleCommandsRepository_0 == null)
			{
				consoleCommandsRepository_0 = new ConsoleCommandsRepository();
			}
			return consoleCommandsRepository_0;
		}
	}

	public ConsoleCommandsRepository()
	{
		dictionary_0 = new Dictionary<string, ConsoleCommandCallback>();
		RegisterCommand("list", ListCommands);
	}

	public void RegisterCommand(string string_0, ConsoleCommandCallback consoleCommandCallback_0)
	{
		if (HasCommand(string_0))
		{
			ConsoleLog.ConsoleLog_0.Log(string.Format("Command already exists: {0}, new definition ignored", string_0));
		}
		else
		{
			dictionary_0[string_0] = consoleCommandCallback_0.Invoke;
		}
	}

	public bool HasCommand(string string_0)
	{
		return dictionary_0.ContainsKey(string_0);
	}

	public void ExecuteCommand(string string_0, string[] string_1)
	{
		if (HasCommand(string_0))
		{
			dictionary_0[string_0](string_1);
		}
		else
		{
			ConsoleLog.ConsoleLog_0.Log("Command not found");
		}
	}

	public void ListCommands(params string[] string_0)
	{
		ConsoleLog.ConsoleLog_0.Log("Commands:");
		foreach (KeyValuePair<string, ConsoleCommandCallback> item in dictionary_0)
		{
			ConsoleLog.ConsoleLog_0.Log(item.Key);
		}
	}

	public List<string> SearchCommands(string string_0)
	{
		string[] array = new string[dictionary_0.Count];
		dictionary_0.Keys.CopyTo(array, 0);
		List<string> list = new List<string>();
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (text.StartsWith(string_0))
			{
				list.Add(text);
			}
		}
		return list;
	}
}
