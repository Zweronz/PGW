using System;
using UnityEngine;

internal sealed class ZombiSynchPhoton : MonoBehaviour
{
	private ThirdPersonCamera thirdPersonCamera_0;

	private ThirdPersonController thirdPersonController_0;

	private PhotonView photonView_0;

	public int int_0;

	private Vector3 vector3_0 = Vector3.zero;

	private Quaternion quaternion_0 = Quaternion.identity;

	private Transform transform_0;

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

	private void Start()
	{
		try
		{
			transform_0 = base.transform;
			photonView_0 = PhotonView.Get(this);
			vector3_0 = transform_0.position;
			quaternion_0 = transform_0.rotation;
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
			photonStream_0.SendNext(transform_0.position);
			photonStream_0.SendNext(transform_0.rotation);
		}
		else
		{
			vector3_0 = (Vector3)photonStream_0.ReceiveNext();
			quaternion_0 = (Quaternion)photonStream_0.ReceiveNext();
		}
	}

	private void Update()
	{
		try
		{
			if (!photonView_0.Boolean_1)
			{
				transform_0.position = Vector3.Lerp(transform_0.position, vector3_0, Time.deltaTime * 5f);
				transform_0.rotation = Quaternion.Lerp(transform_0.rotation, quaternion_0, Time.deltaTime * 5f);
				if (int_0 < 10)
				{
					int_0++;
				}
			}
		}
		catch (Exception exception)
		{
			Debug.LogError("Cooperative mode failure.");
			Debug.LogException(exception);
			throw;
		}
	}
}
