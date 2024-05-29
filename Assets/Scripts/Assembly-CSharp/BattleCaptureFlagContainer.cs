using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleCaptureFlagContainer : BattleModeContainer
{
	public UILabel uilabel_0;

	public UILabel uilabel_1;

	public UISprite uisprite_0;

	public BattlePlayerTable battlePlayerTable_0;

	public UILabel uilabel_2;

	public UILabel uilabel_3;

	public UISprite uisprite_1;

	public BattlePlayerTable battlePlayerTable_1;

	private List<BattlePlayerData> list_0 = new List<BattlePlayerData>();

	private List<BattlePlayerData> list_1 = new List<BattlePlayerData>();

	private int int_0;

	private int int_1;

	private TypeCommand typeCommand_0;

	public override void UpdateData(List<NetworkStartTable> list_2)
	{
		list_0.Clear();
		list_1.Clear();
		List<BattlePlayerData> list = new List<BattlePlayerData>();
		List<BattlePlayerData> list2 = new List<BattlePlayerData>();
		foreach (NetworkStartTable item in list_2)
		{
			try
			{
				BattlePlayerData battlePlayerData = new BattlePlayerData();
				battlePlayerData.string_0 = item.String_5;
				battlePlayerData.string_1 = item.String_3;
				battlePlayerData.texture_0 = item.Texture_2;
				battlePlayerData.int_1 = ((item.Int32_4 != -1) ? item.Int32_4 : 0);
				battlePlayerData.int_2 = item.Int32_8;
				battlePlayerData.int_3 = item.Int32_7;
				battlePlayerData.int_4 = item.Int32_5;
				battlePlayerData.int_5 = item.Int32_1;
				battlePlayerData.bool_0 = item.Equals(WeaponManager.weaponManager_0.myNetworkStartTable);
				battlePlayerData.bool_1 = item.PhotonView_0.PhotonPlayer_0.Boolean_0;
				if (battlePlayerData.bool_0)
				{
					typeCommand_0 = item.PlayerCommandController_0.TypeCommand_1;
				}
				if (item.PlayerCommandController_0.TypeCommand_1 == TypeCommand.Diggers)
				{
					list.Add(battlePlayerData);
				}
				else if (item.PlayerCommandController_0.TypeCommand_1 == TypeCommand.Kritters)
				{
					list2.Add(battlePlayerData);
				}
			}
			catch (Exception)
			{
			}
		}
		bool flag = typeCommand_0 == TypeCommand.Diggers;
		list_0 = ((!flag) ? list2 : list);
		list_1 = ((!flag) ? list : list2);
		UpdateDataInternal();
	}

	private void UpdateDataInternal()
	{
		SortTeam(list_0);
		SortTeam(list_1);
		battlePlayerTable_0.SetData(list_0);
		battlePlayerTable_1.SetData(list_1);
		UpdateTeamInfo();
	}

	private void SortTeam(List<BattlePlayerData> list_2)
	{
		list_2.Sort(FlagBattleCompare);
		for (int i = 0; i < list_2.Count; i++)
		{
			list_2[i].int_0 = i + 1;
		}
		int num = 0;
		int num2 = 0;
		if (WeaponManager.weaponManager_0.myPlayerMoveC != null)
		{
			num = WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0.Int16_4;
			num2 = WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0.Int16_5;
		}
		bool flag = typeCommand_0 == TypeCommand.Diggers;
		int_0 = ((!flag) ? num2 : num);
		int_1 = ((!flag) ? num : num2);
	}

	private int FlagBattleCompare(BattlePlayerData battlePlayerData_0, BattlePlayerData battlePlayerData_1)
	{
		if (battlePlayerData_0.int_1 != battlePlayerData_1.int_1)
		{
			return battlePlayerData_1.int_1.CompareTo(battlePlayerData_0.int_1);
		}
		if (battlePlayerData_0.int_2 != battlePlayerData_1.int_2)
		{
			return battlePlayerData_1.int_2.CompareTo(battlePlayerData_0.int_2);
		}
		if (battlePlayerData_0.int_3 != battlePlayerData_1.int_3)
		{
			return battlePlayerData_1.int_3.CompareTo(battlePlayerData_0.int_3);
		}
		if (battlePlayerData_0.int_4 != battlePlayerData_1.int_4)
		{
			return battlePlayerData_0.int_4.CompareTo(battlePlayerData_1.int_4);
		}
		return 0;
	}

	private void UpdateTeamInfo()
	{
		if (int_0 == int_1)
		{
			NGUITools.SetActive(uisprite_0.gameObject, false);
			NGUITools.SetActive(uisprite_1.gameObject, false);
		}
		else
		{
			NGUITools.SetActive(uisprite_0.gameObject, int_0 > int_1);
			NGUITools.SetActive(uisprite_1.gameObject, int_0 < int_1);
		}
		uilabel_1.String_0 = int_0.ToString();
		uilabel_3.String_0 = int_1.ToString();
		uilabel_0.Color_0 = ((int_0 <= int_1) ? Color.white : Color.yellow);
		uilabel_1.Color_0 = ((int_0 <= int_1) ? Color.white : Color.yellow);
		uilabel_2.Color_0 = ((int_0 >= int_1) ? Color.white : Color.yellow);
		uilabel_3.Color_0 = ((int_0 >= int_1) ? Color.white : Color.yellow);
	}
}
