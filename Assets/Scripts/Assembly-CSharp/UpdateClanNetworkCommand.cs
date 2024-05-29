using System.Collections.Generic;
using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class UpdateClanNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public UserClanData userClanData_0;

	[ProtoMember(3)]
	public List<UserClanMemberData> list_0;

	[ProtoMember(4)]
	public List<int> list_1;

	[ProtoMember(5)]
	public bool bool_0;

	private bool bool_1;

	private bool bool_2;

	private bool bool_3;

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
		if (UserClansData.UserClansData_0 != null)
		{
			UpdateClan();
			UpdateMambers();
			RemoveMembers();
			RemoveClan();
			CheckEvents();
		}
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("[UpdateClanNetworkCommand. Update clans data fail!] {0}", (abstractNetworkCommand_0 != null) ? string.Format("error code = {0}, message = {1}", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0) : string.Empty));
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(UpdateClanNetworkCommand), 306);
	}

	private void UpdateClan()
	{
		if (userClanData_0 == null)
		{
			return;
		}
		UserClanData userClanData = ClanController.ClanController_0.UserClanData_0;
		if (userClanData == null)
		{
			userClanData_0.list_0 = list_0;
			if (UserClansData.UserClansData_0.userClanStorage_0.GetObjectByKey(userClanData_0.string_0) == null)
			{
				UserClansData.UserClansData_0.userClanStorage_0.AddObject(userClanData_0.string_0, userClanData_0);
			}
			else
			{
				UserClansData.UserClansData_0.userClanStorage_0.UpdateObject(userClanData_0.string_0, userClanData_0);
			}
			UserClansData.UserClansData_0.string_0 = userClanData_0.string_0;
			bool_1 = true;
			bool_3 = true;
		}
		else
		{
			userClanData_0.list_0 = userClanData.list_0;
			UserClansData.UserClansData_0.userClanStorage_0.UpdateObject(userClanData_0.string_0, userClanData_0);
		}
		bool_2 = true;
	}

	private void UpdateMambers()
	{
		if (list_0 == null)
		{
			return;
		}
		UserClanData userClanData = ClanController.ClanController_0.UserClanData_0;
		if (userClanData == null)
		{
			return;
		}
		UserClanMemberData userClanMemberData_2;
		foreach (UserClanMemberData item in list_0)
		{
			userClanMemberData_2 = item;
			int num = userClanData.list_0.FindIndex((UserClanMemberData userClanMemberData_1) => userClanMemberData_1.int_0 == userClanMemberData_2.int_0);
			if (num == -1)
			{
				userClanData.list_0.Add(userClanMemberData_2);
			}
			else
			{
				userClanData.list_0[num] = userClanMemberData_2;
			}
		}
		bool_2 = true;
	}

	private void RemoveMembers()
	{
		if (list_1 == null)
		{
			return;
		}
		UserClanData userClanData = ClanController.ClanController_0.UserClanData_0;
		if (userClanData == null)
		{
			return;
		}
		int int_0;
		foreach (int item in list_1)
		{
			int_0 = item;
			int num = userClanData.list_0.FindIndex((UserClanMemberData userClanMemberData_0) => userClanMemberData_0.int_0 == int_0);
			if (num > -1)
			{
				userClanData.list_0.RemoveAt(num);
			}
		}
		bool_2 = true;
	}

	private void RemoveClan()
	{
		UserClanData userClanData = ClanController.ClanController_0.UserClanData_0;
		if (userClanData != null && bool_0)
		{
			UserClansData.UserClansData_0.userClanStorage_0.RemoveObject(userClanData.string_0);
			UserClansData.UserClansData_0.string_0 = null;
			bool_2 = true;
			bool_3 = true;
		}
	}

	private void CheckEvents()
	{
		if (bool_1)
		{
			ClanController.ClanController_0.OnCreatedClanSuccess(userClanData_0);
		}
		if (bool_2)
		{
			ClanController.ClanController_0.OnUpdateClan();
		}
		if (bool_3)
		{
			ClanController.ClanController_0.UpdateClanTop();
		}
	}
}
