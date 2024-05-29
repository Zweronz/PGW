using UnityEngine;

public class UIKeyboardButton : MonoBehaviour
{
	public KeyCode keyCode;

	private UIButton uibutton_0;

	private void Awake()
	{
		uibutton_0 = GetComponent<UIButton>();
		if (uibutton_0 == null)
		{
			base.enabled = false;
		}
	}

	private void Update()
	{
		UpdateKeyboard();
	}

	private void UpdateKeyboard()
	{
		if (keyCode != 0)
		{
			if (Input.GetKeyDown(keyCode))
			{
				SendMessage("OnPress", true, SendMessageOptions.DontRequireReceiver);
			}
			else if (Input.GetKeyUp(keyCode))
			{
				SendMessage("OnPress", false, SendMessageOptions.DontRequireReceiver);
				SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
