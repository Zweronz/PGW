using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.Lite;
using UnityEngine;

internal class NetworkingPeer : LoadbalancingPeer, IPhotonPeerListener
{
	protected internal const string string_0 = "curScn";

	protected internal string string_1;

	protected internal string string_2;

	private string string_3 = string.Empty;

	private IPhotonPeerListener iphotonPeerListener_0;

	private JoinType joinType_0;

	private bool bool_0;

	public Dictionary<int, PhotonPlayer> dictionary_0 = new Dictionary<int, PhotonPlayer>();

	public PhotonPlayer[] photonPlayer_0 = new PhotonPlayer[0];

	public PhotonPlayer[] photonPlayer_1 = new PhotonPlayer[0];

	public PhotonPlayer photonPlayer_2;

	public bool bool_1;

	public bool bool_2 = true;

	private Dictionary<Type, List<MethodInfo>> dictionary_1 = new Dictionary<Type, List<MethodInfo>>();

	public static bool bool_3 = true;

	public static Dictionary<string, GameObject> dictionary_2 = new Dictionary<string, GameObject>();

	public Dictionary<string, RoomInfo> dictionary_3 = new Dictionary<string, RoomInfo>();

	public RoomInfo[] roomInfo_0 = new RoomInfo[0];

	public bool bool_4;

	public Dictionary<int, GameObject> dictionary_4 = new Dictionary<int, GameObject>();

	private HashSet<int> hashSet_0 = new HashSet<int>();

	private HashSet<int> hashSet_1 = new HashSet<int>();

	protected internal Dictionary<int, PhotonView> dictionary_5 = new Dictionary<int, PhotonView>();

	protected internal short short_0;

	private readonly Dictionary<string, int> dictionary_6;

	public bool bool_5;

	public string string_4 = "ns.exitgamescloud.com";

	private static readonly Dictionary<ConnectionProtocol, int> dictionary_7 = new Dictionary<ConnectionProtocol, int>
	{
		{
			ConnectionProtocol.Udp,
			5058
		},
		{
			ConnectionProtocol.Tcp,
			4533
		}
	};

	private bool bool_6;

	private string[] string_5;

	private int int_0;

	private bool bool_7;

	private Dictionary<int, object[]> dictionary_8 = new Dictionary<int, object[]>();

	protected internal bool bool_8;

	[CompilerGenerated]
	private AuthenticationValues authenticationValues_0;

	[CompilerGenerated]
	private string string_6;

	[CompilerGenerated]
	private PeerState peerState_0;

	[CompilerGenerated]
	private Room room_0;

	[CompilerGenerated]
	private RoomOptions roomOptions_0;

	[CompilerGenerated]
	private TypedLobby typedLobby_0;

	[CompilerGenerated]
	private PhotonPlayer photonPlayer_3;

	[CompilerGenerated]
	private string string_7;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private TypedLobby typedLobby_1;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private int int_4;

	[CompilerGenerated]
	private ServerConnection serverConnection_0;

	[CompilerGenerated]
	private bool bool_9;

	[CompilerGenerated]
	private List<Region> list_0;

	[CompilerGenerated]
	private CloudRegionCode cloudRegionCode_0;

	protected internal string String_0
	{
		get
		{
			return string.Format("{0}_{1}", string_1, "1.28");
		}
	}

	public AuthenticationValues AuthenticationValues_0
	{
		[CompilerGenerated]
		get
		{
			return authenticationValues_0;
		}
		[CompilerGenerated]
		set
		{
			authenticationValues_0 = value;
		}
	}

	public string String_1
	{
		[CompilerGenerated]
		get
		{
			return string_6;
		}
		[CompilerGenerated]
		protected internal set
		{
			string_6 = value;
		}
	}

	public string String_2
	{
		get
		{
			return string_3;
		}
		set
		{
			if (!string.IsNullOrEmpty(value) && !value.Equals(string_3))
			{
				if (PhotonPlayer_0 != null)
				{
					PhotonPlayer_0.String_0 = value;
				}
				string_3 = value;
				if (Room_0 != null)
				{
					SendPlayerName();
				}
			}
		}
	}

	public PeerState PeerState_0
	{
		[CompilerGenerated]
		get
		{
			return peerState_0;
		}
		[CompilerGenerated]
		internal set
		{
			peerState_0 = value;
		}
	}

	public Room Room_0
	{
		get
		{
			if (Room_1 != null && Room_1.Boolean_1)
			{
				return Room_1;
			}
			return null;
		}
	}

	internal Room Room_1
	{
		[CompilerGenerated]
		get
		{
			return room_0;
		}
		[CompilerGenerated]
		set
		{
			room_0 = value;
		}
	}

	internal RoomOptions RoomOptions_0
	{
		[CompilerGenerated]
		get
		{
			return roomOptions_0;
		}
		[CompilerGenerated]
		set
		{
			roomOptions_0 = value;
		}
	}

	internal TypedLobby TypedLobby_0
	{
		[CompilerGenerated]
		get
		{
			return typedLobby_0;
		}
		[CompilerGenerated]
		set
		{
			typedLobby_0 = value;
		}
	}

	public PhotonPlayer PhotonPlayer_0
	{
		[CompilerGenerated]
		get
		{
			return photonPlayer_3;
		}
		[CompilerGenerated]
		internal set
		{
			photonPlayer_3 = value;
		}
	}

	public string String_3
	{
		[CompilerGenerated]
		get
		{
			return string_7;
		}
		[CompilerGenerated]
		internal set
		{
			string_7 = value;
		}
	}

	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		internal set
		{
			int_1 = value;
		}
	}

	public TypedLobby TypedLobby_1
	{
		[CompilerGenerated]
		get
		{
			return typedLobby_1;
		}
		[CompilerGenerated]
		set
		{
			typedLobby_1 = value;
		}
	}

	public int Int32_1
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		internal set
		{
			int_2 = value;
		}
	}

	public int Int32_2
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		internal set
		{
			int_3 = value;
		}
	}

	public int Int32_3
	{
		[CompilerGenerated]
		get
		{
			return int_4;
		}
		[CompilerGenerated]
		internal set
		{
			int_4 = value;
		}
	}

	protected internal ServerConnection ServerConnection_0
	{
		[CompilerGenerated]
		get
		{
			return serverConnection_0;
		}
		[CompilerGenerated]
		private set
		{
			serverConnection_0 = value;
		}
	}

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_9;
		}
		[CompilerGenerated]
		protected internal set
		{
			bool_9 = value;
		}
	}

	public List<Region> List_0
	{
		[CompilerGenerated]
		get
		{
			return list_0;
		}
		[CompilerGenerated]
		protected internal set
		{
			list_0 = value;
		}
	}

	public CloudRegionCode CloudRegionCode_0
	{
		[CompilerGenerated]
		get
		{
			return cloudRegionCode_0;
		}
		[CompilerGenerated]
		protected internal set
		{
			cloudRegionCode_0 = value;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return false;
		}
	}

	protected internal int Int32_4
	{
		get
		{
			return (!bool_7 && int_0 != 0) ? (Environment.TickCount - int_0) : 0;
		}
	}

	public NetworkingPeer(IPhotonPeerListener iphotonPeerListener_1, string string_8, ConnectionProtocol connectionProtocol_0)
		: base(iphotonPeerListener_1, connectionProtocol_0)
	{
		if (PhotonHandler.type_0 == null)
		{
			PhotonHandler.type_0 = typeof(PingMono);
		}
		base.Listener = this;
		TypedLobby_1 = TypedLobby.typedLobby_0;
		base.LimitOfUnreliableCommands = 40;
		iphotonPeerListener_0 = iphotonPeerListener_1;
		String_2 = string_8;
		PhotonPlayer_0 = new PhotonPlayer(true, -1, string_3);
		AddNewPlayer(PhotonPlayer_0.Int32_0, PhotonPlayer_0);
		dictionary_6 = new Dictionary<string, int>(PhotonNetwork.serverSettings_0.RpcList.Count);
		for (int i = 0; i < PhotonNetwork.serverSettings_0.RpcList.Count; i++)
		{
			string key = PhotonNetwork.serverSettings_0.RpcList[i];
			dictionary_6[key] = i;
		}
		PeerState_0 = global::PeerState.PeerCreated;
	}

	public override bool Connect(string serverAddress, string applicationName)
	{
		Debug.LogError("Avoid using this directly. Thanks.");
		return false;
	}

	public bool Connect(string string_8, ServerConnection serverConnection_1)
	{
		if (PhotonHandler.bool_1)
		{
			Debug.LogWarning("Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
			return false;
		}
		if (PhotonNetwork.PeerState_0 == global::PeerState.Disconnecting)
		{
			Debug.LogError("Connect() failed. Can't connect while disconnecting (still). Current state: " + PhotonNetwork.PeerState_0);
			return false;
		}
		bool result;
		if (result = base.Connect(string_8, string.Empty))
		{
			switch (serverConnection_1)
			{
			case ServerConnection.MasterServer:
				PeerState_0 = global::PeerState.ConnectingToMasterserver;
				break;
			case ServerConnection.GameServer:
				PeerState_0 = global::PeerState.ConnectingToGameserver;
				break;
			case ServerConnection.NameServer:
				PeerState_0 = global::PeerState.ConnectingToNameServer;
				break;
			}
		}
		return result;
	}

	public bool ConnectToNameServer()
	{
		if (PhotonHandler.bool_1)
		{
			Debug.LogWarning("Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
			return false;
		}
		Boolean_0 = true;
		CloudRegionCode_0 = CloudRegionCode.none;
		if (PeerState_0 == global::PeerState.ConnectedToNameServer)
		{
			return true;
		}
		string text = string_4;
		if (!text.Contains(":"))
		{
			int value = 0;
			dictionary_7.TryGetValue(base.UsedProtocol, out value);
			text = string.Format("{0}:{1}", text, value);
			Debug.Log("Server to connect to: " + text + " settings protocol: " + PhotonNetwork.serverSettings_0.Protocol);
		}
		if (!base.Connect(text, "ns"))
		{
			return false;
		}
		PeerState_0 = global::PeerState.ConnectingToNameServer;
		return true;
	}

	public bool ConnectToRegionMaster(CloudRegionCode cloudRegionCode_1)
	{
		if (PhotonHandler.bool_1)
		{
			Debug.LogWarning("Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
			return false;
		}
		Boolean_0 = true;
		CloudRegionCode_0 = cloudRegionCode_1;
		if (PeerState_0 == global::PeerState.ConnectedToNameServer)
		{
			return OpAuthenticate(string_2, String_0, String_2, AuthenticationValues_0, cloudRegionCode_1.ToString());
		}
		string text = string_4;
		if (!text.Contains(":"))
		{
			int value = 0;
			dictionary_7.TryGetValue(base.UsedProtocol, out value);
			text = string.Format("{0}:{1}", text, value);
		}
		if (!base.Connect(text, "ns"))
		{
			return false;
		}
		PeerState_0 = global::PeerState.ConnectingToNameServer;
		return true;
	}

	public bool GetRegions()
	{
		if (ServerConnection_0 != ServerConnection.NameServer)
		{
			return false;
		}
		bool result;
		if (result = OpGetRegions(string_2))
		{
			List_0 = null;
		}
		return result;
	}

	public override void Disconnect()
	{
		if (base.PeerState == PeerStateValue.Disconnected)
		{
			if (!PhotonHandler.bool_1)
			{
				Debug.LogWarning(string.Format("Can't execute Disconnect() while not connected. Nothing changed. State: {0}", PeerState_0));
			}
		}
		else
		{
			PeerState_0 = global::PeerState.Disconnecting;
			base.Disconnect();
		}
	}

	private void DisconnectToReconnect()
	{
		switch (ServerConnection_0)
		{
		case ServerConnection.MasterServer:
			PeerState_0 = global::PeerState.DisconnectingFromMasterserver;
			base.Disconnect();
			break;
		case ServerConnection.GameServer:
			PeerState_0 = global::PeerState.DisconnectingFromGameserver;
			base.Disconnect();
			break;
		case ServerConnection.NameServer:
			PeerState_0 = global::PeerState.DisconnectingFromNameServer;
			base.Disconnect();
			break;
		}
	}

	private void LeftLobbyCleanup()
	{
		dictionary_3 = new Dictionary<string, RoomInfo>();
		roomInfo_0 = new RoomInfo[0];
		if (bool_4)
		{
			bool_4 = false;
			SendMonoMessage(PhotonNetworkingMessage.OnLeftLobby);
		}
	}

	private void LeftRoomCleanup()
	{
		bool flag = Room_1 != null;
		bool flag2 = ((Room_1 == null) ? PhotonNetwork.Boolean_5 : Room_1.Boolean_6);
		bool_1 = false;
		Room_1 = null;
		dictionary_0 = new Dictionary<int, PhotonPlayer>();
		photonPlayer_1 = new PhotonPlayer[0];
		photonPlayer_0 = new PhotonPlayer[0];
		photonPlayer_2 = null;
		hashSet_0 = new HashSet<int>();
		hashSet_1 = new HashSet<int>();
		dictionary_3 = new Dictionary<string, RoomInfo>();
		roomInfo_0 = new RoomInfo[0];
		bool_7 = false;
		ChangeLocalID(-1);
		if (flag2)
		{
			LocalCleanupAnythingInstantiated(true);
			PhotonNetwork.list_0 = new List<int>();
		}
		if (flag)
		{
			SendMonoMessage(PhotonNetworkingMessage.OnLeftRoom);
		}
	}

	protected internal void LocalCleanupAnythingInstantiated(bool bool_10)
	{
		if (dictionary_8.Count > 0)
		{
			Debug.LogWarning("It seems some instantiation is not completed, as instantiation data is used. You should make sure instantiations are paused when calling this method. Cleaning now, despite this.");
		}
		if (bool_10)
		{
			HashSet<GameObject> hashSet = new HashSet<GameObject>(dictionary_4.Values);
			foreach (GameObject item in hashSet)
			{
				RemoveInstantiatedGO(item, true);
			}
		}
		dictionary_8.Clear();
		dictionary_4 = new Dictionary<int, GameObject>();
		PhotonNetwork.int_3 = 0;
		PhotonNetwork.int_4 = 0;
	}

	private void ReadoutProperties(ExitGames.Client.Photon.Hashtable hashtable_0, ExitGames.Client.Photon.Hashtable hashtable_1, int int_5)
	{
		if (Room_0 != null && hashtable_0 != null)
		{
			Room_0.CacheProperties(hashtable_0);
			SendMonoMessage(PhotonNetworkingMessage.OnPhotonCustomRoomPropertiesChanged, hashtable_0);
			if (PhotonNetwork.Boolean_4)
			{
				LoadLevelIfSynced();
			}
		}
		if (hashtable_1 == null || hashtable_1.Count <= 0)
		{
			return;
		}
		if (int_5 > 0)
		{
			PhotonPlayer playerWithID = GetPlayerWithID(int_5);
			if (playerWithID != null)
			{
				ExitGames.Client.Photon.Hashtable actorPropertiesForActorNr = GetActorPropertiesForActorNr(hashtable_1, int_5);
				playerWithID.InternalCacheProperties(actorPropertiesForActorNr);
				SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, playerWithID, actorPropertiesForActorNr);
			}
			return;
		}
		foreach (object key in hashtable_1.Keys)
		{
			int int_6 = (int)key;
			ExitGames.Client.Photon.Hashtable hashtable = (ExitGames.Client.Photon.Hashtable)hashtable_1[key];
			string text = (string)hashtable[byte.MaxValue];
			PhotonPlayer photonPlayer = GetPlayerWithID(int_6);
			if (photonPlayer == null)
			{
				photonPlayer = new PhotonPlayer(false, int_6, text);
				AddNewPlayer(int_6, photonPlayer);
			}
			photonPlayer.InternalCacheProperties(hashtable);
			SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, photonPlayer, hashtable);
		}
	}

	private void AddNewPlayer(int int_5, PhotonPlayer photonPlayer_4)
	{
		if (!dictionary_0.ContainsKey(int_5))
		{
			dictionary_0[int_5] = photonPlayer_4;
			RebuildPlayerListCopies();
		}
		else
		{
			Debug.LogError("Adding player twice: " + int_5);
		}
	}

	private void RemovePlayer(int int_5, PhotonPlayer photonPlayer_4)
	{
		dictionary_0.Remove(int_5);
		if (!photonPlayer_4.bool_0)
		{
			RebuildPlayerListCopies();
		}
	}

	private void RebuildPlayerListCopies()
	{
		photonPlayer_1 = new PhotonPlayer[dictionary_0.Count];
		dictionary_0.Values.CopyTo(photonPlayer_1, 0);
		List<PhotonPlayer> list = new List<PhotonPlayer>();
		PhotonPlayer[] array = photonPlayer_1;
		foreach (PhotonPlayer photonPlayer in array)
		{
			if (!photonPlayer.bool_0)
			{
				list.Add(photonPlayer);
			}
		}
		photonPlayer_0 = list.ToArray();
	}

	private void ResetPhotonViewsOnSerialize()
	{
		foreach (PhotonView value in dictionary_5.Values)
		{
			value.object_1 = null;
		}
	}

	private void HandleEventLeave(int int_5)
	{
		if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
		{
			Debug.Log("HandleEventLeave for player ID: " + int_5);
		}
		if (int_5 >= 0 && dictionary_0.ContainsKey(int_5))
		{
			PhotonPlayer playerWithID = GetPlayerWithID(int_5);
			if (playerWithID == null)
			{
				Debug.LogError("HandleEventLeave for player ID: " + int_5 + " has no PhotonPlayer!");
			}
			CheckMasterClient(int_5);
			if (Room_0 != null && Room_0.Boolean_6)
			{
				DestroyPlayerObjects(int_5, true);
			}
			RemovePlayer(int_5, playerWithID);
			SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerDisconnected, playerWithID);
		}
		else
		{
			Debug.LogError(string.Format("Received event Leave for unknown player ID: {0}", int_5));
		}
	}

	private void CheckMasterClient(int int_5)
	{
		bool flag = photonPlayer_2 != null && photonPlayer_2.Int32_0 == int_5;
		bool flag2;
		if ((flag2 = int_5 > 0) && !flag)
		{
			return;
		}
		if (dictionary_0.Count <= 1)
		{
			photonPlayer_2 = PhotonPlayer_0;
		}
		else
		{
			int num = int.MaxValue;
			foreach (int key in dictionary_0.Keys)
			{
				if (key < num && key != int_5)
				{
					num = key;
				}
			}
			photonPlayer_2 = dictionary_0[num];
		}
		if (flag2)
		{
			SendMonoMessage(PhotonNetworkingMessage.OnMasterClientSwitched, photonPlayer_2);
		}
	}

	private static int ReturnLowestPlayerId(PhotonPlayer[] photonPlayer_4, int int_5)
	{
		if (photonPlayer_4 != null && photonPlayer_4.Length != 0)
		{
			int num = int.MaxValue;
			foreach (PhotonPlayer photonPlayer in photonPlayer_4)
			{
				if (photonPlayer.Int32_0 != int_5 && photonPlayer.Int32_0 < num)
				{
					num = photonPlayer.Int32_0;
				}
			}
			return num;
		}
		return -1;
	}

	protected internal bool SetMasterClient(int int_5, bool bool_10)
	{
		if (photonPlayer_2 != null && photonPlayer_2.Int32_0 != int_5 && dictionary_0.ContainsKey(int_5))
		{
			if (bool_10 && !OpRaiseEvent(208, new ExitGames.Client.Photon.Hashtable { 
			{
				(byte)1,
				int_5
			} }, true, null))
			{
				return false;
			}
			bool_1 = true;
			photonPlayer_2 = dictionary_0[int_5];
			SendMonoMessage(PhotonNetworkingMessage.OnMasterClientSwitched, photonPlayer_2);
			return true;
		}
		return false;
	}

	private ExitGames.Client.Photon.Hashtable GetActorPropertiesForActorNr(ExitGames.Client.Photon.Hashtable hashtable_0, int int_5)
	{
		if (hashtable_0.ContainsKey(int_5))
		{
			return (ExitGames.Client.Photon.Hashtable)hashtable_0[int_5];
		}
		return hashtable_0;
	}

	private PhotonPlayer GetPlayerWithID(int int_5)
	{
		if (dictionary_0 != null && dictionary_0.ContainsKey(int_5))
		{
			return dictionary_0[int_5];
		}
		return null;
	}

	private void SendPlayerName()
	{
		if (PeerState_0 == global::PeerState.Joining)
		{
			bool_0 = true;
		}
		else if (PhotonPlayer_0 != null)
		{
			PhotonPlayer_0.String_0 = String_2;
			ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
			hashtable[byte.MaxValue] = String_2;
			if (PhotonPlayer_0.Int32_0 > 0)
			{
				OpSetPropertiesOfActor(PhotonPlayer_0.Int32_0, hashtable, true, 0);
				bool_0 = false;
			}
		}
	}

	private void GameEnteredOnGameServer(OperationResponse operationResponse_0)
	{
		if (operationResponse_0.ReturnCode != 0)
		{
			switch (operationResponse_0.OperationCode)
			{
			case 225:
				if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
				{
					Debug.Log("Join failed on GameServer. Changing back to MasterServer. Msg: " + operationResponse_0.DebugMessage);
					if (operationResponse_0.ReturnCode == 32758)
					{
						Debug.Log("Most likely the game became empty during the switch to GameServer.");
					}
				}
				SendMonoMessage(PhotonNetworkingMessage.OnPhotonRandomJoinFailed, operationResponse_0.ReturnCode, operationResponse_0.DebugMessage);
				break;
			case 226:
				if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
				{
					Debug.Log("Join failed on GameServer. Changing back to MasterServer. Msg: " + operationResponse_0.DebugMessage);
					if (operationResponse_0.ReturnCode == 32758)
					{
						Debug.Log("Most likely the game became empty during the switch to GameServer.");
					}
				}
				SendMonoMessage(PhotonNetworkingMessage.OnPhotonJoinRoomFailed, operationResponse_0.ReturnCode, operationResponse_0.DebugMessage);
				break;
			case 227:
				if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
				{
					Debug.Log("Create failed on GameServer. Changing back to MasterServer. Msg: " + operationResponse_0.DebugMessage);
				}
				SendMonoMessage(PhotonNetworkingMessage.OnPhotonCreateRoomFailed, operationResponse_0.ReturnCode, operationResponse_0.DebugMessage);
				break;
			}
			DisconnectToReconnect();
		}
		else
		{
			PeerState_0 = global::PeerState.Joined;
			Room_1.Boolean_1 = true;
			ExitGames.Client.Photon.Hashtable hashtable_ = (ExitGames.Client.Photon.Hashtable)operationResponse_0[249];
			ExitGames.Client.Photon.Hashtable hashtable_2 = (ExitGames.Client.Photon.Hashtable)operationResponse_0[248];
			ReadoutProperties(hashtable_2, hashtable_, 0);
			int int_ = (int)operationResponse_0[254];
			ChangeLocalID(int_);
			CheckMasterClient(-1);
			if (bool_0)
			{
				SendPlayerName();
			}
			switch (operationResponse_0.OperationCode)
			{
			case 227:
				SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom);
				break;
			case 225:
			case 226:
				break;
			}
		}
	}

	private ExitGames.Client.Photon.Hashtable GetLocalActorProperties()
	{
		if (PhotonNetwork.PhotonPlayer_0 != null)
		{
			return PhotonNetwork.PhotonPlayer_0.Hashtable_1;
		}
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable[byte.MaxValue] = String_2;
		return hashtable;
	}

	public void ChangeLocalID(int int_5)
	{
		if (PhotonPlayer_0 == null)
		{
			Debug.LogWarning(string.Format("Local actor is null or not in mActors! mLocalActor: {0} mActors==null: {1} newID: {2}", PhotonPlayer_0, dictionary_0 == null, int_5));
		}
		if (dictionary_0.ContainsKey(PhotonPlayer_0.Int32_0))
		{
			dictionary_0.Remove(PhotonPlayer_0.Int32_0);
		}
		PhotonPlayer_0.InternalChangeLocalID(int_5);
		dictionary_0[PhotonPlayer_0.Int32_0] = PhotonPlayer_0;
		RebuildPlayerListCopies();
	}

	public bool OpCreateGame(string string_8, RoomOptions roomOptions_1, TypedLobby typedLobby_2)
	{
		bool flag;
		if (!(flag = ServerConnection_0 == ServerConnection.GameServer))
		{
			RoomOptions_0 = roomOptions_1;
			Room_1 = new Room(string_8, roomOptions_1);
			TypedLobby_0 = typedLobby_2 ?? ((!bool_4) ? null : TypedLobby_1);
		}
		joinType_0 = JoinType.CreateGame;
		return base.OpCreateRoom(string_8, roomOptions_1, TypedLobby_0, GetLocalActorProperties(), flag);
	}

	public bool OpJoinRoom(string string_8, RoomOptions roomOptions_1, TypedLobby typedLobby_2, bool bool_10)
	{
		bool flag;
		if (!(flag = ServerConnection_0 == ServerConnection.GameServer))
		{
			RoomOptions_0 = roomOptions_1;
			Room_1 = new Room(string_8, roomOptions_1);
			TypedLobby_0 = null;
			if (bool_10)
			{
				TypedLobby_0 = typedLobby_2 ?? ((!bool_4) ? null : TypedLobby_1);
			}
		}
		joinType_0 = ((!bool_10) ? JoinType.JoinGame : JoinType.JoinOrCreateOnDemand);
		return base.OpJoinRoom(string_8, roomOptions_1, TypedLobby_0, bool_10, GetLocalActorProperties(), flag);
	}

	public override bool OpJoinRandomRoom(ExitGames.Client.Photon.Hashtable hashtable_0, byte byte_0, ExitGames.Client.Photon.Hashtable hashtable_1, MatchmakingMode matchmakingMode_0, TypedLobby typedLobby_2, string string_8)
	{
		Room_1 = new Room(null, null);
		TypedLobby_0 = null;
		joinType_0 = JoinType.JoinRandomGame;
		return base.OpJoinRandomRoom(hashtable_0, byte_0, hashtable_1, matchmakingMode_0, typedLobby_2, string_8);
	}

	public virtual bool OpLeave()
	{
		if (PeerState_0 != global::PeerState.Joined)
		{
			Debug.LogWarning("Not sending leave operation. State is not 'Joined': " + PeerState_0);
			return false;
		}
		return OpCustom(254, null, true, 0);
	}

	public override bool OpRaiseEvent(byte byte_0, object object_0, bool bool_10, RaiseEventOptions raiseEventOptions_0)
	{
		if (PhotonNetwork.Boolean_3)
		{
			return false;
		}
		return base.OpRaiseEvent(byte_0, object_0, bool_10, raiseEventOptions_0);
	}

	public void DebugReturn(DebugLevel level, string message)
	{
		iphotonPeerListener_0.DebugReturn(level, message);
	}

	public void OnOperationResponse(OperationResponse operationResponse)
	{
		if (PhotonNetwork.networkingPeer_0.PeerState_0 == global::PeerState.Disconnecting)
		{
			if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
			{
				Debug.Log("OperationResponse ignored while disconnecting. Code: " + operationResponse.OperationCode);
			}
			return;
		}
		if (operationResponse.ReturnCode == 0)
		{
			if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
			{
				Debug.Log(operationResponse.ToString());
			}
		}
		else if (operationResponse.ReturnCode == -3)
		{
			Debug.LogError("Operation " + operationResponse.OperationCode + " could not be executed (yet). Wait for state JoinedLobby or ConnectedToMaster and their callbacks before calling operations. WebRPCs need a server-side configuration. Enum OperationCode helps identify the operation.");
		}
		else if (operationResponse.ReturnCode == 32752)
		{
			Debug.LogError("Operation " + operationResponse.OperationCode + " failed in a server-side plugin. Check the configuration in the Dashboard. Message from server-plugin: " + operationResponse.DebugMessage);
		}
		else if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
		{
			Debug.LogError("Operation failed: " + operationResponse.ToStringFull() + " Server: " + ServerConnection_0);
		}
		if (operationResponse.Parameters.ContainsKey(221))
		{
			if (AuthenticationValues_0 == null)
			{
				AuthenticationValues_0 = new AuthenticationValues();
			}
			AuthenticationValues_0.string_1 = operationResponse[221] as string;
		}
		switch (operationResponse.OperationCode)
		{
		case 219:
			SendMonoMessage(PhotonNetworkingMessage.OnWebRpcResponse, operationResponse);
			break;
		case 220:
		{
			if (operationResponse.ReturnCode == short.MaxValue)
			{
				Debug.LogError(string.Format("The appId this client sent is unknown on the server (Cloud). Check settings. If using the Cloud, check account."));
				SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, DisconnectCause.InvalidAuthentication);
				PeerState_0 = global::PeerState.Disconnecting;
				Disconnect();
				return;
			}
			string[] array = operationResponse[210] as string[];
			string[] array2 = operationResponse[230] as string[];
			if (array != null && array2 != null && array.Length == array2.Length)
			{
				List_0 = new List<Region>(array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = array[i];
					if (!string.IsNullOrEmpty(text2))
					{
						text2 = text2.ToLower();
						CloudRegionCode cloudRegionCode = Region.Parse(text2);
						List_0.Add(new Region
						{
							cloudRegionCode_0 = cloudRegionCode,
							string_0 = array2[i]
						});
					}
				}
				if (PhotonNetwork.serverSettings_0.HostType == ServerSettings.HostingOption.BestRegion)
				{
					PhotonHandler.PingAvailableRegionsAndConnectToBest();
				}
			}
			else
			{
				Debug.LogError("The region arrays from Name Server are not ok. Must be non-null and same length.");
			}
			break;
		}
		case 222:
		{
			bool[] array3 = operationResponse[1] as bool[];
			string[] array4 = operationResponse[2] as string[];
			if (array3 != null && array4 != null && string_5 != null && array3.Length == string_5.Length)
			{
				List<FriendInfo> list = new List<FriendInfo>(string_5.Length);
				for (int j = 0; j < string_5.Length; j++)
				{
					FriendInfo friendInfo = new FriendInfo();
					friendInfo.String_0 = string_5[j];
					friendInfo.String_1 = array4[j];
					friendInfo.Boolean_0 = array3[j];
					list.Insert(j, friendInfo);
				}
				PhotonNetwork.List_0 = list;
			}
			else
			{
				Debug.LogError("FindFriends failed to apply the result, as a required value wasn't provided or the friend list length differed from result.");
			}
			string_5 = null;
			bool_7 = false;
			int_0 = Environment.TickCount;
			if (int_0 == 0)
			{
				int_0 = 1;
			}
			SendMonoMessage(PhotonNetworkingMessage.OnUpdatedFriendList);
			break;
		}
		default:
			Debug.LogWarning(string.Format("OperationResponse unhandled: {0}", operationResponse.ToString()));
			break;
		case 251:
		{
			ExitGames.Client.Photon.Hashtable hashtable_ = (ExitGames.Client.Photon.Hashtable)operationResponse[249];
			ExitGames.Client.Photon.Hashtable hashtable_2 = (ExitGames.Client.Photon.Hashtable)operationResponse[248];
			ReadoutProperties(hashtable_2, hashtable_, 0);
			break;
		}
		case 254:
			DisconnectToReconnect();
			break;
		case 225:
			if (operationResponse.ReturnCode != 0)
			{
				if (operationResponse.ReturnCode == 32760)
				{
					if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Full)
					{
						Debug.Log("JoinRandom failed: No open game. Calling: OnPhotonRandomJoinFailed() and staying on master server.");
					}
				}
				else if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
				{
					Debug.LogWarning(string.Format("JoinRandom failed: {0}.", operationResponse.ToStringFull()));
				}
				SendMonoMessage(PhotonNetworkingMessage.OnPhotonRandomJoinFailed);
			}
			else
			{
				string text = (string)operationResponse[byte.MaxValue];
				Room_1.String_1 = text;
				String_3 = (string)operationResponse[230];
				DisconnectToReconnect();
			}
			break;
		case 226:
			if (ServerConnection_0 != ServerConnection.GameServer)
			{
				if (operationResponse.ReturnCode != 0)
				{
					if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
					{
						Debug.Log(string.Format("JoinRoom failed (room maybe closed by now). Client stays on masterserver: {0}. State: {1}", operationResponse.ToStringFull(), PeerState_0));
					}
					SendMonoMessage(PhotonNetworkingMessage.OnPhotonJoinRoomFailed);
				}
				else
				{
					String_3 = (string)operationResponse[230];
					DisconnectToReconnect();
				}
			}
			else
			{
				GameEnteredOnGameServer(operationResponse);
			}
			break;
		case 227:
		{
			if (ServerConnection_0 == ServerConnection.GameServer)
			{
				GameEnteredOnGameServer(operationResponse);
				break;
			}
			if (operationResponse.ReturnCode != 0)
			{
				if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
				{
					Debug.LogWarning(string.Format("CreateRoom failed, client stays on masterserver: {0}.", operationResponse.ToStringFull()));
				}
				SendMonoMessage(PhotonNetworkingMessage.OnPhotonCreateRoomFailed);
				break;
			}
			string value = (string)operationResponse[byte.MaxValue];
			if (!string.IsNullOrEmpty(value))
			{
				Room_1.String_1 = value;
			}
			String_3 = (string)operationResponse[230];
			DisconnectToReconnect();
			break;
		}
		case 228:
			PeerState_0 = global::PeerState.Authenticated;
			LeftLobbyCleanup();
			break;
		case 229:
			PeerState_0 = global::PeerState.JoinedLobby;
			bool_4 = true;
			SendMonoMessage(PhotonNetworkingMessage.OnJoinedLobby);
			break;
		case 230:
			if (operationResponse.ReturnCode != 0)
			{
				if (operationResponse.ReturnCode == -2)
				{
					Debug.LogError(string.Format("If you host Photon yourself, make sure to start the 'Instance LoadBalancing' " + base.ServerAddress));
				}
				else if (operationResponse.ReturnCode == short.MaxValue)
				{
					Debug.LogError(string.Format("The appId this client sent is unknown on the server (Cloud). Check settings. If using the Cloud, check account."));
					SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, DisconnectCause.InvalidAuthentication);
				}
				else if (operationResponse.ReturnCode == 32755)
				{
					Debug.LogError(string.Format("Custom Authentication failed (either due to user-input or configuration or AuthParameter string format). Calling: OnCustomAuthenticationFailed()"));
					SendMonoMessage(PhotonNetworkingMessage.OnCustomAuthenticationFailed, operationResponse.DebugMessage);
				}
				else
				{
					Debug.LogError(string.Format("Authentication failed: '{0}' Code: {1}", operationResponse.DebugMessage, operationResponse.ReturnCode));
				}
				PeerState_0 = global::PeerState.Disconnecting;
				Disconnect();
				if (operationResponse.ReturnCode == 32757)
				{
					if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
					{
						Debug.LogWarning(string.Format("Currently, the limit of users is reached for this title. Try again later. Disconnecting"));
					}
					SendMonoMessage(PhotonNetworkingMessage.OnPhotonMaxCccuReached);
					SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, DisconnectCause.MaxCcuReached);
				}
				else if (operationResponse.ReturnCode == 32756)
				{
					if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
					{
						Debug.LogError(string.Format("The used master server address is not available with the subscription currently used. Got to Photon Cloud Dashboard or change URL. Disconnecting."));
					}
					SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, DisconnectCause.InvalidRegion);
				}
				else if (operationResponse.ReturnCode == 32753)
				{
					if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
					{
						Debug.LogError(string.Format("The authentication ticket expired. You need to connect (and authenticate) again. Disconnecting."));
					}
					SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, DisconnectCause.AuthenticationTicketExpired);
				}
			}
			else if (ServerConnection_0 == ServerConnection.NameServer)
			{
				String_1 = operationResponse[230] as string;
				DisconnectToReconnect();
			}
			else if (ServerConnection_0 == ServerConnection.MasterServer)
			{
				if (PhotonNetwork.Boolean_6)
				{
					PeerState_0 = global::PeerState.Authenticated;
					OpJoinLobby(TypedLobby_1);
				}
				else
				{
					PeerState_0 = global::PeerState.ConnectedToMaster;
					SendMonoMessage(PhotonNetworkingMessage.OnConnectedToMaster);
				}
			}
			else
			{
				if (ServerConnection_0 != ServerConnection.GameServer)
				{
					break;
				}
				PeerState_0 = global::PeerState.Joining;
				if (joinType_0 != JoinType.JoinGame && joinType_0 != JoinType.JoinRandomGame && joinType_0 != JoinType.JoinOrCreateOnDemand)
				{
					if (joinType_0 == JoinType.CreateGame)
					{
						OpCreateGame(Room_1.String_1, RoomOptions_0, TypedLobby_0);
					}
				}
				else
				{
					OpJoinRoom(Room_1.String_1, RoomOptions_0, TypedLobby_0, joinType_0 == JoinType.JoinOrCreateOnDemand);
				}
			}
			break;
		case 252:
		case 253:
			break;
		}
		iphotonPeerListener_0.OnOperationResponse(operationResponse);
	}

	public override bool OpFindFriends(string[] string_8)
	{
		if (bool_7)
		{
			return false;
		}
		string_5 = string_8;
		bool_7 = true;
		return base.OpFindFriends(string_8);
	}

	public void OnStatusChanged(StatusCode statusCode)
	{
		if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
		{
			Debug.Log(string.Format("OnStatusChanged: {0}", statusCode.ToString()));
		}
		switch (statusCode)
		{
		case StatusCode.SecurityExceptionOnConnect:
		case StatusCode.ExceptionOnConnect:
		{
			PeerState_0 = global::PeerState.PeerCreated;
			if (AuthenticationValues_0 != null)
			{
				AuthenticationValues_0.string_1 = null;
			}
			DisconnectCause disconnectCause = (DisconnectCause)statusCode;
			SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, disconnectCause);
			break;
		}
		case StatusCode.Connect:
			if (PeerState_0 == global::PeerState.ConnectingToNameServer)
			{
				if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Full)
				{
					Debug.Log("Connected to NameServer.");
				}
				ServerConnection_0 = ServerConnection.NameServer;
				if (AuthenticationValues_0 != null)
				{
					AuthenticationValues_0.string_1 = null;
				}
			}
			if (PeerState_0 == global::PeerState.ConnectingToGameserver)
			{
				if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Full)
				{
					Debug.Log("Connected to gameserver.");
				}
				ServerConnection_0 = ServerConnection.GameServer;
				PeerState_0 = global::PeerState.ConnectedToGameserver;
			}
			if (PeerState_0 == global::PeerState.ConnectingToMasterserver)
			{
				if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Full)
				{
					Debug.Log("Connected to masterserver.");
				}
				ServerConnection_0 = ServerConnection.MasterServer;
				PeerState_0 = global::PeerState.ConnectedToMaster;
				if (bool_5)
				{
					bool_5 = false;
					SendMonoMessage(PhotonNetworkingMessage.OnConnectedToPhoton);
				}
			}
			EstablishEncryption();
			if (Boolean_1)
			{
				bool_6 = OpAuthenticate(string_2, String_0, String_2, AuthenticationValues_0, CloudRegionCode_0.ToString());
				if (bool_6)
				{
					PeerState_0 = global::PeerState.Authenticating;
				}
			}
			break;
		case StatusCode.Disconnect:
			bool_6 = false;
			bool_7 = false;
			if (ServerConnection_0 == ServerConnection.GameServer)
			{
				LeftRoomCleanup();
			}
			if (ServerConnection_0 == ServerConnection.MasterServer)
			{
				LeftLobbyCleanup();
			}
			if (PeerState_0 == global::PeerState.DisconnectingFromMasterserver)
			{
				if (Connect(String_3, ServerConnection.GameServer))
				{
					PeerState_0 = global::PeerState.ConnectingToGameserver;
				}
			}
			else if (PeerState_0 != global::PeerState.DisconnectingFromGameserver && PeerState_0 != global::PeerState.DisconnectingFromNameServer)
			{
				if (AuthenticationValues_0 != null)
				{
					AuthenticationValues_0.string_1 = null;
				}
				PeerState_0 = global::PeerState.PeerCreated;
				SendMonoMessage(PhotonNetworkingMessage.OnDisconnectedFromPhoton);
			}
			else if (Connect(String_1, ServerConnection.MasterServer))
			{
				PeerState_0 = global::PeerState.ConnectingToMasterserver;
			}
			break;
		case StatusCode.Exception:
			if (bool_5)
			{
				Debug.LogError("Exception while connecting to: " + base.ServerAddress + ". Check if the server is available.");
				if (base.ServerAddress == null || base.ServerAddress.StartsWith("127.0.0.1"))
				{
					Debug.LogWarning("The server address is 127.0.0.1 (localhost): Make sure the server is running on this machine. Android and iOS emulators have their own localhost.");
					if (base.ServerAddress == String_3)
					{
						Debug.LogWarning("This might be a misconfiguration in the game server config. You need to edit it to a (public) address.");
					}
				}
				PeerState_0 = global::PeerState.PeerCreated;
				DisconnectCause disconnectCause = (DisconnectCause)statusCode;
				SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, disconnectCause);
			}
			else
			{
				PeerState_0 = global::PeerState.PeerCreated;
				DisconnectCause disconnectCause = (DisconnectCause)statusCode;
				SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, disconnectCause);
			}
			Disconnect();
			break;
		case StatusCode.QueueIncomingReliableWarning:
		case StatusCode.QueueIncomingUnreliableWarning:
			Debug.Log(string.Concat(statusCode, ". This client buffers many incoming messages. This is OK temporarily. With lots of these warnings, check if you send too much or execute messages too slow. ", (!PhotonNetwork.Boolean_8) ? "Your isMessageQueueRunning is false. This can cause the issue temporarily." : string.Empty));
			break;
		case StatusCode.ExceptionOnReceive:
		case StatusCode.TimeoutDisconnect:
		case StatusCode.DisconnectByServer:
		case StatusCode.DisconnectByServerUserLimit:
		case StatusCode.DisconnectByServerLogic:
			if (bool_5)
			{
				Debug.LogWarning(string.Concat(statusCode, " while connecting to: ", base.ServerAddress, ". Check if the server is available."));
				DisconnectCause disconnectCause = (DisconnectCause)statusCode;
				SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, disconnectCause);
			}
			else
			{
				DisconnectCause disconnectCause = (DisconnectCause)statusCode;
				SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, disconnectCause);
			}
			if (AuthenticationValues_0 != null)
			{
				AuthenticationValues_0.string_1 = null;
			}
			Disconnect();
			break;
		default:
			Debug.LogError("Received unknown status code: " + statusCode);
			break;
		case StatusCode.EncryptionEstablished:
			if (ServerConnection_0 == ServerConnection.NameServer)
			{
				PeerState_0 = global::PeerState.ConnectedToNameServer;
				if (!bool_6 && CloudRegionCode_0 == CloudRegionCode.none)
				{
					OpGetRegions(string_2);
				}
			}
			if (!bool_6 && (!Boolean_0 || CloudRegionCode_0 != CloudRegionCode.none))
			{
				bool_6 = OpAuthenticate(string_2, String_0, String_2, AuthenticationValues_0, CloudRegionCode_0.ToString());
				if (bool_6)
				{
					PeerState_0 = global::PeerState.Authenticating;
				}
			}
			break;
		case StatusCode.EncryptionFailedToEstablish:
			Debug.LogError(string.Concat("Encryption wasn't established: ", statusCode, ". Going to authenticate anyways."));
			OpAuthenticate(string_2, String_0, String_2, AuthenticationValues_0, CloudRegionCode_0.ToString());
			break;
		case StatusCode.QueueOutgoingReliableWarning:
		case StatusCode.QueueOutgoingUnreliableWarning:
		case StatusCode.SendError:
		case StatusCode.QueueOutgoingAcksWarning:
		case StatusCode.QueueSentWarning:
			break;
		}
		iphotonPeerListener_0.OnStatusChanged(statusCode);
	}

	public void OnEvent(EventData eventData)
	{
		if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
		{
			Debug.Log(string.Format("OnEvent: {0}", eventData.ToString()));
		}
		int num = -1;
		PhotonPlayer photonPlayer = null;
		if (eventData.Parameters.ContainsKey(254))
		{
			num = (int)eventData[254];
			if (dictionary_0.ContainsKey(num))
			{
				photonPlayer = dictionary_0[num];
			}
		}
		switch (eventData.Code)
		{
		case 200:
			ExecuteRPC(eventData[245] as ExitGames.Client.Photon.Hashtable, photonPlayer);
			break;
		case 202:
			DoInstantiate((ExitGames.Client.Photon.Hashtable)eventData[245], photonPlayer, null);
			break;
		case 203:
			if (photonPlayer != null && photonPlayer.Boolean_0)
			{
				PhotonNetwork.LeaveRoom();
			}
			else
			{
				Debug.LogError(string.Concat("Error: Someone else(", photonPlayer, ") then the masterserver requests a disconnect!"));
			}
			break;
		case 204:
		{
			ExitGames.Client.Photon.Hashtable hashtable = (ExitGames.Client.Photon.Hashtable)eventData[245];
			int num3 = (int)hashtable[(byte)0];
			GameObject value = null;
			dictionary_4.TryGetValue(num3, out value);
			if (!(value == null) && photonPlayer != null)
			{
				RemoveInstantiatedGO(value, true);
			}
			else if ((int)base.DebugOut >= 1)
			{
				Debug.LogError(string.Concat("Can't execute received Destroy request for view ID=", num3, " as GO can't be found. From player/actorNr: ", num, " GO to destroy=", value, "  originating Player=", photonPlayer));
			}
			break;
		}
		case 226:
			Int32_3 = (int)eventData[229];
			Int32_1 = (int)eventData[227];
			Int32_2 = (int)eventData[228];
			break;
		default:
			if (eventData.Code < 200 && PhotonNetwork.eventCallback_0 != null)
			{
				object object_ = eventData[245];
				PhotonNetwork.eventCallback_0(eventData.Code, object_, num);
			}
			else
			{
				Debug.LogError("Error. Unhandled event: " + eventData);
			}
			break;
		case 253:
		{
			int num4 = (int)eventData[253];
			ExitGames.Client.Photon.Hashtable hashtable_ = null;
			ExitGames.Client.Photon.Hashtable hashtable_2 = null;
			if (num4 == 0)
			{
				hashtable_ = (ExitGames.Client.Photon.Hashtable)eventData[251];
			}
			else
			{
				hashtable_2 = (ExitGames.Client.Photon.Hashtable)eventData[251];
			}
			ReadoutProperties(hashtable_, hashtable_2, num4);
			break;
		}
		case 254:
			HandleEventLeave(num);
			break;
		case byte.MaxValue:
		{
			ExitGames.Client.Photon.Hashtable hashtable_3 = (ExitGames.Client.Photon.Hashtable)eventData[249];
			if (photonPlayer == null)
			{
				bool flag = PhotonPlayer_0.Int32_0 == num;
				AddNewPlayer(num, new PhotonPlayer(flag, num, hashtable_3));
				ResetPhotonViewsOnSerialize();
			}
			if (num == PhotonPlayer_0.Int32_0)
			{
				int[] array = (int[])eventData[252];
				int[] array2 = array;
				foreach (int num7 in array2)
				{
					if (PhotonPlayer_0.Int32_0 != num7 && !dictionary_0.ContainsKey(num7))
					{
						AddNewPlayer(num7, new PhotonPlayer(false, num7, string.Empty));
					}
				}
				if (joinType_0 == JoinType.JoinOrCreateOnDemand && PhotonPlayer_0.Int32_0 == 1)
				{
					SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom);
				}
				SendMonoMessage(PhotonNetworkingMessage.OnJoinedRoom);
			}
			else
			{
				SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerConnected, dictionary_0[num]);
			}
			break;
		}
		case 228:
			if (eventData.Parameters.ContainsKey(223))
			{
				Int32_0 = (int)eventData[223];
			}
			else
			{
				Debug.LogError("Event QueueState must contain position!");
			}
			if (Int32_0 == 0)
			{
				if (PhotonNetwork.Boolean_6)
				{
					PeerState_0 = global::PeerState.Authenticated;
					OpJoinLobby(TypedLobby_1);
				}
				else
				{
					PeerState_0 = global::PeerState.ConnectedToMaster;
					SendMonoMessage(PhotonNetworkingMessage.OnConnectedToMaster);
				}
			}
			break;
		case 229:
		{
			ExitGames.Client.Photon.Hashtable hashtable4 = (ExitGames.Client.Photon.Hashtable)eventData[222];
			foreach (DictionaryEntry item in hashtable4)
			{
				string key2 = (string)item.Key;
				RoomInfo roomInfo = new RoomInfo(key2, (ExitGames.Client.Photon.Hashtable)item.Value);
				if (roomInfo.Boolean_0)
				{
					dictionary_3.Remove(key2);
				}
				else
				{
					dictionary_3[key2] = roomInfo;
				}
			}
			roomInfo_0 = new RoomInfo[dictionary_3.Count];
			dictionary_3.Values.CopyTo(roomInfo_0, 0);
			SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate);
			break;
		}
		case 230:
		{
			dictionary_3 = new Dictionary<string, RoomInfo>();
			ExitGames.Client.Photon.Hashtable hashtable3 = (ExitGames.Client.Photon.Hashtable)eventData[222];
			foreach (DictionaryEntry item2 in hashtable3)
			{
				string key = (string)item2.Key;
				dictionary_3[key] = new RoomInfo(key, (ExitGames.Client.Photon.Hashtable)item2.Value);
			}
			roomInfo_0 = new RoomInfo[dictionary_3.Count];
			dictionary_3.Values.CopyTo(roomInfo_0, 0);
			SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate);
			break;
		}
		case 201:
		case 206:
		{
			ExitGames.Client.Photon.Hashtable hashtable2 = (ExitGames.Client.Photon.Hashtable)eventData[245];
			int int_2 = (int)hashtable2[(byte)0];
			short short_ = -1;
			short num5 = 1;
			if (hashtable2.ContainsKey((byte)1))
			{
				short_ = (short)hashtable2[(byte)1];
				num5 = 2;
			}
			for (short num6 = num5; num6 < hashtable2.Count; num6++)
			{
				OnSerializeRead(hashtable2[num6] as ExitGames.Client.Photon.Hashtable, photonPlayer, int_2, short_);
			}
			break;
		}
		case 207:
		{
			ExitGames.Client.Photon.Hashtable hashtable = (ExitGames.Client.Photon.Hashtable)eventData[245];
			int num2 = (int)hashtable[(byte)0];
			if (num2 >= 0)
			{
				DestroyPlayerObjects(num2, true);
				break;
			}
			if ((int)base.DebugOut >= 3)
			{
				Debug.Log("Ev DestroyAll! By PlayerId: " + num);
			}
			DestroyAll(true);
			break;
		}
		case 208:
		{
			ExitGames.Client.Photon.Hashtable hashtable = (ExitGames.Client.Photon.Hashtable)eventData[245];
			int int_ = (int)hashtable[(byte)1];
			SetMasterClient(int_, false);
			break;
		}
		}
		iphotonPeerListener_0.OnEvent(eventData);
	}

	public static void SendMonoMessage(PhotonNetworkingMessage photonNetworkingMessage_0, params object[] object_0)
	{
		HashSet<GameObject> hashSet;
		if (PhotonNetwork.hashSet_0 != null)
		{
			hashSet = PhotonNetwork.hashSet_0;
		}
		else
		{
			hashSet = new HashSet<GameObject>();
			Component[] array = (Component[])UnityEngine.Object.FindObjectsOfType(typeof(MonoBehaviour));
			for (int i = 0; i < array.Length; i++)
			{
				hashSet.Add(array[i].gameObject);
			}
		}
		string methodName = photonNetworkingMessage_0.ToString();
		foreach (GameObject item in hashSet)
		{
			if (object_0 != null && object_0.Length == 1)
			{
				item.SendMessage(methodName, object_0[0], SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				item.SendMessage(methodName, object_0, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public void ExecuteRPC(ExitGames.Client.Photon.Hashtable hashtable_0, PhotonPlayer photonPlayer_4)
	{
		if (hashtable_0 != null && hashtable_0.ContainsKey((byte)0))
		{
			int num = (int)hashtable_0[(byte)0];
			int num2 = 0;
			if (hashtable_0.ContainsKey((byte)1))
			{
				num2 = (short)hashtable_0[(byte)1];
			}
			string text;
			if (hashtable_0.ContainsKey((byte)5))
			{
				int num3 = (byte)hashtable_0[(byte)5];
				if (num3 > PhotonNetwork.serverSettings_0.RpcList.Count - 1)
				{
					Debug.LogError("Could not find RPC with index: " + num3 + ". Going to ignore! Check PhotonServerSettings.RpcList");
					return;
				}
				text = PhotonNetwork.serverSettings_0.RpcList[num3];
			}
			else
			{
				text = (string)hashtable_0[(byte)3];
			}
			object[] array = null;
			if (hashtable_0.ContainsKey((byte)4))
			{
				array = (object[])hashtable_0[(byte)4];
			}
			if (array == null)
			{
				array = new object[0];
			}
			PhotonView photonView = GetPhotonView(num);
			if (photonView == null)
			{
				int num4 = num / PhotonNetwork.int_0;
				bool flag = num4 == PhotonPlayer_0.Int32_0;
				bool flag2 = num4 == photonPlayer_4.Int32_0;
				if (flag)
				{
					Debug.LogWarning("Received RPC \"" + text + "\" for viewID " + num + " but this PhotonView does not exist! View was/is ours." + ((!flag2) ? " Remote called." : " Owner called."));
				}
				else
				{
					Debug.LogError("Received RPC \"" + text + "\" for viewID " + num + " but this PhotonView does not exist! Was remote PV." + ((!flag2) ? " Remote called." : " Owner called."));
				}
				return;
			}
			if (photonView.Int32_0 != num2)
			{
				Debug.LogError("Received RPC \"" + text + "\" on viewID " + num + " with a prefix of " + num2 + ", our prefix is " + photonView.Int32_0 + ". The RPC has been ignored.");
				return;
			}
			if (text == string.Empty)
			{
				Debug.LogError("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString(hashtable_0));
				return;
			}
			if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Full)
			{
				Debug.Log("Received RPC: " + text);
			}
			if (photonView.int_2 != 0 && !hashSet_0.Contains(photonView.int_2))
			{
				return;
			}
			Type[] array2 = new Type[0];
			if (array.Length > 0)
			{
				array2 = new Type[array.Length];
				int num5 = 0;
				foreach (object obj in array)
				{
					if (obj == null)
					{
						array2[num5] = null;
					}
					else
					{
						array2[num5] = obj.GetType();
					}
					num5++;
				}
			}
			int num6 = 0;
			int num7 = 0;
			MonoBehaviour[] components = photonView.GetComponents<MonoBehaviour>();
			foreach (MonoBehaviour monoBehaviour in components)
			{
				if (monoBehaviour == null)
				{
					Debug.LogError("ERROR You have missing MonoBehaviours on your gameobjects!");
					continue;
				}
				Type type = monoBehaviour.GetType();
				List<MethodInfo> list = null;
				if (dictionary_1.ContainsKey(type))
				{
					list = dictionary_1[type];
				}
				if (list == null)
				{
					List<MethodInfo> methods = SupportClass.GetMethods(type, typeof(RPC));
					dictionary_1[type] = methods;
					list = methods;
				}
				if (list == null)
				{
					continue;
				}
				for (int k = 0; k < list.Count; k++)
				{
					MethodInfo methodInfo = list[k];
					if (!(methodInfo.Name == text))
					{
						continue;
					}
					num7++;
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (parameters.Length == array2.Length)
					{
						if (CheckTypeMatch(parameters, array2))
						{
							num6++;
							object obj2 = methodInfo.Invoke(monoBehaviour, array);
							if (methodInfo.ReturnType == typeof(IEnumerator))
							{
								monoBehaviour.StartCoroutine((IEnumerator)obj2);
							}
						}
					}
					else if (parameters.Length - 1 == array2.Length)
					{
						if (CheckTypeMatch(parameters, array2) && parameters[parameters.Length - 1].ParameterType == typeof(PhotonMessageInfo))
						{
							num6++;
							int num8 = (int)hashtable_0[(byte)2];
							object[] array3 = new object[array.Length + 1];
							array.CopyTo(array3, 0);
							array3[array3.Length - 1] = new PhotonMessageInfo(photonPlayer_4, num8, photonView);
							object obj3 = methodInfo.Invoke(monoBehaviour, array3);
							if (methodInfo.ReturnType == typeof(IEnumerator))
							{
								monoBehaviour.StartCoroutine((IEnumerator)obj3);
							}
						}
					}
					else if (parameters.Length == 1 && parameters[0].ParameterType.IsArray)
					{
						num6++;
						object obj4 = methodInfo.Invoke(monoBehaviour, new object[1] { array });
						if (methodInfo.ReturnType == typeof(IEnumerator))
						{
							monoBehaviour.StartCoroutine((IEnumerator)obj4);
						}
					}
				}
			}
			if (num6 == 1)
			{
				return;
			}
			string text2 = string.Empty;
			foreach (Type type2 in array2)
			{
				if (text2 != string.Empty)
				{
					text2 += ", ";
				}
				text2 = ((type2 != null) ? (text2 + type2.Name) : (text2 + "null"));
			}
			if (num6 == 0)
			{
				if (num7 == 0)
				{
					Debug.LogError("PhotonView with ID " + num + " has no method \"" + text + "\" marked with the [RPC](C#) or @RPC(JS) property! Args: " + text2);
				}
				else
				{
					Debug.LogError("PhotonView with ID " + num + " has no method \"" + text + "\" that takes " + array2.Length + " argument(s): " + text2);
				}
			}
			else
			{
				Debug.LogError("PhotonView with ID " + num + " has " + num6 + " methods \"" + text + "\" that takes " + array2.Length + " argument(s): " + text2 + ". Should be just one?");
			}
		}
		else
		{
			Debug.LogError("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString(hashtable_0));
		}
	}

	private bool CheckTypeMatch(ParameterInfo[] parameterInfo_0, Type[] type_0)
	{
		if (parameterInfo_0.Length < type_0.Length)
		{
			return false;
		}
		int num = 0;
		while (true)
		{
			if (num < type_0.Length)
			{
				Type parameterType = parameterInfo_0[num].ParameterType;
				if (type_0[num] != null && !parameterType.Equals(type_0[num]))
				{
					break;
				}
				num++;
				continue;
			}
			return true;
		}
		return false;
	}

	internal ExitGames.Client.Photon.Hashtable SendInstantiate(string string_8, Vector3 vector3_0, Quaternion quaternion_0, int int_5, int[] int_6, object[] object_0, bool bool_10)
	{
		int num = int_6[0];
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable[(byte)0] = string_8;
		if (vector3_0 != Vector3.zero)
		{
			hashtable[(byte)1] = vector3_0;
		}
		if (quaternion_0 != Quaternion.identity)
		{
			hashtable[(byte)2] = quaternion_0;
		}
		if (int_5 != 0)
		{
			hashtable[(byte)3] = int_5;
		}
		if (int_6.Length > 1)
		{
			hashtable[(byte)4] = int_6;
		}
		if (object_0 != null)
		{
			hashtable[(byte)5] = object_0;
		}
		if (short_0 > 0)
		{
			hashtable[(byte)8] = short_0;
		}
		hashtable[(byte)6] = base.ServerTimeInMilliSeconds;
		hashtable[(byte)7] = num;
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
		raiseEventOptions.eventCaching_0 = ((!bool_10) ? EventCaching.AddToRoomCache : EventCaching.AddToRoomCacheGlobal);
		OpRaiseEvent(202, hashtable, true, raiseEventOptions);
		return hashtable;
	}

	internal GameObject DoInstantiate(ExitGames.Client.Photon.Hashtable hashtable_0, PhotonPlayer photonPlayer_4, GameObject gameObject_0)
	{
		string text = (string)hashtable_0[(byte)0];
		int num = (int)hashtable_0[(byte)6];
		int num2 = (int)hashtable_0[(byte)7];
		Vector3 position = ((!hashtable_0.ContainsKey((byte)1)) ? Vector3.zero : ((Vector3)hashtable_0[(byte)1]));
		Quaternion rotation = Quaternion.identity;
		if (hashtable_0.ContainsKey((byte)2))
		{
			rotation = (Quaternion)hashtable_0[(byte)2];
		}
		int num3 = 0;
		if (hashtable_0.ContainsKey((byte)3))
		{
			num3 = (int)hashtable_0[(byte)3];
		}
		short int32_ = 0;
		if (hashtable_0.ContainsKey((byte)8))
		{
			int32_ = (short)hashtable_0[(byte)8];
		}
		int[] array = ((!hashtable_0.ContainsKey((byte)4)) ? new int[1] { num2 } : ((int[])hashtable_0[(byte)4]));
		object[] object_ = ((!hashtable_0.ContainsKey((byte)5)) ? null : ((object[])hashtable_0[(byte)5]));
		if (num3 != 0 && !hashSet_0.Contains(num3))
		{
			return null;
		}
		if (gameObject_0 == null)
		{
			if (!bool_3 || !dictionary_2.TryGetValue(text, out gameObject_0))
			{
				gameObject_0 = (GameObject)Resources.Load(text, typeof(GameObject));
				if (bool_3)
				{
					dictionary_2.Add(text, gameObject_0);
				}
			}
			if (gameObject_0 == null)
			{
				Debug.LogError("PhotonNetwork error: Could not Instantiate the prefab [" + text + "]. Please verify you have this gameobject in a Resources folder.");
				return null;
			}
		}
		PhotonView[] photonViewsInChildren = gameObject_0.GetPhotonViewsInChildren();
		if (photonViewsInChildren.Length != array.Length)
		{
			throw new Exception("Error in Instantiation! The resource's PhotonView count is not the same as in incoming data.");
		}
		for (int i = 0; i < array.Length; i++)
		{
			photonViewsInChildren[i].Int32_1 = array[i];
			photonViewsInChildren[i].Int32_0 = int32_;
			photonViewsInChildren[i].int_4 = num2;
		}
		StoreInstantiationData(num2, object_);
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(gameObject_0, position, rotation);
		for (int j = 0; j < array.Length; j++)
		{
			photonViewsInChildren[j].Int32_1 = 0;
			photonViewsInChildren[j].Int32_0 = -1;
			photonViewsInChildren[j].int_3 = -1;
			photonViewsInChildren[j].int_4 = -1;
		}
		RemoveInstantiationData(num2);
		if (dictionary_4.ContainsKey(num2))
		{
			GameObject gameObject2 = dictionary_4[num2];
			string text2 = string.Empty;
			if (gameObject2 != null)
			{
				PhotonView[] photonViewsInChildren2 = gameObject2.GetPhotonViewsInChildren();
				PhotonView[] array2 = photonViewsInChildren2;
				foreach (PhotonView photonView in array2)
				{
					if (!(photonView == null))
					{
						text2 = text2 + photonView.ToString() + ", ";
					}
				}
			}
			Debug.LogError(string.Format("DoInstantiate re-defines a GameObject. Destroying old entry! New: '{0}' (instantiationID: {1}) Old: {3}. PhotonViews on old: {4}. instantiatedObjects.Count: {2}. PhotonNetwork.lastUsedViewSubId: {5} PhotonNetwork.lastUsedViewSubIdStatic: {6} this.photonViewList.Count {7}.)", gameObject, num2, dictionary_4.Count, gameObject2, text2, PhotonNetwork.int_3, PhotonNetwork.int_4, dictionary_5.Count));
			RemoveInstantiatedGO(gameObject2, true);
		}
		dictionary_4.Add(num2, gameObject);
		gameObject.SendMessage(PhotonNetworkingMessage.OnPhotonInstantiate.ToString(), new PhotonMessageInfo(photonPlayer_4, num, null), SendMessageOptions.DontRequireReceiver);
		return gameObject;
	}

	private void StoreInstantiationData(int int_5, object[] object_0)
	{
		dictionary_8[int_5] = object_0;
	}

	public object[] FetchInstantiationData(int int_5)
	{
		object[] value = null;
		if (int_5 == 0)
		{
			return null;
		}
		dictionary_8.TryGetValue(int_5, out value);
		return value;
	}

	private void RemoveInstantiationData(int int_5)
	{
		dictionary_8.Remove(int_5);
	}

	public void RemoveAllInstantiatedObjects()
	{
		GameObject[] array = new GameObject[dictionary_4.Count];
		dictionary_4.Values.CopyTo(array, 0);
		foreach (GameObject gameObject in array)
		{
			if (!(gameObject == null))
			{
				RemoveInstantiatedGO(gameObject, false);
			}
		}
		if (dictionary_4.Count > 0)
		{
			Debug.LogError("RemoveAllInstantiatedObjects() this.instantiatedObjects.Count should be 0 by now.");
		}
		dictionary_4 = new Dictionary<int, GameObject>();
	}

	public void DestroyPlayerObjects(int int_5, bool bool_10)
	{
		if (int_5 <= 0)
		{
			Debug.LogError("Failed to Destroy objects of playerId: " + int_5);
			return;
		}
		if (!bool_10)
		{
			OpRemoveFromServerInstantiationsOfPlayer(int_5);
			OpCleanRpcBuffer(int_5);
			SendDestroyOfPlayer(int_5);
		}
		Queue<GameObject> queue = new Queue<GameObject>();
		int num = int_5 * PhotonNetwork.int_0;
		int num2 = num + PhotonNetwork.int_0;
		foreach (KeyValuePair<int, GameObject> item in dictionary_4)
		{
			if (item.Key > num && item.Key < num2)
			{
				queue.Enqueue(item.Value);
			}
		}
		foreach (GameObject item2 in queue)
		{
			RemoveInstantiatedGO(item2, true);
		}
	}

	public void DestroyAll(bool bool_10)
	{
		if (!bool_10)
		{
			OpRemoveCompleteCache();
			SendDestroyOfAll();
		}
		LocalCleanupAnythingInstantiated(true);
	}

	public void RemoveInstantiatedGO(GameObject gameObject_0, bool bool_10)
	{
		if (gameObject_0 == null)
		{
			Debug.LogError("Failed to 'network-remove' GameObject because it's null.");
			return;
		}
		PhotonView[] componentsInChildren = gameObject_0.GetComponentsInChildren<PhotonView>();
		if (componentsInChildren != null && componentsInChildren.Length > 0)
		{
			PhotonView photonView = componentsInChildren[0];
			int int32_ = photonView.Int32_2;
			int num = photonView.int_4;
			if (!bool_10)
			{
				if (!photonView.Boolean_1 && (!PhotonPlayer_0.Boolean_0 || dictionary_0.ContainsKey(int32_)))
				{
					Debug.LogError("Failed to 'network-remove' GameObject. Client is neither owner nor masterClient taking over for owner who left: " + photonView);
					return;
				}
				if (num < 1)
				{
					Debug.LogError(string.Concat("Failed to 'network-remove' GameObject because it is missing a valid InstantiationId on view: ", photonView, ". Not Destroying GameObject or PhotonViews!"));
					return;
				}
			}
			if (!bool_10)
			{
				ServerCleanInstantiateAndDestroy(num, int32_);
			}
			dictionary_4.Remove(num);
			for (int num2 = componentsInChildren.Length - 1; num2 >= 0; num2--)
			{
				PhotonView photonView2 = componentsInChildren[num2];
				if (!(photonView2 == null))
				{
					if (photonView2.int_4 >= 1)
					{
						LocalCleanPhotonView(photonView2);
					}
					if (!bool_10)
					{
						OpCleanRpcBuffer(photonView2);
					}
				}
			}
			if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Full)
			{
				Debug.Log("Network destroy Instantiated GO: " + gameObject_0.name);
			}
			UnityEngine.Object.Destroy(gameObject_0);
		}
		else
		{
			Debug.LogError("Failed to 'network-remove' GameObject because has no PhotonView components: " + gameObject_0);
		}
	}

	public int GetInstantiatedObjectsId(GameObject gameObject_0)
	{
		int result = -1;
		if (gameObject_0 == null)
		{
			Debug.LogError("GetInstantiatedObjectsId() for GO == null.");
			return result;
		}
		PhotonView[] photonViewsInChildren = gameObject_0.GetPhotonViewsInChildren();
		if (photonViewsInChildren != null && photonViewsInChildren.Length > 0 && photonViewsInChildren[0] != null)
		{
			return photonViewsInChildren[0].int_4;
		}
		if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
		{
			Debug.Log("GetInstantiatedObjectsId failed for GO: " + gameObject_0);
		}
		return result;
	}

	private void ServerCleanInstantiateAndDestroy(int int_5, int int_6)
	{
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable[(byte)7] = int_5;
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
		raiseEventOptions.eventCaching_0 = EventCaching.RemoveFromRoomCache;
		raiseEventOptions.int_0 = new int[1] { int_6 };
		RaiseEventOptions raiseEventOptions_ = raiseEventOptions;
		OpRaiseEvent(202, hashtable, true, raiseEventOptions_);
		ExitGames.Client.Photon.Hashtable hashtable2 = new ExitGames.Client.Photon.Hashtable();
		hashtable2[(byte)0] = int_5;
		OpRaiseEvent(204, hashtable2, true, null);
	}

	private void SendDestroyOfPlayer(int int_5)
	{
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable[(byte)0] = int_5;
		OpRaiseEvent(207, hashtable, true, null);
	}

	private void SendDestroyOfAll()
	{
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable[(byte)0] = -1;
		OpRaiseEvent(207, hashtable, true, null);
	}

	private void OpRemoveFromServerInstantiationsOfPlayer(int int_5)
	{
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
		raiseEventOptions.eventCaching_0 = EventCaching.RemoveFromRoomCache;
		raiseEventOptions.int_0 = new int[1] { int_5 };
		RaiseEventOptions raiseEventOptions_ = raiseEventOptions;
		OpRaiseEvent(202, null, true, raiseEventOptions_);
	}

	public void LocalCleanPhotonView(PhotonView photonView_0)
	{
		photonView_0.bool_2 = true;
		dictionary_5.Remove(photonView_0.Int32_1);
	}

	public PhotonView GetPhotonView(int int_5)
	{
		PhotonView value = null;
		dictionary_5.TryGetValue(int_5, out value);
		if (value == null)
		{
			PhotonView[] array = UnityEngine.Object.FindObjectsOfType(typeof(PhotonView)) as PhotonView[];
			PhotonView[] array2 = array;
			foreach (PhotonView photonView in array2)
			{
				if (photonView.Int32_1 == int_5)
				{
					if (photonView.bool_1)
					{
						Debug.LogWarning("Had to lookup view that wasn't in dict: " + photonView);
					}
					return photonView;
				}
			}
		}
		return value;
	}

	public void RegisterPhotonView(PhotonView photonView_0)
	{
		if (!Application.isPlaying)
		{
			dictionary_5 = new Dictionary<int, PhotonView>();
		}
		else
		{
			if (photonView_0.int_0 == 0)
			{
				return;
			}
			if (dictionary_5.ContainsKey(photonView_0.Int32_1))
			{
				if (photonView_0 != dictionary_5[photonView_0.Int32_1])
				{
					Debug.LogError(string.Format("PhotonView ID duplicate found: {0}. New: {1} old: {2}. Maybe one wasn't destroyed on scene load?! Check for 'DontDestroyOnLoad'. Destroying old entry, adding new.", photonView_0.Int32_1, photonView_0, dictionary_5[photonView_0.Int32_1]));
				}
				RemoveInstantiatedGO(dictionary_5[photonView_0.Int32_1].gameObject, true);
			}
			dictionary_5.Add(photonView_0.Int32_1, photonView_0);
			if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Full)
			{
				Debug.Log("Registered PhotonView: " + photonView_0.Int32_1);
			}
		}
	}

	public void OpCleanRpcBuffer(int int_5)
	{
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
		raiseEventOptions.eventCaching_0 = EventCaching.RemoveFromRoomCache;
		raiseEventOptions.int_0 = new int[1] { int_5 };
		RaiseEventOptions raiseEventOptions_ = raiseEventOptions;
		OpRaiseEvent(200, null, true, raiseEventOptions_);
	}

	public void OpRemoveCompleteCacheOfPlayer(int int_5)
	{
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
		raiseEventOptions.eventCaching_0 = EventCaching.RemoveFromRoomCache;
		raiseEventOptions.int_0 = new int[1] { int_5 };
		RaiseEventOptions raiseEventOptions_ = raiseEventOptions;
		OpRaiseEvent(0, null, true, raiseEventOptions_);
	}

	public void OpRemoveCompleteCache()
	{
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
		raiseEventOptions.eventCaching_0 = EventCaching.RemoveFromRoomCache;
		raiseEventOptions.receiverGroup_0 = ReceiverGroup.MasterClient;
		RaiseEventOptions raiseEventOptions_ = raiseEventOptions;
		OpRaiseEvent(0, null, true, raiseEventOptions_);
	}

	private void RemoveCacheOfLeftPlayers()
	{
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		dictionary[244] = (byte)0;
		dictionary[247] = (byte)7;
		OpCustom(253, dictionary, true, 0);
	}

	public void CleanRpcBufferIfMine(PhotonView photonView_0)
	{
		if (photonView_0.int_1 != PhotonPlayer_0.Int32_0 && !PhotonPlayer_0.Boolean_0)
		{
			Debug.LogError(string.Concat("Cannot remove cached RPCs on a PhotonView thats not ours! ", photonView_0.PhotonPlayer_0, " scene: ", photonView_0.Boolean_0));
		}
		else
		{
			OpCleanRpcBuffer(photonView_0);
		}
	}

	public void OpCleanRpcBuffer(PhotonView photonView_0)
	{
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable[(byte)0] = photonView_0.Int32_1;
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
		raiseEventOptions.eventCaching_0 = EventCaching.RemoveFromRoomCache;
		RaiseEventOptions raiseEventOptions_ = raiseEventOptions;
		OpRaiseEvent(200, hashtable, true, raiseEventOptions_);
	}

	public void RemoveRPCsInGroup(int int_5)
	{
		foreach (KeyValuePair<int, PhotonView> item in dictionary_5)
		{
			PhotonView value = item.Value;
			if (value.int_2 == int_5)
			{
				CleanRpcBufferIfMine(value);
			}
		}
	}

	public void SetLevelPrefix(short short_1)
	{
		short_0 = short_1;
	}

	internal void RPC(PhotonView photonView_0, string string_8, PhotonPlayer photonPlayer_4, params object[] object_0)
	{
		if (!hashSet_1.Contains(photonView_0.int_2))
		{
			if (photonView_0.Int32_1 < 1)
			{
				Debug.LogError("Illegal view ID:" + photonView_0.Int32_1 + " method: " + string_8 + " GO:" + photonView_0.gameObject.name);
			}
			if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Full)
			{
				Debug.Log(string.Concat("Sending RPC \"", string_8, "\" to player[", photonPlayer_4, "]"));
			}
			ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
			hashtable[(byte)0] = photonView_0.Int32_1;
			if (photonView_0.Int32_0 > 0)
			{
				hashtable[(byte)1] = (short)photonView_0.Int32_0;
			}
			hashtable[(byte)2] = base.ServerTimeInMilliSeconds;
			int value = 0;
			if (dictionary_6.TryGetValue(string_8, out value))
			{
				hashtable[(byte)5] = (byte)value;
			}
			else
			{
				hashtable[(byte)3] = string_8;
			}
			if (object_0 != null && object_0.Length > 0)
			{
				hashtable[(byte)4] = object_0;
			}
			if (PhotonPlayer_0 == photonPlayer_4)
			{
				ExecuteRPC(hashtable, photonPlayer_4);
				return;
			}
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
			raiseEventOptions.int_0 = new int[1] { photonPlayer_4.Int32_0 };
			RaiseEventOptions raiseEventOptions_ = raiseEventOptions;
			OpRaiseEvent(200, hashtable, true, raiseEventOptions_);
		}
	}

	internal void RPC(PhotonView photonView_0, string string_8, PhotonTargets photonTargets_0, params object[] object_0)
	{
		if (hashSet_1.Contains(photonView_0.int_2))
		{
			return;
		}
		if (photonView_0.Int32_1 < 1)
		{
			Debug.LogError("Illegal view ID:" + photonView_0.Int32_1 + " method: " + string_8 + " GO:" + photonView_0.gameObject.name);
		}
		if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Full)
		{
			Debug.Log("Sending RPC \"" + string_8 + "\" to " + photonTargets_0);
		}
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable[(byte)0] = photonView_0.Int32_1;
		if (photonView_0.Int32_0 > 0)
		{
			hashtable[(byte)1] = (short)photonView_0.Int32_0;
		}
		hashtable[(byte)2] = base.ServerTimeInMilliSeconds;
		int value = 0;
		if (dictionary_6.TryGetValue(string_8, out value))
		{
			hashtable[(byte)5] = (byte)value;
		}
		else
		{
			hashtable[(byte)3] = string_8;
		}
		if (object_0 != null && object_0.Length > 0)
		{
			hashtable[(byte)4] = object_0;
		}
		switch (photonTargets_0)
		{
		case PhotonTargets.All:
		{
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
			raiseEventOptions.byte_0 = (byte)photonView_0.int_2;
			RaiseEventOptions raiseEventOptions_5 = raiseEventOptions;
			OpRaiseEvent(200, hashtable, true, raiseEventOptions_5);
			ExecuteRPC(hashtable, PhotonPlayer_0);
			break;
		}
		case PhotonTargets.Others:
		{
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
			raiseEventOptions.byte_0 = (byte)photonView_0.int_2;
			RaiseEventOptions raiseEventOptions_4 = raiseEventOptions;
			OpRaiseEvent(200, hashtable, true, raiseEventOptions_4);
			break;
		}
		case PhotonTargets.AllBuffered:
		{
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
			raiseEventOptions.eventCaching_0 = EventCaching.AddToRoomCache;
			RaiseEventOptions raiseEventOptions_7 = raiseEventOptions;
			OpRaiseEvent(200, hashtable, true, raiseEventOptions_7);
			ExecuteRPC(hashtable, PhotonPlayer_0);
			break;
		}
		case PhotonTargets.OthersBuffered:
		{
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
			raiseEventOptions.eventCaching_0 = EventCaching.AddToRoomCache;
			RaiseEventOptions raiseEventOptions_6 = raiseEventOptions;
			OpRaiseEvent(200, hashtable, true, raiseEventOptions_6);
			break;
		}
		case PhotonTargets.MasterClient:
		{
			if (photonPlayer_2 == PhotonPlayer_0)
			{
				ExecuteRPC(hashtable, PhotonPlayer_0);
				break;
			}
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
			raiseEventOptions.receiverGroup_0 = ReceiverGroup.MasterClient;
			RaiseEventOptions raiseEventOptions_3 = raiseEventOptions;
			OpRaiseEvent(200, hashtable, true, raiseEventOptions_3);
			break;
		}
		case PhotonTargets.AllViaServer:
		{
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
			raiseEventOptions.byte_0 = (byte)photonView_0.int_2;
			raiseEventOptions.receiverGroup_0 = ReceiverGroup.All;
			RaiseEventOptions raiseEventOptions_2 = raiseEventOptions;
			OpRaiseEvent(200, hashtable, true, raiseEventOptions_2);
			break;
		}
		case PhotonTargets.AllBufferedViaServer:
		{
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
			raiseEventOptions.byte_0 = (byte)photonView_0.int_2;
			raiseEventOptions.receiverGroup_0 = ReceiverGroup.All;
			raiseEventOptions.eventCaching_0 = EventCaching.AddToRoomCache;
			RaiseEventOptions raiseEventOptions_ = raiseEventOptions;
			OpRaiseEvent(200, hashtable, true, raiseEventOptions_);
			break;
		}
		default:
			Debug.LogError("Unsupported target enum: " + photonTargets_0);
			break;
		}
	}

	public void SetReceivingEnabled(int int_5, bool bool_10)
	{
		if (int_5 <= 0)
		{
			Debug.LogError("Error: PhotonNetwork.SetReceivingEnabled was called with an illegal group number: " + int_5 + ". The group number should be at least 1.");
		}
		else if (bool_10)
		{
			if (!hashSet_0.Contains(int_5))
			{
				hashSet_0.Add(int_5);
				byte[] byte_ = new byte[1] { (byte)int_5 };
				OpChangeGroups(null, byte_);
			}
		}
		else if (hashSet_0.Contains(int_5))
		{
			hashSet_0.Remove(int_5);
			byte[] byte_2 = new byte[1] { (byte)int_5 };
			OpChangeGroups(byte_2, null);
		}
	}

	public void SetReceivingEnabled(int[] int_5, int[] int_6)
	{
		List<byte> list = new List<byte>();
		List<byte> list2 = new List<byte>();
		if (int_5 != null)
		{
			foreach (int num in int_5)
			{
				if (num <= 0)
				{
					Debug.LogError("Error: PhotonNetwork.SetReceivingEnabled was called with an illegal group number: " + num + ". The group number should be at least 1.");
				}
				else if (!hashSet_0.Contains(num))
				{
					hashSet_0.Add(num);
					list.Add((byte)num);
				}
			}
		}
		if (int_6 != null)
		{
			foreach (int num2 in int_6)
			{
				if (num2 <= 0)
				{
					Debug.LogError("Error: PhotonNetwork.SetReceivingEnabled was called with an illegal group number: " + num2 + ". The group number should be at least 1.");
				}
				else if (list.Contains((byte)num2))
				{
					Debug.LogError("Error: PhotonNetwork.SetReceivingEnabled disableGroups contains a group that is also in the enableGroups: " + num2 + ".");
				}
				else if (hashSet_0.Contains(num2))
				{
					hashSet_0.Remove(num2);
					list2.Add((byte)num2);
				}
			}
		}
		OpChangeGroups((list2.Count <= 0) ? null : list2.ToArray(), (list.Count <= 0) ? null : list.ToArray());
	}

	public void SetSendingEnabled(int int_5, bool bool_10)
	{
		if (!bool_10)
		{
			hashSet_1.Add(int_5);
		}
		else
		{
			hashSet_1.Remove(int_5);
		}
	}

	public void SetSendingEnabled(int[] int_5, int[] int_6)
	{
		if (int_5 != null)
		{
			foreach (int item in int_5)
			{
				if (hashSet_1.Contains(item))
				{
					hashSet_1.Remove(item);
				}
			}
		}
		if (int_6 == null)
		{
			return;
		}
		foreach (int item2 in int_6)
		{
			if (!hashSet_1.Contains(item2))
			{
				hashSet_1.Add(item2);
			}
		}
	}

	public void NewSceneLoaded()
	{
		if (bool_8)
		{
			bool_8 = false;
			PhotonNetwork.Boolean_8 = true;
		}
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, PhotonView> item in dictionary_5)
		{
			PhotonView value = item.Value;
			if (value == null)
			{
				list.Add(item.Key);
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			int key = list[i];
			dictionary_5.Remove(key);
		}
		if (list.Count > 0 && PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
		{
			Debug.Log("New level loaded. Removed " + list.Count + " scene view IDs from last level.");
		}
	}

	public void RunViewUpdate()
	{
		if (!PhotonNetwork.Boolean_0 || PhotonNetwork.Boolean_3 || dictionary_0 == null || dictionary_0.Count <= 1)
		{
			return;
		}
		Dictionary<int, ExitGames.Client.Photon.Hashtable> dictionary = new Dictionary<int, ExitGames.Client.Photon.Hashtable>();
		Dictionary<int, ExitGames.Client.Photon.Hashtable> dictionary2 = new Dictionary<int, ExitGames.Client.Photon.Hashtable>();
		foreach (KeyValuePair<int, PhotonView> item in dictionary_5)
		{
			PhotonView value = item.Value;
			if (!(value.component_0 != null) || value.viewSynchronization_0 == ViewSynchronization.Off || (value.int_1 != PhotonPlayer_0.Int32_0 && (!value.Boolean_0 || photonPlayer_2 != PhotonPlayer_0)) || !value.gameObject.activeInHierarchy || hashSet_1.Contains(value.int_2))
			{
				continue;
			}
			ExitGames.Client.Photon.Hashtable hashtable = OnSerializeWrite(value);
			if (hashtable == null)
			{
				continue;
			}
			if (value.viewSynchronization_0 != ViewSynchronization.ReliableDeltaCompressed && !value.bool_0)
			{
				if (!dictionary2.ContainsKey(value.int_2))
				{
					dictionary2[value.int_2] = new ExitGames.Client.Photon.Hashtable();
					dictionary2[value.int_2][(byte)0] = base.ServerTimeInMilliSeconds;
					if (short_0 >= 0)
					{
						dictionary2[value.int_2][(byte)1] = short_0;
					}
				}
				ExitGames.Client.Photon.Hashtable hashtable2 = dictionary2[value.int_2];
				hashtable2.Add((short)hashtable2.Count, hashtable);
			}
			else
			{
				if (!hashtable.ContainsKey((byte)1) && !hashtable.ContainsKey((byte)2))
				{
					continue;
				}
				if (!dictionary.ContainsKey(value.int_2))
				{
					dictionary[value.int_2] = new ExitGames.Client.Photon.Hashtable();
					dictionary[value.int_2][(byte)0] = base.ServerTimeInMilliSeconds;
					if (short_0 >= 0)
					{
						dictionary[value.int_2][(byte)1] = short_0;
					}
				}
				ExitGames.Client.Photon.Hashtable hashtable3 = dictionary[value.int_2];
				hashtable3.Add((short)hashtable3.Count, hashtable);
			}
		}
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
		foreach (KeyValuePair<int, ExitGames.Client.Photon.Hashtable> item2 in dictionary)
		{
			raiseEventOptions.byte_0 = (byte)item2.Key;
			OpRaiseEvent(206, item2.Value, true, raiseEventOptions);
		}
		foreach (KeyValuePair<int, ExitGames.Client.Photon.Hashtable> item3 in dictionary2)
		{
			raiseEventOptions.byte_0 = (byte)item3.Key;
			OpRaiseEvent(201, item3.Value, false, raiseEventOptions);
		}
	}

	private ExitGames.Client.Photon.Hashtable OnSerializeWrite(PhotonView photonView_0)
	{
		List<object> list = new List<object>();
		if (photonView_0.component_0 is MonoBehaviour)
		{
			PhotonStream photonStream = new PhotonStream(true, null);
			PhotonMessageInfo photonMessageInfo_ = new PhotonMessageInfo(PhotonPlayer_0, base.ServerTimeInMilliSeconds, photonView_0);
			photonView_0.ExecuteOnSerialize(photonStream, photonMessageInfo_);
			if (photonStream.Int32_0 == 0)
			{
				return null;
			}
			list = photonStream.list_0;
		}
		else if (photonView_0.component_0 is Transform)
		{
			Transform transform = (Transform)photonView_0.component_0;
			if (photonView_0.onSerializeTransform_0 != 0 && photonView_0.onSerializeTransform_0 != OnSerializeTransform.PositionAndRotation && photonView_0.onSerializeTransform_0 != OnSerializeTransform.All)
			{
				list.Add(null);
			}
			else
			{
				list.Add(transform.localPosition);
			}
			if (photonView_0.onSerializeTransform_0 != OnSerializeTransform.OnlyRotation && photonView_0.onSerializeTransform_0 != OnSerializeTransform.PositionAndRotation && photonView_0.onSerializeTransform_0 != OnSerializeTransform.All)
			{
				list.Add(null);
			}
			else
			{
				list.Add(transform.localRotation);
			}
			if (photonView_0.onSerializeTransform_0 == OnSerializeTransform.OnlyScale || photonView_0.onSerializeTransform_0 == OnSerializeTransform.All)
			{
				list.Add(transform.localScale);
			}
		}
		else
		{
			if (!(photonView_0.component_0 is Rigidbody))
			{
				Debug.LogError("Observed type is not serializable: " + photonView_0.component_0.GetType());
				return null;
			}
			Rigidbody rigidbody = (Rigidbody)photonView_0.component_0;
			if (photonView_0.onSerializeRigidBody_0 != OnSerializeRigidBody.OnlyAngularVelocity)
			{
				list.Add(rigidbody.velocity);
			}
			else
			{
				list.Add(null);
			}
			if (photonView_0.onSerializeRigidBody_0 != 0)
			{
				list.Add(rigidbody.angularVelocity);
			}
		}
		object[] array = list.ToArray();
		if (photonView_0.viewSynchronization_0 == ViewSynchronization.UnreliableOnChange)
		{
			if (AlmostEquals(array, photonView_0.object_1))
			{
				if (photonView_0.bool_0)
				{
					return null;
				}
				photonView_0.bool_0 = true;
				photonView_0.object_1 = array;
			}
			else
			{
				photonView_0.bool_0 = false;
				photonView_0.object_1 = array;
			}
		}
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable[(byte)0] = photonView_0.Int32_1;
		hashtable[(byte)1] = array;
		if (photonView_0.viewSynchronization_0 == ViewSynchronization.ReliableDeltaCompressed)
		{
			bool flag = DeltaCompressionWrite(photonView_0, hashtable);
			photonView_0.object_1 = array;
			if (!flag)
			{
				return null;
			}
		}
		return hashtable;
	}

	private void OnSerializeRead(ExitGames.Client.Photon.Hashtable hashtable_0, PhotonPlayer photonPlayer_4, int int_5, short short_1)
	{
		int num = (int)hashtable_0[(byte)0];
		PhotonView photonView = GetPhotonView(num);
		if (photonView == null)
		{
			Debug.LogWarning("Received OnSerialization for view ID " + num + ". We have no such PhotonView! Ignored this if you're leaving a room. State: " + PeerState_0);
		}
		else if (photonView.Int32_0 > 0 && short_1 != photonView.Int32_0)
		{
			Debug.LogError("Received OnSerialization for view ID " + num + " with prefix " + short_1 + ". Our prefix is " + photonView.Int32_0);
		}
		else
		{
			if (photonView.int_2 != 0 && !hashSet_0.Contains(photonView.int_2))
			{
				return;
			}
			if (photonView.viewSynchronization_0 == ViewSynchronization.ReliableDeltaCompressed)
			{
				if (!DeltaCompressionRead(photonView, hashtable_0))
				{
					if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
					{
						Debug.Log("Skipping packet for " + photonView.name + " [" + photonView.Int32_1 + "] as we haven't received a full packet for delta compression yet. This is OK if it happens for the first few frames after joining a game.");
					}
					return;
				}
				photonView.object_2 = hashtable_0[(byte)1] as object[];
			}
			if (photonView.component_0 is MonoBehaviour)
			{
				object[] object_ = hashtable_0[(byte)1] as object[];
				PhotonStream photonStream_ = new PhotonStream(false, object_);
				PhotonMessageInfo photonMessageInfo_ = new PhotonMessageInfo(photonPlayer_4, int_5, photonView);
				photonView.ExecuteOnSerialize(photonStream_, photonMessageInfo_);
			}
			else if (photonView.component_0 is Transform)
			{
				object[] array = hashtable_0[(byte)1] as object[];
				Transform transform = (Transform)photonView.component_0;
				if (array.Length >= 1 && array[0] != null)
				{
					transform.localPosition = (Vector3)array[0];
				}
				if (array.Length >= 2 && array[1] != null)
				{
					transform.localRotation = (Quaternion)array[1];
				}
				if (array.Length >= 3 && array[2] != null)
				{
					transform.localScale = (Vector3)array[2];
				}
			}
			else if (photonView.component_0 is Rigidbody)
			{
				object[] array2 = hashtable_0[(byte)1] as object[];
				Rigidbody rigidbody = (Rigidbody)photonView.component_0;
				if (array2.Length >= 1 && array2[0] != null)
				{
					rigidbody.velocity = (Vector3)array2[0];
				}
				if (array2.Length >= 2 && array2[1] != null)
				{
					rigidbody.angularVelocity = (Vector3)array2[1];
				}
			}
			else
			{
				Debug.LogError("Type of observed is unknown when receiving.");
			}
		}
	}

	private bool AlmostEquals(object[] object_0, object[] object_1)
	{
		if (object_0 == null && object_1 == null)
		{
			return true;
		}
		if (object_0 != null && object_1 != null && object_0.Length == object_1.Length)
		{
			int num = 0;
			while (true)
			{
				if (num < object_1.Length)
				{
					object object_2 = object_1[num];
					object object_3 = object_0[num];
					if (!ObjectIsSameWithInprecision(object_2, object_3))
					{
						break;
					}
					num++;
					continue;
				}
				return true;
			}
			return false;
		}
		return false;
	}

	private bool DeltaCompressionWrite(PhotonView photonView_0, ExitGames.Client.Photon.Hashtable hashtable_0)
	{
		if (photonView_0.object_1 == null)
		{
			return true;
		}
		object[] object_ = photonView_0.object_1;
		object[] array = hashtable_0[(byte)1] as object[];
		if (array == null)
		{
			return false;
		}
		if (object_.Length != array.Length)
		{
			return true;
		}
		object[] array2 = new object[array.Length];
		int num = 0;
		List<int> list = new List<int>();
		for (int i = 0; i < array2.Length; i++)
		{
			object obj = array[i];
			object object_2 = object_[i];
			if (ObjectIsSameWithInprecision(obj, object_2))
			{
				num++;
				continue;
			}
			array2[i] = array[i];
			if (obj == null)
			{
				list.Add(i);
			}
		}
		if (num > 0)
		{
			hashtable_0.Remove((byte)1);
			if (num == array.Length)
			{
				return false;
			}
			hashtable_0[(byte)2] = array2;
			if (list.Count > 0)
			{
				hashtable_0[(byte)3] = list.ToArray();
			}
		}
		return true;
	}

	private bool DeltaCompressionRead(PhotonView photonView_0, ExitGames.Client.Photon.Hashtable hashtable_0)
	{
		if (hashtable_0.ContainsKey((byte)1))
		{
			return true;
		}
		if (photonView_0.object_2 == null)
		{
			return false;
		}
		object[] array = hashtable_0[(byte)2] as object[];
		if (array == null)
		{
			return false;
		}
		int[] array2 = hashtable_0[(byte)3] as int[];
		if (array2 == null)
		{
			array2 = new int[0];
		}
		object[] object_ = photonView_0.object_2;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == null && !array2.Contains(i))
			{
				object obj = object_[i];
				array[i] = obj;
			}
		}
		hashtable_0[(byte)1] = array;
		return true;
	}

	private bool ObjectIsSameWithInprecision(object object_0, object object_1)
	{
		if (object_0 != null && object_1 != null)
		{
			if (!object_0.Equals(object_1))
			{
				if (object_0 is Vector3)
				{
					Vector3 vector3_ = (Vector3)object_0;
					Vector3 vector3_2 = (Vector3)object_1;
					if (vector3_.AlmostEquals(vector3_2, PhotonNetwork.float_0))
					{
						return true;
					}
				}
				else if (object_0 is Vector2)
				{
					Vector2 vector2_ = (Vector2)object_0;
					Vector2 vector2_2 = (Vector2)object_1;
					if (vector2_.AlmostEquals(vector2_2, PhotonNetwork.float_0))
					{
						return true;
					}
				}
				else if (object_0 is Quaternion)
				{
					Quaternion quaternion_ = (Quaternion)object_0;
					Quaternion quaternion_2 = (Quaternion)object_1;
					if (quaternion_.AlmostEquals(quaternion_2, PhotonNetwork.float_1))
					{
						return true;
					}
				}
				else if (object_0 is float)
				{
					float float_ = (float)object_0;
					float float_2 = (float)object_1;
					if (float_.AlmostEquals(float_2, PhotonNetwork.float_2))
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}
		return object_0 == null && object_1 == null;
	}

	protected internal static bool GetMethod(MonoBehaviour monoBehaviour_0, string string_8, out MethodInfo methodInfo_0)
	{
		methodInfo_0 = null;
		if (!(monoBehaviour_0 == null) && !string.IsNullOrEmpty(string_8))
		{
			List<MethodInfo> methods = SupportClass.GetMethods(monoBehaviour_0.GetType(), null);
			int num = 0;
			MethodInfo methodInfo;
			while (true)
			{
				if (num < methods.Count)
				{
					methodInfo = methods[num];
					if (methodInfo.Name.Equals(string_8))
					{
						break;
					}
					num++;
					continue;
				}
				return false;
			}
			methodInfo_0 = methodInfo;
			return true;
		}
		return false;
	}

	protected internal void LoadLevelIfSynced()
	{
		if (!PhotonNetwork.Boolean_4 || PhotonNetwork.Boolean_9 || PhotonNetwork.Room_0 == null || !PhotonNetwork.Room_0.Hashtable_0.ContainsKey("curScn"))
		{
			return;
		}
		object obj = PhotonNetwork.Room_0.Hashtable_0["curScn"];
		if (obj is int)
		{
			if (Application.loadedLevel != (int)obj)
			{
				PhotonNetwork.LoadLevel((int)obj);
			}
		}
		else if (obj is string && Application.loadedLevelName != (string)obj)
		{
			PhotonNetwork.LoadLevel((string)obj);
		}
	}

	protected internal void SetLevelInPropsIfSynced(object object_0)
	{
		if (!PhotonNetwork.Boolean_4 || !PhotonNetwork.Boolean_9 || PhotonNetwork.Room_0 == null)
		{
			return;
		}
		if (object_0 == null)
		{
			Debug.LogError("Parameter levelId can't be null!");
			return;
		}
		if (PhotonNetwork.Room_0.Hashtable_0.ContainsKey("curScn"))
		{
			object obj = PhotonNetwork.Room_0.Hashtable_0["curScn"];
			if ((obj is int && Application.loadedLevel == (int)obj) || (obj is string && Application.loadedLevelName.Equals((string)obj)))
			{
				return;
			}
		}
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		if (object_0 is int)
		{
			hashtable["curScn"] = (int)object_0;
		}
		else if (object_0 is string)
		{
			hashtable["curScn"] = (string)object_0;
		}
		else
		{
			Debug.LogError("Parameter levelId must be int or string!");
		}
		PhotonNetwork.Room_0.SetCustomProperties(hashtable);
		SendOutgoingCommands();
	}

	public void SetApp(string string_8, string string_9)
	{
		string_2 = string_8.Trim();
		if (!string.IsNullOrEmpty(string_9))
		{
			string_1 = string_9.Trim();
		}
	}

	public bool WebRpc(string string_8, object object_0)
	{
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		dictionary.Add(209, string_8);
		dictionary.Add(208, object_0);
		return OpCustom(219, dictionary, true);
	}
}
