using System.Runtime.CompilerServices;
using engine.integrations;

public sealed class IntegrationEventDeviceStat : IntegrationEvent
{
	private static IntegrationEventDeviceStat integrationEventDeviceStat_0;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private string string_1;

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private string string_2;

	[CompilerGenerated]
	private string string_3;

	public static IntegrationEventDeviceStat IntegrationEventDeviceStat_0
	{
		get
		{
			if (integrationEventDeviceStat_0 == null)
			{
				integrationEventDeviceStat_0 = new IntegrationEventDeviceStat();
			}
			return integrationEventDeviceStat_0;
		}
	}

	public string String_0
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		set
		{
			string_0 = value;
		}
	}

	public string String_1
	{
		[CompilerGenerated]
		get
		{
			return string_1;
		}
		[CompilerGenerated]
		set
		{
			string_1 = value;
		}
	}

	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		set
		{
			int_0 = value;
		}
	}

	public string String_2
	{
		[CompilerGenerated]
		get
		{
			return string_2;
		}
		[CompilerGenerated]
		set
		{
			string_2 = value;
		}
	}

	public string String_3
	{
		[CompilerGenerated]
		get
		{
			return string_3;
		}
		[CompilerGenerated]
		set
		{
			string_3 = value;
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

	public IntegrationEventDeviceStat()
		: base(IntegrationEventType.DEVICE_STAT)
	{
	}
}
