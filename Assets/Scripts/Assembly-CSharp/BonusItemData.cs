using ProtoBuf;

[ProtoContract]
public sealed class BonusItemData
{
	public enum BonusItemType
	{
		BONUS_ITEM_ARTICUL = 1,
		BONUS_ITEM_SKILL = 2,
		BONUS_ITEM_MONEY = 3,
		BONUS_ITEM_BONUS = 4,
		BONUS_ITEM_EXPIRIENCE = 5
	}

	[ProtoMember(1)]
	public int int_0;

	[ProtoMember(2)]
	public BonusItemType bonusItemType_0;

	[ProtoMember(3)]
	public int int_1;

	[ProtoMember(4)]
	public int int_2;

	[ProtoMember(5)]
	public int int_3;

	[ProtoMember(6)]
	public int int_4;

	[ProtoMember(7)]
	public int int_5;

	[ProtoMember(8)]
	public NeedsData needsData_0;

	[ProtoMember(9)]
	public int int_6;
}
