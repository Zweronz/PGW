using UnityEngine;

public class ChatSettings : MonoBehaviour
{
	public UIToggle checkBox1;

	public UIToggle checkBox2;

	public void Init()
	{
		checkBox1.Boolean_0 = !ChatController.ChatController_0.Boolean_0;
		checkBox2.Boolean_0 = !ChatController.ChatController_0.Boolean_1;
	}

	public void OnAcceptClick()
	{
		ChatController.ChatController_0.Boolean_0 = !checkBox1.Boolean_0;
		ChatController.ChatController_0.Boolean_1 = !checkBox2.Boolean_0;
		Hide();
	}

	public void Show()
	{
		Init();
		NGUITools.SetActive(base.gameObject, true);
	}

	public void Hide()
	{
		NGUITools.SetActive(base.gameObject, false);
	}

	public void OnClick()
	{
		Hide();
	}
}
