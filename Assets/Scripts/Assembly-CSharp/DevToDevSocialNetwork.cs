public sealed class DevToDevSocialNetwork
{
	private string string_0;

	public static readonly DevToDevSocialNetwork devToDevSocialNetwork_0 = new DevToDevSocialNetwork("vk");

	public static readonly DevToDevSocialNetwork devToDevSocialNetwork_1 = new DevToDevSocialNetwork("tw");

	public static readonly DevToDevSocialNetwork devToDevSocialNetwork_2 = new DevToDevSocialNetwork("fb");

	public static readonly DevToDevSocialNetwork devToDevSocialNetwork_3 = new DevToDevSocialNetwork("gp");

	private DevToDevSocialNetwork(string string_1)
	{
		string_0 = string_1;
	}

	public override string ToString()
	{
		return string_0;
	}

	public static DevToDevSocialNetwork Custom(string string_1)
	{
		return new DevToDevSocialNetwork(string_1);
	}
}
