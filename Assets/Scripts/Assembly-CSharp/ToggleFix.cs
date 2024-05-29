using UnityEngine;

public class ToggleFix : MonoBehaviour
{
	public UIToggle toggle;

	public UIButton button;

	public UISprite background;

	public UISprite checkmark;

	public bool oldState;

	public bool firstUpdate = true;

	private void Start()
	{
		button = GetComponent<UIButton>();
	}

	private void Update()
	{
		if (button.State_0 != UIButtonColor.State.Pressed)
		{
			checkmark.Color_0 = new Color(checkmark.Color_0.r, checkmark.Color_0.g, checkmark.Color_0.b, (!toggle.Boolean_0) ? 0f : 1f);
			background.Color_0 = new Color(background.Color_0.r, background.Color_0.g, background.Color_0.b, (!toggle.Boolean_0) ? 1f : 0f);
		}
	}

	private void OnPress()
	{
		checkmark.Color_0 = new Color(checkmark.Color_0.r, checkmark.Color_0.g, checkmark.Color_0.b, 0f);
		background.Color_0 = new Color(background.Color_0.r, background.Color_0.g, background.Color_0.b, 1f);
	}
}
