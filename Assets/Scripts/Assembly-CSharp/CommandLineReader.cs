using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.helpers;

public class CommandLineReader
{
	private const string string_0 = "-CustomArgs:";

	private const char char_0 = ',';

	[CompilerGenerated]
	private static Func<string, bool> func_0;

	public static string[] GetCommandLineArgs()
	{
		return Environment.GetCommandLineArgs();
	}

	public static string GetCommandLine()
	{
		string[] commandLineArgs = GetCommandLineArgs();
		if (commandLineArgs.Length > 0)
		{
			return string.Join(" ", commandLineArgs);
		}
		Log.AddLine("CommandLineReader.cs - GetCommandLine() - Can't find any command line arguments!", Log.LogLevel.WARNING);
		return string.Empty;
	}

	public static Dictionary<string, string> GetCustomArguments()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		string[] commandLineArgs = GetCommandLineArgs();
		string empty = string.Empty;
		try
		{
			empty = commandLineArgs.Where((string string_1) => string_1.Contains("-CustomArgs:")).Single();
		}
		catch (Exception ex)
		{
			Log.AddLine(string.Concat("CommandLineReader.cs - GetCustomArguments() - Can't retrieve any custom arguments in the command line [", commandLineArgs, "]. Exception: ", ex), Log.LogLevel.WARNING);
			return dictionary;
		}
		empty = empty.Replace("-CustomArgs:", string.Empty);
		string[] array = empty.Split(',');
		string[] array2 = array;
		foreach (string text in array2)
		{
			string[] array3 = text.Split('=');
			if (array3.Length == 2)
			{
				dictionary.Add(array3[0], array3[1]);
			}
			else
			{
				Debug.LogWarning("CommandLineReader.cs - GetCustomArguments() - The custom argument [" + text + "] seem to be malformed.");
			}
		}
		return dictionary;
	}

	public static string GetCustomArgument(string string_1)
	{
		Dictionary<string, string> customArguments = GetCustomArguments();
		if (customArguments.ContainsKey(string_1))
		{
			return customArguments[string_1];
		}
		Log.AddLine("CommandLineReader.cs - GetCustomArgument() - Can't retrieve any custom argument named [" + string_1 + "] in the command line [" + GetCommandLine() + "].", Log.LogLevel.WARNING);
		return string.Empty;
	}
}
