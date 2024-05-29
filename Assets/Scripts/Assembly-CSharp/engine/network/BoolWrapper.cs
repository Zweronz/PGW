using System.Runtime.CompilerServices;

namespace engine.network
{
	public class BoolWrapper
	{
		public static BoolWrapper boolWrapper_0 = new BoolWrapper(true);

		public static BoolWrapper boolWrapper_1 = new BoolWrapper(false);

		[CompilerGenerated]
		private bool bool_0;

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

		public BoolWrapper(bool bool_1)
		{
			Boolean_0 = bool_1;
		}

		public static BoolWrapper getWrapper(bool bool_1)
		{
			return (!bool_1) ? boolWrapper_1 : boolWrapper_0;
		}
	}
}
