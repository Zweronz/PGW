using System.Collections.Generic;
using System.Security.Cryptography;
using Rilisoft;
using UnityEngine;

public sealed class Defs2
{
	private class MapInfo
	{
		public string string_0;

		public string string_1;

		public string string_2;

		public string string_3;

		public MapInfo(string string_4, string string_5, string string_6, string string_7)
		{
			string_0 = string_4;
			string_1 = string_5;
			string_2 = string_6;
			string_3 = string_7;
		}
	}

	public static Dictionary<string, string> dictionary_0;

	public static Dictionary<string, string> dictionary_1;

	public static Dictionary<string, string> dictionary_2;

	public static Dictionary<string, string> dictionary_3;

	private static List<MapInfo> list_0;

	private static SignedPreferences signedPreferences_0;

	private static readonly byte[] byte_0;

	public static string String_0
	{
		get
		{
			return string.Empty;
		}
	}

	internal static SignedPreferences SignedPreferences_0
	{
		get
		{
			if (signedPreferences_0 == null)
			{
				RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider(512);
				rSACryptoServiceProvider.ImportCspBlob(byte_0);
				signedPreferences_0 = new RsaSignedPreferences(new PersistentPreferences(), rSACryptoServiceProvider, SystemInfo.deviceUniqueIdentifier);
			}
			return signedPreferences_0;
		}
	}

	static Defs2()
	{
		dictionary_0 = new Dictionary<string, string>();
		dictionary_1 = new Dictionary<string, string>();
		dictionary_2 = new Dictionary<string, string>();
		dictionary_3 = new Dictionary<string, string>();
		list_0 = new List<MapInfo>();
		byte_0 = new byte[308]
		{
			7, 2, 0, 0, 0, 164, 0, 0, 82, 83,
			65, 50, 0, 2, 0, 0, 17, 0, 0, 0,
			1, 24, 67, 211, 214, 189, 210, 144, 254, 145,
			230, 212, 19, 254, 185, 112, 117, 120, 142, 89,
			80, 227, 74, 157, 136, 99, 204, 254, 117, 105,
			106, 52, 143, 219, 180, 55, 4, 174, 130, 222,
			59, 143, 80, 32, 56, 220, 204, 215, 254, 202,
			38, 42, 34, 141, 116, 38, 68, 147, 247, 71,
			65, 49, 18, 153, 205, 10, 30, 210, 118, 97,
			196, 36, 168, 88, 201, 246, 230, 160, 110, 13,
			124, 85, 105, 5, 43, 72, 1, 158, 28, 194,
			234, 109, 169, 124, 57, 167, 5, 106, 4, 145,
			166, 174, 181, 8, 222, 238, 193, 247, 67, 4,
			63, 158, 68, 238, 149, 46, 126, 245, 244, 34,
			194, 82, 16, 202, 202, 47, 85, 234, 177, 145,
			103, 107, 6, 167, 139, 19, 113, 83, 144, 51,
			172, 211, 28, 133, 56, 20, 84, 65, 236, 67,
			16, 239, 26, 32, 10, 254, 38, 72, 99, 157,
			197, 181, 106, 238, 33, 247, 188, 47, 35, 40,
			87, 193, 215, 151, 33, 197, 170, 220, 239, 73,
			82, 102, 162, 100, 132, 69, 125, 74, 225, 224,
			235, 68, 230, 233, 9, 162, 182, 97, 205, 7,
			35, 71, 107, 239, 213, 14, 6, 135, 7, 137,
			140, 150, 80, 39, 253, 197, 12, 101, 164, 157,
			109, 89, 10, 134, 225, 17, 130, 168, 84, 111,
			116, 89, 20, 67, 132, 7, 204, 191, 33, 103,
			113, 0, 12, 11, 19, 139, 190, 49, 110, 98,
			16, 209, 75, 236, 139, 213, 86, 4, 8, 182,
			121, 126, 53, 5, 123, 132, 234, 114, 1, 125,
			120, 63, 150, 29, 192, 102, 100, 11, 230, 161,
			170, 133, 253, 231, 199, 89, 5, 45
		};
		SetMapsInfo();
		SetLocalizeForMaps();
		LocalizationStore.AddEventCallAfterLocalize(SetLocalizeForMaps);
		PlayerEventScoreController.SetScoreEventInfo();
	}

	public static bool IsAvalibleAddFrends()
	{
		return false;
	}

	public static void SetMapsInfo()
	{
		list_0.Clear();
		list_0.Add(new MapInfo("Maze", "Key_0446", "Key_0492", "Key_0538"));
		list_0.Add(new MapInfo("Cementery", "Key_0447", "Key_0493", "Key_0539"));
		list_0.Add(new MapInfo("City", "Key_0448", "Key_0494", "Key_0540"));
		list_0.Add(new MapInfo("Gluk", "Key_0449", "Key_0495", "Key_0540"));
		list_0.Add(new MapInfo("Jail", "Key_0450", "Key_0496", "Key_0538"));
		list_0.Add(new MapInfo("Hospital", "Key_0451", "Key_0497", "Key_0538"));
		list_0.Add(new MapInfo("Pool", "Key_0452", "Key_0498", "Key_0541"));
		list_0.Add(new MapInfo("Slender", "Key_0453", "Key_0499", "Key_0540"));
		list_0.Add(new MapInfo("Castle", "Key_0454", "Key_0500", "Key_0538"));
		list_0.Add(new MapInfo("Ranch", "Key_0455", "Key_0501", "Key_0541"));
		list_0.Add(new MapInfo("Arena_MP", "Key_0456", "Key_0502", "Key_0541"));
		list_0.Add(new MapInfo("Sky_islands", "Key_0457", "Key_0503", "Key_0540"));
		list_0.Add(new MapInfo("Dust", "Key_0458", "Key_0504", "Key_0538"));
		list_0.Add(new MapInfo("Bridge", "Key_0459", "Key_0505", "Key_0538"));
		list_0.Add(new MapInfo("Farm", "Key_0460", "Key_0506", "Key_0539"));
		list_0.Add(new MapInfo("Utopia", "Key_0461", "Key_0507", "Key_0540"));
		list_0.Add(new MapInfo("Aztec", "Key_0462", "Key_0508", "Key_0540"));
		list_0.Add(new MapInfo("Arena", "Key_0463", "Key_0509", "Key_0539"));
		list_0.Add(new MapInfo("Assault", "Key_0464", "Key_0510", "Key_0538"));
		list_0.Add(new MapInfo("Winter", "Key_0465", "Key_0511", "Key_0538"));
		list_0.Add(new MapInfo("School", "Key_0466", "Key_0512", "Key_0540"));
		list_0.Add(new MapInfo("Parkour", "Key_0467", "Key_0513", "Key_0540"));
		list_0.Add(new MapInfo("Coliseum_MP", "Key_0468", "Key_0514", "Key_0541"));
		list_0.Add(new MapInfo("Hungry", "Key_0469", "Key_0515", "Key_0540"));
		list_0.Add(new MapInfo("Hungry_Night", "Key_0470", "Key_0516", "Key_0540"));
		list_0.Add(new MapInfo("Hungry_2", "Key_0471", "Key_0517", "Key_0540"));
		list_0.Add(new MapInfo("Estate", "Key_0472", "Key_0518", "Key_0539"));
		list_0.Add(new MapInfo("Space", "Key_0473", "Key_0519", "Key_0540"));
		list_0.Add(new MapInfo("Portal", "Key_0474", "Key_0520", "Key_0540"));
		list_0.Add(new MapInfo("Two_Castles", "Key_0475", "Key_0521", "Key_0540"));
		list_0.Add(new MapInfo("Ships", "Key_0476", "Key_0522", "Key_0539"));
		list_0.Add(new MapInfo("Ships_Night", "Key_0477", "Key_0523", "Key_0539"));
		list_0.Add(new MapInfo("Gluk_3", "Key_0478", "Key_0524", "Key_0540"));
		list_0.Add(new MapInfo("Matrix", "Key_0479", "Key_0525", "Key_0540"));
		list_0.Add(new MapInfo("Ants", "Key_0480", "Key_0526", "Key_0540"));
		list_0.Add(new MapInfo("Hill", "Key_0481", "Key_0527", "Key_0539"));
		list_0.Add(new MapInfo("Heaven", "Key_0482", "Key_0528", "Key_0538"));
		list_0.Add(new MapInfo("Underwater", "Key_0483", "Key_0529", "Key_0540"));
		list_0.Add(new MapInfo("Knife", "Key_0484", "Key_0530", "Key_0539"));
		list_0.Add(new MapInfo("Day_D", "Key_0485", "Key_0531", "Key_0540"));
		list_0.Add(new MapInfo("NuclearCity", "Key_0486", "Key_0532", "Key_0540"));
		list_0.Add(new MapInfo("Cube", "Key_0487", "Key_0533", "Key_0540"));
		list_0.Add(new MapInfo("Train", "Key_0488", "Key_0534", "Key_0540"));
		list_0.Add(new MapInfo("Sniper", "Key_0489", "Key_0535", "Key_0540"));
		list_0.Add(new MapInfo("Supermarket", "Key_0490", "Key_0536", "Key_0540"));
		list_0.Add(new MapInfo("Pumpkins", "Key_0491", "Key_0537", "Key_0539"));
		list_0.Add(new MapInfo("Christmas_Town", "Key_0985", "Key_0917", "Key_0538"));
		list_0.Add(new MapInfo("Christmas_Town_Night", "Key_1021", "Key_1020", "Key_0538"));
		list_0.Add(new MapInfo("Paradise", "Key_1030", "Key_1029", "Key_0540"));
		list_0.Add(new MapInfo("Bota", "Key_1069", "Key_1044", "Key_0540"));
		list_0.Add(new MapInfo("Pizza", "Key_1093", "Key_1092", "Key_0538"));
		list_0.Add(new MapInfo("Barge", "Key_1099", "Key_1198", "Key_0539"));
		list_0.Add(new MapInfo("Mine", "Key_MainMap1", "Key_MainMap2", "Key_MainMap3"));
	}

	public static void SetLocalizeForMaps()
	{
		dictionary_0.Clear();
		dictionary_1.Clear();
		dictionary_2.Clear();
		dictionary_3.Clear();
		foreach (MapInfo item in list_0)
		{
			dictionary_0.Add(item.string_0, LocalizationStore.Get(item.string_1));
			dictionary_1.Add(item.string_0, LocalizationStore.GetByDefault(item.string_2));
			dictionary_2.Add(item.string_0, LocalizationStore.Get(item.string_2));
			dictionary_3.Add(item.string_0, LocalizationStore.Get(item.string_3));
		}
	}

	public static string GetMapEnglishName(string string_0)
	{
		if (dictionary_1.ContainsKey(string_0))
		{
			return dictionary_1[string_0];
		}
		return "Unknown";
	}
}
