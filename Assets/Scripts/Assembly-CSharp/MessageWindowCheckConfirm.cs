using System;
using engine.unity;

public class MessageWindowCheckConfirm : MessageWindow
{
	private static MessageWindowCheckConfirm messageWindowCheckConfirm_0;

	public UIButton uibutton_2;

	public UILabel uilabel_2;

	public UIToggle uitoggle_0;

	protected Action action_1;

	public static MessageWindowCheckConfirm MessageWindowCheckConfirm_0
	{
		get
		{
			return messageWindowCheckConfirm_0;
		}
	}

	public new static void Show(MessageWindowParams messageWindowParams_0 = null)
	{
		if (!(messageWindowCheckConfirm_0 != null))
		{
			messageWindowCheckConfirm_0 = BaseWindow.Load("MessageWindowCheckConfirm") as MessageWindowCheckConfirm;
			messageWindowCheckConfirm_0.Parameters_0.type_0 = WindowQueue.Type.New;
			messageWindowCheckConfirm_0.Parameters_0.bool_5 = true;
			messageWindowCheckConfirm_0.Parameters_0.bool_0 = true;
			messageWindowCheckConfirm_0.Parameters_0.bool_6 = true;
			messageWindowCheckConfirm_0.InternalShow(messageWindowParams_0);
		}
	}

	protected override void Init()
	{
		base.Init();
		uibutton_2.list_0.Add(new EventDelegate(OnCancelButton));
	}

	protected override void InitParams()
	{
		base.InitParams();
		if (base.WindowShowParameters_0 != null)
		{
			MessageWindowCheckConfirmParams messageWindowCheckConfirmParams = (MessageWindowCheckConfirmParams)base.WindowShowParameters_0;
			uilabel_2.String_0 = ((!string.IsNullOrEmpty(messageWindowCheckConfirmParams.string_2)) ? messageWindowCheckConfirmParams.string_2 : LocalizationStorage.Get.Term("ui.common.btn.cancel"));
			action_1 = messageWindowCheckConfirmParams.action_1;
			uitoggle_0.Boolean_0 = false;
		}
	}

	protected virtual void OnCancelButton()
	{
		Hide();
		if (action_1 != null)
		{
			action_1();
		}
	}
}
