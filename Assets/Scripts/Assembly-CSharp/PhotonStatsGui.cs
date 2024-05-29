using ExitGames.Client.Photon;
using UnityEngine;

public class PhotonStatsGui : MonoBehaviour
{
	public bool statsWindowOn = true;

	public bool statsOn = true;

	public bool healthStatsVisible;

	public bool trafficStatsOn;

	public bool buttonsOn;

	public Rect statsRect = new Rect(0f, 100f, 200f, 50f);

	public int WindowId = 100;

	public void Start()
	{
		statsRect.x = (float)Screen.width - statsRect.width;
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
		{
			statsWindowOn = !statsWindowOn;
			statsOn = true;
		}
	}

	public void OnGUI()
	{
		if (PhotonNetwork.networkingPeer_0.TrafficStatsEnabled != statsOn)
		{
			PhotonNetwork.networkingPeer_0.TrafficStatsEnabled = statsOn;
		}
		if (statsWindowOn)
		{
			statsRect = GUILayout.Window(WindowId, statsRect, TrafficStatsWindow, "Messages (shift+tab)");
		}
	}

	public void TrafficStatsWindow(int int_0)
	{
		bool flag = false;
		TrafficStatsGameLevel trafficStatsGameLevel = PhotonNetwork.networkingPeer_0.TrafficStatsGameLevel;
		long num = PhotonNetwork.networkingPeer_0.TrafficStatsElapsedMs / 1000L;
		if (num == 0L)
		{
			num = 1L;
		}
		GUILayout.BeginHorizontal();
		buttonsOn = GUILayout.Toggle(buttonsOn, "buttons");
		healthStatsVisible = GUILayout.Toggle(healthStatsVisible, "health");
		trafficStatsOn = GUILayout.Toggle(trafficStatsOn, "traffic");
		GUILayout.EndHorizontal();
		string text = string.Format("Out|In|Sum:\t{0,4} | {1,4} | {2,4}", trafficStatsGameLevel.TotalOutgoingMessageCount, trafficStatsGameLevel.TotalIncomingMessageCount, trafficStatsGameLevel.TotalMessageCount);
		string text2 = string.Format("{0}sec average:", num);
		string text3 = string.Format("Out|In|Sum:\t{0,4} | {1,4} | {2,4}", trafficStatsGameLevel.TotalOutgoingMessageCount / num, trafficStatsGameLevel.TotalIncomingMessageCount / num, trafficStatsGameLevel.TotalMessageCount / num);
		GUILayout.Label(text);
		GUILayout.Label(text2);
		GUILayout.Label(text3);
		if (buttonsOn)
		{
			GUILayout.BeginHorizontal();
			statsOn = GUILayout.Toggle(statsOn, "stats on");
			if (GUILayout.Button("Reset"))
			{
				PhotonNetwork.networkingPeer_0.TrafficStatsReset();
				PhotonNetwork.networkingPeer_0.TrafficStatsEnabled = true;
			}
			flag = GUILayout.Button("To Log");
			GUILayout.EndHorizontal();
		}
		string text4 = string.Empty;
		string text5 = string.Empty;
		if (trafficStatsOn)
		{
			text4 = "Incoming: " + PhotonNetwork.networkingPeer_0.TrafficStatsIncoming.ToString();
			text5 = "Outgoing: " + PhotonNetwork.networkingPeer_0.TrafficStatsOutgoing.ToString();
			GUILayout.Label(text4);
			GUILayout.Label(text5);
		}
		string text6 = string.Empty;
		if (healthStatsVisible)
		{
			text6 = string.Format("ping: {6}[+/-{7}]ms\nlongest delta between\nsend: {0,4}ms disp: {1,4}ms\nlongest time for:\nev({3}):{2,3}ms op({5}):{4,3}ms", trafficStatsGameLevel.LongestDeltaBetweenSending, trafficStatsGameLevel.LongestDeltaBetweenDispatching, trafficStatsGameLevel.LongestEventCallback, trafficStatsGameLevel.LongestEventCallbackCode, trafficStatsGameLevel.LongestOpResponseCallback, trafficStatsGameLevel.LongestOpResponseCallbackOpCode, PhotonNetwork.networkingPeer_0.RoundTripTime, PhotonNetwork.networkingPeer_0.RoundTripTimeVariance);
			GUILayout.Label(text6);
		}
		if (flag)
		{
			string message = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}", text, text2, text3, text4, text5, text6);
			Debug.Log(message);
		}
		if (GUI.changed)
		{
			statsRect.height = 100f;
		}
		GUI.DragWindow();
	}
}
