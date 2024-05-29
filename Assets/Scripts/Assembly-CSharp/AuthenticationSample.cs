using System;
using BestHTTP.SignalR;
using BestHTTP.SignalR.Authentication;
using UnityEngine;

internal class AuthenticationSample : MonoBehaviour
{
	private readonly Uri uri_0 = new Uri("https://besthttpsignalr.azurewebsites.net/signalr");

	private Connection connection_0;

	private string string_0 = string.Empty;

	private string string_1 = string.Empty;

	private Vector2 vector2_0;

	private void Start()
	{
		connection_0 = new Connection(uri_0, new BaseHub("noauthhub", "Messages"), new BaseHub("invokeauthhub", "Messages Invoked By Admin or Invoker"), new BaseHub("authhub", "Messages Requiring Authentication to Send or Receive"), new BaseHub("inheritauthhub", "Messages Requiring Authentication to Send or Receive Because of Inheritance"), new BaseHub("incomingauthhub", "Messages Requiring Authentication to Send"), new BaseHub("adminauthhub", "Messages Requiring Admin Membership to Send or Receive"), new BaseHub("userandroleauthhub", "Messages Requiring Name to be \"User\" and Role to be \"Admin\" to Send or Receive"));
		if (!string.IsNullOrEmpty(string_0) && !string.IsNullOrEmpty(string_1))
		{
			connection_0.IAuthenticationProvider_0 = new HeaderAuthenticator(string_0, string_1);
		}
		connection_0.OnConnected += signalRConnection_OnConnected;
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
			vector2_0 = GUILayout.BeginScrollView(vector2_0, false, false);
			GUILayout.BeginVertical();
			if (connection_0.IAuthenticationProvider_0 == null)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label("Username (Enter 'User'):");
				string_0 = GUILayout.TextField(string_0, GUILayout.MinWidth(100f));
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.Label("Roles (Enter 'Invoker' or 'Admin'):");
				string_1 = GUILayout.TextField(string_1, GUILayout.MinWidth(100f));
				GUILayout.EndHorizontal();
				if (GUILayout.Button("Log in"))
				{
					Restart();
				}
			}
			for (int i = 0; i < connection_0.Hub_0.Length; i++)
			{
				(connection_0.Hub_0[i] as BaseHub).Draw();
			}
			GUILayout.EndVertical();
			GUILayout.EndScrollView();
		});
	}

	private void signalRConnection_OnConnected(Connection connection_1)
	{
		for (int i = 0; i < connection_0.Hub_0.Length; i++)
		{
			(connection_0.Hub_0[i] as BaseHub).InvokedFromClient();
		}
	}

	private void Restart()
	{
		connection_0.OnConnected -= signalRConnection_OnConnected;
		connection_0.Close();
		connection_0 = null;
		Start();
	}
}
