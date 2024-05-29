namespace UnityThreading
{
	public class NullDispatcher : DispatcherBase
	{
		public static NullDispatcher nullDispatcher_0 = new NullDispatcher();

		protected override void CheckAccessLimitation()
		{
		}

		internal override void AddTask(Task task_0)
		{
			task_0.DoInternal();
		}
	}
}
