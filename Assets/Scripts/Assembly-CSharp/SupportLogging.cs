using System.Text;
using UnityEngine;

public class SupportLogging : MonoBehaviour
{
	public bool LogTrafficStats;

	public void Start()
	{
		if (LogTrafficStats)
		{
			InvokeRepeating("LogStats", 10f, 10f);
		}
	}

	public void OnApplicationQuit()
	{
		CancelInvoke();
	}

	public void LogStats()
	{
		if (LogTrafficStats)
		{
			Debug.Log("SupportLogger " + PhotonNetwork.NetworkStatisticsToString());
		}
	}

	private void LogBasics()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("SupportLogger Info: PUN {0}: ", "1.28");
		stringBuilder.AppendFormat("AppID: {0}*** GameVersion: {1} ", PhotonNetwork.networkingPeer_0.string_2.Substring(0, 8), PhotonNetwork.networkingPeer_0.String_0);
		stringBuilder.AppendFormat("Server: {0}. Region: {1} ", PhotonNetwork.String_1, PhotonNetwork.networkingPeer_0.CloudRegionCode_0);
		stringBuilder.AppendFormat("HostType: {0} ", PhotonNetwork.serverSettings_0.HostType);
		Debug.Log(stringBuilder.ToString());
	}

	public void OnConnectedToPhoton()
	{
		Debug.Log("SupportLogger OnConnectedToPhoton().");
		LogBasics();
		if (LogTrafficStats)
		{
			PhotonNetwork.Boolean_12 = true;
		}
	}

	public void OnFailedToConnectToPhoton(DisconnectCause disconnectCause_0)
	{
		Debug.Log(string.Concat("SupportLogger OnFailedToConnectToPhoton(", disconnectCause_0, ")."));
		LogBasics();
	}

	public void OnJoinedLobby()
	{
		Debug.Log(string.Concat("SupportLogger OnJoinedLobby(", PhotonNetwork.TypedLobby_0, ")."));
	}

	public void OnJoinedRoom()
	{
		Debug.Log(string.Concat("SupportLogger OnJoinedRoom(", PhotonNetwork.Room_0, "). ", PhotonNetwork.TypedLobby_0));
	}

	public void OnCreatedRoom()
	{
		Debug.Log(string.Concat("SupportLogger OnCreatedRoom(", PhotonNetwork.Room_0, "). ", PhotonNetwork.TypedLobby_0));
	}

	public void OnLeftRoom()
	{
		Debug.Log("SupportLogger OnLeftRoom().");
	}
}
