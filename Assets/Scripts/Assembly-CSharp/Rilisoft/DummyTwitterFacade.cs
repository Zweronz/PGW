namespace Rilisoft
{
	internal sealed class DummyTwitterFacade : TwitterFacade
	{
		public override void Init(string string_0, string string_1)
		{
		}

		public override bool IsLoggedIn()
		{
			return true;
		}

		public override void PostStatusUpdate(string string_0)
		{
		}

		public override void ShowLoginDialog()
		{
		}
	}
}
