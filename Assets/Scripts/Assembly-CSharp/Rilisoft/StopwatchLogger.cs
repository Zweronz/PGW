using System;
using System.Diagnostics;
using UnityEngine;

namespace Rilisoft
{
	internal sealed class StopwatchLogger : IDisposable
	{
		private readonly string string_0;

		private readonly Stopwatch stopwatch_0;

		public StopwatchLogger(string string_1)
		{
			string_0 = string_1 ?? string.Empty;
			UnityEngine.Debug.Log(string.Format("{0}: started.", string_0));
			stopwatch_0 = Stopwatch.StartNew();
		}

		public void Dispose()
		{
			stopwatch_0.Stop();
			UnityEngine.Debug.Log(string.Format("{0}: finished at {1:0.00}", string_0, stopwatch_0.ElapsedMilliseconds));
		}
	}
}
