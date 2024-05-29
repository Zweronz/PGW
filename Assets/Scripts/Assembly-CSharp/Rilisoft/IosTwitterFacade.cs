using System;

namespace Rilisoft
{
	internal sealed class IosTwitterFacade : TwitterFacade
	{
		public override void Init(string string_0, string string_1)
		{
		}

		public override bool IsLoggedIn()
		{
			throw new NotSupportedException();
		}

		public string LoggedInUsername()
		{
			throw new NotSupportedException();
		}

		public override void PostStatusUpdate(string string_0)
		{
		}

		public override void ShowLoginDialog()
		{
		}
	}
}
