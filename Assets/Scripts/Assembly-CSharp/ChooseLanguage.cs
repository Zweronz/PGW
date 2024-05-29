using UnityEngine;

public class ChooseLanguage : MonoBehaviour
{
	public UIButton[] languageButtons;

	public UISprite[] languageArrows;

	private UIButton uibutton_0;

	private UISprite uisprite_0;

	private void SetSelectCurrentLanguage()
	{
		int currentLanguageIndex = LocalizationStore.GetCurrentLanguageIndex();
		if (currentLanguageIndex != -1 && currentLanguageIndex < languageButtons.Length)
		{
			if (uibutton_0 != null)
			{
				uibutton_0.ResetDefaultColor();
			}
			if (uisprite_0 != null)
			{
				NGUITools.SetActive(uisprite_0.gameObject, false);
			}
			languageButtons[currentLanguageIndex].Color_0 = Color.grey;
			NGUITools.SetActive(languageArrows[currentLanguageIndex].gameObject, true);
			uibutton_0 = languageButtons[currentLanguageIndex];
			uisprite_0 = languageArrows[currentLanguageIndex];
		}
	}

	private void Start()
	{
		SetSelectCurrentLanguage();
	}

	private void SelectLanguage(string string_0)
	{
		LocalizationStore.String_44 = string_0;
		SetSelectCurrentLanguage();
	}

	public void SetRussianLanguage()
	{
		SelectLanguage("Russian");
	}

	public void SetEnglishLanguage()
	{
		SelectLanguage("English");
	}

	public void SetFrancianLanguage()
	{
		SelectLanguage("French");
	}

	public void SetDeutschLanguage()
	{
		SelectLanguage("German");
	}

	public void SetJapanLanguage()
	{
		SelectLanguage("Japanese");
	}

	public void SetEspanolaLanguage()
	{
		SelectLanguage("Spanish");
	}
}
