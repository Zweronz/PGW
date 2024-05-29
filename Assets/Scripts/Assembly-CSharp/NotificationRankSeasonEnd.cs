public class NotificationRankSeasonEnd : NotificationView
{
	public override void Init()
	{
		NotificationRanksData notificationRanksData = notificationViewData_0 as NotificationRanksData;
		if (notificationRanksData.Int32_1 > 1)
		{
			title.String_0 = string.Format(base.String_0, notificationRanksData.Int32_1);
		}
		else
		{
			title.String_0 = string.Format(Localizer.Get("notification.one_day_rank_season_end"), notificationRanksData.Int32_1);
		}
	}
}
