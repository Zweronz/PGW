public class TestItem : IPPickerItem
{
	public UILabel uilabel_0;

	protected override void UpdateView()
	{
		uilabel_0.String_0 = itemId.ToString();
	}
}
