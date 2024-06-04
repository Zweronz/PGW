using System.Collections.Generic;
using UnityEngine;
using engine.controllers;
using engine.helpers;

public static class Defs
{
	public enum GameSecondFireButtonMode
	{
		Sniper = 0,
		On = 1,
		Off = 2
	}

	public enum RuntimeAndroidEdition
	{
		None = 0,
		Amazon = 1,
		GoogleLite = 2,
		GooglePro = 3
	}

	public const string string_0 = "Last Payment Time";

	private static int int_0;

	private static int int_1;

	public static bool bool_0;

	public static GameSecondFireButtonMode gameSecondFireButtonMode_0;

	public static int int_2;

	public static int int_3;

	public static int int_4;

	public static int int_5;

	public static int int_6;

	public static int int_7;

	public static int int_8;

	public static int int_9;

	public static int int_10;

	public static int int_11;

	public static int int_12;

	public static int int_13;

	public static int int_14;

	public static int int_15;

	public static int int_16;

	public static int int_17;

	public static string[] string_1;

	public static int int_18;

	private static bool bool_1;

	public static bool bool_2;

	public static bool bool_3;

	public static bool bool_4;

	public static bool bool_5;

	public static bool bool_6;

	public static Dictionary<string, string> dictionary_0;

	private static bool bool_7;

	public static Dictionary<string, int> dictionary_1;

	public static List<int> list_0;

	public static bool bool_8;

	public static readonly string string_2;

	public static string string_3;

	public static int int_19;

	public static bool bool_9;

	public static bool bool_10;

	private static bool? nullable_0;

	public static bool bool_11;

	public static int Int32_0
	{
		get
		{
			if (int_0 == 0)
			{
				int_0 = -5 & ~(1 << LayerMask.NameToLayer("Player"));
			}
			return int_0;
		}
	}

	public static int Int32_1
	{
		get
		{
			if (int_1 == 0)
			{
				int_1 = -5 & ~(1 << LayerMask.NameToLayer("Player"));
				int_1 &= ~(1 << (LayerMask.NameToLayer("TurretSphereCollider") & 0x1F));
			}
			return int_1;
		}
	}

	public static string String_0
	{
		get
		{
			return "SoundMusicSetting";
		}
	}

	public static string String_1
	{
		get
		{
			return "SoundMusicSetting";
		}
	}

	public static string String_2
	{
		get
		{
			return "SoundFXSetting";
		}
	}

	public static string String_3
	{
		get
		{
			return "_Inner";
		}
	}

	public static bool Boolean_0
	{
		get
		{
			return bool_7 && AppStateController.AppStateController_0.bool_0;
		}
		set
		{
			bool_7 = value;
		}
	}

	public static string String_4
	{
		get
		{
			return "LeftHandedSN";
		}
	}

	public static string String_5
	{
		get
		{
			return "ArtLevsS";
		}
	}

	public static string String_6
	{
		get
		{
			return "ArtBoxS";
		}
	}

	public static string String_7
	{
		get
		{
			return "SurvivalScoreSett";
		}
	}

	public static string String_8
	{
		get
		{
			return "LevelsWhereGetCoinS";
		}
	}

	public static float Single_0
	{
		get
		{
			return (float)Screen.height / 768f;
		}
	}

	public static float Single_1
	{
		get
		{
			return (float)Screen.height / 768f * 0.5f;
		}
	}

	public static string String_9
	{
		get
		{
			return "Multiplayer Skins";
		}
	}

	public static string String_10
	{
		get
		{
			return "Player";
		}
	}

	public static string String_11
	{
		get
		{
			return "Main_Menu_Scene";
		}
	}

	public static string String_12
	{
		get
		{
			return "DifficultySett";
		}
	}

	public static bool Boolean_1
	{
		get
		{
			if (!nullable_0.HasValue)
			{
				nullable_0 = Storager.GetInt("ChatOn", 1) == 1;
			}
			return nullable_0.Value;
		}
		set
		{
			nullable_0 = value;
			Storager.SetInt("ChatOn", value ? 1 : 0);
		}
	}

	public static RuntimeAndroidEdition RuntimeAndroidEdition_0
	{
		get
		{
			return RuntimeAndroidEdition.None;
		}
	}

	static Defs()
	{
		int_0 = 0;
		int_1 = 0;
		gameSecondFireButtonMode_0 = GameSecondFireButtonMode.Sniper;
		int_2 = -176;
		int_3 = 431;
		int_4 = -72;
		int_5 = 340;
		int_6 = -95;
		int_7 = 79;
		int_8 = -250;
		int_9 = 150;
		int_10 = 172;
		int_11 = 160;
		int_12 = -46;
		int_13 = 445;
		int_14 = 160;
		int_15 = 337;
		int_16 = 16;
		int_17 = 16;
		string_1 = new string[8] { "Arena_Swamp", "Arena_Underwater", "Coliseum", "Arena_Castle", "Arena_Space", "Arena_Hockey", "Arena_Mine", "Pizza" };
		int_18 = -1;
		bool_1 = false;
		dictionary_0 = new Dictionary<string, string>();
		bool_7 = false;
		dictionary_1 = new Dictionary<string, int>();
		list_0 = new List<int>();
		bool_8 = false;
		string_2 = "InvertCamSN";
		string_3 = "PromScene";
		int_19 = 2;
		bool_9 = false;
		bool_10 = false;
		bool_11 = false;
		dictionary_1.Add("Maze", 2);
		dictionary_1.Add("Cementery", 1);
		dictionary_1.Add("City", 3);
		dictionary_1.Add("Gluk", 6);
		dictionary_1.Add("Jail", 5);
		dictionary_1.Add("Hospital", 4);
		dictionary_1.Add("Pool", 1001);
		dictionary_1.Add("Slender", 9);
		dictionary_1.Add("Castle", 1002);
		dictionary_1.Add("Ranch", 1003);
		dictionary_1.Add("Arena_MP", 1004);
		dictionary_1.Add("Sky_islands", 1005);
		dictionary_1.Add("Dust", 1006);
		dictionary_1.Add("Bridge", 1007);
		dictionary_1.Add("Assault", 1008);
		dictionary_1.Add("Farm", 4001);
		dictionary_1.Add("Utopia", 4002);
		dictionary_1.Add("Arena", 7);
		dictionary_1.Add("Winter", 4003);
		dictionary_1.Add("Aztec", 4005);
		dictionary_1.Add("School", 1009);
		dictionary_1.Add("Parkour", 1010);
		dictionary_1.Add("Coliseum_MP", 1011);
		dictionary_1.Add("Hungry", 1012);
		dictionary_1.Add("Hungry_Night", 1013);
		dictionary_1.Add("Hungry_2", 1014);
		dictionary_1.Add("Estate", 1020);
		dictionary_1.Add("Space", 1022);
		dictionary_1.Add("Portal", 1023);
		dictionary_1.Add("Two_Castles", 1024);
		dictionary_1.Add("Ships", 1025);
		dictionary_1.Add("Ships_Night", 1026);
		dictionary_1.Add("Gluk_3", 1027);
		dictionary_1.Add("Matrix", 1028);
		dictionary_1.Add("Ants", 1029);
		dictionary_1.Add("Hill", 1030);
		dictionary_1.Add("Heaven", 1031);
		dictionary_1.Add("Underwater", 1032);
		dictionary_1.Add("Knife", 1033);
		dictionary_1.Add("Day_D", 1034);
		dictionary_1.Add("NuclearCity", 1035);
		dictionary_1.Add("Cube", 1036);
		dictionary_1.Add("Train", 1037);
		dictionary_1.Add("Sniper", 1038);
		dictionary_1.Add("Supermarket", 1039);
		dictionary_1.Add("Pumpkins", 1040);
		dictionary_1.Add("Christmas_Town", 1041);
		dictionary_1.Add("Christmas_Town_Night", 1042);
		dictionary_1.Add("Paradise", 1043);
		dictionary_1.Add("Bota", 1044);
		dictionary_1.Add("Pizza", 1045);
		dictionary_1.Add("Barge", 1046);
		dictionary_1.Add("Mine", 1047);
		dictionary_1.Add("Area52Labs", 1048);
		dictionary_1.Add("PiratIsland", 1049);
		dictionary_1.Add("Tatuan", 1050);
		dictionary_1.Add("ToyFactory", 1051);
		dictionary_1.Add("WalkingFortress", 1052);
		dictionary_1.Add("ChinaPand", 1053);
		foreach (KeyValuePair<string, int> item in dictionary_1)
		{
			dictionary_0.Add(item.Value.ToString(), item.Key);
		}
		list_0.Add(8);
		list_0.Add(10);
		list_0.Add(11);
		list_0.Add(12);
		list_0.Add(13);
		list_0.Add(14);
		list_0.Add(15);
		list_0.Add(16);
		list_0.Add(1005);
		list_0.Add(1006);
		list_0.Add(1007);
		list_0.Add(1008);
		list_0.Add(1009);
		list_0.Add(1010);
		list_0.Add(1003);
		list_0.Add(1011);
		list_0.Add(1012);
		list_0.Add(1020);
		list_0.Add(1021);
		list_0.Add(1023);
		list_0.Add(4001);
		list_0.Add(4002);
		list_0.Add(4003);
		list_0.Add(4005);
		list_0.Add(1025);
		list_0.Add(1026);
		list_0.Add(1027);
		list_0.Add(1028);
		list_0.Add(1029);
		list_0.Add(1030);
		list_0.Add(1031);
		list_0.Add(1032);
		list_0.Add(1033);
		list_0.Add(1034);
		list_0.Add(1035);
		list_0.Add(1036);
		list_0.Add(1037);
		list_0.Add(1038);
	}

	public static void InitCoordsIphone()
	{
		if (!bool_1)
		{
		}
		bool_1 = true;
	}

	public static string GetPlayerNameOrDefault()
	{
		return PlayerPrefs.GetString("NamePlayer", "Player");
		/*string string_ = String_10;
		if (UsersData.UsersData_0 != null)
		{
			if (UsersData.UsersData_0.UserData_0 != null)
			{
				if (UsersData.UsersData_0.UserData_0.user_0 != null)
				{
					if (!string.IsNullOrEmpty(UsersData.UsersData_0.UserData_0.user_0.string_0))
					{
						string_ = UsersData.UsersData_0.UserData_0.user_0.string_0;
					}
					else
					{
						Log.AddLine("ERROR string.IsNullOrEmpty(UsersData.Get.CurrentUser.user.nick) == true", Log.LogLevel.WARNING);
					}
				}
				else
				{
					Log.AddLine("ERROR UsersData.Get.CurrentUser.user == null", Log.LogLevel.WARNING);
				}
			}
			else
			{
				Log.AddLine("ERROR UsersData.Get.CurrentUser == null", Log.LogLevel.WARNING);
			}
		}
		else
		{
			Log.AddLine("ERROR UsersData.Get == null", Log.LogLevel.WARNING);
		}
		return string_;*/
	}
}
