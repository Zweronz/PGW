using System.Reflection;
using UnityEngine;

public sealed class LoadConnectScene : MonoBehaviour
{
	public static string string_0 = string.Empty;

	public static Texture texture_0 = null;

	public static Texture texture_1 = null;

	public static float float_0 = float_1;

	public Texture loadingNote;

	private static readonly float float_1 = 1f;

	private Texture texture_2;

	private GameObject gameObject_0;

	private LoadingNGUIController loadingNGUIController_0;

	private void Awake()
	{
	}

	private void Start()
	{
		Invoke("_loadConnectScene", float_0);
		float_0 = float_1;
		gameObject_0 = StoreKitEventListener.gameObject_0;
		if (gameObject_0 == null)
		{
			Debug.LogWarning("aInd == null");
		}
		else
		{
			gameObject_0.SetActive(true);
		}
	}

	private void OnGUI()
	{
		if (gameObject_0 != null)
		{
			gameObject_0.SetActive(true);
		}
	}

	[Obfuscation(Exclude = true)]
	private void _loadConnectScene()
	{
		if (string_0.Equals("ConnectScene"))
		{
			Application.LoadLevel(string_0);
		}
		else
		{
			Application.LoadLevelAsync(string_0);
		}
	}

	private void OnDestroy()
	{
		if (!string_0.Equals("ConnectScene") && gameObject_0 != null)
		{
			gameObject_0.SetActive(false);
		}
		texture_0 = null;
	}
}
