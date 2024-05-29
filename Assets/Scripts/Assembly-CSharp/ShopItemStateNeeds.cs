public class ShopItemStateNeeds : BaseShopItemState
{
	public UILabel uilabel_0;

	public void SetData(string string_0, string string_1)
	{
		SetData(string_0);
		uilabel_0.String_0 = string_1;
	}

	public void SetNeedText(string string_0)
	{
		uilabel_0.String_0 = string_0;
	}
}
