using System.Runtime.CompilerServices;

namespace engine.integrations
{
	public abstract class IntegrationEvent
	{
		private IntegrationEventType integrationEventType_0 = IntegrationEventType.UNKNOWN;

		[CompilerGenerated]
		private bool bool_0;

		public abstract bool Boolean_3 { get; set; }

		public IntegrationEventType IntegrationEventType_0
		{
			get
			{
				return integrationEventType_0;
			}
		}

		public bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_0;
			}
			[CompilerGenerated]
			set
			{
				bool_0 = value;
			}
		}

		public IntegrationEvent(IntegrationEventType integrationEventType_1)
		{
			integrationEventType_0 = integrationEventType_1;
		}
	}
}
