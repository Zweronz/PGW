using engine.unity;

public class SkinSaveWindow : MessageWindow
{
	public UIInput uiinput_0;

	public UILabel uilabel_2;

	public UILabel uilabel_3;

	private static SkinSaveWindow skinSaveWindow_0;

	public static SkinSaveWindow SkinSaveWindow_0
	{
		get
		{
			return skinSaveWindow_0;
		}
	}

	public new static void Show(MessageWindowParams messageWindowParams_0 = null)
	{
		if (!(skinSaveWindow_0 != null))
		{
			skinSaveWindow_0 = BaseWindow.Load("SkinSaveWindow") as SkinSaveWindow;
			skinSaveWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			skinSaveWindow_0.Parameters_0.bool_5 = true;
			skinSaveWindow_0.Parameters_0.bool_0 = false;
			skinSaveWindow_0.Parameters_0.bool_6 = false;
			skinSaveWindow_0.InternalShow(messageWindowParams_0);
		}
	}

	public void OnSubmitInputSkin()
	{
		OnOKButton();
	}

	public void OnChangeInputSkin()
	{
		UpdateWarn();
	}

	protected override void InitParams()
	{
		base.InitParams();
		NGUITools.SetActive(uilabel_2.gameObject, false);
		string text = SkinsController.CustomSkinName(SkinEditController.SkinEditController_0.int_0);
		if (!string.IsNullOrEmpty(text))
		{
			uiinput_0.String_2 = text;
		}
		uiinput_0.Boolean_2 = true;
		UpdateWarn();
	}

	protected override void OnOKButton()
	{
		string text = uiinput_0.String_2.Trim();
		if (!string.IsNullOrEmpty(text) && !text.Equals(uiinput_0.String_0))
		{
			SkinEditController.SkinEditController_0.Save(text);
			Hide();
		}
	}

	private void UpdateWarn()
	{
		string text = uiinput_0.String_2.Trim();
		bool flag = string.IsNullOrEmpty(text) || text.Equals(uiinput_0.String_0);
		uibutton_1.Boolean_0 = !flag;
		NGUITools.SetActive(uilabel_3.gameObject, flag);
	}
}
