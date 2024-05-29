using System;
using System.Runtime.CompilerServices;

namespace engine.unity
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class GameWindowParamsAttribute : Attribute
	{
		[CompilerGenerated]
		private GameWindowType gameWindowType_0;

		public GameWindowType GameWindowType_0
		{
			[CompilerGenerated]
			get
			{
				return gameWindowType_0;
			}
			[CompilerGenerated]
			private set
			{
				gameWindowType_0 = value;
			}
		}

		public GameWindowParamsAttribute(GameWindowType gameWindowType_1)
		{
			GameWindowType_0 = gameWindowType_1;
		}
	}
}
