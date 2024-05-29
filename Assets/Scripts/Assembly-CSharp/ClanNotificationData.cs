using ProtoBuf;

[ProtoContract]
public class ClanNotificationData : NotificationViewData
{
	[ProtoMember(1)]
	public UserClanData userClanData_0;
}
