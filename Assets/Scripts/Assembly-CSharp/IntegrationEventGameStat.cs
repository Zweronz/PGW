using System.Runtime.CompilerServices;
using engine.integrations;

public sealed class IntegrationEventGameStat : IntegrationEvent
{
	private static IntegrationEventGameStat integrationEventGameStat_0;

	[CompilerGenerated]
	private bool bool_1;

	[CompilerGenerated]
	private bool bool_2;

	public static IntegrationEventGameStat IntegrationEventGameStat_0
	{
		get
		{
			if (integrationEventGameStat_0 == null)
			{
				integrationEventGameStat_0 = new IntegrationEventGameStat();
			}
			return integrationEventGameStat_0;
		}
	}

	public bool Boolean_1
	{
		[CompilerGenerated]
		get
		{
			return bool_1;
		}
		[CompilerGenerated]
		set
		{
			bool_1 = value;
		}
	}

	public bool Boolean_2
	{
		[CompilerGenerated]
		get
		{
			return bool_2;
		}
		[CompilerGenerated]
		set
		{
			bool_2 = value;
		}
	}

	public override bool Boolean_3
	{
		get
		{
			return true;
		}
		set
		{
		}
	}

	public IntegrationEventGameStat()
		: base(IntegrationEventType.GAME_STAT)
	{
	}
}
