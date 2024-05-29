public sealed class CurrentCampaignGame
{
	public static string string_0 = string.Empty;

	public static string string_1 = string.Empty;

	public static float float_0;

	public static bool bool_0;

	public static bool bool_1;

	public static int Int32_0
	{
		get
		{
			if (Switcher.dictionary_0.ContainsKey(string_1))
			{
				return Switcher.dictionary_0[string_1];
			}
			return 0;
		}
	}

	public static void ResetConditionParameters()
	{
		bool_0 = true;
		bool_1 = true;
	}
}
