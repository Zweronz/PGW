public class UIEquipmentSlot : UIItemSlot
{
	public InvEquipment invEquipment_0;

	public InvBaseItem.Slot slot_0;

	protected override InvGameItem InvGameItem_0
	{
		get
		{
			return (!(invEquipment_0 != null)) ? null : invEquipment_0.GetItem(slot_0);
		}
	}

	protected override InvGameItem Replace(InvGameItem invGameItem_2)
	{
		return (!(invEquipment_0 != null)) ? invGameItem_2 : invEquipment_0.Replace(slot_0, invGameItem_2);
	}
}
