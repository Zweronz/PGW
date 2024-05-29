using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Rilisoft;
using UnityEngine;
using engine.helpers;
using engine.unity;

internal sealed class SettingsController : MonoBehaviour
{
	public static readonly int int_0 = 1;

	public static readonly int int_1 = 10;

	public static bool bool_0 = true;

	public MainMenuHeroCamera rotateCamera;

	public UIButton backButton;

	public UIButton controlsButton;

	public GameObject controlsSettings;

	public GameObject tapPanel;

	public GameObject swipePanel;

	public GameObject mainPanel;

	public GameObject socialPanel;

	public UISlider sensitivitySlider;

	public UILabel versionLabel;

	public SettingsToggleButtons chatToggleButtons;

	public SettingsToggleButtons musicToggleButtons;

	public SettingsToggleButtons soundToggleButtons;

	public SettingsToggleButtons invertCameraToggleButtons;

	public SettingsToggleButtons leftHandedToggleButtons;

	public UILabel resolution;

	public UIPopupList resolutionPopup;

	public UIToggle windowMode;

	private bool bool_1;

	private float float_0;

	private static Action action_0;

	[CompilerGenerated]
	private static EventHandler<ToggleButtonEventArgs> eventHandler_0;

	[CompilerGenerated]
	private static EventHandler<ToggleButtonEventArgs> eventHandler_1;

	[CompilerGenerated]
	private static EventHandler<ToggleButtonEventArgs> eventHandler_2;

	[CompilerGenerated]
	private static EventHandler<ToggleButtonEventArgs> eventHandler_3;

	[CompilerGenerated]
	private static EventHandler<ToggleButtonEventArgs> eventHandler_4;

	public static event Action ControlsClicked
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			action_0 = (Action)Delegate.Combine(action_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			action_0 = (Action)Delegate.Remove(action_0, value);
		}
	}

	public static void SwitchChatSetting(bool bool_2, Action action_1 = null)
	{
		if (Application.isEditor)
		{
			Debug.Log("[Chat] button clicked: " + bool_2);
		}
		if (Defs.Boolean_1 != bool_2)
		{
			Defs.Boolean_1 = bool_2;
			if (action_1 != null)
			{
				action_1();
			}
		}
	}

	public static void ChangeLeftHandedRightHanded(bool bool_2, Action action_1 = null)
	{
		if (Application.isEditor)
		{
			Debug.Log("[Left Handed] button clicked: " + bool_2);
		}
		if (GlobalGameController.bool_0 == bool_2)
		{
			return;
		}
		GlobalGameController.bool_0 = bool_2;
		Storager.SetInt(Defs.String_4, bool_2 ? 1 : 0);
		Storager.Save();
		if (action_1 != null)
		{
			action_1();
		}
		if (action_0 != null)
		{
			action_0();
		}
		if (!bool_2)
		{
			FlurryPluginWrapper.LogEvent("Left-handed Layout Enabled");
			if (Debug.isDebugBuild)
			{
				Debug.Log("Left-handed Layout Enabled");
			}
		}
	}

	private void Awake()
	{
		InitGraphicsParams();
	}

	private void Start()
	{
		string string_ = Assembly.GetExecutingAssembly().GetName().Version.ToString();
		if (versionLabel != null)
		{
			versionLabel.String_0 = string_;
		}
		else
		{
			Debug.LogWarning("versionLabel == null");
		}
		if (backButton != null)
		{
			ButtonHandler component = backButton.GetComponent<ButtonHandler>();
			component.Clicked += HandleBackFromSettings;
		}
		if (controlsButton != null)
		{
			ButtonHandler component2 = controlsButton.GetComponent<ButtonHandler>();
			component2.Clicked += HandleControlsClicked;
		}
		if (sensitivitySlider != null)
		{
			float single_ = PresetGameSettings.PresetGameSettings_0.Single_0;
			float num = Mathf.Clamp(single_, int_0, int_1);
			float num2 = num - (float)int_0;
			sensitivitySlider.Single_0 = num2 / (float)(int_1 - int_0);
			float_0 = num;
		}
		else
		{
			Debug.LogWarning("sensitivitySlider == null");
		}
		musicToggleButtons.Boolean_0 = MenuBackgroundMusic.menuBackgroundMusic_0.Boolean_0;
		musicToggleButtons.Clicked += delegate(object sender, ToggleButtonEventArgs e)
		{
			if (Application.isEditor)
			{
				Debug.Log("[Music] button clicked: " + e.Boolean_0);
			}
			if (MenuBackgroundMusic.menuBackgroundMusic_0.Boolean_0 != e.Boolean_0)
			{
				Storager.SetBool(Defs.String_1, e.Boolean_0);
				Storager.Save();
				MenuBackgroundMusic.menuBackgroundMusic_0.ChangePlayMusicState(e.Boolean_0);
			}
		};
		soundToggleButtons.Boolean_0 = Defs.Boolean_0;
		soundToggleButtons.Clicked += delegate(object sender, ToggleButtonEventArgs e)
		{
			if (Application.isEditor)
			{
				Debug.Log("[Sound] button clicked: " + e.Boolean_0);
			}
			if (Defs.Boolean_0 != e.Boolean_0)
			{
				Defs.Boolean_0 = e.Boolean_0;
				Storager.SetBool(Defs.String_2, Defs.Boolean_0);
				Storager.Save();
			}
		};
		chatToggleButtons.Boolean_0 = Defs.Boolean_1;
		chatToggleButtons.Clicked += delegate(object sender, ToggleButtonEventArgs e)
		{
			SwitchChatSetting(e.Boolean_0);
		};
		invertCameraToggleButtons.Boolean_0 = Storager.GetInt(Defs.string_2) == 1;
		invertCameraToggleButtons.Clicked += delegate(object sender, ToggleButtonEventArgs e)
		{
			if (Application.isEditor)
			{
				Debug.Log("[Invert Camera] button clicked: " + e.Boolean_0);
			}
			if (Storager.GetInt(Defs.string_2) == 1 != e.Boolean_0)
			{
				Storager.SetInt(Defs.string_2, Convert.ToInt32(e.Boolean_0));
				Storager.Save();
			}
		};
		if (leftHandedToggleButtons != null)
		{
			leftHandedToggleButtons.Boolean_0 = GlobalGameController.bool_0;
			leftHandedToggleButtons.Clicked += delegate(object sender, ToggleButtonEventArgs e)
			{
				ChangeLeftHandedRightHanded(e.Boolean_0);
			};
		}
		mainPanel = Lobby.Lobby_0.gameObject;
	}

	private void InitGraphicsParams()
	{
		resolutionPopup.items = GraphicsController.GraphicsController_0.list_2;
		UIPopupList uIPopupList = resolutionPopup;
		string string_ = GraphicsController.GraphicsController_0.String_0;
		resolution.String_0 = string_;
		uIPopupList.String_0 = string_;
		windowMode.Boolean_0 = GraphicsController.GraphicsController_0.Boolean_0;
	}

	public static void CheckKeyboardConflict()
	{
		bool flag = false;
		List<SettingsControlItemData> controlItems = KeyboardController.KeyboardController_0.GetControlItems();
		foreach (SettingsControlItemData item in controlItems)
		{
			foreach (SettingsControlItemData item2 in controlItems)
			{
				if ((!item.string_1.Equals(item2.string_1) || item.int_0 != item2.int_0) && (!item.string_1.Equals(item2.string_1) || (item.string_1.Equals(item2.string_1) && item.int_0 != item2.int_0)))
				{
					bool flag2 = (item.keyCode_0 == item2.keyCode_0 && item2.keyCode_0 != 0) || (item.keyCode_0 == item2.keyCode_1 && item2.keyCode_1 != KeyCode.None);
					bool flag3 = (item.keyCode_1 == item2.keyCode_1 && item2.keyCode_1 != 0) || (item.keyCode_1 == item2.keyCode_0 && item2.keyCode_0 != KeyCode.None);
					if (flag2 || flag3)
					{
						flag = true;
					}
				}
			}
		}
		if (flag)
		{
			InputManager.SetupDefaults(string.Empty);
			InputManager.Save();
			KeyboardController.KeyboardController_0.Dispatch(KeyboardControllerEvent.SETTINGS_CONTROL_CHANGED);
			NotificationController.NotificationController_0.Push(NotificationType.NOTIFICATION_SETTINGS_CHANGED, null);
		}
	}

	private void Update()
	{
		if (bool_1 && !ShopIsActive())
		{
			PresetGameSettings.PresetGameSettings_0.Save();
			bool_1 = false;
			mainPanel.SetActive(true);
			base.gameObject.SetActive(false);
			rotateCamera.OnMainMenuCloseOptions();
			return;
		}
		float num = sensitivitySlider.Single_0 * (float)(int_1 - int_0);
		float num2 = Mathf.Clamp(num + (float)int_0, int_0, int_1);
		if (float_0 != num2)
		{
			if (Application.isEditor)
			{
				Debug.Log("New sensitivity: " + num2);
			}
			PresetGameSettings.PresetGameSettings_0.Single_0 = num2;
			float_0 = num2;
		}
	}

	private void LateUpdate()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			bool_1 = true;
		}
	}

	private void HandleBackFromSettings(object sender, EventArgs e)
	{
		bool_1 = true;
	}

	private void HandleControlsClicked(object sender, EventArgs e)
	{
	}

	private void HandleRestoreClicked(object sender, EventArgs e)
	{
		if (Application.isEditor)
		{
			Debug.Log("[Restore] button clicked.");
		}
	}

	private IEnumerator RestoreProgressIndicator(float float_1)
	{
		yield return new WaitForSeconds(float_1);
		StoreKitEventListener.bool_0 = false;
	}

	private bool ShopIsActive()
	{
		return false;
	}

	public void OnResolutionChange()
	{
		resolution.String_0 = resolutionPopup.String_0;
		GraphicsController.GraphicsController_0.String_0 = resolutionPopup.String_0;
		resolutionPopup.Close();
		Log.AddLine("SettingsController::OnResolutionChange > resolution: " + resolutionPopup.String_0);
	}

	public void OnResolutionButtonClick()
	{
		resolutionPopup.gameObject.SendMessage("OnClick");
	}

	public void OnWindowModeChange()
	{
		GraphicsController.GraphicsController_0.Boolean_0 = windowMode.Boolean_0;
		Log.AddLine("SettingsController::OnWindowModeChange > isWindowMode: " + windowMode.Boolean_0);
	}

	public static void ResetSettings()
	{
		InputManager.SetupDefaults(string.Empty);
		InputManager.Save();
		GraphicsController.GraphicsController_0.SetDefaultSettings();
		Defs.Boolean_1 = true;
		Storager.SetBool(Defs.String_1, true);
		Storager.SetBool(Defs.String_2, true);
		Storager.SetInt(Defs.string_2, 0);
		Storager.Save();
		PresetGameSettings.PresetGameSettings_0.Single_0 = 50f;
		PresetGameSettings.PresetGameSettings_0.Save();
		Storager.DeleteAll();
		HideStuffSettings.HideStuffSettings_0.Clear();
		SharedSettings.SharedSettings_0.Save();
	}
}
