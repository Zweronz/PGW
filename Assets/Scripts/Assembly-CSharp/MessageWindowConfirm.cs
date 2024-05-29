using System;
using engine.unity;

public class MessageWindowConfirm : MessageWindow
{
	private static MessageWindowConfirm messageWindowConfirm_0;

	public UIButton uibutton_2;

	public UILabel uilabel_2;

	protected Action action_1;

	public static MessageWindowConfirm MessageWindowConfirm_0
	{
		get
		{
			return messageWindowConfirm_0;
		}
	}

	public new static void Show(MessageWindowParams messageWindowParams_0 = null)
	{
		if (!(messageWindowConfirm_0 != null))
		{
			messageWindowConfirm_0 = BaseWindow.Load("MessageWindowConfirm") as MessageWindowConfirm;
			messageWindowConfirm_0.Parameters_0.type_0 = WindowQueue.Type.New;
			messageWindowConfirm_0.Parameters_0.bool_5 = true;
			messageWindowConfirm_0.Parameters_0.bool_0 = true;
			messageWindowConfirm_0.Parameters_0.bool_6 = true;
			messageWindowConfirm_0.InternalShow(messageWindowParams_0);
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
			MessageWindowConfirmParams messageWindowConfirmParams = (MessageWindowConfirmParams)base.WindowShowParameters_0;
			uilabel_2.String_0 = ((!string.IsNullOrEmpty(messageWindowConfirmParams.string_2)) ? messageWindowConfirmParams.string_2 : LocalizationStorage.Get.Term("ui.common.btn.cancel"));
			action_1 = messageWindowConfirmParams.action_1;
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
