using engine.helpers;

public class NotificationMaintanance : NotificationView
{
	public override void Init()
	{
		MaintananceNotificationData maintananceNotificationData = notificationViewData_0 as MaintananceNotificationData;
		string string_ = base.String_0;
		if (!string.IsNullOrEmpty(string_))
		{
			string localTime = Utility.GetLocalTime(maintananceNotificationData.Int32_1);
			string localTime2 = Utility.GetLocalTime(maintananceNotificationData.Int32_2);
			string localTime3 = Utility.GetLocalTime(maintananceNotificationData.Int32_2, "MM/dd/yy");
			title.String_0 = string.Format(string_, localTime, localTime2, localTime3);
		}
	}
}
