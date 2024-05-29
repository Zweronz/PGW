using System.Collections.Generic;
using Photon;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PickupItem : Photon.MonoBehaviour, IPunObservable
{
	public float float_0 = 2f;

	public bool bool_0;

	public bool bool_1;

	public UnityEngine.MonoBehaviour monoBehaviour_0;

	public bool bool_2;

	public double double_0;

	public static HashSet<PickupItem> hashSet_0 = new HashSet<PickupItem>();

	public int Int32_0
	{
		get
		{
			return base.PhotonView_0.Int32_1;
		}
	}

	public void OnTriggerEnter(Collider collider_0)
	{
		PhotonView component = collider_0.GetComponent<PhotonView>();
		if (bool_0 && component != null && component.Boolean_1)
		{
			Pickup();
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.Boolean_0 && float_0 <= 0f)
		{
			stream.SendNext(base.gameObject.transform.position);
			return;
		}
		Vector3 position = (Vector3)stream.ReceiveNext();
		base.gameObject.transform.position = position;
	}

	public void Pickup()
	{
		if (!bool_2)
		{
			bool_2 = true;
			base.PhotonView_0.RPC("PunPickup", PhotonTargets.AllViaServer);
		}
	}

	public void Drop()
	{
		if (bool_1)
		{
			base.PhotonView_0.RPC("PunRespawn", PhotonTargets.AllViaServer);
		}
	}

	public void Drop(Vector3 vector3_0)
	{
		if (bool_1)
		{
			base.PhotonView_0.RPC("PunRespawn", PhotonTargets.AllViaServer, vector3_0);
		}
	}

	[RPC]
	public void PunPickup(PhotonMessageInfo photonMessageInfo_0)
	{
		if (photonMessageInfo_0.photonPlayer_0.bool_0)
		{
			bool_2 = false;
		}
		if (!base.gameObject.GetActive())
		{
			Debug.Log(string.Concat("Ignored PU RPC, cause item is inactive. ", base.gameObject, " SecondsBeforeRespawn: ", float_0, " TimeOfRespawn: ", double_0, " respawn in future: ", double_0 > PhotonNetwork.Double_0));
			return;
		}
		bool_1 = photonMessageInfo_0.photonPlayer_0.bool_0;
		if (monoBehaviour_0 != null)
		{
			monoBehaviour_0.SendMessage("OnPickedUp", this);
		}
		if (float_0 <= 0f)
		{
			PickedUp(0f);
			return;
		}
		double num = PhotonNetwork.Double_0 - photonMessageInfo_0.Double_0;
		double num2 = (double)float_0 - num;
		if (num2 > 0.0)
		{
			PickedUp((float)num2);
		}
	}

	internal void PickedUp(float float_1)
	{
		base.gameObject.SetActive(false);
		hashSet_0.Add(this);
		double_0 = 0.0;
		if (float_1 > 0f)
		{
			double_0 = PhotonNetwork.Double_0 + (double)float_1;
			Invoke("PunRespawn", float_1);
		}
	}

	[RPC]
	internal void PunRespawn(Vector3 vector3_0)
	{
		Debug.Log("PunRespawn with Position.");
		PunRespawn();
		base.gameObject.transform.position = vector3_0;
	}

	[RPC]
	internal void PunRespawn()
	{
		hashSet_0.Remove(this);
		double_0 = 0.0;
		bool_1 = false;
		if (base.gameObject != null)
		{
			base.gameObject.SetActive(true);
		}
	}
}
