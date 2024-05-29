using System;
using UnityEngine;

namespace Rilisoft
{
	internal sealed class Wp8TwitterFacade : TwitterFacade
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

		private static void HandlePostComplete(object object_0, string string_0)
		{
			if (string_0 != null)
			{
				Debug.LogWarning("Twitter request error: " + string_0);
			}
			else if (object_0 != null)
			{
				Debug.Log(object_0);
			}
			else
			{
				Debug.LogWarning("obj == null");
			}
		}
	}
}
