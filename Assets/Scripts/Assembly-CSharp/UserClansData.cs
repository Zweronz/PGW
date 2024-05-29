using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.events;
using engine.helpers;
using engine.unity;

[ProtoContract]
public sealed class UserClansData
{
	public sealed class EventData
	{
		public int int_0;

		public string string_0;
	}

	public enum EventType
	{
		INIT_COMPLETE = 0,
		INIT_ERROR = 1
	}

	[ProtoMember(1)]
	public string string_0;

	[ProtoMember(2)]
	public Dictionary<string, UserClanData> dictionary_0;

	public UserClanStorage userClanStorage_0 = new UserClanStorage();

	private static readonly BaseEvent<EventData> baseEvent_0 = new BaseEvent<EventData>();

	[CompilerGenerated]
	private static UserClansData userClansData_0;

	public static UserClansData UserClansData_0
	{
		[CompilerGenerated]
		get
		{
			return userClansData_0;
		}
		[CompilerGenerated]
		private set
		{
			userClansData_0 = value;
		}
	}

	public UserClanData UserClanData_0
	{
		get
		{
			return (!string.IsNullOrEmpty(string_0)) ? userClanStorage_0.GetObjectByKey(string_0) : null;
		}
	}

	public static void Dispatch<EventType>(EventType gparam_0, EventData eventData_0 = null)
	{
		baseEvent_0.Dispatch(eventData_0, gparam_0);
	}

	public static void Subscribe(EventType eventType_0, Action<EventData> action_0)
	{
		if (!baseEvent_0.Contains(action_0, eventType_0))
		{
			baseEvent_0.Subscribe(action_0, eventType_0);
		}
	}

	public static void Unsubscribe(EventType eventType_0, Action<EventData> action_0)
	{
		baseEvent_0.Unsubscribe(action_0, eventType_0);
	}

	public void Init()
	{
		UserClansData_0 = this;
		try
		{
			userClanStorage_0.InitUserStorage(dictionary_0);
		}
		catch (Exception ex)
		{
			MonoSingleton<Log>.Prop_0.DumpError(ex, true);
			string string_ = string.Format("Received server command. Error: {0}", ex.Message);
			Log.AddLine(string_, Log.LogLevel.WARNING);
			baseEvent_0.Dispatch(null, EventType.INIT_ERROR);
		}
		baseEvent_0.Dispatch(null, EventType.INIT_COMPLETE);
	}
}
