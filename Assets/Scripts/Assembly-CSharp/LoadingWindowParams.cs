using engine.unity;

public class LoadingWindowParams : WindowShowParameters
{
	public ModeData modeData_0;

	public string string_0;

	public bool bool_0;

	public LoadingWindowParams(ModeData modeData_1, bool bool_1 = true)
	{
		modeData_0 = modeData_1;
		string_0 = null;
		bool_0 = bool_1;
	}

	public LoadingWindowParams(string string_1, bool bool_1 = true)
	{
		modeData_0 = null;
		string_0 = string_1;
		bool_0 = bool_1;
	}
}
