using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.ClanCreateWindow)]
public class ClanCreateWindow : BaseGameWindow
{
	private static ClanCreateWindow clanCreateWindow_0;

	public UIInput uiinput_0;

	public UILabel uilabel_0;

	public UIButton uibutton_0;

	public static ClanCreateWindow ClanCreateWindow_0
	{
		get
		{
			return clanCreateWindow_0;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return ClanCreateWindow_0 != null && ClanCreateWindow_0.Boolean_0;
		}
	}

	public static void Show(ClanCreateWindowParams clanCreateWindowParams_0 = null)
	{
		if (!(clanCreateWindow_0 != null))
		{
			clanCreateWindow_0 = BaseWindow.Load("ClanCreateWindow") as ClanCreateWindow;
			clanCreateWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			clanCreateWindow_0.Parameters_0.bool_5 = true;
			clanCreateWindow_0.Parameters_0.bool_0 = false;
			clanCreateWindow_0.Parameters_0.bool_6 = true;
			clanCreateWindow_0.InternalShow(clanCreateWindowParams_0);
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
		clanCreateWindow_0 = null;
		ClanController.ClanController_0.Unsubscribe(OnCreateClanResponse, ClanController.EventType.CREATE_SUCCESS);
		ClanController.ClanController_0.Unsubscribe(OnCreateClanResponse, ClanController.EventType.CREATE_FAILURE);
	}

	private void Init()
	{
		SetError(string.Empty);
		uiinput_0.onChange.Add(new EventDelegate(OnInputValidate));
		uiinput_0.Boolean_2 = true;
		ClanController.ClanController_0.Subscribe(OnCreateClanResponse, ClanController.EventType.CREATE_SUCCESS);
		ClanController.ClanController_0.Subscribe(OnCreateClanResponse, ClanController.EventType.CREATE_FAILURE);
	}

	private void SetError(string string_0 = "")
	{
		uilabel_0.String_0 = string_0;
		NGUITools.SetActive(uilabel_0.gameObject, !string.IsNullOrEmpty(string_0));
	}

	public void OnAcceptClick()
	{
		if (string.IsNullOrEmpty(uiinput_0.String_2))
		{
			SetError(Localizer.Get("window.clan_create.warn.name_empty"));
		}
		else if (!uiinput_0.String_2.Equals(uiinput_0.String_0) && uibutton_0.Boolean_0)
		{
			ClanController.ClanController_0.CreateClan(uiinput_0.String_2.Trim());
		}
	}

	public void OnCreateClanResponse(ClanController.EventData eventData_0)
	{
		if (eventData_0.UserClanData_0 != null)
		{
			Hide();
		}
		else if (eventData_0.Int32_0 > 0)
		{
			switch (eventData_0.Int32_0)
			{
			case 27:
			{
				Hide();
				MessageAnimateWindowParams messageAnimateWindowParams = new MessageAnimateWindowParams();
				messageAnimateWindowParams.MessageAnimateWindowType_0 = MessageAnimateWindowType.CONFIRM;
				messageAnimateWindowParams.String_0 = "window.clan_create.warn.limit.title";
				messageAnimateWindowParams.String_1 = "window.clan_create.warn.limit.descr";
				messageAnimateWindowParams.String_2 = "ui.common.btn.ok";
				messageAnimateWindowParams.String_3 = "window.clan_create.warn.limit.detail";
				messageAnimateWindowParams.Action_1 = GotoForum;
				MessageAnimateWindow.Show(messageAnimateWindowParams);
				break;
			}
			case 17:
				SetError(Localizer.Get("window.clan_create.warn.have"));
				break;
			case 16:
				SetError(Localizer.Get("window.clan_create.warn.name"));
				break;
			}
		}
	}

	private void GotoForum()
	{
		Application.OpenURL("http://pixelgun3d.com/en/forum/index.php?/topic/4655-clans%E2%80%99-war-is-coming/");
	}

	private void OnInputValidate()
	{
		bool flag = UserNickController.UserNickController_0.HasInvalidSymbol(uiinput_0.String_2);
		uibutton_0.Boolean_0 = !flag;
		SetError((!flag) ? string.Empty : Localizer.Get("window.msg.rename.bad_symbols"));
		if (uiinput_0.String_2.Length > 16)
		{
			uiinput_0.String_2 = uiinput_0.String_2.Substring(0, uiinput_0.String_2.Length - 1);
			SetError(Localizer.Get("window.clan_create.warn.long_title"));
			uibutton_0.Boolean_0 = !flag;
		}
	}
}
