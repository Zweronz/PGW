using System;
using UnityEngine;
using engine.helpers;
using engine.unity;
using pixelgun.tutorial;

public class MessageWindowRename : MessageWindow
{
	public UIInput uiinput_0;

	public UILabel uilabel_2;

	public UIButton uibutton_2;

	public UILabel uilabel_3;

	private static MessageWindowRename messageWindowRename_0;

	protected Action action_1;

	private bool bool_1 = true;

	public static MessageWindowRename MessageWindowRename_0
	{
		get
		{
			return messageWindowRename_0;
		}
	}

	public static void Show(MessageRenameWindowParams messageRenameWindowParams_0 = null)
	{
		if (!(messageWindowRename_0 != null))
		{
			messageWindowRename_0 = BaseWindow.Load((!messageRenameWindowParams_0.bool_0) ? "MessageWindowRename" : "MessageWindowRenameFree") as MessageWindowRename;
			messageWindowRename_0.Parameters_0.type_0 = WindowQueue.Type.New;
			messageWindowRename_0.Parameters_0.bool_5 = true;
			messageWindowRename_0.Parameters_0.bool_0 = true;
			messageWindowRename_0.Parameters_0.bool_6 = messageRenameWindowParams_0.bool_0;
			messageWindowRename_0.InternalShow(messageRenameWindowParams_0);
		}
	}

	public override void OnShow()
	{
		base.OnShow();
		if (messageWindowRename_0.Parameters_0.bool_6)
		{
			uilabel_1.String_0 = Localizer.Get("ui.rename_wnd.buy");
			if (uilabel_3 != null)
			{
				AdditionalPurchaseData addPurchaseDataByKey = AdditionalPurchasesDataStorage.Get.GetAddPurchaseDataByKey(AdditionalPurchaseType.USER_RENAME);
				if (addPurchaseDataByKey == null)
				{
					Log.AddLine("AdditionalPurchaseType.USER_RENAME not exist");
					Hide();
					return;
				}
				int int_ = addPurchaseDataByKey.int_1;
				uilabel_3.String_0 = int_ + " ?";
			}
		}
		if (uibutton_2 != null)
		{
			uibutton_2.gameObject.SetActive(false);
		}
	}

	public override void OnHide()
	{
		base.OnHide();
		UserNickController.UserNickController_0.Unsubscribe(OnSetNickSuccess, UserNickController.Event.USER_SET_NICK_SUCCESS);
		UserNickController.UserNickController_0.Unsubscribe(OnSetNickFailed, UserNickController.Event.USER_SET_NICK_FAILED);
		uiinput_0.onValidate = null;
		User user_ = UsersData.UsersData_0.UserData_0.user_0;
		bool flag = !string.IsNullOrEmpty(user_.string_0) && !string.Equals(user_.string_0, uiinput_0.String_0);
		if (!user_.bool_1 && bool_1 && flag)
		{
			TutorialController.TutorialController_0.Start();
		}
		if (!bool_1)
		{
			TutorialController.TutorialController_0.CheatCompleteTutor();
		}
	}

	public void OnSubmitInputNick(GameObject gameObject_0)
	{
		if (uibutton_1.Boolean_0)
		{
			OnOKButton();
		}
	}

	public void OnOkAndSkipTutor()
	{
		if (uibutton_1.Boolean_0)
		{
			bool_1 = false;
			OnOKButton();
		}
	}

	protected override void InitParams()
	{
		base.InitParams();
		ShowError(string.Empty);
		UserNickController.UserNickController_0.Subscribe(OnSetNickSuccess, UserNickController.Event.USER_SET_NICK_SUCCESS);
		UserNickController.UserNickController_0.Subscribe(OnSetNickFailed, UserNickController.Event.USER_SET_NICK_FAILED);
		uiinput_0.onChange.Add(new EventDelegate(OnNickInputChange));
		string string_ = UserController.UserController_0.UserData_0.user_0.string_0;
		if (!string.IsNullOrEmpty(string_) && !string_.Equals(uiinput_0.String_2))
		{
			uiinput_0.String_2 = string_;
		}
		uiinput_0.Boolean_2 = true;
	}

	protected override void OnOKButton()
	{
		if (messageWindowRename_0.Parameters_0.bool_6)
		{
			AdditionalPurchaseData addPurchaseDataByKey = AdditionalPurchasesDataStorage.Get.GetAddPurchaseDataByKey(AdditionalPurchaseType.USER_RENAME);
			if (addPurchaseDataByKey == null)
			{
				Log.AddLine("AdditionalPurchaseType.USER_RENAME not exist");
				Hide();
				return;
			}
			int int_ = addPurchaseDataByKey.int_1;
			if (UserController.UserController_0.GetMoneyByType(MoneyType.MONEY_TYPE_COINS) < int_)
			{
				BankController.BankController_0.TryOpenBank(BankController.BankSourceType.BANK_LOBBY);
				return;
			}
		}
		if (!uiinput_0.String_0.Equals(uiinput_0.String_2))
		{
			uibutton_1.Boolean_0 = false;
			ShowError(string.Empty);
			UserNickController.UserNickController_0.ProcessUserSetNick(uiinput_0.String_2);
		}
		else
		{
			ShowError(UserNickController.UserNickController_0.GetErrorText(2));
		}
	}

	private void OnSetNickSuccess(UserNickEventParams userNickEventParams_0)
	{
		uibutton_1.Boolean_0 = true;
	}

	private void OnSetNickFailed(UserNickEventParams userNickEventParams_0)
	{
		uibutton_1.Boolean_0 = true;
		ShowError(UserNickController.UserNickController_0.GetErrorText(userNickEventParams_0.int_0));
	}

	private void OnNickInputChange()
	{
		bool flag = UserNickController.UserNickController_0.HasInvalidSymbol(uiinput_0.String_2);
		ShowError((!flag) ? string.Empty : LocalizationStorage.Get.Term("window.msg.rename.bad_symbols"));
		if (uiinput_0.String_2.Length > 16)
		{
			uiinput_0.String_2 = uiinput_0.String_2.Substring(0, uiinput_0.String_2.Length - 1);
			ShowError(LocalizationStorage.Get.Term("window.msg.rename.to_long_nick"));
			uibutton_1.Boolean_0 = !flag;
		}
	}

	private void ShowError(string string_0)
	{
		if (string.IsNullOrEmpty(string_0))
		{
			uilabel_2.String_0 = string.Empty;
			NGUITools.SetActive(uilabel_2.gameObject, false);
			uibutton_1.Boolean_0 = true;
		}
		else
		{
			uilabel_2.String_0 = string_0;
			NGUITools.SetActive(uilabel_2.gameObject, true);
			uibutton_1.Boolean_0 = false;
		}
	}
}
