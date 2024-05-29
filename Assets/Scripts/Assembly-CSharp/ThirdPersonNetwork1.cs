using Photon;
using UnityEngine;

public class ThirdPersonNetwork1 : Photon.MonoBehaviour
{
	private struct MovementHistoryEntry
	{
		public Vector3 vector3_0;

		public Quaternion quaternion_0;

		public byte byte_0;

		public bool bool_0;

		public double double_0;
	}

	public Player_move_c player_move_c_0;

	public SkinName skinName_0;

	public bool bool_0;

	public bool bool_1;

	public bool bool_2;

	public bool bool_3;

	public string string_0;

	private ThirdPersonCamera thirdPersonCamera_0;

	private ThirdPersonController thirdPersonController_0;

	private bool bool_4;

	private bool bool_5;

	private Vector3 vector3_0;

	private double double_0;

	private Quaternion quaternion_0 = Quaternion.identity;

	private Transform transform_0;

	private double double_1;

	private FPSStateController fpsstateController_0;

	private MovementHistoryEntry[] movementHistoryEntry_0;

	private int int_0 = 5;

	private bool bool_6 = true;

	private int int_1;

	private bool bool_7 = true;

	private bool bool_8;

	private bool bool_9;

	private void Awake()
	{
		if (!Defs.bool_2)
		{
			base.enabled = false;
		}
		transform_0 = base.transform;
		vector3_0 = new Vector3(0f, -10000f, 0f);
		movementHistoryEntry_0 = new MovementHistoryEntry[int_0];
		for (int i = 0; i < int_0; i++)
		{
			movementHistoryEntry_0[i].double_0 = 0.0;
		}
		double_1 = 1.0;
		fpsstateController_0 = GetComponent<FPSStateController>();
	}

	private void Start()
	{
		if (base.PhotonView_0.Boolean_1)
		{
			bool_8 = true;
		}
	}

	private void OnPhotonSerializeView(PhotonStream photonStream_0, PhotonMessageInfo photonMessageInfo_0)
	{
		if (photonStream_0.Boolean_0)
		{
			bool_4 = player_move_c_0.Boolean_20;
			if (player_move_c_0.PlayerParametersController_0.Single_2 <= 0f)
			{
				bool_4 = true;
			}
			photonStream_0.SendNext(transform_0.position);
			photonStream_0.SendNext(transform_0.rotation);
			photonStream_0.SendNext(bool_4);
			photonStream_0.SendNext(PhotonNetwork.Double_0);
			photonStream_0.SendNext(SkinName.GetAnimIntByString(string_0));
			photonStream_0.SendNext(skinName_0.Boolean_0);
			photonStream_0.SendNext(player_move_c_0.Boolean_22);
			photonStream_0.SendNext(fpsstateController_0.Int32_0);
		}
		else if (!bool_7)
		{
			bool_5 = bool_4;
			byte b = 0;
			bool flag = false;
			vector3_0 = (Vector3)photonStream_0.ReceiveNext();
			quaternion_0 = (Quaternion)photonStream_0.ReceiveNext();
			bool_4 = (bool)photonStream_0.ReceiveNext();
			player_move_c_0.Boolean_20 = bool_4;
			double_0 = (double)photonStream_0.ReceiveNext();
			b = (byte)photonStream_0.ReceiveNext();
			flag = (bool)photonStream_0.ReceiveNext();
			player_move_c_0.Boolean_22 = (bool)photonStream_0.ReceiveNext();
			fpsstateController_0.Int32_0 = (int)photonStream_0.ReceiveNext();
			if (bool_4)
			{
				bool_6 = true;
				double_1 = double_0;
			}
			AddNewSnapshot(vector3_0, quaternion_0, double_0, b, flag);
		}
		else
		{
			bool_7 = false;
		}
	}

	public void StartAngel()
	{
		bool_2 = true;
	}

	private void Update()
	{
		if (bool_8)
		{
			return;
		}
		if (!player_move_c_0.Boolean_13)
		{
			transform_0.position = new Vector3(0f, -10000f, 0f);
			return;
		}
		if (bool_4)
		{
			if (!bool_5)
			{
				bool_5 = bool_4;
				bool_2 = false;
			}
			transform_0.position = new Vector3(0f, -10000f, 0f);
		}
		else if (!bool_5 && !bool_6 && (bool_0 || bool_1 || player_move_c_0.Boolean_14))
		{
			double num = ((!(double_1 + (double)Time.deltaTime < movementHistoryEntry_0[movementHistoryEntry_0.Length - 1].double_0)) ? (double_1 + (double)Time.deltaTime) : (double_1 + (double)(Time.deltaTime * 1.5f)));
			int num2 = 0;
			for (int i = 0; i < movementHistoryEntry_0.Length && movementHistoryEntry_0[i].double_0 > double_1; i++)
			{
				num2 = i;
			}
			if (num2 == 0)
			{
				bool_6 = true;
			}
			if (movementHistoryEntry_0[num2].double_0 - double_1 > 4.0 && num2 > 0)
			{
				num2--;
				transform_0.position = movementHistoryEntry_0[num2].vector3_0;
				transform_0.rotation = movementHistoryEntry_0[num2].quaternion_0;
				double_1 = movementHistoryEntry_0[num2].double_0;
			}
			else
			{
				float t = (float)((num - double_1) / (movementHistoryEntry_0[num2].double_0 - double_1));
				transform_0.position = Vector3.Lerp(transform_0.position, movementHistoryEntry_0[num2].vector3_0, t);
				transform_0.rotation = Quaternion.Lerp(transform_0.rotation, movementHistoryEntry_0[num2].quaternion_0, t);
				double_1 = num;
				string animStringByInt = SkinName.GetAnimStringByInt(movementHistoryEntry_0[num2].byte_0);
				if (!string_0.Equals(animStringByInt))
				{
					skinName_0.SetAnim(animStringByInt, movementHistoryEntry_0[num2].bool_0);
					string_0 = animStringByInt;
				}
			}
		}
		else if (!bool_6)
		{
			transform_0.position = movementHistoryEntry_0[movementHistoryEntry_0.Length - 1].vector3_0;
			transform_0.rotation = movementHistoryEntry_0[movementHistoryEntry_0.Length - 1].quaternion_0;
			double_1 = movementHistoryEntry_0[movementHistoryEntry_0.Length - 1].double_0;
		}
		if (bool_2)
		{
			if (bool_5)
			{
				bool_2 = false;
			}
			else
			{
				transform_0.position = new Vector3(0f, -10000f, 0f);
			}
		}
	}

	private void AddNewSnapshot(Vector3 vector3_1, Quaternion quaternion_1, double double_2, byte byte_0, bool bool_10)
	{
		for (int num = movementHistoryEntry_0.Length - 1; num > 0; num--)
		{
			movementHistoryEntry_0[num] = movementHistoryEntry_0[num - 1];
		}
		movementHistoryEntry_0[0].vector3_0 = vector3_1;
		movementHistoryEntry_0[0].quaternion_0 = quaternion_1;
		movementHistoryEntry_0[0].double_0 = double_2;
		movementHistoryEntry_0[0].byte_0 = byte_0;
		movementHistoryEntry_0[0].bool_0 = bool_10;
		if (bool_6 && movementHistoryEntry_0[movementHistoryEntry_0.Length - 1].double_0 > double_1)
		{
			bool_6 = false;
			double_1 = movementHistoryEntry_0[movementHistoryEntry_0.Length - 1].double_0;
			if (!bool_9)
			{
				transform_0.position = movementHistoryEntry_0[movementHistoryEntry_0.Length - 1].vector3_0;
				transform_0.rotation = movementHistoryEntry_0[movementHistoryEntry_0.Length - 1].quaternion_0;
				bool_9 = true;
			}
		}
	}
}
