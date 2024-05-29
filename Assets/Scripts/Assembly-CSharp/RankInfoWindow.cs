using System;
using engine.events;
using engine.helpers;
using engine.unity;

[GameWindowParams(GameWindowType.RankInfoWindow)]
public class RankInfoWindow : BaseGameWindow
{
	private static RankInfoWindow rankInfoWindow_0;

	public UILabel uilabel_0;

	public UILabel uilabel_1;

	public UILabel uilabel_2;

	public RankTrophyView rankTrophyView_0;

	private string string_0;

	private string string_1;

	private string string_2;

	private string string_3;

	public static RankInfoWindow RankInfoWindow_0
	{
		get
		{
			return rankInfoWindow_0;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return RankInfoWindow_0 != null && RankInfoWindow_0.Boolean_0;
		}
	}

	public static void Show(RankInfoWindowParams rankInfoWindowParams_0 = null)
	{
		if (!(rankInfoWindow_0 != null) && RankController.RankController_0.RankSeasonData_0 != null)
		{
			rankInfoWindow_0 = BaseWindow.Load("RankInfoWindow") as RankInfoWindow;
			rankInfoWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			rankInfoWindow_0.Parameters_0.bool_5 = true;
			rankInfoWindow_0.Parameters_0.bool_0 = false;
			rankInfoWindow_0.Parameters_0.bool_6 = true;
			rankInfoWindow_0.InternalShow(rankInfoWindowParams_0);
		}
	}

	public override void OnShow()
	{
		base.OnShow();
		Init();
	}

	public override void OnHide()
	{
		base.OnHide();
		rankInfoWindow_0 = null;
		if (DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateOneSec))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(UpdateOneSec);
		}
	}

	private void Init()
	{
		RankLevelData rankLevelData = RankController.RankController_0.GetRankLevelData(RankController.RankController_0.Int32_0);
		if (rankLevelData != null)
		{
			uilabel_0.String_0 = Localizer.Get(rankLevelData.String_0);
		}
		RankLevelData rankLevelData2 = RankController.RankController_0.GetRankLevelData(RankController.RankController_0.RankSeasonData_0.Int32_2);
		if (rankLevelData2 != null)
		{
			if (RankController.RankController_0.RankSeasonData_0.Int32_2 == RankController.RankController_0.Int32_0)
			{
				uilabel_1.String_0 = Localizer.Get("ui.ranks.your_current_trophy.descr_reward_rank");
			}
			else
			{
				uilabel_1.String_0 = string.Format(Localizer.Get("ui.ranks.your_current_trophy.descr"), rankLevelData2.Int32_0, Localizer.Get(rankLevelData2.String_0));
			}
		}
		InitTimer();
	}

	private void InitTimer()
	{
		string_0 = Localizer.Get("ui.day.mini");
		string_1 = Localizer.Get("ui.hour.mini");
		string_2 = Localizer.Get("ui.min.mini");
		string_3 = Localizer.Get("ui.sec.mini");
		UpdateOneSec();
		if (!DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateOneSec))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(UpdateOneSec);
		}
	}

	private void UpdateOneSec()
	{
		int int_ = Math.Max(0, (int)((double)RankController.RankController_0.RankSeasonData_0.Int32_1 - Utility.Double_0));
		uilabel_2.String_0 = string.Format(Localizer.Get("ui.ranks.your_current_trophy.remains"), Utility.GetLocalizedTime(int_, string_0, string_1, string_2, string_3));
	}
}
