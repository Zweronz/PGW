using System;
using System.Collections;

namespace UnityThreading
{
	public sealed class ActionThread : ThreadBase
	{
		private Action<ActionThread> action_0;

		public ActionThread(Action<ActionThread> action_1)
			: this(action_1, true)
		{
		}

		public ActionThread(Action<ActionThread> action_1, bool bool_0)
			: base("ActionThread", Dispatcher.Dispatcher_0, false)
		{
			action_0 = action_1;
			if (bool_0)
			{
				Start();
			}
		}

		protected override IEnumerator Do()
		{
			action_0(this);
			return null;
		}
	}
}
