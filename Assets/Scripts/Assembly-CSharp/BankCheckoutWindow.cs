using engine.controllers;
using engine.unity;

[GameWindowParams(GameWindowType.BankCheckoutWindow)]
public class BankCheckoutWindow : BaseGameWindow
{
	private static BankCheckoutWindow bankCheckoutWindow_0;

	public UILabel uilabel_0;

	public UILabel uilabel_1;

	public UISprite uisprite_0;

	public UILabel uilabel_2;

	private BankPositionData bankPositionData_0;

	public static BankCheckoutWindow BankCheckoutWindow_0
	{
		get
		{
			return bankCheckoutWindow_0;
		}
	}

	public static void Show(BankCheckoutWindowParams bankCheckoutWindowParams_0 = null)
	{
		if (!(bankCheckoutWindow_0 != null))
		{
			bankCheckoutWindow_0 = BaseWindow.Load("BankCheckoutWindow") as BankCheckoutWindow;
			bankCheckoutWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			bankCheckoutWindow_0.Parameters_0.bool_5 = false;
			bankCheckoutWindow_0.Parameters_0.bool_0 = true;
			bankCheckoutWindow_0.Parameters_0.bool_6 = true;
			bankCheckoutWindow_0.InternalShow(bankCheckoutWindowParams_0);
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
		bankCheckoutWindow_0 = null;
	}

	private void Init()
	{
		BankCheckoutWindowParams bankCheckoutWindowParams = base.WindowShowParameters_0 as BankCheckoutWindowParams;
		if (bankCheckoutWindowParams != null)
		{
			bankPositionData_0 = bankCheckoutWindowParams.BankPositionData_0;
			uilabel_0.String_0 = bankPositionData_0.Int32_1.ToString();
			uilabel_1.String_0 = ((bankPositionData_0.Int32_2 <= 0) ? string.Empty : string.Format("+{0}", bankPositionData_0.Int32_2));
			uilabel_2.String_0 = string.Format("${0,2}", bankPositionData_0.Single_0);
		}
	}

	private void GoBank(string string_0)
	{
		BankController.BankController_0.GoToBank(bankPositionData_0.Int32_1, string_0);
		Hide();
		if (BankWindow.BankWindow_0 != null && AppStateController.AppStateController_0.States_0 < AppStateController.States.JOINED_ROOM)
		{
			BankWindow.BankWindow_0.Hide();
		}
	}

	public void OnCreditCardMethodClick()
	{
		GoBank("1380");
	}

	public void OnPayPalMethodClick()
	{
		GoBank("24");
	}

	public void OnGlobalMethodClick()
	{
		GoBank(string.Empty);
	}
}
