using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CodeStage.AntiCheat.ObscuredTypes;
using engine.controllers;
using engine.events;
using engine.helpers;

public class LocalUserData
{
	public enum ActiveSlotType
	{
		Weapon = 0,
		Consume = 1
	}

	public enum EventType
	{
		SKILLS_UPDATE = 0,
		SKILL_ADD = 1,
		SKILL_REMOVE = 2
	}

	private readonly Dictionary<SlotType, SkillId> dictionary_0 = new Dictionary<SlotType, SkillId>
	{
		{
			SlotType.SLOT_WEAPON_PRIMARY,
			SkillId.SKILL_AMMO_COUNT_MODIFIER_SLOT_PRIMARY
		},
		{
			SlotType.SLOT_WEAPON_BACKUP,
			SkillId.SKILL_AMMO_COUNT_MODIFIER_SLOT_BACKUP
		},
		{
			SlotType.SLOT_WEAPON_SPECIAL,
			SkillId.SKILL_AMMO_COUNT_MODIFIER_SLOT_SPECIAL
		},
		{
			SlotType.SLOT_WEAPON_PREMIUM,
			SkillId.SKILL_AMMO_COUNT_MODIFIER_SLOT_PREMIUM
		},
		{
			SlotType.SLOT_WEAPON_SNIPER,
			SkillId.SKILL_AMMO_COUNT_MODIFIER_SLOT_SNIPER
		}
	};

	private static readonly BaseEvent<int> baseEvent_0 = new BaseEvent<int>();

	private ObscuredInt obscuredInt_0 = 0;

	[CompilerGenerated]
	private Dictionary<ActiveSlotType, Dictionary<SlotType, int>> dictionary_1;

	[CompilerGenerated]
	private Dictionary<SkillId, SkillData> dictionary_2;

	[CompilerGenerated]
	private Dictionary<SkillId, SkillData> dictionary_3;

	[CompilerGenerated]
	private List<int> list_0;

	[CompilerGenerated]
	private List<int> list_1;

	[CompilerGenerated]
	private bool bool_0;

	[CompilerGenerated]
	private ObscuredInt obscuredInt_1;

	[CompilerGenerated]
	private ObscuredInt obscuredInt_2;

	[CompilerGenerated]
	private bool bool_1;

	[CompilerGenerated]
	private bool bool_2;

	public Dictionary<ActiveSlotType, Dictionary<SlotType, int>> Dictionary_0
	{
		[CompilerGenerated]
		get
		{
			return dictionary_1;
		}
		[CompilerGenerated]
		set
		{
			dictionary_1 = value;
		}
	}

	public Dictionary<SkillId, SkillData> Dictionary_1
	{
		[CompilerGenerated]
		get
		{
			return dictionary_2;
		}
		[CompilerGenerated]
		private set
		{
			dictionary_2 = value;
		}
	}

	public Dictionary<SkillId, SkillData> Dictionary_2
	{
		[CompilerGenerated]
		get
		{
			return dictionary_3;
		}
		[CompilerGenerated]
		private set
		{
			dictionary_3 = value;
		}
	}

	public List<int> List_0
	{
		[CompilerGenerated]
		get
		{
			return list_0;
		}
		[CompilerGenerated]
		private set
		{
			list_0 = value;
		}
	}

	public List<int> List_1
	{
		[CompilerGenerated]
		get
		{
			return list_1;
		}
		[CompilerGenerated]
		set
		{
			list_1 = value;
		}
	}

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		set
		{
			bool_0 = value;
		}
	}

	public ObscuredInt ObscuredInt_0
	{
		[CompilerGenerated]
		get
		{
			return obscuredInt_1;
		}
		[CompilerGenerated]
		set
		{
			obscuredInt_1 = value;
		}
	}

	public ObscuredInt ObscuredInt_1
	{
		[CompilerGenerated]
		get
		{
			return obscuredInt_2;
		}
		[CompilerGenerated]
		set
		{
			obscuredInt_2 = value;
		}
	}

	public bool Boolean_1
	{
		[CompilerGenerated]
		get
		{
			return bool_1;
		}
		[CompilerGenerated]
		set
		{
			bool_1 = value;
		}
	}

	public bool Boolean_2
	{
		[CompilerGenerated]
		get
		{
			return bool_2;
		}
		[CompilerGenerated]
		set
		{
			bool_2 = value;
		}
	}

	public int Int32_0
	{
		get
		{
			return obscuredInt_0;
		}
		set
		{
			obscuredInt_0 = value;
			UsersData.Dispatch(UsersData.EventType.OFFLINE_WAVES_OVERCOME_CHANGED, new UsersData.EventData
			{
				int_0 = obscuredInt_0
			});
		}
	}

	private void InitActiveSlots()
	{
		Dictionary_0 = new Dictionary<ActiveSlotType, Dictionary<SlotType, int>>
		{
			{
				ActiveSlotType.Weapon,
				new Dictionary<SlotType, int>()
			},
			{
				ActiveSlotType.Consume,
				new Dictionary<SlotType, int>()
			}
		};
	}

	public SkillId GetSkillIdAmmoModifierBySlotType(SlotType slotType_0)
	{
		SkillId value = SkillId.SKILL_NONE;
		dictionary_0.TryGetValue(slotType_0, out value);
		return value;
	}

	private void InitUserSkillCache()
	{
		Dictionary_1 = new Dictionary<SkillId, SkillData>();
		Dictionary_2 = new Dictionary<SkillId, SkillData>();
		List_0 = new List<int>(100);
		UsersData.Subscribe(UsersData.EventType.INIT_COMPLETE, CreateUserSkillCache);
		UsersData.Subscribe(UsersData.EventType.SKILLS_CHANGED, CreateUserSkillCache);
		UsersData.Subscribe(UsersData.EventType.SLOTS_CHANGED, CreateUserSkillCache);
	}

	private void CreateUserSkillCache(UsersData.EventData eventData_0)
	{
		UserController.UserController_0.CreateUserSkillCache();
	}

	public static void Dispatch(EventType eventType_0, int int_0 = 0)
	{
		baseEvent_0.Dispatch(int_0, eventType_0);
	}

	public static void Subscribe(EventType eventType_0, Action<int> action_0)
	{
		if (!baseEvent_0.Contains(action_0, eventType_0))
		{
			baseEvent_0.Subscribe(action_0, eventType_0);
		}
	}

	public static void Unsubscribe(EventType eventType_0, Action<int> action_0)
	{
		baseEvent_0.Unsubscribe(action_0, eventType_0);
	}

	public void Init()
	{
		InitActiveSlots();
		InitUserSkillCache();
		InitInventory();
	}

	private void InitInventory()
	{
		List_1 = new List<int>();
		AppStateController.AppStateController_0.Subscribe(OnCanShowRebuyWnd, AppStateController.States.MAIN_MENU);
		AppStateController.AppStateController_0.Subscribe(OnCanShowRebuyWnd, AppStateController.States.IN_BATTLE_OVER_WINDOW);
		UsersData.Subscribe(UsersData.EventType.ARTIKUL_CHANGED, OnArticulChange);
		if (!DependSceneEvent<ReloadGameEvent>.Contains(UnsubscribeLocalInventory))
		{
			DependSceneEvent<ReloadGameEvent>.GlobalSubscribe(UnsubscribeLocalInventory);
		}
	}

	private void UnsubscribeLocalInventory()
	{
		UsersData.Unsubscribe(UsersData.EventType.ARTIKUL_CHANGED, OnArticulChange);
	}

	private void OnArticulChange(UsersData.EventData eventData_0)
	{
		if (eventData_0 != null && eventData_0.int_0 > 0 && ArtikulController.ArtikulController_0.GetArtikul(eventData_0.int_0).Int32_5 > 0 && UserController.UserController_0.GetUserArtikulCount(eventData_0.int_0) == 0)
		{
			List_1.Add(eventData_0.int_0);
			if (AppStateController.AppStateController_0.States_0 == AppStateController.States.MAIN_MENU || AppStateController.AppStateController_0.States_0 == AppStateController.States.IN_BATTLE_OVER_WINDOW)
			{
				OnCanShowRebuyWnd();
			}
		}
	}

	private void OnCanShowRebuyWnd()
	{
		foreach (int item in List_1)
		{
			ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(item);
			if (!artikul.List_0.IsEmpty() && artikul.List_0[0] > 0)
			{
				ShopArtikulData shopArtikul = ShopArtikulController.ShopArtikulController_0.GetShopArtikul(artikul.List_0[0]);
				bool flag = UserController.UserController_0.GetUserArtikulByArtikulId(shopArtikul.GetArtikul().Int32_0) != null && UserController.UserController_0.GetUserArtikulByArtikulId(shopArtikul.GetArtikul().Int32_0).int_1 > 0;
				if (artikul != null && artikul.List_0 != null && !flag)
				{
					RebuyArticulWindow.Show(new RebuyArticulWindowParams(shopArtikul.Int32_0, RebuyArticulWindowParams.RebuyWndType.REBUY_WND));
				}
			}
			else
			{
				RebuyArticulWindow.Show(new RebuyArticulWindowParams(artikul.Int32_0, RebuyArticulWindowParams.RebuyWndType.COMMON));
			}
		}
		List_1.Clear();
	}

	public void ClearOfflineFightData()
	{
		Boolean_0 = false;
		ObscuredInt_0 = 0;
		Int32_0 = 1;
		ObscuredInt_1 = 0;
		Boolean_1 = false;
		Boolean_2 = false;
	}
}
