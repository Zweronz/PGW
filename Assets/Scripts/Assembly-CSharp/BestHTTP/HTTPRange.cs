using System.Runtime.CompilerServices;

namespace BestHTTP
{
	public sealed class HTTPRange
	{
		[CompilerGenerated]
		private int int_0;

		[CompilerGenerated]
		private int int_1;

		[CompilerGenerated]
		private int int_2;

		[CompilerGenerated]
		private bool bool_0;

		public int Int32_0
		{
			[CompilerGenerated]
			get
			{
				return int_0;
			}
			[CompilerGenerated]
			private set
			{
				int_0 = value;
			}
		}

		public int Int32_1
		{
			[CompilerGenerated]
			get
			{
				return int_1;
			}
			[CompilerGenerated]
			private set
			{
				int_1 = value;
			}
		}

		public int Int32_2
		{
			[CompilerGenerated]
			get
			{
				return int_2;
			}
			[CompilerGenerated]
			private set
			{
				int_2 = value;
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
			private set
			{
				bool_0 = value;
			}
		}

		internal HTTPRange()
		{
			Int32_2 = -1;
			Boolean_0 = false;
		}

		internal HTTPRange(int int_3)
		{
			Int32_2 = int_3;
			Boolean_0 = false;
		}

		internal HTTPRange(int int_3, int int_4, int int_5)
		{
			Int32_0 = int_3;
			Int32_1 = int_4;
			Int32_2 = int_5;
			Boolean_0 = Int32_0 <= Int32_1 && Int32_2 > Int32_1;
		}

		public override string ToString()
		{
			return string.Format("{0}-{1}/{2} (valid: {3})", Int32_0, Int32_1, Int32_2, Boolean_0);
		}
	}
}
