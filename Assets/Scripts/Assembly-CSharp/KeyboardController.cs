using System.Collections.Generic;
using engine.events;
using engine.unity;

public class KeyboardController : BaseEvent
{
	private static KeyboardController keyboardController_0 = null;

	private static HashSet<string> hashSet_0 = new HashSet<string>
	{
		"Tab", "Next", "Accept", "Back", "Cursor", "ShowBugReport", "ShowTeamCommands", "Seat", "Crawling", "AddHealth",
		"AddAmmo", "Pause", "Option1", "Option5"
	};

	public static KeyboardController KeyboardController_0
	{
		get
		{
			if (keyboardController_0 == null)
			{
				keyboardController_0 = new KeyboardController();
			}
			return keyboardController_0;
		}
	}

	private KeyboardController()
	{
	}

	public List<SettingsControlItemData> GetControlItems()
	{
		SettingsControlItemData settingsControlItemData = null;
		List<SettingsControlItemData> list = new List<SettingsControlItemData>();
		foreach (KeyValuePair<string, InputManager.AxisState> item in InputManager.dictionary_1)
		{
			if (!hashSet_0.Contains(item.Key))
			{
				settingsControlItemData = new SettingsControlItemData();
				settingsControlItemData.string_1 = item.Key;
				settingsControlItemData.string_0 = LocalizationStorage.Get.Term(string.Format("{0}.{1}.{2}", "window.settings.controls.actions", item.Key.ToLower(), 1));
				settingsControlItemData.keyCode_0 = item.Value.keyCode_0;
				settingsControlItemData.keyCode_1 = item.Value.keyCode_1;
				settingsControlItemData.int_0 = 1;
				list.Add(settingsControlItemData);
				settingsControlItemData = new SettingsControlItemData();
				settingsControlItemData.string_1 = item.Key;
				settingsControlItemData.string_0 = LocalizationStorage.Get.Term(string.Format("{0}.{1}.{2}", "window.settings.controls.actions", item.Key.ToLower(), 2));
				settingsControlItemData.keyCode_0 = item.Value.keyCode_2;
				settingsControlItemData.keyCode_1 = item.Value.keyCode_3;
				settingsControlItemData.int_0 = 2;
				list.Add(settingsControlItemData);
			}
		}
		foreach (KeyValuePair<string, InputManager.ButtonState> item2 in InputManager.dictionary_0)
		{
			if (!hashSet_0.Contains(item2.Key))
			{
				settingsControlItemData = new SettingsControlItemData();
				settingsControlItemData.string_1 = item2.Key;
				settingsControlItemData.string_0 = LocalizationStorage.Get.Term(string.Format("window.settings.controls.actions.{0}", item2.Key.ToLower()));
				settingsControlItemData.keyCode_0 = item2.Value.keyCode_0;
				settingsControlItemData.keyCode_1 = item2.Value.keyCode_1;
				list.Add(settingsControlItemData);
			}
		}
		return list;
	}
}
