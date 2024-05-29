public class ShopItemStateUpgrade : BaseShopItemState
{
	public UILabel uilabel_0;

	public ShopItemButton shopItemButton_0;

	public void SetData(string string_0, string string_1, ShopArtikulData shopArtikulData_0)
	{
		SetData(string_0);
		uilabel_0.String_0 = string_1;
		shopItemButton_0.Init(shopArtikulData_0);
	}
}
