using System;
using System.Collections.Generic;
using UnityEngine;

public class PunTeams : MonoBehaviour
{
	public enum Team : byte
	{
		none = 0,
		red = 1,
		blue = 2
	}

	public const string string_0 = "team";

	public static Dictionary<Team, List<PhotonPlayer>> dictionary_0;

	public void Start()
	{
		dictionary_0 = new Dictionary<Team, List<PhotonPlayer>>();
		Array values = Enum.GetValues(typeof(Team));
		foreach (object item in values)
		{
			dictionary_0[(Team)(byte)item] = new List<PhotonPlayer>();
		}
	}

	public void OnJoinedRoom()
	{
		UpdateTeams();
	}

	public void OnPhotonPlayerPropertiesChanged(object[] object_0)
	{
		UpdateTeams();
	}

	public void UpdateTeams()
	{
		Array values = Enum.GetValues(typeof(Team));
		foreach (object item in values)
		{
			dictionary_0[(Team)(byte)item].Clear();
		}
		for (int i = 0; i < PhotonNetwork.PhotonPlayer_2.Length; i++)
		{
			PhotonPlayer photonPlayer = PhotonNetwork.PhotonPlayer_2[i];
			Team team = photonPlayer.GetTeam();
			dictionary_0[team].Add(photonPlayer);
		}
	}
}
