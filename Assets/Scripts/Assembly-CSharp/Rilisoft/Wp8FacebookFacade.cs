using System;
using System.Collections.Generic;
using Rilisoft.MiniJson;
using UnityEngine;

namespace Rilisoft
{
	internal sealed class Wp8FacebookFacade : FacebookFacade
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

		private void HandleCompletion(string string_0, object object_0)
		{
			if (string_0 != null)
			{
				Debug.Log(string_0);
			}
			else
			{
				Debug.Log(Json.Serialize(object_0));
			}
		}
	}
}
