using System;
using Rilisoft;
using UnityEngine;

internal sealed class NicknameInput : MonoBehaviour
{
	private const string string_0 = "NamePlayer";

	public UIInput input;

	private UIButton uibutton_0;

	private void HandleOkClicked(object sender, EventArgs e)
	{
		if (ButtonClickSound.buttonClickSound_0 != null)
		{
			ButtonClickSound.buttonClickSound_0.PlayClick();
		}
		Storager.SetString("NicknameRequested", "1");
		if (input != null)
		{
			if (input.String_2 != null)
			{
				string text = input.String_2.Trim();
				string string_ = ((!string.IsNullOrEmpty(text)) ? text : "Unnamed");
				Storager.SetString("NamePlayer", string_);
				input.String_2 = string_;
			}
			if (uibutton_0 != null)
			{
				uibutton_0.Boolean_0 = false;
			}
		}
		Application.LoadLevel(Defs.String_11);
	}

	private void Start()
	{
		ButtonHandler componentInChildren = base.gameObject.GetComponentInChildren<ButtonHandler>();
		if (componentInChildren != null)
		{
			componentInChildren.Clicked += HandleOkClicked;
			uibutton_0 = componentInChildren.GetComponent<UIButton>();
		}
		if (input != null)
		{
			string playerNameOrDefault = Defs.GetPlayerNameOrDefault();
			input.String_2 = playerNameOrDefault;
		}
	}
}
