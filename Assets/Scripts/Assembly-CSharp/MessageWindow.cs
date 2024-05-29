using System;
using UnityEngine;
using engine.helpers;
using engine.unity;
using pixelgun.tutorial;

[GameWindowParams(GameWindowType.Message)]
public class MessageWindow : BaseGameWindow
{
	public UILabel uilabel_0;

	public UIButton uibutton_0;

	public UIButton uibutton_1;

	public UILabel uilabel_1;

	private static MessageWindow messageWindow_0;

	protected Action action_0;

	public static MessageWindow MessageWindow_0
	{
		get
		{
			if (messageWindow_0 == null)
			{
				messageWindow_0 = new MessageWindow();
			}
			return messageWindow_0;
		}
	}

	public static void Show(MessageWindowParams messageWindowParams_0)
	{
		if (!(messageWindow_0 != null))
		{
			messageWindow_0 = BaseWindow.Load("MessageWindow") as MessageWindow;
			messageWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			messageWindow_0.Parameters_0.bool_5 = true;
			messageWindow_0.Parameters_0.bool_0 = true;
			messageWindow_0.Parameters_0.bool_6 = false;
			if (Tutorial.Tutorial_0 != null)
			{
				messageWindow_0.Parameters_0.gameEvent_0 = WindowController.GameEvent.TUTORIAL_END;
			}
			else if (messageWindowParams_0 != null)
			{
				messageWindow_0.Parameters_0.gameEvent_0 = messageWindowParams_0.gameEvent_0;
			}
			messageWindow_0.InternalShow(messageWindowParams_0);
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
		messageWindow_0 = null;
	}

	protected virtual void Init()
	{
		InitUI();
		InitParams();
		if (uibutton_1.list_0.Count > 0)
		{
			Log.AddLineWarning("[MessageWindow::Init] okButton already have delegate");
		}
		if (uibutton_0.list_0.Count > 0)
		{
			Log.AddLineWarning("[MessageWindow::Init] closeButton already have delegate");
		}
		uibutton_1.list_0.Add(new EventDelegate(OnOKButton));
		uibutton_0.list_0.Add(new EventDelegate(OnCloseButton));
		if (TutorialController.TutorialController_0.Boolean_0)
		{
			TutorialController.TutorialController_0.HideTutor();
		}
	}

	protected virtual void InitUI()
	{
		if (uibutton_0 != null)
		{
			NGUITools.SetActive(uibutton_0.gameObject, false);
		}
	}

	protected virtual void InitParams()
	{
		if (base.WindowShowParameters_0 != null)
		{
			Screen.lockCursor = false;
			MessageWindowParams messageWindowParams = (MessageWindowParams)base.WindowShowParameters_0;
			uilabel_1.String_0 = ((!string.IsNullOrEmpty(messageWindowParams.string_1)) ? messageWindowParams.string_1 : LocalizationStorage.Get.Term("ui.common.btn.ok"));
			uilabel_0.String_0 = messageWindowParams.string_0;
			action_0 = messageWindowParams.action_0;
			if (messageWindowParams.keyCode_0 != 0)
			{
				AddInputKey(messageWindowParams.keyCode_0, OnOKButton);
			}
		}
	}

	public virtual void OnCloseButton()
	{
		Hide();
	}

	protected virtual void OnOKButton()
	{
		Hide();
		if (action_0 != null)
		{
			action_0();
		}
	}
}
