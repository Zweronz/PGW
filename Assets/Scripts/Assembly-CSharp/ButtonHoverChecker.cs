public class ButtonHoverChecker : UIButton
{
	protected override void OnHover(bool bool_3)
	{
		base.OnHover(bool_3);
	}

	public void Show()
	{
		HoverChecker component = base.transform.parent.GetComponent<HoverChecker>();
		if (component != null)
		{
			component.Show();
		}
	}
}
