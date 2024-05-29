using System;
using BestHTTP.Cookies;
using BestHTTP.JSON;
using BestHTTP.SignalR;
using BestHTTP.SignalR.JsonEncoders;
using UnityEngine;

public sealed class ConnectionAPISample : MonoBehaviour
{
	private enum MessageTypes
	{
		Send = 0,
		Broadcast = 1,
		Join = 2,
		PrivateMessage = 3,
		AddToGroup = 4,
		RemoveFromGroup = 5,
		SendToGroup = 6,
		BroadcastExceptMe = 7
	}

	private readonly Uri uri_0 = new Uri("http://besthttpsignalr.azurewebsites.net/raw-connection/");

	private Connection connection_0;

	private string string_0 = string.Empty;

	private string string_1 = string.Empty;

	private string string_2 = string.Empty;

	private string string_3 = string.Empty;

	private GUIMessageList guimessageList_0 = new GUIMessageList();

	private void Start()
	{
		if (PlayerPrefs.HasKey("userName"))
		{
			CookieJar.Set(uri_0, new Cookie("user", PlayerPrefs.GetString("userName")));
		}
		connection_0 = new Connection(uri_0);
		connection_0.IJsonEncoder_0 = new LitJsonEncoder();
		connection_0.OnStateChanged += signalRConnection_OnStateChanged;
		connection_0.OnNonHubMessage += signalRConnection_OnGeneralMessage;
		connection_0.Open();
	}

	private void OnGUI()
	{
		GUIHelper.DrawArea(GUIHelper.rect_0, true, delegate
		{
			GUILayout.BeginVertical();
			GUILayout.Label("To Everybody");
			GUILayout.BeginHorizontal();
			string_0 = GUILayout.TextField(string_0, GUILayout.MinWidth(100f));
			if (GUILayout.Button("Broadcast"))
			{
				Broadcast(string_0);
			}
			if (GUILayout.Button("Broadcast (All Except Me)"))
			{
				BroadcastExceptMe(string_0);
			}
			if (GUILayout.Button("Enter Name"))
			{
				EnterName(string_0);
			}
			if (GUILayout.Button("Join Group"))
			{
				JoinGroup(string_0);
			}
			if (GUILayout.Button("Leave Group"))
			{
				LeaveGroup(string_0);
			}
			GUILayout.EndHorizontal();
			GUILayout.Label("To Me");
			GUILayout.BeginHorizontal();
			string_1 = GUILayout.TextField(string_1, GUILayout.MinWidth(100f));
			if (GUILayout.Button("Send to me"))
			{
				SendToMe(string_1);
			}
			GUILayout.EndHorizontal();
			GUILayout.Label("Private Message");
			GUILayout.BeginHorizontal();
			GUILayout.Label("Message:");
			string_2 = GUILayout.TextField(string_2, GUILayout.MinWidth(100f));
			GUILayout.Label("User or Group name:");
			string_3 = GUILayout.TextField(string_3, GUILayout.MinWidth(100f));
			if (GUILayout.Button("Send to user"))
			{
				SendToUser(string_3, string_2);
			}
			if (GUILayout.Button("Send to group"))
			{
				SendToGroup(string_3, string_2);
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(20f);
			if (connection_0.ConnectionStates_0 == ConnectionStates.Closed)
			{
				if (GUILayout.Button("Start Connection"))
				{
					connection_0.Open();
				}
			}
			else if (GUILayout.Button("Stop Connection"))
			{
				connection_0.Close();
			}
			GUILayout.Space(20f);
			GUILayout.Label("Messages");
			GUILayout.BeginHorizontal();
			GUILayout.Space(20f);
			guimessageList_0.Draw(Screen.width - 20, 0f);
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
		});
	}

	private void OnDestroy()
	{
		connection_0.Close();
	}

	private void signalRConnection_OnGeneralMessage(Connection connection_1, object object_0)
	{
		string text = Json.Encode(object_0);
		guimessageList_0.Add("[Server Message] " + text);
	}

	private void signalRConnection_OnStateChanged(Connection connection_1, ConnectionStates connectionStates_0, ConnectionStates connectionStates_1)
	{
		guimessageList_0.Add(string.Format("[State Change] {0} => {1}", connectionStates_0.ToString(), connectionStates_1.ToString()));
	}

	private void Broadcast(string string_4)
	{
		connection_0.Send(new
		{
			gparam_0 = MessageTypes.Broadcast,
			gparam_1 = string_4
		});
	}

	private void BroadcastExceptMe(string string_4)
	{
		connection_0.Send(new
		{
			gparam_0 = MessageTypes.BroadcastExceptMe,
			gparam_1 = string_4
		});
	}

	private void EnterName(string string_4)
	{
		connection_0.Send(new
		{
			gparam_0 = MessageTypes.Join,
			gparam_1 = string_4
		});
	}

	private void JoinGroup(string string_4)
	{
		connection_0.Send(new
		{
			gparam_0 = MessageTypes.AddToGroup,
			gparam_1 = string_4
		});
	}

	private void LeaveGroup(string string_4)
	{
		connection_0.Send(new
		{
			gparam_0 = MessageTypes.RemoveFromGroup,
			gparam_1 = string_4
		});
	}

	private void SendToMe(string string_4)
	{
		connection_0.Send(new
		{
			gparam_0 = MessageTypes.Send,
			gparam_1 = string_4
		});
	}

	private void SendToUser(string string_4, string string_5)
	{
		connection_0.Send(new
		{
			gparam_0 = MessageTypes.PrivateMessage,
			gparam_1 = string.Format("{0}|{1}", string_4, string_5)
		});
	}

	private void SendToGroup(string string_4, string string_5)
	{
		connection_0.Send(new
		{
			gparam_0 = MessageTypes.SendToGroup,
			gparam_1 = string.Format("{0}|{1}", string_4, string_5)
		});
	}
}
