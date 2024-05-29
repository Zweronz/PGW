using System.Collections.Generic;
using UnityEngine;
using engine.events;
using engine.unity;

public class BattleStatWindow : BaseGameWindow
{
	private static BattleStatWindow battleStatWindow_0;

	public BattleModeContainer[] battleModeContainer_0;

	private ModeType modeType_0;

	public static BattleStatWindow BattleStatWindow_0
	{
		get
		{
			return battleStatWindow_0;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return BattleStatWindow_0 != null && battleStatWindow_0.gameObject.activeSelf;
		}
	}

	public static void Show(BattleTabWindowParams battleTabWindowParams_0 = null)
	{
		if (battleStatWindow_0 != null)
		{
			NGUITools.SetActiveSelf(battleStatWindow_0.gameObject, true);
			battleStatWindow_0.Init();
			return;
		}
		battleStatWindow_0 = BaseWindow.Load("BattleStatWindow") as BattleStatWindow;
		battleStatWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
		battleStatWindow_0.Parameters_0.bool_5 = false;
		battleStatWindow_0.Parameters_0.bool_0 = false;
		battleStatWindow_0.Parameters_0.bool_6 = false;
		battleStatWindow_0.Parameters_0.bool_7 = true;
		battleStatWindow_0.InternalShow(battleTabWindowParams_0);
	}

	public new static void Hide()
	{
		if (!(battleStatWindow_0 == null))
		{
			NGUITools.SetActiveSelf(battleStatWindow_0.gameObject, false);
			if (KillCamWindow.KillCamWindow_0 != null)
			{
				KillCamWindow.KillCamWindow_0.ShowElements();
			}
			else if (HeadUpDisplay.HeadUpDisplay_0 != null)
			{
				HeadUpDisplay.HeadUpDisplay_0.ShowElements();
			}
		}
	}

	public void HideMe()
	{
		base.Hide();
	}

	private void OnEnable()
	{
		if (!DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateTables))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(UpdateTables);
		}
		UpdateTables();
	}

	private void OnDisable()
	{
		if (DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateTables))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(UpdateTables);
		}
	}

	public override void OnShow()
	{
		base.OnShow();
		FirstInit();
	}

	public override void OnHide()
	{
		base.OnHide();
		battleStatWindow_0 = null;
	}

	private void FirstInit()
	{
		modeType_0 = MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0;
		BattleModeContainer[] array = battleModeContainer_0;
		foreach (BattleModeContainer battleModeContainer in array)
		{
			NGUITools.SetActive(battleModeContainer.gameObject, false);
		}
		Init();
	}

	private void Init()
	{
		if (KillCamWindow.KillCamWindow_0 != null)
		{
			KillCamWindow.KillCamWindow_0.HideElements();
		}
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.HideElements();
		}
		UpdateTables();
	}

	private void UpdateTables()
	{
		int num = -1;
		switch (modeType_0)
		{
		case ModeType.DEATH_MATCH:
			num = 1;
			break;
		case ModeType.TEAM_FIGHT:
			num = 0;
			break;
		case ModeType.DUEL:
			num = 1;
			break;
		case ModeType.FLAG_CAPTURE:
			num = 2;
			break;
		}
		if (num <= -1 || num >= battleModeContainer_0.Length)
		{
			return;
		}
		NGUITools.SetActive(battleModeContainer_0[num].gameObject, true);
		GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
		List<NetworkStartTable> list = new List<NetworkStartTable>();
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			NetworkStartTable component = gameObject.GetComponent<NetworkStartTable>();
			if (!(component == null) && !(component.Player_move_c_0 == null))
			{
				list.Add(component);
			}
		}
		battleModeContainer_0[num].UpdateData(list);
	}

	private new void Update()
	{
		if (InputManager.GetButtonUp("Grenade") && !WeaponManager.weaponManager_0.myPlayerMoveC.PlayerGrenadeController_0.Boolean_0)
		{
			WeaponManager.weaponManager_0.myPlayerMoveC.PlayerGrenadeController_0.GrenadeFire();
		}
	}
}
