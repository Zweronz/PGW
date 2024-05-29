using System;
using System.Runtime.CompilerServices;

namespace ExitGames.Client.DemoParticle
{
	public class TimeKeeper
	{
		private int int_0 = Environment.TickCount;

		private bool bool_0;

		[CompilerGenerated]
		private int int_1;

		[CompilerGenerated]
		private bool bool_1;

		public int Int32_0
		{
			[CompilerGenerated]
			get
			{
				return int_1;
			}
			[CompilerGenerated]
			set
			{
				int_1 = value;
			}
		}

		public bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_1;
			}
			[CompilerGenerated]
			set
			{
				bool_1 = value;
			}
		}

		public bool Boolean_1
		{
			get
			{
				return Boolean_0 && (bool_0 || Environment.TickCount - int_0 > Int32_0);
			}
			set
			{
				bool_0 = value;
			}
		}

		public TimeKeeper(int int_2)
		{
			Boolean_0 = true;
			Int32_0 = int_2;
		}

		public void Reset()
		{
			bool_0 = false;
			int_0 = Environment.TickCount;
		}
	}
}
