using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BestHTTP.SocketIO;
using UnityEngine;

public sealed class SocketIOWePlaySample : MonoBehaviour
{
	private enum States
	{
		Connecting = 0,
		WaitForNick = 1,
		Joined = 2
	}

	private const float float_0 = 1.5f;

	private string[] string_0 = new string[8] { "left", "right", "a", "b", "up", "down", "select", "start" };

	private int int_0 = 50;

	private States states_0;

	private Socket socket_0;

	private string string_1 = string.Empty;

	private string string_2 = string.Empty;

	private int int_1;

	private List<string> list_0 = new List<string>();

	private Vector2 vector2_0;

	private Texture2D texture2D_0;

	[CompilerGenerated]
	private static Action action_0;

	private void Start()
	{
		SocketOptions socketOptions = new SocketOptions();
		socketOptions.Boolean_1 = false;
		SocketManager socketManager = new SocketManager(new Uri("http://io.weplay.io/socket.io/"), socketOptions);
		socket_0 = socketManager.Socket_0;
		socket_0.On(SocketIOEventTypes.Connect, OnConnected);
		socket_0.On("joined", OnJoined);
		socket_0.On("connections", OnConnections);
		socket_0.On("join", OnJoin);
		socket_0.On("move", OnMove);
		socket_0.On("message", OnMessage);
		socket_0.On("reload", OnReload);
		socket_0.On("frame", OnFrame, false);
		socket_0.On(SocketIOEventTypes.Error, OnError);
		socketManager.Open();
		states_0 = States.Connecting;
	}

	private void OnDestroy()
	{
		socket_0.SocketManager_0.Close();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SampleSelector.sampleDescriptor_0.DestroyUnityObject();
		}
	}

	private void OnGUI()
	{
		switch (states_0)
		{
		case States.Connecting:
			GUIHelper.DrawArea(GUIHelper.rect_0, true, delegate
			{
				GUILayout.BeginVertical();
				GUILayout.FlexibleSpace();
				GUIHelper.DrawCenteredText("Connecting to the server...");
				GUILayout.FlexibleSpace();
				GUILayout.EndVertical();
			});
			break;
		case States.WaitForNick:
			GUIHelper.DrawArea(GUIHelper.rect_0, true, delegate
			{
				DrawLoginScreen();
			});
			break;
		case States.Joined:
			GUIHelper.DrawArea(GUIHelper.rect_0, true, delegate
			{
				if (texture2D_0 != null)
				{
					GUILayout.Box(texture2D_0);
				}
				DrawControls();
				DrawChat();
			});
			break;
		}
	}

	private void DrawLoginScreen()
	{
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();
		GUIHelper.DrawCenteredText("What's your nickname?");
		string_1 = GUILayout.TextField(string_1);
		if (GUILayout.Button("Join"))
		{
			Join();
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
	}

	private void DrawControls()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label("Controls:");
		for (int i = 0; i < string_0.Length; i++)
		{
			if (GUILayout.Button(string_0[i]))
			{
				socket_0.Emit("move", string_0[i]);
			}
		}
		GUILayout.Label(" Connections: " + int_1);
		GUILayout.EndHorizontal();
	}

	private void DrawChat(bool bool_0 = true)
	{
		GUILayout.BeginVertical();
		vector2_0 = GUILayout.BeginScrollView(vector2_0, false, false);
		for (int i = 0; i < list_0.Count; i++)
		{
			GUILayout.Label(list_0[i], GUILayout.MinWidth(Screen.width));
		}
		GUILayout.EndScrollView();
		if (bool_0)
		{
			GUILayout.Label("Your message: ");
			GUILayout.BeginHorizontal();
			string_2 = GUILayout.TextField(string_2);
			if (GUILayout.Button("Send", GUILayout.MaxWidth(100f)))
			{
				SendMessage();
			}
			GUILayout.EndHorizontal();
		}
		GUILayout.EndVertical();
	}

	private void AddMessage(string string_3)
	{
		list_0.Insert(0, string_3);
		if (list_0.Count > int_0)
		{
			list_0.RemoveRange(int_0, list_0.Count - int_0);
		}
	}

	private void SendMessage()
	{
		if (!string.IsNullOrEmpty(string_2))
		{
			socket_0.Emit("message", string_2);
			AddMessage(string.Format("{0}: {1}", string_1, string_2));
			string_2 = string.Empty;
		}
	}

	private void Join()
	{
		PlayerPrefs.SetString("Nick", string_1);
		socket_0.Emit("join", string_1);
	}

	private void Reload()
	{
		texture2D_0 = null;
		if (socket_0 != null)
		{
			socket_0.SocketManager_0.Close();
			socket_0 = null;
			Start();
		}
	}

	private void OnConnected(Socket socket_1, Packet packet_0, params object[] object_0)
	{
		if (PlayerPrefs.HasKey("Nick"))
		{
			string_1 = PlayerPrefs.GetString("Nick", "NickName");
			Join();
		}
		else
		{
			states_0 = States.WaitForNick;
		}
		AddMessage("connected");
	}

	private void OnJoined(Socket socket_1, Packet packet_0, params object[] object_0)
	{
		states_0 = States.Joined;
	}

	private void OnReload(Socket socket_1, Packet packet_0, params object[] object_0)
	{
		Reload();
	}

	private void OnMessage(Socket socket_1, Packet packet_0, params object[] object_0)
	{
		if (object_0.Length == 1)
		{
			AddMessage(object_0[0] as string);
		}
		else
		{
			AddMessage(string.Format("{0}: {1}", object_0[1], object_0[0]));
		}
	}

	private void OnMove(Socket socket_1, Packet packet_0, params object[] object_0)
	{
		AddMessage(string.Format("{0} pressed {1}", object_0[1], object_0[0]));
	}

	private void OnJoin(Socket socket_1, Packet packet_0, params object[] object_0)
	{
		string arg = ((object_0.Length <= 1) ? string.Empty : string.Format("({0})", object_0[1]));
		AddMessage(string.Format("{0} joined {1}", object_0[0], arg));
	}

	private void OnConnections(Socket socket_1, Packet packet_0, params object[] object_0)
	{
		int_1 = Convert.ToInt32(object_0[0]);
	}

	private void OnFrame(Socket socket_1, Packet packet_0, params object[] object_0)
	{
		if (states_0 == States.Joined)
		{
			if (texture2D_0 == null)
			{
				texture2D_0 = new Texture2D(0, 0, TextureFormat.RGBA32, false);
				texture2D_0.filterMode = FilterMode.Point;
			}
			byte[] data = packet_0.List_0[0];
			texture2D_0.LoadImage(data);
		}
	}

	private void OnError(Socket socket_1, Packet packet_0, params object[] object_0)
	{
		AddMessage(string.Format("--ERROR - {0}", object_0[0].ToString()));
	}
}
