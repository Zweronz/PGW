using System.Runtime.CompilerServices;

namespace UnityThreading
{
	public class SwitchTo
	{
		public enum TargetType
		{
			Main = 0,
			Thread = 1
		}

		public static readonly SwitchTo switchTo_0 = new SwitchTo(TargetType.Main);

		public static readonly SwitchTo switchTo_1 = new SwitchTo(TargetType.Thread);

		[CompilerGenerated]
		private TargetType targetType_0;

		public TargetType TargetType_0
		{
			[CompilerGenerated]
			get
			{
				return targetType_0;
			}
			[CompilerGenerated]
			private set
			{
				targetType_0 = value;
			}
		}

		private SwitchTo(TargetType targetType_1)
		{
			TargetType_0 = targetType_1;
		}
	}
}
