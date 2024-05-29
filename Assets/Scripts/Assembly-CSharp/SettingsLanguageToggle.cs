using System.Collections.Generic;
using UnityEngine;

public class SettingsLanguageToggle : MonoBehaviour
{
	public UISprite activeSprite;

	public UISprite inactiveSprite;

	public string code;

	public EventDelegate onClick = new EventDelegate();

	private List<EventDelegate> list_0 = new List<EventDelegate>();

	private bool bool_0;

	public bool Boolean_0
	{
		get
		{
			return bool_0;
		}
		set
		{
			bool_0 = value;
			NGUITools.SetActive(activeSprite.gameObject, bool_0);
			NGUITools.SetActive(inactiveSprite.gameObject, !bool_0);
		}
	}

	private void Awake()
	{
		list_0.Add(onClick);
	}

	public void OnLanguageClick()
	{
		EventDelegate.Execute(list_0);
		Boolean_0 = true;
		LocalizationStore.String_44 = code;
	}
}
