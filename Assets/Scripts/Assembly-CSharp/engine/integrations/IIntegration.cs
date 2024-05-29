using System.Collections.Generic;

namespace engine.integrations
{
	public abstract class IIntegration
	{
		private IIntegrationCallback iintegrationCallback_0;

		public IIntegration()
		{
		}

		public virtual bool IsSocial()
		{
			return false;
		}

		public virtual HashSet<IntegrationEventType> ProcessingEvents()
		{
			return new HashSet<IntegrationEventType>();
		}

		public virtual void SetCallback(IIntegrationCallback iintegrationCallback_1)
		{
			iintegrationCallback_0 = iintegrationCallback_1;
		}

		public virtual void OnEvent(IntegrationEvent integrationEvent_0)
		{
		}

		public virtual bool SendCallback(IntegrationEvent integrationEvent_0)
		{
			if (iintegrationCallback_0 == null)
			{
				return false;
			}
			return iintegrationCallback_0.OnIntegrationEvent(this, integrationEvent_0);
		}

		public abstract bool Init();

		public virtual void OnApplicationFocus(bool bool_0)
		{
		}

		public virtual void OnApplicationPause(bool bool_0)
		{
		}

		public virtual void OnApplicationQuit()
		{
		}
	}
}
