using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace WebSocketSharp
{
	public class LogData
	{
		private StackFrame stackFrame_0;

		private DateTime dateTime_0;

		private LogLevel logLevel_0;

		private string string_0;

		public StackFrame StackFrame_0
		{
			get
			{
				return stackFrame_0;
			}
		}

		public DateTime DateTime_0
		{
			get
			{
				return dateTime_0;
			}
		}

		public LogLevel LogLevel_0
		{
			get
			{
				return logLevel_0;
			}
		}

		public string String_0
		{
			get
			{
				return string_0;
			}
		}

		internal LogData(LogLevel logLevel_1, StackFrame stackFrame_1, string string_1)
		{
			logLevel_0 = logLevel_1;
			stackFrame_0 = stackFrame_1;
			string_0 = string_1 ?? string.Empty;
			dateTime_0 = DateTime.Now;
		}

		public override string ToString()
		{
			string text = string.Format("{0}|{1,-5}|", dateTime_0, logLevel_0);
			MethodBase method = stackFrame_0.GetMethod();
			Type declaringType = method.DeclaringType;
			string arg = string.Format("{0}{1}.{2}|", text, declaringType.Name, method.Name);
			string[] array = string_0.Replace("\r\n", "\n").TrimEnd('\n').Split('\n');
			if (array.Length <= 1)
			{
				return string.Format("{0}{1}", arg, string_0);
			}
			StringBuilder stringBuilder = new StringBuilder(string.Format("{0}{1}\n", arg, array[0]), 64);
			string format = string.Format("{{0,{0}}}{{1}}\n", text.Length);
			for (int i = 1; i < array.Length; i++)
			{
				stringBuilder.AppendFormat(format, string.Empty, array[i]);
			}
			stringBuilder.Length--;
			return stringBuilder.ToString();
		}
	}
}
