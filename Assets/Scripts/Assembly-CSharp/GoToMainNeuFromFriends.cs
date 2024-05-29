using UnityEngine;

internal sealed class GoToMainNeuFromFriends : MonoBehaviour
{
	private bool bool_0 = true;

	private bool bool_1;

	private void HandleClick()
	{
		ButtonClickSound.buttonClickSound_0.PlayClick();
		LoadConnectScene.texture_0 = null;
		LoadConnectScene.string_0 = Defs.String_11;
		LoadConnectScene.texture_1 = null;
		Application.LoadLevel(Defs.string_3);
	}

	private void OnPress(bool bool_2)
	{
		if (bool_2)
		{
			bool_0 = false;
		}
		else if (!bool_0)
		{
			HandleClick();
		}
	}

	private void Update()
	{
		if (bool_1)
		{
			bool_1 = false;
			HandleClick();
		}
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			bool_1 = true;
		}
	}
}
