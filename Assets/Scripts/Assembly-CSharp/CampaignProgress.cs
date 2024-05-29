using System;
using System.Collections.Generic;
using Rilisoft.MiniJson;
using UnityEngine;

public sealed class CampaignProgress
{
	private const string string_0 = "CampaignProgress";

	public static Dictionary<string, Dictionary<string, int>> dictionary_0;

	static CampaignProgress()
	{
		dictionary_0 = new Dictionary<string, Dictionary<string, int>>();
		InitProgress();
	}

	public static void OpenNewBoxIfPossible()
	{
		int num = 0;
		for (int i = 1; i < LevelBox.list_0.Count; i++)
		{
			LevelBox levelBox = LevelBox.list_0[i];
			if (dictionary_0.ContainsKey(levelBox.string_0))
			{
				num = i;
			}
		}
		int num2 = 0;
		foreach (KeyValuePair<string, Dictionary<string, int>> item in dictionary_0)
		{
			foreach (KeyValuePair<string, int> item2 in item.Value)
			{
				num2 += item2.Value;
			}
		}
		int num3 = num + 1;
		if (num3 < LevelBox.list_0.Count)
		{
			string text = LevelBox.list_0[num3].string_0;
			if (LevelBox.list_0[num3].int_0 <= num2 && !dictionary_0.ContainsKey(text))
			{
				dictionary_0.Add(text, new Dictionary<string, int>());
				SaveCampaignProgress();
				FlurryPluginWrapper.LogBoxOpened(text);
			}
		}
		SaveCampaignProgress();
	}

	public static void ResetProgress()
	{
		dictionary_0.Clear();
		InitProgress();
	}

	private static void InitProgress()
	{
		LoadCampaignProgress();
		if (dictionary_0.Keys.Count == 0)
		{
			dictionary_0.Add(LevelBox.list_0[0].string_0, new Dictionary<string, int>());
			SaveCampaignProgress();
		}
	}

	public static bool FirstTimeCompletesLevel(string string_1)
	{
		return !dictionary_0[CurrentCampaignGame.string_0].ContainsKey(string_1);
	}

	public static void SaveCampaignProgress()
	{
		SetProgressDictionary("CampaignProgress", dictionary_0);
	}

	public static string GetCampaignProgressString()
	{
		return Storager.GetString("CampaignProgress", string.Empty);
	}

	public static void LoadCampaignProgress()
	{
		Dictionary<string, Dictionary<string, int>> progressDictionary = GetProgressDictionary("CampaignProgress");
		dictionary_0 = progressDictionary;
	}

	internal static string SerializeProgress(Dictionary<string, Dictionary<string, int>> dictionary_1)
	{
		try
		{
			return Json.Serialize(dictionary_1);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("Message", ex.Message);
			FlurryPluginWrapper.LogEvent(ex.GetType().Name);
			return "{ }";
		}
	}

	internal static Dictionary<string, Dictionary<string, int>> DeserializeProgress(string string_1)
	{
		Dictionary<string, Dictionary<string, int>> dictionary = new Dictionary<string, Dictionary<string, int>>();
		object obj = Json.Deserialize(string_1);
		Dictionary<string, object> dictionary2 = obj as Dictionary<string, object>;
		if (dictionary2 != null)
		{
			foreach (KeyValuePair<string, object> item in dictionary2)
			{
				Dictionary<string, int> dictionary3 = new Dictionary<string, int>();
				IDictionary<string, object> dictionary4 = item.Value as IDictionary<string, object>;
				if (dictionary4 != null)
				{
					foreach (KeyValuePair<string, object> item2 in dictionary4)
					{
						try
						{
							int value = Convert.ToInt32(item2.Value);
							dictionary3.Add(item2.Key, value);
						}
						catch (InvalidCastException)
						{
							Debug.LogWarning(string.Concat("Cannot convert ", item2.Value, " to int."));
						}
					}
				}
				else if (Debug.isDebugBuild)
				{
					Debug.LogWarning("boxProgressDictionary == null");
				}
				dictionary.Add(item.Key, dictionary3);
			}
		}
		else if (Debug.isDebugBuild)
		{
			Debug.LogWarning("campaignProgressDictionary == null,    serializedProgress == " + string_1);
		}
		return dictionary;
	}

	private static void SetProgressDictionary(string string_1, Dictionary<string, Dictionary<string, int>> dictionary_1)
	{
		string string_2 = SerializeProgress(dictionary_1);
		Storager.SetString(string_1, string_2);
		Storager.Save();
	}

	private static Dictionary<string, Dictionary<string, int>> GetProgressDictionary(string string_1)
	{
		string @string = Storager.GetString(string_1, string.Empty);
		return DeserializeProgress(@string);
	}

	internal static Dictionary<string, Dictionary<string, int>> DeserializeTestDictionary()
	{
		string string_ = "{\"Box_11\": { \"Level_02\": 1, \"Level_05\": 3 },\"Box_13\": { \"Level_03\": 1, \"Level_08\": 3, \"Level_21\": 2 },\"Box_34\": { },\"Box_99\": { \"Level_55\": 2 },}";
		return DeserializeProgress(string_);
	}
}
