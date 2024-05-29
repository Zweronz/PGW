using System.Runtime.CompilerServices;

namespace pixelgun.data
{
	public class IDataCondition
	{
		[CompilerGenerated]
		private bool bool_0;

		protected bool Boolean_0
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

		protected IDataCondition()
		{
			Boolean_0 = false;
		}

		protected virtual bool CheckOrigin()
		{
			return false;
		}

		public bool Check()
		{
			bool flag = CheckOrigin();
			return (!Boolean_0) ? flag : (!flag);
		}
	}
}
