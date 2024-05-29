using engine.unity;

[GameWindowParams(GameWindowType.MessageAnimateWindow)]
public class MessageAnimateWindow : BaseGameWindow
{
	private static MessageAnimateWindow messageAnimateWindow_0;

	public UILabel uilabel_0;

	public UILabel uilabel_1;

	public UILabel uilabel_2;

	public UILabel uilabel_3;

	public UILabel uilabel_4;

	public UIButton uibutton_0;

	public UIButton uibutton_1;

	public UIButton uibutton_2;

	private MessageAnimateWindowParams messageAnimateWindowParams_0;

	public static MessageAnimateWindow MessageAnimateWindow_0
	{
		get
		{
			return messageAnimateWindow_0;
		}
	}

	public static void Show(MessageAnimateWindowParams messageAnimateWindowParams_1 = null)
	{
		if (!(messageAnimateWindow_0 != null))
		{
			messageAnimateWindow_0 = BaseWindow.Load("MessageAnimateWindow") as MessageAnimateWindow;
			messageAnimateWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			messageAnimateWindow_0.Parameters_0.bool_5 = true;
			messageAnimateWindow_0.Parameters_0.bool_0 = true;
			messageAnimateWindow_0.Parameters_0.bool_6 = true;
			messageAnimateWindow_0.InternalShow(messageAnimateWindowParams_1);
		}
	}

	public override void OnShow()
	{
		base.OnShow();
		Init();
	}

	public override void OnHide()
	{
		base.OnHide();
		messageAnimateWindow_0 = null;
	}

	private void Init()
	{
		messageAnimateWindowParams_0 = base.WindowShowParameters_0 as MessageAnimateWindowParams;
		NGUITools.SetActive(uibutton_0.gameObject, messageAnimateWindowParams_0.MessageAnimateWindowType_0 == MessageAnimateWindowType.INFO);
		NGUITools.SetActive(uibutton_1.gameObject, messageAnimateWindowParams_0.MessageAnimateWindowType_0 == MessageAnimateWindowType.CONFIRM);
		NGUITools.SetActive(uibutton_2.gameObject, messageAnimateWindowParams_0.MessageAnimateWindowType_0 == MessageAnimateWindowType.CONFIRM);
		uilabel_2.String_0 = ((!string.IsNullOrEmpty(messageAnimateWindowParams_0.String_2)) ? Localizer.Get(messageAnimateWindowParams_0.String_2) : string.Empty);
		uilabel_3.String_0 = ((!string.IsNullOrEmpty(messageAnimateWindowParams_0.String_2)) ? Localizer.Get(messageAnimateWindowParams_0.String_2) : string.Empty);
		uilabel_4.String_0 = ((!string.IsNullOrEmpty(messageAnimateWindowParams_0.String_3)) ? Localizer.Get(messageAnimateWindowParams_0.String_3) : string.Empty);
		uilabel_0.String_0 = ((!string.IsNullOrEmpty(messageAnimateWindowParams_0.String_0)) ? Localizer.Get(messageAnimateWindowParams_0.String_0) : string.Empty);
		uilabel_1.String_0 = ((!string.IsNullOrEmpty(messageAnimateWindowParams_0.String_1)) ? Localizer.Get(messageAnimateWindowParams_0.String_1) : string.Empty);
	}

	public void OnOkButtonClick()
	{
		if (messageAnimateWindowParams_0.Action_0 != null)
		{
			messageAnimateWindowParams_0.Action_0();
		}
		Hide();
	}

	public void OnCancelButtonClick()
	{
		if (messageAnimateWindowParams_0.Action_1 != null)
		{
			messageAnimateWindowParams_0.Action_1();
		}
		Hide();
	}
}
