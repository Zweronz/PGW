using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using engine.helpers;
using engine.unity;
using UnityEngine;

public sealed class ModesController
{
	private static ModesController modesController_0;

	private List<ModeData> list_0 = new List<ModeData>(100);

	[CompilerGenerated]
	private static Func<KeyValuePair<int, ModeRewardData>, ModeRewardData> func_0;

	[CompilerGenerated]
	private static Func<ModeRewardData, int> func_1;

	public static ModesController ModesController_0
	{
		get
		{
			if (modesController_0 == null)
			{
				modesController_0 = new ModesController();
			}
			return modesController_0;
		}
	}

	public bool IsModeValid(ModeData modeData_0)
	{
		User user_ = UserController.UserController_0.UserData_0.user_0;
		return modeData_0.Boolean_1 && (!modeData_0.Boolean_3 || user_.bool_0) && !modeData_0.Boolean_4;
	}

	public ModeData GetMode(int int_0)
	{
		return ModeStorage.Get.Storage.GetObjectByKey(int_0);
	}

	private List<ModeData> modeData
	{
		get
		{
			if (_modeData == null)
			{
				List<ModeData> data = new List<ModeData>();
				Modes modes = Resources.Load<Modes>("Modes");

				foreach (Modes.Mode mode in modes.modes)
				{
					data.Add(mode.ToModeData());
				}

				_modeData = data;
			}

			return _modeData;
		}
	}

	private List<ModeData> _modeData;

	public List<ModeData> GetModesByType(ModeType modeType_0)
	{
		list_0.Clear();
		List<ModeData> list = modeData;//ModeStorage.Get.Storage.Search(0, modeType_0);
		for (int i = 0; i < list.Count; i++)
		{
			ModeData modeData = list[i];
			if (IsModeValid(modeData))
			{
				MapData objectByKey = MapStorage.Get.Storage.GetObjectByKey(modeData.Int32_1);
				if (objectByKey != null && objectByKey.Boolean_0 && !objectByKey.Boolean_3)
				{
					list_0.Add(modeData);
				}
			}
		}
		return list_0;
	}

	public int GetMinTimeLimit(int int_0)
	{
		ModeData mode = GetMode(int_0);
		if (mode.Int32_2 > 0)
		{
			return mode.Int32_2;
		}
		ModeTypeData objectByKey = ModeTypeStorage.Get.Storage.GetObjectByKey(mode.ModeType_0);
		return objectByKey.Int32_0;
	}

	public ModeRewardData GetRewardAfterMatch(int int_0, int int_1, EndFightNetworkCommand.IsWinState isWinState_0, bool bool_0, int int_2)
	{
		if (bool_0)
		{
			MonoSingleton<FightController>.Prop_0.FinishFight(int_0, isWinState_0, int_1, false, int_2);
			MonoSingleton<FightController>.Prop_0.OnFinishFight();
		}
		ModeData modeData_ = MonoSingleton<FightController>.Prop_0.ModeData_0;
		if (int_1 < modeData_.Int32_4)
		{
			return null;
		}
		List<ModeRewardData> list = (from keyValuePair_0 in modeData_.Dictionary_0
			select keyValuePair_0.Value into modeRewardData_0
			where modeRewardData_0.Int32_0 == int_0
			orderby modeRewardData_0.int_0
			select modeRewardData_0).ToList();
		if (list.Count == 0)
		{
			Log.AddLine(string.Format("[ModesController::GetRewardAfterMatch] ERROR rewards.Count == 0"), Log.LogLevel.ERROR);
			return null;
		}
		int int32_ = MonoSingleton<FightController>.Prop_0.Int32_0;
		ModeRewardData result = list[0];
		bool flag = false;
		for (int i = 0; i < list.Count; i++)
		{
			ModeRewardData modeRewardData = list[i];
			if (int32_ >= modeRewardData.int_0)
			{
				result = modeRewardData;
				flag = true;
			}
		}
		if (!flag)
		{
			Log.AddLineWarning("[ModesController::GetRewardAfterMatch] WARNING rewards not found");
		}
		return result;
	}

	public ModeRewardData GetRewardAfterArena(int int_0, int int_1, int int_2)
	{
		ModeData modeData_ = MonoSingleton<FightController>.Prop_0.ModeData_0;
		if (modeData_ == null)
		{
			return null;
		}
		if (modeData_.Dictionary_0 != null && modeData_.Dictionary_0.Count != 0)
		{
			ModeRewardData modeRewardData = null;
			foreach (KeyValuePair<int, ModeRewardData> item in modeData_.Dictionary_0)
			{
				ModeRewardData value = item.Value;
				if (value.Int32_0 <= int_0)
				{
					if (value.Int32_0 == int_0)
					{
						modeRewardData = value;
						break;
					}
					if (modeRewardData == null || value.Int32_0 > modeRewardData.Int32_0)
					{
						modeRewardData = value;
					}
				}
			}
			MonoSingleton<FightController>.Prop_0.FinishFight((modeRewardData != null) ? modeRewardData.Int32_0 : 0, EndFightNetworkCommand.IsWinState.Win, int_0);
			return modeRewardData;
		}
		return null;
	}
}
