using System;
using System.Runtime.CompilerServices;
using engine.data;
using engine.events;

public sealed class UserOverrideContentGroupStorage : UserStorage<int, UserOverrideContentGroupData>
{
	public const int int_0 = 0;

	public const int int_1 = 1;

	private static UserOverrideContentGroupStorage userOverrideContentGroupStorage_0;

	private static readonly BaseEvent<OverrideContentGroupEventData> baseEvent_0 = new BaseEvent<OverrideContentGroupEventData>();

	[CompilerGenerated]
	private static Func<UserOverrideContentGroupData, HashIndex<StockAligmentType, UserOverrideContentGroupData>, StockAligmentType> func_0;

	[CompilerGenerated]
	private static Func<UserOverrideContentGroupData, HashUniqueIndex<int, UserOverrideContentGroupData>, int> func_1;

	public static UserOverrideContentGroupStorage UserOverrideContentGroupStorage_0
	{
		get
		{
			return userOverrideContentGroupStorage_0 ?? (userOverrideContentGroupStorage_0 = new UserOverrideContentGroupStorage());
		}
	}

	public UserOverrideContentGroupStorage()
	{
		DependSceneEvent<ReloadGameEvent>.GlobalSubscribe(delegate
		{
			Clear();
		});
		AddIndex(new HashCallbackIndex<StockAligmentType, UserOverrideContentGroupData>(delegate(UserOverrideContentGroupData userOverrideContentGroupData_0, HashIndex<StockAligmentType, UserOverrideContentGroupData> hashIndex_0)
		{
			hashIndex_0.Boolean_0 = userOverrideContentGroupData_0.int_4 == 0;
			hashIndex_0.Prop_0 = StockAligmentType.NONE;
			ActionData objectByKey2 = StockDataStorage.Get.Storage.GetObjectByKey(userOverrideContentGroupData_0.int_4);
			return (!hashIndex_0.Boolean_0 && objectByKey2 != null) ? objectByKey2.stockAligmentType_0 : hashIndex_0.Prop_0;
		}));
		AddIndex(new HashUniqueCallbackIndex<int, UserOverrideContentGroupData>(delegate(UserOverrideContentGroupData userOverrideContentGroupData_0, HashUniqueIndex<int, UserOverrideContentGroupData> hashUniqueIndex_0)
		{
			hashUniqueIndex_0.Boolean_0 = userOverrideContentGroupData_0.int_4 == 0;
			hashUniqueIndex_0.Prop_0 = 0;
			ActionData objectByKey = StockDataStorage.Get.Storage.GetObjectByKey(userOverrideContentGroupData_0.int_4);
			return (!hashUniqueIndex_0.Boolean_0 && objectByKey != null) ? objectByKey.int_0 : hashUniqueIndex_0.Prop_0;
		}));
	}

	public static void Dispatch<OverrideContentGroupEventType>(OverrideContentGroupEventType gparam_0, OverrideContentGroupEventData overrideContentGroupEventData_0 = null)
	{
		baseEvent_0.Dispatch(overrideContentGroupEventData_0, gparam_0);
	}

	public static void Subscribe<OverrideContentGroupEventType>(OverrideContentGroupEventType gparam_0, Action<OverrideContentGroupEventData> action_0)
	{
		if (!baseEvent_0.Contains(action_0, gparam_0))
		{
			baseEvent_0.Subscribe(action_0, gparam_0);
		}
	}

	public static void Unsubscribe<OverrideContentGroupEventType>(OverrideContentGroupEventType gparam_0, Action<OverrideContentGroupEventData> action_0)
	{
		if (baseEvent_0.Contains(action_0, gparam_0))
		{
			baseEvent_0.Unsubscribe(action_0, gparam_0);
		}
	}
}
