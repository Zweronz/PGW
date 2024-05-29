using UnityEngine;

public class IPPickerItem : MonoBehaviour
{
	public int itemId;

	public void UpdateItem(int int_0)
	{
		itemId = int_0;
		UpdateView();
	}

	protected virtual void UpdateView()
	{
	}
}
