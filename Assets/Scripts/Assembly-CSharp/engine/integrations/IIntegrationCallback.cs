namespace engine.integrations
{
	public abstract class IIntegrationCallback
	{
		public abstract bool OnIntegrationEvent(IIntegration iintegration_0, IntegrationEvent integrationEvent_0);

		public virtual bool IsIntegrationActive(IIntegration iintegration_0)
		{
			return true;
		}
	}
}
