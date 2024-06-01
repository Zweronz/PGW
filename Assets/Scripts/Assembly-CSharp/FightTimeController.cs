using UnityEngine;
using engine.helpers;
using engine.network;
using engine.unity;

public class FightTimeController
{
	private double double_0;

	private float float_0 = 5f;

	private float float_1;

	public double Double_0
	{
		get
		{
			return double_0 - Utility.Double_0;
		}
		private set
		{
			double_0 = value;
		}
	}

	public bool Boolean_0
	{
		get
		{
			if (Defs.bool_6 && Double_0 <= 0.0)
			{
				PlayerScoreController playerScoreController_ = WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0;
				return playerScoreController_.Int16_4 == playerScoreController_.Int16_5;
			}
			return false;
		}
	}

	public void SetFakeTimeEndFight()
	{
		Double_0 = Utility.Double_0 + (double)(MonoSingleton<FightController>.Prop_0.Int32_0 * 60);
	}

	public void Update()
	{
		FightController prop_ = MonoSingleton<FightController>.Prop_0;
		bool flag = prop_.NetworkStateMode_0 == FightController.NetworkStateMode.Offline;
		if (prop_.ConnectionStatus_0 == FightController.ConnectionStatus.InBattle && !flag)
		{
			float_1 += Time.deltaTime;
			if (float_1 >= float_0)
			{
				float_1 = 0f;
				SendPingCommand();
			}
		}
	}

	public void SendPingCommand(bool bool_0 = false)
	{
		try
		{
			FightPingNetworkCommand fightPingNetworkCommand = new FightPingNetworkCommand();
			fightPingNetworkCommand.string_0 = MonoSingleton<FightController>.Prop_0.String_1;
			fightPingNetworkCommand.int_1 = (bool_0 ? 1 : 0);
			AbstractNetworkCommand.Send(fightPingNetworkCommand);
		} catch {
			
		}
	}

	public void SetTimeToEnd(double double_1)
	{
		Double_0 = double_1;
		if (double_1 > 0.0)
		{
			if (Double_0 < 10.0)
			{
				float_0 = 1f;
			}
			else
			{
				float_0 = 5f;
			}
		}
		else if (WeaponManager.weaponManager_0 != null && !Boolean_0)
		{
			if (WeaponManager.weaponManager_0.myPlayerMoveC != null)
			{
				WeaponManager.weaponManager_0.myPlayerMoveC.PlayerBattleOver();
			}
			TimeBattleOver();
		}
	}

	public void TimeBattleOver()
	{
		NetworkStartTable myNetworkStartTable = WeaponManager.weaponManager_0.myNetworkStartTable;
		if (myNetworkStartTable == null)
		{
			Log.AddLineError("[PlayerMoveC::BattleOver. ERROR End battle impossibel. My NetworkStartTable == null!!!]");
			return;
		}
		bool boolean_ = myNetworkStartTable.Boolean_7;
		bool boolean_2 = myNetworkStartTable.Boolean_5;
		bool boolean_3 = myNetworkStartTable.Boolean_8;
		PlayerScoreController myScoreController = WeaponManager.weaponManager_0.myScoreController;
		if (boolean_)
		{
			int int_ = 0;
			if (myScoreController.Int16_4 > myScoreController.Int16_5)
			{
				int_ = 1;
			}
			if (myScoreController.Int16_5 > myScoreController.Int16_4)
			{
				int_ = 2;
			}
			myNetworkStartTable.BattleOver(int_);
		}
		else if (boolean_2)
		{
			ZombiManager.zombiManager_0.EndMatch();
		}
		else if (boolean_3)
		{
			int int16_ = myScoreController.Int16_4;
			int int16_2 = myScoreController.Int16_5;
			if (int16_ > int16_2)
			{
				myNetworkStartTable.BattleOver(1);
			}
			else
			{
				myNetworkStartTable.BattleOver(2);
			}
		}
		else
		{
			myNetworkStartTable.BattleOver();
		}
	}
}
