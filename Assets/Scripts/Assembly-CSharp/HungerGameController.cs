using ExitGames.Client.Photon;
using Photon;
using UnityEngine;

internal sealed class HungerGameController : Photon.MonoBehaviour
{
	public bool bool_0;

	public bool bool_1;

	public float float_0 = 30f;

	public int int_0;

	public int int_1 = 10;

	public bool bool_2;

	public float float_1 = 10.5f;

	public bool bool_3;

	private float float_2 = 2f;

	public int int_2 = 2;

	public bool bool_4;

	private float float_3 = 1f;

	public float float_4 = 600f;

	public bool bool_5;

	private static HungerGameController hungerGameController_0;

	public static HungerGameController HungerGameController_0
	{
		get
		{
			return hungerGameController_0;
		}
	}

	private void Start()
	{
		int_1 = PhotonNetwork.Room_0.Int32_2;
		float_4 = int.Parse(PhotonNetwork.Room_0.Hashtable_0["RoundTime"].ToString()) * 60;
		hungerGameController_0 = this;
	}

	private void OnDestroy()
	{
		hungerGameController_0 = null;
	}

	private void Update()
	{
		if (bool_1 && float_0 > 0f)
		{
			float_0 -= Time.deltaTime;
		}
		if (bool_0 && float_1 > 0f)
		{
			float_1 -= Time.deltaTime;
		}
		if (float_1 < 0f)
		{
			float_1 = 0f;
		}
		if (bool_4 && float_3 >= 0f)
		{
			float_3 -= Time.deltaTime;
		}
		if (bool_4 && float_3 < 0f)
		{
			bool_4 = false;
		}
		if (bool_3 && float_4 > 0f && GameObject.FindGameObjectsWithTag("Player").Length > 0)
		{
			float_4 -= Time.deltaTime;
		}
		if (!base.PhotonView_0.Boolean_1)
		{
			return;
		}
		if (float_4 <= 0f && !bool_5)
		{
			bool_5 = true;
			base.PhotonView_0.RPC("Draw", PhotonTargets.AllBuffered);
		}
		float_2 -= Time.deltaTime;
		if (bool_3 && float_2 < 0f)
		{
			float_2 = 0.5f;
			base.PhotonView_0.RPC("SynchGameTimer", PhotonTargets.Others, float_4);
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
		if (!bool_0)
		{
			if (!bool_1 && array.Length >= int_2)
			{
				base.PhotonView_0.RPC("StartTimer", PhotonTargets.AllBuffered, true);
			}
			if (float_2 < 0f)
			{
				float_2 = 0.5f;
				base.PhotonView_0.RPC("SynchStartTimer", PhotonTargets.Others, float_0);
			}
			if ((!bool_0 && bool_1 && float_0 < 0.1f && array.Length >= int_2) || (!bool_0 && bool_1 && array.Length == PhotonNetwork.Room_0.Int32_2))
			{
				base.PhotonView_0.RPC("StartGame", PhotonTargets.AllBuffered);
				Hashtable hashtable = new Hashtable();
				hashtable["starting"] = 1;
				PhotonNetwork.Room_0.Boolean_5 = false;
				PhotonNetwork.Room_0.SetCustomProperties(hashtable);
			}
		}
		else
		{
			if (float_2 < 0f)
			{
				float_2 = 0.5f;
				base.PhotonView_0.RPC("SynchTimerGo", PhotonTargets.Others, float_1);
			}
			if (!bool_3 && float_1 < 0.1f)
			{
				base.PhotonView_0.RPC("Go", PhotonTargets.AllBuffered);
			}
		}
	}

	[RPC]
	private void Draw()
	{
		Debug.Log("Draw!!!");
	}

	[RPC]
	private void StartTimer(bool bool_6)
	{
		bool_1 = bool_6;
	}

	[RPC]
	private void SynchStartTimer(float float_5)
	{
		float_0 = float_5;
	}

	[RPC]
	private void SynchTimerGo(float float_5)
	{
		float_1 = float_5;
	}

	[RPC]
	private void SynchGameTimer(float float_5)
	{
		float_4 = float_5;
	}

	[RPC]
	private void StartGame()
	{
		bool_0 = true;
	}

	[RPC]
	private void Go()
	{
		bool_3 = true;
		bool_4 = true;
	}
}
