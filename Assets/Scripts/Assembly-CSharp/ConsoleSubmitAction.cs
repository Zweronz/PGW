using System.Linq;
using UnityEngine;

public class ConsoleSubmitAction : ConsoleAction
{
	public ConsoleGUI consoleGUI_0;

	private ConsoleCommandsRepository consoleCommandsRepository_0;

	private ConsoleLog consoleLog_0;

	private void Start()
	{
		consoleCommandsRepository_0 = ConsoleCommandsRepository.ConsoleCommandsRepository_0;
		consoleLog_0 = ConsoleLog.ConsoleLog_0;
	}

	public override void Activate()
	{
		string[] array = consoleGUI_0.string_0.Split(' ');
		string text = array[0];
		string[] string_ = array.Skip(1).ToArray();
		consoleLog_0.Log("> " + consoleGUI_0.string_0);
		if (consoleCommandsRepository_0.HasCommand(text))
		{
			consoleCommandsRepository_0.ExecuteCommand(text, string_);
		}
		else
		{
			Debug.LogError("Command " + text + " not found");
		}
	}
}
