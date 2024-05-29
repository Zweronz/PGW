namespace engine.integrations
{
	public abstract class ISocialNetwork : IIntegration
	{
		public override bool IsSocial()
		{
			return true;
		}

		public abstract bool IsConnected();

		public abstract bool Connected();
	}
}
