using System;
using System.Collections.Generic;
using UnityEngine;

public static class Localization
{
	public static bool bool_0 = false;

	private static string[] string_0 = null;

	private static Dictionary<string, string> dictionary_0 = new Dictionary<string, string>();

	private static Dictionary<string, string[]> dictionary_1 = new Dictionary<string, string[]>();

	private static int int_0 = -1;

	private static string string_1;

	public static Dictionary<string, string[]> Dictionary_0
	{
		get
		{
			if (!bool_0)
			{
				String_1 = PlayerPrefs.GetString("Language", "English");
			}
			return dictionary_1;
		}
		set
		{
			bool_0 = value != null;
			dictionary_1 = value;
		}
	}

	public static string[] String_0
	{
		get
		{
			if (!bool_0)
			{
				LoadDictionary(PlayerPrefs.GetString("Language", "English"));
			}
			return string_0;
		}
	}

	public static string String_1
	{
		get
		{
			if (string.IsNullOrEmpty(string_1))
			{
				string[] array = String_0;
				string_1 = PlayerPrefs.GetString("Language", (array == null) ? "English" : array[0]);
				LoadAndSelect(string_1);
			}
			return string_1;
		}
		set
		{
			if (string_1 != value)
			{
				string_1 = value;
				LoadAndSelect(value);
			}
		}
	}

	[Obsolete("Localization is now always active. You no longer need to check this property.")]
	public static bool Boolean_0
	{
		get
		{
			return true;
		}
	}

	private static bool LoadDictionary(string string_2)
	{
		TextAsset textAsset = ((!bool_0) ? (Resources.Load("Localization", typeof(TextAsset)) as TextAsset) : null);
		bool_0 = true;
		if (textAsset != null && LoadCSV(textAsset))
		{
			return true;
		}
		if (string.IsNullOrEmpty(string_2))
		{
			return false;
		}
		textAsset = Resources.Load(string_2, typeof(TextAsset)) as TextAsset;
		if (textAsset != null)
		{
			Load(textAsset);
			return true;
		}
		return false;
	}

	private static bool LoadAndSelect(string string_2)
	{
		if (!string.IsNullOrEmpty(string_2))
		{
			if (dictionary_1.Count == 0 && !LoadDictionary(string_2))
			{
				return false;
			}
			if (SelectLanguage(string_2))
			{
				return true;
			}
		}
		if (dictionary_0.Count > 0)
		{
			return true;
		}
		dictionary_0.Clear();
		dictionary_1.Clear();
		if (string.IsNullOrEmpty(string_2))
		{
			PlayerPrefs.DeleteKey("Language");
		}
		return false;
	}

	public static void Load(TextAsset textAsset_0)
	{
		ByteReader byteReader = new ByteReader(textAsset_0);
		Set(textAsset_0.name, byteReader.ReadDictionary());
	}

	public static bool LoadCSV(TextAsset textAsset_0)
	{
		ByteReader byteReader = new ByteReader(textAsset_0);
		BetterList<string> betterList = byteReader.ReadCSV();
		if (betterList.size < 2)
		{
			return false;
		}
		betterList[0] = "KEY";
		if (!string.Equals(betterList[0], "KEY"))
		{
			Debug.LogError("Invalid localization CSV file. The first value is expected to be 'KEY', followed by language columns.\nInstead found '" + betterList[0] + "'", textAsset_0);
			return false;
		}
		string_0 = new string[betterList.size - 1];
		for (int i = 0; i < string_0.Length; i++)
		{
			string_0[i] = betterList[i + 1];
		}
		dictionary_1.Clear();
		while (betterList != null)
		{
			AddCSV(betterList);
			betterList = byteReader.ReadCSV();
		}
		return true;
	}

	private static bool SelectLanguage(string string_2)
	{
		int_0 = -1;
		if (dictionary_1.Count == 0)
		{
			return false;
		}
		string[] value;
		if (dictionary_1.TryGetValue("KEY", out value))
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == string_2)
				{
					dictionary_0.Clear();
					int_0 = i;
					string_1 = string_2;
					PlayerPrefs.SetString("Language", string_1);
					UIRoot.Broadcast("OnLocalize");
					return true;
				}
			}
		}
		return false;
	}

	private static void AddCSV(BetterList<string> betterList_0)
	{
		if (betterList_0.size < 2)
		{
			return;
		}
		string[] array = new string[betterList_0.size - 1];
		for (int i = 1; i < betterList_0.size; i++)
		{
			array[i - 1] = betterList_0[i];
		}
		try
		{
			dictionary_1.Add(betterList_0[0], array);
		}
		catch (Exception ex)
		{
			Debug.LogError("Unable to add '" + betterList_0[0] + "' to the Localization dictionary.\n" + ex.Message);
		}
	}

	public static void Set(string string_2, Dictionary<string, string> dictionary_2)
	{
		string_1 = string_2;
		PlayerPrefs.SetString("Language", string_1);
		dictionary_0 = dictionary_2;
		bool_0 = false;
		int_0 = -1;
		string_0 = new string[1] { string_2 };
		UIRoot.Broadcast("OnLocalize");
	}

	public static string Get(string string_2)
	{
		if (!bool_0)
		{
			String_1 = PlayerPrefs.GetString("Language", "English");
		}
		string[] value;
		string value2;
		if (int_0 != -1 && dictionary_1.TryGetValue(string_2, out value))
		{
			if (int_0 < value.Length)
			{
				return value[int_0];
			}
		}
		else if (dictionary_0.TryGetValue(string_2, out value2))
		{
			return value2;
		}
		return string_2;
	}

	[Obsolete("Use Localization.Get instead")]
	public static string Localize(string string_2)
	{
		return Get(string_2);
	}

	public static bool Exists(string string_2)
	{
		if (!bool_0)
		{
			String_1 = PlayerPrefs.GetString("Language", "English");
		}
		return dictionary_1.ContainsKey(string_2) || dictionary_0.ContainsKey(string_2);
	}
}
