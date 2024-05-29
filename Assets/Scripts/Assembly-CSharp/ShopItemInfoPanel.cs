using UnityEngine;

public class ShopItemInfoPanel : MonoBehaviour
{
	public ShopItemInfo itemInfo;

	public UILabel description;

	protected ArtikulData artikulData_0;

	public void SetData(ArtikulData artikulData_1)
	{
		artikulData_0 = artikulData_1;
		itemInfo.Init(artikulData_1);
		description.String_0 = string.Empty;
		InitComponents();
	}

	protected virtual void InitComponents()
	{
	}
}
