using UnityEngine;

public sealed class ActiveObjectFromTimerNetwork : ActiveObjectFromTimer
{
	private PhotonView photonView_0;

	private bool Boolean_0
	{
		get
		{
			return PhotonNetwork.Boolean_9 && photonView_0.Boolean_1;
		}
	}

	protected override void Awake()
	{
		photonView_0 = PhotonView.Get(this);
		if (photonView_0 == null)
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			base.Awake();
		}
	}

	protected override void Update()
	{
		if (Boolean_0)
		{
			base.Update();
		}
	}

	protected override void DestroyObject()
	{
		PhotonNetwork.Destroy(ActivateObject);
	}

	protected override void SetActiveObject(bool bool_0)
	{
		base.SetActiveObject(bool_0);
		photonView_0.RPC("SetActiveObjectRPC", PhotonTargets.Others, bool_0);
	}

	public void OnPhotonPlayerConnected(PhotonPlayer photonPlayer_0)
	{
		if (Boolean_0)
		{
			photonView_0.RPC("SetActiveObjectRPC", photonPlayer_0, ActivateObject.activeSelf);
		}
	}

	[RPC]
	public void SetActiveObjectRPC(bool bool_0)
	{
		base.SetActiveObject(bool_0);
	}
}
