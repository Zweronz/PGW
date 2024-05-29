using Photon;
using UnityEngine;

public class ConnectAndJoinRandom : Photon.MonoBehaviour
{
	public bool bool_0 = true;

	private bool bool_1 = true;

	public virtual void Start()
	{
		PhotonNetwork.Boolean_6 = false;
	}

	public virtual void Update()
	{
		if (bool_1 && bool_0 && !PhotonNetwork.Boolean_0)
		{
			Debug.Log("Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");
			bool_1 = false;
			PhotonNetwork.ConnectUsingSettings("2." + Application.loadedLevel);
		}
	}

	public virtual void OnConnectedToMaster()
	{
		if (PhotonNetwork.networkingPeer_0.List_0 != null)
		{
			Debug.LogWarning(string.Concat("List of available regions counts ", PhotonNetwork.networkingPeer_0.List_0.Count, ". First: ", PhotonNetwork.networkingPeer_0.List_0[0], " \t Current Region: ", PhotonNetwork.networkingPeer_0.CloudRegionCode_0));
		}
		Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
		PhotonNetwork.JoinRandomRoom();
	}

	public virtual void OnPhotonRandomJoinFailed()
	{
		Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
		PhotonNetwork.CreateRoom(null, new RoomOptions
		{
			int_0 = 4
		}, null);
	}

	public virtual void OnFailedToConnectToPhoton(DisconnectCause disconnectCause_0)
	{
		Debug.LogError("Cause: " + disconnectCause_0);
	}

	public void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");
	}

	public virtual void OnJoinedLobby()
	{
		Debug.Log("OnJoinedLobby(). Use a GUI to show existing rooms available in PhotonNetwork.GetRoomList().");
	}
}
