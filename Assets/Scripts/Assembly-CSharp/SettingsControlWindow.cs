using System.Collections.Generic;
using UnityEngine;
using engine.helpers;
using engine.unity;

[GameWindowParams(GameWindowType.SettingsControl)]
public class SettingsControlWindow : BaseGameWindow
{
	private static SettingsControlWindow settingsControlWindow_0 = null;

	public UIScrollView uiscrollView_0;

	public UIGrid uigrid_0;

	public SettingsControlItem settingsControlItem_0;

	public GameObject gameObject_0;

	public GameObject gameObject_1;

	public UIButton uibutton_0;

	public UILabel uilabel_0;

	public GameObject gameObject_2;

	private Dictionary<int, SettingsControlItem> dictionary_1 = new Dictionary<int, SettingsControlItem>();

	private HashSet<SettingsControlItemData> hashSet_0 = new HashSet<SettingsControlItemData>();

	private Dictionary<int, KeyCode> dictionary_2 = new Dictionary<int, KeyCode>();

	private bool bool_1;

	private bool bool_2 = true;

	private bool bool_3;

	private static HashSet<string> hashSet_1 = new HashSet<string> { "Sprint" };

	public static SettingsControlWindow SettingsControlWindow_0
	{
		get
		{
			return settingsControlWindow_0;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return bool_1;
		}
	}

	public bool Boolean_2
	{
		get
		{
			return bool_2;
		}
		set
		{
			if (bool_2 != value)
			{
				gameObject_2.SetActive(!value);
			}
			bool_2 = value;
		}
	}

	public static bool Boolean_3
	{
		get
		{
			return SettingsControlWindow_0 != null && SettingsControlWindow_0.Boolean_0;
		}
	}

	public static void Show(SettingsControlWindowParams settingsControlWindowParams_0)
	{
		if (!(settingsControlWindow_0 != null))
		{
			settingsControlWindow_0 = BaseWindow.Load("SettingsControlWindow") as SettingsControlWindow;
			settingsControlWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			settingsControlWindow_0.Parameters_0.bool_5 = true;
			settingsControlWindow_0.Parameters_0.bool_0 = false;
			settingsControlWindow_0.Parameters_0.bool_6 = false;
			settingsControlWindow_0.InternalShow(settingsControlWindowParams_0);
		}
	}

	public override void OnShow()
	{
		gameObject_2.SetActive(false);
		Init();
		bool_3 = ((SettingsControlWindowParams)base.WindowShowParameters_0).Boolean_0;
		base.OnShow();
	}

	public override void OnHide()
	{
		base.OnHide();
		settingsControlWindow_0 = null;
		if (bool_3)
		{
			ShowSettingPanel();
		}
	}

	private new void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape) && Boolean_2)
		{
			if (Boolean_1)
			{
				ClearCached();
				UpdateItems();
				CheckConflicts();
			}
			else
			{
				Hide();
			}
		}
	}

	private void ShowSettingPanel()
	{
		SettingsWindow.Show();
	}

	private void Init()
	{
		ClearCached();
		CheckConflicts();
		ResetTemplate();
		UpdateItems();
	}

	private void ResetTemplate()
	{
		settingsControlItem_0.transform.position = Vector3.zero;
		NGUITools.SetActive(settingsControlItem_0.gameObject, false);
	}

	private void ClearGrid()
	{
		dictionary_1.Clear();
		BetterList<Transform> childList = uigrid_0.GetChildList();
		foreach (Transform item in childList)
		{
			item.parent = null;
			Object.Destroy(item.gameObject);
		}
	}

	private void UpdateItems()
	{
		ClearGrid();
		List<SettingsControlItemData> controlItems = KeyboardController.KeyboardController_0.GetControlItems();
		for (int i = 0; i < controlItems.Count; i++)
		{
			SettingsControlItemData settingsControlItemData = controlItems[i];
			if (!hashSet_1.Contains(settingsControlItemData.string_1))
			{
				GameObject gameObject = NGUITools.AddChild(uigrid_0.gameObject, settingsControlItem_0.gameObject);
				gameObject.name = string.Format("{0:00}", i);
				SettingsControlItem component = gameObject.GetComponent<SettingsControlItem>();
				settingsControlItemData.int_1 = i;
				component.SetItemData(controlItems[i], OnItemKeyChanged);
				dictionary_1.Add(i, component);
				NGUITools.SetActive(gameObject, true);
			}
		}
		uigrid_0.onReposition_0 = delegate
		{
			uiscrollView_0.UpdateScrollbars(true);
			uiscrollView_0.verticalScrollBar.Single_0 = 0f;
		};
		uigrid_0.Reposition();
	}

	private void CheckConflicts()
	{
		bool_1 = false;
		dictionary_2.Clear();
		UpdateItemsWithConflicts();
		Boolean_2 = true;
		foreach (SettingsControlItemData item in hashSet_0)
		{
			foreach (KeyValuePair<int, SettingsControlItem> item2 in dictionary_1)
			{
				SettingsControlItemData settingsControlItemData_ = item2.Value.SettingsControlItemData_0;
				Boolean_2 &= !item2.Value.keySlot.editing && !item2.Value.altKeySlot.editing;
				if ((item.string_1.Equals(settingsControlItemData_.string_1) && item.int_0 == settingsControlItemData_.int_0) || (item.string_1.Equals(settingsControlItemData_.string_1) && (!item.string_1.Equals(settingsControlItemData_.string_1) || item.int_0 == settingsControlItemData_.int_0)))
				{
					continue;
				}
				bool flag = (item.keyCode_0 == settingsControlItemData_.keyCode_0 && settingsControlItemData_.keyCode_0 != 0) || (item.keyCode_0 == settingsControlItemData_.keyCode_1 && settingsControlItemData_.keyCode_1 != KeyCode.None);
				bool flag2 = (item.keyCode_1 == settingsControlItemData_.keyCode_1 && settingsControlItemData_.keyCode_1 != 0) || (item.keyCode_1 == settingsControlItemData_.keyCode_0 && settingsControlItemData_.keyCode_0 != KeyCode.None);
				if (flag || flag2)
				{
					uilabel_0.String_0 = Localizer.Get("ui.settings_conrol.conflict_codes");
					bool_1 = true;
					KeyCode value = ((!flag) ? item.keyCode_1 : item.keyCode_0);
					if (dictionary_2.ContainsKey(item2.Key))
					{
						dictionary_2[item2.Key] = value;
					}
					else
					{
						dictionary_2.Add(item2.Key, value);
					}
					if (dictionary_2.ContainsKey(item.int_1))
					{
						dictionary_2[item.int_1] = value;
					}
					else
					{
						dictionary_2.Add(item.int_1, value);
					}
				}
			}
		}
		UpdateSaveButton();
		UpdateItemsWithConflicts();
	}

	private void UpdateSaveButton()
	{
		bool flag = hashSet_0.Count > 0 && !bool_1;
		uibutton_0.Boolean_0 = flag;
		uibutton_0.GetComponent<UISprite>().String_0 = ((!flag) ? "settings_btn_grey_inactive" : "settings_btn_yellow_active");
		NGUITools.SetActive(uilabel_0.gameObject, bool_1);
	}

	private void UpdateItemsWithConflicts()
	{
		if (dictionary_2.Count == 0)
		{
			foreach (KeyValuePair<int, SettingsControlItem> item in dictionary_1)
			{
				item.Value.SetItemState(SettingControlItemState.NORMAL);
			}
			return;
		}
		foreach (KeyValuePair<int, KeyCode> item2 in dictionary_2)
		{
			SettingsControlItem settingsControlItem = dictionary_1[item2.Key];
			settingsControlItem.SetItemState(SettingControlItemState.CONFLICT, item2.Value);
		}
	}

	private void ClearCached()
	{
		hashSet_0.Clear();
	}

	private void OnItemKeyChanged(SettingsControlItemData settingsControlItemData_0)
	{
		hashSet_0.Add(settingsControlItemData_0);
		CheckConflicts();
	}

	public void OnSupportButtonClick()
	{
		NGUITools.SetActive(gameObject_0, false);
		NGUITools.SetActive(gameObject_1, true);
	}

	public void OnDefaultButtonClick()
	{
		InputManager.SetupDefaults(string.Empty);
		InputManager.Save();
		UpdateItems();
		ClearCached();
		CheckConflicts();
		KeyboardController.KeyboardController_0.Dispatch(KeyboardControllerEvent.SETTINGS_CONTROL_CHANGED);
	}

	public void OnSaveButtonClick()
	{
		if (hashSet_0.Count == 0)
		{
			Log.AddLine("SettingsControlWindow::OnSaveButtonClick > nothing to save");
			return;
		}
		foreach (SettingsControlItemData item in hashSet_0)
		{
			if (!item.string_1.Equals("Vertical") && !item.string_1.Equals("Horizontal"))
			{
				InputManager.ChangeButtonKey(item.string_1, item.keyCode_0, item.keyCode_1);
			}
			else
			{
				InputManager.ChangeAxisKey(item.string_1, item.int_0, item.keyCode_0, item.keyCode_1);
			}
		}
		InputManager.Save();
		UpdateItems();
		ClearCached();
		CheckConflicts();
		KeyboardController.KeyboardController_0.Dispatch(KeyboardControllerEvent.SETTINGS_CONTROL_CHANGED);
	}
}
