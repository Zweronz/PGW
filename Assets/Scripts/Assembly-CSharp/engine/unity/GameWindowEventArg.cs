using System.Runtime.CompilerServices;

namespace engine.unity
{
	public sealed class GameWindowEventArg
	{
		private static GameWindowEventArg gameWindowEventArg_0;

		[CompilerGenerated]
		private GameWindowType gameWindowType_0;

		[CompilerGenerated]
		private bool bool_0;

		public GameWindowType GameWindowType_0
		{
			[CompilerGenerated]
			get
			{
				return gameWindowType_0;
			}
			[CompilerGenerated]
			set
			{
				gameWindowType_0 = value;
			}
		}

		public bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_0;
			}
			[CompilerGenerated]
			set
			{
				bool_0 = value;
			}
		}

		public static GameWindowEventArg GameWindowEventArg_0
		{
			get
			{
				if (gameWindowEventArg_0 == null)
				{
					gameWindowEventArg_0 = new GameWindowEventArg();
				}
				return gameWindowEventArg_0;
			}
		}
	}
}
