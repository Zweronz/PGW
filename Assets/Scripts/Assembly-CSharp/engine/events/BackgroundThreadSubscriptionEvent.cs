using System;

namespace engine.events
{
	public sealed class BackgroundThreadSubscriptionEvent<ArgumentEvent> : SubscriptionEvent<ArgumentEvent>
	{
		public BackgroundThreadSubscriptionEvent(Action<ArgumentEvent> action_0, Func<ArgumentEvent, bool> func_0)
			: base(action_0, func_0)
		{
		}

		public override void InvokeAction(Action<ArgumentEvent> action_0, ArgumentEvent gparam_0)
		{
			UnityThreadHelper.CreateThread((Action)delegate
			{
				action_0(gparam_0);
			});
		}
	}
	public sealed class BackgroundThreadSubscriptionEvent : SubscriptionEvent
	{
		public BackgroundThreadSubscriptionEvent(Action action_1, Func<bool> func_1)
			: base(action_1, func_1)
		{
		}

		public override void InvokeAction(Action action_1)
		{
			UnityThreadHelper.CreateThread((Action)delegate
			{
				action_1();
			});
		}
	}
}
