public class DevToDevSocialNetworkPostReason
{
	private string string_0;

	public static readonly DevToDevSocialNetworkPostReason devToDevSocialNetworkPostReason_0 = new DevToDevSocialNetworkPostReason("levelup");

	public static readonly DevToDevSocialNetworkPostReason devToDevSocialNetworkPostReason_1 = new DevToDevSocialNetworkPostReason("quest");

	public static readonly DevToDevSocialNetworkPostReason devToDevSocialNetworkPostReason_2 = new DevToDevSocialNetworkPostReason("ss");

	public static readonly DevToDevSocialNetworkPostReason devToDevSocialNetworkPostReason_3 = new DevToDevSocialNetworkPostReason("help");

	public static readonly DevToDevSocialNetworkPostReason devToDevSocialNetworkPostReason_4 = new DevToDevSocialNetworkPostReason("colch");

	public static readonly DevToDevSocialNetworkPostReason devToDevSocialNetworkPostReason_5 = new DevToDevSocialNetworkPostReason("bldnew");

	public static readonly DevToDevSocialNetworkPostReason devToDevSocialNetworkPostReason_6 = new DevToDevSocialNetworkPostReason("bldupg");

	public static readonly DevToDevSocialNetworkPostReason devToDevSocialNetworkPostReason_7 = new DevToDevSocialNetworkPostReason("playing");

	public static readonly DevToDevSocialNetworkPostReason devToDevSocialNetworkPostReason_8 = new DevToDevSocialNetworkPostReason("other");

	public static readonly DevToDevSocialNetworkPostReason devToDevSocialNetworkPostReason_9 = new DevToDevSocialNetworkPostReason("ad");

	public static readonly DevToDevSocialNetworkPostReason devToDevSocialNetworkPostReason_10 = new DevToDevSocialNetworkPostReason("rcs");

	private DevToDevSocialNetworkPostReason(string string_1)
	{
		string_0 = string_1;
	}

	public override string ToString()
	{
		return string_0;
	}

	public static DevToDevSocialNetworkPostReason Custom(string string_1)
	{
		return new DevToDevSocialNetworkPostReason(string_1);
	}
}
