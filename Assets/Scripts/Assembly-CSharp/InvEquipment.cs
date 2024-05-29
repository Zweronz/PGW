using UnityEngine;

public class InvEquipment : MonoBehaviour
{
	private InvGameItem[] invGameItem_0;

	private InvAttachmentPoint[] invAttachmentPoint_0;

	public InvGameItem[] InvGameItem_0
	{
		get
		{
			return invGameItem_0;
		}
	}

	public InvGameItem Replace(InvBaseItem.Slot slot_0, InvGameItem invGameItem_1)
	{
		InvBaseItem invBaseItem = ((invGameItem_1 == null) ? null : invGameItem_1.InvBaseItem_0);
		if (slot_0 != 0)
		{
			if (invBaseItem != null && invBaseItem.slot != slot_0)
			{
				return invGameItem_1;
			}
			if (invGameItem_0 == null)
			{
				invGameItem_0 = new InvGameItem[8];
			}
			InvGameItem result = invGameItem_0[(int)(slot_0 - 1)];
			invGameItem_0[(int)(slot_0 - 1)] = invGameItem_1;
			if (invAttachmentPoint_0 == null)
			{
				invAttachmentPoint_0 = GetComponentsInChildren<InvAttachmentPoint>();
			}
			int i = 0;
			for (int num = invAttachmentPoint_0.Length; i < num; i++)
			{
				InvAttachmentPoint invAttachmentPoint = invAttachmentPoint_0[i];
				if (invAttachmentPoint.slot != slot_0)
				{
					continue;
				}
				GameObject gameObject = invAttachmentPoint.Attach((invBaseItem == null) ? null : invBaseItem.attachment);
				if (invBaseItem != null && gameObject != null)
				{
					Renderer renderer = gameObject.GetComponent<Renderer>();
					if (renderer != null)
					{
						renderer.material.color = invBaseItem.color;
					}
				}
			}
			return result;
		}
		if (invGameItem_1 != null)
		{
			Debug.LogWarning("Can't equip \"" + invGameItem_1.String_0 + "\" because it doesn't specify an item slot");
		}
		return invGameItem_1;
	}

	public InvGameItem Equip(InvGameItem invGameItem_1)
	{
		if (invGameItem_1 != null)
		{
			InvBaseItem invBaseItem_ = invGameItem_1.InvBaseItem_0;
			if (invBaseItem_ != null)
			{
				return Replace(invBaseItem_.slot, invGameItem_1);
			}
			Debug.LogWarning("Can't resolve the item ID of " + invGameItem_1.Int32_0);
		}
		return invGameItem_1;
	}

	public InvGameItem Unequip(InvGameItem invGameItem_1)
	{
		if (invGameItem_1 != null)
		{
			InvBaseItem invBaseItem_ = invGameItem_1.InvBaseItem_0;
			if (invBaseItem_ != null)
			{
				return Replace(invBaseItem_.slot, null);
			}
		}
		return invGameItem_1;
	}

	public InvGameItem Unequip(InvBaseItem.Slot slot_0)
	{
		return Replace(slot_0, null);
	}

	public bool HasEquipped(InvGameItem invGameItem_1)
	{
		if (invGameItem_0 != null)
		{
			int i = 0;
			for (int num = invGameItem_0.Length; i < num; i++)
			{
				if (invGameItem_0[i] == invGameItem_1)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool HasEquipped(InvBaseItem.Slot slot_0)
	{
		if (invGameItem_0 != null)
		{
			int i = 0;
			for (int num = invGameItem_0.Length; i < num; i++)
			{
				InvBaseItem invBaseItem_ = invGameItem_0[i].InvBaseItem_0;
				if (invBaseItem_ != null && invBaseItem_.slot == slot_0)
				{
					return true;
				}
			}
		}
		return false;
	}

	public InvGameItem GetItem(InvBaseItem.Slot slot_0)
	{
		if (slot_0 != 0)
		{
			int num = (int)(slot_0 - 1);
			if (invGameItem_0 != null && num < invGameItem_0.Length)
			{
				return invGameItem_0[num];
			}
		}
		return null;
	}
}
