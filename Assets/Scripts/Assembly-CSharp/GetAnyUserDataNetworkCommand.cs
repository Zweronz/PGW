using ProtoBuf;
using UnityEngine;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class GetAnyUserDataNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public int int_1;

	[ProtoMember(3)]
	public UserData userData_0;

	public override NetworkCommandInfo NetworkCommandInfo_0
	{
		get
		{
			return networkCommandInfo_0;
		}
	}

	public override void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		GetAnyUserDataNetworkCommand getAnyUserDataNetworkCommand = abstractNetworkCommand_0 as GetAnyUserDataNetworkCommand;
		if (getAnyUserDataNetworkCommand == null)
		{
			Log.AddLine("[GetAnyUserDataNetworkCommand::Answered] ERROR cmd==null", Log.LogLevel.ERROR);
			UsersData.Dispatch(UsersData.EventType.GET_ANY_USER_CMD_ERROR);
			return;
		}
		UserData userData = getAnyUserDataNetworkCommand.userData_0;
		if (userData != null && userData.user_0 != null)
		{
			UsersStorage usersStorage_ = UsersData.UsersData_0.usersStorage_0;
			if (usersStorage_.GetObjectByKey(userData.user_0.int_0) == null)
			{
				usersStorage_.AddObject(userData.user_0.int_0, userData);
			}
			else
			{
				usersStorage_.UpdateObject(userData.user_0.int_0, userData);
			}
			userData.InitStorages();
			LocalSkinTextureData.UpdateOtherUserSkins(userData.dictionary_4, userData.user_0.int_0);
			userData.user_0.float_0 = Time.fixedTime;
			UsersData.Dispatch(UsersData.EventType.GET_ANY_USER_CMD_COMPLETE, new UsersData.EventData(getAnyUserDataNetworkCommand.userData_0.user_0.int_0));
		}
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		string empty;
		if (abstractNetworkCommand_0 != null)
		{
			string.Format("error code = {0}, message = {1}", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0);
		}
		else
			empty = string.Empty;
		Log.AddLine(string.Format("[GetAnyUserDataNetworkCommand::HandleError] ERROR {0}", abstractNetworkCommand_0), Log.LogLevel.ERROR);
		UsersData.Dispatch(UsersData.EventType.GET_ANY_USER_CMD_ERROR);
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(GetAnyUserDataNetworkCommand), 126);
	}
}
