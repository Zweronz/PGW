namespace Rilisoft
{
	internal static class ServiceLocator
	{
		private static readonly FacebookFacade facebookFacade_0;

		private static readonly TwitterFacade twitterFacade_0;

		public static FacebookFacade FacebookFacade_0
		{
			get
			{
				return facebookFacade_0;
			}
		}

		public static TwitterFacade TwitterFacade_0
		{
			get
			{
				return twitterFacade_0;
			}
		}

		static ServiceLocator()
		{
			facebookFacade_0 = new DummyFacebookFacade();
			twitterFacade_0 = new DummyTwitterFacade();
		}
	}
}
