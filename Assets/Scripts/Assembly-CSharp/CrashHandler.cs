using System;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using engine.data;

public sealed class CrashHandler
{
	private static BaseSharedSettings baseSharedSettings_0;

	public static void Init(BaseSharedSettings baseSharedSettings_1)
	{
		baseSharedSettings_0 = baseSharedSettings_1;
		Application.RegisterLogCallback(HandleUnityLog);
		AppDomain.CurrentDomain.UnhandledException += HandleSystemLog;
		UnityEngine.Debug.Log("CrashHandler::Init > Crash Handler Initialized");
	}

	private static void HandleUnityLog(string string_0, string string_1, LogType logType_0)
	{
		if (logType_0 == LogType.Exception)
		{
			Save(string.Format("{0}\n\n{1}", string_0, string_1));
			UnityEngine.Debug.Log("CrashHandler::HandleUnityLog > Handle Unity Log");
		}
	}

	private static void HandleSystemLog(object sender, UnhandledExceptionEventArgs e)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine(((Exception)e.ExceptionObject).Message);
		stringBuilder.AppendLine();
		int num = 0;
		StackFrame[] frames = new StackTrace().GetFrames();
		foreach (StackFrame stackFrame in frames)
		{
			stringBuilder.AppendFormat("{0}: {1}[{2}]: {3}\n", num, stackFrame.GetFileName(), stackFrame.GetFileLineNumber(), stackFrame.GetMethod().Name);
			num++;
		}
		string string_ = stringBuilder.ToString();
		Save(string_);
		UnityEngine.Debug.Log("CrashHandler::HandleUnityLog > Handle System Log");
	}

	private static void Save(string string_0)
	{
		if (baseSharedSettings_0 != null)
		{
			baseSharedSettings_0.SetValue("last_crash_report", string_0, true);
		}
	}
}
