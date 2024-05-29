using System;
using System.Collections.Generic;

namespace Rilisoft
{
	internal sealed class IosFacebookFacade : FacebookFacade
	{
		public override bool CanUserUseFacebookComposer()
		{
			throw new NotSupportedException();
		}

		public override List<object> GetSessionPermissions()
		{
			throw new NotSupportedException();
		}

		public override void Init()
		{
		}

		public override bool IsSessionValid()
		{
			throw new NotSupportedException();
		}

		public override void LoginWithReadPermissions(string[] string_0)
		{
		}

		public override void ReauthorizeWithPublishPermissions(string[] string_0, FacebookSessionDefaultAudience facebookSessionDefaultAudience_0)
		{
		}

		public override void SetSessionLoginBehavior(FacebookSessionLoginBehavior facebookSessionLoginBehavior_0)
		{
		}
	}
}
