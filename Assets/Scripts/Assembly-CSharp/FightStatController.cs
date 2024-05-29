using System.Collections.Generic;
using UnityEngine;
using engine.helpers;
using engine.network;
using engine.unity;

public sealed class FightStatController
{
	private int int_0 = 60;

	private float float_0;

	private Dictionary<int, BattleStatData> dictionary_0 = new Dictionary<int, BattleStatData>();

	public Dictionary<int, BattleStatData> Dictionary_0
	{
		get
		{
			return dictionary_0;
		}
	}

	public void Update()
	{
		SendStats();
		UpdateFightLength();
	}

	public void Clear()
	{
		dictionary_0.Clear();
	}

	public override string ToString()
	{
		string text = string.Empty;
		foreach (KeyValuePair<int, BattleStatData> item in dictionary_0)
		{
			text += string.Format("UID: {0}\n{1}\n", item.Key, item.Value.ToString());
		}
		return text;
	}

	private void SendStats()
	{
		if (MonoSingleton<FightController>.Prop_0.ConnectionStatus_0 == FightController.ConnectionStatus.InBattle && dictionary_0 != null && dictionary_0.Count != 0)
		{
			float_0 += Time.deltaTime;
			if (float_0 >= (float)int_0)
			{
				float_0 = 0f;
				BattleStatNetworkCommand battleStatNetworkCommand = new BattleStatNetworkCommand();
				battleStatNetworkCommand.string_0 = MonoSingleton<FightController>.Prop_0.String_1;
				battleStatNetworkCommand.dictionary_0 = dictionary_0;
				AbstractNetworkCommand.Send(battleStatNetworkCommand);
			}
		}
	}

	public bool GetFirstBood()
	{
		int num = 0;
		foreach (KeyValuePair<int, BattleStatData> item in dictionary_0)
		{
			num += item.Value.Int32_0;
		}
		return num == 1;
	}

	public void OnShoot(int int_1, int int_2)
	{
		InitStat(int_1);
		dictionary_0[int_1].OnShoot(int_2);
	}

	public void OnHit(int int_1, int int_2, bool bool_0, float float_1)
	{
		InitStat(int_1);
		dictionary_0[int_1].OnHit(int_2, float_1);
		if (bool_0)
		{
			dictionary_0[int_1].OnHeadshot(int_2);
		}
	}

	public void OnKill(int int_1, int int_2)
	{
		InitStat(int_1);
		dictionary_0[int_1].OnKill(int_2);
	}

	public void OnKillAssists(int int_1)
	{
		InitStat(int_1);
		dictionary_0[int_1].OnKillAssists();
	}

	public void OnGrenadeKill(int int_1)
	{
		InitStat(int_1);
		dictionary_0[int_1].OnGrenadeKill();
	}

	public void OnMechKill(int int_1)
	{
		InitStat(int_1);
		dictionary_0[int_1].OnMechKill();
	}

	public void OnTurretKill(int int_1)
	{
		InitStat(int_1);
		dictionary_0[int_1].OnTurretKill();
	}

	public void OnUseConsumable(int int_1, SlotType slotType_0)
	{
		if (!ArtikulData.IsConsumable(slotType_0))
		{
			return;
		}
		InitStat(int_1);
		dictionary_0[int_1].OnConsumableUsed(slotType_0);
		if (UserController.UserController_0.UserData_0.user_0.int_0 == int_1)
		{
			int artikulIdFromSlot = UserController.UserController_0.GetArtikulIdFromSlot(slotType_0);
			if (artikulIdFromSlot != 0)
			{
				UseArtikulNetworkCommand useArtikulNetworkCommand = new UseArtikulNetworkCommand();
				useArtikulNetworkCommand.int_1 = artikulIdFromSlot;
				AbstractNetworkCommand.Send(useArtikulNetworkCommand);
			}
		}
	}

	public void FixMaxKills(int int_1)
	{
		InitStat(int_1);
		dictionary_0[int_1].OnFixMaxKills();
	}

	public void OnDeath(int int_1, int int_2)
	{
		InitStat(int_1);
		dictionary_0[int_1].OnDeath(int_2);
	}

	public void OnDamage(int int_1, float float_1)
	{
		InitStat(int_1);
		dictionary_0[int_1].OnDamage(float_1);
	}

	public void OnPickedUpAmmo(int int_1, int int_2, int int_3)
	{
		InitStat(int_1);
		dictionary_0[int_1].OnAmmoPickup(int_2, int_3);
	}

	public void OnPickedUpGrenade(int int_1, int int_2)
	{
		InitStat(int_1);
		dictionary_0[int_1].OnGrenadePickup(int_2);
		if (UserController.UserController_0.UserData_0.user_0.int_0 == int_1)
		{
			PickedupArtikulNetworkCommand pickedupArtikulNetworkCommand = new PickedupArtikulNetworkCommand();
			pickedupArtikulNetworkCommand.int_1 = UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_CONSUM_GRENADE);
			AbstractNetworkCommand.Send(pickedupArtikulNetworkCommand);
		}
	}

	public void OnPickedUpHealth(int int_1, float float_1)
	{
		InitStat(int_1);
		dictionary_0[int_1].OnHealthPickup(float_1);
	}

	public void OnPickedUpArmor(int int_1, int int_2)
	{
		InitStat(int_1);
		dictionary_0[int_1].OnArmorPickup(int_2);
	}

	public void OnPickedUpExplosion(int int_1, int int_2)
	{
		InitStat(int_1);
		dictionary_0[int_1].OnExplosionPickup(int_2);
	}

	public void OnFlagCaptured(int int_1)
	{
		InitStat(int_1);
		dictionary_0[int_1].OnFlagCaptured();
	}

	private void InitStat(int int_1)
	{
		if (!dictionary_0.ContainsKey(int_1))
		{
			BattleStatData battleStatData = new BattleStatData();
			battleStatData.double_1 = Utility.Double_0;
			if (int_1 == UserController.UserController_0.UserData_0.user_0.int_0)
			{
				battleStatData.int_7 = UserController.UserController_0.UserData_0.user_0.int_2;
			}
			dictionary_0.Add(int_1, battleStatData);
		}
	}

	private void UpdateFightLength()
	{
		double double_ = Utility.Double_0;
		foreach (KeyValuePair<int, BattleStatData> item in dictionary_0)
		{
			item.Value.double_0 = double_ - item.Value.double_1;
		}
	}
}
