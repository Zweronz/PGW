public class SkinSelectItemStateBuy : BaseSkinSelectItemState
{
	public SkinSelectItemButton skinSelectItemButton_0;

	public void SetData(string string_0, ShopArtikulData shopArtikulData_0)
	{
		SetData(string_0);
		skinSelectItemButton_0.Init(shopArtikulData_0);
	}
}
