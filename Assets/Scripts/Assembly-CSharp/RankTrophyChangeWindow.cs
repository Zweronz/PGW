using System;
using System.Collections.Generic;
using UnityEngine;
using engine.controllers;
using engine.unity;

[GameWindowParams(GameWindowType.RankTrophyChangeWindow)]
public class RankTrophyChangeWindow : BaseGameWindow
{
	public enum TrophyState
	{
		NONE = 0,
		RANK_UP = 1,
		RANK_DOWN = 2,
		BULLET_UP = 3,
		BULLET_DOWN = 4
	}

	private enum TrophySounds
	{
		SND_BULLET_UP = 0,
		SND_BULLET_DOWN = 1,
		SND_RANK_UP = 2,
		SND_RANK_DOWN = 3
	}

	private static RankTrophyChangeWindow rankTrophyChangeWindow_0;

	public UILabel uilabel_0;

	public UILabel uilabel_1;

	public UILabel uilabel_2;

	public UILabel uilabel_3;

	public UILabel uilabel_4;

	public UISprite uisprite_0;

	public UISprite uisprite_1;

	public UISprite uisprite_2;

	public UISprite uisprite_3;

	public RankTrophyBullet rankTrophyBullet_0;

	public UIWidget uiwidget_0;

	public Animation animation_0;

	public RankTrophyAnimationHandler rankTrophyAnimationHandler_0;

	public UILabel uilabel_5;

	public AnimatedColor[] animatedColor_0;

	public Color[] color_0;

	public AudioClip[] audioClip_0;

	private RankTrophyChangeWindowParams rankTrophyChangeWindowParams_0;

	private List<RankTrophyBullet> list_0 = new List<RankTrophyBullet>();

	private int int_0 = 5;

	public static RankTrophyChangeWindow RankTrophyChangeWindow_0
	{
		get
		{
			return rankTrophyChangeWindow_0;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return RankTrophyChangeWindow_0 != null && RankTrophyChangeWindow_0.Boolean_0;
		}
	}

	public static void Show(RankTrophyChangeWindowParams rankTrophyChangeWindowParams_1 = null)
	{
		if (!(rankTrophyChangeWindow_0 != null))
		{
			if (RankInfoWindow.RankInfoWindow_0 != null)
			{
				RankInfoWindow.RankInfoWindow_0.Hide();
			}
			bool boolean_ = RebuyArticulWindow.Boolean_1;
			rankTrophyChangeWindow_0 = BaseWindow.Load("RankTrophyChangeWindow") as RankTrophyChangeWindow;
			rankTrophyChangeWindow_0.Parameters_0.type_0 = ((!boolean_) ? WindowQueue.Type.New : WindowQueue.Type.Top);
			rankTrophyChangeWindow_0.Parameters_0.bool_5 = true;
			rankTrophyChangeWindow_0.Parameters_0.bool_0 = false;
			rankTrophyChangeWindow_0.Parameters_0.bool_6 = true;
			if ((AppStateController.AppStateController_0.States_0 == AppStateController.States.IN_BATTLE || AppStateController.AppStateController_0.States_0 == AppStateController.States.IN_BATTLE_OVER_WINDOW) && (BattleOverWindow.BattleOverWindow_0 == null || !BattleOverWindow.BattleOverWindow_0.Boolean_0))
			{
				rankTrophyChangeWindow_0.Parameters_0.gameEvent_0 = WindowController.GameEvent.BATTLE_OVER_WINDOW_SHOW;
				rankTrophyChangeWindow_0.Parameters_0.bool_4 = true;
			}
			rankTrophyChangeWindow_0.InternalShow(rankTrophyChangeWindowParams_1);
		}
	}

	public override void OnShow()
	{
		int_0 = CommonParamsSettings.Get.MaxBulletInRow;
		base.OnShow();
		Init();
	}

	public override void OnHide()
	{
		base.OnHide();
		rankTrophyChangeWindow_0 = null;
	}

	private void Init()
	{
		rankTrophyChangeWindowParams_0 = base.WindowShowParameters_0 as RankTrophyChangeWindowParams;
		InitDefault();
		InitBullets();
		InitAnimation();
	}

	private void InitDefault()
	{
		uilabel_0.String_0 = Localizer.Get((rankTrophyChangeWindowParams_0.TrophyState_0 != TrophyState.RANK_UP) ? "ui.ranks.your_trophy.title" : "ui.ranks.new_trophy.title");
		RankLevelData rankLevelData = RankController.RankController_0.GetRankLevelData(rankTrophyChangeWindowParams_0.Int32_0);
		RankLevelData rankLevelData2 = RankController.RankController_0.GetRankLevelData(rankTrophyChangeWindowParams_0.Int32_1);
		if (rankLevelData == null || rankLevelData2 == null)
		{
			return;
		}
		uilabel_1.String_0 = Localizer.Get(rankLevelData.String_0);
		uilabel_2.String_0 = Localizer.Get(rankLevelData2.String_0);
		uilabel_3.String_0 = rankLevelData.Int32_0.ToString();
		uilabel_4.String_0 = rankLevelData2.Int32_0.ToString();
		uisprite_0.String_0 = rankLevelData.String_2;
		uisprite_1.String_0 = rankLevelData2.String_2;
		uisprite_2.String_0 = rankLevelData.String_3;
		uisprite_3.String_0 = rankLevelData2.String_3;
		animatedColor_0[0].color = GetShieldColor((rankTrophyChangeWindowParams_0.TrophyState_0 != TrophyState.RANK_UP) ? rankLevelData2.String_3 : rankLevelData.String_3);
		animatedColor_0[1].color = GetShieldColor((rankTrophyChangeWindowParams_0.TrophyState_0 != TrophyState.RANK_UP) ? rankLevelData.String_3 : rankLevelData2.String_3);
		animatedColor_0[2].color = GetShieldColor((rankTrophyChangeWindowParams_0.TrophyState_0 != TrophyState.RANK_UP) ? rankLevelData.String_3 : rankLevelData2.String_3);
		if (rankTrophyChangeWindowParams_0.TrophyState_0 != TrophyState.BULLET_UP && rankTrophyChangeWindowParams_0.TrophyState_0 != TrophyState.RANK_UP)
		{
			if (rankLevelData2.Boolean_2 && rankTrophyChangeWindowParams_0.Int32_2 == 0)
			{
				uilabel_5.gameObject.SetActive(true);
				uilabel_5.String_0 = Localizer.Get("ui.rank_wnd.rank_not_remove");
			}
			else if (rankLevelData2.Boolean_0)
			{
				uilabel_5.gameObject.SetActive(true);
				uilabel_5.String_0 = Localizer.Get("ui.rank_wnd.medals_not_remove");
			}
			else
			{
				uilabel_5.gameObject.SetActive(false);
			}
		}
		else
		{
			uilabel_5.gameObject.SetActive(false);
		}
	}

	private void InitBullets()
	{
		switch (rankTrophyChangeWindowParams_0.TrophyState_0)
		{
		default:
			InitBulletsMedalsChange();
			break;
		case TrophyState.RANK_DOWN:
			InitBulletsRankDown();
			break;
		case TrophyState.RANK_UP:
			InitBulletsRankUp();
			break;
		}
	}

	private void InitBulletsMedalsChange()
	{
		InitBulletCommon(rankTrophyChangeWindowParams_0.Int32_1, delegate(int int_1)
		{
			int num = rankTrophyChangeWindowParams_0.Int32_3;
			if (rankTrophyChangeWindowParams_0.Int32_3 < rankTrophyChangeWindowParams_0.Int32_2)
			{
				num++;
				if (Defs.Boolean_0)
				{
					NGUITools.PlaySound(audioClip_0[0], 1f, 1f);
				}
			}
			else if (rankTrophyChangeWindowParams_0.Int32_3 > rankTrophyChangeWindowParams_0.Int32_2)
			{
				if (Defs.Boolean_0)
				{
					NGUITools.PlaySound(audioClip_0[1], 1f, 1f);
				}
				num--;
			}
			return int_1 < num;
		});
	}

	private void InitBulletsRankUp()
	{
		if (Defs.Boolean_0)
		{
			NGUITools.PlaySound(audioClip_0[2], 1f, 1f);
		}
		InitBulletCommon(rankTrophyChangeWindowParams_0.Int32_0, (int int_1) => int_1 < rankTrophyChangeWindowParams_0.Int32_3);
	}

	private void InitBulletsRankDown()
	{
		if (Defs.Boolean_0)
		{
			NGUITools.PlaySound(audioClip_0[3], 1f, 1f);
		}
		InitBulletCommon(rankTrophyChangeWindowParams_0.Int32_0, (int int_1) => int_1 < rankTrophyChangeWindowParams_0.Int32_3);
	}

	private void InitBulletCommon(int int_1, Func<int, bool> func_0)
	{
		int int32_ = rankTrophyBullet_0.Int32_0;
		int int32_2 = rankTrophyBullet_0.Int32_1;
		int int32_3 = uiwidget_0.Int32_0;
		NGUITools.SetActive(rankTrophyBullet_0.gameObject, false);
		int medalsForNextRank = RankController.RankController_0.GetMedalsForNextRank(int_1);
		int num = Mathf.CeilToInt((float)medalsForNextRank / (float)int_0);
		float num2 = 0f;
		float num3 = 0f;
		int num4 = 0;
		for (int i = 0; i < num; i++)
		{
			int num5 = Mathf.Min(medalsForNextRank - num4, int_0);
			num2 = (float)(int32_3 - int32_ * (num5 - 1)) * 0.5f;
			for (int j = 0; j < num5; j++)
			{
				GameObject gameObject = NGUITools.AddChild(uiwidget_0.gameObject, rankTrophyBullet_0.gameObject);
				gameObject.name = string.Format("bullet_{0}", num4);
				Vector3 localPosition = new Vector3(num2, num3);
				gameObject.transform.localPosition = localPosition;
				RankTrophyBullet component = gameObject.GetComponent<RankTrophyBullet>();
				component.SetActiveState(func_0 != null && func_0(num4));
				list_0.Add(component);
				num2 += (float)int32_;
				NGUITools.SetActive(gameObject, true);
				num4++;
			}
			num3 -= (float)int32_2;
		}
		Vector3 localPosition2 = uilabel_1.transform.localPosition;
		localPosition2.y = num3 - (float)(int32_2 * 2);
		uilabel_1.transform.localPosition = localPosition2;
		uilabel_2.transform.localPosition = localPosition2;
	}

	private Color GetShieldColor(string string_0)
	{
		int num = 0;
		while (true)
		{
			if (num < color_0.Length)
			{
				if (string_0.Contains((num + 1).ToString()))
				{
					break;
				}
				num++;
				continue;
			}
			return Color.white;
		}
		return color_0[num];
	}

	private void ClearBullets()
	{
		foreach (RankTrophyBullet item in list_0)
		{
			item.gameObject.transform.parent = null;
			UnityEngine.Object.Destroy(item.gameObject);
		}
		list_0.Clear();
	}

	private void InitAnimation()
	{
		rankTrophyAnimationHandler_0.onAppearCompleteCallback = OnAppearAnimationComplete;
		rankTrophyAnimationHandler_0.onRankUpDownAnimationComplete = OnRankUpDownAnimationComplete;
		animation_0.Play("RankAppear");
	}

	private void AnimateLastBullet()
	{
		if (list_0.Count == 0)
		{
			return;
		}
		int num = ((rankTrophyChangeWindowParams_0.TrophyState_0 != TrophyState.RANK_UP) ? rankTrophyChangeWindowParams_0.Int32_2 : 0);
		if (num > rankTrophyChangeWindowParams_0.Int32_3 && num > 0)
		{
			for (int num2 = num - 1; num2 >= rankTrophyChangeWindowParams_0.Int32_3; num2--)
			{
				RankTrophyBullet rankTrophyBullet = list_0[num2];
				rankTrophyBullet.AnimateDisappear();
			}
		}
		else if (num < rankTrophyChangeWindowParams_0.Int32_3 && num < list_0.Count)
		{
			for (int i = num; i < rankTrophyChangeWindowParams_0.Int32_3; i++)
			{
				RankTrophyBullet rankTrophyBullet2 = list_0[i];
				rankTrophyBullet2.AnimateAppear();
			}
		}
	}

	private void OnAppearAnimationComplete()
	{
		switch (rankTrophyChangeWindowParams_0.TrophyState_0)
		{
		case TrophyState.RANK_UP:
			animation_0.Play("RankAscend");
			ClearBullets();
			InitBulletCommon(rankTrophyChangeWindowParams_0.Int32_1, (int int_1) => int_1 < rankTrophyChangeWindowParams_0.Int32_3);
			break;
		case TrophyState.RANK_DOWN:
			animation_0.Play("RankDescend");
			ClearBullets();
			InitBulletCommon(rankTrophyChangeWindowParams_0.Int32_1, (int int_1) => int_1 < rankTrophyChangeWindowParams_0.Int32_3);
			break;
		case TrophyState.BULLET_UP:
			AnimateLastBullet();
			break;
		case TrophyState.BULLET_DOWN:
			AnimateLastBullet();
			break;
		}
	}

	private void OnRankUpDownAnimationComplete()
	{
		switch (rankTrophyChangeWindowParams_0.TrophyState_0)
		{
		case TrophyState.RANK_DOWN:
			AnimateLastBullet();
			break;
		case TrophyState.RANK_UP:
			AnimateLastBullet();
			break;
		}
	}
}
