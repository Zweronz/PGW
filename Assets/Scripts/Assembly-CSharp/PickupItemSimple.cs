using Photon;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PickupItemSimple : Photon.MonoBehaviour
{
	public float float_0 = 2f;

	public bool bool_0;

	public bool bool_1;

	public void OnTriggerEnter(Collider collider_0)
	{
		PhotonView component = collider_0.GetComponent<PhotonView>();
		if (bool_0 && component != null && component.Boolean_1)
		{
			Pickup();
		}
	}

	public void Pickup()
	{
		if (!bool_1)
		{
			bool_1 = true;
			base.PhotonView_0.RPC("PunPickupSimple", PhotonTargets.AllViaServer);
		}
	}

	[RPC]
	public void PunPickupSimple(PhotonMessageInfo photonMessageInfo_0)
	{
		if (!bool_1 || !photonMessageInfo_0.photonPlayer_0.bool_0 || base.gameObject.GetActive())
		{
		}
		bool_1 = false;
		if (!base.gameObject.GetActive())
		{
			Debug.Log("Ignored PU RPC, cause item is inactive. " + base.gameObject);
			return;
		}
		double num = PhotonNetwork.Double_0 - photonMessageInfo_0.Double_0;
		float num2 = float_0 - (float)num;
		if (num2 > 0f)
		{
			base.gameObject.SetActive(false);
			Invoke("RespawnAfter", num2);
		}
	}

	public void RespawnAfter()
	{
		if (base.gameObject != null)
		{
			base.gameObject.SetActive(true);
		}
	}
}
