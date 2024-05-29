using System;
using System.Text;
using ExitGames.Client.Photon;
using UnityEngine;
using engine.helpers;

public class PhotonNetworkStatGui : MonoBehaviour
{
	private sealed class StatisticsData
	{
		private TrafficStatsGameLevel trafficStatsGameLevel_0;

		public int Int32_0
		{
			get
			{
				return Math.Max(1, (int)(PhotonNetwork.networkingPeer_0.TrafficStatsElapsedMs / 1000L));
			}
		}

		public string String_0
		{
			get
			{
				return string.Format("Elapsed time: {0} sec", Int32_0);
			}
		}

		public int Int32_1
		{
			get
			{
				return trafficStatsGameLevel_0.TotalIncomingMessageCount;
			}
		}

		public int Int32_2
		{
			get
			{
				return trafficStatsGameLevel_0.TotalOutgoingMessageCount;
			}
		}

		public int Int32_3
		{
			get
			{
				return trafficStatsGameLevel_0.TotalMessageCount;
			}
		}

		public string String_1
		{
			get
			{
				return string.Format("Total messages: \n Out|In|Sum: \n {0,4}|{1,4}|{2,4}", Int32_2, Int32_1, Int32_3);
			}
		}

		public int Int32_4
		{
			get
			{
				return Int32_1 / Int32_0;
			}
		}

		public int Int32_5
		{
			get
			{
				return Int32_2 / Int32_0;
			}
		}

		public int Int32_6
		{
			get
			{
				return Int32_3 / Int32_0;
			}
		}

		public string String_2
		{
			get
			{
				return string.Format("Average messages: \n Out|In|Sum: \n {0,4}|{1,4}|{2,4}", Int32_5, Int32_4, Int32_6);
			}
		}

		public string String_3
		{
			get
			{
				return "Incoming: \n" + PhotonNetwork.networkingPeer_0.TrafficStatsIncoming.ToString();
			}
		}

		public string String_4
		{
			get
			{
				return "Outgoing: \n" + PhotonNetwork.networkingPeer_0.TrafficStatsOutgoing.ToString();
			}
		}

		public int Int32_7
		{
			get
			{
				return trafficStatsGameLevel_0.LongestDeltaBetweenSending;
			}
		}

		public int Int32_8
		{
			get
			{
				return trafficStatsGameLevel_0.LongestDeltaBetweenDispatching;
			}
		}

		public string String_5
		{
			get
			{
				return string.Format("ping: {6}[+/-{7}]ms \n longest delta between \n send: {0,4}ms dispatching: {1,4}ms \n longest time for: \n event({3}):{2,3}ms op({5}):{4,3}ms", Int32_7, Int32_8, trafficStatsGameLevel_0.LongestEventCallback, trafficStatsGameLevel_0.LongestEventCallbackCode, trafficStatsGameLevel_0.LongestOpResponseCallback, trafficStatsGameLevel_0.LongestOpResponseCallbackOpCode, PhotonNetwork.networkingPeer_0.RoundTripTime, PhotonNetwork.networkingPeer_0.RoundTripTimeVariance);
			}
		}

		public StatisticsData()
		{
			trafficStatsGameLevel_0 = PhotonNetwork.networkingPeer_0.TrafficStatsGameLevel;
		}

		public void Reset()
		{
			PhotonNetwork.networkingPeer_0.TrafficStatsReset();
			trafficStatsGameLevel_0 = PhotonNetwork.networkingPeer_0.TrafficStatsGameLevel;
			PhotonNetwork.networkingPeer_0.TrafficStatsEnabled = true;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("[Elapsed Time in sec]: " + Int32_0);
			stringBuilder.AppendLine("[Messages statistics]:");
			stringBuilder.AppendLine("----- [Messages]: " + String_1);
			stringBuilder.AppendLine("----- [Messages average]: " + String_2);
			stringBuilder.AppendLine("[Traffic statistics]:");
			stringBuilder.AppendLine("----- [Incoming]: " + String_3);
			stringBuilder.AppendLine("----- [Outgoing]: " + String_4);
			stringBuilder.AppendLine("[Traffic statistics]: " + String_5);
			return stringBuilder.ToString();
		}
	}

	private bool bool_0;

	private bool bool_1 = true;

	private bool bool_2;

	private bool bool_3;

	private bool bool_4;

	private Rect rect_0 = new Rect(0f, 100f, 250f, 100f);

	private int int_0 = 100;

	private StatisticsData statisticsData_0;

	public int Int32_0
	{
		get
		{
			if (statisticsData_0 == null)
			{
				return 0;
			}
			return statisticsData_0.Int32_7;
		}
	}

	public int Int32_1
	{
		get
		{
			if (statisticsData_0 == null)
			{
				return 0;
			}
			return statisticsData_0.Int32_8;
		}
	}

	private void Start()
	{
		rect_0.x = (float)Screen.width - rect_0.width;
		statisticsData_0 = new StatisticsData();
	}

	private void OnDestroy()
	{
		statisticsData_0 = null;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F11))
		{
			bool_0 = !bool_0;
			bool_1 = true;
		}
	}

	private void OnGUI()
	{
		if (PhotonNetwork.networkingPeer_0.TrafficStatsEnabled != bool_1)
		{
			PhotonNetwork.networkingPeer_0.TrafficStatsEnabled = bool_1;
		}
		if (bool_0)
		{
			rect_0 = GUILayout.Window(int_0, rect_0, TrafficStatsWindow, "Network statistics (shift+J)");
		}
	}

	private void TrafficStatsWindow(int int_1)
	{
		if (statisticsData_0 != null)
		{
			GUILayout.Label(statisticsData_0.String_0);
			GUILayout.Label(statisticsData_0.String_1);
			GUILayout.Label(statisticsData_0.String_2);
			GUILayout.BeginHorizontal();
			bool_1 = GUILayout.Toggle(bool_1, "stats on");
			if (GUILayout.Button("Reset"))
			{
				statisticsData_0.Reset();
			}
			bool flag = GUILayout.Button("To Log");
			GUILayout.EndHorizontal();
			GUILayout.Label(statisticsData_0.String_3);
			GUILayout.Label(statisticsData_0.String_4);
			GUILayout.Label(statisticsData_0.String_5);
			if (flag)
			{
				Log.AddLine(statisticsData_0.ToString());
			}
			GUI.DragWindow();
		}
	}

	public void Reset()
	{
		if (statisticsData_0 != null)
		{
			statisticsData_0.Reset();
		}
	}

	public override string ToString()
	{
		if (statisticsData_0 != null)
		{
			return statisticsData_0.ToString();
		}
		return string.Empty;
	}
}
