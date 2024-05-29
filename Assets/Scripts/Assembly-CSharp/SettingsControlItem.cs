using System;
using UnityEngine;

public class SettingsControlItem : MonoBehaviour
{
	public UILabel actionLabel;

	public SettingsControlItemSlot keySlot;

	public SettingsControlItemSlot altKeySlot;

	public Color[] colors;

	private SettingsControlItemData settingsControlItemData_0;

	private Action<SettingsControlItemData> action_0;

	private SettingControlItemState settingControlItemState_0;

	private bool bool_0;

	private int int_0;

	public SettingsControlItemData SettingsControlItemData_0
	{
		get
		{
			return settingsControlItemData_0;
		}
	}

	public int Int32_0
	{
		get
		{
			return (settingsControlItemData_0 != null) ? settingsControlItemData_0.int_1 : 0;
		}
	}

	public void SetItemData(SettingsControlItemData settingsControlItemData_1, Action<SettingsControlItemData> action_1)
	{
		settingsControlItemData_0 = settingsControlItemData_1;
		action_0 = action_1;
		UpdateItem();
	}

	public void UpdateItem()
	{
		if (settingsControlItemData_0 != null)
		{
			SettingsControlItemSlotData settingsControlItemSlotData = null;
			actionLabel.String_0 = settingsControlItemData_0.string_0;
			UpdateStateColor();
			settingsControlItemSlotData = new SettingsControlItemSlotData(settingsControlItemData_0.keyCode_0, false);
			keySlot.SetSlotData(settingsControlItemSlotData, int_0, OnSlotKeyChange);
			settingsControlItemSlotData = new SettingsControlItemSlotData(settingsControlItemData_0.keyCode_1, true);
			altKeySlot.SetSlotData(settingsControlItemSlotData, int_0, OnSlotKeyChange);
		}
	}

	public void SetItemState(SettingControlItemState settingControlItemState_1, KeyCode keyCode_0 = KeyCode.None)
	{
		settingControlItemState_0 = settingControlItemState_1;
		UpdateStateColor();
		keySlot.SetSlotState((keyCode_0 != 0 && keySlot.SettingsControlItemSlotData_0.keyCode_0 == keyCode_0) ? SettingControlItemState.CONFLICT : SettingControlItemState.NORMAL);
		altKeySlot.SetSlotState((keyCode_0 != 0 && altKeySlot.SettingsControlItemSlotData_0.keyCode_0 == keyCode_0) ? SettingControlItemState.CONFLICT : SettingControlItemState.NORMAL);
	}

	private void UpdateStateColor()
	{
		Color color_ = actionLabel.Color_1;
		Color color_2 = actionLabel.Color_2;
		if (settingControlItemState_0 == SettingControlItemState.NORMAL && bool_0)
		{
			settingControlItemState_0 = SettingControlItemState.EDITED;
		}
		switch (settingControlItemState_0)
		{
		case SettingControlItemState.NORMAL:
			color_ = colors[0];
			color_2 = colors[1];
			break;
		case SettingControlItemState.EDITED:
			color_ = colors[2];
			color_2 = colors[3];
			break;
		case SettingControlItemState.CONFLICT:
			color_ = colors[4];
			color_2 = colors[5];
			break;
		}
		actionLabel.Color_1 = color_;
		actionLabel.Color_2 = color_2;
	}

	private void OnSlotKeyChange(KeyCode keyCode_0, bool bool_1)
	{
		if (settingsControlItemData_0 != null)
		{
			bool_0 = true;
			settingsControlItemData_0.keyCode_0 = (bool_1 ? settingsControlItemData_0.keyCode_0 : keyCode_0);
			settingsControlItemData_0.keyCode_1 = ((!bool_1) ? settingsControlItemData_0.keyCode_1 : keyCode_0);
			if (action_0 != null)
			{
				action_0(settingsControlItemData_0);
			}
		}
	}
}
