namespace engine.helpers
{
	public static class Flags
	{
		public static void Set<T>(ref T gparam_0, T gparam_1) where T : struct
		{
			int num = (int)(object)gparam_0;
			int num2 = (int)(object)gparam_1;
			gparam_0 = (T)(object)(num | num2);
		}

		public static void Unset<T>(ref T gparam_0, T gparam_1) where T : struct
		{
			int num = (int)(object)gparam_0;
			int num2 = (int)(object)gparam_1;
			gparam_0 = (T)(object)(num & ~num2);
		}

		public static void Toggle<T>(ref T gparam_0, T gparam_1) where T : struct
		{
			if (Contains(gparam_0, gparam_1))
			{
				Unset(ref gparam_0, gparam_1);
			}
			else
			{
				Set(ref gparam_0, gparam_1);
			}
		}

		public static bool Contains<T>(T gparam_0, T gparam_1) where T : struct
		{
			return Contains((int)(object)gparam_0, (int)(object)gparam_1);
		}

		public static bool Contains(int int_0, int int_1)
		{
			return (int_0 & int_1) != 0;
		}
	}
}
