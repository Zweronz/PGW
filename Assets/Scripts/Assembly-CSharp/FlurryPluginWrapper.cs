using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using Rilisoft.MiniJson;
using UnityEngine;

public sealed class FlurryPluginWrapper : MonoBehaviour
{
	public const string string_0 = "Back to Main Menu";

	public const string string_1 = "Enable_Deadly Games";

	private const string string_2 = "PayingUser";

	public static string string_3 = "ModeEnteredEvent";

	public static string string_4 = "MapEnteredEvent";

	public static string string_5 = "MapName";

	public static string string_6 = "Mode";

	public static string string_7 = "ModePressed";

	public static string string_8 = "Post to Social";

	public static string string_9 = "Service";

	public static string string_10 = "App_version";

	public static string string_11 = "Local Button Pressed";

	public static string string_12 = "Way_to_start_multiplayer_DEATHMATCH";

	public static string string_13 = "Way_to_start_multiplayer_COOP";

	public static string string_14 = "Way_to_start_multiplayer_Company";

	public static string string_15 = "Button";

	public static readonly string string_16 = "Hats_Capes_Shop";

	public static string string_17 = "FreeCoins";

	public static string string_18 = "type";

	public static string string_19 = "Rate_Us";

	[CompilerGenerated]
	private static AsyncCallback asyncCallback_0;

	public static string String_0
	{
		get
		{
			return Defs.bool_4 ? string_13 : ((!Defs.bool_5) ? string_12 : string_14);
		}
	}

	public static void LogLevelPressed(string string_20)
	{
		LogEventToDevToDev(string_20 + " Pressed");
	}

	public static void LogBoxOpened(string string_20)
	{
		LogEvent(string_20 + "_Box_Opened");
	}

	public static void LogEventWithParameterAndValue(string string_20, string string_21, string string_22)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add(string_21, string_22);
		dictionary.Add("Paying User", IsPayingUser().ToString());
		LogEventToDevToDev(string_20);
	}

	public static void LogEvent(string string_20)
	{
		LogEventToDevToDev(string_20);
	}

	public static void LogTimedEvent(string string_20)
	{
	}

	public static void LogEvent(string string_20, Dictionary<string, string> dictionary_0)
	{
		if (!dictionary_0.ContainsKey("Paying User"))
		{
			dictionary_0.Add("Paying User", IsPayingUser().ToString());
		}
		LogEventToDevToDev(string_20, dictionary_0);
	}

	public static void LogTimedEvent(string string_20, Dictionary<string, string> dictionary_0)
	{
		if (!dictionary_0.ContainsKey("Paying User"))
		{
			dictionary_0.Add("Paying User", IsPayingUser().ToString());
		}
	}

	public static void EndTimedEvent(string string_20)
	{
	}

	public static void LogEventAndDublicateToConsole(string string_20, Dictionary<string, string> dictionary_0)
	{
		LogEvent(string_20, dictionary_0);
		if (Debug.isDebugBuild)
		{
			string text = ((dictionary_0 == null) ? "{ }" : Json.Serialize(dictionary_0));
			Debug.Log(string_20 + " : " + text);
		}
	}

	public static void LogFastPurchase(string string_20)
	{
	}

	public static void LogMatchCompleted(string string_20)
	{
	}

	public static void LogWinInMatch(string string_20)
	{
	}

	public static void LogTimedEventAndDublicateToConsole(string string_20)
	{
		LogTimedEvent(string_20);
		if (Debug.isDebugBuild)
		{
			Debug.Log(string_20);
		}
	}

	public static void LogModeEventWithValue(string string_20)
	{
		if (!Storager.HasKey("Mode Pressed First Time"))
		{
			Storager.SetInt("Mode Pressed First Time", 0);
			LogEventWithParameterAndValue("Mode Pressed First Time", string_6, string_20);
		}
		else
		{
			LogEventWithParameterAndValue(string_7, string_6, string_20);
		}
	}

	public static void LogMultiplayerWayStart()
	{
		LogEventWithParameterAndValue(String_0, string_15, "Start");
		LogEvent("Start");
	}

	public static void LogMultiplayerWayQuckRandGame()
	{
		LogEventWithParameterAndValue(String_0, string_15, "Quick_rand_game");
		LogEvent("Random");
	}

	public static void LogMultiplayerWayCustom()
	{
		LogEventWithParameterAndValue(String_0, string_15, "Custom");
		LogEvent("Custom");
	}

	public static void LogDeathmatchModePress()
	{
		LogModeEventWithValue("Deathmatch");
		LogEvent("Deathmatch");
	}

	public static void LogCampaignModePress()
	{
		LogModeEventWithValue("Survival");
		LogEvent("Campaign");
	}

	public static void LogTrueSurvivalModePress()
	{
		LogModeEventWithValue("Arena_Survival");
		LogEvent("Survival");
	}

	public static void LogCooperativeModePress()
	{
		LogModeEventWithValue("COOP");
		LogEvent("Cooperative");
	}

	public static void LogSkinsMakerModePress()
	{
		LogEvent("Skins Maker");
	}

	public static void LogTwitter()
	{
		LogEventWithParameterAndValue(string_8, string_9, "Twitter");
		DevToDevSDK.SocialNetworkPost(DevToDevSocialNetwork.devToDevSocialNetwork_1, DevToDevSocialNetworkPostReason.devToDevSocialNetworkPostReason_8);
	}

	public static void LogFacebook()
	{
		LogEventWithParameterAndValue(string_8, string_9, "Facebook");
		DevToDevSDK.SocialNetworkPost(DevToDevSocialNetwork.devToDevSocialNetwork_2, DevToDevSocialNetworkPostReason.devToDevSocialNetworkPostReason_8);
	}

	public static void LogGamecenter()
	{
		LogEvent("Game Center");
	}

	public static void LogFreeCoinsFacebook()
	{
		LogEventWithParameterAndValue(string_17, string_18, "Facebook");
		LogEvent("Facebook");
		DevToDevSDK.SocialNetworkPost(DevToDevSocialNetwork.devToDevSocialNetwork_2, DevToDevSocialNetworkPostReason.devToDevSocialNetworkPostReason_8);
	}

	public static void LogFreeCoinsTwitter()
	{
		LogEventWithParameterAndValue(string_17, string_18, "Twitter");
		LogEvent("Twitter");
		DevToDevSDK.SocialNetworkPost(DevToDevSocialNetwork.devToDevSocialNetwork_1, DevToDevSocialNetworkPostReason.devToDevSocialNetworkPostReason_8);
	}

	public static void LogFreeCoinsYoutube()
	{
		LogEventWithParameterAndValue(string_17, string_18, "Youtube");
		LogEvent("YouTube");
	}

	public static void LogCoinEarned()
	{
		LogEvent("Earned Coin Survival");
	}

	public static void LogCoinEarned_COOP()
	{
		LogEvent("Earned Coin COOP");
	}

	public static void LogCoinEarned_Deathmatch()
	{
		LogEvent("Earned Coin Deathmatch");
	}

	public static void LogFreeCoinsRateUs()
	{
		LogEvent(string_19);
	}

	public static void LogSkinsMakerEnteredEvent()
	{
		LogEvent("SkinsMaker");
	}

	public static void LogAddYourSkinBoughtEvent()
	{
		LogEvent("AddYourSkin_Bought");
	}

	public static void LogAddYourSkinTriedToBoughtEvent()
	{
		LogEvent("AddYourSkin_TriedToBought");
	}

	public static void LogAddYourSkinUsedEvent()
	{
		LogEvent("AddYourSkin_Used");
	}

	public static void LogMultiplayeLocalEvent()
	{
		LogEvent(string_11);
	}

	public static void LogMultiplayeWorldwideEvent()
	{
		LogEvent("Worldwide");
	}

	public static void LogCategoryEnteredEvent(string string_20)
	{
		LogEventWithParameterAndValue("Dhop_Category", "Category_name", string_20);
	}

	public static void LogEnteringMap(int int_0, string string_20)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add(string_5, string_20);
		string text;
		string text2;
		if (Defs.bool_4)
		{
			text = "COOP";
		}
		else
			text2 = "Deathmatch_WorldWide";
	}

	public static void DoWithResponse(HttpWebRequest httpWebRequest_0, Action<HttpWebResponse> action_0)
	{
		Action action = delegate
		{
			httpWebRequest_0.BeginGetResponse(delegate(IAsyncResult iasyncResult_0)
			{
				HttpWebResponse obj = (HttpWebResponse)((HttpWebRequest)iasyncResult_0.AsyncState).EndGetResponse(iasyncResult_0);
				action_0(obj);
			}, httpWebRequest_0);
		};
		action.BeginInvoke(delegate(IAsyncResult iasyncResult_0)
		{
			Action action2 = (Action)iasyncResult_0.AsyncState;
			action2.EndInvoke(iasyncResult_0);
		}, action);
	}

	public static HttpWebRequest RequestAppWithID(string string_20)
	{
		return (HttpWebRequest)WebRequest.Create("http://itunes.apple.com/lookup?id=" + string_20);
	}

	public static bool IsPayingUser()
	{
		try
		{
			string @string = Storager.GetString("Last Payment Time", string.Empty);
			DateTime result;
			if (DateTime.TryParse(@string, out result))
			{
				TimeSpan timeSpan = DateTime.UtcNow - result;
				return timeSpan <= TimeSpan.FromDays(14.0);
			}
			return false;
		}
		catch (ArgumentException exception)
		{
			Debug.LogWarning("IsPayingUser() called incorrectly, stacktrace:    " + Environment.StackTrace);
			Debug.LogException(exception);
			return false;
		}
	}

	public static bool IsPayingNUser(int int_0)
	{
		return IsPayingUser() && Storager.GetInt("ALLCoins") + Storager.GetInt("ALLGems") > int_0;
	}

	private void CheckForEdnermanApp()
	{
	}

	private void InitializeFlurryWindowsPhone()
	{
	}

	private void OnApplicationPause(bool bool_0)
	{
		if (!bool_0)
		{
			CheckForEdnermanApp();
			InitializeFlurryWindowsPhone();
		}
	}

	private void Start()
	{
		CheckForEdnermanApp();
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		InitializeFlurryWindowsPhone();
		DevToDevSDK.Initialize(StatisticsApiKeys.string_0, StatisticsApiKeys.string_1);
		DevToDevSDK.StartSession();
	}

	private void OnDestroy()
	{
		DevToDevSDK.EndSession();
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void spaceDidDismissEvent(string string_20)
	{
		Debug.Log("spaceDidDismissEvent: " + string_20);
	}

	private void spaceWillLeaveApplicationEvent(string string_20)
	{
		Debug.Log("spaceWillLeaveApplicationEvent: " + string_20);
	}

	private void spaceDidFailToRenderEvent(string string_20)
	{
		Debug.Log("spaceDidFailToRenderEvent: " + string_20);
	}

	private void spaceDidReceiveAdEvent(string string_20)
	{
		Debug.Log("spaceDidReceiveAdEvent: " + string_20);
	}

	private void spaceDidFailToReceiveAdEvent(string string_20)
	{
		Debug.Log("spaceDidFailToReceiveAdEvent: " + string_20);
	}

	private void onCurrencyValueUpdatedEvent(string string_20, float float_0)
	{
		Debug.LogError("onCurrencyValueUpdatedEvent. currency: " + string_20 + ", amount: " + float_0);
	}

	private void videoDidFinishEvent(string string_20)
	{
		Debug.Log("videoDidFinishEvent: " + string_20);
	}

	private static void LogEventToDevToDev(string string_20, Dictionary<string, string> dictionary_0 = null)
	{
		DevToDevCustomEventParams devToDevCustomEventParams = new DevToDevCustomEventParams();
		using (devToDevCustomEventParams)
		{
			try
			{
				if (dictionary_0 == null)
				{
					dictionary_0 = new Dictionary<string, string>();
				}
				foreach (KeyValuePair<string, string> item in dictionary_0)
				{
					devToDevCustomEventParams.Put(item.Key, item.Value);
				}
				if (!dictionary_0.ContainsKey("Initial Version"))
				{
				}
				DevToDevSDK.CustomEvent(string_20, devToDevCustomEventParams);
			}
			catch (Exception message)
			{
				Debug.LogWarning(message);
			}
		}
	}

	public static string GetEventName(string string_20)
	{
		return string_20 + GetPayingSuffix();
	}

	public static string GetPayingSuffix()
	{
		if (!IsPayingUser())
		{
			return " (Non Paying)";
		}
		if (IsPayingNUser(10))
		{
			return string.Format(" (Paying ${0})", 10);
		}
		return " (Paying)";
	}

	public static string GetPayingSuffixNo10()
	{
		if (!IsPayingUser())
		{
			return " (Non Paying)";
		}
		return " (Paying)";
	}
}
