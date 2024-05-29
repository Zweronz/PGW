using System;
using System.IO;
using System.Reflection;
using System.Text;
using Rilisoft;
using UnityEngine;

public sealed class MainMenu : MonoBehaviour
{
	private const string string_0 = "skinsmaker";

	public GUIStyle avardButtonStyle;

	public bool isShowAvard;

	private float float_0;

	public bool showFreeCoins;

	public bool isShowSetting;

	private float float_1;

	private Texture2D texture2D_0;

	private Texture2D texture2D_1;

	private float float_2;

	private Rect rect_0;

	private Rect rect_1;

	private Rect rect_2;

	private Rect rect_3;

	private bool bool_0;

	public static readonly string string_1 = "811995374";

	private bool bool_1;

	private string string_2;

	private GUIStyle guistyle_0;

	private GUIStyle guistyle_1;

	private bool bool_2;

	public static MainMenu mainMenu_0;

	public GameObject JoysticksUIRoot;

	public static bool bool_3;

	public static bool bool_4;

	private bool bool_5;

	private bool bool_6;

	public bool isFirstFrame = true;

	public bool isInappWinOpen;

	private bool bool_7;

	private bool bool_8;

	private bool bool_9;

	public Texture inAppFon;

	public GUIStyle puliInApp;

	public GUIStyle healthInApp;

	public GUIStyle pulemetInApp;

	public GUIStyle crystalSwordInapp;

	public GUIStyle elixirInapp;

	private bool bool_10;

	private bool bool_11;

	private string[] string_3 = StoreKitEventListener.string_30;

	private float float_3;

	public GameObject skinsManagerPrefab;

	public GameObject weaponManagerPrefab;

	public GUIStyle backBut;

	private bool bool_12;

	private bool bool_13;

	private bool bool_14;

	private bool bool_15;

	private bool bool_16;

	private bool bool_17;

	private bool bool_18;

	public static int Int32_0
	{
		get
		{
			return Mathf.RoundToInt((float)Screen.height * 0.03f);
		}
	}

	public static float Single_0
	{
		get
		{
			float result = -1f;
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				string text = SystemInfo.operatingSystem.Replace("iPhone OS ", string.Empty);
				float.TryParse(text.Substring(0, 1), out result);
			}
			return result;
		}
	}

	private void GoToTraining(Action action_0)
	{
		Defs.bool_6 = false;
		Defs.bool_4 = false;
		Defs.bool_2 = false;
		Defs.bool_5 = false;
		Defs.bool_0 = false;
		WeaponManager.weaponManager_0.Reset();
		Application.LoadLevel("CampaignLoading");
	}

	private static string ReadPrefsFileToString()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			try
			{
				using (StreamReader streamReader = File.OpenText("/data/data/com.P3D.Pixlgun/shared_prefs/com.P3D.Pixlgun.xml"))
				{
					return streamReader.ReadToEnd();
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}
		return string.Empty;
	}

	private void DrawGameModeButtons(float float_4)
	{
	}

	private void HandleShopButton()
	{
	}

	public static bool SkinsMakerSupproted()
	{
		return true;
	}

	private void DrawToolbar(float float_4)
	{
	}

	public static void GoToFriends()
	{
		LoadConnectScene.texture_0 = null;
		LoadConnectScene.string_0 = "Friends";
		LoadConnectScene.texture_1 = null;
		Application.LoadLevel(Defs.string_3);
	}

	private void HandleSkinsMakerQueryInventoryFailedEvent(string string_4)
	{
		Debug.LogWarning("Skins Maker query failed.\n\t" + string_4);
	}

	private void HandleSkinsMakerPurchasePurchaseFailedEvent(string string_4)
	{
		Debug.LogWarning("Skins Maker purchase failed.\n\t" + string_4);
	}

	public static string GetEndermanUrl()
	{
		if (Application.platform != RuntimePlatform.IPhonePlayer && !Application.isEditor)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				return (Defs.RuntimeAndroidEdition_0 != Defs.RuntimeAndroidEdition.Amazon) ? "https://play.google.com/store/apps/details?id=com.slender.android" : "http://www.amazon.com/Pocket-Slenderman-Rising-your-virtual/dp/B00I6IXU5A/ref=sr_1_5?s=mobile-apps&ie=UTF8&qid=1395990920&sr=1-5&keywords=slendy";
			}
			return string.Empty;
		}
		return "https://itunes.apple.com/us/app/slendy-raise-your-virtual/id" + string_1 + "?mt=8";
	}

	private void DrawFreeCoins()
	{
	}

	private void HandleBackFromFreeCoins()
	{
		showFreeCoins = false;
	}

	private void DrawDeadMatch()
	{
	}

	private void DrawCOOP()
	{
	}

	private void DrawVersionLabel()
	{
		if (string_2 == null)
		{
			string_2 = Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}
		if (guistyle_0 == null)
		{
			guistyle_0 = new GUIStyle(GUI.skin.label)
			{
				fontSize = Convert.ToInt32(24f * Defs.Single_0),
				normal = new GUIStyleState
				{
					textColor = new Color(0.75f, 0.75f, 0.75f)
				},
				alignment = TextAnchor.UpperRight
			};
			Font font = Resources.Load<Font>("Ponderosa");
			if (font != null)
			{
				guistyle_0.font = font;
			}
		}
		if (string_2.StartsWith("0"))
		{
			Debug.LogWarning("Invalid version: " + string_2);
		}
		else
		{
			GUI.Label(new Rect(0f, 21f * Defs.Single_0, (float)Screen.width - 21f * Defs.Single_0, 32f * Defs.Single_0), string_2, guistyle_0);
		}
	}

	private void DrawDebugInfo()
	{
		if (guistyle_1 == null)
		{
			guistyle_1 = new GUIStyle(GUI.skin.label)
			{
				fontSize = Convert.ToInt32(32f * Defs.Single_0),
				fontStyle = FontStyle.Bold,
				richText = true,
				alignment = TextAnchor.UpperLeft
			};
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("<color='green'>Device model:  {0}</color>", SystemInfo.deviceModel).AppendLine();
		Rect position = new Rect(21f * Defs.Single_0, (float)Screen.height * 0.5f, (float)Screen.width - 42f * Defs.Single_0, (float)Screen.height * 0.5f);
		GUI.Label(position, stringBuilder.ToString(), guistyle_1);
	}

	private void HandleBackFromSettings()
	{
		isShowSetting = false;
	}

	private void OnGUI()
	{
	}

	private GUIStyle CreateButtonStyle(Texture2D texture2D_2)
	{
		GUIStyle gUIStyle = new GUIStyle(GUI.skin.button);
		gUIStyle.active = new GUIStyleState
		{
			background = texture2D_2
		};
		gUIStyle.hover = new GUIStyleState
		{
			background = texture2D_2
		};
		gUIStyle.normal = new GUIStyleState
		{
			background = texture2D_2
		};
		return gUIStyle;
	}

	private string _SocialMessage()
	{
		string text = Defs2.String_0;
		return "Come and play with me in epic multiplayer shooter - Pixel Gun 3D! " + text;
	}

	private string _SocialSentSuccess(string string_4)
	{
		return "Your best score was sent to " + string_4;
	}

	private void completionHandler(string string_4, object object_0)
	{
		if (string_4 != null)
		{
			Debug.LogError(string_4);
		}
	}

	private void facebookGraphReqCompl(object object_0)
	{
	}

	private void facebookSessionOpened()
	{
		bool_17 = ServiceLocator.FacebookFacade_0.GetSessionPermissions().Contains("publish_stream");
		bool_18 = ServiceLocator.FacebookFacade_0.GetSessionPermissions().Contains("publish_actions");
	}

	private void facebookreauthorizationSucceededEvent()
	{
		bool_17 = ServiceLocator.FacebookFacade_0.GetSessionPermissions().Contains("publish_stream");
		bool_18 = ServiceLocator.FacebookFacade_0.GetSessionPermissions().Contains("publish_actions");
	}

	private void Awake()
	{
		using (new StopwatchLogger("MainMenu.Awake()"))
		{
			if (WeaponManager.weaponManager_0 != null)
			{
				WeaponManager.weaponManager_0.Reset();
			}
			else if (!WeaponManager.weaponManager_0 && (bool)weaponManagerPrefab)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(weaponManagerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				gameObject.GetComponent<WeaponManager>().Reset();
			}
		}
	}

	private void Start()
	{
		using (new StopwatchLogger("MainMenu.Start()"))
		{
			mainMenu_0 = this;
			StoreKitEventListener.StoreKitEventListenerState_0.String_0 = "In_main_menu";
			StoreKitEventListener.StoreKitEventListenerState_0.String_1 = "In shop";
			StoreKitEventListener.StoreKitEventListenerState_0.IDictionary_0.Clear();
			bool_16 = Storager.GetInt(Defs.string_2) == 1;
			Invoke("setEnabledGUI", 0.1f);
			Storager.SetInt("typeConnect__", -1);
			string_3 = StoreKitEventListener.string_30;
			if (!WeaponManager.weaponManager_0 && (bool)weaponManagerPrefab)
			{
				UnityEngine.Object.Instantiate(weaponManagerPrefab, Vector3.zero, Quaternion.identity);
			}
			MenuBackgroundMusic.menuBackgroundMusic_0.Play();
		}
	}

	private void Update()
	{
		float num = ((float)Screen.width - 42f * Defs.Single_0 - Defs.Single_0 * (672f + (float)(SkinsMakerSupproted() ? 262 : 0))) / ((!SkinsMakerSupproted()) ? 2f : 3f);
		if (showFreeCoins)
		{
			if (bool_1)
			{
				bool_1 = false;
				HandleBackFromFreeCoins();
				return;
			}
			if (Input.GetKeyUp(KeyCode.Escape))
			{
				Input.ResetInputAxes();
				bool_1 = true;
				return;
			}
		}
		if (isShowSetting && !JoysticksUIRoot.activeInHierarchy)
		{
			if (bool_2)
			{
				bool_2 = false;
				HandleBackFromSettings();
			}
			else if (Input.GetKeyUp(KeyCode.Escape))
			{
				Input.ResetInputAxes();
				bool_2 = true;
			}
		}
	}

	[Obfuscation(Exclude = true)]
	private void setEnabledGUI()
	{
		isFirstFrame = false;
	}

	private bool FacebookSupported()
	{
		return (Application.platform == RuntimePlatform.IPhonePlayer) ? (Single_0 > 5f) : (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WP8Player);
	}

	private void CompletionHandlerPostFacebook(string string_4, object object_0)
	{
	}

	private void OnDestroy()
	{
		mainMenu_0 = null;
	}

	[Obfuscation(Exclude = true)]
	private void hideMessag()
	{
		bool_12 = false;
	}

	[Obfuscation(Exclude = true)]
	private void hideMessagTwitter()
	{
		bool_13 = false;
	}
}
