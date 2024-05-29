using System;
using engine.helpers;
using engine.unity;

namespace engine.events
{
	public class DependSceneEvent<EventType, ArgumentType> : BaseEvent<ArgumentType> where EventType : BaseEvent<ArgumentType>, new()
	{
		protected static DependSceneSubscriptions _cache;

		protected static EventType _event;

		protected static EventType Prop_0
		{
			get
			{
				if (_event == null)
				{
					_event = EventManager.EventManager_0.GetEvent<EventType>();
				}
				return _event;
			}
		}

		public static void GlobalSubscribe(Action<ArgumentType> action_0, bool bool_0 = false)
		{
			EventType prop_ = Prop_0;
			SubscriptionUID subscriptionUID_ = prop_.Subscribe(action_0);
			if (bool_0)
			{
				if (_cache == null)
				{
					_cache = new DependSceneSubscriptions();
				}
				_cache.AddSubscriptionUID(subscriptionUID_);
			}
		}

		public static void GlobalUnsubscribe(Action<ArgumentType> action_0)
		{
			EventType prop_ = Prop_0;
			prop_.Unsubscribe(action_0);
		}

		public static void GlobalDispatch(ArgumentType gparam_0)
		{
			try
			{
				EventType prop_ = Prop_0;
				prop_.Dispatch(gparam_0);
			}
			catch (Exception exception_)
			{
				MonoSingleton<Log>.Prop_0.DumpError(exception_);
			}
		}

		public new static bool Contains(Action<ArgumentType> action_0)
		{
			EventType prop_ = Prop_0;
			return prop_.Contains(action_0);
		}
	}
	public class DependSceneEvent<EventType> : BaseEvent where EventType : BaseEvent, new()
	{
		protected static DependSceneSubscriptions _cache;

		protected static EventType _event;

		protected static EventType Prop_0
		{
			get
			{
				if (_event == null)
				{
					_event = EventManager.EventManager_0.GetEvent<EventType>();
				}
				return _event;
			}
		}

		public static void GlobalSubscribe(Action action_0, bool bool_0 = false)
		{
			EventType prop_ = Prop_0;
			SubscriptionUID subscriptionUID_ = prop_.Subscribe(action_0);
			if (bool_0)
			{
				if (_cache == null)
				{
					_cache = new DependSceneSubscriptions();
				}
				_cache.AddSubscriptionUID(subscriptionUID_);
			}
		}

		public static void GlobalUnsubscribe(Action action_0)
		{
			EventType prop_ = Prop_0;
			prop_.Unsubscribe(action_0);
		}

		public static void GlobalDispatch()
		{
			try
			{
				EventType prop_ = Prop_0;
				prop_.Dispatch();
			}
			catch (Exception exception_)
			{
				MonoSingleton<Log>.Prop_0.DumpError(exception_);
			}
		}

		public new static bool Contains(Action action_0)
		{
			EventType prop_ = Prop_0;
			return prop_.Contains(action_0);
		}
	}
}
