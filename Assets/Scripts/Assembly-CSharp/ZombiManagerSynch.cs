using System;
using UnityEngine;

internal sealed class ZombiManagerSynch : MonoBehaviour
{
	private ThirdPersonCamera thirdPersonCamera_0;

	private ThirdPersonController thirdPersonController_0;

	private Vector3 vector3_0 = Vector3.zero;

	private Quaternion quaternion_0 = Quaternion.identity;

	private void Awake()
	{
		try
		{
			if (!Defs.bool_2 || !Defs.bool_3)
			{
				base.enabled = false;
			}
		}
		catch (Exception exception)
		{
			Debug.LogError("Cooperative mode failure.");
			Debug.LogException(exception);
			throw;
		}
	}

	private void OnPhotonSerializeView(PhotonStream photonStream_0, PhotonMessageInfo photonMessageInfo_0)
	{
		if (photonStream_0.Boolean_0)
		{
			photonStream_0.SendNext(base.transform.position);
		}
		else
		{
			vector3_0 = (Vector3)photonStream_0.ReceiveNext();
		}
	}
}
