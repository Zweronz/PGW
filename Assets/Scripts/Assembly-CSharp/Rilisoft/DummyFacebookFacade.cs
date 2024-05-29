using System.Collections.Generic;

namespace Rilisoft
{
	internal sealed class DummyFacebookFacade : FacebookFacade
	{
		public override bool CanUserUseFacebookComposer()
		{
			return false;
		}

		public override List<object> GetSessionPermissions()
		{
			return new List<object>();
		}

		public override void Init()
		{
		}

		public override bool IsSessionValid()
		{
			return true;
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
