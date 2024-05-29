using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleOverPlayerData
{
	public enum ComplaintType
	{
		SPAM = 0,
		THREAT = 1,
		INACTION = 2,
		UNPRINTABLE_NICK = 3,
		WALL_HACK = 1000,
		HEALTH = 1001,
		FAST_RUN = 1002,
		TIME_CHEAT = 1003,
		CLAN_INVITE = 2000
	}

	public int int_0;

	public string string_0;

	public int int_1;

	public string string_1;

	public string string_2;

	public Texture texture_0;

	public int int_2;

	public int int_3;

	public int int_4;

	public int int_5;

	public int int_6;

	public int int_7;

	public int int_8;

	public int int_9;

	public float float_0;

	public float float_1;

	public int int_10;

	public int int_11;

	public bool bool_0;

	public byte byte_0;

	public bool bool_1;

	public Dictionary<int, bool> dictionary_0 = new Dictionary<int, bool>();

	public BattleOverPlayerData()
	{
		foreach (int value in Enum.GetValues(typeof(ComplaintType)))
		{
			dictionary_0.Add(value, false);
		}
	}

	public static BattleOverPlayerData CreateFake(bool bool_2 = false, byte byte_1 = 0)
	{
		BattleOverPlayerData battleOverPlayerData = new BattleOverPlayerData();
		if (bool_2)
		{
			battleOverPlayerData.int_1 = UserController.UserController_0.UserData_0.user_0.int_0;
		}
		else
		{
			battleOverPlayerData.int_1 = UnityEngine.Random.Range(0, 9999999);
		}
		battleOverPlayerData.string_0 = createRandomName();
		battleOverPlayerData.int_2 = UnityEngine.Random.Range(0, 2000);
		battleOverPlayerData.int_3 = UnityEngine.Random.Range(0, 500);
		battleOverPlayerData.int_4 = UnityEngine.Random.Range(0, 10);
		battleOverPlayerData.int_5 = UnityEngine.Random.Range(0, 1000);
		battleOverPlayerData.int_6 = UnityEngine.Random.Range(0, 1000);
		battleOverPlayerData.int_7 = UnityEngine.Random.Range(0, 10000);
		battleOverPlayerData.int_8 = UnityEngine.Random.Range(0, battleOverPlayerData.int_7);
		battleOverPlayerData.int_9 = UnityEngine.Random.Range(0, battleOverPlayerData.int_7);
		battleOverPlayerData.float_1 = UnityEngine.Random.Range(0f, 10000f);
		battleOverPlayerData.byte_0 = byte_1;
		return battleOverPlayerData;
	}

	private static string createRandomName()
	{
		string text = string.Empty;
		int num = UnityEngine.Random.Range(3, 32);
		if (num > 16 && UnityEngine.Random.Range(0, 10) < 7)
		{
			num = UnityEngine.Random.Range(3, 16);
		}
		for (int i = 0; i < num; i++)
		{
			int max = ((i != 0) ? 5 : 2);
			int num2 = 0;
			int num3 = 0;
			if (UnityEngine.Random.Range(0, max) == 0)
			{
				num2 = Convert.ToInt32('A');
				num3 = Convert.ToInt32('Z') + 1;
			}
			else
			{
				num2 = Convert.ToInt32('a');
				num3 = Convert.ToInt32('z') + 1;
			}
			max = UnityEngine.Random.Range(num2, num3);
			text = string.Format("{0}{1}", text, char.ConvertFromUtf32(max));
		}
		return text;
	}
}
