using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class StoreKitEventListener
{
	internal sealed class StoreKitEventListenerState
	{
		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private IDictionary<string, string> idictionary_0;

		public string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			set
			{
				string_0 = value;
			}
		}

		public string String_1
		{
			[CompilerGenerated]
			get
			{
				return string_1;
			}
			[CompilerGenerated]
			set
			{
				string_1 = value;
			}
		}

		public IDictionary<string, string> IDictionary_0
		{
			[CompilerGenerated]
			get
			{
				return idictionary_0;
			}
			[CompilerGenerated]
			private set
			{
				idictionary_0 = value;
			}
		}

		public StoreKitEventListenerState()
		{
			String_0 = string.Empty;
			String_1 = string.Empty;
			IDictionary_0 = new Dictionary<string, string>();
		}
	}

	public const string string_0 = "bigammopack";

	public const string string_1 = "crystalsword";

	public const string string_2 = "Fullhealth";

	public const string string_3 = "MinerWeapon";

	public static bool bool_0;

	public static GameObject gameObject_0;

	public static string string_4;

	public static string string_5;

	public static string string_6;

	public static string string_7;

	public static string string_8;

	public static string string_9;

	public static string string_10;

	public static string string_11;

	public static string string_12;

	public static string string_13;

	public static string string_14;

	public static string string_15;

	public static string string_16;

	public static string string_17;

	public static string string_18;

	public static string string_19;

	public static string string_20;

	public static string string_21;

	public static string string_22;

	public static string string_23;

	public static string string_24;

	public static string string_25;

	public static string string_26;

	public static readonly string[] string_27;

	public static readonly string[] string_28;

	public static readonly string[] string_29;

	public static readonly string[] string_30;

	public static readonly string[][] string_31;

	public static readonly string[][] string_32;

	private static readonly StoreKitEventListenerState storeKitEventListenerState_0;

	internal static StoreKitEventListenerState StoreKitEventListenerState_0
	{
		get
		{
			return storeKitEventListenerState_0;
		}
	}

	static StoreKitEventListener()
	{
		bool_0 = false;
		string_4 = "captainskin";
		string_5 = "hawkskin";
		string_6 = "greenguyskin";
		string_7 = "thundergodskin";
		string_8 = "gordonskin";
		string_9 = "animeGirl";
		string_10 = "EMOGirl";
		string_11 = "Nurse";
		string_12 = "magicGirl";
		string_13 = "braveGirl";
		string_14 = "glamDoll";
		string_15 = "kittyGirl";
		string_16 = "famosBoy";
		string_17 = "skin810_1";
		string_18 = "skin810_2";
		string_19 = "skin810_3";
		string_20 = "skin810_4";
		string_21 = "skin810_5";
		string_22 = "skin810_6";
		string_23 = "extendedversion";
		string_24 = "armor";
		string_25 = "armor2";
		string_26 = "armor3";
		storeKitEventListenerState_0 = new StoreKitEventListenerState();
		string_27 = new string[13]
		{
			string_4, string_5, string_6, string_7, string_8, string_9, string_10, string_11, string_12, string_13,
			string_14, string_15, string_16
		};
		List<string> list = new List<string>();
		string[] array = string_27;
		foreach (string item in array)
		{
			list.Add(item);
		}
		int j;
		for (j = 0; j < 11; j++)
		{
			list.Add("newskin_" + j);
		}
		for (; j < 19; j++)
		{
			list.Add("newskin_" + j);
		}
		list.Add(string_17);
		list.Add(string_18);
		list.Add(string_19);
		list.Add(string_20);
		list.Add(string_21);
		list.Add(string_22);
		string_27 = list.ToArray();
		string_28 = new string[10] { "bigammopack", "Fullhealth", "ironSword", "MinerWeapon", "steelAxe", "spas", "glock", "chainsaw", "scythe", "shovel" };
		string_29 = new string[10]
		{
			string_28[2],
			string_28[3],
			"steelAxe",
			"woodenBow",
			"combatrifle",
			"spas",
			"goldeneagle",
			string_28[7],
			string_28[8],
			"famas"
		};
		string_30 = new string[1] { string_23 };
		string_32 = new string[2][]
		{
			new string[5]
			{
				string_28[0],
				string_28[1],
				string_24,
				string_25,
				string_26
			},
			new string[0]
		};
		string_31 = string_32;
	}
}
