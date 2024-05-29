public sealed class ResPath
{
	public static string Combine(string string_0, string string_1)
	{
		if (string_0 != null && string_1 != null)
		{
			return string_0 + "/" + string_1;
		}
		return string.Empty;
	}
}
