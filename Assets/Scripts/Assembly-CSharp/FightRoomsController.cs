using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using WebSocketSharp;
using engine.events;

public class FightRoomsController : BaseEvent
{
	private Dictionary<int, int> dictionary_1 = new Dictionary<int, int>();

	private Dictionary<int, ModePopularityType> dictionary_2 = new Dictionary<int, ModePopularityType>();

	private readonly List<RoomItemData> list_1 = new List<RoomItemData>();

	private HashSet<int> hashSet_0 = new HashSet<int>();

	[CompilerGenerated]
	private RoomInfo[] roomInfo_0;

	public RoomInfo[] RoomInfo_0
	{
		[CompilerGenerated]
		get
		{
			return roomInfo_0;
		}
		[CompilerGenerated]
		private set
		{
			roomInfo_0 = value;
		}
	}

	public bool GetRoomFromList(string string_0, out ModeData modeData_0)
	{
		modeData_0 = null;
		int num = 0;
		while (true)
		{
			if (num < RoomInfo_0.Length)
			{
				if (string.Equals(string_0, RoomInfo_0[num].String_0))
				{
					break;
				}
				num++;
				continue;
			}
			return false;
		}
		modeData_0 = ModeStorage.Get.Storage.GetObjectByKey((int)RoomInfo_0[num].Hashtable_0["Mode"]);
		return true;
	}

	public bool GetRoomFreePlaceFromList(string string_0)
	{
		int num = 0;
		while (true)
		{
			if (num < RoomInfo_0.Length)
			{
				if (string.Equals(string_0, RoomInfo_0[num].String_0) && RoomInfo_0[num].Int32_0 + 1 < RoomInfo_0[num].Byte_0)
				{
					break;
				}
				num++;
				continue;
			}
			return false;
		}
		return true;
	}

	public void GetRoomItemDataList(List<RoomItemData> list_2, bool bool_0 = true, Func<RoomItemData, bool> func_1 = null)
	{
		list_2 = list_2 ?? (list_2 = new List<RoomItemData>());
		RoomInfo[] array = RoomInfo_0;
		if (array == null)
		{
			return;
		}
		for (int i = 0; i < array.Length; i++)
		{
			RoomItemData roomItemData = FilterData(array[i], bool_0);
			if (roomItemData != null && roomItemData.modeData_0.ModeType_0 != ModeType.DUEL && (func_1 == null || func_1(roomItemData)))
			{
				list_2.Add(roomItemData);
			}
		}
	}

	private RoomItemData FilterData(RoomInfo roomInfo_1, bool bool_0)
	{
		try
		{
			if (roomInfo_1.Boolean_0)
			{
				return null;
			}
			if (roomInfo_1.Byte_0 <= 0)
			{
				return null;
			}
			if (!roomInfo_1.Boolean_3)
			{
				return null;
			}
			RoomItemData roomItemData = new RoomItemData();
			roomItemData.string_0 = (string)roomInfo_1.Hashtable_0["FightId"];
			roomItemData.string_1 = roomInfo_1.String_0;
			if (roomInfo_1.Hashtable_0.ContainsKey("CustomRoomName") && roomInfo_1.Hashtable_0["CustomRoomName"] != null && !roomInfo_1.Hashtable_0["CustomRoomName"].ToString().IsNullOrEmpty())
			{
				string text = (string)roomInfo_1.Hashtable_0["CustomRoomName"];
				if (string.IsNullOrEmpty(text))
				{
					roomItemData.string_2 = Localizer.Get("ui.map_list_wnd.random_server");
				}
				else
				{
					roomItemData.string_2 = text;
				}
			}
			else if (roomInfo_1.Hashtable_0.ContainsKey("IsRanked") && (bool)roomInfo_1.Hashtable_0["IsRanked"])
			{
				roomItemData.string_2 = Localizer.Get("ui.map_list_wnd.battle_for_trophy");
			}
			else
			{
				roomItemData.string_2 = Localizer.Get("ui.map_list_wnd.random_server");
			}
			roomItemData.int_0 = (int)roomInfo_1.Hashtable_0["Mode"];
			roomItemData.int_1 = (int)roomInfo_1.Hashtable_0["RoundTime"];
			try
			{
				roomItemData.double_0 = (double)roomInfo_1.Hashtable_0["TimeMatchStart"];
			}
			catch (Exception)
			{
				roomItemData.double_0 = (int)roomInfo_1.Hashtable_0["TimeMatchStart"];
			}
			roomItemData.int_2 = roomInfo_1.Byte_0;
			roomItemData.int_3 = roomInfo_1.Int32_0;
			roomItemData.string_4 = (string)roomInfo_1.Hashtable_0["Pass"];
			roomItemData.bool_1 = (bool)roomInfo_1.Hashtable_0["IsRanked"];
			if (bool_0 && roomItemData.bool_1 && !MatchMakingSettings.Get.ShowRankBattleInMapList)
			{
				return null;
			}
			roomItemData.modeData_0 = ModeStorage.Get.Storage.GetObjectByKey(roomItemData.int_0);
			if (roomItemData.modeData_0 == null)
			{
				return null;
			}
			roomItemData.mapData_0 = MapStorage.Get.Storage.GetObjectByKey(roomItemData.modeData_0.Int32_1);
			if (roomItemData.mapData_0 == null)
			{
				return null;
			}
			roomItemData.string_3 = Localizer.Get(roomItemData.mapData_0.String_1);
			return roomItemData;
		}
		catch
		{
			return null;
		}
	}

	private RoomItemData GetRandomFakeRoom()
	{
		RoomItemData roomItemData = new RoomItemData();
		roomItemData.string_0 = "Fake";
		roomItemData.string_1 = "Fake";
		roomItemData.string_2 = UnityEngine.Random.Range(11, 999999999).ToString();
		if (hashSet_0.Count == 0)
		{
			foreach (KeyValuePair<int, ModeData> item in (IEnumerable<KeyValuePair<int, ModeData>>)ModeStorage.Get.Storage)
			{
				if ((item.Value.ModeType_0 == ModeType.DEATH_MATCH || item.Value.ModeType_0 == ModeType.TEAM_FIGHT || item.Value.ModeType_0 == ModeType.FLAG_CAPTURE) && item.Value.ModeType_0 != ModeType.DUEL)
				{
					hashSet_0.Add(item.Key);
				}
			}
		}
		int num = UnityEngine.Random.Range(0, hashSet_0.Count);
		int num2 = 0;
		foreach (int item2 in hashSet_0)
		{
			if (num2 != num)
			{
				num2++;
				continue;
			}
			roomItemData.int_0 = item2;
			break;
		}
		roomItemData.int_1 = UnityEngine.Random.Range(5, 16);
		switch (UnityEngine.Random.Range(0, 4))
		{
		case 0:
			roomItemData.int_2 = 4;
			break;
		case 1:
			roomItemData.int_2 = 6;
			break;
		case 2:
			roomItemData.int_2 = 8;
			break;
		case 3:
			roomItemData.int_2 = 16;
			break;
		}
		roomItemData.int_3 = UnityEngine.Random.Range(0, roomItemData.int_2 + 1);
		if (UnityEngine.Random.Range(0, 4) == 0)
		{
			roomItemData.string_4 = "fakepassword";
		}
		roomItemData.modeData_0 = ModeStorage.Get.Storage.GetObjectByKey(roomItemData.int_0);
		roomItemData.mapData_0 = MapStorage.Get.Storage.GetObjectByKey(roomItemData.modeData_0.Int32_1);
		roomItemData.string_3 = Localizer.Get(roomItemData.mapData_0.String_1);
		roomItemData.bool_0 = false;
		return roomItemData;
	}

	public int GetPopularity(ModeData modeData_0)
	{
		if (dictionary_1.ContainsKey(modeData_0.Int32_0))
		{
			return dictionary_1[modeData_0.Int32_0];
		}
		return 0;
	}

	public ModePopularityType GetPopularityType(ModeData modeData_0)
	{
		if (dictionary_2.ContainsKey(modeData_0.Int32_0))
		{
			return dictionary_2[modeData_0.Int32_0];
		}
		return ModePopularityType.None;
	}

	private void UpdatePopularityMap()
	{
		dictionary_1.Clear();
		dictionary_2.Clear();
		for (int i = 0; i < RoomInfo_0.Length; i++)
		{
			RoomInfo roomInfo = RoomInfo_0[i];
			int num = (int)roomInfo.Hashtable_0["Mode"];
			if (!(bool)roomInfo.Hashtable_0["IsRanked"])
			{
				continue;
			}
			if (RoomInfo_0[i].Int32_0 > ((!MatchMakingSettings.Get.IsSummPlayersCountForMapMode) ? 1 : 0))
			{
				if (dictionary_1.ContainsKey(num))
				{
					Dictionary<int, int> dictionary;
					Dictionary<int, int> dictionary2 = (dictionary = dictionary_1);
					int key;
					int key2 = (key = num);
					key = dictionary[key];
					dictionary2[key2] = key + ((!MatchMakingSettings.Get.IsSummPlayersCountForMapMode) ? 1 : ((RoomInfo_0[i].Int32_0 >= RoomInfo_0[i].Byte_0) ? 1 : RoomInfo_0[i].Int32_0));
				}
				else
				{
					dictionary_1.Add(num, (!MatchMakingSettings.Get.IsSummPlayersCountForMapMode) ? 1 : ((RoomInfo_0[i].Int32_0 >= RoomInfo_0[i].Byte_0) ? 1 : RoomInfo_0[i].Int32_0));
				}
			}
			else if (!dictionary_1.ContainsKey(num))
			{
				dictionary_1.Add(num, 0);
			}
		}
		Dictionary<ModeType, int> dictionary3 = new Dictionary<ModeType, int>();
		foreach (KeyValuePair<int, int> item in dictionary_1)
		{
			int key3 = item.Key;
			ModeData objectByKey = ModeStorage.Get.Storage.GetObjectByKey(key3);
			if (objectByKey != null)
			{
				if (dictionary3.ContainsKey(objectByKey.ModeType_0))
				{
					Dictionary<ModeType, int> dictionary4;
					Dictionary<ModeType, int> dictionary5 = (dictionary4 = dictionary3);
					ModeType modeType_;
					ModeType key4 = (modeType_ = objectByKey.ModeType_0);
					int key = dictionary4[modeType_];
					dictionary5[key4] = key + item.Value;
				}
				else
				{
					dictionary3.Add(objectByKey.ModeType_0, item.Value);
				}
			}
		}
		foreach (KeyValuePair<int, int> item2 in dictionary_1)
		{
			int key5 = item2.Key;
			int value = item2.Value;
			ModeData objectByKey2 = ModeStorage.Get.Storage.GetObjectByKey(key5);
			if (objectByKey2 != null)
			{
				int value2 = 0;
				dictionary3.TryGetValue(objectByKey2.ModeType_0, out value2);
				float num2 = 0f;
				if (value2 != 0)
				{
					num2 = (float)value * 100f / (float)value2;
				}
				ModePopularityType value3 = ModePopularityType.None;
				if (num2 >= 0f && num2 < 10f)
				{
					value3 = ModePopularityType.Few;
				}
				else if (num2 >= 10f && num2 < 20f)
				{
					value3 = ModePopularityType.Several;
				}
				else if (num2 >= 20f && num2 < 40f)
				{
					value3 = ModePopularityType.Many;
				}
				else if (num2 >= 40f)
				{
					value3 = ModePopularityType.Lots_of_players;
				}
				dictionary_2.Add(key5, value3);
			}
		}
	}

	public void OnGetRoomList()
	{
		RoomInfo_0 = PhotonNetwork.GetRoomList();
		UpdatePopularityMap();
		Dispatch(RoomsEventType.UpdateMaps);
	}
}
