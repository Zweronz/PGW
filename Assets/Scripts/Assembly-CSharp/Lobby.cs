using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.unity;
using pixelgun.tutorial;

public sealed class Lobby : MonoBehaviour
{
	public UIButton developerConsole;

	public UIButton webViewButton;

	public StockViewController stockViewController;

	public UIWidget rankTrophyPlaceholder;

	public static PersController persController_0;

	private static Lobby lobby_0;

	[CompilerGenerated]
	private static Action action_0;

	public static Lobby Lobby_0
	{
		get
		{
			if (lobby_0 == null)
			{
				GameObject gameObject = ScreenController.ScreenController_0.LoadUI("Lobby");
				lobby_0 = gameObject.GetComponent<Lobby>();
				GameObject gameObject2 = GameObject.Find("PersConfigurator");
				if (gameObject2 != null)
				{
					persController_0 = gameObject2.GetComponent<PersController>();
				}
			}
			return lobby_0;
		}
	}

	public static bool Boolean_0
	{
		get
		{
			return lobby_0 != null && lobby_0.isActiveAndEnabled;
		}
	}

	private void OnDestroy()
	{
		lobby_0 = null;
	}

	private void Update()
	{
		if (WindowController.WindowController_0.Int32_0 == 0 && InputManager.GetButtonUp("Back"))
		{
			if (TutorialController.TutorialController_0.Boolean_0)
			{
				TutorialController.TutorialController_0.HideTutor();
			}
			else
			{
				OnExitButtonClick();
			}
		}
	}

	public void Show()
	{
		NGUITools.SetActive(base.gameObject, true);
		OnShow();
	}

	public void Hide()
	{
		OnHide();
		lobby_0 = null;
		NGUITools.Destroy(base.gameObject);
	}

	private void OnShow()
	{
		Init();
	}

	private void OnHide()
	{
	}

	private void Init()
	{
		Screen.lockCursor = false;
		InitControls();
		InitDevControls();
		InitRankTrophy();
	}

	private void InitControls()
	{
	}

	private void InitDevControls()
	{
		if (developerConsole != null)
		{
			developerConsole.gameObject.SetActive(false);
		}
		if (webViewButton != null)
		{
			webViewButton.gameObject.SetActive(false);
		}
	}

	private void InitRankTrophy()
	{
		if (RankController.RankController_0.Boolean_0 && RankController.RankController_0.Boolean_2)
		{
			RankController.RankController_0.InitRankTrophy(rankTrophyPlaceholder.gameObject, true);
			RankController.RankController_0.CheckSeasonNotitifaction();
		}
	}

	public void UpdateStockTable()
	{
		stockViewController.UpdateStockTable();
	}

	public void OnClanButtonClick()
	{
		if (!ClanController.ClanController_0.Boolean_0)
		{
			MessageWindow.Show(new MessageWindowParams(Localizer.Get("clan.message.clan_not_available")));
		}
		else if (ClanController.ClanController_0.UserClanData_0 == null)
		{
			ClanTopWindowParams clanTopWindowParams = new ClanTopWindowParams();
			clanTopWindowParams.Boolean_0 = true;
			ClanTopWindow.Show(clanTopWindowParams);
		}
		else
		{
			ClanWindow.Show();
		}
	}

	public void OnExitButtonClick()
	{
		Hide();
		GameController.GameController_0.Exit();
	}

	public void OnSettingsButtonClick()
	{
		Hide();
		SettingsWindow.Show();
	}

	public void OnProfileButtonClick()
	{
		Hide();
		ProfileWindow.Show();
	}

	public void OnMoneyButtonClick()
	{
		BankController.BankController_0.TryOpenBank(BankController.BankSourceType.BANK_LOBBY);
	}

	public void OnMissionsButtonClick()
	{
		FightOfflineController.FightOfflineController_0.StartFight();
	}

	public void OnShopButtonClick()
	{
		Hide();
		ShopWindowParams shopWindowParams = new ShopWindowParams();
		shopWindowParams.action_0 = delegate
		{
			Lobby_0.Show();
		};
		shopWindowParams.openStyle_0 = ShopWindow.OpenStyle.ANIMATED;
		ShopWindow.Show(shopWindowParams);
	}

	public void OnFightButtonClick()
	{
		Hide();
		SelectMapWindow.Show();
	}

	public void OnDeveloperConsoleClick()
	{
	}

	public void OnWebViewClick()
	{
	}

	public void OnBugReportButtonClick()
	{
		BugReportController.BugReportController_0.SwitchWindow();
	}

	public void OnRankCupArrowClick(GameObject gameObject_0)
	{
		if (gameObject_0.name.Equals("Left"))
		{
			RankCupController.RankCupController_0.PrevCup();
		}
		else
		{
			RankCupController.RankCupController_0.NextCup();
		}
	}
}
