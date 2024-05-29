using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.helpers;
using engine.unity;

[GameWindowParams(GameWindowType.MapList)]
public class MapListWindow : BaseGameWindow
{
	public enum CompareType
	{
		ModeUp = 0,
		ModeDown = 1,
		DurationUp = 2,
		DurationDown = 3,
		MapUp = 4,
		MapDown = 5,
		PopulationUp = 6,
		PopulationDown = 7,
		RoomNameUp = 8,
		RoomNameDown = 9,
		PasswordUp = 10,
		PasswordDown = 11
	}

	private const float float_0 = 3f;

	private const float float_1 = 0.5f;

	private static MapListWindow mapListWindow_0 = null;

	public UITable uitable_0;

	public UIScrollView uiscrollView_0;

	public UIWidget uiwidget_0;

	public MapListTableObject mapListTableObject_0;

	public GameObject gameObject_0;

	public GameObject gameObject_1;

	public GameObject gameObject_2;

	public GameObject gameObject_3;

	public UIPopupList uipopupList_0;

	public UISprite[] uisprite_0;

	public UILabel uilabel_0;

	public UISprite uisprite_1;

	public UIInput uiinput_0;

	public UIPopupList uipopupList_1;

	public UILabel uilabel_1;

	public UIInput uiinput_1;

	public UIInput uiinput_2;

	public UIInput uiinput_3;

	public UILabel uilabel_2;

	public UIButton uibutton_0;

	public UISprite uisprite_2;

	public UISprite uisprite_3;

	public UIWidget uiwidget_1;

	private MapListTableObject mapListTableObject_1;

	private Comparison<RoomItemData> comparison_0;

	private CompareType compareType_0;

	private List<RoomItemData> list_0 = new List<RoomItemData>();

	private List<RoomItemData> list_1 = new List<RoomItemData>();

	private List<GameObject> list_2 = new List<GameObject>();

	private List<FilterDelegate> list_3 = new List<FilterDelegate>();

	private int int_0;

	private string string_0;

	private int int_1;

	private int int_2;

	private int int_3;

	private RoomItemData roomItemData_0;

	private float float_2;

	private static List<string> list_4 = new List<string>();

	private static Dictionary<string, int> dictionary_1 = new Dictionary<string, int>();

	[CompilerGenerated]
	private static Comparison<KeyValuePair<int, string>> comparison_1;

	public static MapListWindow MapListWindow_0
	{
		get
		{
			return mapListWindow_0;
		}
	}

	private void OnEnable()
	{
		if (!MonoSingleton<FightController>.Prop_0.FightRoomsController_0.Contains(UpdateAll, RoomsEventType.UpdateMaps))
		{
			MonoSingleton<FightController>.Prop_0.FightRoomsController_0.Subscribe(UpdateAll, RoomsEventType.UpdateMaps);
		}
	}

	private void OnDisable()
	{
		if (MonoSingleton<FightController>.Prop_0.FightRoomsController_0.Contains(UpdateAll, RoomsEventType.UpdateMaps))
		{
			MonoSingleton<FightController>.Prop_0.FightRoomsController_0.Unsubscribe(UpdateAll, RoomsEventType.UpdateMaps);
		}
	}

	public static void Show(MapListWindowParams mapListWindowParams_0 = null)
	{
		if (!(mapListWindow_0 != null))
		{
			mapListWindow_0 = BaseWindow.Load("MapListWindow") as MapListWindow;
			mapListWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			mapListWindow_0.Parameters_0.bool_5 = false;
			mapListWindow_0.Parameters_0.bool_0 = false;
			mapListWindow_0.Parameters_0.bool_6 = true;
			mapListWindow_0.InternalShow(mapListWindowParams_0);
		}
	}

	public override void OnShow()
	{
		NGUITools.SetActive(uiwidget_1.gameObject, ClanController.ClanController_0.UserClanData_0 != null);
		if (ClanController.ClanController_0.UserClanData_0 == null)
		{
			uisprite_2.String_0 = "chat_big_bkg1-2";
			uisprite_3.String_0 = "chat_big_bkg2-2";
		}
		fillModeTypes();
		fillMapNames();
		list_3.Add(RoomNameFilter);
		list_3.Add(ModeFilter);
		list_3.Add(MapFilter);
		list_3.Add(PlayersFilter);
		list_3.Add(DurationFilter);
		comparison_0 = PopularityCompareDown;
		compareType_0 = CompareType.PopulationDown;
		UpdateAll(true);
		for (int i = 0; i < list_4.Count; i++)
		{
			uipopupList_0.items.Add(list_4[i]);
		}
		uipopupList_0.String_0 = list_4[0];
		foreach (KeyValuePair<string, int> item in dictionary_1)
		{
			uipopupList_1.items.Add(item.Key);
		}
		uipopupList_1.String_0 = Localizer.Get("ui.map_list_window.all_maps");
		base.OnShow();
		Invoke("UpdateAll", 0.1f);
	}

	public override void OnHide()
	{
		base.OnHide();
		mapListWindow_0 = null;
	}

	protected override void Update()
	{
		base.Update();
		float_2 -= Time.deltaTime;
		if (float_2 <= 0f)
		{
			float_2 = 0f;
			uilabel_2.gameObject.SetActive(false);
			return;
		}
		uilabel_2.gameObject.SetActive(true);
		float num = 2.5f;
		if (float_2 > num)
		{
			uilabel_2.Color_0 = new Color(uilabel_2.Color_0.r, uilabel_2.Color_0.g, uilabel_2.Color_0.b, 1f);
			return;
		}
		float a = float_2 / num;
		uilabel_2.Color_0 = new Color(uilabel_2.Color_0.r, uilabel_2.Color_0.g, uilabel_2.Color_0.b, a);
	}

	public void UpdateAll()
	{
		UpdateAll(false);
	}

	public void UpdateAll(bool bool_1)
	{
		ReloadData();
		UpdateTable(bool_1);
	}

	private void ReloadData()
	{
		list_0.Clear();
		MonoSingleton<FightController>.Prop_0.FightRoomsController_0.GetRoomItemDataList(list_0);
	}

	private void UpdateTable(bool bool_1 = true)
	{
		filterData();
		sortData();
		updateObjectsCount();
		setData();
		RepositionContent(bool_1);
		UpdateStartBattleButton();
	}

	private void filterData()
	{
		list_1.Clear();
		for (int i = 0; i < list_0.Count; i++)
		{
			bool flag = true;
			for (int j = 0; j < list_3.Count; j++)
			{
				if (!list_3[j](list_0[i]))
				{
					flag = false;
					break;
				}
			}
			if (flag && Utility.Double_0 < list_0[i].double_0 + (double)(list_0[i].int_1 * 60) - (double)MatchMakingSettings.Get.TimeBlockToEnterBattle)
			{
				list_1.Add(list_0[i]);
			}
		}
	}

	private void sortData()
	{
		list_1.Sort(comparison_0);
	}

	private void updateObjectsCount()
	{
		if (list_1.Count > list_2.Count)
		{
			while (list_1.Count > list_2.Count)
			{
				GameObject gameObject = NGUITools.AddChild(uitable_0.gameObject, mapListTableObject_0.gameObject);
				gameObject.name = string.Format("{0:0000}", list_2.Count);
				list_2.Add(gameObject);
			}
		}
		else if (list_1.Count < list_2.Count)
		{
			while (list_1.Count < list_2.Count)
			{
				GameObject gameObject2 = list_2[list_2.Count - 1];
				list_2.RemoveAt(list_2.Count - 1);
				gameObject2.transform.parent = null;
				UnityEngine.Object.Destroy(gameObject2);
			}
		}
		NGUITools.SetActive(uiwidget_0.gameObject, (float)list_2.Count / (float)uitable_0.int_0 > 10f);
	}

	private void setData()
	{
		for (int i = 0; i < list_2.Count; i++)
		{
			MapListTableObject component = list_2[i].GetComponent<MapListTableObject>();
			component.SetData(list_1[i]);
			component.SetCallbacks(OnSelectedItem, OnUnSelectedItem, OnStartBattle);
			NGUITools.SetActive(list_2[i], true);
		}
	}

	private void RepositionContent(bool bool_1 = true)
	{
		uitable_0.Reposition();
		if (bool_1)
		{
			uiscrollView_0.ResetPosition();
		}
	}

	private void fillModeTypes()
	{
		list_4.Clear();
		list_4.Add(Localizer.Get("ui.map_list_window.all_modes"));
		list_4.Add(Localizer.Get("ui.map_list_window.teamfight"));
		list_4.Add(Localizer.Get("ui.map_list_window.flagcapture"));
		list_4.Add(Localizer.Get("ui.map_list_window.dethmatch"));
	}

	private void fillMapNames()
	{
		dictionary_1.Clear();
		dictionary_1.Add(Localizer.Get("ui.map_list_window.all_maps"), 0);
		Dictionary<int, string> dictionary = new Dictionary<int, string>();
		foreach (KeyValuePair<int, ModeData> item in (IEnumerable<KeyValuePair<int, ModeData>>)ModeStorage.Get.Storage)
		{
			if (!dictionary.ContainsKey(item.Value.Int32_1) && item.Value.Boolean_1 && !item.Value.Boolean_4 && (item.Value.ModeType_0 == ModeType.DEATH_MATCH || item.Value.ModeType_0 == ModeType.TEAM_FIGHT || item.Value.ModeType_0 == ModeType.FLAG_CAPTURE) && item.Value.ModeType_0 != ModeType.DUEL)
			{
				MapData objectByKey = MapStorage.Get.Storage.GetObjectByKey(item.Value.Int32_1);
				if (objectByKey != null && objectByKey.Boolean_0 && !objectByKey.Boolean_3)
				{
					dictionary.Add(objectByKey.Int32_0, Localizer.Get(objectByKey.String_1));
				}
			}
		}
		List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
		foreach (KeyValuePair<int, string> item2 in dictionary)
		{
			list.Add(item2);
		}
		list.Sort((KeyValuePair<int, string> keyValuePair_0, KeyValuePair<int, string> keyValuePair_1) => keyValuePair_0.Value.CompareTo(keyValuePair_1.Value));
		for (int i = 0; i < list.Count; i++)
		{
			dictionary_1.Add(list[i].Value, list[i].Key);
		}
	}

	private void OnSelectedItem(MapListTableObject mapListTableObject_2)
	{
		if (mapListTableObject_1 != null)
		{
			mapListTableObject_1.SetSelected(false);
		}
		mapListTableObject_1 = mapListTableObject_2;
		UpdateStartBattleButton();
	}

	private void OnUnSelectedItem(MapListTableObject mapListTableObject_2)
	{
		mapListTableObject_1 = null;
		UpdateStartBattleButton();
	}

	private void OnStartBattle(MapListTableObject mapListTableObject_2)
	{
		StartBattle(mapListTableObject_2.RoomItemData_0, string.Empty);
	}

	private void UpdateStartBattleButton()
	{
		if (!(uibutton_0 == null))
		{
			uibutton_0.Boolean_0 = mapListTableObject_1 != null;
		}
	}

	private void StartBattle(RoomItemData roomItemData_1, string string_1 = "", bool bool_1 = false)
	{
		if (roomItemData_1 != null)
		{
			if (roomItemData_1.int_3 == roomItemData_1.int_2)
			{
				MessageWindow.Show(new MessageWindowParams(LocalizationStorage.Get.Term("window.msg.to_many_players")));
			}
			else if (string.IsNullOrEmpty(roomItemData_1.string_4) && string.IsNullOrEmpty(string_1) && !bool_1)
			{
				StartBattleNow(roomItemData_1);
			}
			else if (string.IsNullOrEmpty(string_1) && !bool_1)
			{
				gameObject_0.SetActive(false);
				gameObject_1.SetActive(false);
				gameObject_2.SetActive(false);
				gameObject_3.SetActive(true);
				roomItemData_0 = roomItemData_1;
				uiinput_3.String_2 = string.Empty;
				uiinput_3.Boolean_2 = true;
			}
			else if (string.Equals(roomItemData_1.string_4, string_1))
			{
				StartBattleNow(roomItemData_1);
			}
			else
			{
				float_2 = 3f;
			}
		}
	}

	private void StartBattleNow(RoomItemData roomItemData_1)
	{
		if (ClanController.ClanController_0.UserClanData_0 != null && ClanController.ClanController_0.UserClanData_0.list_0.Count > 1)
		{
			ClanInviteBattleWindowParams clanInviteBattleWindowParams = new ClanInviteBattleWindowParams();
			clanInviteBattleWindowParams.string_0 = roomItemData_1.string_1;
			clanInviteBattleWindowParams.modeData_0 = roomItemData_1.modeData_0;
			clanInviteBattleWindowParams.string_1 = uiinput_3.String_2;
			clanInviteBattleWindowParams.bool_0 = false;
			clanInviteBattleWindowParams.int_5 = 2;
			ClanInviteBattleWindow.Show(clanInviteBattleWindowParams);
		}
		else
		{
			if (MapListWindow_0 != null)
			{
				MapListWindow_0.Hide();
			}
			if (SelectMapWindow.SelectMapWindow_0 != null)
			{
				SelectMapWindow.SelectMapWindow_0.Hide();
			}
			MonoSingleton<FightController>.Prop_0.JoinRoom(roomItemData_1.string_1);
		}
	}

	public void OnHideButtonClick()
	{
		if (!gameObject_3.activeSelf)
		{
			Hide();
			return;
		}
		gameObject_0.SetActive(true);
		gameObject_1.SetActive(true);
		gameObject_2.SetActive(true);
		gameObject_3.SetActive(false);
		roomItemData_0 = null;
		UpdateAll(false);
	}

	public void OnBattleClick()
	{
		if (!(mapListTableObject_1 == null))
		{
			StartBattle(mapListTableObject_1.RoomItemData_0, string.Empty);
		}
	}

	public void OnPasswordBattleClick()
	{
		StartBattle(roomItemData_0, uiinput_3.String_2, true);
	}

	public void OnRoomNameDeleteClick()
	{
		uiinput_0.String_2 = string.Empty;
	}

	public void OnPlayersDeleteClick()
	{
		uiinput_1.String_2 = "00";
		if (int_2 != 0)
		{
			int_2 = 0;
			UpdateTable();
		}
	}

	public void OnPlayersMinusClick()
	{
		int num = 0;
		try
		{
			num = Convert.ToInt32(uiinput_1.String_2);
		}
		catch (Exception)
		{
		}
		if (num > 0)
		{
			num--;
		}
		if (int_2 != num)
		{
			int_2 = num;
			UpdateTable();
			uiinput_1.String_2 = string.Format("{0:00}", int_2);
		}
	}

	public void OnPlayersPlusClick()
	{
		int num = 0;
		try
		{
			num = Convert.ToInt32(uiinput_1.String_2);
		}
		catch (Exception)
		{
		}
		if (num < 16)
		{
			num++;
		}
		if (int_2 != num)
		{
			int_2 = num;
			UpdateTable();
			uiinput_1.String_2 = string.Format("{0:00}", int_2);
		}
	}

	public void OnDurationDeleteClick()
	{
		uiinput_2.String_2 = "00";
		if (int_3 != 0)
		{
			int_3 = 0;
			UpdateTable();
		}
	}

	public void OnDurationMinusClick()
	{
		int num = 0;
		try
		{
			num = Convert.ToInt32(uiinput_2.String_2);
		}
		catch (Exception)
		{
		}
		if (num > 0)
		{
			num--;
		}
		if (int_3 != num)
		{
			int_3 = num;
			UpdateTable();
			uiinput_2.String_2 = string.Format("{0:00}", int_3);
		}
	}

	public void OnDurationPlusClick()
	{
		int num = 0;
		try
		{
			num = Convert.ToInt32(uiinput_2.String_2);
		}
		catch (Exception)
		{
		}
		if (num < 15)
		{
			num++;
		}
		if (int_3 != num)
		{
			int_3 = num;
			UpdateTable();
			uiinput_2.String_2 = string.Format("{0:00}", int_3);
		}
	}

	public void OnCreateButtonClick()
	{
		CreateBattleWindow.Show();
	}

	public void OnRoomNameFilterInput()
	{
		if (!string.Equals(string_0, uiinput_0.String_2))
		{
			string_0 = uiinput_0.String_2;
			UpdateTable();
		}
	}

	public void OnModePopupValueChange()
	{
		string a = uipopupList_0.String_0;
		int num = 0;
		for (int i = 1; i < list_4.Count; i++)
		{
			if (string.Equals(a, list_4[i]))
			{
				num = i;
				break;
			}
		}
		uilabel_0.String_0 = a;
		for (int j = 0; j < 5; j++)
		{
			uisprite_0[j].gameObject.SetActive(false);
		}
		switch (num)
		{
		case 0:
			uisprite_0[0].gameObject.SetActive(true);
			break;
		case 1:
			uisprite_0[2].gameObject.SetActive(true);
			break;
		case 2:
			uisprite_0[3].gameObject.SetActive(true);
			break;
		case 3:
			uisprite_0[1].gameObject.SetActive(true);
			break;
		case 4:
			uisprite_0[4].gameObject.SetActive(true);
			break;
		}
		if (int_0 != num)
		{
			int_0 = num;
			UpdateTable();
		}
	}

	public void OnMapPopupValueChange()
	{
		int num = 0;
		if (dictionary_1.ContainsKey(uipopupList_1.String_0))
		{
			num = dictionary_1[uipopupList_1.String_0];
		}
		uilabel_1.String_0 = uipopupList_1.String_0;
		if (int_1 != num)
		{
			int_1 = num;
			UpdateTable();
		}
	}

	public void OnPlayerInput()
	{
		int num = 0;
		try
		{
			num = Convert.ToInt32(uiinput_1.String_2);
		}
		catch (Exception)
		{
		}
		if (int_2 != num)
		{
			int_2 = num;
			UpdateTable();
			uiinput_1.String_2 = string.Format("{0:00}", int_2);
		}
	}

	public void OnDurationInput()
	{
		int num = 0;
		try
		{
			num = Convert.ToInt32(uiinput_2.String_2);
		}
		catch (Exception)
		{
		}
		if (int_3 != num)
		{
			int_3 = num;
			UpdateTable();
			uiinput_2.String_2 = string.Format("{0:00}", int_3);
		}
	}

	private bool ModeFilter(RoomItemData roomItemData_1)
	{
		if (int_0 == 0)
		{
			return true;
		}
		if (int_0 == 3)
		{
			if (roomItemData_1.modeData_0.ModeType_0 == ModeType.DEATH_MATCH)
			{
				return true;
			}
		}
		else if (int_0 == 1)
		{
			if (roomItemData_1.modeData_0.ModeType_0 == ModeType.TEAM_FIGHT)
			{
				return true;
			}
		}
		else if (int_0 == 2 && roomItemData_1.modeData_0.ModeType_0 == ModeType.FLAG_CAPTURE)
		{
			return true;
		}
		return false;
	}

	private bool RoomNameFilter(RoomItemData roomItemData_1)
	{
		if (string.IsNullOrEmpty(string_0))
		{
			return true;
		}
		if (string_0.Length <= 0)
		{
			return true;
		}
		if (roomItemData_1.string_2.IndexOf(string_0, StringComparison.OrdinalIgnoreCase) == 0)
		{
			return true;
		}
		return false;
	}

	private bool MapFilter(RoomItemData roomItemData_1)
	{
		if (int_1 == 0)
		{
			return true;
		}
		if (roomItemData_1.mapData_0.Int32_0 == int_1)
		{
			return true;
		}
		return false;
	}

	private bool PlayersFilter(RoomItemData roomItemData_1)
	{
		if (int_2 <= 0)
		{
			return true;
		}
		if (int_2 <= roomItemData_1.int_2)
		{
			return true;
		}
		return false;
	}

	private bool DurationFilter(RoomItemData roomItemData_1)
	{
		if (int_3 <= 0)
		{
			return true;
		}
		if (roomItemData_1.int_1 == int_3)
		{
			return true;
		}
		return false;
	}

	public void OnModeCompareClick()
	{
		if (compareType_0 == CompareType.ModeUp)
		{
			compareType_0 = CompareType.ModeDown;
			comparison_0 = ModeCompareDown;
		}
		else
		{
			compareType_0 = CompareType.ModeUp;
			comparison_0 = ModeCompareUp;
		}
		sortData();
		setData();
		RepositionContent();
	}

	public void OnTimeCompareClick()
	{
		if (compareType_0 == CompareType.DurationUp)
		{
			compareType_0 = CompareType.DurationDown;
			comparison_0 = DurationCompareDown;
		}
		else
		{
			compareType_0 = CompareType.DurationUp;
			comparison_0 = DurationCompareUp;
		}
		sortData();
		setData();
		RepositionContent();
	}

	public void OnMapCompareClick()
	{
		if (compareType_0 == CompareType.MapUp)
		{
			compareType_0 = CompareType.MapDown;
			comparison_0 = MapCompareDown;
		}
		else
		{
			compareType_0 = CompareType.MapUp;
			comparison_0 = MapCompareUp;
		}
		sortData();
		setData();
		RepositionContent();
	}

	public void OnPopularityCompareClick()
	{
		if (compareType_0 == CompareType.PopulationUp)
		{
			compareType_0 = CompareType.PopulationDown;
			comparison_0 = PopularityCompareDown;
		}
		else
		{
			compareType_0 = CompareType.PopulationUp;
			comparison_0 = PopularityCompareUp;
		}
		sortData();
		setData();
		RepositionContent();
	}

	public void OnRoomNameCompareClick()
	{
		if (compareType_0 == CompareType.RoomNameUp)
		{
			compareType_0 = CompareType.RoomNameDown;
			comparison_0 = RoomNameCompareDown;
		}
		else
		{
			compareType_0 = CompareType.RoomNameUp;
			comparison_0 = RoomNameCompareUp;
		}
		sortData();
		setData();
		RepositionContent();
	}

	public void OnPassCompareClick()
	{
		if (compareType_0 == CompareType.PasswordUp)
		{
			compareType_0 = CompareType.PasswordDown;
			comparison_0 = LockCompareDowun;
		}
		else
		{
			compareType_0 = CompareType.PasswordUp;
			comparison_0 = LockCompareUp;
		}
		sortData();
		setData();
		RepositionContent();
	}

	private int ModeCompareUp(RoomItemData roomItemData_1, RoomItemData roomItemData_2)
	{
		return roomItemData_1.modeData_0.ModeType_0.CompareTo(roomItemData_2.modeData_0.ModeType_0);
	}

	private int ModeCompareDown(RoomItemData roomItemData_1, RoomItemData roomItemData_2)
	{
		return roomItemData_2.modeData_0.ModeType_0.CompareTo(roomItemData_1.modeData_0.ModeType_0);
	}

	private int DurationCompareUp(RoomItemData roomItemData_1, RoomItemData roomItemData_2)
	{
		return roomItemData_1.int_1 - roomItemData_2.int_1;
	}

	private int DurationCompareDown(RoomItemData roomItemData_1, RoomItemData roomItemData_2)
	{
		return roomItemData_2.int_1 - roomItemData_1.int_1;
	}

	private int MapCompareUp(RoomItemData roomItemData_1, RoomItemData roomItemData_2)
	{
		return roomItemData_1.string_3.CompareTo(roomItemData_2.string_3);
	}

	private int MapCompareDown(RoomItemData roomItemData_1, RoomItemData roomItemData_2)
	{
		return roomItemData_2.string_3.CompareTo(roomItemData_1.string_3);
	}

	private int PopularityCompareUp(RoomItemData roomItemData_1, RoomItemData roomItemData_2)
	{
		return roomItemData_1.int_3 - roomItemData_2.int_3;
	}

	private int PopularityCompareDown(RoomItemData roomItemData_1, RoomItemData roomItemData_2)
	{
		return roomItemData_2.int_3 - roomItemData_1.int_3;
	}

	private int RoomNameCompareUp(RoomItemData roomItemData_1, RoomItemData roomItemData_2)
	{
		if (roomItemData_1.bool_0 != roomItemData_2.bool_0)
		{
			if (roomItemData_1.bool_0)
			{
				return -1;
			}
			return 1;
		}
		return roomItemData_1.string_2.CompareTo(roomItemData_2.string_2);
	}

	private int RoomNameCompareDown(RoomItemData roomItemData_1, RoomItemData roomItemData_2)
	{
		if (roomItemData_1.bool_0 != roomItemData_2.bool_0)
		{
			if (roomItemData_1.bool_0)
			{
				return 1;
			}
			return -1;
		}
		return roomItemData_2.string_2.CompareTo(roomItemData_1.string_2);
	}

	private int LockCompareUp(RoomItemData roomItemData_1, RoomItemData roomItemData_2)
	{
		bool flag = !string.IsNullOrEmpty(roomItemData_1.string_4);
		bool value = !string.IsNullOrEmpty(roomItemData_2.string_4);
		return flag.CompareTo(value);
	}

	private int LockCompareDowun(RoomItemData roomItemData_1, RoomItemData roomItemData_2)
	{
		bool value = !string.IsNullOrEmpty(roomItemData_1.string_4);
		return (!string.IsNullOrEmpty(roomItemData_2.string_4)).CompareTo(value);
	}
}
