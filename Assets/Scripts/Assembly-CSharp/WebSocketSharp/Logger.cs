using System;
using System.Diagnostics;
using System.IO;

namespace WebSocketSharp
{
	public class Logger
	{
		private volatile string string_0;

		private volatile LogLevel logLevel_0;

		private Action<LogData, string> action_0;

		private object object_0;

		public string String_0
		{
			get
			{
				return string_0;
			}
			set
			{
				lock (object_0)
				{
					string_0 = value;
					Warn(string.Format("The current path to the log file has been changed to {0}.", string_0));
				}
			}
		}

		public LogLevel LogLevel_0
		{
			get
			{
				return logLevel_0;
			}
			set
			{
				lock (object_0)
				{
					logLevel_0 = value;
					Warn(string.Format("The current logging level has been changed to {0}.", logLevel_0));
				}
			}
		}

		public Action<LogData, string> Action_0
		{
			get
			{
				return action_0;
			}
			set
			{
				lock (object_0)
				{
					action_0 = value ?? new Action<LogData, string>(defaultOutput);
					Warn("The current output action has been changed.");
				}
			}
		}

		public Logger()
			: this(LogLevel.Error, null, null)
		{
		}

		public Logger(LogLevel logLevel_1)
			: this(logLevel_1, null, null)
		{
		}

		public Logger(LogLevel logLevel_1, string string_1, Action<LogData, string> action_1)
		{
			logLevel_0 = logLevel_1;
			string_0 = string_1;
			action_0 = action_1 ?? new Action<LogData, string>(defaultOutput);
			object_0 = new object();
		}

		private static void defaultOutput(LogData logData_0, string string_1)
		{
			string text = logData_0.ToString();
			Console.WriteLine(text);
			if (string_1 != null && string_1.Length > 0)
			{
				writeToFile(text, string_1);
			}
		}

		private void output(string string_1, LogLevel logLevel_1)
		{
			lock (object_0)
			{
				if (logLevel_0 > logLevel_1)
				{
					return;
				}
				LogData logData = null;
				try
				{
					logData = new LogData(logLevel_1, new StackFrame(2, true), string_1);
					action_0(logData, string_0);
				}
				catch (Exception ex)
				{
					logData = new LogData(LogLevel.Fatal, new StackFrame(0, true), ex.Message);
					Console.WriteLine(logData.ToString());
				}
			}
		}

		private static void writeToFile(string string_1, string string_2)
		{
			using (StreamWriter writer = new StreamWriter(string_2, true))
			{
				using (TextWriter textWriter = TextWriter.Synchronized(writer))
				{
					textWriter.WriteLine(string_1);
				}
			}
		}

		public void Debug(string string_1)
		{
			if (logLevel_0 <= LogLevel.Debug)
			{
				output(string_1, LogLevel.Debug);
			}
		}

		public void Error(string string_1)
		{
			if (logLevel_0 <= LogLevel.Error)
			{
				output(string_1, LogLevel.Error);
			}
		}

		public void Fatal(string string_1)
		{
			output(string_1, LogLevel.Fatal);
		}

		public void Info(string string_1)
		{
			if (logLevel_0 <= LogLevel.Info)
			{
				output(string_1, LogLevel.Info);
			}
		}

		public void Trace(string string_1)
		{
			if (logLevel_0 <= LogLevel.Trace)
			{
				output(string_1, LogLevel.Trace);
			}
		}

		public void Warn(string string_1)
		{
			if (logLevel_0 <= LogLevel.Warn)
			{
				output(string_1, LogLevel.Warn);
			}
		}
	}
}
