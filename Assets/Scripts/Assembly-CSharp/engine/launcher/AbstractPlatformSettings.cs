using System.Runtime.CompilerServices;

namespace engine.launcher
{
	public static class AbstractPlatformSettings
	{
		[CompilerGenerated]
		private static IPlatformSettings iplatformSettings_0;

		public static IPlatformSettings IPlatformSettings_0
		{
			[CompilerGenerated]
			get
			{
				return iplatformSettings_0;
			}
			[CompilerGenerated]
			set
			{
				iplatformSettings_0 = value;
			}
		}
	}
}
