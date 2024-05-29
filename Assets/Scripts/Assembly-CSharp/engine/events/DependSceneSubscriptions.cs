using System.Collections.Generic;

namespace engine.events
{
	public class DependSceneSubscriptions
	{
		private List<SubscriptionUID> list_0 = new List<SubscriptionUID>();

		public DependSceneSubscriptions()
		{
			EventManager.EventManager_0.GetEvent<ChangeSceneUnityEvent>().Subscribe(ClearOnSceneChange);
		}

		public void AddSubscriptionUID(SubscriptionUID subscriptionUID_0)
		{
			list_0.Add(subscriptionUID_0);
		}

		private void ClearOnSceneChange()
		{
			for (int i = 0; i < list_0.Count; i++)
			{
				list_0[i].Dispose();
			}
			list_0.Clear();
		}
	}
}
