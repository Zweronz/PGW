using System.Reflection;
using I2.Loc;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class LocalizationStorage : BaseStorage<string, LocalizationData>
{
	private string _currentLanguageCode;

	private static LocalizationStorage _instance;

	public static LocalizationStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new LocalizationStorage();
			}
			return _instance;
		}
	}

	protected override void OnCreate()
	{
		ChangeLanguage();
		AddEventCallAfterLocalize(ChangeLanguage);
	}

	public void AddEventCallAfterLocalize(LocalizationManager.OnLocalizeCallback addEvent)
	{
		LocalizationManager.OnLocalizeEvent += addEvent;
	}

	public void RemoveEventCallAfterLocalize(LocalizationManager.OnLocalizeCallback addEvent)
	{
		LocalizationManager.OnLocalizeEvent -= addEvent;
	}

	public void ChangeLanguage()
	{
		_currentLanguageCode = LocalizationStore.GetCurrentLanguageCode();
	}

	public string Term(string key)
	{
		return Localizer.Get(key);
		/*string[] split = key.Split('.');
		return split[split.Length - 1].Replace("_", " ");*/
		if (base.Storage != null)
		{
			LocalizationData objectByKey = base.Storage.GetObjectByKey(key);
			if (objectByKey != null && objectByKey.Dictionary_0.ContainsKey(_currentLanguageCode))
			{
				string text = objectByKey.Dictionary_0[_currentLanguageCode];
				if (string.IsNullOrEmpty(text))
				{
					return key;
				}
				return text;
			}
		}
		return LocalizationStore.Get(key);
	}
}
