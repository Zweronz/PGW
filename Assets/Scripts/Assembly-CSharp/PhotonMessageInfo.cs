public class PhotonMessageInfo
{
	private int int_0;

	public PhotonPlayer photonPlayer_0;

	public PhotonView photonView_0;

	public double Double_0
	{
		get
		{
			return (double)(uint)int_0 / 1000.0;
		}
	}

	public PhotonMessageInfo()
	{
		photonPlayer_0 = PhotonNetwork.PhotonPlayer_0;
		int_0 = (int)(PhotonNetwork.Double_0 * 1000.0);
		photonView_0 = null;
	}

	public PhotonMessageInfo(PhotonPlayer photonPlayer_1, int int_1, PhotonView photonView_1)
	{
		photonPlayer_0 = photonPlayer_1;
		int_0 = int_1;
		photonView_0 = photonView_1;
	}

	public override string ToString()
	{
		return string.Format("[PhotonMessageInfo: player='{1}' timestamp={0}]", Double_0, photonPlayer_0);
	}
}
