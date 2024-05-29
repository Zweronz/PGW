public class UIStorageSlot : UIItemSlot
{
	public UIItemStorage uiitemStorage_0;

	public int int_0;

	protected override InvGameItem InvGameItem_0
	{
		get
		{
			return (!(uiitemStorage_0 != null)) ? null : uiitemStorage_0.GetItem(int_0);
		}
	}

	protected override InvGameItem Replace(InvGameItem invGameItem_2)
	{
		return (!(uiitemStorage_0 != null)) ? invGameItem_2 : uiitemStorage_0.Replace(int_0, invGameItem_2);
	}
}
