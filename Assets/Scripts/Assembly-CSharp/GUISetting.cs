using UnityEngine;

public sealed class GUISetting : MonoBehaviour
{
	public GUIStyle back;

	public GUIStyle soundOnOff;

	public GUIStyle restore;

	public GUIStyle sliderStyle;

	public GUIStyle thumbStyle;

	public Texture settingPlashka;

	public Texture settingTitle;

	public Texture fon;

	public Texture slow_fast;

	public Texture polzunok;

	private float float_0;

	private void Awake()
	{
	}

	private void OnGUI()
	{
		GUI.depth = 2;
		float num = (float)Screen.height / 768f;
		GUI.DrawTexture(new Rect((float)Screen.width / 2f - 683f * num, 0f, 1366f * num, Screen.height), fon);
		GUI.DrawTexture(new Rect((float)Screen.width / 2f - (float)settingPlashka.width * num * 0.5f, (float)Screen.height * 0.52f - (float)settingPlashka.height * num * 0.5f, (float)settingPlashka.width * num, (float)settingPlashka.height * num), settingPlashka);
		GUI.DrawTexture(new Rect((float)Screen.width / 2f - (float)settingTitle.width / 2f * Defs.Single_0, (float)Screen.height * 0.08f, (float)settingTitle.width * Defs.Single_0, (float)settingTitle.height * Defs.Single_0), settingTitle);
		Rect position = new Rect((float)Screen.width * 0.5f - (float)soundOnOff.normal.background.width * 0.5f * num, (float)Screen.height * 0.52f - (float)soundOnOff.normal.background.height * 0.5f * num, (float)soundOnOff.normal.background.width * num, (float)soundOnOff.normal.background.height * num);
		bool @bool = Storager.GetBool(Defs.String_0, true);
		AudioListener.volume = ((@bool = GUI.Toggle(position, @bool, string.Empty, soundOnOff)) ? 1 : 0);
		Storager.SetBool(Defs.String_0, @bool);
		Storager.Save();
		Rect position2 = new Rect((float)Screen.width * 0.5f - (float)soundOnOff.normal.background.width * 0.5f * num, (float)Screen.height * 0.72f - (float)soundOnOff.normal.background.height * 0.5f * num, (float)soundOnOff.normal.background.width * num, (float)soundOnOff.normal.background.height * num);
		bool boolean_ = Defs.Boolean_1;
		boolean_ = (Defs.Boolean_1 = GUI.Toggle(position2, boolean_, string.Empty, soundOnOff));
		Storager.Save();
		if (GUI.Button(new Rect(21f * num, (float)Screen.height - (21f + (float)back.normal.background.height) * num, (float)back.normal.background.width * num, (float)back.normal.background.height * num), string.Empty, back))
		{
			FlurryPluginWrapper.LogEvent("Back to Main Menu");
			Application.LoadLevel(Defs.String_11);
		}
		GUI.enabled = !StoreKitEventListener.bool_0;
		GUI.enabled = true;
		sliderStyle.fixedWidth = (float)slow_fast.width * num;
		sliderStyle.fixedHeight = (float)slow_fast.height * num;
		thumbStyle.fixedWidth = (float)polzunok.width * num;
		thumbStyle.fixedHeight = (float)polzunok.height * num;
		Rect position3 = new Rect((float)Screen.width * 0.5f - (float)slow_fast.width * 0.5f * num, (float)Screen.height * 0.81f - (float)slow_fast.height * 0.5f * num, (float)slow_fast.width * num, (float)slow_fast.height * num);
		float_0 = GUI.HorizontalSlider(position3, PresetGameSettings.PresetGameSettings_0.Single_0, 6f, 18f, sliderStyle, thumbStyle);
		PresetGameSettings.PresetGameSettings_0.Single_0 = float_0;
	}

	private void OnDestroy()
	{
		PresetGameSettings.PresetGameSettings_0.Save();
	}
}
