using UnityEngine;

public class ClansClicked : MonoBehaviour
{
	private void OnClick()
	{
		ButtonClickSound.buttonClickSound_0.PlayClick();
		LoadConnectScene.texture_0 = Resources.Load<Texture>("Friends_Loading");
		LoadConnectScene.string_0 = "Clans";
		LoadConnectScene.texture_1 = null;
		Application.LoadLevel(Defs.string_3);
	}
}
