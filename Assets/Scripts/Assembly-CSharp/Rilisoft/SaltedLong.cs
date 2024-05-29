namespace Rilisoft
{
	public struct SaltedLong
	{
		private readonly long long_0;

		private long long_1;

		public long Int64_0
		{
			get
			{
				return long_0 ^ long_1;
			}
			set
			{
				long_1 = long_0 ^ value;
			}
		}

		public SaltedLong(long long_2, long long_3)
		{
			long_0 = long_2;
			long_1 = long_2 ^ long_3;
		}

		public SaltedLong(long long_2)
			: this(long_2, 0L)
		{
		}
	}
}
