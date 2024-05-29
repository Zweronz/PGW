using System.Collections.Generic;
using ProtoBuf;

[ProtoContract]
public sealed class BattleStatData
{
	[ProtoMember(1)]
	public Dictionary<int, int> dictionary_0 = new Dictionary<int, int>();

	[ProtoMember(2)]
	public Dictionary<int, int> dictionary_1 = new Dictionary<int, int>();

	[ProtoMember(3)]
	public Dictionary<int, int> dictionary_2 = new Dictionary<int, int>();

	[ProtoMember(4)]
	public Dictionary<int, float> dictionary_3 = new Dictionary<int, float>();

	[ProtoMember(5)]
	public Dictionary<SlotType, int> dictionary_4 = new Dictionary<SlotType, int>();

	[ProtoMember(6)]
	public Dictionary<int, int> dictionary_5 = new Dictionary<int, int>();

	[ProtoMember(7)]
	public int int_0;

	[ProtoMember(8)]
	public int int_1;

	[ProtoMember(9)]
	public int int_2;

	[ProtoMember(10)]
	public float float_0;

	[ProtoMember(11)]
	public int int_3;

	[ProtoMember(12)]
	public Dictionary<int, int> dictionary_6 = new Dictionary<int, int>();

	[ProtoMember(13)]
	public int int_4;

	[ProtoMember(14)]
	public float float_1;

	[ProtoMember(15)]
	public int int_5;

	[ProtoMember(16)]
	public double double_0;

	[ProtoMember(17)]
	public int int_6;

	[ProtoMember(18)]
	public int int_7;

	[ProtoMember(19)]
	public int int_8;

	[ProtoMember(20)]
	public int int_9;

	[ProtoMember(21)]
	public int int_10;

	[ProtoMember(22)]
	public Dictionary<int, int> dictionary_7 = new Dictionary<int, int>();

	public double double_1;

	private int int_11;

	private int int_12;

	public int Int32_0
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<int, int> item in dictionary_5)
			{
				num += item.Value;
			}
			num += int_3;
			return num + int_1;
		}
	}

	public void OnShoot(int int_13)
	{
		if (!dictionary_0.ContainsKey(int_13))
		{
			dictionary_0.Add(int_13, 0);
		}
		Dictionary<int, int> dictionary;
		Dictionary<int, int> dictionary2 = (dictionary = dictionary_0);
		int key;
		int key2 = (key = int_13);
		key = dictionary[key];
		key = (dictionary2[key2] = key + 1);
		int_12 = key;
	}

	public void OnHit(int int_13, float float_2)
	{
		int key;
		if (int_12 != 0)
		{
			int_12 = 0;
			if (!dictionary_1.ContainsKey(int_13))
			{
				dictionary_1.Add(int_13, 0);
			}
			Dictionary<int, int> dictionary;
			Dictionary<int, int> dictionary2 = (dictionary = dictionary_1);
			int key2 = (key = int_13);
			key = dictionary[key];
			dictionary2[key2] = key + 1;
		}
		if (!dictionary_3.ContainsKey(int_13))
		{
			dictionary_3.Add(int_13, 0f);
		}
		Dictionary<int, float> dictionary3;
		Dictionary<int, float> dictionary4 = (dictionary3 = dictionary_3);
		int key3 = (key = int_13);
		float num = dictionary3[key];
		dictionary4[key3] = num + float_2;
	}

	public void OnHeadshot(int int_13)
	{
		if (!dictionary_2.ContainsKey(int_13))
		{
			dictionary_2.Add(int_13, 0);
		}
		Dictionary<int, int> dictionary;
		Dictionary<int, int> dictionary2 = (dictionary = dictionary_2);
		int key;
		int key2 = (key = int_13);
		key = dictionary[key];
		dictionary2[key2] = key + 1;
	}

	public void OnConsumableUsed(SlotType slotType_0)
	{
		if (!dictionary_4.ContainsKey(slotType_0))
		{
			dictionary_4.Add(slotType_0, 0);
		}
		Dictionary<SlotType, int> dictionary;
		Dictionary<SlotType, int> dictionary2 = (dictionary = dictionary_4);
		SlotType key;
		SlotType key2 = (key = slotType_0);
		int num = dictionary[key];
		dictionary2[key2] = num + 1;
	}

	public void OnKill(int int_13)
	{
		if (!dictionary_5.ContainsKey(int_13))
		{
			dictionary_5.Add(int_13, 0);
		}
		Dictionary<int, int> dictionary;
		Dictionary<int, int> dictionary2 = (dictionary = dictionary_5);
		int key;
		int key2 = (key = int_13);
		key = dictionary[key];
		dictionary2[key2] = key + 1;
		int_11++;
	}

	public void OnKillAssists()
	{
		int_6++;
	}

	public void OnMechKill()
	{
		int_1++;
	}

	public void OnGrenadeKill()
	{
		int_0++;
	}

	public void OnTurretKill()
	{
		int_3++;
	}

	public void OnDeath(int int_13)
	{
		int_2++;
		OnFixMaxKills();
		if (!dictionary_7.ContainsKey(int_13))
		{
			dictionary_7.Add(int_13, 1);
			return;
		}
		Dictionary<int, int> dictionary;
		Dictionary<int, int> dictionary2 = (dictionary = dictionary_7);
		int key;
		int key2 = (key = int_13);
		key = dictionary[key];
		dictionary2[key2] = key + 1;
	}

	public void OnFixMaxKills()
	{
		if (int_11 >= int_9)
		{
			int_9 = int_11;
		}
		int_11 = 0;
	}

	public void OnDamage(float float_2)
	{
		float_0 += float_2;
	}

	public void OnAmmoPickup(int int_13, int int_14)
	{
		if (!dictionary_6.ContainsKey(int_13))
		{
			dictionary_6.Add(int_13, 0);
		}
		Dictionary<int, int> dictionary;
		Dictionary<int, int> dictionary2 = (dictionary = dictionary_6);
		int key;
		int key2 = (key = int_13);
		key = dictionary[key];
		dictionary2[key2] = key + int_14;
	}

	public void OnArmorPickup(int int_13)
	{
		int_4 += int_13;
	}

	public void OnExplosionPickup(int int_13)
	{
		int_10 += int_13;
	}

	public void OnHealthPickup(float float_2)
	{
		float_1 += float_2;
	}

	public void OnGrenadePickup(int int_13)
	{
		int_5 += int_13;
	}

	public void OnFlagCaptured()
	{
		int_8++;
	}

	public override string ToString()
	{
		string text = "\n==== Weapon shoots ====\n";
		foreach (KeyValuePair<int, int> item in dictionary_0)
		{
			string text2 = text;
			text = text2 + "wid: " + item.Key + " cnt: " + item.Value + "\n";
		}
		text += "========\n";
		text += "\n==== Weapon hits ====\n";
		foreach (KeyValuePair<int, int> item2 in dictionary_1)
		{
			string text2 = text;
			text = text2 + "wid: " + item2.Key + " cnt: " + item2.Value + "\n";
		}
		text += "========\n";
		text += "\n==== Weapon headshots ====\n";
		foreach (KeyValuePair<int, int> item3 in dictionary_2)
		{
			string text2 = text;
			text = text2 + "wid: " + item3.Key + " cnt: " + item3.Value + "\n";
		}
		text += "========\n";
		text += "\n==== Weapon damage ====\n";
		foreach (KeyValuePair<int, float> item4 in dictionary_3)
		{
			string text2 = text;
			text = text2 + "wid: " + item4.Key + " cnt: " + item4.Value + "\n";
		}
		text += "========\n";
		text += "\n==== Consumable using ====\n";
		foreach (KeyValuePair<SlotType, int> item5 in dictionary_4)
		{
			string text2 = text;
			text = string.Concat(text2, "wid: ", item5.Key, " cnt: ", item5.Value, "\n");
		}
		text += "========\n";
		text += "\n==== Kills ====\n";
		foreach (KeyValuePair<int, int> item6 in dictionary_5)
		{
			string text2 = text;
			text = text2 + "wid: " + item6.Key + " cnt: " + item6.Value + "\n";
		}
		text += "========\n";
		text += "\n==== DeathWithWeapon ====\n";
		foreach (KeyValuePair<int, int> item7 in dictionary_7)
		{
			string text2 = text;
			text = text2 + "wid: " + item7.Key + " cnt: " + item7.Value + "\n";
		}
		text += "========\n";
		text += "\n==== Grenade kills ====\n";
		text = text + "cnt: " + int_0 + "\n";
		text += "========\n";
		text += "\n==== Mech kills ====\n";
		text = text + "cnt: " + int_1 + "\n";
		text += "========\n";
		text += "\n==== Turret kills ====\n";
		text = text + "cnt: " + int_3 + "\n";
		text += "========\n";
		text += "\n==== Deaths ====\n";
		text = text + "cnt: " + int_2 + "\n";
		text += "========\n";
		text += "\n==== Damage ====\n";
		text = text + "cnt: " + float_0 + "\n";
		text += "========\n";
		text += "\n==== Ammo pickups ====\n";
		foreach (KeyValuePair<int, int> item8 in dictionary_6)
		{
			string text2 = text;
			text = text2 + "wid: " + item8.Key + " cnt: " + item8.Value + "\n";
		}
		text += "========\n";
		text += "\n==== Grenade Pickups ====\n";
		text = text + "cnt: " + int_5 + "\n";
		text += "========\n";
		text += "\n==== Health Pickups ====\n";
		text = text + "cnt: " + float_1 + "\n";
		text += "========\n";
		text += "\n==== Armor Pickups ====\n";
		text = text + "cnt: " + int_4 + "\n";
		text += "========\n";
		text += "\n==== Fight Length ====\n";
		text = text + "cnt: " + double_0 + "\n";
		return text + "========\n";
	}
}
