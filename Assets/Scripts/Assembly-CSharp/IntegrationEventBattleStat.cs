using System.Runtime.CompilerServices;
using engine.integrations;

public sealed class IntegrationEventBattleStat : IntegrationEvent
{
	private static IntegrationEventBattleStat integrationEventBattleStat_0;

	[CompilerGenerated]
	private bool bool_1;

	public static IntegrationEventBattleStat IntegrationEventBattleStat_0
	{
		get
		{
			if (integrationEventBattleStat_0 == null)
			{
				integrationEventBattleStat_0 = new IntegrationEventBattleStat();
			}
			return integrationEventBattleStat_0;
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

	public IntegrationEventBattleStat()
		: base(IntegrationEventType.BATTLE_STAT)
	{
	}
}
