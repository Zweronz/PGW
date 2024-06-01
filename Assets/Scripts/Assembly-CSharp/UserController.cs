using System;
using System.Collections.Generic;
using UnityEngine;
using engine.helpers;
using engine.network;

public sealed class UserController
{
	private static UserController userController_0;

	private List<UserArtikul> list_0 = new List<UserArtikul>();

	public static UserController UserController_0
	{
		get
		{
			if (userController_0 == null)
			{
				userController_0 = new UserController();
			}
			return userController_0;
		}
	}

	public UserData UserData_0
	{
		get
		{
			return UsersData.UsersData_0.UserData_0;
		}
	}

	public UserData GetUser(int int_0)
	{
		if (int_0 == UsersData.UsersData_0.UserData_0.user_0.int_0)
		{
			return UserData.LocalUserData;
		}
		return UsersData.UsersData_0.usersStorage_0.SearchUnique(0, int_0);
	}

	public UserArtikul GetUserArtikulById(string string_0)
	{
		return UserData_0.userArtikulStorage_0.GetObjectByKey(string_0);
	}

	private static Dictionary<int, UserArtikul> userArtikuls = new Dictionary<int, UserArtikul>
	{
		{1, new UserArtikul { int_0 = 1, int_1 = 1 }},
		{2, new UserArtikul { int_0 = 100, int_1 = 1 }}
	};

	public UserArtikul GetUserArtikulByArtikulId(int int_0)
	{
		UserArtikul artikul;
		userArtikuls.TryGetValue(int_0, out artikul);
		return artikul;
	}

	public List<UserArtikul> GetUserArtikulsBySlotType(SlotType slotType_0)
	{
		return FilterUserArticulByAmount(UserData_0.userArtikulStorage_0.Search(1, slotType_0));
	}

	public int GetUserArtikulCount(int int_0)
	{
		UserArtikul userArtikulByArtikulId = GetUserArtikulByArtikulId(int_0);
		return (userArtikulByArtikulId != null) ? userArtikulByArtikulId.int_1 : 0;
	}

	public void GetUserSkillsList(ref List<SkillData> list_1)
	{
		if (list_1 == null)
		{
			list_1 = new List<SkillData>();
		}
		list_1.AddRange(UsersData.UsersData_0.UserData_0.localUserData_0.Dictionary_1.Values);
	}

	public int GetMoneyByType(MoneyType moneyType_0)
	{
		User user_ = UsersData.UsersData_0.UserData_0.user_0;
		int value = 0;
		if (user_.dictionary_0 != null)
		{
			user_.dictionary_0.TryGetValue(moneyType_0, out value);
		}
		return value;
	}

	public int GetUserExp()
	{
		return UsersData.UsersData_0.UserData_0.user_0.int_1;
	}

	public int GetUserMinExpForCurrentLevel()
	{
		return LevelStorage.Get.GetMinExpForLevel(GetUserLevel());
	}

	public int GetUserMaxExpForCurrentLevel()
	{
		return LevelStorage.Get.GetMaxExpForLevel(GetUserLevel());
	}

	public bool IsLevelMax()
	{
		int userLevel = GetUserLevel();
		return userLevel >= LevelStorage.Get.LevelMax;
	}

	public int CheckShowLevelUpWindow(int int_0)
	{
		if (IsLevelMax())
		{
			return 0;
		}
		int num = GetUserExp() + int_0;
		int userMaxExpForCurrentLevel = GetUserMaxExpForCurrentLevel();
		return (num >= userMaxExpForCurrentLevel) ? (GetUserLevel() + 1) : 0;
	}

	public int GetUserLevel()
	{
		return UsersData.UsersData_0.UserData_0.user_0.int_2;
	}

	public LevelData GetUserLeveData()
	{
		return LevelStorage.Get.GetlLevelData(GetUserLevel());
	}

	public int GetUserTier()
	{
		return LevelStorage.Get.GetTier(GetUserLevel());
	}

	public int GetArtikulIdFromSlot(SlotType slotType_0)
	{
		Dictionary<SlotType, int> dictionary = null;
		dictionary = UsersData.UsersData_0.UserData_0.dictionary_3;
		if (dictionary.ContainsKey(slotType_0))
		{
			return dictionary[slotType_0];
		}
		return 0;
	}

	public ArtikulData GetArtikulDataFromSlot(SlotType slotType_0)
	{
		int artikulIdFromSlot = GetArtikulIdFromSlot(slotType_0);
		if (artikulIdFromSlot == 0)
		{
			return null;
		}
		return ArtikulController.ArtikulController_0.GetArtikul(artikulIdFromSlot);
	}

	public UserArtikul GetUserArtikulDataFromSlot(SlotType slotType_0)
	{
		int int_ = 0;
		return GetUserArtikulDataFromSlot(slotType_0, out int_);
	}

	public UserArtikul GetUserArtikulDataFromSlot(SlotType slotType_0, out int int_0)
	{
		int_0 = GetArtikulIdFromSlot(slotType_0);
		if (int_0 == 0)
		{
			return null;
		}
		return GetUserArtikulByArtikulId(int_0);
	}

	public bool HasUserArtikul(int int_0)
	{
		UserArtikul userArtikulByArtikulId = GetUserArtikulByArtikulId(int_0);
		return userArtikulByArtikulId != null && userArtikulByArtikulId.int_1 > 0;
	}

	public bool HasAnyUpgrade(int int_0)
	{
		List<ArtikulData> upgrades = ArtikulController.ArtikulController_0.GetUpgrades(int_0);
		foreach (ArtikulData item in upgrades)
		{
			if (HasUserArtikul(item.Int32_0))
			{
				return true;
			}
		}
		return false;
	}

	public void LogSlots()
	{
		foreach (int value in Enum.GetValues(typeof(SlotType)))
		{
			if (value != 0)
			{
				ArtikulData artikulDataFromSlot = GetArtikulDataFromSlot((SlotType)value);
				string string_ = ((artikulDataFromSlot == null) ? string.Format("slot {0} artikul = NULL", ((SlotType)value).ToString()) : string.Format("slot {0} artikul = {1} ({2})", ((SlotType)value).ToString(), artikulDataFromSlot.String_4, artikulDataFromSlot.Int32_0));
				Log.AddLine(string_);
			}
		}
	}

	public int GetUserTotalStuffRating()
	{
		int num = 0;
		UserArtikul userArtikul = null;
		for (int i = 1; i < 11; i++)
		{
			userArtikul = GetUserArtikulDataFromSlot((SlotType)i);
			if (userArtikul != null)
			{
				num += userArtikul.ArtikulData_0.Int32_4;
			}
		}
		return num;
	}

	public UserArtikul GetAnyUserArtikulById(string string_0, int int_0)
	{
		UserData user = GetUser(int_0);
		if (user == null)
		{
			return null;
		}
		return user.userArtikulStorage_0.GetObjectByKey(string_0);
	}

	public UserArtikul GetAnyUserArtikulByArtikulId(int int_0, int int_1)
	{
		UserData user = GetUser(int_1);
		if (user == null)
		{
			return null;
		}
		return user.userArtikulStorage_0.SearchUnique(0, int_0);
	}

	public UserArtikul GetAnyUserArtikulDataFromSlot(SlotType slotType_0, out int int_0, int int_1)
	{
		UserData user = GetUser(int_1);
		if (user == null)
		{
			int_0 = 0;
			return null;
		}
		int_0 = GetAnyUserArtikulIdFromSlot(slotType_0, int_1);
		if (int_0 == 0)
		{
			return null;
		}
		return GetAnyUserArtikulByArtikulId(int_0, int_1);
	}

	public int GetAnyUserArtikulIdFromSlot(SlotType slotType_0, int int_0)
	{
		UserData user = GetUser(int_0);
		if (user == null)
		{
			return 0;
		}
		Dictionary<SlotType, int> dictionary_ = user.dictionary_3;
		if (dictionary_.ContainsKey(slotType_0))
		{
			return dictionary_[slotType_0];
		}
		return 0;
	}

	public ArtikulData GetAnyUserArtikulDataFromSlot(SlotType slotType_0, int int_0)
	{
		int anyUserArtikulIdFromSlot = GetAnyUserArtikulIdFromSlot(slotType_0, int_0);
		if (anyUserArtikulIdFromSlot == 0)
		{
			return null;
		}
		return ArtikulController.ArtikulController_0.GetArtikul(anyUserArtikulIdFromSlot);
	}

	public List<UserArtikul> GetAnyUserArtikulsBySlotType(SlotType slotType_0, int int_0)
	{
		UserData user = GetUser(int_0);
		if (user == null)
		{
			return new List<UserArtikul>();
		}
		return FilterUserArticulByAmount(user.userArtikulStorage_0.Search(1, slotType_0));
	}

	private List<UserArtikul> FilterUserArticulByAmount(List<UserArtikul> list_1)
	{
		list_0.Clear();
		foreach (UserArtikul item in list_1)
		{
			if (item.int_1 > 0)
			{
				list_0.Add(item);
			}
		}
		return list_0;
	}

	public SkillData GetUserSkill(SkillId skillId_0)
	{
		SkillData value = null;
		UsersData.UsersData_0.UserData_0.localUserData_0.Dictionary_1.TryGetValue(skillId_0, out value);
		return value;
	}

	public float GetFloatMultModifier(SkillId skillId_0)
	{
		SkillData userSkill = GetUserSkill(skillId_0);
		if (userSkill == null)
		{
			return 1f;
		}
		return userSkill.Single_1;
	}

	public float GetFloatSummModifier(SkillId skillId_0)
	{
		SkillData userSkill = GetUserSkill(skillId_0);
		if (userSkill == null)
		{
			return 0f;
		}
		return userSkill.Single_0;
	}

	public int GetIntMultModifier(SkillId skillId_0)
	{
		SkillData userSkill = GetUserSkill(skillId_0);
		if (userSkill == null)
		{
			return 1;
		}
		return userSkill.Int32_1;
	}

	public int GetIntSummModifier(SkillId skillId_0)
	{
		SkillData userSkill = GetUserSkill(skillId_0);
		if (userSkill == null)
		{
			return 0;
		}
		return userSkill.Int32_0;
	}

	public float GetAmmoModifierBySlotType(SlotType slotType_0)
	{
		SkillId skillIdAmmoModifierBySlotType = UsersData.UsersData_0.UserData_0.localUserData_0.GetSkillIdAmmoModifierBySlotType(slotType_0);
		return (skillIdAmmoModifierBySlotType != 0) ? (1f + GetFloatSummModifier(skillIdAmmoModifierBySlotType)) : 1f;
	}

	public float GetSpeedModifier()
	{
		float floatSummModifier = UserController_0.GetFloatSummModifier(SkillId.SKILL_SPEED_MODIFIER);
		return floatSummModifier * PlayerDotEffectController.Single_0;
	}

	public void EquipArtikul(int int_0)
	{
		EquipArtikulNetworkCommand equipArtikulNetworkCommand = new EquipArtikulNetworkCommand();
		equipArtikulNetworkCommand.int_1 = int_0;
		equipArtikulNetworkCommand.bool_0 = true;
		AbstractNetworkCommand.Send(equipArtikulNetworkCommand);
	}

	public void UnequipArtikul(int int_0)
	{
		try
		{
			WearData objectByKey = WearStorage.Get.Storage.GetObjectByKey(int_0);
			if (objectByKey == null || !objectByKey.Boolean_1)
			{
				EquipArtikulNetworkCommand equipArtikulNetworkCommand = new EquipArtikulNetworkCommand();
				equipArtikulNetworkCommand.int_1 = int_0;
				equipArtikulNetworkCommand.bool_0 = false;
				AbstractNetworkCommand.Send(equipArtikulNetworkCommand);
			}
		}
		catch
		{
			
		}
	}

	public void SetActiveSlot(SlotType slotType_0, bool bool_0)
	{
		Dictionary<SlotType, int> activesSlotType = GetActivesSlotType(slotType_0);
		if (activesSlotType != null)
		{
			if (!activesSlotType.ContainsKey(slotType_0))
			{
				activesSlotType.Add(slotType_0, 0);
			}
			int int_ = 0;
			UserArtikul userArtikulDataFromSlot = UserController_0.GetUserArtikulDataFromSlot(slotType_0, out int_);
			if ((userArtikulDataFromSlot == null || userArtikulDataFromSlot.int_1 <= 0) && int_ == 0)
			{
				bool_0 = false;
			}
			if (!bool_0)
			{
				activesSlotType[slotType_0] = 0;
			}
			else
			{
				activesSlotType[slotType_0] = int_;
			}
			CreateUserSkillCache();
		}
	}

	public bool IsActiveSlot(SlotType slotType_0)
	{
		LocalUserData localUserData_ = UsersData.UsersData_0.UserData_0.localUserData_0;
		bool result = false;
		if (ArtikulData.IsWeapon(slotType_0))
		{
			result = localUserData_.Dictionary_0[LocalUserData.ActiveSlotType.Weapon].ContainsKey(slotType_0) && localUserData_.Dictionary_0[LocalUserData.ActiveSlotType.Weapon][slotType_0] != 0;
		}
		else if (ArtikulData.IsConsumable(slotType_0))
		{
			result = localUserData_.Dictionary_0[LocalUserData.ActiveSlotType.Consume].ContainsKey(slotType_0) && localUserData_.Dictionary_0[LocalUserData.ActiveSlotType.Consume][slotType_0] != 0;
		}
		return result;
	}

	public Dictionary<SlotType, int> GetActivesSlotType(SlotType slotType_0)
	{
		LocalUserData localUserData_ = UsersData.UsersData_0.UserData_0.localUserData_0;
		if (ArtikulData.IsWeapon(slotType_0))
		{
			return localUserData_.Dictionary_0[LocalUserData.ActiveSlotType.Weapon];
		}
		if (ArtikulData.IsConsumable(slotType_0))
		{
			return localUserData_.Dictionary_0[LocalUserData.ActiveSlotType.Consume];
		}
		return null;
	}

	public Dictionary<SlotType, int> GetActivesSlotType(LocalUserData.ActiveSlotType activeSlotType_0)
	{
		Dictionary<SlotType, int> value = null;
		UsersData.UsersData_0.UserData_0.localUserData_0.Dictionary_0.TryGetValue(activeSlotType_0, out value);
		return value;
	}

	public void ClearActiveSlots()
	{
		foreach (KeyValuePair<LocalUserData.ActiveSlotType, Dictionary<SlotType, int>> item in UsersData.UsersData_0.UserData_0.localUserData_0.Dictionary_0)
		{
			item.Value.Clear();
		}
	}

	public void CreateUserSkillCache()
	{
		UserData userData_ = UsersData.UsersData_0.UserData_0;
		Dictionary<SkillId, SkillData> dictionary_ = userData_.localUserData_0.Dictionary_1;
		Dictionary<SkillId, SkillData> dictionary_2 = userData_.localUserData_0.Dictionary_2;
		dictionary_2.Clear();
		dictionary_.ToDictionary(dictionary_2);
		dictionary_.Clear();
		NeedData needData_ = null;
		foreach (KeyValuePair<SkillId, SkillData> item in userData_.dictionary_2)
		{
			if (item.Value.CheckNeeds(needData_))
			{
				dictionary_.Add(item.Key, item.Value);
			}
		}
		List<int> list = userData_.localUserData_0.List_0;
		list.Clear();
		Dictionary<SlotType, int> activesSlotType = GetActivesSlotType(LocalUserData.ActiveSlotType.Weapon);
		foreach (KeyValuePair<SlotType, int> item2 in activesSlotType)
		{
			if (item2.Value != 0)
			{
				list.Add(item2.Value);
			}
		}
		activesSlotType = GetActivesSlotType(LocalUserData.ActiveSlotType.Consume);
		foreach (KeyValuePair<SlotType, int> item3 in activesSlotType)
		{
			if (item3.Value != 0)
			{
				list.Add(item3.Value);
			}
		}
		for (SlotType slotType = SlotType.SLOT_WEAR_HAT; slotType <= SlotType.SLOT_WEAR_BOOTS; slotType++)
		{
			int artikulIdFromSlot = GetArtikulIdFromSlot(slotType);
			if (artikulIdFromSlot != 0)
			{
				list.Add(artikulIdFromSlot);
			}
		}
		ArtikulController.ArtikulController_0.ApplaySkillsForArtikuls(dictionary_, list.ToArray());
		SendEventsChangeSkills(dictionary_, dictionary_2);
	}

	private void SendEventsChangeSkills(Dictionary<SkillId, SkillData> dictionary_0, Dictionary<SkillId, SkillData> dictionary_1)
	{
		LocalUserData.Dispatch(LocalUserData.EventType.SKILLS_UPDATE);
		foreach (SkillId key in dictionary_0.Keys)
		{
			if (!dictionary_1.ContainsKey(key))
			{
				LocalUserData.Dispatch(LocalUserData.EventType.SKILL_ADD, (int)key);
			}
		}
		foreach (SkillId key2 in dictionary_1.Keys)
		{
			if (!dictionary_0.ContainsKey(key2))
			{
				LocalUserData.Dispatch(LocalUserData.EventType.SKILL_REMOVE, (int)key2);
			}
		}
	}

	public double GetTimerForFreeBuy(UserTimerData.UserTimerType userTimerType_0, int int_0)
	{
		if (UserData_0.dictionary_5 != null)
		{
			foreach (UserTimerData item in UserData_0.dictionary_5[userTimerType_0])
			{
				if (item.int_0 == int_0)
				{
					return item.double_0;
				}
			}
		}
		return 0.0;
	}

	public GameObject GetGameObject(int int_0)
	{
		return UserData_0.localShopData_0.GetGameObject(int_0);
	}

	public GameObject GetInnerGameObject(int int_0)
	{
		return UserData_0.localShopData_0.GetInnerGameObject(int_0);
	}
}
