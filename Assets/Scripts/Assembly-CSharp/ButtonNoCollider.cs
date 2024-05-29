public class ButtonNoCollider : UIButton
{
	public override bool Boolean_0
	{
		get
		{
			return base.enabled;
		}
		set
		{
			base.enabled = value;
		}
	}

	public void OnHoverOver()
	{
		OnHover(true);
	}

	public void OnHoverOut()
	{
		OnHover(false);
	}

	public void OnPressTap()
	{
		OnPress(true);
	}

	public void OnReleaseTap()
	{
		OnPress(false);
	}
}
