using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BestHTTP.SocketIO;
using BestHTTP.SocketIO.Events;
using UnityEngine;

public sealed class SocketIOChatSample : MonoBehaviour
{
	private enum ChatStates
	{
		Login = 0,
		Chat = 1
	}

	private readonly TimeSpan timeSpan_0 = TimeSpan.FromMilliseconds(700.0);

	private SocketManager socketManager_0;

	private ChatStates chatStates_0;

	private string string_0 = string.Empty;

	private string string_1 = string.Empty;

	private string string_2 = string.Empty;

	private Vector2 vector2_0;

	private bool bool_0;

	private DateTime dateTime_0 = DateTime.MinValue;

	private List<string> list_0 = new List<string>();

	[CompilerGenerated]
	private static SocketIOCallback socketIOCallback_0;

	private void Start()
	{
		chatStates_0 = ChatStates.Login;
		SocketOptions socketOptions = new SocketOptions();
		socketOptions.Boolean_1 = false;
		socketManager_0 = new SocketManager(new Uri("http://chat.socket.io/socket.io/"), socketOptions);
		socketManager_0.Socket_0.On("login", OnLogin);
		socketManager_0.Socket_0.On("new message", OnNewMessage);
		socketManager_0.Socket_0.On("user joined", OnUserJoined);
		socketManager_0.Socket_0.On("user left", OnUserLeft);
		socketManager_0.Socket_0.On("typing", OnTyping);
		socketManager_0.Socket_0.On("stop typing", OnStopTyping);
		socketManager_0.Socket_0.On(SocketIOEventTypes.Error, delegate(Socket socket_0, Packet packet_0, object[] object_0)
		{
			Debug.LogError(string.Format("Error: {0}", object_0[0].ToString()));
		});
		socketManager_0.Open();
	}

	private void OnDestroy()
	{
		socketManager_0.Close();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SampleSelector.sampleDescriptor_0.DestroyUnityObject();
		}
		if (bool_0)
		{
			DateTime utcNow = DateTime.UtcNow;
			TimeSpan timeSpan = utcNow - dateTime_0;
			if (timeSpan >= timeSpan_0)
			{
				socketManager_0.Socket_0.Emit("stop typing");
				bool_0 = false;
			}
		}
	}

	private void OnGUI()
	{
		switch (chatStates_0)
		{
		case ChatStates.Chat:
			DrawChatScreen();
			break;
		case ChatStates.Login:
			DrawLoginScreen();
			break;
		}
	}

	private void DrawLoginScreen()
	{
		GUIHelper.DrawArea(GUIHelper.rect_0, true, delegate
		{
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();
			GUIHelper.DrawCenteredText("What's your nickname?");
			string_0 = GUILayout.TextField(string_0);
			if (GUILayout.Button("Join"))
			{
				SetUserName();
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
		});
	}

	private void DrawChatScreen()
	{
		GUIHelper.DrawArea(GUIHelper.rect_0, true, delegate
		{
			GUILayout.BeginVertical();
			vector2_0 = GUILayout.BeginScrollView(vector2_0);
			GUILayout.Label(string_2, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
			GUILayout.EndScrollView();
			string text = string.Empty;
			if (list_0.Count > 0)
			{
				text += string.Format("{0}", list_0[0]);
				for (int i = 1; i < list_0.Count; i++)
				{
					text += string.Format(", {0}", list_0[i]);
				}
				text = ((list_0.Count != 1) ? (text + " are typing!") : (text + " is typing!"));
			}
			GUILayout.Label(text);
			GUILayout.Label("Type here:");
			GUILayout.BeginHorizontal();
			string_1 = GUILayout.TextField(string_1);
			if (GUILayout.Button("Send", GUILayout.MaxWidth(100f)))
			{
				SendMessage();
			}
			GUILayout.EndHorizontal();
			if (GUI.changed)
			{
				UpdateTyping();
			}
			GUILayout.EndVertical();
		});
	}

	private void SetUserName()
	{
		if (!string.IsNullOrEmpty(string_0))
		{
			chatStates_0 = ChatStates.Chat;
			socketManager_0.Socket_0.Emit("add user", string_0);
		}
	}

	private void SendMessage()
	{
		if (!string.IsNullOrEmpty(string_1))
		{
			socketManager_0.Socket_0.Emit("new message", string_1);
			string_2 += string.Format("{0}: {1}\n", string_0, string_1);
			string_1 = string.Empty;
		}
	}

	private void UpdateTyping()
	{
		if (!bool_0)
		{
			bool_0 = true;
			socketManager_0.Socket_0.Emit("typing");
		}
		dateTime_0 = DateTime.UtcNow;
	}

	private void addParticipantsMessage(Dictionary<string, object> dictionary_0)
	{
		int num = Convert.ToInt32(dictionary_0["numUsers"]);
		if (num == 1)
		{
			string_2 += "there's 1 participant\n";
			return;
		}
		string text = string_2;
		string_2 = text + "there are " + num + " participants\n";
	}

	private void addChatMessage(Dictionary<string, object> dictionary_0)
	{
		string arg = dictionary_0["username"] as string;
		string arg2 = dictionary_0["message"] as string;
		string_2 += string.Format("{0}: {1}\n", arg, arg2);
	}

	private void AddChatTyping(Dictionary<string, object> dictionary_0)
	{
		string item = dictionary_0["username"] as string;
		list_0.Add(item);
	}

	private void RemoveChatTyping(Dictionary<string, object> dictionary_0)
	{
		string string_2 = dictionary_0["username"] as string;
		int num = list_0.FindIndex((string string_1) => string_1.Equals(string_2));
		if (num != -1)
		{
			list_0.RemoveAt(num);
		}
	}

	private void OnLogin(Socket socket_0, Packet packet_0, params object[] object_0)
	{
		string_2 = "Welcome to Socket.IO Chat — \n";
		addParticipantsMessage(object_0[0] as Dictionary<string, object>);
	}

	private void OnNewMessage(Socket socket_0, Packet packet_0, params object[] object_0)
	{
		addChatMessage(object_0[0] as Dictionary<string, object>);
	}

	private void OnUserJoined(Socket socket_0, Packet packet_0, params object[] object_0)
	{
		Dictionary<string, object> dictionary = object_0[0] as Dictionary<string, object>;
		string arg = dictionary["username"] as string;
		string_2 += string.Format("{0} joined\n", arg);
		addParticipantsMessage(dictionary);
	}

	private void OnUserLeft(Socket socket_0, Packet packet_0, params object[] object_0)
	{
		Dictionary<string, object> dictionary = object_0[0] as Dictionary<string, object>;
		string arg = dictionary["username"] as string;
		string_2 += string.Format("{0} left\n", arg);
		addParticipantsMessage(dictionary);
	}

	private void OnTyping(Socket socket_0, Packet packet_0, params object[] object_0)
	{
		AddChatTyping(object_0[0] as Dictionary<string, object>);
	}

	private void OnStopTyping(Socket socket_0, Packet packet_0, params object[] object_0)
	{
		RemoveChatTyping(object_0[0] as Dictionary<string, object>);
	}
}
