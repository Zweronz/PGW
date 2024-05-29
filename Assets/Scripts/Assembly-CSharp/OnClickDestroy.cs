using Photon;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class OnClickDestroy : Photon.MonoBehaviour
{
	public bool bool_0;

	private void OnClick()
	{
		if (!bool_0)
		{
			PhotonNetwork.Destroy(base.gameObject);
		}
		else
		{
			base.PhotonView_0.RPC("DestroyRpc", PhotonTargets.AllBuffered);
		}
	}

	[RPC]
	public void DestroyRpc()
	{
		Object.Destroy(base.gameObject);
		PhotonNetwork.UnAllocateViewID(base.PhotonView_0.Int32_1);
	}
}
