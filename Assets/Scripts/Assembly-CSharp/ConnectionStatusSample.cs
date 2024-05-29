using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BestHTTP.SignalR;
using BestHTTP.SignalR.Hubs;
using UnityEngine;

internal sealed class ConnectionStatusSample : MonoBehaviour
{
	private readonly Uri uri_0 = new Uri("http://besthttpsignalr.azurewebsites.net/signalr");

	private Connection connection_0;

	private GUIMessageList guimessageList_0 = new GUIMessageList();

	[CompilerGenerated]
	private static Dictionary<string, int> dictionary_0;

	private void Start()
	{
		connection_0 = new Connection(uri_0, "StatusHub");
		connection_0.OnNonHubMessage += signalRConnection_OnNonHubMessage;
		connection_0.OnError += signalRConnection_OnError;
		connection_0.OnStateChanged += signalRConnection_OnStateChanged;
		connection_0["StatusHub"].OnMethodCall += statusHub_OnMethodCall;
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
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("START") && connection_0.ConnectionStates_0 != ConnectionStates.Connected)
			{
				connection_0.Open();
			}
			if (GUILayout.Button("STOP") && connection_0.ConnectionStates_0 == ConnectionStates.Connected)
			{
				connection_0.Close();
				guimessageList_0.Clear();
			}
			if (GUILayout.Button("PING") && connection_0.ConnectionStates_0 == ConnectionStates.Connected)
			{
				connection_0["StatusHub"].Call("Ping");
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(20f);
			GUILayout.Label("Connection Status Messages");
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

	private void statusHub_OnMethodCall(Hub hub_0, string string_0, params object[] object_0)
	{
		string arg = ((object_0.Length <= 0) ? string.Empty : (object_0[0] as string));
		string arg2 = ((object_0.Length <= 1) ? string.Empty : object_0[1].ToString());
		switch (string_0)
		{
		case "joined":
			guimessageList_0.Add(string.Format("[{0}] {1} joined at {2}", hub_0.String_0, arg, arg2));
			break;
		case "rejoined":
			guimessageList_0.Add(string.Format("[{0}] {1} reconnected at {2}", hub_0.String_0, arg, arg2));
			break;
		case "leave":
			guimessageList_0.Add(string.Format("[{0}] {1} leaved at {2}", hub_0.String_0, arg, arg2));
			break;
		default:
			guimessageList_0.Add(string.Format("[{0}] {1}", hub_0.String_0, string_0));
			break;
		}
	}
}
