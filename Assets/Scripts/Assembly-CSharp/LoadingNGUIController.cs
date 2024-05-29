using UnityEngine;

public sealed class LoadingNGUIController : MonoBehaviour
{
	public UITexture loadingNGUITexture;

	public UILabel[] levelNameLabels;

	public Transform gunsPoint;

	private string string_0 = string.Empty;

	public string String_0
	{
		set
		{
			string_0 = value;
		}
	}

	private void OnEnable()
	{
		Lobby.Lobby_0.Hide();
	}

	public void Init()
	{
	}
}
