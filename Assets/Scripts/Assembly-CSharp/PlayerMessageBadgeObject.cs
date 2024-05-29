public class PlayerMessageBadgeObject : PlayerMessageQueueObject
{
	public string string_0;

	public PlayerMessageBadgeObject()
	{
		base.Single_0 = 2f;
	}

	public override void Start()
	{
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.ShowMessageBadge(string_0);
		}
	}

	public override void End()
	{
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.StopMessageBadge();
		}
	}

	public override void Pause()
	{
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.StopMessageBadge();
		}
	}

	public override void Resume()
	{
		base.Single_0 = 2f;
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.ShowMessageBadge(string_0);
		}
	}
}
