using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using UnityEngine;

public static class PhotonNetwork
{
	public delegate void EventCallback(byte byte_0, object object_0, int int_0);

	public const string string_0 = "1.28";

	public const string string_1 = "PhotonServerSettings";

	public const string string_2 = "Assets/Photon Unity Networking/Resources/PhotonServerSettings.asset";

	internal static readonly PhotonHandler photonHandler_0;

	internal static NetworkingPeer networkingPeer_0;

	public static readonly int int_0;

	public static ServerSettings serverSettings_0;

	public static float float_0;

	public static float float_1;

	public static float float_2;

	public static bool bool_0;

	public static PhotonLogLevel photonLogLevel_0;

	public static bool bool_1;

	public static Dictionary<string, GameObject> dictionary_0;

	private static bool bool_2;

	private static Room room_0;

	public static bool bool_3;

	public static HashSet<GameObject> hashSet_0;

	private static bool bool_4;

	private static bool bool_5;

	private static bool bool_6;

	private static int int_1;

	private static int int_2;

	private static bool bool_7;

	public static EventCallback eventCallback_0;

	internal static int int_3;

	internal static int int_4;

	internal static List<int> list_0;

	[CompilerGenerated]
	private static List<FriendInfo> list_1;

	public static string String_0
	{
		get
		{
			return networkingPeer_0.string_1;
		}
		set
		{
			networkingPeer_0.string_1 = value;
		}
	}

	public static string String_1
	{
		get
		{
			return (networkingPeer_0 == null) ? "<not connected>" : networkingPeer_0.ServerAddress;
		}
	}

	public static bool Boolean_0
	{
		get
		{
			if (Boolean_3)
			{
				return true;
			}
			if (networkingPeer_0 == null)
			{
				return false;
			}
			return !networkingPeer_0.bool_5 && networkingPeer_0.PeerState_0 != PeerState.PeerCreated && networkingPeer_0.PeerState_0 != PeerState.Disconnected && networkingPeer_0.PeerState_0 != PeerState.Disconnecting && networkingPeer_0.PeerState_0 != PeerState.ConnectingToNameServer;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return networkingPeer_0.bool_5 && !Boolean_3;
		}
	}

	public static bool Boolean_2
	{
		get
		{
			if (!Boolean_0)
			{
				return false;
			}
			if (Boolean_3)
			{
				return true;
			}
			switch (PeerState_0)
			{
			default:
				return true;
			case PeerState.PeerCreated:
			case PeerState.ConnectingToGameserver:
			case PeerState.Joining:
			case PeerState.Leaving:
			case PeerState.ConnectingToMasterserver:
			case PeerState.Disconnecting:
			case PeerState.Disconnected:
			case PeerState.ConnectingToNameServer:
			case PeerState.Authenticating:
				return false;
			}
		}
	}

	public static ConnectionState ConnectionState_0
	{
		get
		{
			if (Boolean_3)
			{
				return ConnectionState.Connected;
			}
			if (networkingPeer_0 == null)
			{
				return ConnectionState.Disconnected;
			}
			switch (networkingPeer_0.PeerState)
			{
			case PeerStateValue.Disconnected:
				return ConnectionState.Disconnected;
			case PeerStateValue.Connecting:
				return ConnectionState.Connecting;
			default:
				return ConnectionState.Disconnected;
			case PeerStateValue.InitializingApplication:
				return ConnectionState.InitializingApplication;
			case PeerStateValue.Connected:
				return ConnectionState.Connected;
			case PeerStateValue.Disconnecting:
				return ConnectionState.Disconnecting;
			}
		}
	}

	public static PeerState PeerState_0
	{
		get
		{
			if (Boolean_3)
			{
				return (room_0 == null) ? PeerState.ConnectedToMaster : PeerState.Joined;
			}
			if (networkingPeer_0 == null)
			{
				return PeerState.Disconnected;
			}
			return networkingPeer_0.PeerState_0;
		}
	}

	public static AuthenticationValues AuthenticationValues_0
	{
		get
		{
			return (networkingPeer_0 == null) ? null : networkingPeer_0.AuthenticationValues_0;
		}
		set
		{
			if (networkingPeer_0 != null)
			{
				networkingPeer_0.AuthenticationValues_0 = value;
			}
		}
	}

	public static Room Room_0
	{
		get
		{
			if (bool_2)
			{
				return room_0;
			}
			return networkingPeer_0.Room_0;
		}
	}

	public static PhotonPlayer PhotonPlayer_0
	{
		get
		{
			if (networkingPeer_0 == null)
			{
				return null;
			}
			return networkingPeer_0.PhotonPlayer_0;
		}
	}

	public static PhotonPlayer PhotonPlayer_1
	{
		get
		{
			if (networkingPeer_0 == null)
			{
				return null;
			}
			return networkingPeer_0.photonPlayer_2;
		}
	}

	public static string String_2
	{
		get
		{
			return networkingPeer_0.String_2;
		}
		set
		{
			networkingPeer_0.String_2 = value;
		}
	}

	public static PhotonPlayer[] PhotonPlayer_2
	{
		get
		{
			if (networkingPeer_0 == null)
			{
				return new PhotonPlayer[0];
			}
			return networkingPeer_0.photonPlayer_1;
		}
	}

	public static PhotonPlayer[] PhotonPlayer_3
	{
		get
		{
			if (networkingPeer_0 == null)
			{
				return new PhotonPlayer[0];
			}
			return networkingPeer_0.photonPlayer_0;
		}
	}

	public static List<FriendInfo> List_0
	{
		[CompilerGenerated]
		get
		{
			return list_1;
		}
		[CompilerGenerated]
		internal set
		{
			list_1 = value;
		}
	}

	public static int Int32_0
	{
		get
		{
			return (networkingPeer_0 != null) ? networkingPeer_0.Int32_4 : 0;
		}
	}

	public static bool Boolean_3
	{
		get
		{
			return bool_2;
		}
		set
		{
			if (value == bool_2)
			{
				return;
			}
			if (value && Boolean_0)
			{
				Debug.LogError("Can't start OFFLINE mode while connected!");
				return;
			}
			if (networkingPeer_0.PeerState != 0)
			{
				networkingPeer_0.Disconnect();
			}
			bool_2 = value;
			if (bool_2)
			{
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectedToMaster);
				networkingPeer_0.ChangeLocalID(1);
				networkingPeer_0.photonPlayer_2 = PhotonPlayer_0;
			}
			else
			{
				room_0 = null;
				networkingPeer_0.ChangeLocalID(-1);
				networkingPeer_0.photonPlayer_2 = null;
			}
		}
	}

	[Obsolete("Used for compatibility with Unity networking only.")]
	public static int Int32_1
	{
		get
		{
			if (Room_0 == null)
			{
				return 0;
			}
			return Room_0.Int32_2;
		}
		set
		{
			Room_0.Int32_2 = value;
		}
	}

	public static bool Boolean_4
	{
		get
		{
			return bool_4;
		}
		set
		{
			bool_4 = value;
			if (bool_4 && Room_0 != null)
			{
				networkingPeer_0.LoadLevelIfSynced();
			}
		}
	}

	public static bool Boolean_5
	{
		get
		{
			return bool_5;
		}
		set
		{
			if (Room_0 != null)
			{
				Debug.LogError("Setting autoCleanUpPlayerObjects while in a room is not supported.");
			}
			else
			{
				bool_5 = value;
			}
		}
	}

	public static bool Boolean_6
	{
		get
		{
			return bool_6;
		}
		set
		{
			bool_6 = value;
		}
	}

	public static bool Boolean_7
	{
		get
		{
			return networkingPeer_0.bool_4;
		}
	}

	public static TypedLobby TypedLobby_0
	{
		get
		{
			return networkingPeer_0.TypedLobby_1;
		}
		set
		{
			networkingPeer_0.TypedLobby_1 = value;
		}
	}

	public static int Int32_2
	{
		get
		{
			return 1000 / int_1;
		}
		set
		{
			int_1 = 1000 / value;
			if (photonHandler_0 != null)
			{
				photonHandler_0.updateInterval = int_1;
			}
			if (value < Int32_3)
			{
				Int32_3 = value;
			}
		}
	}

	public static int Int32_3
	{
		get
		{
			return 1000 / int_2;
		}
		set
		{
			if (value > Int32_2)
			{
				Debug.LogError("Error, can not set the OnSerialize SendRate more often then the overall SendRate");
				value = Int32_2;
			}
			int_2 = 1000 / value;
			if (photonHandler_0 != null)
			{
				photonHandler_0.updateIntervalOnSerialize = int_2;
			}
		}
	}

	public static bool Boolean_8
	{
		get
		{
			return bool_7;
		}
		set
		{
			if (value)
			{
				PhotonHandler.StartFallbackSendAckThread();
			}
			networkingPeer_0.IsSendingOnlyAcks = !value;
			bool_7 = value;
		}
	}

	public static int Int32_4
	{
		get
		{
			return networkingPeer_0.LimitOfUnreliableCommands;
		}
		set
		{
			networkingPeer_0.LimitOfUnreliableCommands = value;
		}
	}

	public static double Double_0
	{
		get
		{
			if (Boolean_3)
			{
				return Time.time;
			}
			return (double)(uint)networkingPeer_0.ServerTimeInMilliSeconds / 1000.0;
		}
	}

	public static bool Boolean_9
	{
		get
		{
			if (Boolean_3)
			{
				return true;
			}
			return networkingPeer_0.photonPlayer_2 == networkingPeer_0.PhotonPlayer_0;
		}
	}

	public static bool Boolean_10
	{
		get
		{
			return PeerState_0 == PeerState.Joined;
		}
	}

	public static bool Boolean_11
	{
		get
		{
			return !Boolean_9 && Room_0 != null;
		}
	}

	public static int Int32_5
	{
		get
		{
			return networkingPeer_0.Int32_1;
		}
	}

	public static int Int32_6
	{
		get
		{
			return networkingPeer_0.Int32_3;
		}
	}

	public static int Int32_7
	{
		get
		{
			return networkingPeer_0.Int32_3 + networkingPeer_0.Int32_1;
		}
	}

	public static int Int32_8
	{
		get
		{
			return networkingPeer_0.Int32_2;
		}
	}

	public static bool Boolean_12
	{
		get
		{
			return networkingPeer_0.TrafficStatsEnabled;
		}
		set
		{
			networkingPeer_0.TrafficStatsEnabled = value;
		}
	}

	public static int Int32_9
	{
		get
		{
			return networkingPeer_0.ResentReliableCommands;
		}
	}

	public static bool Boolean_13
	{
		get
		{
			return networkingPeer_0.CrcEnabled;
		}
		set
		{
			if (!Boolean_0 && !Boolean_1)
			{
				networkingPeer_0.CrcEnabled = value;
			}
			else
			{
				Debug.Log("Can't change CrcCheckEnabled while being connected. CrcCheckEnabled stays " + networkingPeer_0.CrcEnabled);
			}
		}
	}

	public static int Int32_10
	{
		get
		{
			return networkingPeer_0.PacketLossByCrc;
		}
	}

	public static int Int32_11
	{
		get
		{
			return networkingPeer_0.SentCountAllowance;
		}
		set
		{
			if (value < 3)
			{
				value = 3;
			}
			if (value > 10)
			{
				value = 10;
			}
			networkingPeer_0.SentCountAllowance = value;
		}
	}

	public static ServerConnection ServerConnection_0
	{
		get
		{
			return networkingPeer_0.ServerConnection_0;
		}
	}

	static PhotonNetwork()
	{
		int_0 = 1000;
		serverSettings_0 = (ServerSettings)Resources.Load("PhotonServerSettings", typeof(ServerSettings));
		float_0 = 9.9E-05f;
		float_1 = 1f;
		float_2 = 0.01f;
		bool_0 = true;
		photonLogLevel_0 = PhotonLogLevel.ErrorsOnly;
		bool_1 = true;
		dictionary_0 = new Dictionary<string, GameObject>();
		bool_2 = false;
		room_0 = null;
		bool_3 = true;
		bool_4 = false;
		bool_5 = true;
		bool_6 = true;
		int_1 = 50;
		int_2 = 100;
		bool_7 = true;
		int_3 = 0;
		int_4 = 0;
		list_0 = new List<int>();
		Application.runInBackground = true;
		GameObject gameObject = new GameObject();
		photonHandler_0 = gameObject.AddComponent<PhotonHandler>();
		gameObject.name = "PhotonMono";
		gameObject.hideFlags = HideFlags.HideInHierarchy;
		networkingPeer_0 = new NetworkingPeer(photonHandler_0, string.Empty, ConnectionProtocol.Udp);
		CustomTypes.Register();
	}

	public static bool SetMasterClient(PhotonPlayer photonPlayer_0)
	{
		if (VerifyCanUseNetwork() && Boolean_9)
		{
			return networkingPeer_0.SetMasterClient(photonPlayer_0.Int32_0, true);
		}
		return false;
	}

	public static void NetworkStatisticsReset()
	{
		networkingPeer_0.TrafficStatsReset();
	}

	public static string NetworkStatisticsToString()
	{
		if (networkingPeer_0 != null && !Boolean_3)
		{
			return networkingPeer_0.VitalStatsToString(false);
		}
		return "Offline or in OfflineMode. No VitalStats available.";
	}

	public static void SwitchToProtocol(ConnectionProtocol connectionProtocol_0)
	{
		if (networkingPeer_0.UsedProtocol != connectionProtocol_0)
		{
			try
			{
				networkingPeer_0.Disconnect();
				networkingPeer_0.StopThread();
			}
			catch
			{
			}
			networkingPeer_0 = new NetworkingPeer(photonHandler_0, string.Empty, connectionProtocol_0);
			Debug.Log("Protocol switched to: " + connectionProtocol_0);
		}
	}

	public static void InternalCleanPhotonMonoFromSceneIfStuck()
	{
		PhotonHandler[] array = UnityEngine.Object.FindObjectsOfType(typeof(PhotonHandler)) as PhotonHandler[];
		if (array == null || array.Length <= 0)
		{
			return;
		}
		Debug.Log("Cleaning up hidden PhotonHandler instances in scene. Please save it. This is not an issue.");
		PhotonHandler[] array2 = array;
		foreach (PhotonHandler photonHandler in array2)
		{
			photonHandler.gameObject.hideFlags = HideFlags.None;
			if (photonHandler.gameObject != null && photonHandler.gameObject.name == "PhotonMono")
			{
				UnityEngine.Object.DestroyImmediate(photonHandler.gameObject);
			}
			UnityEngine.Object.DestroyImmediate(photonHandler);
		}
	}

	public static bool ConnectUsingSettings(string string_3)
	{
		if (serverSettings_0 == null)
		{
			Debug.LogError("Can't connect: Loading settings failed. ServerSettings asset must be in any 'Resources' folder as: PhotonServerSettings");
			return false;
		}
		SwitchToProtocol(serverSettings_0.Protocol);
		networkingPeer_0.SetApp(serverSettings_0.AppID, string_3);
		if (serverSettings_0.HostType == ServerSettings.HostingOption.OfflineMode)
		{
			Boolean_3 = true;
			return true;
		}
		if (Boolean_3)
		{
			Debug.LogWarning("ConnectUsingSettings() disabled the offline mode. No longer offline.");
		}
		Boolean_3 = false;
		Boolean_8 = true;
		networkingPeer_0.bool_5 = true;
		if (serverSettings_0.HostType == ServerSettings.HostingOption.SelfHosted)
		{
			networkingPeer_0.Boolean_0 = false;
			networkingPeer_0.String_1 = serverSettings_0.ServerAddress + ":" + serverSettings_0.ServerPort;
			return networkingPeer_0.Connect(networkingPeer_0.String_1, ServerConnection.MasterServer);
		}
		if (serverSettings_0.HostType == ServerSettings.HostingOption.BestRegion)
		{
			return ConnectToBestCloudServer(string_3);
		}
		return networkingPeer_0.ConnectToRegionMaster(serverSettings_0.PreferredRegion);
	}

	public static bool ConnectToMaster(string string_3, int int_5, string string_4, string string_5)
	{
		if (networkingPeer_0.PeerState != 0)
		{
			Debug.LogWarning("ConnectToMaster() failed. Can only connect while in state 'Disconnected'. Current state: " + networkingPeer_0.PeerState);
			return false;
		}
		if (Boolean_3)
		{
			Boolean_3 = false;
			Debug.LogWarning("ConnectToMaster() disabled the offline mode. No longer offline.");
		}
		if (!Boolean_8)
		{
			Boolean_8 = true;
			Debug.LogWarning("ConnectToMaster() enabled isMessageQueueRunning. Needs to be able to dispatch incoming messages.");
		}
		networkingPeer_0.SetApp(string_4, string_5);
		networkingPeer_0.Boolean_0 = false;
		networkingPeer_0.bool_5 = true;
		networkingPeer_0.String_1 = string_3 + ":" + int_5;
		return networkingPeer_0.Connect(networkingPeer_0.String_1, ServerConnection.MasterServer);
	}

	public static bool ConnectToBestCloudServer(string string_3)
	{
		if (serverSettings_0 == null)
		{
			Debug.LogError("Can't connect: Loading settings failed. ServerSettings asset must be in any 'Resources' folder as: PhotonServerSettings");
			return false;
		}
		if (serverSettings_0.HostType == ServerSettings.HostingOption.OfflineMode)
		{
			return ConnectUsingSettings(string_3);
		}
		networkingPeer_0.bool_5 = true;
		networkingPeer_0.SetApp(serverSettings_0.AppID, string_3);
		CloudRegionCode cloudRegionCode_ = PhotonHandler.CloudRegionCode_0;
		if (cloudRegionCode_ != CloudRegionCode.none)
		{
			Debug.Log("Best region found in PlayerPrefs. Connecting to: " + cloudRegionCode_);
			return networkingPeer_0.ConnectToRegionMaster(cloudRegionCode_);
		}
		return networkingPeer_0.ConnectToNameServer();
	}

	public static void OverrideBestCloudServer(CloudRegionCode cloudRegionCode_0)
	{
		PhotonHandler.CloudRegionCode_0 = cloudRegionCode_0;
	}

	public static void RefreshCloudServerRating()
	{
		throw new NotImplementedException("not available at the moment");
	}

	public static void Disconnect()
	{
		if (Boolean_3)
		{
			Boolean_3 = false;
			room_0 = null;
			networkingPeer_0.PeerState_0 = PeerState.Disconnecting;
			networkingPeer_0.OnStatusChanged(StatusCode.Disconnect);
		}
		else if (networkingPeer_0 != null)
		{
			networkingPeer_0.Disconnect();
		}
	}

	[Obsolete("Used for compatibility with Unity networking only. Encryption is automatically initialized while connecting.")]
	public static void InitializeSecurity()
	{
	}

	public static bool FindFriends(string[] string_3)
	{
		if (networkingPeer_0 != null && !bool_2)
		{
			return networkingPeer_0.OpFindFriends(string_3);
		}
		return false;
	}

	[Obsolete("Use overload with RoomOptions and TypedLobby parameters.")]
	public static bool CreateRoom(string string_3, bool bool_8, bool bool_9, int int_5)
	{
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.bool_0 = bool_8;
		roomOptions.bool_1 = bool_9;
		roomOptions.int_0 = int_5;
		return CreateRoom(string_3, roomOptions, null);
	}

	[Obsolete("Use overload with RoomOptions and TypedLobby parameters.")]
	public static bool CreateRoom(string string_3, bool bool_8, bool bool_9, int int_5, Hashtable hashtable_0, string[] string_4)
	{
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.bool_0 = bool_8;
		roomOptions.bool_1 = bool_9;
		roomOptions.int_0 = int_5;
		roomOptions.hashtable_0 = hashtable_0;
		roomOptions.string_0 = string_4;
		return CreateRoom(string_3, roomOptions, null);
	}

	public static bool CreateRoom(string string_3)
	{
		return CreateRoom(string_3, null, null);
	}

	public static bool CreateRoom(string string_3, RoomOptions roomOptions_0, TypedLobby typedLobby_0)
	{
		if (Boolean_3)
		{
			if (room_0 != null)
			{
				Debug.LogError("CreateRoom failed. In offline mode you still have to leave a room to enter another.");
				return false;
			}
			room_0 = new Room(string_3, roomOptions_0);
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom);
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedRoom);
			return true;
		}
		if (networkingPeer_0.ServerConnection_0 == ServerConnection.MasterServer && Boolean_2)
		{
			return networkingPeer_0.OpCreateGame(string_3, roomOptions_0, typedLobby_0);
		}
		Debug.LogError("CreateRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
		return false;
	}

	[Obsolete("Use overload with roomOptions and TypedLobby parameter.")]
	public static bool JoinRoom(string string_3, bool bool_8)
	{
		if (PeerState_0 != PeerState.Joining && PeerState_0 != PeerState.Joined && PeerState_0 != PeerState.ConnectedToGameserver)
		{
			if (Room_0 != null)
			{
				Debug.LogError("JoinRoom aborted: You are already in a room!");
			}
			else
			{
				if (!(string_3 == string.Empty))
				{
					if (Boolean_3)
					{
						room_0 = new Room(string_3, null);
						NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedRoom);
						return true;
					}
					return networkingPeer_0.OpJoinRoom(string_3, null, null, bool_8);
				}
				Debug.LogError("JoinRoom aborted: You must specifiy a room name!");
			}
		}
		else
		{
			Debug.LogError("JoinRoom aborted: You can only join a room while not currently connected/connecting to a room.");
		}
		return false;
	}

	public static bool JoinRoom(string string_3)
	{
		if (Boolean_3)
		{
			if (room_0 != null)
			{
				Debug.LogError("JoinRoom failed. In offline mode you still have to leave a room to enter another.");
				return false;
			}
			room_0 = new Room(string_3, null);
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedRoom);
			return true;
		}
		if (networkingPeer_0.ServerConnection_0 == ServerConnection.MasterServer && Boolean_2)
		{
			if (string.IsNullOrEmpty(string_3))
			{
				Debug.LogError("JoinRoom failed. A roomname is required. If you don't know one, how will you join?");
				return false;
			}
			return networkingPeer_0.OpJoinRoom(string_3, null, null, false);
		}
		Debug.LogError("JoinRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
		return false;
	}

	public static bool JoinOrCreateRoom(string string_3, RoomOptions roomOptions_0, TypedLobby typedLobby_0)
	{
		if (Boolean_3)
		{
			if (room_0 != null)
			{
				Debug.LogError("JoinOrCreateRoom failed. In offline mode you still have to leave a room to enter another.");
				return false;
			}
			room_0 = new Room(string_3, roomOptions_0);
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom);
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedRoom);
			return true;
		}
		if (networkingPeer_0.ServerConnection_0 == ServerConnection.MasterServer && Boolean_2)
		{
			if (string.IsNullOrEmpty(string_3))
			{
				Debug.LogError("JoinOrCreateRoom failed. A roomname is required. If you don't know one, how will you join?");
				return false;
			}
			return networkingPeer_0.OpJoinRoom(string_3, roomOptions_0, typedLobby_0, true);
		}
		Debug.LogError("JoinOrCreateRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
		return false;
	}

	public static bool JoinRandomRoom()
	{
		return JoinRandomRoom(null, 0, MatchmakingMode.FillRoom, null, null);
	}

	public static bool JoinRandomRoom(Hashtable hashtable_0, byte byte_0)
	{
		return JoinRandomRoom(hashtable_0, byte_0, MatchmakingMode.FillRoom, null, null);
	}

	public static bool JoinRandomRoom(Hashtable hashtable_0, byte byte_0, MatchmakingMode matchmakingMode_0, TypedLobby typedLobby_0, string string_3)
	{
		if (Boolean_3)
		{
			if (room_0 != null)
			{
				Debug.LogError("JoinRandomRoom failed. In offline mode you still have to leave a room to enter another.");
				return false;
			}
			room_0 = new Room("offline room", null);
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedRoom);
			return true;
		}
		if (networkingPeer_0.ServerConnection_0 == ServerConnection.MasterServer && Boolean_2)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.MergeStringKeys(hashtable_0);
			if (byte_0 > 0)
			{
				hashtable[byte.MaxValue] = byte_0;
			}
			return networkingPeer_0.OpJoinRandomRoom(hashtable, 0, null, matchmakingMode_0, typedLobby_0, string_3);
		}
		Debug.LogError("JoinRandomRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
		return false;
	}

	public static bool JoinLobby()
	{
		return JoinLobby(null);
	}

	public static bool JoinLobby(TypedLobby typedLobby_0)
	{
		if (Boolean_0 && ServerConnection_0 == ServerConnection.MasterServer)
		{
			if (typedLobby_0 == null)
			{
				typedLobby_0 = TypedLobby.typedLobby_0;
			}
			bool result;
			if (result = networkingPeer_0.OpJoinLobby(typedLobby_0))
			{
				networkingPeer_0.TypedLobby_1 = typedLobby_0;
			}
			return result;
		}
		return false;
	}

	public static bool LeaveLobby()
	{
		if (Boolean_0 && ServerConnection_0 == ServerConnection.MasterServer)
		{
			return networkingPeer_0.OpLeaveLobby();
		}
		return false;
	}

	public static bool LeaveRoom()
	{
		if (Boolean_3)
		{
			room_0 = null;
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnLeftRoom);
			return true;
		}
		if (Room_0 == null)
		{
			Debug.LogWarning("PhotonNetwork.room is null. You don't have to call LeaveRoom() when you're not in one. State: " + PeerState_0);
		}
		return networkingPeer_0.OpLeave();
	}

	public static RoomInfo[] GetRoomList()
	{
		if (!Boolean_3 && networkingPeer_0 != null)
		{
			return networkingPeer_0.roomInfo_0;
		}
		return new RoomInfo[0];
	}

	public static void SetPlayerCustomProperties(Hashtable hashtable_0)
	{
		if (hashtable_0 == null)
		{
			hashtable_0 = new Hashtable();
			foreach (object key in PhotonPlayer_0.Hashtable_0.Keys)
			{
				hashtable_0[(string)key] = null;
			}
		}
		if (Room_0 != null && Room_0.Boolean_1)
		{
			PhotonPlayer_0.SetCustomProperties(hashtable_0);
		}
		else
		{
			PhotonPlayer_0.InternalCacheProperties(hashtable_0);
		}
	}

	public static bool RaiseEvent(byte byte_0, object object_0, bool bool_8, RaiseEventOptions raiseEventOptions_0)
	{
		if (Boolean_10 && byte_0 < 200)
		{
			return networkingPeer_0.OpRaiseEvent(byte_0, object_0, bool_8, raiseEventOptions_0);
		}
		Debug.LogWarning("RaiseEvent() failed. Your event is not being sent! Check if your are in a Room and the eventCode must be less than 200 (0..199).");
		return false;
	}

	public static int AllocateViewID()
	{
		int num = AllocateViewID(PhotonPlayer_0.Int32_0);
		list_0.Add(num);
		return num;
	}

	public static void UnAllocateViewID(int int_5)
	{
		list_0.Remove(int_5);
		if (networkingPeer_0.dictionary_5.ContainsKey(int_5))
		{
			Debug.LogWarning(string.Format("Unallocated manually used viewID: {0} but found it used still in a PhotonView: {1}", int_5, networkingPeer_0.dictionary_5[int_5]));
		}
	}

	private static int AllocateViewID(int int_5)
	{
		if (int_5 == 0)
		{
			int num = int_4;
			int num2 = int_5 * int_0;
			int num3 = 1;
			int num4;
			while (true)
			{
				if (num3 < int_0)
				{
					num = (num + 1) % int_0;
					if (num != 0)
					{
						num4 = num + num2;
						if (!networkingPeer_0.dictionary_5.ContainsKey(num4))
						{
							break;
						}
					}
					num3++;
					continue;
				}
				throw new Exception(string.Format("AllocateViewID() failed. Room (user {0}) is out of subIds, as all room viewIDs are used.", int_5));
			}
			int_4 = num;
			return num4;
		}
		int num5 = int_3;
		int num6 = int_5 * int_0;
		int num7 = 1;
		int num8;
		while (true)
		{
			if (num7 < int_0)
			{
				num5 = (num5 + 1) % int_0;
				if (num5 != 0)
				{
					num8 = num5 + num6;
					if (!networkingPeer_0.dictionary_5.ContainsKey(num8) && !list_0.Contains(num8))
					{
						break;
					}
				}
				num7++;
				continue;
			}
			throw new Exception(string.Format("AllocateViewID() failed. User {0} is out of subIds, as all viewIDs are used.", int_5));
		}
		int_3 = num5;
		return num8;
	}

	private static int[] AllocateSceneViewIDs(int int_5)
	{
		int[] array = new int[int_5];
		for (int i = 0; i < int_5; i++)
		{
			array[i] = AllocateViewID(0);
		}
		return array;
	}

	public static GameObject Instantiate(string string_3, Vector3 vector3_0, Quaternion quaternion_0, int int_5)
	{
		return Instantiate(string_3, vector3_0, quaternion_0, int_5, null);
	}

	public static GameObject Instantiate(string string_3, Vector3 vector3_0, Quaternion quaternion_0, int int_5, object[] object_0)
	{
		if (Boolean_0 && (!bool_0 || Boolean_10))
		{
			GameObject value;
			if (!bool_1 || !dictionary_0.TryGetValue(string_3, out value))
			{
				value = (GameObject)Resources.Load(string_3, typeof(GameObject));
				if (bool_1)
				{
					dictionary_0.Add(string_3, value);
				}
			}
			if (value == null)
			{
				Debug.LogError("Failed to Instantiate prefab: " + string_3 + ". Verify the Prefab is in a Resources folder (and not in a subfolder)");
				return null;
			}
			if (value.GetComponent<PhotonView>() == null)
			{
				Debug.LogError("Failed to Instantiate prefab:" + string_3 + ". Prefab must have a PhotonView component.");
				return null;
			}
			Component[] photonViewsInChildren = value.GetPhotonViewsInChildren();
			int[] array = new int[photonViewsInChildren.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = AllocateViewID(PhotonPlayer_0.Int32_0);
			}
			Hashtable hashtable_ = networkingPeer_0.SendInstantiate(string_3, vector3_0, quaternion_0, int_5, array, object_0, false);
			return networkingPeer_0.DoInstantiate(hashtable_, networkingPeer_0.PhotonPlayer_0, value);
		}
		Debug.LogError("Failed to Instantiate prefab: " + string_3 + ". Client should be in a room. Current connectionStateDetailed: " + PeerState_0);
		return null;
	}

	public static GameObject InstantiateSceneObject(string string_3, Vector3 vector3_0, Quaternion quaternion_0, int int_5, object[] object_0)
	{
		if (Boolean_0 && (!bool_0 || Boolean_10))
		{
			if (!Boolean_9)
			{
				Debug.LogError("Failed to InstantiateSceneObject prefab: " + string_3 + ". Client is not the MasterClient in this room.");
				return null;
			}
			GameObject value;
			if (!bool_1 || !dictionary_0.TryGetValue(string_3, out value))
			{
				value = (GameObject)Resources.Load(string_3, typeof(GameObject));
				if (bool_1)
				{
					dictionary_0.Add(string_3, value);
				}
			}
			if (value == null)
			{
				Debug.LogError("Failed to InstantiateSceneObject prefab: " + string_3 + ". Verify the Prefab is in a Resources folder (and not in a subfolder)");
				return null;
			}
			if (value.GetComponent<PhotonView>() == null)
			{
				Debug.LogError("Failed to InstantiateSceneObject prefab:" + string_3 + ". Prefab must have a PhotonView component.");
				return null;
			}
			Component[] photonViewsInChildren = value.GetPhotonViewsInChildren();
			int[] array = AllocateSceneViewIDs(photonViewsInChildren.Length);
			if (array == null)
			{
				Debug.LogError("Failed to InstantiateSceneObject prefab: " + string_3 + ". No ViewIDs are free to use. Max is: " + int_0);
				return null;
			}
			Hashtable hashtable_ = networkingPeer_0.SendInstantiate(string_3, vector3_0, quaternion_0, int_5, array, object_0, true);
			return networkingPeer_0.DoInstantiate(hashtable_, networkingPeer_0.PhotonPlayer_0, value);
		}
		Debug.LogError("Failed to InstantiateSceneObject prefab: " + string_3 + ". Client should be in a room. Current connectionStateDetailed: " + PeerState_0);
		return null;
	}

	public static int GetPing()
	{
		return networkingPeer_0.RoundTripTime;
	}

	public static void FetchServerTimestamp()
	{
		if (networkingPeer_0 != null)
		{
			networkingPeer_0.FetchServerTimestamp();
		}
	}

	public static void SendOutgoingCommands()
	{
		if (VerifyCanUseNetwork())
		{
			while (networkingPeer_0.SendOutgoingCommands())
			{
			}
		}
	}

	public static bool CloseConnection(PhotonPlayer photonPlayer_0)
	{
		if (!VerifyCanUseNetwork())
		{
			return false;
		}
		if (!PhotonPlayer_0.Boolean_0)
		{
			Debug.LogError("CloseConnection: Only the masterclient can kick another player.");
			return false;
		}
		if (photonPlayer_0 == null)
		{
			Debug.LogError("CloseConnection: No such player connected!");
			return false;
		}
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
		raiseEventOptions.int_0 = new int[1] { photonPlayer_0.Int32_0 };
		RaiseEventOptions raiseEventOptions_ = raiseEventOptions;
		return networkingPeer_0.OpRaiseEvent(203, null, true, raiseEventOptions_);
	}

	public static void Destroy(PhotonView photonView_0)
	{
		if (photonView_0 != null)
		{
			networkingPeer_0.RemoveInstantiatedGO(photonView_0.gameObject, !Boolean_10);
		}
		else
		{
			Debug.LogError("Destroy(targetPhotonView) failed, cause targetPhotonView is null.");
		}
	}

	public static void Destroy(GameObject gameObject_0)
	{
		networkingPeer_0.RemoveInstantiatedGO(gameObject_0, !Boolean_10);
	}

	public static void DestroyPlayerObjects(PhotonPlayer photonPlayer_0)
	{
		if (PhotonPlayer_0 == null)
		{
			Debug.LogError("DestroyPlayerObjects() failed, cause parameter 'targetPlayer' was null.");
		}
		DestroyPlayerObjects(photonPlayer_0.Int32_0);
	}

	public static void DestroyPlayerObjects(int int_5)
	{
		if (VerifyCanUseNetwork())
		{
			if (!PhotonPlayer_0.Boolean_0 && int_5 != PhotonPlayer_0.Int32_0)
			{
				Debug.LogError("DestroyPlayerObjects() failed, cause players can only destroy their own GameObjects. A Master Client can destroy anyone's. This is master: " + Boolean_9);
			}
			else
			{
				networkingPeer_0.DestroyPlayerObjects(int_5, false);
			}
		}
	}

	public static void DestroyAll()
	{
		if (Boolean_9)
		{
			networkingPeer_0.DestroyAll(false);
		}
		else
		{
			Debug.LogError("Couldn't call DestroyAll() as only the master client is allowed to call this.");
		}
	}

	public static void RemoveRPCs(PhotonPlayer photonPlayer_0)
	{
		if (VerifyCanUseNetwork())
		{
			if (!photonPlayer_0.bool_0 && !Boolean_9)
			{
				Debug.LogError("Error; Only the MasterClient can call RemoveRPCs for other players.");
			}
			else
			{
				networkingPeer_0.OpCleanRpcBuffer(photonPlayer_0.Int32_0);
			}
		}
	}

	public static void RemoveRPCs(PhotonView photonView_0)
	{
		if (VerifyCanUseNetwork())
		{
			networkingPeer_0.CleanRpcBufferIfMine(photonView_0);
		}
	}

	public static void RemoveRPCsInGroup(int int_5)
	{
		if (VerifyCanUseNetwork())
		{
			networkingPeer_0.RemoveRPCsInGroup(int_5);
		}
	}

	internal static void RPC(PhotonView photonView_0, string string_3, PhotonTargets photonTargets_0, params object[] object_0)
	{
		if (VerifyCanUseNetwork())
		{
			if (Room_0 == null)
			{
				Debug.LogWarning("Cannot send RPCs in Lobby! RPC dropped.");
			}
			else if (networkingPeer_0 != null)
			{
				networkingPeer_0.RPC(photonView_0, string_3, photonTargets_0, object_0);
			}
			else
			{
				Debug.LogWarning("Could not execute RPC " + string_3 + ". Possible scene loading in progress?");
			}
		}
	}

	internal static void RPC(PhotonView photonView_0, string string_3, PhotonPlayer photonPlayer_0, params object[] object_0)
	{
		if (!VerifyCanUseNetwork())
		{
			return;
		}
		if (Room_0 == null)
		{
			Debug.LogWarning("Cannot send RPCs in Lobby, only processed locally");
			return;
		}
		if (PhotonPlayer_0 == null)
		{
			Debug.LogError("Error; Sending RPC to player null! Aborted \"" + string_3 + "\"");
		}
		if (networkingPeer_0 != null)
		{
			networkingPeer_0.RPC(photonView_0, string_3, photonPlayer_0, object_0);
		}
		else
		{
			Debug.LogWarning("Could not execute RPC " + string_3 + ". Possible scene loading in progress?");
		}
	}

	public static void SetReceivingEnabled(int int_5, bool bool_8)
	{
		if (VerifyCanUseNetwork())
		{
			networkingPeer_0.SetReceivingEnabled(int_5, bool_8);
		}
	}

	public static void SetReceivingEnabled(int[] int_5, int[] int_6)
	{
		if (VerifyCanUseNetwork())
		{
			networkingPeer_0.SetReceivingEnabled(int_5, int_6);
		}
	}

	public static void SetSendingEnabled(int int_5, bool bool_8)
	{
		if (VerifyCanUseNetwork())
		{
			networkingPeer_0.SetSendingEnabled(int_5, bool_8);
		}
	}

	public static void SetSendingEnabled(int[] int_5, int[] int_6)
	{
		if (VerifyCanUseNetwork())
		{
			networkingPeer_0.SetSendingEnabled(int_5, int_6);
		}
	}

	public static void SetLevelPrefix(short short_0)
	{
		if (VerifyCanUseNetwork())
		{
			networkingPeer_0.SetLevelPrefix(short_0);
		}
	}

	private static bool VerifyCanUseNetwork()
	{
		if (Boolean_0)
		{
			return true;
		}
		Debug.LogError("Cannot send messages when not connected. Either connect to Photon OR use offline mode!");
		return false;
	}

	public static void LoadLevel(int int_5)
	{
		networkingPeer_0.SetLevelInPropsIfSynced(int_5);
		Boolean_8 = false;
		networkingPeer_0.bool_8 = true;
		Application.LoadLevel(int_5);
	}

	public static void LoadLevel(string string_3)
	{
		networkingPeer_0.SetLevelInPropsIfSynced(string_3);
		Boolean_8 = false;
		networkingPeer_0.bool_8 = true;
		Application.LoadLevel(string_3);
	}

	public static bool WebRpc(string string_3, object object_0)
	{
		return networkingPeer_0.WebRpc(string_3, object_0);
	}
}
