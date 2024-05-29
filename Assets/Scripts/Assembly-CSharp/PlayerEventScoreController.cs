using System.Collections.Generic;

public class PlayerEventScoreController
{
	private class ScoreEventInfo
	{
		public string string_0;

		public int int_0;

		public string string_1;

		public string string_2;

		public string string_3;

		public ScoreEventInfo(string string_4, int int_1, string string_5, string string_6, string string_7 = "")
		{
			string_0 = string_4;
			int_0 = int_1;
			string_1 = string_5;
			string_2 = string_6;
			string_3 = string_7;
		}
	}

	private static List<ScoreEventInfo> list_0;

	public static Dictionary<string, int> dictionary_0;

	public static Dictionary<string, string> dictionary_1;

	public static Dictionary<string, string> dictionary_2;

	public static Dictionary<string, string> dictionary_3;

	static PlayerEventScoreController()
	{
		list_0 = new List<ScoreEventInfo>();
		dictionary_0 = new Dictionary<string, int>();
		dictionary_1 = new Dictionary<string, string>();
		dictionary_2 = new Dictionary<string, string>();
		dictionary_3 = new Dictionary<string, string>();
		SetScoreEventInfo();
		SetLocalizeForScoreEvent();
	}

	public static void SetScoreEventInfo()
	{
		list_0.Clear();
		list_0.Add(new ScoreEventInfo(KillStreakType.KILLSTREAK_MULTIKILL_2.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILLSTREAK_MULTIKILL_2), "score_message.double_kill", "kill_1", "badge_1"));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILLSTREAK_MULTIKILL_3.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILLSTREAK_MULTIKILL_3), "score_message.triple_kill", "kill_2", "badge_2"));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILLSTREAK_MULTIKILL_4.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILLSTREAK_MULTIKILL_4), "score_message.multi_kill", "kill_3", "badge_3"));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILLSTREAK_MULTIKILL_5.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILLSTREAK_MULTIKILL_5), "score_message.ultra_kill", "kill_4", "badge_4"));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILLSTREAK_MULTIKILL_6.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILLSTREAK_MULTIKILL_6), "score_message.monster_kill", "kill_5", "badge_5"));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILLSTREAK_MULTIKILL_10.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILLSTREAK_MULTIKILL_10), "score_message.fury", "kill_9", "badge_9"));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILLSTREAK_MULTIKILL_20.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILLSTREAK_MULTIKILL_20), "score_message.rampage", "kill_19", "badge_19"));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILLSTREAK_MULTIKILL_50.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILLSTREAK_MULTIKILL_50), "score_message.violence", "kill_49", "badge_49"));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILLSTREAK_FLAGTOUCH.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILLSTREAK_FLAGTOUCH), "score_message.touch_down", "kstouchdown", string.Empty));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILLSTREAK_FLAGTOUCH_2.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILLSTREAK_FLAGTOUCH_2), "score_message.double_touch_down", "kstouchdown2", string.Empty));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILLSTREAK_FLAGTOUCH_3.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILLSTREAK_FLAGTOUCH_3), "score_message.triple_touch_down", "kstouchdown3", string.Empty));
		list_0.Add(new ScoreEventInfo(KillStreakType.FIRST_BLOOD.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.FIRST_BLOOD), "score_message.kill", "kill_fblood", "badge_fblood"));
		list_0.Add(new ScoreEventInfo(KillStreakType.REVENGE.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.REVENGE), "score_message.revenge", "ksrevenge", string.Empty));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILL_ASSIST.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILL_ASSIST), "score_message.kill_assist", "ksassist", string.Empty));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILL_COMMON.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILL_COMMON), "score_message.kill", "Kill", string.Empty));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILL_HEADSHOT.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILL_HEADSHOT), "score_message.headshot", "ksheadshot", string.Empty));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILL_DOUBLE_HEADSHOT.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILL_DOUBLE_HEADSHOT), "score_message.double_headshot", "ksheadshot2", string.Empty));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILL_GERENADE.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILL_GERENADE), "score_message.grenade", "ksgrenade", string.Empty));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILL_EXPLOSION.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILL_EXPLOSION), "score_message.kill", "Kill", string.Empty));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILL_TURRET.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILL_TURRET), "score_message.turret_kill", "TurretKill", string.Empty));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILL_INVISIBLE_KILL.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILL_INVISIBLE_KILL), "score_message.invisible_kill", "InvisibleKill", string.Empty));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILL_PLAYER_MECH.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILL_PLAYER_MECH), "score_message.mech_kill", "MechKill", string.Empty));
		list_0.Add(new ScoreEventInfo(KillStreakType.KILL_PLAYER_WITH_FLAG.ToString(), KillStreakStorage.Get.GetScoreForKillStreak(KillStreakType.KILL_PLAYER_WITH_FLAG), "score_message.flag_kill", "ksrflagkill", string.Empty));
	}

	public static void SetLocalizeForScoreEvent()
	{
		dictionary_0.Clear();
		dictionary_1.Clear();
		dictionary_2.Clear();
		dictionary_3.Clear();
		foreach (ScoreEventInfo item in list_0)
		{
			dictionary_0.Add(item.string_0, item.int_0);
			dictionary_1.Add(item.string_0, item.string_1);
			dictionary_2.Add(item.string_0, item.string_2);
			dictionary_3.Add(item.string_0, item.string_3);
		}
	}
}
