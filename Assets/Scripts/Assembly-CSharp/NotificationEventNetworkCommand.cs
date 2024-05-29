using System;
using System.IO;
using ProtoBuf;
using engine.helpers;
using engine.network;

[ProtoContract]
public sealed class NotificationEventNetworkCommand : AbstractNetworkCommand
{
	[ProtoMember(1)]
	public NetworkCommandInfo networkCommandInfo_0 = new NetworkCommandInfo();

	[ProtoMember(2)]
	public NotificationType notificationType_0;

	[ProtoMember(3)]
	public string string_0;

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
		NotificationViewData notificationViewData = null;
		switch (notificationType_0)
		{
		default:
			notificationViewData = null;
			break;
		case NotificationType.NOTIFICATION_4LEVEL:
			notificationViewData = new NotificationViewData();
			break;
		case NotificationType.NOTIFICATION_CLAN_INVITE:
		case NotificationType.NOTIFICATION_CLAN_ENTER:
		case NotificationType.NOTIFICATION_CLAN_CLEANED:
			notificationViewData = FromBase64<ClanNotificationData>(string_0);
			break;
		case NotificationType.NOTIFICATION_BANK_BUY:
			notificationViewData = FromBase64<PaymentNotificationData>(string_0);
			break;
		case NotificationType.NOTIFICATION_MAINTANANCE:
			notificationViewData = FromBase64<MaintananceNotificationData>(string_0);
			break;
		case NotificationType.NOTIFICATION_WARNING:
			notificationViewData = FromBase64<WarningNotificationData>(string_0);
			break;
		case NotificationType.NOTIFICATION_EMAIL_CONFIRM:
		case NotificationType.NOTIFICATION_FRIENDSP_REQ:
		case NotificationType.NOTIFICATION_HELP_REQ:
			notificationViewData = null;
			break;
		}
		NotificationController.NotificationController_0.Push(notificationType_0, notificationViewData);
	}

	public override void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
	{
		Log.AddLine(string.Format("[NotificationEvent. Get notification data fail!] {0}", (abstractNetworkCommand_0 != null) ? string.Format("error code = {0}, message = {1}", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, abstractNetworkCommand_0.NetworkCommandInfo_0.string_0) : string.Empty));
	}

	private T FromBase64<T>(string string_1) where T : NotificationViewData
	{
		try
		{
			byte[] buffer = Convert.FromBase64String(string_1);
			return Serializer.Deserialize<T>(new MemoryStream(buffer));
		}
		catch (Exception)
		{
		}
		return (T)null;
	}

	private new static void Init()
	{
		NetworkCommands.Register(typeof(NotificationEventNetworkCommand), 127);
	}
}
