public class NotificationClanInvite : NotificationView
{
	private UserClanData userClanData_0;

	public override void Init()
	{
		ClanNotificationData clanNotificationData = notificationViewData_0 as ClanNotificationData;
		title.String_0 = string.Format(base.String_0, clanNotificationData.userClanData_0.string_0, clanNotificationData.userClanData_0.string_2);
		userClanData_0 = clanNotificationData.userClanData_0;
	}

	public override void OnClick()
	{
		string urlAtPosition = title.GetUrlAtPosition(UICamera.vector3_0);
		if (!string.IsNullOrEmpty(urlAtPosition))
		{
			ClanWindowParams clanWindowParams = new ClanWindowParams();
			clanWindowParams.String_0 = urlAtPosition;
			clanWindowParams.UserClanData_0 = userClanData_0;
			ClanWindow.Show(clanWindowParams);
			Close();
		}
	}
}
