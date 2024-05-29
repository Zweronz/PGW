using System;
using System.Collections.Generic;
using UnityEngine;
using engine.unity;

public sealed class ZombiManager : MonoBehaviour
{
	public static ZombiManager zombiManager_0;

	public double timeGame;

	public float nextTimeSynch;

	public float nextAddZombi;

	public List<GameObject> zombiePrefabs = new List<GameObject>();

	private List<string[]> list_0 = new List<string[]>();

	private GameObject[] gameObject_0;

	public bool startGame;

	public double maxTimeGame = 240.0;

	public PhotonView photonView;

	private void Awake()
	{
		try
		{
			string[] array = null;
			array = new string[10] { "1", "15", "14", "2", "3", "9", "11", "12", "10", "16" };
			string[] array2 = array;
			foreach (string text in array2)
			{
				GameObject item = Resources.Load("Enemies/Enemy" + text + "_go") as GameObject;
				zombiePrefabs.Add(item);
			}
		}
		catch (Exception exception)
		{
			Debug.LogError("Cooperative mode failure.");
			Debug.LogException(exception);
			throw;
		}
	}

	private void Start()
	{
		zombiManager_0 = this;
		try
		{
			nextAddZombi = 5f;
			gameObject_0 = GameObject.FindGameObjectsWithTag("EnemyCreationZone");
			photonView = PhotonView.Get(this);
		}
		catch (Exception exception)
		{
			Debug.LogError("Cooperative mode failure.");
			Debug.LogException(exception);
			throw;
		}
	}

	[RPC]
	private void synchTime(float float_0)
	{
	}

	public void EndMatch()
	{
		if (!photonView.Boolean_1)
		{
			return;
		}
		startGame = false;
		timeGame = 0.0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
		float num = -100f;
		string text = string.Empty;
		int num2 = 0;
		for (int i = 0; i < array.Length; i++)
		{
			if ((float)array[i].GetComponent<NetworkStartTable>().Int32_4 > num)
			{
				num = array[i].GetComponent<NetworkStartTable>().Int32_4;
				text = array[i].GetComponent<NetworkStartTable>().String_5;
				num2 = i;
			}
		}
		photonView.RPC("win", PhotonTargets.All, text);
		photonView.RPC("WinID", PhotonTargets.All, array[num2].GetComponent<PhotonView>().int_1);
	}

	private void Update()
	{
		try
		{
			int num = GameObject.FindGameObjectsWithTag("Player").Length;
			if (!startGame && num > 0)
			{
				startGame = true;
				timeGame = 0.0;
				nextTimeSynch = 0f;
				nextAddZombi = 0f;
			}
			if (startGame && num == 0)
			{
				startGame = false;
				timeGame = 0.0;
				nextTimeSynch = 0f;
				nextAddZombi = 0f;
			}
			if (!startGame)
			{
				return;
			}
			timeGame = maxTimeGame - MonoSingleton<FightController>.Prop_0.FightTimeController_0.Double_0;
			if (timeGame > (double)nextAddZombi && photonView.Boolean_1 && GameObject.FindGameObjectsWithTag("Enemy").Length < 15)
			{
				float num2 = 4f;
				if (timeGame > maxTimeGame * 0.4000000059604645)
				{
					num2 = 3f;
				}
				if (timeGame > maxTimeGame * 0.800000011920929)
				{
					num2 = 2f;
				}
				nextAddZombi += num2;
				addZombi();
			}
		}
		catch (Exception exception)
		{
			Debug.LogError("Cooperative mode failure.");
			Debug.LogException(exception);
			throw;
		}
	}

	[RPC]
	private void win(string string_0)
	{
	}

	private void addZombi()
	{
		GameObject gameObject = gameObject_0[UnityEngine.Random.Range(0, gameObject_0.Length)];
		BoxCollider component = gameObject.GetComponent<BoxCollider>();
		Vector2 vector = new Vector2(component.size.x * gameObject.transform.localScale.x, component.size.z * gameObject.transform.localScale.z);
		Rect rect = new Rect(gameObject.transform.position.x - vector.x / 2f, gameObject.transform.position.z - vector.y / 2f, vector.x, vector.y);
		Vector3 vector2 = new Vector3(rect.x + UnityEngine.Random.Range(0f, rect.width), (!Defs.list_0.Contains(GlobalGameController.Int32_0)) ? 0f : gameObject.transform.position.y, rect.y + UnityEngine.Random.Range(0f, rect.height));
		int num = 0;
		double num2 = timeGame / maxTimeGame * 100.0;
		if (num2 < 15.0)
		{
			num = UnityEngine.Random.Range(0, 3);
		}
		if (num2 >= 15.0 && num2 < 30.0)
		{
			num = UnityEngine.Random.Range(0, 5);
		}
		if (num2 >= 30.0 && num2 < 45.0)
		{
			num = UnityEngine.Random.Range(0, 6);
		}
		if (num2 >= 45.0 && num2 < 60.0)
		{
			num = UnityEngine.Random.Range(3, 8);
		}
		if (num2 >= 60.0 && num2 < 75.0)
		{
			num = UnityEngine.Random.Range(5, 9);
		}
		if (num2 >= 75.0)
		{
			num = UnityEngine.Random.Range(5, 10);
		}
		photonView.RPC("addZombiRPC", PhotonTargets.All, num, vector2, PhotonNetwork.AllocateViewID());
	}

	[RPC]
	private void addZombiRPC(int int_0, Vector3 vector3_0, int int_1)
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(zombiePrefabs[int_0], vector3_0, Quaternion.identity);
		gameObject.GetComponent<ZombiUpravlenie>().typeZombInMas = int_0;
		PhotonView component = gameObject.GetComponent<PhotonView>();
		component.Int32_1 = int_1;
	}

	[RPC]
	private void WinID(int int_0)
	{
	}

	private void OnDestroy()
	{
		zombiManager_0 = null;
	}
}
