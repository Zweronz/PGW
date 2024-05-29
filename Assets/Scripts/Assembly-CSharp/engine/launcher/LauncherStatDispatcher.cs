using engine.events;

namespace engine.launcher
{
	public sealed class LauncherStatDispatcher : BaseEvent<LauncherStatArgs>
	{
		private static LauncherStatDispatcher launcherStatDispatcher_0;

		public static LauncherStatDispatcher LauncherStatDispatcher_0
		{
			get
			{
				if (launcherStatDispatcher_0 == null)
				{
					launcherStatDispatcher_0 = new LauncherStatDispatcher();
				}
				return launcherStatDispatcher_0;
			}
		}

		private LauncherStatDispatcher()
		{
		}
	}
}
