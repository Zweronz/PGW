public sealed class Localizer
{
	public static string Get(string string_0)
	{
		string[] split = string_0.Split('.');
		if (string_0.Contains("settings"))
		{
			if (!string_0.Contains("window") || (string_0.Contains("window") && split.Length == 4 && string_0.EndsWith("title")))
				return split[split.Length - 2].Replace("_", " ");
		}
		return split[split.Length - 1].Replace("_", " ");
		//return LocalizationStorage.Get.Term(string_0);
	}
}
