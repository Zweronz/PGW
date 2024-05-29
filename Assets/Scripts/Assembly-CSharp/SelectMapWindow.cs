using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.unity;
using pixelgun.tutorial;

[GameWindowParams(GameWindowType.SelectMap)]
public class SelectMapWindow : BaseGameWindow
{
	private static SelectMapWindow selectMapWindow_0;

	public UITable uitable_0;

	public UIScrollView uiscrollView_0;

	public UIWidget uiwidget_0;

	public UITabsContentController uitabsContentController_0;

	public SelectMapWindowItem selectMapWindowItem_0;

	public UIButton uibutton_0;

	public UISprite uisprite_0;

	public UISprite uisprite_1;

	public UIWidget uiwidget_1;

	private int int_0 = -1;

	private ModeType modeType_0 = ModeType.TEAM_FIGHT;

	private int int_1;

	private bool bool_1 = true;

	private bool bool_2;

	private SelectMapWindowItem selectMapWindowItem_1;

	[CompilerGenerated]
	private static Comparison<ModeData> comparison_0;

	[CompilerGenerated]
	private static Action action_0;

	public static SelectMapWindow SelectMapWindow_0
	{
		get
		{
			return selectMapWindow_0;
		}
	}

	public static void Show(SelectMapWindowParams selectMapWindowParams_0 = null)
	{
		if (!(selectMapWindow_0 != null))
		{
			selectMapWindow_0 = BaseWindow.Load("SelectMapWindow") as SelectMapWindow;
			selectMapWindow_0.Parameters_0.type_0 = WindowQueue.Type.Default;
			selectMapWindow_0.Parameters_0.bool_5 = false;
			selectMapWindow_0.Parameters_0.bool_0 = false;
			selectMapWindow_0.Parameters_0.bool_6 = true;
			if (selectMapWindowParams_0 != null && selectMapWindowParams_0.gameEvent_0 != 0)
			{
				selectMapWindow_0.Parameters_0.gameEvent_0 = selectMapWindowParams_0.gameEvent_0;
			}
			selectMapWindow_0.InternalShow(selectMapWindowParams_0);
		}
	}

	public override void OnShow()
	{
		Init();
		UserOverrideContentGroupStorage.Subscribe(OverrideContentGroupEventType.UPDATE_ALL, ForceUpdateTabContent);
		UserOverrideContentGroupStorage.Subscribe(OverrideContentGroupEventType.REMOVE_ALL, ForceUpdateTabContent);
		base.OnShow();
	}

	public override void OnHide()
	{
		UserOverrideContentGroupStorage.Unsubscribe(OverrideContentGroupEventType.UPDATE_ALL, ForceUpdateTabContent);
		UserOverrideContentGroupStorage.Unsubscribe(OverrideContentGroupEventType.REMOVE_ALL, ForceUpdateTabContent);
		base.OnHide();
		selectMapWindow_0 = null;
		Lobby.Lobby_0.Show();
		if (TutorialController.TutorialController_0.Boolean_0)
		{
			TutorialController.TutorialController_0.HideTutor();
		}
	}

	private void Init()
	{
		NGUITools.SetActive(uiwidget_1.gameObject, ClanController.ClanController_0.UserClanData_0 != null);
		if (ClanController.ClanController_0.UserClanData_0 == null)
		{
			uisprite_0.String_0 = "chat_big_bkg1-2";
			uisprite_1.String_0 = "chat_big_bkg2-2";
		}
		int_0 = -1;
		uitable_0.onReposition_0 = delegate
		{
			uiscrollView_0.UpdateScrollbars(true);
		};
		uitabsContentController_0.onTabActive = SetTabContent;
		selectMapWindowItem_0.gameObject.SetActive(false);
		uiscrollView_0.verticalScrollBar.Single_0 = 1f;
		uiscrollView_0.UpdatePosition();
	}

	private void ForceUpdateTabContent(OverrideContentGroupEventData overrideContentGroupEventData_0)
	{
		bool_2 = true;
		SetTabContent(int_0);
	}

	private void SetTabContent(int int_2)
	{
		if (int_0 != int_2 || bool_2)
		{
			bool_2 = false;
			int_0 = int_2;
			modeType_0 = ModeType.TEAM_FIGHT;
			switch (int_2)
			{
			case 1:
				modeType_0 = ModeType.DEATH_MATCH;
				break;
			case 2:
				modeType_0 = ModeType.FLAG_CAPTURE;
				break;
			case 3:
				modeType_0 = ModeType.DUEL;
				break;
			}
			NGUITools.SetActive(uibutton_0.gameObject, modeType_0 != ModeType.DUEL);
			ClearItems();
			List<ModeData> modesByType = ModesController.ModesController_0.GetModesByType(modeType_0);
			List<ModeData> list = new List<ModeData>(modesByType.ToArray());
			list.Sort((ModeData modeData_0, ModeData modeData_1) => modeData_0.Int32_5.CompareTo(modeData_1.Int32_5));
			int_1 = 0;
			for (int i = 0; i < list.Count; i++)
			{
				ModeData mode = list[i];
				GameObject gameObject = NGUITools.AddChild(uitable_0.gameObject, selectMapWindowItem_0.gameObject);
				gameObject.name = string.Format("{0:0000}", int_1++);
				SelectMapWindowItem component = gameObject.GetComponent<SelectMapWindowItem>();
				component.mode = mode;
				component.initSelected = i == 0;
				component.SetCallbacks(OnSelectedItem);
				NGUITools.SetActive(gameObject, true);
			}
			NGUITools.SetActive(uiwidget_0.gameObject, (float)int_1 / (float)uitable_0.int_0 > 2f);
			RepositionContent();
		}
	}

	public void OnCreateButtnoClick()
	{
		if (MatchMakingSettings.Get.ShowRankBattleInMapList)
		{
			MapListWindow.Show();
		}
		else if (SettingsController.bool_0 && !MatchMakingSettings.Get.ShowRankBattleInMapList)
		{
			MessageWindowCheckConfirm.Show(new MessageWindowCheckConfirmParams(Localizer.Get("ui.select_map.friendly_confirm"), delegate
			{
				SettingsController.bool_0 = !MessageWindowCheckConfirm.MessageWindowCheckConfirm_0.uitoggle_0.Boolean_0;
				MapListWindow.Show();
			}, "OK", KeyCode.None, null, string.Empty));
		}
		else
		{
			MapListWindow.Show();
		}
	}

	public void OnFightButtonClick()
	{
		MonoSingleton<FightController>.Prop_0.StartFightForModeType(modeType_0);
	}

	private void ClearItems()
	{
		selectMapWindowItem_1 = null;
		List<Transform> list_ = uitable_0.List_0;
		foreach (Transform item in list_)
		{
			if (!(item == null))
			{
				item.parent = null;
				UnityEngine.Object.Destroy(item.gameObject);
			}
		}
	}

	private void RepositionContent()
	{
		uitable_0.Reposition();
		if (bool_1)
		{
			bool_1 = false;
		}
		else
		{
			uiscrollView_0.ResetPosition();
		}
	}

	private void OnSelectedItem(SelectMapWindowItem selectMapWindowItem_2)
	{
		if (selectMapWindowItem_1 != null)
		{
			selectMapWindowItem_1.SetSelected(false);
		}
		selectMapWindowItem_1 = selectMapWindowItem_2;
	}
}
