using ExitGames.Client.Photon;

internal static class ScoreExtensions
{
	public static void SetScore(this PhotonPlayer photonPlayer_0, int int_0)
	{
		Hashtable hashtable = new Hashtable();
		hashtable["score"] = int_0;
		photonPlayer_0.SetCustomProperties(hashtable);
	}

	public static void AddScore(this PhotonPlayer photonPlayer_0, int int_0)
	{
		int score = photonPlayer_0.GetScore();
		score += int_0;
		Hashtable hashtable = new Hashtable();
		hashtable["score"] = score;
		photonPlayer_0.SetCustomProperties(hashtable);
	}

	public static int GetScore(this PhotonPlayer photonPlayer_0)
	{
		object value;
		if (photonPlayer_0.Hashtable_0.TryGetValue("score", out value))
		{
			return (int)value;
		}
		return 0;
	}
}
