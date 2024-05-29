using System;
using UnityEngine;
using engine.unity;

public class SettingsControlItemSlot : MonoBehaviour
{
	private const string string_0 = "Alpha";

	public UILabel keyCode;

	public UILabel waitLabel;

	public UISprite slotBack;

	public Color[] colors;

	private SettingsControlItemSlotData settingsControlItemSlotData_0;

	private Action<KeyCode, bool> action_0;

	private SettingControlItemState settingControlItemState_0;

	public bool editing;

	private bool bool_0;

	private int int_0;

	public SettingsControlItemSlotData SettingsControlItemSlotData_0
	{
		get
		{
			return settingsControlItemSlotData_0;
		}
	}

	public int Int32_0
	{
		get
		{
			return int_0;
		}
	}

	private void Start()
	{
		NGUITools.SetActive(waitLabel.gameObject, false);
	}

	private void LateUpdate()
	{
		if (editing && Input.GetKeyUp(KeyCode.Escape))
		{
			editing = false;
			SettingsControlWindow.SettingsControlWindow_0.Boolean_2 = true;
			UpdateSlot();
		}
	}

	public void SetSlotData(SettingsControlItemSlotData settingsControlItemSlotData_1, int int_1, Action<KeyCode, bool> action_1)
	{
		settingsControlItemSlotData_0 = settingsControlItemSlotData_1;
		int_0 = int_1;
		action_0 = action_1;
		UpdateSlot();
		UpdateStateColor();
	}

	public void UpdateSlot()
	{
		if (settingsControlItemSlotData_0 != null)
		{
			keyCode.String_0 = GetKeyText();
			NGUITools.SetActive(keyCode.gameObject, !editing);
			NGUITools.SetActive(waitLabel.gameObject, editing);
		}
	}

	public void SetSlotState(SettingControlItemState settingControlItemState_1)
	{
		if (settingControlItemState_0 != SettingControlItemState.BLOCKED)
		{
			settingControlItemState_0 = settingControlItemState_1;
			UpdateStateColor();
		}
	}

	private string GetKeyText()
	{
		if (settingsControlItemSlotData_0 == null)
		{
			return string.Empty;
		}
		return GetKeyText(settingsControlItemSlotData_0.keyCode_0);
	}

	public static string GetKeyText(KeyCode keyCode_0)
	{
		if (keyCode_0 == KeyCode.None)
		{
			return LocalizationStore.Get("window.settings.controls.actions.none");
		}
		string text = keyCode_0.ToString();
		if (text.Contains("Alpha"))
		{
			text = text.Substring("Alpha".Length, text.Length - "Alpha".Length);
		}
		return text;
	}

	private void UpdateStateColor()
	{
		if (settingsControlItemSlotData_0 != null)
		{
			Color color_ = keyCode.Color_1;
			Color color_2 = keyCode.Color_2;
			switch (settingControlItemState_0)
			{
			case SettingControlItemState.NORMAL:
				color_ = colors[0];
				color_2 = colors[1];
				slotBack.String_0 = ((settingsControlItemSlotData_0.keyCode_0 == KeyCode.None) ? "settings_btn_grey_active" : ((!settingsControlItemSlotData_0.bool_0) ? "settings_btn_yellow_active" : "settings_btn_green_active"));
				break;
			case SettingControlItemState.EDITED:
				color_ = colors[2];
				color_2 = colors[3];
				break;
			case SettingControlItemState.CONFLICT:
				color_ = colors[4];
				color_2 = colors[5];
				slotBack.String_0 = "settings_btn_red_active";
				break;
			case SettingControlItemState.BLOCKED:
				color_ = colors[0];
				color_2 = colors[1];
				slotBack.String_0 = "settings_btn_grey_inactive";
				break;
			}
			keyCode.Color_1 = color_;
			keyCode.Color_2 = color_2;
		}
	}

	private void OnKeyChanged(KeyCode keyCode_0)
	{
		if (settingsControlItemSlotData_0 != null)
		{
			settingsControlItemSlotData_0.keyCode_0 = keyCode_0;
			editing = false;
			bool_0 = true;
			UpdateSlot();
			if (action_0 != null)
			{
				action_0(keyCode_0, settingsControlItemSlotData_0.bool_0);
			}
		}
	}

	private void OnClick()
	{
		if ((!(SettingsControlWindow.SettingsControlWindow_0 != null) || !SettingsControlWindow.SettingsControlWindow_0.Boolean_1 || settingControlItemState_0 == SettingControlItemState.CONFLICT) && settingControlItemState_0 != SettingControlItemState.BLOCKED)
		{
			SettingsControlWindow.SettingsControlWindow_0.Boolean_2 = false;
			editing = true;
			UpdateSlot();
			InputManager.GetAnyButton(OnKeyChanged);
		}
	}
}
