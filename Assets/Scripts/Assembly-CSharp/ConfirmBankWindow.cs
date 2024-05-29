using engine.unity;

[GameWindowParams(GameWindowType.ConfirmBankWindow)]
public class ConfirmBankWindow : BaseGameWindow
{
	private static ConfirmBankWindow confirmBankWindow_0;

	public static ConfirmBankWindow ConfirmBankWindow_0
	{
		get
		{
			return confirmBankWindow_0;
		}
	}

	public static void Show(ExitGameWindowParams exitGameWindowParams_0 = null)
	{
		if (!(confirmBankWindow_0 != null))
		{
			confirmBankWindow_0 = BaseWindow.Load("ConfirmBankWindow") as ConfirmBankWindow;
			confirmBankWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			confirmBankWindow_0.Parameters_0.bool_5 = true;
			confirmBankWindow_0.Parameters_0.bool_0 = false;
			confirmBankWindow_0.Parameters_0.bool_6 = true;
			confirmBankWindow_0.InternalShow(exitGameWindowParams_0);
		}
	}

	public override void OnShow()
	{
		base.OnShow();
	}

	public override void OnHide()
	{
		base.OnHide();
		confirmBankWindow_0 = null;
	}

	public void OnClickOk()
	{
		Hide();
		BankController.BankController_0.TryOpenBank(BankController.BankSourceType.BANK_SHOP);
	}
}
