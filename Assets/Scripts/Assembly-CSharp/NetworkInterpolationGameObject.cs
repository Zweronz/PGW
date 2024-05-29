using UnityEngine;

public class NetworkInterpolationGameObject : MonoBehaviour
{
	private Quaternion quaternion_0 = Quaternion.identity;

	private void Awake()
	{
		if (!Defs.bool_2 || Defs.bool_3)
		{
			base.enabled = false;
		}
	}

	private void OnSerializeNetworkView(BitStream bitStream_0, NetworkMessageInfo networkMessageInfo_0)
	{
		if (bitStream_0.isWriting)
		{
			Quaternion value = base.transform.localRotation;
			bitStream_0.Serialize(ref value);
		}
		else
		{
			Quaternion value2 = Quaternion.identity;
			bitStream_0.Serialize(ref value2);
			quaternion_0 = value2;
		}
	}

	private void Update()
	{
		if (!base.GetComponent<NetworkView>().isMine)
		{
			base.transform.localRotation = quaternion_0;
		}
	}
}
