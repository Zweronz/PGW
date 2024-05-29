using UnityEngine;

public class LoadLevelOnClick : MonoBehaviour
{
	public string levelName;

	private void OnClick()
	{
		if (!string.IsNullOrEmpty(levelName))
		{
			Application.LoadLevel(levelName);
		}
	}
}
