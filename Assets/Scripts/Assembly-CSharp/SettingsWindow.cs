using UnityEngine;
using engine.helpers;
using engine.unity;

[GameWindowParams(GameWindowType.Settings)]
public class SettingsWindow : BaseGameWindow
{
	private static SettingsWindow settingsWindow_0;

	public UILabel uilabel_0;

	public UIPopupList uipopupList_0;

	public SettingsToggle settingsToggle_0;

	public SettingsLanguageToggle[] settingsLanguageToggle_0;

	public UIWidget uiwidget_0;

	private GameObject gameObject_0;

	public static SettingsWindow SettingsWindow_0
	{
		get
		{
			return settingsWindow_0;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return SettingsWindow_0 != null && SettingsWindow_0.Boolean_0;
		}
	}

	public static void Show(WindowShowParameters windowShowParameters_1 = null)
	{
		if (!(settingsWindow_0 != null))
		{
			settingsWindow_0 = BaseWindow.Load("SettingsWindow") as SettingsWindow;
			settingsWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			settingsWindow_0.Parameters_0.bool_5 = false;
			settingsWindow_0.Parameters_0.bool_0 = false;
			settingsWindow_0.Parameters_0.bool_6 = true;
			settingsWindow_0.InternalShow(windowShowParameters_1);
		}
	}

	public override void OnShow()
	{
		Init();
		base.OnShow();
	}

	public override void OnHide()
	{
		base.OnHide();
		PresetGameSettings.PresetGameSettings_0.Save();
		settingsWindow_0 = null;
		AnimateCamera(false);
		if (!SettingsControlWindow.Boolean_3)
		{
			Lobby.Lobby_0.Show();
		}
	}

	private void Init()
	{
		InitSettingsParams();
		gameObject_0 = GameObject.Find("Camera_Rotate");
		gameObject_0.GetComponent<Animation>().enabled = true;
		AnimateCamera(true);
	}

	private void AnimateCamera(bool bool_1)
	{
		gameObject_0.GetComponent<Animation>().Play((!bool_1) ? "SettingsCloseCamera" : "SettingsOpenCamera");
	}

	private void InitSettingsParams()
	{
		uipopupList_0.items = GraphicsController.GraphicsController_0.list_2;
		UIPopupList uIPopupList = uipopupList_0;
		string string_ = GraphicsController.GraphicsController_0.String_0;
		uilabel_0.String_0 = string_;
		uIPopupList.String_0 = string_;
		settingsToggle_0.Boolean_0 = GraphicsController.GraphicsController_0.Boolean_0;
		SettingsLanguageToggle[] array = settingsLanguageToggle_0;
		foreach (SettingsLanguageToggle settingsLanguageToggle in array)
		{
			settingsLanguageToggle.Boolean_0 = settingsLanguageToggle.code.Equals(LocalizationStore.String_44);
		}
	}

	protected override void Update()
	{
		base.Update();
		float num = uiwidget_0.transform.parent.GetComponent<UIWidget>().Int32_0;
		uiwidget_0.transform.localPosition = new Vector3(num / 2f - (float)(uiwidget_0.Int32_0 / 2) - 40f, -15f, uiwidget_0.transform.localPosition.z);
	}

	public void OnResolutionChange()
	{
		uilabel_0.String_0 = uipopupList_0.String_0;
		GraphicsController.GraphicsController_0.String_0 = uipopupList_0.String_0;
		uipopupList_0.Close();
		if (Application.isEditor)
		{
			Log.AddLine("SettingsWindow::OnResolutionChange > resolution: " + uipopupList_0.String_0);
		}
	}

	public void OnResolutionButtonClick()
	{
		uipopupList_0.gameObject.SendMessage("OnClick");
	}

	public void OnWindowModeChange()
	{
		GraphicsController.GraphicsController_0.Boolean_0 = settingsToggle_0.Boolean_0;
		if (Application.isEditor)
		{
			Log.AddLine("SettingsWindow::OnWindowModeChange > mode: " + settingsToggle_0.Boolean_0);
		}
	}

	public void OnControlsButtonClick()
	{
		SettingsControlWindowParams settingsControlWindowParams = new SettingsControlWindowParams();
		settingsControlWindowParams.Boolean_0 = true;
		SettingsControlWindow.Show(settingsControlWindowParams);
		Hide();
	}

	public void OnLanguageClick()
	{
		SettingsLanguageToggle[] array = settingsLanguageToggle_0;
		foreach (SettingsLanguageToggle settingsLanguageToggle in array)
		{
			settingsLanguageToggle.Boolean_0 = false;
		}
	}
}
