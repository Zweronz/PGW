using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.data;
using engine.events;
using engine.helpers;
using pixelgun.tutorial;

public sealed class RankController : BaseEvent<RankController.EventData>
{
	public sealed class EventData
	{
		public int int_0;
	}

	public enum EventType
	{
		UPDATE_SEASON = 0,
		PROFILE_OPENED = 1,
		PROFILE_CLOSED = 2
	}

	private const string string_0 = "last_season_notif_day";

	private const int int_0 = 432000;

	private static RankController rankController_0;

	private static BaseSharedSettings baseSharedSettings_0;

	private readonly string string_1 = "UI/Controls/Ranks/RankTrophyView";

	private GameObject gameObject_0;

	private int int_1;

	private RankSeasonData rankSeasonData_0;

	[CompilerGenerated]
	private bool bool_0;

	public static RankController RankController_0
	{
		get
		{
			if (rankController_0 == null)
			{
				rankController_0 = new RankController();
			}
			return rankController_0;
		}
	}

	public RankSeasonData RankSeasonData_0
	{
		get
		{
			return rankSeasonData_0;
		}
	}

	public int Int32_0
	{
		get
		{
			return UsersData.UsersData_0.UserData_0.userRankData_0.int_1;
		}
	}

	public int Int32_1
	{
		get
		{
			return UsersData.UsersData_0.UserData_0.userRankData_0.int_0;
		}
	}

	public bool Boolean_0
	{
		get
		{
			return rankSeasonData_0 != null;
		}
	}

	public bool Boolean_1
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		set
		{
			bool_0 = value;
		}
	}

	public bool Boolean_2
	{
		get
		{
			if (!Boolean_0)
			{
				return false;
			}
			if (Int32_1 == 0 && Int32_0 == RankSeasonData_0.Int32_4 && UserController.UserController_0.UserData_0.user_0.int_7 == 0)
			{
				return false;
			}
			return true;
		}
	}

	private RankController()
	{
	}

	public void Init(BaseSharedSettings baseSharedSettings_1)
	{
		baseSharedSettings_0 = baseSharedSettings_1;
		Boolean_1 = false;
		int_1 = baseSharedSettings_0.GetValue("last_season_notif_day", 0);
		UsersData.Subscribe(UsersData.EventType.RANK_LEVEL_UP, OnRankLevelUp);
		UsersData.Subscribe(UsersData.EventType.RANK_LEVEL_DOWN, OnRankLevelDown);
		UsersData.Subscribe(UsersData.EventType.RANK_MEDALS_CHANGED, OnRankMedalsChanged);
		UsersData.Subscribe(UsersData.EventType.RANK_FIRST_RANK_BATTLE_END, OnFirstRankBattleEnd);
	}

	public RankLevelData GetRankLevelData(int int_2)
	{
		return RankLevelStorage.Get.Storage.GetObjectByKey(int_2);
	}

	public int GetMedalsForNextRank(int int_2 = 0)
	{
		int_2 = ((int_2 <= 0) ? Int32_0 : int_2);
		return RankLevelStorage.Get.GetMaxMedalsForLevel(int_2);
	}

	public ArtikulData GetRankCupArtikul(int int_2)
	{
		return ArtikulController.ArtikulController_0.GetArtikul(int_2);
	}

	public List<ArtikulData> GetRankCupArtikuls()
	{
		return ArtikulController.ArtikulController_0.GetArtikulsBySlot(SlotType.SLOT_RANK_CUP);
	}

	public GameObject GetRankCupGameObject(int int_2)
	{
		return UserController.UserController_0.GetGameObject(int_2);
	}

	public int GetPlaceSeasonByCup(int int_2)
	{
		int value = 0;
		UserController.UserController_0.UserData_0.dictionary_6.TryGetValue(int_2, out value);
		return value;
	}

	private void OnRankLevelUp(UsersData.EventData eventData_0)
	{
		if (rankSeasonData_0 == null || !((double)rankSeasonData_0.Int32_1 < Utility.Double_0))
		{
			RankTrophyChangeWindowParams rankTrophyChangeWindowParams = new RankTrophyChangeWindowParams();
			rankTrophyChangeWindowParams.TrophyState_0 = RankTrophyChangeWindow.TrophyState.RANK_UP;
			rankTrophyChangeWindowParams.Int32_0 = eventData_0.int_0;
			rankTrophyChangeWindowParams.Int32_1 = Int32_0;
			rankTrophyChangeWindowParams.Int32_2 = (int)eventData_0.double_0;
			rankTrophyChangeWindowParams.Int32_3 = Int32_1;
			RankTrophyChangeWindow.Show(rankTrophyChangeWindowParams);
			Log.AddLineFormat("RankController::OnRankLevelUp > rank: {0}, medals: {1}", Int32_0, Int32_1);
		}
	}

	private void OnRankLevelDown(UsersData.EventData eventData_0)
	{
		if (rankSeasonData_0 == null || !((double)rankSeasonData_0.Int32_1 < Utility.Double_0))
		{
			RankTrophyChangeWindowParams rankTrophyChangeWindowParams = new RankTrophyChangeWindowParams();
			rankTrophyChangeWindowParams.TrophyState_0 = RankTrophyChangeWindow.TrophyState.RANK_DOWN;
			rankTrophyChangeWindowParams.Int32_0 = eventData_0.int_0;
			rankTrophyChangeWindowParams.Int32_1 = Int32_0;
			rankTrophyChangeWindowParams.Int32_2 = (int)eventData_0.double_0;
			rankTrophyChangeWindowParams.Int32_3 = Int32_1;
			RankTrophyChangeWindow.Show(rankTrophyChangeWindowParams);
			Log.AddLineFormat("RankController::OnRankLevelDown > rank: {0}, medals: {1}", Int32_0, Int32_1);
		}
	}

	private void OnRankMedalsChanged(UsersData.EventData eventData_0)
	{
		if (rankSeasonData_0 == null || !((double)rankSeasonData_0.Int32_1 < Utility.Double_0))
		{
			int num = (int)eventData_0.double_0;
			RankTrophyChangeWindowParams rankTrophyChangeWindowParams = new RankTrophyChangeWindowParams();
			rankTrophyChangeWindowParams.TrophyState_0 = ((num >= Int32_1) ? RankTrophyChangeWindow.TrophyState.BULLET_DOWN : RankTrophyChangeWindow.TrophyState.BULLET_UP);
			rankTrophyChangeWindowParams.Int32_0 = eventData_0.int_0;
			rankTrophyChangeWindowParams.Int32_1 = Int32_0;
			rankTrophyChangeWindowParams.Int32_2 = (int)eventData_0.double_0;
			rankTrophyChangeWindowParams.Int32_3 = Int32_1;
			RankTrophyChangeWindow.Show(rankTrophyChangeWindowParams);
			Log.AddLineFormat("RankController::OnRankMedalsChanged > rank: {0}, medals: {1}", Int32_0, Int32_1);
		}
	}

	private void OnFirstRankBattleEnd(UsersData.EventData eventData_0)
	{
		if (Boolean_0 && (rankSeasonData_0 == null || !((double)rankSeasonData_0.Int32_1 < Utility.Double_0)))
		{
			RankTrophyChangeWindowParams rankTrophyChangeWindowParams = new RankTrophyChangeWindowParams();
			rankTrophyChangeWindowParams.TrophyState_0 = RankTrophyChangeWindow.TrophyState.NONE;
			rankTrophyChangeWindowParams.Int32_0 = Int32_0;
			rankTrophyChangeWindowParams.Int32_1 = Int32_0;
			rankTrophyChangeWindowParams.Int32_2 = Int32_1;
			rankTrophyChangeWindowParams.Int32_3 = Int32_1;
			RankTrophyChangeWindow.Show(rankTrophyChangeWindowParams);
			Log.AddLineFormat("RankController::OnFirstRankBattleEnd > rank: {0}, medals: {1}", Int32_0, Int32_1);
		}
	}

	public void UpdateRankSeason(RankSeasonData rankSeasonData_1)
	{
		rankSeasonData_0 = rankSeasonData_1;
		Dispatch(null, EventType.UPDATE_SEASON);
	}

	public void OnRankSeasonStart(double double_0, double double_1)
	{
		Log.AddLineFormat("RankController::OnRankSeasonStart > time_start: {0}, time_end: {1}", double_0, double_1);
	}

	public void OnRankSeasonEnd(int int_2, UserRankData userRankData_0)
	{
		int num = ((userRankData_0 != null) ? userRankData_0.int_1 : 0);
		int num2 = ((userRankData_0 != null) ? userRankData_0.int_0 : 0);
		Log.AddLineFormat("RankController::OnRankSeasonEnd > artikul_id: {0}, rank_level: {1}, rank_medals: {2}", int_2, num, num2);
		Boolean_1 = false;
		if (!TutorialController.TutorialController_0.Boolean_0 && Boolean_2)
		{
			RankSeasonCompleteWindowParams rankSeasonCompleteWindowParams = new RankSeasonCompleteWindowParams();
			rankSeasonCompleteWindowParams.Int32_0 = int_2;
			rankSeasonCompleteWindowParams.UserRankData_0 = userRankData_0;
			RankSeasonCompleteWindow.Show(rankSeasonCompleteWindowParams);
		}
	}

	public void CheckSeasonNotitifaction()
	{
		if (!Boolean_0)
		{
			return;
		}
		double double_ = Utility.Double_0;
		if (!(double_ + 432000.0 < (double)RankSeasonData_0.Int32_1))
		{
			int num = (int)(double_ / 86400.0) * 86400;
			int num2 = RankSeasonData_0.Int32_1 / 86400 * 86400;
			if (num != int_1 && num < num2)
			{
				int int32_ = Math.Max(1, (num2 - num) / 86400);
				NotificationController.NotificationController_0.Push(NotificationType.NOTIFICATION_RANK_SEASON_TIME_END, new NotificationRanksData
				{
					Int32_1 = int32_
				});
				int_1 = num;
				baseSharedSettings_0.SetValue("last_season_notif_day", int_1, true);
			}
		}
	}

	public void InitRankTrophy(GameObject gameObject_1, bool bool_1, int int_2 = 0, int int_3 = -1, bool bool_2 = true)
	{
		if (Boolean_0)
		{
			if (gameObject_0 == null)
			{
				gameObject_0 = Resources.Load<GameObject>(string_1);
			}
			GameObject gameObject = NGUITools.AddChild(gameObject_1, gameObject_0);
			RankTrophyView component = gameObject.GetComponent<RankTrophyView>();
			component.SetData(int_2, int_3, bool_1, bool_2);
			gameObject.transform.localPosition = new Vector3(0f, 0f);
		}
	}
}
