using System;
using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.ArenaComplete)]
public class ArenaCompleteWindow : BaseGameWindow
{
	public UILabel uilabel_0;

	public UILabel uilabel_1;

	public UILabel uilabel_2;

	public UILabel uilabel_3;

	public UILabel uilabel_4;

	public UILabel uilabel_5;

	public UILabel uilabel_6;

	public UIWidget uiwidget_0;

	public UILabel uilabel_7;

	public UILabel uilabel_8;

	public UITexture uitexture_0;

	public UILabel uilabel_9;

	private int int_0;

	private ArenaCompleteWindowParams arenaCompleteWindowParams_0;

	private static ArenaCompleteWindow arenaCompleteWindow_0;

	private int int_1;

	public static ArenaCompleteWindow ArenaCompleteWindow_0
	{
		get
		{
			return arenaCompleteWindow_0;
		}
	}

	public static void Show(ArenaCompleteWindowParams arenaCompleteWindowParams_1 = null)
	{
		if (!(arenaCompleteWindow_0 != null))
		{
			arenaCompleteWindow_0 = BaseWindow.Load("ArenaCompleteWindow") as ArenaCompleteWindow;
			arenaCompleteWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			arenaCompleteWindow_0.Parameters_0.bool_5 = false;
			arenaCompleteWindow_0.Parameters_0.bool_0 = false;
			arenaCompleteWindow_0.Parameters_0.bool_6 = false;
			arenaCompleteWindow_0.InternalShow(arenaCompleteWindowParams_1);
		}
	}

	public override void OnShow()
	{
		base.OnShow();
		arenaCompleteWindowParams_0 = (ArenaCompleteWindowParams)base.WindowShowParameters_0;
		Init();
		InitCustomKey();
	}

	public override void OnHide()
	{
		base.OnHide();
		arenaCompleteWindow_0 = null;
	}

	private void Init()
	{
		Player_move_c.SetBlockKeyboardControl(true, true);
		uilabel_0.String_0 = arenaCompleteWindowParams_0.Int32_0.ToString();
		uilabel_1.String_0 = arenaCompleteWindowParams_0.Int32_1.ToString();
		uilabel_2.String_0 = arenaCompleteWindowParams_0.Int32_2.ToString();
		int num = arenaCompleteWindowParams_0.Int32_3;
		if (num < arenaCompleteWindowParams_0.Int32_2)
		{
			num = arenaCompleteWindowParams_0.Int32_2;
		}
		uilabel_3.String_0 = num.ToString();
		int num2 = arenaCompleteWindowParams_0.Int32_4;
		if (num2 < arenaCompleteWindowParams_0.Int32_2)
		{
			num2 = arenaCompleteWindowParams_0.Int32_2;
		}
		uilabel_4.String_0 = num2.ToString();
		int num3 = FightOfflineController.FightOfflineController_0.Int32_0;
		string string_ = FightOfflineController.FightOfflineController_0.String_0;
		int_1 = FightOfflineController.FightOfflineController_0.Int32_1;
		if (num3 < arenaCompleteWindowParams_0.Int32_2)
		{
			num3 = arenaCompleteWindowParams_0.Int32_2;
			string_ = UserController.UserController_0.UserData_0.user_0.string_0;
			int_1 = UserController.UserController_0.UserData_0.user_0.int_0;
		}
		uilabel_5.String_0 = num3.ToString();
		uilabel_6.String_0 = string_;
		MapData objectByKey = MapStorage.Get.Storage.GetObjectByKey(arenaCompleteWindowParams_0.ModeData_0.Int32_1);
		uitexture_0.Texture_0 = Resources.Load("LevelLoadings/Hi/Loading_" + objectByKey.String_0) as Texture2D;
		if (arenaCompleteWindowParams_0.ModeRewardData_0 != null)
		{
			uilabel_7.String_0 = arenaCompleteWindowParams_0.ModeRewardData_0.Int32_1.ToString();
			uilabel_8.String_0 = arenaCompleteWindowParams_0.ModeRewardData_0.Int32_2.ToString();
			int_0 = UserController.UserController_0.CheckShowLevelUpWindow(arenaCompleteWindowParams_0.ModeRewardData_0.Int32_2);
		}
		else
		{
			NGUITools.SetActive(uiwidget_0.gameObject, false);
		}
		NGUITools.SetActive(uilabel_9.gameObject, false);
		if (arenaCompleteWindowParams_0.Boolean_1 || arenaCompleteWindowParams_0.Boolean_0)
		{
			uilabel_9.String_0 = Localizer.Get((!arenaCompleteWindowParams_0.Boolean_1) ? "window.arena_complete.dead" : "window.arena_complete.timeout");
			NGUITools.SetActive(uilabel_9.gameObject, true);
		}
	}

	private void InitCustomKey()
	{
		AddInputKey(KeyCode.Escape, delegate
		{
			if (base.Boolean_0)
			{
				OnBackButtonClick();
			}
		});
		AddInputKey(KeyCode.Space, delegate
		{
			if (base.Boolean_0)
			{
				OnFightButtonClick();
			}
		});
	}

	public void OnBackButtonClick()
	{
		ShowLevelUpIfNeed(LeaveRoom);
	}

	public void OnFightButtonClick()
	{
		ShowLevelUpIfNeed(StartBattle);
	}

	public void OnTopUserClick()
	{
		ProfileWindowController.ProfileWindowController_0.ShowProfileWindowForPlayer(int_1);
	}

	private void ShowLevelUpIfNeed(Action action_0)
	{
		if (action_0 == null)
		{
			return;
		}
		if (int_0 != 0)
		{
			Hide();
			MapData objectByKey = MapStorage.Get.Storage.GetObjectByKey(arenaCompleteWindowParams_0.ModeData_0.Int32_1);
			Texture texture_ = Resources.Load("LevelLoadings/Hi/Loading_" + objectByKey.String_0) as Texture;
			LvlUpWindow.Show(new LvlUpWindowParams(int_0, action_0, texture_));
			NickLabelController[] lables = NickLabelStack.nickLabelStack_0.lables;
			NickLabelController[] array = lables;
			foreach (NickLabelController nickLabelController in array)
			{
				NGUITools.SetActive(nickLabelController.gameObject, false);
			}
		}
		else
		{
			action_0();
		}
	}

	private void LeaveRoom()
	{
		NickLabelController[] lables = NickLabelStack.nickLabelStack_0.lables;
		NickLabelController[] array = lables;
		foreach (NickLabelController nickLabelController in array)
		{
			NGUITools.SetActive(nickLabelController.gameObject, true);
		}
		FightScreen.FightScreen_0.SwitchToMenu();
	}

	private void StartBattle()
	{
		NickLabelController[] lables = NickLabelStack.nickLabelStack_0.lables;
		NickLabelController[] array = lables;
		foreach (NickLabelController nickLabelController in array)
		{
			NGUITools.SetActive(nickLabelController.gameObject, true);
		}
		if (FightOfflineController.FightOfflineController_0.ModeData_0 != null)
		{
			FightOfflineController.FightOfflineController_0.StartFight(0, FightOfflineController.FightOfflineController_0.ModeData_0.Int32_0);
		}
	}
}
