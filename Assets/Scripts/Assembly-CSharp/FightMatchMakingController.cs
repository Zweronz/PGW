using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using UnityEngine;
using engine.helpers;
using engine.unity;

public sealed class FightMatchMakingController
{
	public enum State
	{
		None = 0,
		JoinRandomRoomModeType = 1,
		JoinRandomRoomMode = 2
	}

	private List<string> list_0 = new List<string>();

	private Func<int, int, bool> func_0;

	[CompilerGenerated]
	private State state_0;

	[CompilerGenerated]
	private PresetMatchMakingData presetMatchMakingData_0;

	[CompilerGenerated]
	private static Func<KeyValuePair<int, RoomItemData>, int> func_1;

	[CompilerGenerated]
	private static Comparison<RoomInfo> comparison_0;

	public State State_0
	{
		[CompilerGenerated]
		get
		{
			return state_0;
		}
		[CompilerGenerated]
		private set
		{
			state_0 = value;
		}
	}

	public PresetMatchMakingData PresetMatchMakingData_0
	{
		[CompilerGenerated]
		get
		{
			return presetMatchMakingData_0;
		}
		[CompilerGenerated]
		private set
		{
			presetMatchMakingData_0 = value;
		}
	}

	public void GetPressetDataFromRating()
	{
		int userTotalStuffRating = UserController.UserController_0.GetUserTotalStuffRating();
		foreach (PresetMatchMakingData presetsDatum in PresetMatchMahingDataStorage.Get.GetPresetsData())
		{
			if (presetsDatum.CheckRating(userTotalStuffRating))
			{
				PresetMatchMakingData_0 = presetsDatum;
				break;
			}
		}
	}

	public int GetRatingFromBasePresset()
	{
		return PresetMatchMakingData_0.int_1;
	}

	public int GetNextPressetFromCustomRating(PresetMatchMakingData presetMatchMakingData_1, out PresetMatchMakingData presetMatchMakingData_2)
	{
		if (PresetMatchMakingData_0 != null)
		{
			int int_ = ((PresetMatchMakingData_0.int_3 != 1) ? (presetMatchMakingData_1.int_1 - 1) : (presetMatchMakingData_1.int_2 + 1));
			foreach (PresetMatchMakingData presetsDatum in PresetMatchMahingDataStorage.Get.GetPresetsData())
			{
				if (presetsDatum.CheckRating(int_))
				{
					presetMatchMakingData_2 = presetsDatum;
					return presetsDatum.int_1;
				}
			}
		}
		presetMatchMakingData_2 = null;
		return -1;
	}

	public ModeData GetRandomModeForModeType(ModeType modeType_0, Func<int, int, bool> func_2 = null)
	{
		State_0 = State.JoinRandomRoomModeType;
		func_0 = func_0 ?? func_2;
		List<RoomItemData> list = new List<RoomItemData>();
		MonoSingleton<FightController>.Prop_0.FightRoomsController_0.GetRoomItemDataList(list, false, (RoomItemData roomItemData_0) => roomItemData_0.modeData_0.ModeType_0 == modeType_0 && roomItemData_0.int_3 > 0 && roomItemData_0.int_3 < roomItemData_0.int_2 && roomItemData_0.bool_1 && list_0.IndexOf(roomItemData_0.string_1) == -1 && ((func_0 != null && func_0(roomItemData_0.int_3, roomItemData_0.int_2)) || func_0 == null));
		Dictionary<int, RoomItemData> dictionary = new Dictionary<int, RoomItemData>();
		for (int i = 0; i < list.Count; i++)
		{
			RoomItemData roomItemData = list[i];
			if (roomItemData.modeData_0.ModeType_0 == modeType_0)
			{
				RoomItemData value = null;
				if (!dictionary.TryGetValue(roomItemData.modeData_0.Int32_0, out value))
				{
					dictionary.Add(roomItemData.modeData_0.Int32_0, roomItemData);
				}
				else if (roomItemData.int_3 > value.int_3)
				{
					dictionary[roomItemData.modeData_0.Int32_0] = roomItemData;
				}
			}
		}
		if (dictionary.Count == 0)
		{
			List<ModeData> modesByType = ModesController.ModesController_0.GetModesByType(modeType_0);
			return modesByType[UnityEngine.Random.Range(0, modesByType.Count)];
		}
		IOrderedEnumerable<KeyValuePair<int, RoomItemData>> orderedEnumerable = dictionary.OrderByDescending((KeyValuePair<int, RoomItemData> keyValuePair_0) => keyValuePair_0.Value.int_3);
		Log.AddLine("++++++++++++++++++GetRandomModeForModeType candidates+++++++++++++++++++++++++++");
		foreach (KeyValuePair<int, RoomItemData> item in orderedEnumerable)
		{
			Log.AddLine(string.Format("room name ={0} \n players {1}/{2}", item.Value.string_1, item.Value.int_3, item.Value.int_2));
		}
		Log.AddLine("---------------------------------------------------------------");
		RoomItemData value2 = orderedEnumerable.First().Value;
		return value2.modeData_0;
	}

	public void JoinRandomRoom(Hashtable hashtable_0, Func<int, int, bool> func_2 = null)
	{
		if (hashtable_0 == null)
		{
			Log.AddLine("FightMatchMakingController::JoinRandomRoom. roomProperties == null");
			return;
		}
		func_0 = func_0 ?? func_2;
		List<RoomInfo> list = new List<RoomInfo>(MonoSingleton<FightController>.Prop_0.FightRoomsController_0.RoomInfo_0);
		List<RoomInfo> list2 = new List<RoomInfo>();
		for (int i = 0; i < list.Count; i++)
		{
			RoomInfo roomInfo = list[i];
			bool flag = FilterRoom(hashtable_0, roomInfo);
			bool flag2 = func_0 == null || func_0(roomInfo.Int32_0, roomInfo.Byte_0);
			bool flag3 = list_0.IndexOf(roomInfo.String_0) != -1;
			if (flag && flag2 && !flag3)
			{
				list2.Add(roomInfo);
			}
		}
		State_0 = ((State_0 == State.None) ? State.JoinRandomRoomMode : State_0);
		if (list2.Count == 0)
		{
			MonoSingleton<FightController>.Prop_0.CreateRoom(MonoSingleton<FightController>.Prop_0.ModeData_0, 0, 0, string.Empty, string.Empty);
			return;
		}
		list2.Sort((RoomInfo roomInfo_0, RoomInfo roomInfo_1) => roomInfo_1.Int32_0.CompareTo(roomInfo_0.Int32_0));
		RoomInfo roomInfo2 = list2[0];
		list_0.Add(roomInfo2.String_0);
		MonoSingleton<FightController>.Prop_0.JoinRoom(roomInfo2.String_0);
	}

	public void OnJoinedRoom()
	{
		func_0 = null;
		list_0.Clear();
		State_0 = State.None;
	}

	private bool FilterRoom(Hashtable hashtable_0, RoomInfo roomInfo_0)
	{
		if (roomInfo_0.Boolean_0)
		{
			return false;
		}
		if (roomInfo_0.Boolean_2 && roomInfo_0.Boolean_3)
		{
			if (roomInfo_0.Byte_0 <= 0 && roomInfo_0.Byte_0 <= roomInfo_0.Int32_0)
			{
				return false;
			}
			int customProperties = hashtable_0.GetCustomProperties("Mode", int.TryParse, -1);
			if (customProperties == -1)
			{
				return false;
			}
			int customProperties2 = roomInfo_0.Hashtable_0.GetCustomProperties("Mode", int.TryParse, -1);
			if (customProperties != customProperties2)
			{
				return false;
			}
			string value = roomInfo_0.Hashtable_0["Pass"] as string;
			if (!string.IsNullOrEmpty(value))
			{
				return false;
			}
			if (!roomInfo_0.Hashtable_0.GetCustomProperties("IsRanked", bool.TryParse, false))
			{
				return false;
			}
			int customProperties3 = hashtable_0.GetCustomProperties("PressetRating", int.TryParse, -1);
			if (customProperties3 != -1)
			{
				int customProperties4 = roomInfo_0.Hashtable_0.GetCustomProperties("PressetRating", int.TryParse, -1);
				if (customProperties3 != customProperties4)
				{
					return false;
				}
			}
			int customProperties5 = roomInfo_0.Hashtable_0.GetCustomProperties("RoundTime", int.TryParse, -1);
			if (customProperties5 == -1)
			{
				return false;
			}
			double customProperties6 = roomInfo_0.Hashtable_0.GetCustomProperties("TimeMatchStart", double.TryParse, -1.0);
			if (customProperties6 == -1.0)
			{
				return false;
			}
			if (Utility.Double_0 >= customProperties6 + (double)(customProperties5 * 60) - (double)MatchMakingSettings.Get.TimeBlockToEnterBattle)
			{
				return false;
			}
			return true;
		}
		return false;
	}
}
