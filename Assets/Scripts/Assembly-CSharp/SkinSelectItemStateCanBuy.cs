public class SkinSelectItemStateCanBuy : BaseSkinSelectItemState
{
	public UISprite uisprite_0;

	public UILabel uilabel_0;

	public void SetData(string string_0, string string_1, int int_0)
	{
		SetData(string_0);
		uisprite_0.String_0 = string_1;
		uilabel_0.String_0 = int_0.ToString();
	}
}
