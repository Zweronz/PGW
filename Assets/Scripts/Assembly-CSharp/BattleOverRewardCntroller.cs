using System;
using System.Collections.Generic;
using UnityEngine;
using engine.helpers;
using engine.unity;

public static class BattleOverRewardCntroller
{
	private static bool bool_0;

	private static bool bool_1;

	private static bool bool_2;

	private static PlayerCommandController playerCommandController_0;

	private static PlayerScoreController playerScoreController_0;

	public static BattleOverWindowParams GetFightRewards()
	{
		bool_0 = false;
		bool_1 = false;
		if (MonoSingleton<FightController>.Prop_0 != null && MonoSingleton<FightController>.Prop_0.ModeData_0 != null)
		{
			if (MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.TEAM_FIGHT)
			{
				bool_0 = true;
			}
			if (MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.FLAG_CAPTURE)
			{
				bool_1 = true;
			}
			if (MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.DEATH_MATCH)
			{
				bool_2 = true;
			}
		}
		playerCommandController_0 = WeaponManager.weaponManager_0.myNetworkStartTable.PlayerCommandController_0;
		playerScoreController_0 = WeaponManager.weaponManager_0.myScoreController;
		List<BattleOverPlayerData> playersParamsParams = getPlayersParamsParams();
		List<BattleOverPlayerData> list_ = new List<BattleOverPlayerData>();
		List<BattleOverPlayerData> list_2 = new List<BattleOverPlayerData>();
		return getFightReward(playersParamsParams, list_, list_2);
	}

	private static List<BattleOverPlayerData> getPlayersParamsParams()
	{
		List<BattleOverPlayerData> list = new List<BattleOverPlayerData>();
		GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
		if (array != null && array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				try
				{
					NetworkStartTable component = array[i].GetComponent<NetworkStartTable>();
					if (!(component == null) && !(component.PlayerScoreController_0 == null))
					{
						BattleOverPlayerData battleOverPlayerData = new BattleOverPlayerData();
						battleOverPlayerData.int_1 = (int)component.PhotonView_0.PhotonPlayer_0.Hashtable_0["uid"];
						battleOverPlayerData.byte_0 = (byte)component.PlayerCommandController_0.Int32_0;
						battleOverPlayerData.string_0 = component.String_5;
						battleOverPlayerData.string_1 = component.String_2;
						battleOverPlayerData.string_2 = component.String_3;
						battleOverPlayerData.texture_0 = component.Texture_2;
						battleOverPlayerData.int_2 = component.PlayerScoreController_0.Int32_0;
						battleOverPlayerData.int_3 = component.PlayerScoreController_0.Int16_0;
						battleOverPlayerData.int_4 = component.PlayerScoreController_0.Int16_1;
						battleOverPlayerData.int_5 = component.PlayerScoreController_0.Int16_3;
						battleOverPlayerData.int_6 = component.PlayerScoreController_0.Int16_2;
						battleOverPlayerData.int_7 = component.PlayerScoreController_0.Int32_1;
						battleOverPlayerData.int_8 = component.PlayerScoreController_0.Int32_3;
						battleOverPlayerData.int_9 = component.PlayerScoreController_0.Int32_2;
						battleOverPlayerData.float_1 = component.PlayerScoreController_0.Single_0;
						list.Add(battleOverPlayerData);
					}
				}
				catch (Exception ex)
				{
					Log.AddLineError("[NetworkStartTable::getPlayersParamsParams] EXCEPTION {0}", ex.ToString());
					MonoSingleton<Log>.Prop_0.DumpError(ex, true);
				}
			}
			if (list.Count == 0)
			{
				Log.AddLine("[NetworkStartTable::getPlayersParamsParams] res.Count = 0", Log.LogLevel.ERROR);
			}
			return list;
		}
		Log.AddLine("[NetworkStartTable::getPlayersParamsParams] ERROR tabs == null || tabs.Length == 0", Log.LogLevel.ERROR);
		return list;
	}

	private static BattleOverWindowParams getFightReward(List<BattleOverPlayerData> list_0, List<BattleOverPlayerData> list_1, List<BattleOverPlayerData> list_2)
	{
		if ((bool_0 || bool_1) && playerCommandController_0.TypeCommand_1 == TypeCommand.None)
		{
			Log.AddLineWarning("[NetworkStartTable::getFightReward] isTeamBattle = {0}  isFlag = {1}  CommandController.MyCommandInt {2}", bool_0.ToString(), bool_1.ToString(), playerCommandController_0.Int32_1);
		}
		bool bool_ = !string.IsNullOrEmpty((string)PhotonNetwork.Room_0.Hashtable_0["Pass"]);
		bool bool_2 = (bool)PhotonNetwork.Room_0.Hashtable_0["IsRanked"];
		for (int i = 0; i < list_0.Count; i++)
		{
			if (list_0[i].byte_0 == playerCommandController_0.Int32_1)
			{
				list_1.Add(list_0[i]);
			}
			else
			{
				list_2.Add(list_0[i]);
			}
		}
		if (bool_1)
		{
			list_1.Sort(FlagBattleCompare);
			list_2.Sort(FlagBattleCompare);
		}
		else if (bool_0)
		{
			list_1.Sort(TeamBattleCompare);
			list_2.Sort(TeamBattleCompare);
		}
		else
		{
			list_1.Sort(DeathMatchCompare);
		}
		EndFightNetworkCommand.IsWinState isWinState_ = EndFightNetworkCommand.IsWinState.Win;
		EndFightNetworkCommand.IsWinState isWinState_2 = EndFightNetworkCommand.IsWinState.Win;
		int int_ = 0;
		if (!bool_1 && !bool_0)
		{
			if (BattleOverRewardCntroller.bool_2)
			{
				int_ = list_1.Count;
			}
		}
		else
		{
			findCommandsWin((byte)playerCommandController_0.Int32_1, playerScoreController_0.Int16_4, playerScoreController_0.Int16_5, out isWinState_, out isWinState_2);
		}
		for (int j = 0; j < list_1.Count; j++)
		{
			BattleOverPlayerData battleOverPlayerDatum = list_1[j];
			int num = j + 1;
			EndFightNetworkCommand.IsWinState isWinState_3 = isWinState_;
			if (MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.DUEL)
			{
				isWinState_3 = ((num == 1) ? EndFightNetworkCommand.IsWinState.Win : EndFightNetworkCommand.IsWinState.Lose);
			}
			getRewardForPlayer(list_1[j], num, isWinState_3, bool_, int_, bool_2);
		}
		for (int k = 0; k < list_2.Count; k++)
		{
			BattleOverPlayerData battleOverPlayerDatum2 = list_2[k];
			int int_2 = k + 1;
			EndFightNetworkCommand.IsWinState isWinState_4 = isWinState_2;
			getRewardForPlayer(list_2[k], int_2, isWinState_4, bool_, int_, bool_2);
		}
		BattleOverWindowParams battleOverWindowParams = new BattleOverWindowParams();
		if (bool_0 || bool_1)
		{
			battleOverWindowParams.int_0 = playerScoreController_0.Int16_4;
			battleOverWindowParams.int_1 = playerScoreController_0.Int16_5;
		}
		battleOverWindowParams.int_2 = playerCommandController_0.Int32_1;
		battleOverWindowParams.isWinState_0 = isWinState_;
		battleOverWindowParams.isWinState_1 = isWinState_2;
		battleOverWindowParams.string_0 = (string)PhotonNetwork.Room_0.Hashtable_0["Pass"];
		battleOverWindowParams.bool_0 = bool_2;
		battleOverWindowParams.list_0 = list_1;
		battleOverWindowParams.list_1 = list_2;
		return battleOverWindowParams;
	}

	private static void getRewardForPlayer(BattleOverPlayerData battleOverPlayerData_0, int int_0, EndFightNetworkCommand.IsWinState isWinState_0, bool bool_3, int int_1, bool bool_4)
	{
		battleOverPlayerData_0.int_0 = int_0;
		bool flag = battleOverPlayerData_0.int_1 == UserController.UserController_0.UserData_0.user_0.int_0;
		int int_2 = battleOverPlayerData_0.int_2;
		if (bool_3 || !bool_4)
		{
			int_2 = 0;
		}
		ModeRewardData rewardAfterMatch = ModesController.ModesController_0.GetRewardAfterMatch(battleOverPlayerData_0.int_0, int_2, isWinState_0, flag, int_1);
		if (rewardAfterMatch == null)
		{
			battleOverPlayerData_0.int_10 = 0;
			battleOverPlayerData_0.int_11 = 0;
		}
		else
		{
			battleOverPlayerData_0.int_10 = ((isWinState_0 != EndFightNetworkCommand.IsWinState.Win) ? rewardAfterMatch.Int32_3 : rewardAfterMatch.Int32_1);
			battleOverPlayerData_0.int_11 = ((isWinState_0 != EndFightNetworkCommand.IsWinState.Win) ? rewardAfterMatch.Int32_4 : rewardAfterMatch.Int32_2);
		}
		if (battleOverPlayerData_0.int_7 > 0)
		{
			battleOverPlayerData_0.float_0 = (float)battleOverPlayerData_0.int_9 * 1f / (float)battleOverPlayerData_0.int_7;
		}
	}

	private static void findCommandsWin(byte byte_0, int int_0, int int_1, out EndFightNetworkCommand.IsWinState isWinState_0, out EndFightNetworkCommand.IsWinState isWinState_1)
	{
		if (byte_0 == 1)
		{
			if (int_0 > int_1)
			{
				isWinState_0 = EndFightNetworkCommand.IsWinState.Win;
				isWinState_1 = EndFightNetworkCommand.IsWinState.Lose;
			}
			else if (int_1 > int_0)
			{
				isWinState_0 = EndFightNetworkCommand.IsWinState.Lose;
				isWinState_1 = EndFightNetworkCommand.IsWinState.Win;
			}
			else
			{
				isWinState_0 = EndFightNetworkCommand.IsWinState.Draw;
				isWinState_1 = EndFightNetworkCommand.IsWinState.Draw;
			}
		}
		else if (int_1 > int_0)
		{
			isWinState_0 = EndFightNetworkCommand.IsWinState.Win;
			isWinState_1 = EndFightNetworkCommand.IsWinState.Lose;
		}
		else if (int_0 > int_1)
		{
			isWinState_0 = EndFightNetworkCommand.IsWinState.Lose;
			isWinState_1 = EndFightNetworkCommand.IsWinState.Win;
		}
		else
		{
			isWinState_0 = EndFightNetworkCommand.IsWinState.Draw;
			isWinState_1 = EndFightNetworkCommand.IsWinState.Draw;
		}
	}

	private static int DeathMatchCompare(BattleOverPlayerData battleOverPlayerData_0, BattleOverPlayerData battleOverPlayerData_1)
	{
		if (battleOverPlayerData_0.int_2 != battleOverPlayerData_1.int_2)
		{
			return battleOverPlayerData_1.int_2.CompareTo(battleOverPlayerData_0.int_2);
		}
		if (battleOverPlayerData_0.int_3 != battleOverPlayerData_1.int_3)
		{
			return battleOverPlayerData_1.int_3.CompareTo(battleOverPlayerData_0.int_3);
		}
		if (battleOverPlayerData_0.int_5 != battleOverPlayerData_1.int_5)
		{
			return battleOverPlayerData_1.int_5.CompareTo(battleOverPlayerData_0.int_5);
		}
		if (battleOverPlayerData_0.int_6 != battleOverPlayerData_1.int_6)
		{
			return battleOverPlayerData_0.int_6.CompareTo(battleOverPlayerData_1.int_6);
		}
		return 0;
	}

	private static int TeamBattleCompare(BattleOverPlayerData battleOverPlayerData_0, BattleOverPlayerData battleOverPlayerData_1)
	{
		if (battleOverPlayerData_0.int_2 != battleOverPlayerData_1.int_2)
		{
			return battleOverPlayerData_1.int_2.CompareTo(battleOverPlayerData_0.int_2);
		}
		if (battleOverPlayerData_0.int_3 != battleOverPlayerData_1.int_3)
		{
			return battleOverPlayerData_1.int_3.CompareTo(battleOverPlayerData_0.int_3);
		}
		if (battleOverPlayerData_0.int_5 != battleOverPlayerData_1.int_5)
		{
			return battleOverPlayerData_1.int_5.CompareTo(battleOverPlayerData_0.int_5);
		}
		if (battleOverPlayerData_0.int_6 != battleOverPlayerData_1.int_6)
		{
			return battleOverPlayerData_0.int_6.CompareTo(battleOverPlayerData_1.int_6);
		}
		return 0;
	}

	private static int FlagBattleCompare(BattleOverPlayerData battleOverPlayerData_0, BattleOverPlayerData battleOverPlayerData_1)
	{
		if (battleOverPlayerData_0.int_2 != battleOverPlayerData_1.int_2)
		{
			return battleOverPlayerData_1.int_2.CompareTo(battleOverPlayerData_0.int_2);
		}
		if (battleOverPlayerData_0.int_4 != battleOverPlayerData_1.int_4)
		{
			return battleOverPlayerData_1.int_4.CompareTo(battleOverPlayerData_0.int_4);
		}
		if (battleOverPlayerData_0.int_3 != battleOverPlayerData_1.int_3)
		{
			return battleOverPlayerData_1.int_3.CompareTo(battleOverPlayerData_0.int_3);
		}
		if (battleOverPlayerData_0.int_6 != battleOverPlayerData_1.int_6)
		{
			return battleOverPlayerData_0.int_6.CompareTo(battleOverPlayerData_1.int_6);
		}
		return 0;
	}
}
