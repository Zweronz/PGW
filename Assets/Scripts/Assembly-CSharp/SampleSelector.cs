using System.Collections.Generic;
using BestHTTP;
using BestHTTP.Caching;
using BestHTTP.Cookies;
using BestHTTP.Logger;
using BestHTTP.Statistics;
using UnityEngine;

public class SampleSelector : MonoBehaviour
{
	public const int int_0 = 160;

	private List<SampleDescriptor> list_0 = new List<SampleDescriptor>();

	public static SampleDescriptor sampleDescriptor_0;

	private Vector2 vector2_0;

	private void Awake()
	{
		HTTPManager.ILogger_0.Loglevels_0 = Loglevels.All;
		list_0.Add(new SampleDescriptor(null, "HTTP Samples", string.Empty, string.Empty)
		{
			Boolean_0 = true
		});
		list_0.Add(new SampleDescriptor(typeof(TextureDownloadSample), "Texture Download", "With HTTPManager.MaxConnectionPerServer you can control how many requests can be processed per server parallel.\n\nFeatures demoed in this example:\n-Parallel requests to the same server\n-Controlling the parallelization\n-Automatic Caching\n-Create a Texture2D from the downloaded data", CodeBlocks.string_0));
		list_0.Add(new SampleDescriptor(typeof(AssetBundleSample), "AssetBundle Download", "A small example that shows a possible way to download an AssetBundle and load a resource from it.\n\nFeatures demoed in this example:\n-Using HTTPRequest without a callback\n-Using HTTPRequest in a Coroutine\n-Loading an AssetBundle from the downloaded bytes\n-Automatic Caching", CodeBlocks.string_2));
		list_0.Add(new SampleDescriptor(typeof(LargeFileDownloadSample), "Large File Download", "This example demonstrates how you can download a (large) file and continue the download after the connection is aborted.\n\nFeatures demoed in this example:\n-Setting up a streamed download\n-How to access the downloaded data while the download is in progress\n-Setting the HTTPRequest's StreamFragmentSize to controll the frequency and size of the fragments\n-How to use the SetRangeHeader to continue a previously disconnected download\n-How to disable the local, automatic caching", CodeBlocks.string_3));
		list_0.Add(new SampleDescriptor(null, "WebSocket Samples", string.Empty, string.Empty)
		{
			Boolean_0 = true
		});
		list_0.Add(new SampleDescriptor(typeof(WebSocketSample), "Echo", "A WebSocket demonstration that connects to a WebSocket echo service.\n\nFeatures demoed in this example:\n-Basic useage of the WebSocket class", CodeBlocks.string_1));
		list_0.Add(new SampleDescriptor(null, "Socket.IO Samples", string.Empty, string.Empty)
		{
			Boolean_0 = true
		});
		list_0.Add(new SampleDescriptor(typeof(SocketIOChatSample), "Chat", "This example uses the Socket.IO implementation to connect to the official Chat demo server(http://chat.socket.io/).\n\nFeatures demoed in this example:\n-Instantiating and setting up a SocketManager to connect to a Socket.IO server\n-Changing SocketOptions property\n-Subscribing to Socket.IO events\n-Sending custom events to the server", CodeBlocks.string_4));
		list_0.Add(new SampleDescriptor(typeof(SocketIOWePlaySample), "WePlay", "This example uses the Socket.IO implementation to connect to the official WePlay demo server(http://weplay.io/).\n\nFeatures demoed in this example:\n-Instantiating and setting up a SocketManager to connect to a Socket.IO server\n-Subscribing to Socket.IO events\n-Receiving binary data\n-How to load a texture from the received binary data\n-How to disable payload decoding for fine tune for some speed\n-Sending custom events to the server", CodeBlocks.string_5));
		list_0.Add(new SampleDescriptor(null, "SignalR Samples", string.Empty, string.Empty)
		{
			Boolean_0 = true
		});
		list_0.Add(new SampleDescriptor(typeof(SimpleStreamingSample), "Simple Streaming", "A very simple example of a background thread that broadcasts the server time to all connected clients every two seconds.\n\nFeatures demoed in this example:\n-Subscribing and handling non-hub messages", CodeBlocks.string_6));
		list_0.Add(new SampleDescriptor(typeof(ConnectionAPISample), "Connection API", "Demonstrates all features of the lower-level connection API including starting and stopping, sending and receiving messages, and managing groups.\n\nFeatures demoed in this example:\n-Instantiating and setting up a SignalR Connection to connect to a SignalR server\n-Changing the default Json encoder\n-Subscribing to state changes\n-Receiving and handling of non-hub messages\n-Sending non-hub messages\n-Managing groups", CodeBlocks.string_7));
		list_0.Add(new SampleDescriptor(typeof(ConnectionStatusSample), "Connection Status", "Demonstrates how to handle the events that are raised when connections connect, reconnect and disconnect from the Hub API.\n\nFeatures demoed in this example:\n-Connecting to a Hub\n-Setting up a callback for Hub events\n-Handling server-sent method call requests\n-Calling a Hub-method on the server-side\n-Opening and closing the SignalR Connection", CodeBlocks.string_8));
		list_0.Add(new SampleDescriptor(typeof(DemoHubSample), "Demo Hub", "A contrived example that exploits every feature of the Hub API.\n\nFeatures demoed in this example:\n-Creating and using wrapper Hub classes to encapsulate hub functions and events\n-Handling long running server-side functions by handling progress messages\n-Groups\n-Handling server-side functions with return value\n-Handling server-side functions throwing Exceptions\n-Calling server-side functions with complex type parameters\n-Calling server-side functions with array parameters\n-Calling overloaded server-side functions\n-Changing Hub states\n-Receiving and handling hub state changes\n-Calling server-side functions implemented in VB .NET", CodeBlocks.string_9));
		list_0.Add(new SampleDescriptor(typeof(AuthenticationSample), "Authentication", "Demonstrates how to use the authorization features of the Hub API to restrict certain Hubs and methods to specific users.\n\nFeatures demoed in this example:\n-Creating and using wrapper Hub classes to encapsulate hub functions and events\n-Create and use a Header-based authenticator to access protected APIs\n-SignalR over HTTPS", CodeBlocks.string_10));
		list_0.Add(new SampleDescriptor(null, "Plugin Samples", string.Empty, string.Empty)
		{
			Boolean_0 = true
		});
		list_0.Add(new SampleDescriptor(typeof(CacheMaintenanceSample), "Cache Maintenance", "With this demo you can see how you can use the HTTPCacheService's BeginMaintainence function to delete too old cached entities and keep the cache size under a specified value.\n\nFeatures demoed in this example:\n-How to set up a HTTPCacheMaintananceParams\n-How to call the BeginMaintainence function", CodeBlocks.string_11));
		sampleDescriptor_0 = list_0[1];
	}

	private void Start()
	{
		GUIHelper.rect_0 = new Rect(0f, 165f, Screen.width, Screen.height - 160 - 50);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (sampleDescriptor_0 != null && sampleDescriptor_0.Boolean_2)
			{
				sampleDescriptor_0.DestroyUnityObject();
			}
			else
			{
				Application.Quit();
			}
		}
		if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) && sampleDescriptor_0 != null && !sampleDescriptor_0.Boolean_2)
		{
			sampleDescriptor_0.CreateUnityObject();
		}
	}

	private void OnGUI()
	{
		GeneralStatistics generalStatistics_0 = HTTPManager.GetGeneralStatistics(StatisticsQueryFlags.All);
		GUIHelper.DrawArea(new Rect(0f, 0f, Screen.width / 3, 160f), false, delegate
		{
			GUIHelper.DrawCenteredText("Connections");
			GUILayout.Space(5f);
			GUIHelper.DrawRow("Sum:", generalStatistics_0.int_0.ToString());
			GUIHelper.DrawRow("Active:", generalStatistics_0.int_1.ToString());
			GUIHelper.DrawRow("Free:", generalStatistics_0.int_2.ToString());
			GUIHelper.DrawRow("Recycled:", generalStatistics_0.int_3.ToString());
			GUIHelper.DrawRow("Requests in queue:", generalStatistics_0.int_4.ToString());
		});
		GUIHelper.DrawArea(new Rect(Screen.width / 3, 0f, Screen.width / 3, 160f), false, delegate
		{
			GUIHelper.DrawCenteredText("Cache");
			if (!HTTPCacheService.Boolean_0)
			{
				GUI.color = Color.yellow;
				GUIHelper.DrawCenteredText("Disabled in WebPlayer & Samsung Smart TV Builds!");
				GUI.color = Color.white;
			}
			GUILayout.Space(5f);
			GUIHelper.DrawRow("Cached entities:", generalStatistics_0.int_5.ToString());
			GUIHelper.DrawRow("Sum Size (bytes): ", generalStatistics_0.ulong_0.ToString("N0"));
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Clear Cache"))
			{
				HTTPCacheService.BeginClear();
			}
			GUILayout.EndVertical();
		});
		GUIHelper.DrawArea(new Rect(Screen.width / 3 * 2, 0f, Screen.width / 3, 160f), false, delegate
		{
			GUIHelper.DrawCenteredText("Cookies");
			if (!CookieJar.Boolean_0)
			{
				GUI.color = Color.yellow;
				GUIHelper.DrawCenteredText("Saving and loading from disk is disabled in WebPlayer & Samsung Smart TV Builds!");
				GUI.color = Color.white;
			}
			GUILayout.Space(5f);
			GUIHelper.DrawRow("Cookies:", generalStatistics_0.int_6.ToString());
			GUIHelper.DrawRow("Estimated size (bytes):", generalStatistics_0.uint_0.ToString("N0"));
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Clear Cookies"))
			{
				CookieJar.Clear();
			}
			GUILayout.EndVertical();
		});
		if (sampleDescriptor_0 != null && (sampleDescriptor_0 == null || sampleDescriptor_0.Boolean_2))
		{
			if (sampleDescriptor_0 != null && sampleDescriptor_0.Boolean_2)
			{
				GUILayout.BeginArea(new Rect(0f, Screen.height - 50, Screen.width, 50f), string.Empty);
				GUILayout.FlexibleSpace();
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				GUILayout.BeginVertical();
				GUILayout.FlexibleSpace();
				if (GUILayout.Button("Back", GUILayout.MinWidth(100f)))
				{
					sampleDescriptor_0.DestroyUnityObject();
				}
				GUILayout.FlexibleSpace();
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
				GUILayout.EndArea();
			}
			return;
		}
		GUIHelper.DrawArea(new Rect(0f, 165f, (sampleDescriptor_0 != null) ? (Screen.width / 3) : Screen.width, Screen.height - 160 - 5), false, delegate
		{
			vector2_0 = GUILayout.BeginScrollView(vector2_0);
			for (int i = 0; i < list_0.Count; i++)
			{
				DrawSample(list_0[i]);
			}
			GUILayout.EndScrollView();
		});
		if (sampleDescriptor_0 != null)
		{
			DrawSampleDetails(sampleDescriptor_0);
		}
	}

	private void DrawSample(SampleDescriptor sampleDescriptor_1)
	{
		if (sampleDescriptor_1.Boolean_0)
		{
			GUILayout.Space(15f);
			GUIHelper.DrawCenteredText(sampleDescriptor_1.String_0);
			GUILayout.Space(5f);
		}
		else if (GUILayout.Button(sampleDescriptor_1.String_0))
		{
			sampleDescriptor_1.Boolean_1 = true;
			if (sampleDescriptor_0 != null)
			{
				sampleDescriptor_0.Boolean_1 = false;
			}
			sampleDescriptor_0 = sampleDescriptor_1;
		}
	}

	private void DrawSampleDetails(SampleDescriptor sampleDescriptor_1)
	{
		Rect rect = new Rect(Screen.width / 3, 165f, Screen.width / 3 * 2, Screen.height - 160 - 5);
		GUI.Box(rect, string.Empty);
		GUILayout.BeginArea(rect);
		GUILayout.BeginVertical();
		GUIHelper.DrawCenteredText(sampleDescriptor_1.String_0);
		GUILayout.Space(5f);
		GUILayout.Label(sampleDescriptor_1.String_1);
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Start Sample"))
		{
			sampleDescriptor_1.CreateUnityObject();
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
}
