using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using engine.controllers;
using engine.events;
using engine.unity;

public sealed class ConsumablesController
{
	public enum UseResultType
	{
		SUCCESS = 0,
		SOME_CONS_ALREADY_USES = 1,
		IN_COOLDOWN = 2,
		THIS_CONS_ALREADY_USES = 3,
		OTHER = 4
	}

	public enum EventType
	{
		CONS_ACTIVATE = 0,
		CONS_DEACTIVATE = 1
	}

	private static ConsumablesController consumablesController_0 = null;

	private static readonly BaseEvent<SlotType> baseEvent_0 = new BaseEvent<SlotType>();

	private Dictionary<int, int> dictionary_0 = new Dictionary<int, int>();

	private Dictionary<SlotType, int> dictionary_1 = new Dictionary<SlotType, int>();

	private List<int> list_0 = new List<int>(20);

	private List<SlotType> list_1 = new List<SlotType>();

	public static ConsumablesController ConsumablesController_0
	{
		get
		{
			if (consumablesController_0 == null)
			{
				consumablesController_0 = new ConsumablesController();
			}
			return consumablesController_0;
		}
	}

	private ConsumablesController()
	{
		DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(UpdateActive);
		DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(UpdateCooldown);
		AppStateController.AppStateController_0.Subscribe(OnLeaveRoom, AppStateController.States.MAIN_MENU);
		AppStateController.AppStateController_0.Subscribe(OnLeaveRoom, AppStateController.States.IN_BATTLE_OVER_WINDOW);
	}

	public ConsumableData GetConsumable(int int_0)
	{
		return ConsumablesStorage.Get.Storage.GetObjectByKey(int_0);
	}

	public ConsumableData GetConsumableByPrefabName(string string_0)
	{
		ArtikulData artikulByPrefabName = ArtikulController.ArtikulController_0.GetArtikulByPrefabName(string_0);
		return (artikulByPrefabName != null) ? GetConsumable(artikulByPrefabName.Int32_0) : null;
	}

	public int GetConsumableCountForSlot(SlotType slotType_0)
	{
		if (!ArtikulData.IsConsumable(slotType_0))
		{
			return 0;
		}
		ArtikulData artikulDataFromSlot = UserController.UserController_0.GetArtikulDataFromSlot(slotType_0);
		if (artikulDataFromSlot == null)
		{
			return 0;
		}
		return UserController.UserController_0.GetUserArtikulCount(artikulDataFromSlot.Int32_0);
	}

	public int GetConsumableMaxCountForSlot(SlotType slotType_0)
	{
		if (!ArtikulData.IsConsumable(slotType_0))
		{
			return 0;
		}
		ArtikulData artikulDataFromSlot = UserController.UserController_0.GetArtikulDataFromSlot(slotType_0);
		return (artikulDataFromSlot != null) ? artikulDataFromSlot.Int32_3 : 0;
	}

	public bool IsConsumableMaxCountForSlot(SlotType slotType_0)
	{
		int consumableCountForSlot = GetConsumableCountForSlot(slotType_0);
		int consumableMaxCountForSlot = GetConsumableMaxCountForSlot(slotType_0);
		return consumableCountForSlot >= consumableMaxCountForSlot;
	}

	public bool IsSlotAnyTypeMayBeUsed(SlotType slotType_0)
	{
		int artikulIdFromSlot = UserController.UserController_0.GetArtikulIdFromSlot(slotType_0);
		if (artikulIdFromSlot == 0)
		{
			return false;
		}
		if (UserController.UserController_0.GetUserArtikulCount(artikulIdFromSlot) <= 0)
		{
			return false;
		}
		return true;
	}

	public UseResultType IsSlotDurationTypeMayBeUsed(SlotType slotType_0)
	{
		if (!IsSlotAnyTypeMayBeUsed(slotType_0))
		{
			return UseResultType.OTHER;
		}
		int artikulIdFromSlot = UserController.UserController_0.GetArtikulIdFromSlot(slotType_0);
		ConsumableData consumable = ConsumablesController_0.GetConsumable(artikulIdFromSlot);
		if (consumable == null)
		{
			return UseResultType.OTHER;
		}
		if (consumable.Int32_2 <= 0)
		{
			return UseResultType.OTHER;
		}
		if (dictionary_0.ContainsKey(artikulIdFromSlot))
		{
			return UseResultType.THIS_CONS_ALREADY_USES;
		}
		if (dictionary_1.ContainsKey(consumable.ArtikulData_0.SlotType_0))
		{
			return UseResultType.IN_COOLDOWN;
		}
		if (dictionary_0.Count > 0)
		{
			return UseResultType.SOME_CONS_ALREADY_USES;
		}
		return UseResultType.SUCCESS;
	}

	public UseResultType UseDurationConsumableSlot(SlotType slotType_0)
	{
		UseResultType useResultType = IsSlotDurationTypeMayBeUsed(slotType_0);
		if (useResultType != 0)
		{
			return useResultType;
		}
		int artikulIdFromSlot = UserController.UserController_0.GetArtikulIdFromSlot(slotType_0);
		ConsumableData consumable = ConsumablesController_0.GetConsumable(artikulIdFromSlot);
		dictionary_0.Add(consumable.Int32_0, consumable.Int32_2);
		UserController.UserController_0.SetActiveSlot(slotType_0, true);
		AddingTimeBuffForConsumable(consumable.Int32_0);
		MonoSingleton<FightController>.Prop_0.FightStatController_0.OnUseConsumable(UsersData.UsersData_0.UserData_0.user_0.int_0, slotType_0);
		Dispatch(EventType.CONS_ACTIVATE, slotType_0);
		return UseResultType.SUCCESS;
	}

	public int GetTimeForSlot(SlotType slotType_0)
	{
		foreach (int key in dictionary_0.Keys)
		{
			ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(key);
			if (artikul != null && artikul.SlotType_0 == slotType_0)
			{
				return dictionary_0[key];
			}
		}
		return 0;
	}

	public float GetCooldownForSlot(SlotType slotType_0)
	{
		if (dictionary_1.ContainsKey(slotType_0))
		{
			return dictionary_1[slotType_0];
		}
		return 0f;
	}

	public void ForceStopDurationConsumable(SlotType slotType_0)
	{
		List<int> list = dictionary_0.Keys.ToList();
		int num = 0;
		int num2 = 0;
		ArtikulData artikul;
		while (true)
		{
			if (num2 < list.Count)
			{
				num = list[num2];
				artikul = ArtikulController.ArtikulController_0.GetArtikul(num);
				if (artikul != null && artikul.SlotType_0 == slotType_0)
				{
					break;
				}
				num2++;
				continue;
			}
			return;
		}
		dictionary_0.Remove(num);
		UserController.UserController_0.SetActiveSlot(artikul.SlotType_0, false);
		Dispatch(EventType.CONS_DEACTIVATE, artikul.SlotType_0);
		ConsumableData consumable = GetConsumable(num);
		if (consumable != null && consumable.Single_3 > 0f)
		{
			if (dictionary_1.ContainsKey(artikul.SlotType_0))
			{
				dictionary_1[artikul.SlotType_0] = Mathf.RoundToInt(consumable.Single_3);
			}
			else
			{
				dictionary_1.Add(artikul.SlotType_0, Mathf.RoundToInt(consumable.Single_3));
			}
		}
	}

	public static void Dispatch(EventType eventType_0, SlotType slotType_0)
	{
		baseEvent_0.Dispatch(slotType_0, eventType_0);
	}

	public static void Subscribe(EventType eventType_0, Action<SlotType> action_0)
	{
		if (!baseEvent_0.Contains(action_0, eventType_0))
		{
			baseEvent_0.Subscribe(action_0, eventType_0);
		}
	}

	public static void Unsubscribe(EventType eventType_0, Action<SlotType> action_0)
	{
		if (baseEvent_0.Contains(action_0, eventType_0))
		{
			baseEvent_0.Unsubscribe(action_0, eventType_0);
		}
	}

	private void AddingTimeBuffForConsumable(int int_0)
	{
		int value = 0;
		if (!dictionary_0.TryGetValue(int_0, out value))
		{
			return;
		}
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(int_0);
		if (artikul != null)
		{
			int num = 0;
			switch (artikul.SlotType_0)
			{
			case SlotType.SLOT_CONSUM_POTION:
				num = UserController.UserController_0.GetIntSummModifier(SkillId.SKILL_CONSUMABLE_ADDING_TIME_POTION);
				break;
			case SlotType.SLOT_CONSUM_JETPACK:
				num = UserController.UserController_0.GetIntSummModifier(SkillId.SKILL_CONSUMABLE_ADDING_TIME_JETPACK);
				break;
			case SlotType.SLOT_CONSUM_MECH:
				num = UserController.UserController_0.GetIntSummModifier(SkillId.SKILL_CONSUMABLE_ADDING_TIME_MECH);
				break;
			case SlotType.SLOT_CONSUM_TURRET:
				num = UserController.UserController_0.GetIntSummModifier(SkillId.SKILL_CONSUMABLE_ADDING_TIME_TURRET);
				break;
			}
			if (num != 0)
			{
				dictionary_0[int_0] = value + num;
			}
		}
	}

	private static void UpdateActive()
	{
		ConsumablesController consumablesController = ConsumablesController_0;
		if (consumablesController.dictionary_0.Count == 0)
		{
			return;
		}
		List<int> list = consumablesController.list_0;
		list.Clear();
		int num = 0;
		Dictionary<int, int> dictionary = consumablesController.dictionary_0;
		List<int> list2 = dictionary.Keys.ToList();
		for (int i = 0; i < list2.Count; i++)
		{
			num = list2[i];
			Dictionary<int, int> dictionary2;
			Dictionary<int, int> dictionary3 = (dictionary2 = dictionary);
			int key;
			int key2 = (key = num);
			key = dictionary2[key];
			dictionary3[key2] = key - 1;
			if (dictionary[num] <= 0)
			{
				list.Add(num);
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			num = list[j];
			dictionary.Remove(num);
			ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(num);
			if (artikul == null)
			{
				continue;
			}
			UserController.UserController_0.SetActiveSlot(artikul.SlotType_0, false);
			Dispatch(EventType.CONS_DEACTIVATE, artikul.SlotType_0);
			ConsumableData consumable = consumablesController.GetConsumable(num);
			if (consumable != null && consumable.Single_3 > 0f)
			{
				if (consumablesController.dictionary_1.ContainsKey(artikul.SlotType_0))
				{
					consumablesController.dictionary_1[artikul.SlotType_0] = Mathf.RoundToInt(consumable.Single_3);
				}
				else
				{
					consumablesController.dictionary_1.Add(artikul.SlotType_0, Mathf.RoundToInt(consumable.Single_3));
				}
			}
		}
	}

	private static void UpdateCooldown()
	{
		ConsumablesController consumablesController = ConsumablesController_0;
		if (consumablesController.dictionary_1.Count == 0)
		{
			return;
		}
		List<SlotType> list = consumablesController.list_1;
		list.Clear();
		SlotType slotType = SlotType.SLOT_NONE;
		Dictionary<SlotType, int> dictionary = consumablesController.dictionary_1;
		List<SlotType> list2 = dictionary.Keys.ToList();
		for (int i = 0; i < list2.Count; i++)
		{
			slotType = list2[i];
			Dictionary<SlotType, int> dictionary2;
			Dictionary<SlotType, int> dictionary3 = (dictionary2 = dictionary);
			SlotType key;
			SlotType key2 = (key = slotType);
			int num = dictionary2[key];
			dictionary3[key2] = num - 1;
			if (dictionary[slotType] <= 0)
			{
				list.Add(slotType);
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			slotType = list[j];
			dictionary.Remove(slotType);
		}
	}

	private void OnLeaveRoom()
	{
		Clear();
	}

	private void Clear()
	{
		dictionary_0.Clear();
		dictionary_1.Clear();
		baseEvent_0.UnsubscribeAll();
	}

	public void OnArenaComplete()
	{
		Clear();
	}
}
