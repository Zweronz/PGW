using System;
using BestHTTP;
using BestHTTP.WebSocket;
using UnityEngine;

public class WebSocketSample : MonoBehaviour
{
	private string string_0 = "ws://echo.websocket.org";

	private string string_1 = "Hello World!";

	private string string_2 = string.Empty;

	private WebSocket webSocket_0;

	private Vector2 vector2_0;

	private void OnDestroy()
	{
		if (webSocket_0 != null)
		{
			webSocket_0.Close();
		}
	}

	private void OnGUI()
	{
		GUIHelper.DrawArea(GUIHelper.rect_0, true, delegate
		{
			vector2_0 = GUILayout.BeginScrollView(vector2_0);
			GUILayout.Label(string_2);
			GUILayout.EndScrollView();
			GUILayout.Space(5f);
			GUILayout.FlexibleSpace();
			string_0 = GUILayout.TextField(string_0);
			if (webSocket_0 == null && GUILayout.Button("Open Web Socket"))
			{
				webSocket_0 = new WebSocket(new Uri(string_0));
				if (HTTPManager.HTTPProxy_0 != null)
				{
					webSocket_0.HTTPRequest_0.HTTPProxy_0 = new HTTPProxy(HTTPManager.HTTPProxy_0.Uri_0, HTTPManager.HTTPProxy_0.Credentials_0, false);
				}
				WebSocket webSocket = webSocket_0;
				webSocket.onWebSocketOpenDelegate_0 = (OnWebSocketOpenDelegate)Delegate.Combine(webSocket.onWebSocketOpenDelegate_0, new OnWebSocketOpenDelegate(OnOpen));
				WebSocket webSocket2 = webSocket_0;
				webSocket2.onWebSocketMessageDelegate_0 = (OnWebSocketMessageDelegate)Delegate.Combine(webSocket2.onWebSocketMessageDelegate_0, new OnWebSocketMessageDelegate(OnMessageReceived));
				WebSocket webSocket3 = webSocket_0;
				webSocket3.onWebSocketClosedDelegate_0 = (OnWebSocketClosedDelegate)Delegate.Combine(webSocket3.onWebSocketClosedDelegate_0, new OnWebSocketClosedDelegate(OnClosed));
				WebSocket webSocket4 = webSocket_0;
				webSocket4.onWebSocketErrorDelegate_0 = (OnWebSocketErrorDelegate)Delegate.Combine(webSocket4.onWebSocketErrorDelegate_0, new OnWebSocketErrorDelegate(OnError));
				webSocket_0.Open();
				string_2 += "Opening Web Socket...\n";
			}
			if (webSocket_0 != null && webSocket_0.Boolean_0)
			{
				GUILayout.Space(10f);
				GUILayout.BeginHorizontal();
				string_1 = GUILayout.TextField(string_1);
				if (GUILayout.Button("Send", GUILayout.MaxWidth(70f)))
				{
					string_2 += "Sending message...\n";
					webSocket_0.Send(string_1);
				}
				GUILayout.EndHorizontal();
				GUILayout.Space(10f);
				if (GUILayout.Button("Close"))
				{
					webSocket_0.Close(1000, "Bye!");
				}
			}
		});
	}

	private void OnOpen(WebSocket webSocket_1)
	{
		string_2 += string.Format("-WebSocket Open!\n");
	}

	private void OnMessageReceived(WebSocket webSocket_1, string string_3)
	{
		string_2 += string.Format("-Message received: {0}\n", string_3);
	}

	private void OnClosed(WebSocket webSocket_1, ushort ushort_0, string string_3)
	{
		string_2 += string.Format("-WebSocket closed! Code: {0} Message: {1}\n", ushort_0, string_3);
		webSocket_0 = null;
	}

	private void OnError(WebSocket webSocket_1, Exception exception_0)
	{
		string text = string.Empty;
		if (webSocket_1.HTTPRequest_0.HTTPResponse_0 != null)
		{
			text = string.Format("Status Code from Server: {0} and Message: {1}", webSocket_1.HTTPRequest_0.HTTPResponse_0.Int32_2, webSocket_1.HTTPRequest_0.HTTPResponse_0.String_0);
		}
		string_2 += string.Format("-An error occured: {0}\n", (exception_0 == null) ? ("Unknown Error " + text) : exception_0.Message);
		webSocket_0 = null;
	}
}
