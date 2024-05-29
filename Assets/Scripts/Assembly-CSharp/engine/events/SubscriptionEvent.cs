using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace engine.events
{
	public class SubscriptionEvent<ArgumentEvent> : ISubscriptionEvent
	{
		public Action<ArgumentEvent> Action_0 { get; private set; }

		public Func<ArgumentEvent, bool> Func_0 { get; private set; }

		public SubscriptionUID SubscriptionUID_0 { get; set; }

		public SubscriptionEvent(Action<ArgumentEvent> action_0, Func<ArgumentEvent, bool> func_0)
		{
			if (action_0 != null && func_0 != null)
			{
				Action_0 = action_0;
				Func_0 = func_0;
			}
			else
			{
				Debug.LogError("Create SubscriptionEvent error! Constructor arguments equals null");
			}
		}

		public virtual Action<object[]> GetAction()
		{
			if (Action_0 != null && Func_0 != null)
			{
				return delegate(object[] object_0)
				{
					ArgumentEvent val = default(ArgumentEvent);
					if (object_0 != null && object_0.Length > 0 && object_0[0] != null)
					{
						val = (ArgumentEvent)object_0[0];
					}
					if (Func_0(val))
					{
						InvokeAction(Action_0, val);
					}
				};
			}
			return null;
		}

		public virtual void InvokeAction(Action<ArgumentEvent> action_0, ArgumentEvent gparam_0)
		{
			if (action_0 == null)
			{
				Debug.LogError("Invoke action error! Action equals null");
			}
			else
			{
				action_0(gparam_0);
			}
		}
	}
	public class SubscriptionEvent : ISubscriptionEvent
	{
		[CompilerGenerated]
		private Action action_0;

		[CompilerGenerated]
		private Func<bool> func_0;

		[CompilerGenerated]
		private SubscriptionUID subscriptionUID_0;

		public Action Action_0
		{
			[CompilerGenerated]
			get
			{
				return action_0;
			}
			[CompilerGenerated]
			private set
			{
				action_0 = value;
			}
		}

		public Func<bool> Func_0
		{
			[CompilerGenerated]
			get
			{
				return func_0;
			}
			[CompilerGenerated]
			private set
			{
				func_0 = value;
			}
		}

		public SubscriptionUID SubscriptionUID_0
		{
			[CompilerGenerated]
			get
			{
				return subscriptionUID_0;
			}
			[CompilerGenerated]
			set
			{
				subscriptionUID_0 = value;
			}
		}

		public SubscriptionEvent(Action action_1, Func<bool> func_1)
		{
			if (action_1 != null && func_1 != null)
			{
				Action_0 = action_1;
				Func_0 = func_1;
			}
			else
			{
				Debug.LogError("Create SubscriptionEvent error! Constructor arguments equals null");
			}
		}

		public virtual Action<object[]> GetAction()
		{
			if (Action_0 != null && Func_0 != null)
			{
				return delegate
				{
					if (Func_0())
					{
						InvokeAction(Action_0);
					}
				};
			}
			return null;
		}

		public virtual void InvokeAction(Action action_1)
		{
			if (action_1 == null)
			{
				Debug.LogError("Invoke action error! Action equals null");
			}
			else
			{
				action_1();
			}
		}
	}
}
