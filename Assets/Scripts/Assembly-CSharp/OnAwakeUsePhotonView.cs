using Photon;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class OnAwakeUsePhotonView : Photon.MonoBehaviour
{
	private void Awake()
	{
		if (base.PhotonView_0.Boolean_1)
		{
			base.PhotonView_0.RPC("OnAwakeRPC", PhotonTargets.All);
		}
	}

	private void Start()
	{
		if (base.PhotonView_0.Boolean_1)
		{
			base.PhotonView_0.RPC("OnAwakeRPC", PhotonTargets.All, (byte)1);
		}
	}

	[RPC]
	public void OnAwakeRPC()
	{
		Debug.Log("RPC: 'OnAwakeRPC' PhotonView: " + base.PhotonView_0);
	}

	[RPC]
	public void OnAwakeRPC(byte byte_0)
	{
		Debug.Log("RPC: 'OnAwakeRPC' Parameter: " + byte_0 + " PhotonView: " + base.PhotonView_0);
	}
}
