using System;
using BestHTTP.SignalR;
using UnityEngine;

internal sealed class SimpleStreamingSample : MonoBehaviour
{
	private readonly Uri uri_0 = new Uri("http://besthttpsignalr.azurewebsites.net/streaming-connection");

	private Connection connection_0;

	private GUIMessageList guimessageList_0 = new GUIMessageList();

	private void Start()
	{
		connection_0 = new Connection(uri_0);
		connection_0.OnNonHubMessage += signalRConnection_OnNonHubMessage;
		connection_0.OnStateChanged += signalRConnection_OnStateChanged;
		connection_0.OnError += signalRConnection_OnError;
		connection_0.Open();
	}

	private void OnDestroy()
	{
		connection_0.Close();
	}

	private void OnGUI()
	{
		GUIHelper.DrawArea(GUIHelper.rect_0, true, delegate
		{
			GUILayout.Label("Messages");
			GUILayout.BeginHorizontal();
			GUILayout.Space(20f);
			guimessageList_0.Draw(Screen.width - 20, 0f);
			GUILayout.EndHorizontal();
		});
	}

	private void signalRConnection_OnNonHubMessage(Connection connection_1, object object_0)
	{
		guimessageList_0.Add("[Server Message] " + object_0.ToString());
	}

	private void signalRConnection_OnStateChanged(Connection connection_1, ConnectionStates connectionStates_0, ConnectionStates connectionStates_1)
	{
		guimessageList_0.Add(string.Format("[State Change] {0} => {1}", connectionStates_0, connectionStates_1));
	}

	private void signalRConnection_OnError(Connection connection_1, string string_0)
	{
		guimessageList_0.Add("[Error] " + string_0);
	}
}
