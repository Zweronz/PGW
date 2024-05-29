using engine.unity;

public class RankCupChangeWindowParams : WindowShowParameters
{
	public enum OpenType
	{
		NONE = 0,
		FROM_LOBBY = 1,
		FROM_PROFILE = 2
	}

	public int int_0;

	public OpenType openType_0;

	public RankCupChangeWindowParams(int int_1, OpenType openType_1)
	{
		int_0 = int_1;
		openType_0 = openType_1;
	}
}
