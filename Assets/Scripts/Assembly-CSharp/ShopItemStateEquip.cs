public class ShopItemStateEquip : BaseShopItemState
{
	public ShopItemButton shopItemButton_0;

	public void SetData(string string_0, ShopArtikulData shopArtikulData_0)
	{
		SetData(string_0);
		shopItemButton_0.Init(shopArtikulData_0);
	}
}
