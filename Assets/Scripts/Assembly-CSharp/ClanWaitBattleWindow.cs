using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.Message)]
public class ClanWaitBattleWindow : BaseGameWindow
{
	public UILabel uilabel_0;

	public UIButton uibutton_0;

	public UIButton uibutton_1;

	public UILabel uilabel_1;

	private static ClanWaitBattleWindow clanWaitBattleWindow_0;

	public static ClanWaitBattleWindow ClanWaitBattleWindow_0
	{
		get
		{
			if (clanWaitBattleWindow_0 == null)
			{
				clanWaitBattleWindow_0 = new ClanWaitBattleWindow();
			}
			return clanWaitBattleWindow_0;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return ClanWaitBattleWindow_0 != null && ClanWaitBattleWindow_0.Boolean_0;
		}
	}

	public static void Show(ClanWaitBattleWindowParams clanWaitBattleWindowParams_0 = null)
	{
		if (!(clanWaitBattleWindow_0 != null))
		{
			clanWaitBattleWindow_0 = BaseWindow.Load("ClanWaitBattleWindow") as ClanWaitBattleWindow;
			clanWaitBattleWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			clanWaitBattleWindow_0.Parameters_0.bool_5 = false;
			clanWaitBattleWindow_0.Parameters_0.bool_0 = false;
			clanWaitBattleWindow_0.Parameters_0.bool_6 = false;
			clanWaitBattleWindow_0.InternalShow(clanWaitBattleWindowParams_0);
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
		clanWaitBattleWindow_0 = null;
	}

	protected virtual void Init()
	{
		InitParams();
		uibutton_1.list_0.Add(new EventDelegate(OnCloseButton));
		uibutton_0.list_0.Add(new EventDelegate(OnCloseButton));
	}

	protected virtual void InitParams()
	{
		if (base.WindowShowParameters_0 != null)
		{
			Screen.lockCursor = false;
			ClanWaitBattleWindowParams clanWaitBattleWindowParam = (ClanWaitBattleWindowParams)base.WindowShowParameters_0;
			uilabel_1.String_0 = Localizer.Get("ui.clan_wait.cancel");
			uilabel_0.String_0 = Localizer.Get("ui.clan_wait.message");
		}
	}

	public virtual void OnCloseButton()
	{
		ClanWaitBattleWindowParams clanWaitBattleWindowParams = (ClanWaitBattleWindowParams)base.WindowShowParameters_0;
		ClanBattleController.ClanBattleController_0.SendRequestToUser(3, clanWaitBattleWindowParams.int_0, string.Empty);
		Hide();
	}
}
