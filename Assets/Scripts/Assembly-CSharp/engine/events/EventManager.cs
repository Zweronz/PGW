using System;
using System.Collections.Generic;

namespace engine.events
{
	public class EventManager
	{
		private static EventManager eventManager_0;

		private readonly Dictionary<Type, AbstractEvent> dictionary_0 = new Dictionary<Type, AbstractEvent>();

		public static EventManager EventManager_0
		{
			get
			{
				if (eventManager_0 == null)
				{
					eventManager_0 = new EventManager();
				}
				return eventManager_0;
			}
		}

		private EventManager()
		{
		}

		public T GetEvent<T>() where T : AbstractEvent, new()
		{
			lock (dictionary_0)
			{
				Type typeFromHandle = typeof(T);
				AbstractEvent value = null;
				if (!dictionary_0.TryGetValue(typeFromHandle, out value))
				{
					AbstractEvent abstractEvent = new T();
					dictionary_0[typeFromHandle] = abstractEvent;
					return abstractEvent as T;
				}
				return value as T;
			}
		}
	}
}
