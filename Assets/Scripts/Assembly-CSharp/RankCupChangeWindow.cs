using System.Runtime.CompilerServices;
using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.Profile)]
public class RankCupChangeWindow : BaseGameWindow
{
	private GameObject gameObject_0;

	private RankCupChangeWindowParams.OpenType openType_0;

	[CompilerGenerated]
	private static RankCupChangeWindow rankCupChangeWindow_0;

	public static RankCupChangeWindow RankCupChangeWindow_0
	{
		[CompilerGenerated]
		get
		{
			return rankCupChangeWindow_0;
		}
		[CompilerGenerated]
		private set
		{
			rankCupChangeWindow_0 = value;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return RankCupChangeWindow_0 != null && RankCupChangeWindow_0.Boolean_0;
		}
	}

	public static void Show(RankCupChangeWindowParams rankCupChangeWindowParams_0 = null)
	{
		if (RankCupChangeWindow_0 != null)
		{
			return;
		}
		rankCupChangeWindowParams_0 = rankCupChangeWindowParams_0 ?? (rankCupChangeWindowParams_0 = new RankCupChangeWindowParams(0, RankCupChangeWindowParams.OpenType.NONE));
		if (rankCupChangeWindowParams_0.openType_0 == RankCupChangeWindowParams.OpenType.NONE)
		{
			if (Lobby.Boolean_0)
			{
				rankCupChangeWindowParams_0.openType_0 = RankCupChangeWindowParams.OpenType.FROM_LOBBY;
			}
			else if (ProfileWindow.Boolean_1)
			{
				rankCupChangeWindowParams_0.openType_0 = RankCupChangeWindowParams.OpenType.FROM_PROFILE;
			}
		}
		RankCupChangeWindow_0 = BaseWindow.Load("RankCupChangeWindow") as RankCupChangeWindow;
		RankCupChangeWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
		RankCupChangeWindow_0.Parameters_0.bool_5 = false;
		RankCupChangeWindow_0.Parameters_0.bool_0 = false;
		RankCupChangeWindow_0.Parameters_0.bool_6 = true;
		RankCupChangeWindow_0.InternalShow(rankCupChangeWindowParams_0);
	}

	public override void OnShow()
	{
		base.OnShow();
		Lobby.persController_0.Boolean_1 = false;
		openType_0 = (base.WindowShowParameters_0 as RankCupChangeWindowParams).openType_0;
		switch (openType_0)
		{
		case RankCupChangeWindowParams.OpenType.FROM_PROFILE:
			NGUITools.SetActive(ProfileWindow.ProfileWindow_0.gameObject, false);
			break;
		case RankCupChangeWindowParams.OpenType.FROM_LOBBY:
			Lobby.Lobby_0.Hide();
			break;
		}
		InitRankCupArrows();
		gameObject_0 = GameObject.Find("Camera_Rotate");
		if (gameObject_0 != null)
		{
			gameObject_0.GetComponent<Animation>().enabled = true;
			AnimateCamera(true);
		}
	}

	public override void OnHide()
	{
		base.OnHide();
		RankCupChangeWindow_0 = null;
		AnimateCamera(false);
		switch (openType_0)
		{
		case RankCupChangeWindowParams.OpenType.FROM_PROFILE:
			NGUITools.SetActive(ProfileWindow.ProfileWindow_0.gameObject, true);
			break;
		case RankCupChangeWindowParams.OpenType.FROM_LOBBY:
			Lobby.Lobby_0.Show();
			break;
		}
		Lobby.persController_0.Boolean_1 = true;
		Lobby.persController_0.Reset();
	}

	private void AnimateCamera(bool bool_1)
	{
		if (gameObject_0 != null)
		{
			gameObject_0.GetComponent<Animation>().Play((!bool_1) ? "RankCupChangeCloseCamera" : "RankCupChangeOpenCamera");
		}
	}

	private void InitRankCupArrows()
	{
		RankCupController.RankCupController_0.InitRankCupArrows(base.transform.GetChild(0).gameObject);
	}
}
