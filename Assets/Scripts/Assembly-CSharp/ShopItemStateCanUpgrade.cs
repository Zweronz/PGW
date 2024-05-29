public class ShopItemStateCanUpgrade : BaseShopItemState
{
	public UILabel uilabel_0;

	public UISprite uisprite_0;

	public UILabel uilabel_1;

	public void SetData(string string_0, string string_1, string string_2, int int_0)
	{
		SetData(string_0);
		uilabel_0.String_0 = string_1;
		uisprite_0.String_0 = string_2;
		uilabel_1.String_0 = int_0.ToString();
	}
}
