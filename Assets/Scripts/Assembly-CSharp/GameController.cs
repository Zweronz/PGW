using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.unity;

public class GameController
{
	private static GameController gameController_0;

	[CompilerGenerated]
	private static Action action_0;

	public static GameController GameController_0
	{
		get
		{
			if (gameController_0 == null)
			{
				gameController_0 = new GameController();
			}
			return gameController_0;
		}
	}

	private GameController()
	{
	}

	public void Exit(bool bool_0 = true)
	{
		if (WindowController.WindowController_0.BaseWindow_0 != null)
		{
			WindowController.WindowController_0.BaseWindow_0.Hide();
			return;
		}
		Action action = delegate
		{
			Application.Quit();
		};
		if (bool_0)
		{
			ExitGameWindow.Show();
		}
		else
		{
			action();
		}
	}
}
