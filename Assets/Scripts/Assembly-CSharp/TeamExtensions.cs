using ExitGames.Client.Photon;
using UnityEngine;

internal static class TeamExtensions
{
	public static PunTeams.Team GetTeam(this PhotonPlayer photonPlayer_0)
	{
		object value;
		if (photonPlayer_0.Hashtable_0.TryGetValue("team", out value))
		{
			return (PunTeams.Team)(byte)value;
		}
		return PunTeams.Team.none;
	}

	public static void SetTeam(this PhotonPlayer photonPlayer_0, PunTeams.Team team_0)
	{
		if (!PhotonNetwork.Boolean_2)
		{
			Debug.LogWarning(string.Concat("JoinTeam was called in state: ", PhotonNetwork.PeerState_0, ". Not connectedAndReady."));
		}
		PunTeams.Team team = PhotonNetwork.PhotonPlayer_0.GetTeam();
		if (team != team_0)
		{
			PhotonNetwork.PhotonPlayer_0.SetCustomProperties(new Hashtable { 
			{
				"team",
				(byte)team_0
			} });
		}
	}
}
