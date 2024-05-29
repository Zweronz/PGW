using System.Collections.Generic;
using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class GetContentOverrideNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public Dictionary<int, UserOverrideContentGroupData> dictionary_0;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void Run()
	{
		base.Run();
		Update();
		Remove();
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("[GetContentOverrideNetworkCommand::HandleError. Get override contend group data fail!] {0}", (abstractNetworkCommand_0 != null) ? string.Format("error code = {0}, message = {1}", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0) : string.Empty));
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(GetContentOverrideNetworkCommand), 129);
	}

	private void Update()
	{
		bool flag = false;
		if (dictionary_0 != null && dictionary_0.Count != 0)
		{
			UserOverrideContentGroupData userOverrideContentGroupData = null;
			UserOverrideContentGroupStorage userOverrideContentGroupStorage_ = UserOverrideContentGroupStorage.UserOverrideContentGroupStorage_0;
			foreach (KeyValuePair<int, UserOverrideContentGroupData> item in dictionary_0)
			{
				userOverrideContentGroupData = userOverrideContentGroupStorage_.GetObjectByKey(item.Key);
				OverrideContentGroupChangeType overrideContentGroupChangeType = OverrideContentGroupChangeType.All;
				if (userOverrideContentGroupData != null)
				{
					overrideContentGroupChangeType = userOverrideContentGroupData.IsChange(item.Value);
					if (overrideContentGroupChangeType == OverrideContentGroupChangeType.Error || overrideContentGroupChangeType == OverrideContentGroupChangeType.No)
					{
						continue;
					}
				}
				flag = true;
				userOverrideContentGroupStorage_.UpdateObject(item.Key, item.Value);
				UserOverrideContentGroupStorage.Dispatch(OverrideContentGroupEventType.UPDATE, new OverrideContentGroupEventData
				{
					userOverrideContentGroupData_0 = item.Value,
					overrideContentGroupChangeType_0 = overrideContentGroupChangeType
				});
			}
		}
		if (flag)
		{
			UserOverrideContentGroupStorage.Dispatch(OverrideContentGroupEventType.UPDATE_ALL, new OverrideContentGroupEventData());
		}
	}

	private void Remove()
	{
		bool flag = false;
		UserOverrideContentGroupStorage userOverrideContentGroupStorage_ = UserOverrideContentGroupStorage.UserOverrideContentGroupStorage_0;
		UserOverrideContentGroupData value = null;
		foreach (UserOverrideContentGroupData @object in userOverrideContentGroupStorage_.GetObjects())
		{
			if (dictionary_0 == null || !dictionary_0.TryGetValue(@object.int_0, out value))
			{
				flag = true;
				userOverrideContentGroupStorage_.RemoveObject(@object.int_0);
				UserOverrideContentGroupStorage.Dispatch(OverrideContentGroupEventType.REMOVE, new OverrideContentGroupEventData
				{
					userOverrideContentGroupData_0 = @object
				});
			}
		}
		if (flag)
		{
			UserOverrideContentGroupStorage.Dispatch(OverrideContentGroupEventType.REMOVE_ALL, new OverrideContentGroupEventData());
		}
	}
}
