using System.Runtime.CompilerServices;

public abstract class PlayerMessageQueueObject
{
	private bool bool_0;

	[CompilerGenerated]
	private float float_0;

	public float Single_0
	{
		[CompilerGenerated]
		get
		{
			return float_0;
		}
		[CompilerGenerated]
		protected set
		{
			float_0 = value;
		}
	}

	public virtual void UpdateTime(float float_1)
	{
		if (Single_0 > 0f)
		{
			Single_0 -= float_1;
		}
		if (Single_0 < 0f)
		{
			Single_0 = 0f;
		}
		if (Single_0 == 0f && !bool_0)
		{
			bool_0 = true;
			End();
		}
	}

	public abstract void Start();

	public abstract void End();

	public abstract void Pause();

	public abstract void Resume();
}
