namespace RC4_Encodeding
{
	internal static class SwapExt
	{
		public static void Swap<T>(this T[] gparam_0, int int_0, int int_1)
		{
			T val = gparam_0[int_0];
			gparam_0[int_0] = gparam_0[int_1];
			gparam_0[int_1] = val;
		}
	}
}
