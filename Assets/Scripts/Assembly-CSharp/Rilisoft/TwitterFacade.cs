namespace Rilisoft
{
	internal abstract class TwitterFacade
	{
		public abstract void Init(string string_0, string string_1);

		public abstract bool IsLoggedIn();

		public abstract void PostStatusUpdate(string string_0);

		public abstract void ShowLoginDialog();
	}
}
