using engine.controllers;
using engine.unity;

[GameWindowParams(GameWindowType.RankSeasonCompleteWindow)]
public class RankSeasonCompleteWindow : BaseGameWindow
{
	private static RankSeasonCompleteWindow rankSeasonCompleteWindow_0;

	public UILabel uilabel_0;

	public UILabel uilabel_1;

	public UILabel uilabel_2;

	public UIButton uibutton_0;

	public UIButton uibutton_1;

	public UIWidget uiwidget_0;

	public UIWidget uiwidget_1;

	public UITexture uitexture_0;

	public RankTrophyView rankTrophyView_0;

	private ArtikulData artikulData_0;

	public static RankSeasonCompleteWindow RankSeasonCompleteWindow_0
	{
		get
		{
			return rankSeasonCompleteWindow_0;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return RankSeasonCompleteWindow_0 != null && RankSeasonCompleteWindow_0.Boolean_0;
		}
	}

	public static void Show(RankSeasonCompleteWindowParams rankSeasonCompleteWindowParams_0 = null)
	{
		if (!(rankSeasonCompleteWindow_0 != null) && RankController.RankController_0.Boolean_0)
		{
			if (RankInfoWindow.RankInfoWindow_0 != null)
			{
				RankInfoWindow.RankInfoWindow_0.Hide();
			}
			bool flag = false;
			if (RebuyArticulWindow.Boolean_1 || StockGachaWindow.StockGachaWindow_0 != null || StockGachaRewardWindow.StockGachaRewardWindow_0 != null)
			{
				flag = true;
			}
			rankSeasonCompleteWindow_0 = BaseWindow.Load("RankSeasonCompleteWindow") as RankSeasonCompleteWindow;
			rankSeasonCompleteWindow_0.Parameters_0.type_0 = ((!flag) ? WindowQueue.Type.New : WindowQueue.Type.Top);
			rankSeasonCompleteWindow_0.Parameters_0.bool_5 = true;
			rankSeasonCompleteWindow_0.Parameters_0.bool_0 = false;
			rankSeasonCompleteWindow_0.Parameters_0.bool_6 = true;
			if (AppStateController.AppStateController_0.States_0 != AppStateController.States.MAIN_MENU)
			{
				rankSeasonCompleteWindow_0.Parameters_0.gameEvent_0 = WindowController.GameEvent.IN_MAIN_MENU;
			}
			rankSeasonCompleteWindow_0.InternalShow(rankSeasonCompleteWindowParams_0);
		}
	}

	public override void OnShow()
	{
		if (RankTrophyChangeWindow.RankTrophyChangeWindow_0 != null && RankTrophyChangeWindow.RankTrophyChangeWindow_0.Boolean_0)
		{
			RankTrophyChangeWindow.RankTrophyChangeWindow_0.UIPanel_0.Single_2 = 0f;
		}
		base.OnShow();
		RankSeasonCompleteWindowParams rankSeasonCompleteWindowParams = base.WindowShowParameters_0 as RankSeasonCompleteWindowParams;
		artikulData_0 = ((rankSeasonCompleteWindowParams == null) ? null : ArtikulController.ArtikulController_0.GetArtikul(rankSeasonCompleteWindowParams.Int32_0));
		Init();
	}

	public override void OnHide()
	{
		base.OnHide();
		rankSeasonCompleteWindow_0 = null;
		if (RankTrophyChangeWindow.RankTrophyChangeWindow_0 != null && RankTrophyChangeWindow.RankTrophyChangeWindow_0.Boolean_0)
		{
			RankTrophyChangeWindow.RankTrophyChangeWindow_0.UIPanel_0.Single_2 = 1f;
		}
		if (RankTrophyChangeWindow.RankTrophyChangeWindow_0 == null && RankController.RankController_0.Boolean_0 && RankController.RankController_0.Int32_0 == RankController.RankController_0.RankSeasonData_0.Int32_4 && RankController.RankController_0.Int32_1 == 0)
		{
			RankTrophyChangeWindowParams rankTrophyChangeWindowParams = new RankTrophyChangeWindowParams();
			rankTrophyChangeWindowParams.TrophyState_0 = RankTrophyChangeWindow.TrophyState.NONE;
			rankTrophyChangeWindowParams.Int32_0 = RankController.RankController_0.Int32_0;
			rankTrophyChangeWindowParams.Int32_1 = RankController.RankController_0.Int32_0;
			rankTrophyChangeWindowParams.Int32_2 = RankController.RankController_0.Int32_1;
			rankTrophyChangeWindowParams.Int32_3 = RankController.RankController_0.Int32_1;
			RankTrophyChangeWindow.Show(rankTrophyChangeWindowParams);
		}
	}

	private void Init()
	{
		uilabel_0.String_0 = Localizer.Get("ui.ranks.your_trophy.title");
		NGUITools.SetActive(uiwidget_1.gameObject, false);
		NGUITools.SetActive(uiwidget_0.gameObject, true);
		RankSeasonCompleteWindowParams rankSeasonCompleteWindowParams = base.WindowShowParameters_0 as RankSeasonCompleteWindowParams;
		RankLevelData rankLevelData = null;
		if (rankSeasonCompleteWindowParams != null && rankSeasonCompleteWindowParams.UserRankData_0 != null)
		{
			rankTrophyView_0.SetData(rankSeasonCompleteWindowParams.UserRankData_0.int_1, rankSeasonCompleteWindowParams.UserRankData_0.int_0, false, false);
			rankLevelData = RankController.RankController_0.GetRankLevelData(rankSeasonCompleteWindowParams.UserRankData_0.int_1);
		}
		else
		{
			rankLevelData = RankController.RankController_0.GetRankLevelData(RankController.RankController_0.Int32_0);
		}
		NGUITools.SetActive(rankTrophyView_0.gameObject, true);
		if (rankLevelData != null)
		{
			uilabel_1.String_0 = Localizer.Get(rankLevelData.String_0);
		}
		if (artikulData_0 != null)
		{
			uilabel_2.String_0 = artikulData_0.String_4;
		}
		NGUITools.SetActive(uibutton_0.gameObject, artikulData_0 == null);
		NGUITools.SetActive(uibutton_1.gameObject, artikulData_0 != null);
		uitexture_0.Texture_0 = ImageLoader.LoadArtikulTexture(artikulData_0);
	}

	public void OnNextButtonClick()
	{
		uilabel_0.String_0 = Localizer.Get("ui.ranks.your_reward.title");
		NGUITools.SetActive(uiwidget_1.gameObject, true);
		NGUITools.SetActive(uiwidget_0.gameObject, false);
	}
}
