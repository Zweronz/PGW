using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace engine.events
{
	public class BaseEvent<ArgumentEvent> : AbstractEvent
	{
		public SubscriptionUID Subscribe(Action<ArgumentEvent> action_0)
		{
			return Subscribe(action_0, ThreadOption.Self);
		}

		public SubscriptionUID Subscribe(Action<ArgumentEvent> action_0, ThreadOption threadOption_0)
		{
			return Subscribe(action_0, threadOption_0, null);
		}

		public SubscriptionUID Subscribe(Action<ArgumentEvent> action_0, Func<ArgumentEvent, bool> func_0)
		{
			return Subscribe(action_0, ThreadOption.Self, func_0);
		}

		public SubscriptionUID Subscribe(Action<ArgumentEvent> action_0, ThreadOption threadOption_0, Func<ArgumentEvent, bool> func_0)
		{
			func_0 = ((func_0 == null) ? ((Func<ArgumentEvent, bool>)((ArgumentEvent gparam_0) => true)) : func_0);
			SubscriptionEvent<ArgumentEvent> subscription = GetSubscription(action_0, func_0, threadOption_0);
			return base.Subscribe(subscription);
		}

		public SubscriptionUID Subscribe<TypeEvent>(Action<ArgumentEvent> action_0, TypeEvent gparam_0)
		{
			SubscriptionEvent<ArgumentEvent> subscription = GetSubscription(action_0, (ArgumentEvent gparam_1) => true, ThreadOption.Self);
			return base.Subscribe(subscription, gparam_0);
		}

		public virtual void Dispatch(ArgumentEvent gparam_0)
		{
			base.Dispatch(gparam_0);
		}

		public virtual void Dispatch<TypeEvent>(ArgumentEvent gparam_0, TypeEvent gparam_1)
		{
			base.DispatchOfType(gparam_1, gparam_0);
		}

		public virtual void Unsubscribe(Action<ArgumentEvent> action_0)
		{
			lock (base.List_0)
			{
				SubscriptionEvent<ArgumentEvent> subscriptionEvent = base.List_0.Cast<SubscriptionEvent<ArgumentEvent>>().FirstOrDefault((SubscriptionEvent<ArgumentEvent> subscriptionEvent_0) => subscriptionEvent_0.Action_0 == action_0);
				if (subscriptionEvent != null)
				{
					base.List_0.Remove(subscriptionEvent);
				}
			}
			Dictionary<object, List<ISubscriptionEvent>> dictionary = base.Dictionary_0;
			if (dictionary == null)
			{
				return;
			}
			lock (dictionary)
			{
				List<object> list = new List<object>();
				foreach (object key in dictionary.Keys)
				{
					List<ISubscriptionEvent> list2 = dictionary[key];
					SubscriptionEvent<ArgumentEvent> subscriptionEvent2 = list2.Cast<SubscriptionEvent<ArgumentEvent>>().FirstOrDefault((SubscriptionEvent<ArgumentEvent> subscriptionEvent_0) => subscriptionEvent_0.Action_0 == action_0);
					if (subscriptionEvent2 != null)
					{
						list2.Remove(subscriptionEvent2);
						if (list2.Count == 0)
						{
							list2.Clear();
							list.Add(key);
						}
					}
				}
				foreach (object item in list)
				{
					dictionary.Remove(item);
				}
			}
		}

		public virtual void Unsubscribe<TypeEvent>(Action<ArgumentEvent> action_0, TypeEvent gparam_0)
		{
			Dictionary<object, List<ISubscriptionEvent>> dictionary = base.Dictionary_0;
			if (dictionary == null)
			{
				return;
			}
			lock (dictionary)
			{
				if (!dictionary.ContainsKey(gparam_0))
				{
					return;
				}
				List<ISubscriptionEvent> list = dictionary[gparam_0];
				SubscriptionEvent<ArgumentEvent> subscriptionEvent = list.Cast<SubscriptionEvent<ArgumentEvent>>().FirstOrDefault((SubscriptionEvent<ArgumentEvent> subscriptionEvent_0) => subscriptionEvent_0.Action_0 == action_0);
				if (subscriptionEvent != null)
				{
					list.Remove(subscriptionEvent);
					if (list.Count == 0)
					{
						list.Clear();
						dictionary.Remove(gparam_0);
					}
				}
			}
		}

		public virtual bool Contains(Action<ArgumentEvent> action_0)
		{
			ISubscriptionEvent subscriptionEvent;
			lock (base.List_0)
			{
				subscriptionEvent = base.List_0.Cast<SubscriptionEvent<ArgumentEvent>>().FirstOrDefault((SubscriptionEvent<ArgumentEvent> subscriptionEvent_0) => subscriptionEvent_0.Action_0 == action_0);
			}
			if (subscriptionEvent == null)
			{
				Dictionary<object, List<ISubscriptionEvent>> dictionary = base.Dictionary_0;
				if (dictionary != null)
				{
					lock (dictionary)
					{
						foreach (object key in dictionary.Keys)
						{
							List<ISubscriptionEvent> source = dictionary[key];
							subscriptionEvent = source.Cast<SubscriptionEvent<ArgumentEvent>>().FirstOrDefault((SubscriptionEvent<ArgumentEvent> subscriptionEvent_0) => subscriptionEvent_0.Action_0 == action_0);
							if (subscriptionEvent != null)
							{
								break;
							}
						}
					}
				}
			}
			return subscriptionEvent != null;
		}

		public virtual bool Contains<TypeEvent>(Action<ArgumentEvent> action_0, TypeEvent gparam_0)
		{
			ISubscriptionEvent subscriptionEvent;
			lock (base.List_0)
			{
				subscriptionEvent = base.List_0.Cast<SubscriptionEvent<ArgumentEvent>>().FirstOrDefault((SubscriptionEvent<ArgumentEvent> subscriptionEvent_0) => subscriptionEvent_0.Action_0 == action_0);
			}
			if (subscriptionEvent == null)
			{
				Dictionary<object, List<ISubscriptionEvent>> dictionary = base.Dictionary_0;
				if (dictionary != null)
				{
					lock (dictionary)
					{
						List<ISubscriptionEvent> value;
						if (dictionary.TryGetValue(gparam_0, out value))
						{
							subscriptionEvent = value.Cast<SubscriptionEvent<ArgumentEvent>>().FirstOrDefault((SubscriptionEvent<ArgumentEvent> subscriptionEvent_0) => subscriptionEvent_0.Action_0 == action_0);
						}
					}
				}
			}
			return subscriptionEvent != null;
		}

		private SubscriptionEvent<ArgumentEvent> GetSubscription(Action<ArgumentEvent> action_0, Func<ArgumentEvent, bool> func_0, ThreadOption threadOption_0)
		{
			SubscriptionEvent<ArgumentEvent> subscriptionEvent = null;
			switch (threadOption_0)
			{
			default:
				return new SubscriptionEvent<ArgumentEvent>(action_0, func_0);
			case ThreadOption.Self:
				return new SubscriptionEvent<ArgumentEvent>(action_0, func_0);
			case ThreadOption.UI:
				return new MainThreadSubscriptionEvent<ArgumentEvent>(action_0, func_0);
			case ThreadOption.Background:
				return new BackgroundThreadSubscriptionEvent<ArgumentEvent>(action_0, func_0);
			}
		}
	}
	public class BaseEvent : AbstractEvent
	{
		[CompilerGenerated]
		private static Func<bool> func_0;

		public SubscriptionUID Subscribe(Action action_0)
		{
			return Subscribe(action_0, ThreadOption.Self);
		}

		public SubscriptionUID Subscribe(Action action_0, ThreadOption threadOption_0)
		{
			return Subscribe(action_0, threadOption_0, null);
		}

		public SubscriptionUID Subscribe(Action action_0, Func<bool> func_1)
		{
			return Subscribe(action_0, ThreadOption.Self, func_1);
		}

		public SubscriptionUID Subscribe(Action action_0, ThreadOption threadOption_0, Func<bool> func_1)
		{
			func_1 = ((func_1 == null) ? ((Func<bool>)(() => true)) : func_1);
			SubscriptionEvent subscription = GetSubscription(action_0, func_1, threadOption_0);
			return base.Subscribe(subscription);
		}

		public SubscriptionUID Subscribe<TypeEvent>(Action action_0, TypeEvent gparam_0)
		{
			SubscriptionEvent subscription = GetSubscription(action_0, () => true, ThreadOption.Self);
			return base.Subscribe(subscription, gparam_0);
		}

		public virtual void Dispatch()
		{
			base.Dispatch();
		}

		public virtual void Dispatch<TypeEvent>(TypeEvent gparam_0)
		{
			base.DispatchOfType(gparam_0);
		}

		public virtual void Unsubscribe(Action action_0)
		{
			lock (base.List_0)
			{
				SubscriptionEvent subscriptionEvent = base.List_0.Cast<SubscriptionEvent>().FirstOrDefault((SubscriptionEvent subscriptionEvent_0) => subscriptionEvent_0.Action_0 == action_0);
				if (subscriptionEvent != null)
				{
					base.List_0.Remove(subscriptionEvent);
				}
			}
			Dictionary<object, List<ISubscriptionEvent>> dictionary = base.Dictionary_0;
			if (dictionary == null)
			{
				return;
			}
			lock (dictionary)
			{
				List<object> list = new List<object>();
				foreach (object key in dictionary.Keys)
				{
					List<ISubscriptionEvent> list2 = dictionary[key];
					SubscriptionEvent subscriptionEvent2 = list2.Cast<SubscriptionEvent>().FirstOrDefault((SubscriptionEvent subscriptionEvent_0) => subscriptionEvent_0.Action_0 == action_0);
					if (subscriptionEvent2 != null)
					{
						list2.Remove(subscriptionEvent2);
						if (list2.Count == 0)
						{
							list2.Clear();
							list.Add(key);
						}
					}
				}
				foreach (object item in list)
				{
					dictionary.Remove(item);
				}
			}
		}

		public virtual void Unsubscribe<TypeEvent>(Action action_0, TypeEvent gparam_0)
		{
			Dictionary<object, List<ISubscriptionEvent>> dictionary = base.Dictionary_0;
			if (dictionary == null)
			{
				return;
			}
			lock (dictionary)
			{
				List<ISubscriptionEvent> list = dictionary[gparam_0];
				SubscriptionEvent subscriptionEvent = list.Cast<SubscriptionEvent>().FirstOrDefault((SubscriptionEvent subscriptionEvent_0) => subscriptionEvent_0.Action_0 == action_0);
				if (subscriptionEvent != null)
				{
					list.Remove(subscriptionEvent);
					if (list.Count == 0)
					{
						list.Clear();
						dictionary.Remove(gparam_0);
					}
				}
			}
		}

		public virtual bool Contains(Action action_0)
		{
			ISubscriptionEvent subscriptionEvent;
			lock (base.List_0)
			{
				subscriptionEvent = base.List_0.Cast<SubscriptionEvent>().FirstOrDefault((SubscriptionEvent subscriptionEvent_0) => subscriptionEvent_0.Action_0 == action_0);
			}
			if (subscriptionEvent == null)
			{
				Dictionary<object, List<ISubscriptionEvent>> dictionary = base.Dictionary_0;
				if (dictionary != null)
				{
					lock (dictionary)
					{
						foreach (object key in dictionary.Keys)
						{
							List<ISubscriptionEvent> source = dictionary[key];
							subscriptionEvent = source.Cast<SubscriptionEvent>().FirstOrDefault((SubscriptionEvent subscriptionEvent_0) => subscriptionEvent_0.Action_0 == action_0);
							if (subscriptionEvent != null)
							{
								break;
							}
						}
					}
				}
			}
			return subscriptionEvent != null;
		}

		public virtual bool Contains<TypeEvent>(Action action_0, TypeEvent gparam_0)
		{
			ISubscriptionEvent subscriptionEvent;
			lock (base.List_0)
			{
				subscriptionEvent = base.List_0.Cast<SubscriptionEvent>().FirstOrDefault((SubscriptionEvent subscriptionEvent_0) => subscriptionEvent_0.Action_0 == action_0);
			}
			if (subscriptionEvent == null)
			{
				Dictionary<object, List<ISubscriptionEvent>> dictionary = base.Dictionary_0;
				if (dictionary != null)
				{
					lock (dictionary)
					{
						List<ISubscriptionEvent> value;
						if (dictionary.TryGetValue(gparam_0, out value))
						{
							subscriptionEvent = value.Cast<SubscriptionEvent>().FirstOrDefault((SubscriptionEvent subscriptionEvent_0) => subscriptionEvent_0.Action_0 == action_0);
						}
					}
				}
			}
			return subscriptionEvent != null;
		}

		private SubscriptionEvent GetSubscription(Action action_0, Func<bool> func_1, ThreadOption threadOption_0)
		{
			SubscriptionEvent subscriptionEvent = null;
			switch (threadOption_0)
			{
			default:
				return new SubscriptionEvent(action_0, func_1);
			case ThreadOption.Self:
				return new SubscriptionEvent(action_0, func_1);
			case ThreadOption.UI:
				return new MainThreadSubscriptionEvent(action_0, func_1);
			case ThreadOption.Background:
				return new BackgroundThreadSubscriptionEvent(action_0, func_1);
			}
		}
	}
}
