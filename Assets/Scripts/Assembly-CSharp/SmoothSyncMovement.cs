using Photon;
using UnityEngine;

public class SmoothSyncMovement : Photon.MonoBehaviour
{
	public float float_0 = 5f;

	private Vector3 vector3_0 = Vector3.zero;

	private Quaternion quaternion_0 = Quaternion.identity;

	public void Awake()
	{
		if (base.PhotonView_0 == null || base.PhotonView_0.component_0 != this)
		{
			Debug.LogWarning(string.Concat(this, " is not observed by this object's photonView! OnPhotonSerializeView() in this class won't be used."));
		}
	}

	public void OnPhotonSerializeView(PhotonStream photonStream_0, PhotonMessageInfo photonMessageInfo_0)
	{
		if (photonStream_0.Boolean_0)
		{
			photonStream_0.SendNext(base.transform.position);
			photonStream_0.SendNext(base.transform.rotation);
		}
		else
		{
			vector3_0 = (Vector3)photonStream_0.ReceiveNext();
			quaternion_0 = (Quaternion)photonStream_0.ReceiveNext();
		}
	}

	public void Update()
	{
		if (!base.PhotonView_0.Boolean_1)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, vector3_0, Time.deltaTime * float_0);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, quaternion_0, Time.deltaTime * float_0);
		}
	}
}
