using UnityEngine;
using engine.unity;

public class KeyCodeIndicatior : MonoBehaviour
{
	private const string string_0 = "Alpha";

	public UILabel keyCode;

	public KeyCodeIndicatorType type;

	private void Start()
	{
		UpdateIndicator();
		KeyboardController.KeyboardController_0.Subscribe(UpdateIndicator, KeyboardControllerEvent.SETTINGS_CONTROL_CHANGED);
	}

	private void OnDestroy()
	{
		KeyboardController.KeyboardController_0.Unsubscribe(UpdateIndicator, KeyboardControllerEvent.SETTINGS_CONTROL_CHANGED);
	}

	private void UpdateIndicator()
	{
		KeyCode keyCode_ = KeyCode.None;
		switch (type)
		{
		case KeyCodeIndicatorType.WEAPON_PRIMARY:
			keyCode_ = GetKeyCode("Weapon1");
			break;
		case KeyCodeIndicatorType.WEAPON_BACKUP:
			keyCode_ = GetKeyCode("Weapon2");
			break;
		case KeyCodeIndicatorType.WEAPON_MELEE:
			keyCode_ = GetKeyCode("Weapon6");
			break;
		case KeyCodeIndicatorType.WEAPON_SPECIAL:
			keyCode_ = GetKeyCode("Weapon3");
			break;
		case KeyCodeIndicatorType.WEAPON_PREMIUM:
			keyCode_ = GetKeyCode("Weapon4");
			break;
		case KeyCodeIndicatorType.CONSUMABLE_INVISIBLE:
			keyCode_ = GetKeyCode("Option2");
			break;
		case KeyCodeIndicatorType.CONSUMABLE_TURRET:
			keyCode_ = GetKeyCode("Option3");
			break;
		case KeyCodeIndicatorType.CONSUMABLE_JETPACK:
			keyCode_ = GetKeyCode("Option4");
			break;
		case KeyCodeIndicatorType.CONSUMABLE_ROBOT:
			keyCode_ = GetKeyCode("Option5");
			break;
		case KeyCodeIndicatorType.ADD_AMMO:
			keyCode_ = GetKeyCode("AddAmmo");
			break;
		case KeyCodeIndicatorType.ADD_HEALTH:
			keyCode_ = GetKeyCode("AddHealth");
			break;
		case KeyCodeIndicatorType.USE_GRENADE:
			keyCode_ = GetKeyCode("Grenade");
			break;
		case KeyCodeIndicatorType.TAB:
			keyCode_ = GetKeyCode("Tab");
			break;
		case KeyCodeIndicatorType.WEAPON_SNIPER:
			keyCode_ = GetKeyCode("Weapon5");
			break;
		}
		keyCode.String_0 = GetKeyText(keyCode_);
	}

	private string GetKeyText(KeyCode keyCode_0)
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

	private KeyCode GetKeyCode(string string_1)
	{
		return InputManager.dictionary_0[string_1].keyCode_0;
	}

	private KeyCode GetAltKeyCode(string string_1)
	{
		return InputManager.dictionary_0[string_1].keyCode_1;
	}
}
