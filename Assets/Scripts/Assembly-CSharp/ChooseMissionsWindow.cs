using engine.unity;

[GameWindowParams(GameWindowType.ChooseMissions)]
public class ChooseMissionsWindow : BaseGameWindow
{
	private static ChooseMissionsWindow chooseMissionsWindow_0;

	public static ChooseMissionsWindow ChooseMissionsWindow_0
	{
		get
		{
			return chooseMissionsWindow_0;
		}
	}

	public static void Show(WindowShowParameters windowShowParameters_1 = null)
	{
		if (!(chooseMissionsWindow_0 != null))
		{
			chooseMissionsWindow_0 = BaseWindow.Load("ChooseMissionsWindow") as ChooseMissionsWindow;
			chooseMissionsWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			chooseMissionsWindow_0.Parameters_0.bool_5 = true;
			chooseMissionsWindow_0.Parameters_0.bool_0 = false;
			chooseMissionsWindow_0.Parameters_0.bool_6 = true;
			chooseMissionsWindow_0.InternalShow(windowShowParameters_1);
		}
	}

	public override void OnShow()
	{
		base.OnShow();
	}

	public override void OnHide()
	{
		base.OnHide();
		chooseMissionsWindow_0 = null;
	}

	public void OnArenaBoxClick()
	{
		Hide();
	}

	public void OnCampaignClick()
	{
		Hide();
	}
}
