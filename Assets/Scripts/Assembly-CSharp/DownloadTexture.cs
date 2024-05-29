using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UITexture))]
public class DownloadTexture : MonoBehaviour
{
	public string url = "http://www.yourwebsite.com/logo.png";

	private Texture2D texture2D_0;

	private IEnumerator Start()
	{
		WWW wWW = new WWW(url);
		yield return wWW;
		texture2D_0 = wWW.texture;
		if (texture2D_0 != null)
		{
			UITexture component = GetComponent<UITexture>();
			component.Texture_0 = texture2D_0;
			component.MakePixelPerfect();
		}
		wWW.Dispose();
	}

	private void OnDestroy()
	{
		if (texture2D_0 != null)
		{
			Object.Destroy(texture2D_0);
		}
	}
}
