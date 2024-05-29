using engine.unity;

public class NotificationClanBattleInvite : NotificationView
{
	public UISprite uisprite_0;

	private NotificationClanBattleInviteData notificationClanBattleInviteData_0;

	public override void Init()
	{
		NotificationClanBattleInviteData notificationClanBattleInviteData = (notificationClanBattleInviteData_0 = notificationViewData_0 as NotificationClanBattleInviteData);
		ModeData objectByKey = ModeStorage.Get.Storage.GetObjectByKey(notificationClanBattleInviteData.Int32_3);
		MapData objectByKey2 = MapStorage.Get.Storage.GetObjectByKey(objectByKey.Int32_1);
		title.String_0 = string.Format(base.String_0, notificationClanBattleInviteData.String_0, Localizer.Get(objectByKey2.String_1));
		uisprite_0.String_0 = "notif_icon_duel";
		switch (objectByKey.ModeType_0)
		{
		case ModeType.DEATH_MATCH:
			uisprite_0.String_0 = "notif_icon_deathmath";
			break;
		case ModeType.TEAM_FIGHT:
			uisprite_0.String_0 = "notif_icon_team";
			break;
		case ModeType.FLAG_CAPTURE:
			uisprite_0.String_0 = "notif_icon_flag";
			break;
		}
		ClanBattleController.ClanBattleController_0.Subscribe(OnCancelInviteFight, ClanBattleController.EventType.HIDE_INVITE_NOTIF);
	}

	private void OnCancelInviteFight(ClanBattleController.EventData eventData_0)
	{
		if (eventData_0.Int32_1 == notificationClanBattleInviteData_0.Int32_2)
		{
			NotificationController.NotificationController_0.HideNotification(this);
		}
	}

	public override void OnClick()
	{
		string urlAtPosition = title.GetUrlAtPosition(UICamera.vector3_0);
		if (!string.IsNullOrEmpty(urlAtPosition))
		{
			ClanBattleController.ClanBattleController_0.SendRequestToUser(2, notificationClanBattleInviteData_0.Int32_2, string.Empty);
			WindowController.WindowController_0.ForceHideAllWindow();
			ClanWaitBattleWindow.Show(new ClanWaitBattleWindowParams(notificationClanBattleInviteData_0.Int32_2));
			base.Disappear(true);
		}
	}

	public override void OnClose()
	{
		if (notificationClanBattleInviteData_0 != null)
		{
			ClanBattleController.ClanBattleController_0.SendRequestToUser(3, notificationClanBattleInviteData_0.Int32_2, string.Empty);
		}
	}

	private void OnDestroy()
	{
		ClanBattleController.ClanBattleController_0.Unsubscribe(OnCancelInviteFight);
	}
}
