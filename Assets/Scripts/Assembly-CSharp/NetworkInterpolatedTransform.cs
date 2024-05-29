using UnityEngine;

public sealed class NetworkInterpolatedTransform : MonoBehaviour
{
	private bool bool_0;

	private bool bool_1;

	public Player_move_c playerMovec;

	public bool isStartAngel;

	public Vector3 correctPlayerPos;

	public Quaternion correctPlayerRot = Quaternion.identity;

	private Transform transform_0;

	private void Awake()
	{
		if (!Defs.bool_2 || Defs.bool_3)
		{
			base.enabled = false;
		}
		correctPlayerPos = new Vector3(0f, -10000f, 0f);
		transform_0 = base.transform;
	}

	private void OnSerializeNetworkView(BitStream bitStream_0, NetworkMessageInfo networkMessageInfo_0)
	{
		if (bitStream_0.isWriting)
		{
			Vector3 value = base.transform.localPosition;
			Quaternion value2 = base.transform.localRotation;
			bitStream_0.Serialize(ref value);
			bitStream_0.Serialize(ref value2);
			bool_0 = playerMovec.Boolean_20;
			bitStream_0.Serialize(ref bool_0);
		}
		else
		{
			Vector3 value3 = Vector3.zero;
			Quaternion value4 = Quaternion.identity;
			bitStream_0.Serialize(ref value3);
			bitStream_0.Serialize(ref value4);
			correctPlayerPos = value3;
			correctPlayerRot = value4;
			bool_1 = bool_0;
			bitStream_0.Serialize(ref bool_0);
			playerMovec.Boolean_20 = bool_0;
		}
	}

	public void StartAngel()
	{
		isStartAngel = true;
	}

	private void Update()
	{
		if (Defs.bool_3 || base.GetComponent<NetworkView>().isMine)
		{
			return;
		}
		if (bool_0)
		{
			if (!bool_1)
			{
				bool_1 = bool_0;
				if (!isStartAngel)
				{
					StartAngel();
				}
				isStartAngel = false;
			}
			transform_0.position = new Vector3(0f, -1000f, 0f);
		}
		else if (!bool_1)
		{
			if (Vector3.SqrMagnitude(transform_0.position - correctPlayerPos) > 0.04f)
			{
				transform_0.position = Vector3.Lerp(transform_0.position, correctPlayerPos, Time.deltaTime * 5f);
			}
			transform_0.rotation = Quaternion.Lerp(transform_0.rotation, correctPlayerRot, Time.deltaTime * 5f);
		}
		else
		{
			transform_0.position = correctPlayerPos;
			transform_0.rotation = correctPlayerRot;
		}
		if (isStartAngel)
		{
			transform_0.position = new Vector3(0f, -1000f, 0f);
		}
	}
}
