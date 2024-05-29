using System;
using System.Collections.Generic;

public class BattleDeathMatchContainer : BattleModeContainer
{
	public UILabel uilabel_0;

	public UILabel uilabel_1;

	public BattlePlayerTable battlePlayerTable_0;

	private List<BattlePlayerData> list_0 = new List<BattlePlayerData>();

	private int int_0;

	private string string_0 = string.Empty;

	private void Awake()
	{
		uilabel_0.String_0 = string.Empty;
	}

	public override void UpdateData(List<NetworkStartTable> list_1)
	{
		list_0.Clear();
		foreach (NetworkStartTable item in list_1)
		{
			try
			{
				BattlePlayerData battlePlayerData = new BattlePlayerData();
				battlePlayerData.string_0 = item.String_5;
				battlePlayerData.string_1 = item.String_3;
				battlePlayerData.texture_0 = item.Texture_2;
				battlePlayerData.int_1 = ((item.Int32_4 != -1) ? item.Int32_4 : 0);
				battlePlayerData.int_2 = item.Int32_7;
				battlePlayerData.int_3 = item.Int32_6;
				battlePlayerData.int_4 = item.Int32_5;
				battlePlayerData.int_5 = item.Int32_1;
				battlePlayerData.bool_0 = item.Equals(WeaponManager.weaponManager_0.myNetworkStartTable);
				battlePlayerData.bool_1 = item.PhotonView_0.PhotonPlayer_0.Boolean_0;
				if (battlePlayerData.bool_0)
				{
					string_0 = battlePlayerData.string_0;
				}
				list_0.Add(battlePlayerData);
			}
			catch (Exception)
			{
			}
		}
		UpdateDataInternal();
	}

	private void UpdateDataInternal()
	{
		int_0 = 0;
		SortTeam(list_0, ref int_0);
		battlePlayerTable_0.SetData(list_0);
		UpdateTeamInfo();
	}

	private void SortTeam(List<BattlePlayerData> list_1, ref int int_1)
	{
		list_1.Sort(DeathMatchCompare);
		for (int i = 0; i < list_1.Count; i++)
		{
			list_1[i].int_0 = i + 1;
			if (list_1[i].bool_0)
			{
				int_1 = list_1[i].int_2;
			}
		}
	}

	private int DeathMatchCompare(BattlePlayerData battlePlayerData_0, BattlePlayerData battlePlayerData_1)
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
		uilabel_0.String_0 = string_0 + ":";
		uilabel_1.String_0 = int_0.ToString();
	}
}
