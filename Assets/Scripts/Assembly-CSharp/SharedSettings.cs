using System.IO;
using UnityEngine;
using engine.data;
using engine.helpers;

public sealed class SharedSettings : BaseSharedSettings
{
	private const string string_0 = "shared_settings_player.txt";

	private static SharedSettings sharedSettings_0;

	private string string_1 = string.Empty;

	public static SharedSettings SharedSettings_0
	{
		get
		{
			if (sharedSettings_0 == null)
			{
				sharedSettings_0 = new SharedSettings();
			}
			return sharedSettings_0;
		}
	}

	public override string String_1
	{
		get
		{
			return string_1 + "shared_settings_player.txt";
		}
	}

	public override string String_2
	{
		get
		{
			return Path.Combine(Application.persistentDataPath, "shared/");
		}
	}

	private SharedSettings()
	{
	}

	public void Init(string string_2)
	{
		string_1 = string_2;
		Utility.CreateDirectoryForFile(String_2);
		ireadWriterShredSettings_0 = new IReadWriterShredSettings[1]
		{
			new PlayerPrefsWriterSharedSettings(this)
		};
		Load();
	}
}
