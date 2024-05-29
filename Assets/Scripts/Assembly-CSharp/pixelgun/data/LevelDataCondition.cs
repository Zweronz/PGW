using System.Runtime.CompilerServices;

namespace pixelgun.data
{
	public class LevelDataCondition : IDataCondition
	{
		[CompilerGenerated]
		private int int_0;

		public int Int32_0
		{
			[CompilerGenerated]
			get
			{
				return int_0;
			}
			[CompilerGenerated]
			set
			{
				int_0 = value;
			}
		}

		public LevelDataCondition(int int_1, bool bool_1 = false)
		{
			Int32_0 = int_1;
			base.Boolean_0 = bool_1;
		}

		protected override bool CheckOrigin()
		{
			return Int32_0 >= UserController.UserController_0.GetUserLevel();
		}
	}
}
