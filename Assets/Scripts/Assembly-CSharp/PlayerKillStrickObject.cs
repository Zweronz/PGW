public class PlayerKillStrickObject : PlayerMessageQueueObject
{
	public string string_0;

	public string string_1;

	public bool bool_1 = true;

	public PlayerKillStrickObject()
	{
		base.Single_0 = 2f;
	}

	public override void Start()
	{
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.SetKillStrikeBadge(string_0, string_1, bool_1);
		}
	}

	public override void End()
	{
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.SetKillStrikeBadge(string.Empty, string.Empty, false);
		}
	}

	public override void Pause()
	{
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.SetKillStrikePause(bool_1, true);
		}
	}

	public override void Resume()
	{
		base.Single_0 = 2f;
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.SetKillStrikePause(bool_1, false);
		}
	}
}
