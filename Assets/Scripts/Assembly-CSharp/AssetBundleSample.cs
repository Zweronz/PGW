using System;
using System.Collections;
using BestHTTP;
using UnityEngine;

public sealed class AssetBundleSample : MonoBehaviour
{
	private const string string_0 = "http://besthttp.azurewebsites.net/Content/AssetBundle.html";

	private string string_1 = "Waiting for user interaction";

	private AssetBundle assetBundle_0;

	private Texture2D texture2D_0;

	private bool bool_0;

	private void OnGUI()
	{
		GUIHelper.DrawArea(GUIHelper.rect_0, true, delegate
		{
			GUILayout.Label("Status: " + string_1);
			if (texture2D_0 != null)
			{
				GUILayout.Box(texture2D_0, GUILayout.MaxHeight(256f));
			}
			if (!bool_0 && GUILayout.Button("Start Download"))
			{
				UnloadBundle();
				StartCoroutine(DownloadAssetBundle());
			}
		});
	}

	private void OnDestroy()
	{
		UnloadBundle();
	}

	private IEnumerator DownloadAssetBundle()
	{
		bool_0 = true;
		HTTPRequest hTTPRequest = new HTTPRequest(new Uri("http://besthttp.azurewebsites.net/Content/AssetBundle.html")).Send();
		string_1 = "Download started";
		while (hTTPRequest.HTTPRequestStates_0 < HTTPRequestStates.Finished)
		{
			yield return new WaitForSeconds(0.1f);
			string_1 += ".";
		}
		switch (hTTPRequest.HTTPRequestStates_0)
		{
		case HTTPRequestStates.Finished:
			if (hTTPRequest.HTTPResponse_0.Boolean_0)
			{
				string_1 = string.Format("AssetBundle downloaded! Loaded from local cache: {0}", hTTPRequest.HTTPResponse_0.Boolean_3.ToString());
				AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromMemoryAsync(hTTPRequest.HTTPResponse_0.Byte_0);
				yield return assetBundleCreateRequest;
				yield return StartCoroutine(ProcessAssetBundle(assetBundleCreateRequest.assetBundle));
			}
			else
			{
				string_1 = string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", hTTPRequest.HTTPResponse_0.Int32_2, hTTPRequest.HTTPResponse_0.String_0, hTTPRequest.HTTPResponse_0.String_1);
				Debug.LogWarning(string_1);
			}
			break;
		case HTTPRequestStates.Error:
			string_1 = "Request Finished with Error! " + ((hTTPRequest.Exception_0 == null) ? "No Exception" : (hTTPRequest.Exception_0.Message + "\n" + hTTPRequest.Exception_0.StackTrace));
			Debug.LogError(string_1);
			break;
		case HTTPRequestStates.Aborted:
			string_1 = "Request Aborted!";
			Debug.LogWarning(string_1);
			break;
		case HTTPRequestStates.ConnectionTimedOut:
			string_1 = "Connection Timed Out!";
			Debug.LogError(string_1);
			break;
		case HTTPRequestStates.TimedOut:
			string_1 = "Processing the request Timed Out!";
			Debug.LogError(string_1);
			break;
		}
		bool_0 = false;
	}

	private IEnumerator ProcessAssetBundle(AssetBundle assetBundle_1)
	{
		if (!(assetBundle_1 == null))
		{
			assetBundle_0 = assetBundle_1;
			AssetBundleRequest assetBundleRequest = assetBundle_0.LoadAssetAsync("9443182_orig", typeof(Texture2D));
			yield return assetBundleRequest;
			texture2D_0 = assetBundleRequest.asset as Texture2D;
		}
	}

	private void UnloadBundle()
	{
		if (assetBundle_0 != null)
		{
			assetBundle_0.Unload(true);
			assetBundle_0 = null;
		}
	}
}
