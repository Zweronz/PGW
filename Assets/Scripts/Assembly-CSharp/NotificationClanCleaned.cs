public class NotificationClanCleaned : NotificationView
{
	public override void Init()
	{
		title.String_0 = base.String_0;
	}

	public override void OnClick()
	{
		string urlAtPosition = title.GetUrlAtPosition(UICamera.vector3_0);
		if (!string.IsNullOrEmpty(urlAtPosition))
		{
			if (ClanController.ClanController_0.UserClanData_0 != null && ClanWindow.ClanWindow_0 == null)
			{
				ClanWindow.Show();
			}
			else if (ClanController.ClanController_0.UserClanData_0 == null && ClanTopWindow.ClanTopWindow_0 == null)
			{
				ClanTopWindow.Show();
			}
			Close();
		}
	}
}
