using System.Reflection;
using UnityEngine;
using engine.helpers;

[Obfuscation(Exclude = true)]
internal sealed class AppsMenu : MonoBehaviour
{
	public static void SetCurrentLanguage()
	{
		if (LocalizationStore.GetCurrentLanguageCode() == "ru")
		{
			LocalizationStore.String_44 = "English";
		}
		Log.AddLine("[AppsMenu::SetCurrentLanguage. Current language]: " + LocalizationStore.String_44);
	}

	private void Awake()
	{
		Object @object = Resources.Load("Disabler");
		if (@object == null)
		{
			Debug.LogWarning("disabler == null");
		}
		else
		{
			Object.Instantiate(@object);
		}
	}

	private void Start()
	{
		Invoke("LoadLoading", 0.1f);
	}

	private void LoadLoading()
	{
		GlobalGameController.Int32_0 = -1;
		Application.LoadLevel("Loading");
	}
}
