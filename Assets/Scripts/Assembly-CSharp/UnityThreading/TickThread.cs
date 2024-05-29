using System;
using System.Collections;
using System.Threading;

namespace UnityThreading
{
	public sealed class TickThread : ThreadBase
	{
		private Action action_0;

		private int int_0;

		private ManualResetEvent manualResetEvent_1 = new ManualResetEvent(false);

		public TickThread(Action action_1, int int_1)
			: this(action_1, int_1, true)
		{
		}

		public TickThread(Action action_1, int int_1, bool bool_0)
			: base("TickThread", Dispatcher.Dispatcher_1, false)
		{
			int_0 = int_1;
			action_0 = action_1;
			if (bool_0)
			{
				Start();
			}
		}

		protected override IEnumerator Do()
		{
			do
			{
				if (!manualResetEvent_0.InterWaitOne(0))
				{
					action_0();
					continue;
				}
				return null;
			}
			while (WaitHandle.WaitAny(new WaitHandle[2] { manualResetEvent_0, manualResetEvent_1 }, int_0) != 0);
			return null;
		}
	}
}
