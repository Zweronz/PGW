using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace engine.events
{
	public abstract class AbstractEvent
	{
		public enum ThreadOption
		{
			Self = 0,
			UI = 1,
			Background = 2
		}

		private readonly List<ISubscriptionEvent> list_0 = new List<ISubscriptionEvent>();

		private readonly Dictionary<object, List<ISubscriptionEvent>> dictionary_0 = new Dictionary<object, List<ISubscriptionEvent>>();

		public List<ISubscriptionEvent> List_0
		{
			get
			{
				return list_0;
			}
		}

		public Dictionary<object, List<ISubscriptionEvent>> Dictionary_0
		{
			get
			{
				return dictionary_0;
			}
		}

		protected virtual SubscriptionUID Subscribe(ISubscriptionEvent isubscriptionEvent_0)
		{
			isubscriptionEvent_0.SubscriptionUID_0 = new SubscriptionUID(Unsubscribe);
			lock (list_0)
			{
				list_0.Add(isubscriptionEvent_0);
			}
			return isubscriptionEvent_0.SubscriptionUID_0;
		}

		protected virtual SubscriptionUID Subscribe<TypeEvent>(ISubscriptionEvent isubscriptionEvent_0, TypeEvent gparam_0)
		{
			if (isubscriptionEvent_0 != null && gparam_0 != null)
			{
				isubscriptionEvent_0.SubscriptionUID_0 = new SubscriptionUID(Unsubscribe);
				lock (dictionary_0)
				{
					if (!dictionary_0.ContainsKey(gparam_0))
					{
						dictionary_0.Add(gparam_0, new List<ISubscriptionEvent>());
					}
					dictionary_0[gparam_0].Add(isubscriptionEvent_0);
				}
				return isubscriptionEvent_0.SubscriptionUID_0;
			}
			Debug.LogError("subscribe error! subscription or typeEvent = null");
			return null;
		}

		protected virtual void Dispatch(params object[] object_0)
		{
			List<Action<object[]>> actions = GetActions();
			foreach (Action<object[]> item in actions)
			{
				item(object_0);
			}
		}

		protected virtual void DispatchOfType<EventTypes>(EventTypes gparam_0, params object[] object_0)
		{
			List<Action<object[]>> actions = GetActions(gparam_0);
			foreach (Action<object[]> item in actions)
			{
				item(object_0);
			}
		}

		public virtual void Unsubscribe(SubscriptionUID subscriptionUID_0)
		{
			lock (list_0)
			{
				ISubscriptionEvent subscriptionEvent = list_0.FirstOrDefault((ISubscriptionEvent isubscriptionEvent_0) => isubscriptionEvent_0.SubscriptionUID_0 == subscriptionUID_0);
				if (subscriptionEvent != null)
				{
					list_0.Remove(subscriptionEvent);
				}
			}
			lock (dictionary_0)
			{
				foreach (object key in dictionary_0.Keys)
				{
					List<ISubscriptionEvent> list = dictionary_0[key];
					ISubscriptionEvent subscriptionEvent2 = list.FirstOrDefault((ISubscriptionEvent isubscriptionEvent_0) => isubscriptionEvent_0.SubscriptionUID_0 == subscriptionUID_0);
					if (subscriptionEvent2 != null)
					{
						list.Remove(subscriptionEvent2);
						if (list.Count == 0)
						{
							list.Clear();
							dictionary_0.Remove(key);
							break;
						}
					}
				}
			}
		}

		public virtual void UnsubscribeAll()
		{
			lock (list_0)
			{
				if (list_0.Count != 0)
				{
					ISubscriptionEvent[] array = new ISubscriptionEvent[list_0.Count];
					list_0.CopyTo(array);
					for (int i = 0; i < array.Count(); i++)
					{
						array[i].SubscriptionUID_0.Dispose();
					}
					list_0.Clear();
				}
			}
			lock (dictionary_0)
			{
				Dictionary<object, List<ISubscriptionEvent>> dictionary = new Dictionary<object, List<ISubscriptionEvent>>(dictionary_0);
				IDictionaryEnumerator dictionaryEnumerator = dictionary.GetEnumerator();
				while (dictionaryEnumerator.MoveNext())
				{
					List<ISubscriptionEvent> list = dictionaryEnumerator.Value as List<ISubscriptionEvent>;
					for (int j = 0; j < list.Count; j++)
					{
						list[j].SubscriptionUID_0.Dispose();
					}
					list.Clear();
				}
				dictionary_0.Clear();
			}
		}

		public virtual bool Contains(SubscriptionUID subscriptionUID_0)
		{
			lock (list_0)
			{
				ISubscriptionEvent subscriptionEvent = list_0.FirstOrDefault((ISubscriptionEvent isubscriptionEvent_0) => isubscriptionEvent_0.SubscriptionUID_0 == subscriptionUID_0);
				return subscriptionEvent != null;
			}
		}

		public virtual bool Contains(SubscriptionUID subscriptionUID_0, object object_0)
		{
			lock (dictionary_0)
			{
				if (!dictionary_0.ContainsKey(object_0))
				{
					return false;
				}
				ISubscriptionEvent subscriptionEvent = dictionary_0[object_0].FirstOrDefault((ISubscriptionEvent isubscriptionEvent_0) => isubscriptionEvent_0.SubscriptionUID_0 == subscriptionUID_0);
				return subscriptionEvent != null;
			}
		}

		private List<Action<object[]>> GetActions()
		{
			List<Action<object[]>> list = new List<Action<object[]>>();
			lock (list_0)
			{
				for (int num = list_0.Count - 1; num >= 0; num--)
				{
					Action<object[]> action = list_0[num].GetAction();
					if (action == null)
					{
						list_0.RemoveAt(num);
					}
					else
					{
						list.Add(action);
					}
				}
				return list;
			}
		}

		private List<Action<object[]>> GetActions<EventType>(EventType gparam_0)
		{
			List<Action<object[]>> actions = GetActions();
			lock (dictionary_0)
			{
				List<ISubscriptionEvent> value = null;
				if (dictionary_0.TryGetValue(gparam_0, out value))
				{
					for (int num = value.Count - 1; num >= 0; num--)
					{
						Action<object[]> action = value[num].GetAction();
						if (action == null)
						{
							value.RemoveAt(num);
						}
						else
						{
							actions.Add(action);
						}
					}
				}
			}
			return actions;
		}
	}
}
