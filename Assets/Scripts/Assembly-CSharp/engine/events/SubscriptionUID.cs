using System;

namespace engine.events
{
	public class SubscriptionUID : IDisposable, IEquatable<SubscriptionUID>
	{
		private readonly Guid guid_0;

		private Action<SubscriptionUID> action_0;

		public SubscriptionUID(Action<SubscriptionUID> action_1)
		{
			action_0 = action_1;
			guid_0 = Guid.NewGuid();
		}

		public bool Equals(SubscriptionUID other)
		{
			if (other == null)
			{
				return false;
			}
			return object.Equals(guid_0, other.guid_0);
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			return Equals(obj as SubscriptionUID);
		}

		public override int GetHashCode()
		{
			return guid_0.GetHashCode();
		}

		public virtual void Dispose()
		{
			if (action_0 != null)
			{
				action_0(this);
				action_0 = null;
			}
			GC.SuppressFinalize(this);
		}
	}
}
