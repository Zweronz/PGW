using System;
using System.Collections;
using ExitGames.Client.Photon;
using Photon;
using UnityEngine;

internal class PhotonHandler : Photon.MonoBehaviour, IPhotonPeerListener
{
	private const string string_0 = "PUNCloudBestRegion";

	public static PhotonHandler photonHandler_0;

	public int updateInterval;

	public int updateIntervalOnSerialize;

	private int int_0;

	private int int_1;

	private static bool bool_0;

	public static bool bool_1;

	public static Type type_0;

	internal static CloudRegionCode cloudRegionCode_0 = CloudRegionCode.none;

	internal static CloudRegionCode CloudRegionCode_0
	{
		get
		{
			string @string = PlayerPrefs.GetString("PUNCloudBestRegion", string.Empty);
			if (!string.IsNullOrEmpty(@string))
			{
				return Region.Parse(@string);
			}
			return CloudRegionCode.none;
		}
		set
		{
			if (value == CloudRegionCode.none)
			{
				PlayerPrefs.DeleteKey("PUNCloudBestRegion");
			}
			else
			{
				PlayerPrefs.SetString("PUNCloudBestRegion", value.ToString());
			}
		}
	}

	protected void Awake()
	{
		if (photonHandler_0 != null && photonHandler_0 != this && photonHandler_0.gameObject != null)
		{
			UnityEngine.Object.DestroyImmediate(photonHandler_0.gameObject);
		}
		photonHandler_0 = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		updateInterval = 1000 / PhotonNetwork.Int32_2;
		updateIntervalOnSerialize = 1000 / PhotonNetwork.Int32_3;
		StartFallbackSendAckThread();
	}

	protected void OnApplicationQuit()
	{
		bool_1 = true;
		StopFallbackSendAckThread();
		PhotonNetwork.Disconnect();
	}

	protected void Update()
	{
		if (PhotonNetwork.networkingPeer_0 == null)
		{
			Debug.LogError("NetworkPeer broke!");
		}
		else
		{
			if (PhotonNetwork.PeerState_0 == PeerState.PeerCreated || PhotonNetwork.PeerState_0 == PeerState.Disconnected || PhotonNetwork.Boolean_3 || !PhotonNetwork.Boolean_8)
			{
				return;
			}
			bool flag = true;
			while (PhotonNetwork.Boolean_8 && flag)
			{
				flag = PhotonNetwork.networkingPeer_0.DispatchIncomingCommands();
			}
			int num = (int)(Time.realtimeSinceStartup * 1000f);
			if (PhotonNetwork.Boolean_8 && num > int_1)
			{
				PhotonNetwork.networkingPeer_0.RunViewUpdate();
				int_1 = num + updateIntervalOnSerialize;
				int_0 = 0;
			}
			num = (int)(Time.realtimeSinceStartup * 1000f);
			if (num > int_0)
			{
				bool flag2 = true;
				while (PhotonNetwork.Boolean_8 && flag2)
				{
					flag2 = PhotonNetwork.networkingPeer_0.SendOutgoingCommands();
				}
				int_0 = num + updateInterval;
			}
		}
	}

	protected void OnLevelWasLoaded(int int_2)
	{
		PhotonNetwork.networkingPeer_0.NewSceneLoaded();
		PhotonNetwork.networkingPeer_0.SetLevelInPropsIfSynced(Application.loadedLevelName);
	}

	protected void OnJoinedRoom()
	{
		PhotonNetwork.networkingPeer_0.LoadLevelIfSynced();
	}

	protected void OnCreatedRoom()
	{
		PhotonNetwork.networkingPeer_0.SetLevelInPropsIfSynced(Application.loadedLevelName);
	}

	public static void StartFallbackSendAckThread()
	{
		if (!bool_0)
		{
			bool_0 = true;
			SupportClass.CallInBackground(FallbackSendAckThread);
		}
	}

	public static void StopFallbackSendAckThread()
	{
		bool_0 = false;
	}

	public static bool FallbackSendAckThread()
	{
		if (bool_0 && PhotonNetwork.networkingPeer_0 != null)
		{
			PhotonNetwork.networkingPeer_0.SendAcksOnly();
		}
		return bool_0;
	}

	public void DebugReturn(DebugLevel level, string message)
	{
		switch (level)
		{
		case DebugLevel.ERROR:
			Debug.LogError(message);
			return;
		case DebugLevel.WARNING:
			Debug.LogWarning(message);
			return;
		case DebugLevel.INFO:
			if (PhotonNetwork.photonLogLevel_0 >= PhotonLogLevel.Informational)
			{
				Debug.Log(message);
				return;
			}
			break;
		}
		if (level == DebugLevel.ALL && PhotonNetwork.photonLogLevel_0 == PhotonLogLevel.Full)
		{
			Debug.Log(message);
		}
	}

	public void OnOperationResponse(OperationResponse operationResponse)
	{
	}

	public void OnStatusChanged(StatusCode statusCode)
	{
	}

	public void OnEvent(EventData eventData)
	{
	}

	protected internal static void PingAvailableRegionsAndConnectToBest()
	{
		photonHandler_0.StartCoroutine(photonHandler_0.PingAvailableRegionsCoroutine(true));
	}

	internal IEnumerator PingAvailableRegionsCoroutine(bool bool_2)
	{
		cloudRegionCode_0 = CloudRegionCode.none;
		PhotonPingManager photonPingManager;
		while (true)
		{
			if (PhotonNetwork.networkingPeer_0.List_0 != null)
			{
				if (PhotonNetwork.networkingPeer_0.List_0 != null && PhotonNetwork.networkingPeer_0.List_0.Count != 0)
				{
					photonPingManager = new PhotonPingManager();
					foreach (Region item in PhotonNetwork.networkingPeer_0.List_0)
					{
						photonHandler_0.StartCoroutine(photonPingManager.PingSocket(item));
					}
					break;
				}
				Debug.LogError("No regions available. Are you sure your appid is valid and setup?");
				yield break;
			}
			if (PhotonNetwork.PeerState_0 != PeerState.ConnectingToNameServer && PhotonNetwork.PeerState_0 != PeerState.ConnectedToNameServer)
			{
				Debug.LogError("Call ConnectToNameServer to ping available regions.");
				yield break;
			}
			Debug.Log(string.Concat("Waiting for AvailableRegions. State: ", PhotonNetwork.PeerState_0, " Server: ", PhotonNetwork.ServerConnection_0, " PhotonNetwork.networkingPeer.AvailableRegions ", PhotonNetwork.networkingPeer_0.List_0 != null));
			yield return new WaitForSeconds(0.25f);
		}
		while (!photonPingManager.Boolean_0)
		{
			yield return new WaitForSeconds(0.1f);
		}
		Region region_ = photonPingManager.Region_0;
		cloudRegionCode_0 = region_.cloudRegionCode_0;
		CloudRegionCode_0 = region_.cloudRegionCode_0;
		Debug.Log(string.Concat("Found best region: ", region_.cloudRegionCode_0, " ping: ", region_.int_0, ". Calling ConnectToRegionMaster() is: ", bool_2));
		if (bool_2)
		{
			PhotonNetwork.networkingPeer_0.ConnectToRegionMaster(region_.cloudRegionCode_0);
		}
	}
}
