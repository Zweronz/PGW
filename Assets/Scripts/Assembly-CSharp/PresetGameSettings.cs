public class PresetGameSettings
{
	private static PresetGameSettings presetGameSettings_0;

	public readonly string string_0 = "gameCamSens";

	private float float_0 = -1f;

	public static PresetGameSettings PresetGameSettings_0
	{
		get
		{
			if (presetGameSettings_0 == null)
			{
				presetGameSettings_0 = new PresetGameSettings();
			}
			return presetGameSettings_0;
		}
	}

	public float Single_0
	{
		get
		{
			return float_0;
		}
		set
		{
			float_0 = value;
			Save(false);
		}
	}

	private PresetGameSettings()
	{
		Load();
	}

	public void Save()
	{
		Save(true);
	}

	private void Save(bool bool_0)
	{
		SharedSettings.SharedSettings_0.SetValue(string_0, float_0, bool_0);
	}

	private void Load()
	{
		Single_0 = SharedSettings.SharedSettings_0.GetValue(string_0, 50f);
	}
}
