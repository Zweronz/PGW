using System;
using UnityEngine;

namespace I2.Loc
{
	[Serializable]
	public class EventCallback
	{
		public MonoBehaviour Target;

		public string MethodName = string.Empty;

		public void Execute(UnityEngine.Object object_0 = null)
		{
			if ((bool)Target)
			{
				Target.SendMessage(MethodName, object_0, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
