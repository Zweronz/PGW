using System.Collections.Generic;
using Photon;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PickupItemSyncer : Photon.MonoBehaviour
{
	private const float float_0 = 0.2f;

	public bool bool_0;

	public void OnPhotonPlayerConnected(PhotonPlayer photonPlayer_0)
	{
		if (PhotonNetwork.Boolean_9)
		{
			SendPickedUpItems(photonPlayer_0);
		}
	}

	public void OnJoinedRoom()
	{
		Debug.Log("Joined Room. isMasterClient: " + PhotonNetwork.Boolean_9 + " id: " + PhotonNetwork.PhotonPlayer_0.Int32_0);
		bool_0 = !PhotonNetwork.Boolean_9;
		if (PhotonNetwork.PhotonPlayer_2.Length >= 2)
		{
			Invoke("AskForPickupItemSpawnTimes", 2f);
		}
	}

	public void AskForPickupItemSpawnTimes()
	{
		if (!bool_0)
		{
			return;
		}
		if (PhotonNetwork.PhotonPlayer_2.Length < 2)
		{
			Debug.Log("Cant ask anyone else for PickupItem spawn times.");
			bool_0 = false;
			return;
		}
		PhotonPlayer next = PhotonNetwork.PhotonPlayer_1.GetNext();
		if (next == null || next.Equals(PhotonNetwork.PhotonPlayer_0))
		{
			next = PhotonNetwork.PhotonPlayer_0.GetNext();
		}
		if (next != null && !next.Equals(PhotonNetwork.PhotonPlayer_0))
		{
			base.PhotonView_0.RPC("RequestForPickupTimes", next);
			return;
		}
		Debug.Log("No player left to ask");
		bool_0 = false;
	}

	[RPC]
	public void RequestForPickupTimes(PhotonMessageInfo photonMessageInfo_0)
	{
		if (photonMessageInfo_0.photonPlayer_0 == null)
		{
			Debug.LogError("Unknown player asked for PickupItems");
		}
		else
		{
			SendPickedUpItems(photonMessageInfo_0.photonPlayer_0);
		}
	}

	private void SendPickedUpItems(PhotonPlayer photonPlayer_0)
	{
		if (photonPlayer_0 == null)
		{
			Debug.LogWarning("Cant send PickupItem spawn times to unknown targetPlayer.");
			return;
		}
		double double_ = PhotonNetwork.Double_0;
		double num = double_ + 0.20000000298023224;
		PickupItem[] array = new PickupItem[PickupItem.hashSet_0.Count];
		PickupItem.hashSet_0.CopyTo(array);
		List<float> list = new List<float>(array.Length * 2);
		foreach (PickupItem pickupItem in array)
		{
			if (pickupItem.float_0 <= 0f)
			{
				list.Add(pickupItem.Int32_0);
				list.Add(0f);
				continue;
			}
			double num2 = pickupItem.double_0 - PhotonNetwork.Double_0;
			if (pickupItem.double_0 > num)
			{
				Debug.Log(pickupItem.Int32_0 + " respawn: " + pickupItem.double_0 + " timeUntilRespawn: " + num2 + " (now: " + PhotonNetwork.Double_0 + ")");
				list.Add(pickupItem.Int32_0);
				list.Add((float)num2);
			}
		}
		Debug.Log("Sent count: " + list.Count + " now: " + double_);
		base.PhotonView_0.RPC("PickupItemInit", photonPlayer_0, PhotonNetwork.Double_0, list.ToArray());
	}

	[RPC]
	public void PickupItemInit(double double_0, float[] float_1)
	{
		bool_0 = false;
		for (int i = 0; i < float_1.Length / 2; i++)
		{
			int num = i * 2;
			int int_ = (int)float_1[num];
			float num2 = float_1[num + 1];
			PhotonView photonView = PhotonView.Find(int_);
			PickupItem component = photonView.GetComponent<PickupItem>();
			if (num2 <= 0f)
			{
				component.PickedUp(0f);
				continue;
			}
			double num3 = (double)num2 + double_0;
			Debug.Log(photonView.Int32_1 + " respawn: " + num3 + " timeUntilRespawnBasedOnTimeBase:" + num2 + " SecondsBeforeRespawn: " + component.float_0);
			double num4 = num3 - PhotonNetwork.Double_0;
			if (num2 <= 0f)
			{
				num4 = 0.0;
			}
			component.PickedUp((float)num4);
		}
	}
}
