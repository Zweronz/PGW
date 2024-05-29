public class ShopItemStateBuy : BaseShopItemState
{
	public ShopItemButton shopItemButton_0;

	public ShopItemButton shopItemButton_1;

	public UISprite uisprite_0;

	public void SetData(string string_0, ShopArtikulData shopArtikulData_0)
	{
		SetData(string_0);
		shopItemButton_0.Init(shopArtikulData_0);
		shopItemButton_1.Init(shopArtikulData_0);
		bool flag = UserController.UserController_0.GetUserArtikulByArtikulId(shopArtikulData_0.GetArtikul().Int32_0) == null && shopArtikulData_0.GetArtikul().Int32_5 > 0 && shopArtikulData_0.Boolean_4;
		NGUITools.SetActive(shopItemButton_0.gameObject, !flag);
		NGUITools.SetActive(shopItemButton_1.gameObject, flag);
		uisprite_0.String_0 = ((shopArtikulData_0.GetArtikul().Int32_5 <= 0) ? "item_frame_green" : "time_border");
	}
}
