using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.ExitGameWindow)]
public class ExitGameWindow : BaseGameWindow
{
	private static ExitGameWindow exitGameWindow_0;

	private bool bool_1;

	public static ExitGameWindow ExitGameWindow_0
	{
		get
		{
			return exitGameWindow_0;
		}
	}

	public static void Show(ExitGameWindowParams exitGameWindowParams_0 = null)
	{
		if (!(exitGameWindow_0 != null))
		{
			exitGameWindow_0 = BaseWindow.Load("ExitGameWindow") as ExitGameWindow;
			exitGameWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			exitGameWindow_0.Parameters_0.bool_5 = true;
			exitGameWindow_0.Parameters_0.bool_0 = false;
			exitGameWindow_0.Parameters_0.bool_6 = true;
			exitGameWindow_0.InternalShow(exitGameWindowParams_0);
		}
	}

	public override void OnShow()
	{
		base.OnShow();
	}

	public override void OnHide()
	{
		base.OnHide();
		exitGameWindow_0 = null;
		if (!bool_1)
		{
			Lobby.Lobby_0.Show();
		}
	}

	public void OnClickOk()
	{
		bool_1 = true;
		Application.Quit();
	}
}
