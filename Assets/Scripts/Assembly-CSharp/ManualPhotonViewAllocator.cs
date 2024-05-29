using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class ManualPhotonViewAllocator : MonoBehaviour
{
	public GameObject Prefab;

	public void AllocateManualPhotonView()
	{
		PhotonView photonView = base.gameObject.GetPhotonView();
		if (photonView == null)
		{
			Debug.LogError("Can't do manual instantiation without PhotonView component.");
			return;
		}
		int num = PhotonNetwork.AllocateViewID();
		photonView.RPC("InstantiateRpc", PhotonTargets.AllBuffered, num);
	}

	[RPC]
	public void InstantiateRpc(int int_0)
	{
		GameObject gameObject = Object.Instantiate(Prefab, InputToEvent.vector3_0 + new Vector3(0f, 5f, 0f), Quaternion.identity) as GameObject;
		gameObject.GetPhotonView().Int32_1 = int_0;
		OnClickDestroy component = gameObject.GetComponent<OnClickDestroy>();
		component.bool_0 = true;
	}
}
