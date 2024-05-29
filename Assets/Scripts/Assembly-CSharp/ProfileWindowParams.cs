using engine.unity;

public class ProfileWindowParams : WindowShowParameters
{
	public enum OpenType
	{
		COMMON = 0,
		OTHER_PROFILE_LOBBY = 1,
		OTHER_PROFILE = 2
	}

	public int int_0;

	public OpenType openType_0;

	public ProfileWindowParams(int int_1, OpenType openType_1)
	{
		int_0 = int_1;
		openType_0 = openType_1;
	}
}
