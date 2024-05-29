namespace Rilisoft
{
	public struct SaltedInt
	{
		private readonly int int_0;

		private int int_1;

		public int Int32_0
		{
			get
			{
				return int_0 ^ int_1;
			}
			set
			{
				int_1 = int_0 ^ value;
			}
		}

		public SaltedInt(int int_2, int int_3)
		{
			int_0 = int_2;
			int_1 = int_2 ^ int_3;
		}

		public SaltedInt(int int_2)
			: this(int_2, 0)
		{
		}
	}
}
