using System;
using BestHTTP;
using UnityEngine;

public sealed class TextureDownloadSample : MonoBehaviour
{
	private const string string_0 = "http://besthttp.azurewebsites.net/Content/";

	private string[] string_1 = new string[9] { "One.png", "Two.png", "Three.png", "Four.png", "Five.png", "Six.png", "Seven.png", "Eight.png", "Nine.png" };

	private Texture2D[] texture2D_0 = new Texture2D[9];

	private bool bool_0;

	private int int_0;

	private Vector2 vector2_0;

	private void Awake()
	{
		HTTPManager.Byte_0 = 1;
		for (int i = 0; i < string_1.Length; i++)
		{
			texture2D_0[i] = new Texture2D(100, 150);
		}
	}

	private void OnDestroy()
	{
		HTTPManager.Byte_0 = 4;
	}

	private void OnGUI()
	{
		GUIHelper.DrawArea(GUIHelper.rect_0, true, delegate
		{
			vector2_0 = GUILayout.BeginScrollView(vector2_0);
			GUILayout.SelectionGrid(0, texture2D_0, 3);
			if (int_0 == string_1.Length && bool_0)
			{
				GUIHelper.DrawCenteredText("All images loaded from the local cache!");
			}
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal();
			GUILayout.Label("Max Connection/Server: ", GUILayout.Width(150f));
			GUILayout.Label(HTTPManager.Byte_0.ToString(), GUILayout.Width(20f));
			HTTPManager.Byte_0 = (byte)GUILayout.HorizontalSlider((int)HTTPManager.Byte_0, 1f, 10f);
			GUILayout.EndHorizontal();
			if (GUILayout.Button("Start Download"))
			{
				DownloadImages();
			}
			GUILayout.EndScrollView();
		});
	}

	private void DownloadImages()
	{
		bool_0 = true;
		int_0 = 0;
		for (int i = 0; i < string_1.Length; i++)
		{
			texture2D_0[i] = new Texture2D(100, 150);
			HTTPRequest hTTPRequest = new HTTPRequest(new Uri("http://besthttp.azurewebsites.net/Content/" + string_1[i]), ImageDownloaded);
			hTTPRequest.Object_0 = texture2D_0[i];
			hTTPRequest.Send();
		}
	}

	private void ImageDownloaded(HTTPRequest httprequest_0, HTTPResponse httpresponse_0)
	{
		int_0++;
		switch (httprequest_0.HTTPRequestStates_0)
		{
		case HTTPRequestStates.Finished:
			if (httpresponse_0.Boolean_0)
			{
				Texture2D texture2D = httprequest_0.Object_0 as Texture2D;
				texture2D.LoadImage(httpresponse_0.Byte_0);
				bool_0 = bool_0 && httpresponse_0.Boolean_3;
			}
			else
			{
				Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", httpresponse_0.Int32_2, httpresponse_0.String_0, httpresponse_0.String_1));
			}
			break;
		case HTTPRequestStates.Error:
			Debug.LogError("Request Finished with Error! " + ((httprequest_0.Exception_0 == null) ? "No Exception" : (httprequest_0.Exception_0.Message + "\n" + httprequest_0.Exception_0.StackTrace)));
			break;
		case HTTPRequestStates.Aborted:
			Debug.LogWarning("Request Aborted!");
			break;
		case HTTPRequestStates.ConnectionTimedOut:
			Debug.LogError("Connection Timed Out!");
			break;
		case HTTPRequestStates.TimedOut:
			Debug.LogError("Processing the request Timed Out!");
			break;
		}
	}
}
