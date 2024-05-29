using System;
using System.Runtime.CompilerServices;

namespace engine.unity
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ScreenParamsAttribute : Attribute
	{
		[CompilerGenerated]
		private GameScreenType gameScreenType_0;

		public GameScreenType GameScreenType_0
		{
			[CompilerGenerated]
			get
			{
				return gameScreenType_0;
			}
			[CompilerGenerated]
			private set
			{
				gameScreenType_0 = value;
			}
		}

		public ScreenParamsAttribute(GameScreenType gameScreenType_1)
		{
			GameScreenType_0 = gameScreenType_1;
		}
	}
}
