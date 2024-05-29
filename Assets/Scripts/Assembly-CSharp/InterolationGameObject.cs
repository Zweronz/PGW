using Photon;
using UnityEngine;

public class InterolationGameObject : Photon.MonoBehaviour
{
	private struct MovementHistoryEntry
	{
		public Vector3 vector3_0;

		public Quaternion quaternion_0;

		public double double_0;
	}

	public int int_0 = 5;

	public bool bool_0;

	public bool bool_1;

	public bool bool_2;

	public bool bool_3;

	private Quaternion quaternion_0;

	private Vector3 vector3_0;

	private double double_0;

	private double double_1;

	private Transform transform_0;

	private MovementHistoryEntry[] movementHistoryEntry_0;

	private bool bool_4 = true;

	private bool bool_5;

	private void Awake()
	{
		if (!Defs.bool_2)
		{
			base.enabled = false;
		}
		transform_0 = base.transform;
		movementHistoryEntry_0 = new MovementHistoryEntry[int_0];
		for (int i = 0; i < int_0; i++)
		{
			movementHistoryEntry_0[i].double_0 = 0.0;
		}
		double_1 = 1.0;
	}

	private void Start()
	{
		if ((Defs.bool_3 && base.PhotonView_0.Boolean_1) || (!Defs.bool_3 && GetComponent<NetworkView>().isMine))
		{
			bool_5 = true;
		}
	}

	private void OnPhotonSerializeView(PhotonStream photonStream_0, PhotonMessageInfo photonMessageInfo_0)
	{
		if (photonStream_0.Boolean_0)
		{
			if (bool_0)
			{
				photonStream_0.SendNext((!bool_2) ? transform_0.position : transform_0.localPosition);
			}
			if (bool_1)
			{
				photonStream_0.SendNext((!bool_2) ? transform_0.rotation : transform_0.localRotation);
			}
			photonStream_0.SendNext(PhotonNetwork.Double_0);
			return;
		}
		if (bool_0)
		{
			vector3_0 = (Vector3)photonStream_0.ReceiveNext();
		}
		if (bool_1)
		{
			quaternion_0 = (Quaternion)photonStream_0.ReceiveNext();
		}
		double_0 = (double)photonStream_0.ReceiveNext();
		AddNewSnapshot(vector3_0, quaternion_0, double_0);
	}

	private void OnSerializeNetworkView(BitStream bitStream_0, NetworkMessageInfo networkMessageInfo_0)
	{
		if (bitStream_0.isWriting)
		{
			Vector3 value = ((!bool_2) ? transform_0.position : transform_0.localPosition);
			Quaternion value2 = ((!bool_2) ? transform_0.rotation : transform_0.localRotation);
			if (bool_0)
			{
				bitStream_0.Serialize(ref value);
			}
			if (bool_1)
			{
				bitStream_0.Serialize(ref value2);
			}
			float value3 = (float)Network.time;
			bitStream_0.Serialize(ref value3);
			return;
		}
		Vector3 value4 = Vector3.zero;
		Quaternion value5 = Quaternion.identity;
		float value6 = 0f;
		if (bool_0)
		{
			bitStream_0.Serialize(ref value4);
		}
		if (bool_1)
		{
			bitStream_0.Serialize(ref value5);
		}
		vector3_0 = value4;
		quaternion_0 = value5;
		bitStream_0.Serialize(ref value6);
		double_0 = value6;
		AddNewSnapshot(vector3_0, quaternion_0, double_0);
	}

	private void Update()
	{
		if (bool_5)
		{
			return;
		}
		if (bool_3 && !bool_4)
		{
			double num = ((!(double_1 + (double)Time.deltaTime < movementHistoryEntry_0[movementHistoryEntry_0.Length - 1].double_0)) ? (double_1 + (double)Time.deltaTime) : (double_1 + (double)(Time.deltaTime * 1.5f)));
			int num2 = 0;
			for (int i = 0; i < movementHistoryEntry_0.Length && movementHistoryEntry_0[i].double_0 > double_1; i++)
			{
				num2 = i;
			}
			if (num2 == 0)
			{
				bool_4 = true;
			}
			float t = (float)((num - double_1) / (movementHistoryEntry_0[num2].double_0 - double_1));
			if (bool_2)
			{
				if (bool_0)
				{
					transform_0.localPosition = Vector3.Lerp(transform_0.localPosition, movementHistoryEntry_0[num2].vector3_0, t);
				}
				if (bool_1)
				{
					transform_0.localRotation = Quaternion.Lerp(transform_0.localRotation, movementHistoryEntry_0[num2].quaternion_0, t);
				}
			}
			else
			{
				if (bool_0)
				{
					transform_0.position = Vector3.Lerp(transform_0.position, movementHistoryEntry_0[num2].vector3_0, t);
				}
				if (bool_1)
				{
					transform_0.rotation = Quaternion.Lerp(transform_0.rotation, movementHistoryEntry_0[num2].quaternion_0, t);
				}
			}
			double_1 = num;
		}
		else
		{
			if (bool_4)
			{
				return;
			}
			if (bool_2)
			{
				if (bool_0)
				{
					transform_0.localPosition = movementHistoryEntry_0[movementHistoryEntry_0.Length - 1].vector3_0;
				}
				if (bool_1)
				{
					transform_0.localRotation = movementHistoryEntry_0[movementHistoryEntry_0.Length - 1].quaternion_0;
				}
			}
			else
			{
				if (bool_0)
				{
					transform_0.position = movementHistoryEntry_0[movementHistoryEntry_0.Length - 1].vector3_0;
				}
				if (bool_1)
				{
					transform_0.rotation = movementHistoryEntry_0[movementHistoryEntry_0.Length - 1].quaternion_0;
				}
			}
			double_1 = movementHistoryEntry_0[movementHistoryEntry_0.Length - 1].double_0;
		}
	}

	private void AddNewSnapshot(Vector3 vector3_1, Quaternion quaternion_1, double double_2)
	{
		for (int num = movementHistoryEntry_0.Length - 1; num > 0; num--)
		{
			movementHistoryEntry_0[num] = movementHistoryEntry_0[num - 1];
		}
		movementHistoryEntry_0[0].vector3_0 = vector3_1;
		movementHistoryEntry_0[0].quaternion_0 = quaternion_1;
		movementHistoryEntry_0[0].double_0 = double_2;
		if (bool_4 && movementHistoryEntry_0[movementHistoryEntry_0.Length - 1].double_0 > double_1)
		{
			bool_4 = false;
			double_1 = movementHistoryEntry_0[movementHistoryEntry_0.Length - 1].double_0;
		}
	}
}
