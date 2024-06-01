public sealed class Localizer
{
	public static string Get(string string_0)
	{
		string[] split = string_0.Split('.');
		return split[split.Length - 1].Replace("_", " ");
		return LocalizationStorage.Get.Term(string_0);
	}
}
