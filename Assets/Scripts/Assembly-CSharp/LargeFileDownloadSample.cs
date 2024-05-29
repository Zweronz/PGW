using System;
using System.Collections.Generic;
using BestHTTP;
using UnityEngine;

public sealed class LargeFileDownloadSample : MonoBehaviour
{
	private const string string_0 = "http://ipv4.download.thinkbroadband.com/100MB.zip";

	private HTTPRequest httprequest_0;

	private string string_1 = string.Empty;

	private float float_0;

	private int int_0 = 4096;

	private void Awake()
	{
		if (PlayerPrefs.HasKey("DownloadLength"))
		{
			float_0 = (float)PlayerPrefs.GetInt("DownloadProgress") / (float)PlayerPrefs.GetInt("DownloadLength");
		}
	}

	private void OnDestroy()
	{
		if (httprequest_0 != null && httprequest_0.HTTPRequestStates_0 < HTTPRequestStates.Finished)
		{
			httprequest_0.onDownloadProgressDelegate_0 = null;
			httprequest_0.OnRequestFinishedDelegate_0 = null;
			httprequest_0.Abort();
		}
	}

	private void OnGUI()
	{
		GUIHelper.DrawArea(GUIHelper.rect_0, true, delegate
		{
			GUILayout.Label("Request status: " + string_1);
			GUILayout.Space(5f);
			GUILayout.Label(string.Format("Progress: {0:P2} of {1:N0}Mb", float_0, PlayerPrefs.GetInt("DownloadLength") / 1048576));
			GUILayout.HorizontalSlider(float_0, 0f, 1f);
			GUILayout.Space(50f);
			if (httprequest_0 == null)
			{
				GUILayout.Label(string.Format("Desired Fragment Size: {0:N} KBytes", (float)int_0 / 1024f));
				int_0 = (int)GUILayout.HorizontalSlider(int_0, 4096f, 10485760f);
				GUILayout.Space(5f);
				string text = ((!PlayerPrefs.HasKey("DownloadProgress")) ? "Start Download" : "Continue Download");
				if (GUILayout.Button(text))
				{
					StreamLargeFileTest();
				}
			}
			else if (httprequest_0.HTTPRequestStates_0 == HTTPRequestStates.Processing && GUILayout.Button("Abort Download"))
			{
				httprequest_0.Abort();
			}
		});
	}

	private void StreamLargeFileTest()
	{
		httprequest_0 = new HTTPRequest(new Uri("http://ipv4.download.thinkbroadband.com/100MB.zip"), delegate(HTTPRequest httprequest_1, HTTPResponse httpresponse_0)
		{
			switch (httprequest_1.HTTPRequestStates_0)
			{
			case HTTPRequestStates.Processing:
				if (!PlayerPrefs.HasKey("DownloadLength"))
				{
					string firstHeaderValue = httpresponse_0.GetFirstHeaderValue("content-length");
					if (!string.IsNullOrEmpty(firstHeaderValue))
					{
						PlayerPrefs.SetInt("DownloadLength", int.Parse(firstHeaderValue));
					}
				}
				ProcessFragments(httpresponse_0.GetStreamedFragments());
				string_1 = "Processing";
				break;
			case HTTPRequestStates.Finished:
				if (httpresponse_0.Boolean_0)
				{
					ProcessFragments(httpresponse_0.GetStreamedFragments());
					if (httpresponse_0.Boolean_2)
					{
						string_1 = "Streaming finished!";
						PlayerPrefs.DeleteKey("DownloadProgress");
						PlayerPrefs.Save();
						httprequest_0 = null;
					}
					else
					{
						string_1 = "Processing";
					}
				}
				else
				{
					string_1 = string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", httpresponse_0.Int32_2, httpresponse_0.String_0, httpresponse_0.String_1);
					Debug.LogWarning(string_1);
					httprequest_0 = null;
				}
				break;
			case HTTPRequestStates.Error:
				string_1 = "Request Finished with Error! " + ((httprequest_1.Exception_0 == null) ? "No Exception" : (httprequest_1.Exception_0.Message + "\n" + httprequest_1.Exception_0.StackTrace));
				Debug.LogError(string_1);
				httprequest_0 = null;
				break;
			case HTTPRequestStates.Aborted:
				string_1 = "Request Aborted!";
				Debug.LogWarning(string_1);
				httprequest_0 = null;
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				string_1 = "Connection Timed Out!";
				Debug.LogError(string_1);
				httprequest_0 = null;
				break;
			case HTTPRequestStates.TimedOut:
				string_1 = "Processing the request Timed Out!";
				Debug.LogError(string_1);
				httprequest_0 = null;
				break;
			}
		});
		if (PlayerPrefs.HasKey("DownloadProgress"))
		{
			httprequest_0.SetRangeHeader(PlayerPrefs.GetInt("DownloadProgress"));
		}
		else
		{
			PlayerPrefs.SetInt("DownloadProgress", 0);
		}
		httprequest_0.Boolean_3 = true;
		httprequest_0.Boolean_4 = true;
		httprequest_0.Int32_0 = int_0;
		httprequest_0.Send();
	}

	private void ProcessFragments(List<byte[]> list_0)
	{
		if (list_0 != null && list_0.Count > 0)
		{
			for (int i = 0; i < list_0.Count; i++)
			{
				int value = PlayerPrefs.GetInt("DownloadProgress") + list_0[i].Length;
				PlayerPrefs.SetInt("DownloadProgress", value);
			}
			PlayerPrefs.Save();
			float_0 = (float)PlayerPrefs.GetInt("DownloadProgress") / (float)PlayerPrefs.GetInt("DownloadLength");
		}
	}
}
