using System;

namespace Rilisoft
{
	internal sealed class AndroidTwitterFacade : TwitterFacade
	{
		public override void Init(string string_0, string string_1)
		{
		}

		public override bool IsLoggedIn()
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
