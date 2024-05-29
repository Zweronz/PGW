public class UIButtonKeys : UIKeyNavigation
{
	public UIButtonKeys uibuttonKeys_0;

	public UIButtonKeys uibuttonKeys_1;

	public UIButtonKeys uibuttonKeys_2;

	public UIButtonKeys uibuttonKeys_3;

	public UIButtonKeys uibuttonKeys_4;

	protected override void OnEnable()
	{
		Upgrade();
		base.OnEnable();
	}

	public void Upgrade()
	{
		if (onClick == null && uibuttonKeys_0 != null)
		{
			onClick = uibuttonKeys_0.gameObject;
			uibuttonKeys_0 = null;
			NGUITools.SetDirty(this);
		}
		if (onLeft == null && uibuttonKeys_3 != null)
		{
			onLeft = uibuttonKeys_3.gameObject;
			uibuttonKeys_3 = null;
			NGUITools.SetDirty(this);
		}
		if (onRight == null && uibuttonKeys_4 != null)
		{
			onRight = uibuttonKeys_4.gameObject;
			uibuttonKeys_4 = null;
			NGUITools.SetDirty(this);
		}
		if (onUp == null && uibuttonKeys_1 != null)
		{
			onUp = uibuttonKeys_1.gameObject;
			uibuttonKeys_1 = null;
			NGUITools.SetDirty(this);
		}
		if (onDown == null && uibuttonKeys_2 != null)
		{
			onDown = uibuttonKeys_2.gameObject;
			uibuttonKeys_2 = null;
			NGUITools.SetDirty(this);
		}
	}
}
