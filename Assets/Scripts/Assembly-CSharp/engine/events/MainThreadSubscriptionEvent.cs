using System;

namespace engine.events
{
	public sealed class MainThreadSubscriptionEvent<ArgumentEvent> : SubscriptionEvent<ArgumentEvent>
	{
		public MainThreadSubscriptionEvent(Action<ArgumentEvent> action_0, Func<ArgumentEvent, bool> func_0)
			: base(action_0, func_0)
		{
		}

		public override void InvokeAction(Action<ArgumentEvent> action_0, ArgumentEvent gparam_0)
		{
			UnityThreadHelper.Dispatcher_0.Dispatch(delegate
			{
				action_0(gparam_0);
			});
		}
	}
	public sealed class MainThreadSubscriptionEvent : SubscriptionEvent
	{
		public MainThreadSubscriptionEvent(Action action_1, Func<bool> func_1)
			: base(action_1, func_1)
		{
		}

		public override void InvokeAction(Action action_1)
		{
			UnityThreadHelper.Dispatcher_0.Dispatch(delegate
			{
				action_1();
			});
		}
	}
}
