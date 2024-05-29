using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using UnityEngine;

public class PhotonLagSimulationGui : MonoBehaviour
{
	public Rect WindowRect = new Rect(0f, 100f, 120f, 100f);

	public int WindowId = 101;

	public bool Visible = true;

	[CompilerGenerated]
	private PhotonPeer photonPeer_0;

	public PhotonPeer PhotonPeer_0
	{
		[CompilerGenerated]
		get
		{
			return photonPeer_0;
		}
		[CompilerGenerated]
		set
		{
			photonPeer_0 = value;
		}
	}

	public void Start()
	{
		PhotonPeer_0 = PhotonNetwork.networkingPeer_0;
	}

	public void OnGUI()
	{
		if (Visible)
		{
			if (PhotonPeer_0 == null)
			{
				WindowRect = GUILayout.Window(WindowId, WindowRect, NetSimHasNoPeerWindow, "Netw. Sim.");
			}
			else
			{
				WindowRect = GUILayout.Window(WindowId, WindowRect, NetSimWindow, "Netw. Sim.");
			}
		}
	}

	private void NetSimHasNoPeerWindow(int int_0)
	{
		GUILayout.Label("No peer to communicate with. ");
	}

	private void NetSimWindow(int int_0)
	{
		GUILayout.Label(string.Format("Rtt:{0,4} +/-{1,3}", PhotonPeer_0.RoundTripTime, PhotonPeer_0.RoundTripTimeVariance));
		bool isSimulationEnabled2;
		bool isSimulationEnabled;
		if ((isSimulationEnabled2 = GUILayout.Toggle(isSimulationEnabled = PhotonPeer_0.IsSimulationEnabled, "Simulate")) != isSimulationEnabled)
		{
			PhotonPeer_0.IsSimulationEnabled = isSimulationEnabled2;
		}
		float num = PhotonPeer_0.NetworkSimulationSettings.IncomingLag;
		GUILayout.Label("Lag " + num);
		num = GUILayout.HorizontalSlider(num, 0f, 500f);
		PhotonPeer_0.NetworkSimulationSettings.IncomingLag = (int)num;
		PhotonPeer_0.NetworkSimulationSettings.OutgoingLag = (int)num;
		float num2 = PhotonPeer_0.NetworkSimulationSettings.IncomingJitter;
		GUILayout.Label("Jit " + num2);
		num2 = GUILayout.HorizontalSlider(num2, 0f, 100f);
		PhotonPeer_0.NetworkSimulationSettings.IncomingJitter = (int)num2;
		PhotonPeer_0.NetworkSimulationSettings.OutgoingJitter = (int)num2;
		float num3 = PhotonPeer_0.NetworkSimulationSettings.IncomingLossPercentage;
		GUILayout.Label("Loss " + num3);
		num3 = GUILayout.HorizontalSlider(num3, 0f, 10f);
		PhotonPeer_0.NetworkSimulationSettings.IncomingLossPercentage = (int)num3;
		PhotonPeer_0.NetworkSimulationSettings.OutgoingLossPercentage = (int)num3;
		if (GUI.changed)
		{
			WindowRect.height = 100f;
		}
		GUI.DragWindow();
	}
}
