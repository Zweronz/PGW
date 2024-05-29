using System.Runtime.CompilerServices;
using ProtoBuf;

[ProtoContract]
public sealed class NeedData
{
	[CompilerGenerated]
	private NeedTypes needTypes_0;

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private double double_0;

	[CompilerGenerated]
	private double double_1;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private SlotType slotType_0;

	[CompilerGenerated]
	private double double_2;

	[CompilerGenerated]
	private double double_3;

	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private int int_4;

	[CompilerGenerated]
	private int int_5;

	[CompilerGenerated]
	private int int_6;

	[ProtoMember(1)]
	public NeedTypes NeedTypes_0
	{
		[CompilerGenerated]
		get
		{
			return needTypes_0;
		}
		[CompilerGenerated]
		set
		{
			needTypes_0 = value;
		}
	}

	[ProtoMember(2)]
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

	[ProtoMember(3)]
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

	[ProtoMember(4)]
	public double Double_0
	{
		[CompilerGenerated]
		get
		{
			return double_0;
		}
		[CompilerGenerated]
		set
		{
			double_0 = value;
		}
	}

	[ProtoMember(5)]
	public double Double_1
	{
		[CompilerGenerated]
		get
		{
			return double_1;
		}
		[CompilerGenerated]
		set
		{
			double_1 = value;
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

	[ProtoMember(8)]
	public double Double_2
	{
		[CompilerGenerated]
		get
		{
			return double_2;
		}
		[CompilerGenerated]
		set
		{
			double_2 = value;
		}
	}

	[ProtoMember(9)]
	public double Double_3
	{
		[CompilerGenerated]
		get
		{
			return double_3;
		}
		[CompilerGenerated]
		set
		{
			double_3 = value;
		}
	}

	[ProtoMember(10)]
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

	[ProtoMember(11)]
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

	[ProtoMember(12)]
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

	[ProtoMember(13)]
	public int Int32_6
	{
		[CompilerGenerated]
		get
		{
			return int_6;
		}
		[CompilerGenerated]
		set
		{
			int_6 = value;
		}
	}

	public string GetNeedText()
	{
		string empty = string.Empty;
		switch (NeedTypes_0)
		{
		case NeedTypes.LEVEL:
			return string.Format(LocalizationStorage.Get.Term("ui.needs.level"), Int32_0);
		default:
			return "<needs>";
		case NeedTypes.TIER:
			return string.Format(LocalizationStorage.Get.Term("ui.needs.tier"), Int32_2);
		case NeedTypes.ARTIKUL:
		{
			ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(Int32_4);
			return (artikul == null) ? string.Empty : string.Format(LocalizationStorage.Get.Term("ui.needs.artikul"), artikul.String_4);
		}
		}
	}

	public bool Check()
	{
		switch (NeedTypes_0)
		{
		case NeedTypes.LEVEL:
			if (!ChekLevelNeed.Check(this))
			{
				return false;
			}
			break;
		case NeedTypes.SKILL:
			if (!ChekSkillNeed.Check(this))
			{
				return false;
			}
			break;
		case NeedTypes.ARTIKUL:
			if (!ChekArtikulsNeed.Check(this))
			{
				return false;
			}
			break;
		case NeedTypes.DATA:
			if (!ChekDataNeed.Check(this))
			{
				return false;
			}
			break;
		case NeedTypes.PAYMENT_USER:
			if (!ChekPaymentUserNeed.Check(this))
			{
				return false;
			}
			break;
		case NeedTypes.DATA_REGISTRATION:
			if (!ChekDataRegistrationNeed.Check(this))
			{
				return false;
			}
			break;
		case NeedTypes.TIER:
			if (!ChekUserTierNeed.Check(this))
			{
				return false;
			}
			break;
		case NeedTypes.ACTIVE_SLOT_TYPE:
			if (!CheckActiveSlotType.Check(this))
			{
				return false;
			}
			break;
		case NeedTypes.MODE_ID:
			if (!CheckModeId.Check(this))
			{
				return false;
			}
			break;
		case NeedTypes.BLOCK_ARTIKUL:
			if (!ChekBlockArtikulsNeed.Check(this))
			{
				return false;
			}
			break;
		}
		return true;
	}
}
