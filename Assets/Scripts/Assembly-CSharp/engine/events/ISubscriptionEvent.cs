using System;

namespace engine.events
{
	public interface ISubscriptionEvent
	{
		SubscriptionUID SubscriptionUID_0 { get; set; }

		Action<object[]> GetAction();
	}
}
