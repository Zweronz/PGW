using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.data;

[StorageDataKey(typeof(int))]
[ProtoContract]
public class ArtikulData
{
	//id??
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private SlotType slotType_0;
	
	//localization key
	[CompilerGenerated]
	private string string_0;

	//description
	[CompilerGenerated]
	private string string_1;

	//upgrade id??
	[CompilerGenerated]
	private int int_1;

	//downgrade id??
	[CompilerGenerated]
	private int int_2;

	//grenade count? just object count? maybe shot melee count?
	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private NeedsData needsData_0;

	[CompilerGenerated]
	private Dictionary<SkillId, SkillData> dictionary_0;

	//icon name
	[CompilerGenerated]
	private string string_2;

	//weapon prefab
	[CompilerGenerated]
	private string string_3;

	//idk something related to cups
	[CompilerGenerated]
	private int int_4;
	
	//price
	[CompilerGenerated]
	private int int_5;

	//idk
	[CompilerGenerated]
	private List<int> list_0;

	//I dont know
	[CompilerGenerated]
	private bool bool_0;

	[ProtoMember(1)]
	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		set
		{
			int_0 = value;
		}
	}

	[ProtoMember(2)]
	public SlotType SlotType_0
	{
		[CompilerGenerated]
		get
		{
			return slotType_0;
		}
		[CompilerGenerated]
		set
		{
			slotType_0 = value;
		}
	}

	[ProtoMember(3)]
	public string String_0
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		set
		{
			string_0 = value;
		}
	}

	[ProtoMember(4)]
	public string String_1
	{
		[CompilerGenerated]
		get
		{
			return string_1;
		}
		[CompilerGenerated]
		set
		{
			string_1 = value;
		}
	}

	[ProtoMember(5)]
	public int Int32_1
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		set
		{
			int_1 = value;
		}
	}

	[ProtoMember(6)]
	public int Int32_2
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		set
		{
			int_2 = value;
		}
	}

	[ProtoMember(7)]
	public int Int32_3
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		set
		{
			int_3 = value;
		}
	}

	[ProtoMember(8)]
	public NeedsData NeedsData_0
	{
		[CompilerGenerated]
		get
		{
			return needsData_0;
		}
		[CompilerGenerated]
		set
		{
			needsData_0 = value;
		}
	}

	[ProtoMember(9)]
	public Dictionary<SkillId, SkillData> Dictionary_0
	{
		[CompilerGenerated]
		get
		{
			return dictionary_0;
		}
		[CompilerGenerated]
		set
		{
			dictionary_0 = value;
		}
	}

	[ProtoMember(10)]
	public string String_2
	{
		[CompilerGenerated]
		get
		{
			return string_2;
		}
		[CompilerGenerated]
		set
		{
			string_2 = value;
		}
	}

	[ProtoMember(11)]
	public string String_3
	{
		[CompilerGenerated]
		get
		{
			return string_3;
		}
		[CompilerGenerated]
		set
		{
			string_3 = value;
		}
	}

	[ProtoMember(12)]
	public int Int32_4
	{
		[CompilerGenerated]
		get
		{
			return int_4;
		}
		[CompilerGenerated]
		set
		{
			int_4 = value;
		}
	}

	[ProtoMember(13)]
	public int Int32_5
	{
		[CompilerGenerated]
		get
		{
			return int_5;
		}
		[CompilerGenerated]
		set
		{
			int_5 = value;
		}
	}

	[ProtoMember(14)]
	public List<int> List_0
	{
		[CompilerGenerated]
		get
		{
			return list_0;
		}
		[CompilerGenerated]
		set
		{
			list_0 = value;
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

	public bool Boolean_1
	{
		get
		{
			return IsWeapon(SlotType_0);
		}
	}

	public bool Boolean_2
	{
		get
		{
			return IsWear(SlotType_0);
		}
	}

	public bool Boolean_3
	{
		get
		{
			return IsConsumable(SlotType_0);
		}
	}

	public string String_4
	{
		get
		{
			return LocalizationStorage.Get.Term(String_0);
		}
	}

	public static bool IsWeapon(SlotType slotType_1)
	{
		return slotType_1 == SlotType.SLOT_WEAPON_PRIMARY || slotType_1 == SlotType.SLOT_WEAPON_BACKUP || slotType_1 == SlotType.SLOT_WEAPON_MELEE || slotType_1 == SlotType.SLOT_WEAPON_SPECIAL || slotType_1 == SlotType.SLOT_WEAPON_PREMIUM || slotType_1 == SlotType.SLOT_WEAPON_SNIPER;
	}

	public static bool IsWear(SlotType slotType_1)
	{
		return slotType_1 == SlotType.SLOT_WEAR_ARMOR || slotType_1 == SlotType.SLOT_WEAR_BOOTS || slotType_1 == SlotType.SLOT_WEAR_CAPE || slotType_1 == SlotType.SLOT_WEAR_HAT || slotType_1 == SlotType.SLOT_WEAR_SKIN;
	}

	public static bool IsConsumable(SlotType slotType_1)
	{
		return slotType_1 == SlotType.SLOT_CONSUM_JETPACK || slotType_1 == SlotType.SLOT_CONSUM_MECH || slotType_1 == SlotType.SLOT_CONSUM_POTION || slotType_1 == SlotType.SLOT_CONSUM_TURRET || slotType_1 == SlotType.SLOT_CONSUM_GRENADE;
	}
}
