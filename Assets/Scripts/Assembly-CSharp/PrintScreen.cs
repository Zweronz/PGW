using System;
using System.IO;
using UnityEngine;

public class PrintScreen : MonoBehaviour
{
	public int resWidth = 2550;

	public int resHeight = 3300;

	private bool bool_0;

	public static string ScreenShotName(int int_0, int int_1)
	{
		return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", Application.dataPath, int_0, int_1, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}

	public void TakeHiResShot()
	{
		bool_0 = true;
	}

	private void LateUpdate()
	{
		bool_0 |= Input.GetKeyDown("k");
		if (bool_0)
		{
			RenderTexture renderTexture = new RenderTexture(resWidth, resHeight, 24);
			base.GetComponent<Camera>().targetTexture = renderTexture;
			Texture2D texture2D = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
			base.GetComponent<Camera>().Render();
			RenderTexture.active = renderTexture;
			texture2D.ReadPixels(new Rect(0f, 0f, resWidth, resHeight), 0, 0);
			base.GetComponent<Camera>().targetTexture = null;
			RenderTexture.active = null;
			UnityEngine.Object.Destroy(renderTexture);
			byte[] bytes = texture2D.EncodeToPNG();
			string text = ScreenShotName(resWidth, resHeight);
			File.WriteAllBytes(text, bytes);
			Debug.Log(string.Format("Took screenshot to: {0}", text));
			bool_0 = false;
		}
	}
}
