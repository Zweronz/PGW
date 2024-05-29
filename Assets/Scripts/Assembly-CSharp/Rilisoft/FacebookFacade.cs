using System.Collections.Generic;

namespace Rilisoft
{
	internal abstract class FacebookFacade
	{
		public abstract bool CanUserUseFacebookComposer();

		public abstract List<object> GetSessionPermissions();

		public abstract void Init();

		public abstract bool IsSessionValid();

		public abstract void LoginWithReadPermissions(string[] string_0);

		public abstract void ReauthorizeWithPublishPermissions(string[] string_0, FacebookSessionDefaultAudience facebookSessionDefaultAudience_0);

		public abstract void SetSessionLoginBehavior(FacebookSessionLoginBehavior facebookSessionLoginBehavior_0);
	}
}
