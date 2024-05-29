using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using WebSocketSharp;
using engine.helpers;
using engine.network;
using engine.unity;

[GameWindowParams(GameWindowType.BattleOver)]
public class BattleOverWindow : BaseGameWindow
{
	private static BattleOverWindow battleOverWindow_0;

	public UITable uitable_0;

	public UITable uitable_1;

	public UITable uitable_2;

	public UITable uitable_3;

	public UITable uitable_4;

	public UIButton uibutton_0;

	public BattleOverTableObject battleOverTableObject_0;

	public BattleOverTableObject battleOverTableObject_1;

	public GameObject gameObject_0;

	public UIWidget uiwidget_0;

	public BattleOverTitleScript battleOverTitleScript_0;

	public GameObject[] gameObject_1;

	public UILabel[] uilabel_0;

	public UIWidget uiwidget_1;

	public UIWidget uiwidget_2;

	public GameObject gameObject_2;

	public BattleOverTitleScript[] battleOverTitleScript_1;

	public BattleOverTitleScript[] battleOverTitleScript_2;

	public GameObject[] gameObject_3;

	public GameObject[] gameObject_4;

	public UILabel[] uilabel_1;

	public UILabel[] uilabel_2;

	public GameObject[] gameObject_5;

	public UILabel[] uilabel_3;

	public UIWidget uiwidget_3;

	public UIWidget uiwidget_4;

	public BattleOverTitleScript[] battleOverTitleScript_3;

	public BattleOverTitleScript[] battleOverTitleScript_4;

	public GameObject[] gameObject_6;

	public GameObject[] gameObject_7;

	public UILabel[] uilabel_4;

	public UILabel[] uilabel_5;

	public UIWidget uiwidget_5;

	public UILabel uilabel_6;

	public UILabel uilabel_7;

	public UILabel uilabel_8;

	public UILabel uilabel_9;

	public GameObject gameObject_8;

	public UILabel uilabel_10;

	public UIPanel uipanel_0;

	public ShrinkPanel shrinkPanel_0;

	public UITable uitable_5;

	public UITable uitable_6;

	public GameObject gameObject_9;

	public GameObject gameObject_10;

	private List<BattleOverPlayerData> list_0 = new List<BattleOverPlayerData>();

	private List<BattleOverTableObject> list_1 = new List<BattleOverTableObject>();

	private ModeType modeType_0 = ModeType.DEATH_MATCH;

	private int int_0;

	private float float_0;

	private EndFightNetworkCommand.IsWinState isWinState_0 = EndFightNetworkCommand.IsWinState.Win;

	private EndFightNetworkCommand.IsWinState isWinState_1 = EndFightNetworkCommand.IsWinState.Win;

	private float float_1;

	private int int_1;

	private string string_0 = string.Empty;

	private int int_2;

	private bool bool_1 = true;

	private float float_2;

	private Transform transform_0;

	private Transform transform_1;

	private Transform transform_2;

	private Transform transform_3;

	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private int int_4;

	public static BattleOverWindow BattleOverWindow_0
	{
		get
		{
			return battleOverWindow_0;
		}
	}

	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		set
		{
			int_3 = value;
		}
	}

	public int Int32_1
	{
		[CompilerGenerated]
		get
		{
			return int_4;
		}
		[CompilerGenerated]
		set
		{
			int_4 = value;
		}
	}

	public static void Show(BattleOverWindowParams battleOverWindowParams_0)
	{
		if (!(battleOverWindow_0 != null) && battleOverWindowParams_0 != null)
		{
			battleOverWindow_0 = BaseWindow.Load("BattleOverWindow") as BattleOverWindow;
			battleOverWindow_0.Parameters_0.type_0 = WindowQueue.Type.Default;
			battleOverWindow_0.Parameters_0.bool_5 = false;
			battleOverWindow_0.Parameters_0.bool_0 = false;
			battleOverWindow_0.Parameters_0.bool_6 = false;
			battleOverWindow_0.InternalShow(battleOverWindowParams_0);
		}
	}

	public override void OnShow()
	{
		base.OnShow();
		AddInputKey(KeyCode.Escape, delegate
		{
			if (base.Boolean_0)
			{
				OnBackButtonClick();
			}
		});
		uiwidget_5.Single_2 = 0f;
		ResetTimerExitFromFight();
		WindowController.WindowController_0.DispatchEvent(WindowController.GameEvent.BATTLE_OVER_WINDOW_SHOW);
	}

	public override void OnHide()
	{
		SendLikesAndComplants();
		battleOverWindow_0 = null;
		base.OnHide();
	}

	private void Start()
	{
		ModeData modeData_ = MonoSingleton<FightController>.Prop_0.ModeData_0;
		if (modeData_ == null)
		{
			Log.AddLine(string.Format("[BattleOverWindow::Start()]  FightController.Get.CurrentMode == null "), Log.LogLevel.ERROR);
			return;
		}
		modeType_0 = modeData_.ModeType_0;
		BattleOverWindowParams battleOverWindowParams = base.WindowShowParameters_0 as BattleOverWindowParams;
		List<BattleOverPlayerData> list_ = battleOverWindowParams.list_0;
		List<BattleOverPlayerData> list_2 = battleOverWindowParams.list_1;
		gameObject_0.SetActive(modeType_0 == ModeType.DEATH_MATCH || modeType_0 == ModeType.DUEL);
		uiwidget_0.gameObject.SetActive(modeType_0 == ModeType.DEATH_MATCH || modeType_0 == ModeType.DUEL);
		gameObject_2.SetActive(modeType_0 == ModeType.TEAM_FIGHT || modeType_0 == ModeType.FLAG_CAPTURE);
		uiwidget_1.gameObject.SetActive(modeType_0 == ModeType.TEAM_FIGHT);
		uiwidget_2.gameObject.SetActive(modeType_0 == ModeType.TEAM_FIGHT);
		uiwidget_3.gameObject.SetActive(modeType_0 == ModeType.FLAG_CAPTURE);
		uiwidget_4.gameObject.SetActive(modeType_0 == ModeType.FLAG_CAPTURE);
		if (modeType_0 != ModeType.DEATH_MATCH && modeType_0 != ModeType.DUEL)
		{
			if (modeType_0 == ModeType.TEAM_FIGHT)
			{
				TeamFightAndFlags(list_, list_2);
			}
			else if (modeType_0 == ModeType.FLAG_CAPTURE)
			{
				TeamFightAndFlags(list_, list_2, true);
			}
		}
		else
		{
			DeathMatch(list_);
		}
		battleOverTableObject_0.gameObject.SetActive(false);
		battleOverTableObject_1.gameObject.SetActive(false);
		Update();
		setComplaintPanelContent();
		if (PhotonNetwork.Room_0 != null && !PhotonNetwork.Room_0.Boolean_4)
		{
			bool_1 = false;
		}
		if (!bool_1)
		{
			uibutton_0.gameObject.SetActive(false);
		}
		else
		{
			uibutton_0.Boolean_0 = false;
		}
	}

	protected override void Update()
	{
		base.Update();
		if (modeType_0 != ModeType.DEATH_MATCH && modeType_0 != ModeType.DUEL)
		{
			if (modeType_0 == ModeType.TEAM_FIGHT)
			{
				teamReposition(uiwidget_1, uiwidget_2, uiwidget_5, gameObject_2, battleOverTableObject_0.gameObject);
			}
			else if (modeType_0 == ModeType.FLAG_CAPTURE)
			{
				teamReposition(uiwidget_3, uiwidget_4, uiwidget_5, gameObject_2, battleOverTableObject_1.gameObject);
			}
		}
		else
		{
			deathmatchReposition(uiwidget_0, uiwidget_5, gameObject_0, battleOverTableObject_0.gameObject);
		}
		UpdateKeyBoard();
		UpdateTimerExitFromFight();
	}

	private void DeathMatch(List<BattleOverPlayerData> list_2)
	{
		BattleOverWindowParams battleOverWindowParams = base.WindowShowParameters_0 as BattleOverWindowParams;
		int int_ = 0;
		int int_2 = 0;
		int int_3 = 0;
		createTableForPlayer(list_2, uitable_0, battleOverTableObject_0);
		findMyRewardAndPlace(list_2, out int_, out int_2, out int_3);
		if (int_ == 0)
		{
			Log.AddLine(string.Format("[BattleOverWindow::Start()] myPlace = 0 players.Count = {0} ", list_2.Count), Log.LogLevel.ERROR);
			return;
		}
		int_0 = 16 - list_2.Count;
		battleOverTitleScript_0.nameTxt = list_2[0].string_0;
		battleOverTitleScript_0.killsTxt = list_2[0].int_3.ToString();
		battleOverTitleScript_0.DoIt();
		showRevard(string.IsNullOrEmpty(battleOverWindowParams.string_0) && battleOverWindowParams.bool_0, int_2, int_3);
		int num = int_ - 1;
		if (num > 3)
		{
			num = 3;
		}
		for (int i = 0; i < 4; i++)
		{
			gameObject_1[i].SetActive(i == num);
			if (i == num)
			{
				switch (i)
				{
				case 0:
					uilabel_0[i].String_0 = string.Format(Localizer.Get("ui.battle_over_window.deathmatch_title1"), int_);
					break;
				case 1:
					uilabel_0[i].String_0 = string.Format(Localizer.Get("ui.battle_over_window.deathmatch_title2"), int_);
					break;
				case 2:
					uilabel_0[i].String_0 = string.Format(Localizer.Get("ui.battle_over_window.deathmatch_title3"), int_);
					break;
				default:
					uilabel_0[i].String_0 = string.Format(Localizer.Get("ui.battle_over_window.deathmatch_title"), int_);
					break;
				}
			}
		}
		setTableSize(list_2.Count, uiwidget_0, uitable_0, battleOverTableObject_0.gameObject);
	}

	private void TeamFightAndFlags(List<BattleOverPlayerData> list_2, List<BattleOverPlayerData> list_3, bool bool_2 = false)
	{
		BattleOverWindowParams battleOverWindowParams = base.WindowShowParameters_0 as BattleOverWindowParams;
		int int_ = battleOverWindowParams.int_0;
		int int_2 = battleOverWindowParams.int_1;
		int int_3 = 0;
		int int_4 = 0;
		int int_5 = 0;
		byte byte_ = (byte)battleOverWindowParams.int_2;
		isWinState_0 = battleOverWindowParams.isWinState_0;
		isWinState_1 = battleOverWindowParams.isWinState_1;
		if (!bool_2)
		{
			createTableForPlayer(battleOverWindowParams.list_0, uitable_1, battleOverTableObject_0);
			createTableForPlayer(battleOverWindowParams.list_1, uitable_2, battleOverTableObject_0);
		}
		else
		{
			createTableForPlayer(battleOverWindowParams.list_0, uitable_3, battleOverTableObject_1);
			createTableForPlayer(battleOverWindowParams.list_1, uitable_4, battleOverTableObject_1);
		}
		findMyRewardAndPlace(battleOverWindowParams.list_0, out int_3, out int_4, out int_5);
		showRevard(string.IsNullOrEmpty(battleOverWindowParams.string_0) && battleOverWindowParams.bool_0, int_4, int_5);
		bool flag = isWinState_0 == EndFightNetworkCommand.IsWinState.Win;
		bool flag2 = isWinState_0 != EndFightNetworkCommand.IsWinState.Win;
		bool flag3 = isWinState_1 != EndFightNetworkCommand.IsWinState.Win;
		gameObject_5[0].SetActive(flag);
		gameObject_5[1].SetActive(flag2);
		if (flag)
		{
			uilabel_3[0].String_0 = Localizer.Get("ui.battle_over_window.my_team_win_title");
		}
		else
		{
			uilabel_3[1].String_0 = Localizer.Get("ui.battle_over_window.my_team_lose_title");
			if (flag3)
			{
				uilabel_3[1].String_0 = Localizer.Get("ui.battle_over_window.my_team_dead_head_title");
			}
		}
		if (!bool_2)
		{
			setTeamContent(byte_, int_, int_2, battleOverTitleScript_1, battleOverTitleScript_2, gameObject_3, gameObject_4, uilabel_1, uilabel_2);
		}
		else
		{
			setTeamContent(byte_, int_, int_2, battleOverTitleScript_3, battleOverTitleScript_4, gameObject_6, gameObject_7, uilabel_4, uilabel_5);
		}
		int_0 = 16 - battleOverWindowParams.list_0.Count - battleOverWindowParams.list_1.Count;
		if (!bool_2)
		{
			setTableSize(battleOverWindowParams.list_0.Count, uiwidget_1, uitable_1, battleOverTableObject_0.gameObject);
			setTableSize(battleOverWindowParams.list_1.Count, uiwidget_2, uitable_2, battleOverTableObject_0.gameObject);
		}
		else
		{
			setTableSize(battleOverWindowParams.list_0.Count, uiwidget_3, uitable_3, battleOverTableObject_1.gameObject);
			setTableSize(battleOverWindowParams.list_1.Count, uiwidget_4, uitable_4, battleOverTableObject_1.gameObject);
		}
	}

	private void showRevard(bool bool_2, int int_5, int int_6)
	{
		uilabel_6.gameObject.SetActive(bool_2);
		uilabel_7.gameObject.SetActive(bool_2);
		uilabel_8.gameObject.SetActive(bool_2);
		uilabel_9.gameObject.SetActive(bool_2);
		gameObject_8.gameObject.SetActive(bool_2);
		uilabel_10.gameObject.SetActive(!bool_2);
		BattleOverRevardPositionScript component = uiwidget_5.GetComponent<BattleOverRevardPositionScript>();
		component.moneyTxt = int_5.ToString();
		component.expTxt = int_6.ToString();
	}

	private void createTableForPlayer(List<BattleOverPlayerData> list_2, UITable uitable_7, BattleOverTableObject battleOverTableObject_2)
	{
		bool flag = base.WindowShowParameters_0 is BattleOverWindowParams;
		for (int i = 0; i < list_2.Count; i++)
		{
			BattleOverPlayerData battleOverPlayerData_ = list_2[i];
			CreateTableForPlayer(battleOverPlayerData_, uitable_7, battleOverTableObject_2);
		}
	}

	private void CreateTableForPlayer(BattleOverPlayerData battleOverPlayerData_0, UITable uitable_7, BattleOverTableObject battleOverTableObject_2)
	{
		bool flag = battleOverPlayerData_0.int_1 == UserController.UserController_0.UserData_0.user_0.int_0;
		GameObject gameObject = NGUITools.AddChild(uitable_7.gameObject, battleOverTableObject_2.gameObject);
		BattleOverTableObject component = gameObject.GetComponent<BattleOverTableObject>();
		component.data = battleOverPlayerData_0;
		gameObject.SetActive(true);
		if (flag)
		{
			hideColliderForLick(component);
			int_2 = UserController.UserController_0.CheckShowLevelUpWindow(battleOverPlayerData_0.int_11);
		}
		list_0.Add(battleOverPlayerData_0);
		list_1.Add(component);
	}

	private void hideColliderForLick(BattleOverTableObject battleOverTableObject_2)
	{
		BoxCollider component = battleOverTableObject_2.likeBlue.GetComponent<BoxCollider>();
		if (component != null)
		{
			component.enabled = false;
		}
		component = battleOverTableObject_2.likeGreen.GetComponent<BoxCollider>();
		if (component != null)
		{
			component.enabled = false;
		}
		component = battleOverTableObject_2.likeGray.GetComponent<BoxCollider>();
		if (component != null)
		{
			component.enabled = false;
		}
		Transform transform = battleOverTableObject_2.likeGray.transform.Find("click");
		if (transform != null)
		{
			component = transform.GetComponent<BoxCollider>();
			if (component != null)
			{
				component.enabled = false;
			}
		}
	}

	private void setTableSize(int int_5, UIWidget uiwidget_6, UITable uitable_7, GameObject gameObject_11)
	{
		int num = gameObject_11.gameObject.GetComponent<UIWidget>().Int32_1 * int_5 + 1 * int_5;
		int num2 = uitable_7.gameObject.GetComponent<UIWidget>().Int32_1 - num;
		uiwidget_6.Int32_1 -= num2;
	}

	private void setTeamContent(byte byte_0, int int_5, int int_6, BattleOverTitleScript[] battleOverTitleScript_5, BattleOverTitleScript[] battleOverTitleScript_6, GameObject[] gameObject_11, GameObject[] gameObject_12, UILabel[] uilabel_11, UILabel[] uilabel_12)
	{
		bool flag = isWinState_0 == EndFightNetworkCommand.IsWinState.Win;
		bool flag2 = isWinState_0 != EndFightNetworkCommand.IsWinState.Win;
		bool flag3 = isWinState_1 == EndFightNetworkCommand.IsWinState.Win;
		bool flag4 = isWinState_1 != EndFightNetworkCommand.IsWinState.Win;
		battleOverTitleScript_5[0].gameObject.SetActive(flag);
		battleOverTitleScript_5[1].gameObject.SetActive(flag2);
		battleOverTitleScript_6[0].gameObject.SetActive(flag3);
		battleOverTitleScript_6[1].gameObject.SetActive(flag4);
		gameObject_11[0].gameObject.SetActive(flag);
		gameObject_11[1].gameObject.SetActive(flag2);
		gameObject_12[0].gameObject.SetActive(flag3);
		gameObject_12[1].gameObject.SetActive(flag4);
		uilabel_11[0].gameObject.SetActive(flag && flag4);
		uilabel_11[1].gameObject.SetActive(flag2 && flag3);
		uilabel_12[0].gameObject.SetActive(flag3 && flag2);
		uilabel_12[1].gameObject.SetActive(flag4 && flag);
		if (uilabel_11.Length > 2 && uilabel_12.Length > 2)
		{
			uilabel_11[2].gameObject.SetActive(flag2 && flag4);
			uilabel_12[2].gameObject.SetActive(flag2 && flag4);
		}
		if (flag2 && flag4)
		{
			uilabel_11[1].String_0 = Localizer.Get("ui.battle_over_window.other_team_draw");
			uilabel_12[1].String_0 = Localizer.Get("ui.battle_over_window.other_team_draw");
		}
		if (flag)
		{
			battleOverTitleScript_5[0].nameTxt = Localizer.Get("ui.battle_over_window.my_team_win");
			battleOverTitleScript_5[0].killsTxt = ((byte_0 != 1) ? int_6.ToString() : int_5.ToString());
			battleOverTitleScript_5[0].DoIt();
		}
		else
		{
			battleOverTitleScript_5[1].nameTxt = Localizer.Get("ui.battle_over_window.my_team_lose");
			battleOverTitleScript_5[1].killsTxt = ((byte_0 != 1) ? int_6.ToString() : int_5.ToString());
			battleOverTitleScript_5[1].DoIt();
		}
		if (flag3)
		{
			battleOverTitleScript_6[0].nameTxt = Localizer.Get("ui.battle_over_window.other_team_win");
			battleOverTitleScript_6[0].killsTxt = ((byte_0 != 1) ? int_5.ToString() : int_6.ToString());
			battleOverTitleScript_6[0].DoIt();
		}
		else
		{
			battleOverTitleScript_6[1].nameTxt = Localizer.Get("ui.battle_over_window.other_team_lose");
			battleOverTitleScript_6[1].killsTxt = ((byte_0 != 1) ? int_5.ToString() : int_6.ToString());
			battleOverTitleScript_6[1].DoIt();
		}
	}

	private void findMyRewardAndPlace(List<BattleOverPlayerData> list_2, out int int_5, out int int_6, out int int_7)
	{
		int_5 = 0;
		int_6 = 0;
		int_7 = 0;
		for (int i = 0; i < list_2.Count; i++)
		{
			BattleOverPlayerData battleOverPlayerData = list_2[i];
			if (battleOverPlayerData.int_1 == UserController.UserController_0.UserData_0.user_0.int_0)
			{
				int_5 = battleOverPlayerData.int_0;
				int_6 = battleOverPlayerData.int_10;
				int_7 = battleOverPlayerData.int_11;
			}
		}
	}

	private void deathmatchReposition(UIWidget uiwidget_6, UIWidget uiwidget_7, GameObject gameObject_11, GameObject gameObject_12)
	{
		if (transform_0 == null)
		{
			transform_0 = uiwidget_6.transform;
		}
		if (transform_2 == null)
		{
			transform_2 = uiwidget_7.transform;
		}
		if (transform_3 == null)
		{
			transform_3 = gameObject_11.transform;
		}
		float num = (float)(int_0 * gameObject_12.gameObject.GetComponent<UIWidget>().Int32_1 + 1 * int_0) / 2f;
		float num2 = transform_0.localPosition.y - ((float)uiwidget_6.Int32_1 / 2f + (float)uiwidget_7.Int32_1 / 2f + 27f + num / 2f);
		if (transform_2.localPosition.y != num2)
		{
			transform_2.localPosition = new Vector3(transform_2.localPosition.x, num2, transform_2.localPosition.z);
		}
		if (float_0 == 0f)
		{
			float_0 = gameObject_11.transform.localPosition.y;
		}
		float num3 = float_0 - num / 2f;
		if (transform_3.localPosition.y != num3)
		{
			transform_3.localPosition = new Vector3(transform_3.localPosition.x, num3, transform_3.localPosition.z);
		}
	}

	private void teamReposition(UIWidget uiwidget_6, UIWidget uiwidget_7, UIWidget uiwidget_8, GameObject gameObject_11, GameObject gameObject_12)
	{
		if (transform_0 == null)
		{
			transform_0 = uiwidget_6.transform;
		}
		if (transform_1 == null)
		{
			transform_1 = uiwidget_7.transform;
		}
		if (transform_2 == null)
		{
			transform_2 = uiwidget_8.transform;
		}
		if (transform_3 == null)
		{
			transform_3 = gameObject_11.transform;
		}
		float num = 0f - ((float)uiwidget_6.Int32_1 / 2f + 78f);
		float num2 = num - ((float)uiwidget_6.Int32_1 / 2f + (float)uiwidget_7.Int32_1 / 2f + 13f);
		float num3 = (float)(int_0 * gameObject_12.gameObject.GetComponent<UIWidget>().Int32_1 + 1 * int_0) / 2f;
		num -= num3;
		num2 -= num3;
		if (transform_0.localPosition.y != num)
		{
			transform_0.localPosition = new Vector3(transform_0.localPosition.x, num, transform_0.localPosition.z);
		}
		if (transform_1.localPosition.y != num2)
		{
			transform_1.localPosition = new Vector3(transform_1.localPosition.x, num2, transform_1.localPosition.z);
		}
		float num4 = num2 - ((float)uiwidget_7.Int32_1 / 2f + (float)uiwidget_8.Int32_1 / 2f + 27f + num3 / 2f);
		if (transform_2.localPosition.y != num4)
		{
			transform_2.localPosition = new Vector3(transform_2.localPosition.x, num4, transform_2.localPosition.z);
		}
		if (float_0 == 0f)
		{
			float_0 = transform_3.localPosition.y;
		}
		float num5 = float_0 - (float)(int_0 * gameObject_12.gameObject.GetComponent<UIWidget>().Int32_1 + 1 * int_0) / 4f;
		if (transform_3.localPosition.y != num5)
		{
			transform_3.localPosition = new Vector3(transform_3.localPosition.x, num5, transform_3.localPosition.z);
		}
	}

	private void UpdateKeyBoard()
	{
		Screen.lockCursor = false;
		UpdateZumKampfButton();
	}

	private void UpdateZumKampfButton()
	{
		if (!(BugReportWindow.BugReportWindow_0 != null))
		{
			float_1 += Time.deltaTime;
			if (float_1 > 3f && !uibutton_0.Boolean_0)
			{
				uibutton_0.Boolean_0 = true;
			}
			if (InputManager.GetButtonUp("Next"))
			{
				OnZumKampfButtonClick();
			}
		}
	}

	private void UpdateTimerExitFromFight()
	{
		float num = Mathf.Abs(Input.GetAxisRaw("Mouse X"));
		float num2 = Mathf.Abs(Input.GetAxisRaw("Mouse Y"));
		if (num > float.Epsilon || num2 > float.Epsilon)
		{
			ResetTimerExitFromFight();
		}
		if (Time.time >= float_2)
		{
			MessageWindow.Show(new MessageWindowParams(Localizer.Get("ui.battle_over_window.leave_timer"), null, "OK", KeyCode.Return, WindowController.GameEvent.IN_MAIN_MENU));
			Hide();
			LeaveRoom();
		}
	}

	private void ResetTimerExitFromFight()
	{
		float_2 = Time.time + 60f;
		if (UserController.UserController_0.UserData_0.user_0.bool_0 && Application.isEditor)
		{
			float_2 = Time.time + 99999f;
		}
	}

	private void SendLikesAndComplants()
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < list_0.Count; i++)
		{
			if (list_0[i].int_1 == UserController.UserController_0.UserData_0.user_0.int_0)
			{
				continue;
			}
			if (list_0[i].bool_0)
			{
				if (dictionary.ContainsKey(list_0[i].int_1))
				{
					dictionary[list_0[i].int_1] = dictionary[list_0[i].int_1] | 1;
				}
				else
				{
					dictionary.Add(list_0[i].int_1, 1);
				}
			}
			foreach (int value in Enum.GetValues(typeof(BattleOverPlayerData.ComplaintType)))
			{
				if (list_0[i].dictionary_0[value])
				{
					int num = 0;
					switch (value)
					{
					case 0:
						num = 2;
						break;
					case 1:
						num = 4;
						break;
					case 2:
						num = 8;
						break;
					case 3:
						num = 16;
						break;
					case 1000:
						num = 32;
						break;
					case 1001:
						num = 64;
						break;
					case 1002:
						num = 128;
						break;
					case 1003:
						num = 256;
						break;
					}
					if (dictionary.ContainsKey(list_0[i].int_1))
					{
						dictionary[list_0[i].int_1] = dictionary[list_0[i].int_1] | num;
					}
					else
					{
						dictionary.Add(list_0[i].int_1, num);
					}
				}
			}
		}
		if (dictionary.Count > 0)
		{
			LikesAndComplaintsCommand likesAndComplaintsCommand = new LikesAndComplaintsCommand();
			likesAndComplaintsCommand.dictionary_0 = dictionary;
			AbstractNetworkCommand.Send(likesAndComplaintsCommand);
		}
	}

	public void OnBackButtonClick()
	{
		ButtonClickSound.buttonClickSound_0.PlayClick();
		ShowLevelUpIfNeed(LeaveRoom);
	}

	public void OnZumKampfButtonClick()
	{
		if (!(float_1 < 3f))
		{
			float_1 = 0f;
			if (bool_1)
			{
				ButtonClickSound.buttonClickSound_0.PlayClick();
				ShowLevelUpIfNeed(StartBattle);
			}
			if (RankTrophyChangeWindow.Boolean_1)
			{
				RankTrophyChangeWindow.RankTrophyChangeWindow_0.Hide();
			}
		}
	}

	private void ShowLevelUpIfNeed(Action action_0)
	{
		if (action_0 != null)
		{
			if (int_2 != 0)
			{
				Hide();
				LvlUpWindow.Show(new LvlUpWindowParams(int_2, action_0));
			}
			else
			{
				action_0();
			}
		}
	}

	private void LeaveRoom()
	{
		if (PhotonNetwork.Room_0 != null)
		{
			MonoSingleton<FightController>.Prop_0.LeaveRoom(true);
		}
	}

	private void StartBattle()
	{
		WeaponManager.weaponManager_0.myNetworkStartTable.CreatePlayerInFight();
	}

	private void setComplaintPanelContent()
	{
		GameObject gameObject = NGUITools.AddChild(uitable_5.gameObject, gameObject_9);
		ComplaintPanelItem component = gameObject.GetComponent<ComplaintPanelItem>();
		component.text.String_0 = Localizer.Get("ui.battle_over_window.spam");
		component.type = BattleOverPlayerData.ComplaintType.SPAM;
		gameObject.SetActive(true);
		gameObject = NGUITools.AddChild(uitable_5.gameObject, gameObject_9);
		component = gameObject.GetComponent<ComplaintPanelItem>();
		component.text.String_0 = Localizer.Get("ui.battle_over_window.threat");
		component.type = BattleOverPlayerData.ComplaintType.THREAT;
		gameObject.SetActive(true);
		gameObject = NGUITools.AddChild(uitable_5.gameObject, gameObject_9);
		component = gameObject.GetComponent<ComplaintPanelItem>();
		component.text.String_0 = Localizer.Get("ui.battle_over_window.inaction");
		component.type = BattleOverPlayerData.ComplaintType.INACTION;
		gameObject.SetActive(true);
		gameObject = NGUITools.AddChild(uitable_5.gameObject, gameObject_9);
		component = gameObject.GetComponent<ComplaintPanelItem>();
		component.text.String_0 = Localizer.Get("ui.battle_over_window.unprintable_nick");
		component.type = BattleOverPlayerData.ComplaintType.UNPRINTABLE_NICK;
		gameObject.SetActive(true);
		gameObject = NGUITools.AddChild(uitable_5.gameObject, gameObject_10);
		ComplaintPanelSecondItem component2 = gameObject.GetComponent<ComplaintPanelSecondItem>();
		component2.myNum = 1;
		gameObject.SetActive(true);
		shrinkPanel_0.Objects[0].body.Int32_1 = Convert.ToInt32((float)(gameObject_9.GetComponent<UIWidget>().Int32_1 * 5) + uitable_5.vector2_0.y * 5f);
		shrinkPanel_0.Objects[0].height = shrinkPanel_0.Objects[0].body.Int32_1;
		float num = shrinkPanel_0.Objects[0].body.Int32_1 + shrinkPanel_0.Objects[0].header.Int32_1;
		shrinkPanel_0.Objects[1].panel.transform.localPosition = new Vector3(shrinkPanel_0.Objects[1].panel.transform.localPosition.x, shrinkPanel_0.Objects[0].panel.transform.localPosition.y - num, shrinkPanel_0.Objects[1].panel.transform.localPosition.z);
		gameObject = NGUITools.AddChild(uitable_6.gameObject, gameObject_9);
		component = gameObject.GetComponent<ComplaintPanelItem>();
		component.text.String_0 = Localizer.Get("ui.battle_over_window.wall_hack");
		component.type = BattleOverPlayerData.ComplaintType.WALL_HACK;
		gameObject.SetActive(true);
		gameObject = NGUITools.AddChild(uitable_6.gameObject, gameObject_9);
		component = gameObject.GetComponent<ComplaintPanelItem>();
		component.text.String_0 = Localizer.Get("ui.battle_over_window.health");
		component.type = BattleOverPlayerData.ComplaintType.HEALTH;
		gameObject.SetActive(true);
		gameObject = NGUITools.AddChild(uitable_6.gameObject, gameObject_9);
		component = gameObject.GetComponent<ComplaintPanelItem>();
		component.text.String_0 = Localizer.Get("ui.battle_over_window.run");
		component.type = BattleOverPlayerData.ComplaintType.FAST_RUN;
		gameObject.SetActive(true);
		gameObject = NGUITools.AddChild(uitable_6.gameObject, gameObject_9);
		component = gameObject.GetComponent<ComplaintPanelItem>();
		component.text.String_0 = Localizer.Get("ui.battle_over_window.time_cheat");
		component.type = BattleOverPlayerData.ComplaintType.TIME_CHEAT;
		gameObject.SetActive(true);
		gameObject = NGUITools.AddChild(uitable_6.gameObject, gameObject_10);
		component2 = gameObject.GetComponent<ComplaintPanelSecondItem>();
		component2.myNum = 2;
		gameObject.SetActive(true);
		shrinkPanel_0.Objects[1].body.Int32_1 = Convert.ToInt32((float)(gameObject_9.GetComponent<UIWidget>().Int32_1 * 5) + uitable_5.vector2_0.y * 5f) + 4;
		shrinkPanel_0.Objects[1].height = shrinkPanel_0.Objects[1].body.Int32_1 - 4;
		gameObject_9.SetActive(false);
		gameObject_10.SetActive(false);
		shrinkPanel_0.gameObject.SetActive(false);
	}

	public void ShowShrinkPanel(GameObject gameObject_11, BattleOverPlayerData battleOverPlayerData_0, bool bool_2)
	{
		if (int_1 != 0)
		{
			OnMissClick();
		}
		else
		{
			if (battleOverPlayerData_0.int_1 == UserController.UserController_0.UserData_0.user_0.int_0)
			{
				return;
			}
			int_1 = battleOverPlayerData_0.int_1;
			string_0 = battleOverPlayerData_0.string_0;
			Transform transform = uitable_5.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				ComplaintPanelItem component = transform.GetChild(i).gameObject.GetComponent<ComplaintPanelItem>();
				if (component != null)
				{
					component.sended = battleOverPlayerData_0.dictionary_0[(int)component.type];
					component.SetActiveContent();
				}
			}
			Transform transform2 = uitable_6.transform;
			for (int j = 0; j < transform2.childCount; j++)
			{
				ComplaintPanelItem component2 = transform2.GetChild(j).gameObject.GetComponent<ComplaintPanelItem>();
				if (component2 != null)
				{
					component2.sended = battleOverPlayerData_0.dictionary_0[(int)component2.type];
					component2.SetActiveContent();
				}
			}
			bool flag = ClanController.ClanController_0.UserClanData_0 != null && ClanController.ClanController_0.UserClanData_0.int_0 == UserController.UserController_0.UserData_0.user_0.int_0;
			bool flag2 = ClanController.ClanController_0.UserClanData_0 != null && (ClanController.ClanController_0.UserClanData_0.IsMemderOfClan(int_1) || !battleOverPlayerData_0.string_1.IsNullOrEmpty());
			Vector3 position = gameObject_11.transform.position;
			Vector3 vector = default(Vector3);
			float num = ((!flag) ? 0f : 0.1f);
			vector = ((!bool_2) ? new Vector3(position.x - 0.48f, position.y - 0.44f - num, position.z) : new Vector3(position.x + 0.55f, position.y - 0.41f - num, position.z));
			vector.y -= 0.1f;
			shrinkPanel_0.transform.position = vector;
			shrinkPanel_0.gameObject.SetActive(true);
			bool flag3 = flag && !flag2 && !battleOverPlayerData_0.dictionary_0[2000];
			ActionPlayerNick component3 = uipanel_0.GetComponent<ActionPlayerNick>();
			component3.Show(battleOverPlayerData_0.int_1, flag3, false, true);
			component3.SetCloseAction(OnMissClick);
			component3.SetInviteAction(OnInviteToClan);
			shrinkPanel_0.ForceOpenAll();
		}
	}

	public void OnComplaint(ComplaintPanelItem complaintPanelItem_0)
	{
		int num = 0;
		while (true)
		{
			if (num < list_0.Count)
			{
				if (list_0[num].int_1 == int_1)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		list_0[num].dictionary_0[(int)complaintPanelItem_0.type] = true;
		if (complaintPanelItem_0.type < BattleOverPlayerData.ComplaintType.WALL_HACK)
		{
			Int32_0++;
		}
		else
		{
			Int32_1++;
		}
	}

	public void OnMissClick()
	{
		shrinkPanel_0.gameObject.SetActive(false);
		int_1 = 0;
		string_0 = string.Empty;
	}

	public void OnInviteToClan()
	{
		for (int i = 0; i < list_0.Count; i++)
		{
			if (list_0[i].int_1 == int_1)
			{
				list_0[i].dictionary_0[2000] = true;
			}
		}
	}

	public void SetDisableAllLikes(bool bool_2)
	{
		for (int i = 0; i < list_0.Count; i++)
		{
			list_0[i].bool_0 = false;
			list_0[i].bool_1 = bool_2;
		}
		for (int j = 0; j < list_1.Count; j++)
		{
			list_1[j].likeGreen.transform.GetChild(0).GetComponent<TweenColor>().enabled = false;
			list_1[j].likeGreen.transform.GetChild(1).GetComponent<Animation>().enabled = false;
			list_1[j].likeGreen.gameObject.SetActive(false);
			if (list_1[j].data.int_1 == UserController.UserController_0.UserData_0.user_0.int_0)
			{
				list_1[j].likeBlue.gameObject.SetActive(false);
				list_1[j].likeGray.gameObject.SetActive(true);
			}
			else
			{
				list_1[j].likeBlue.gameObject.SetActive(!bool_2);
				list_1[j].likeGray.gameObject.SetActive(bool_2);
			}
		}
	}
}
