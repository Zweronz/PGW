using UnityEngine;
using engine.unity;

public class MessageWindowExit : MessageWindow
{
	public UIButton uibutton_2;

	public UILabel uilabel_2;

	private static MessageWindowExit messageWindowExit_0;

	public static MessageWindowExit MessageWindowExit_0
	{
		get
		{
			return messageWindowExit_0;
		}
	}

	public new static void Show(MessageWindowParams messageWindowParams_0)
	{
		if (!(messageWindowExit_0 != null))
		{
			messageWindowExit_0 = BaseWindow.Load("MessageWindowExit") as MessageWindowExit;
			messageWindowExit_0.Parameters_0.type_0 = WindowQueue.Type.New;
			messageWindowExit_0.Parameters_0.bool_5 = true;
			messageWindowExit_0.Parameters_0.bool_0 = true;
			messageWindowExit_0.Parameters_0.bool_6 = true;
			messageWindowExit_0.InternalShow(messageWindowParams_0);
		}
	}

	protected override void InitParams()
	{
		base.InitParams();
		AddInputKey(KeyCode.Escape, base.Hide);
	}

	public virtual void OnCancelButton()
	{
		Hide();
	}
}
