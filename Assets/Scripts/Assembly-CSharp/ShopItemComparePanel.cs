using UnityEngine;

public class ShopItemComparePanel : MonoBehaviour
{
	public ShopItemInfo itemInfo;

	public ShopItemInfo itemCompareInfo;

	protected ArtikulData artikulData_0;

	protected ArtikulData artikulData_1;

	public void SetData(ArtikulData artikulData_2, ArtikulData artikulData_3)
	{
		artikulData_0 = artikulData_2;
		artikulData_1 = artikulData_3;
		itemInfo.Init(artikulData_2);
		itemCompareInfo.Init(artikulData_3);
		InitComponents();
	}

	protected virtual void InitComponents()
	{
	}
}
