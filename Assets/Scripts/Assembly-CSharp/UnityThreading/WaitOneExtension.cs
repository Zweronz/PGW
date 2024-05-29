using System;
using System.Threading;

namespace UnityThreading
{
	public static class WaitOneExtension
	{
		public static bool InterWaitOne(this ManualResetEvent manualResetEvent_0, int int_0)
		{
			return manualResetEvent_0.WaitOne(int_0, false);
		}

		public static bool InterWaitOne(this ManualResetEvent manualResetEvent_0, TimeSpan timeSpan_0)
		{
			return manualResetEvent_0.WaitOne(timeSpan_0, false);
		}
	}
}
