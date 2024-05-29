public sealed class Localizer
{
	public static string Get(string string_0)
	{
		return LocalizationStorage.Get.Term(string_0);
	}
}
