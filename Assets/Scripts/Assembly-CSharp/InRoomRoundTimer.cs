using ExitGames.Client.Photon;
using UnityEngine;

public class InRoomRoundTimer : MonoBehaviour
{
	private const string string_0 = "st";

	public int SecondsPerTurn = 5;

	public double StartTime;

	public Rect TextPos = new Rect(0f, 80f, 150f, 300f);

	private bool bool_0;

	private void StartRoundNow()
	{
		if (PhotonNetwork.Double_0 < 9.999999747378752E-05)
		{
			bool_0 = true;
			return;
		}
		bool_0 = false;
		Hashtable hashtable = new Hashtable();
		hashtable["st"] = PhotonNetwork.Double_0;
		PhotonNetwork.Room_0.SetCustomProperties(hashtable);
	}

	public void OnJoinedRoom()
	{
		if (PhotonNetwork.Boolean_9)
		{
			StartRoundNow();
		}
		else
		{
			Debug.Log("StartTime already set: " + PhotonNetwork.Room_0.Hashtable_0.ContainsKey("st"));
		}
	}

	public void OnPhotonCustomRoomPropertiesChanged(Hashtable hashtable_0)
	{
		if (hashtable_0.ContainsKey("st"))
		{
			StartTime = (double)hashtable_0["st"];
		}
	}

	public void OnMasterClientSwitched(PhotonPlayer photonPlayer_0)
	{
		if (!PhotonNetwork.Room_0.Hashtable_0.ContainsKey("st"))
		{
			Debug.Log("The new master starts a new round, cause we didn't start yet.");
			StartRoundNow();
		}
	}

	private void Update()
	{
		if (bool_0)
		{
			StartRoundNow();
		}
	}

	public void OnGUI()
	{
		double num = PhotonNetwork.Double_0 - StartTime;
		double num2 = (double)SecondsPerTurn - num % (double)SecondsPerTurn;
		int num3 = (int)(num / (double)SecondsPerTurn);
		GUILayout.BeginArea(TextPos);
		GUILayout.Label(string.Format("elapsed: {0:0.000}", num));
		GUILayout.Label(string.Format("remaining: {0:0.000}", num2));
		GUILayout.Label(string.Format("turn: {0:0}", num3));
		if (GUILayout.Button("new round"))
		{
			StartRoundNow();
		}
		GUILayout.EndArea();
	}
}
