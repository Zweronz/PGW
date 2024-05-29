using System.Collections.Generic;
using UnityEngine;

public class PlayerMessageQueueConroller : MonoBehaviour
{
	private PlayerScoreController playerScoreController_0;

	private NetworkStartTable networkStartTable_0;

	private PlayerMessageQueueObject playerMessageQueueObject_0;

	private List<PlayerMessageQueueObject> list_0 = new List<PlayerMessageQueueObject>();

	private bool Boolean_0
	{
		get
		{
			return networkStartTable_0 != null && networkStartTable_0.Boolean_9;
		}
	}

	private bool Boolean_1
	{
		get
		{
			return networkStartTable_0 != null && networkStartTable_0.Boolean_4;
		}
	}

	private bool Boolean_2
	{
		get
		{
			return networkStartTable_0 != null && networkStartTable_0.Boolean_7;
		}
	}

	private bool Boolean_3
	{
		get
		{
			return networkStartTable_0 != null && networkStartTable_0.Boolean_8;
		}
	}

	private void Awake()
	{
		playerScoreController_0 = base.gameObject.GetComponent<PlayerScoreController>();
	}

	private void Start()
	{
		if (playerScoreController_0 == null)
		{
			base.enabled = false;
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			NetworkStartTable component = gameObject.GetComponent<NetworkStartTable>();
			if (gameObject.GetComponent<PhotonView>().PhotonPlayer_0 == base.transform.GetComponent<PhotonView>().PhotonPlayer_0)
			{
				networkStartTable_0 = component;
			}
		}
		if (networkStartTable_0 == null || !Boolean_1)
		{
			base.enabled = false;
		}
	}

	private void Update()
	{
		if (HeadUpDisplay.HeadUpDisplay_0 == null || !HeadUpDisplay.HeadUpDisplay_0.gameObject.activeSelf || WeaponManager.weaponManager_0 == null || WeaponManager.weaponManager_0.myPlayerMoveC == null || WeaponManager.weaponManager_0.myPlayerMoveC.Boolean_20)
		{
			return;
		}
		if (playerMessageQueueObject_0 != null)
		{
			playerMessageQueueObject_0.UpdateTime(Time.deltaTime);
			if (playerMessageQueueObject_0.Single_0 == 0f)
			{
				playerMessageQueueObject_0 = null;
			}
		}
		if (playerMessageQueueObject_0 == null && list_0.Count > 0)
		{
			playerMessageQueueObject_0 = list_0[0];
			list_0.RemoveAt(0);
			playerMessageQueueObject_0.Start();
		}
	}

	public void Add(PlayerMessageQueueObject playerMessageQueueObject_1)
	{
		list_0.Add(playerMessageQueueObject_1);
	}

	public void Clear()
	{
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.SetKillStrikeBadge(string.Empty, string.Empty, false);
			HeadUpDisplay.HeadUpDisplay_0.SetKillStrikeBadge(string.Empty, string.Empty);
			HeadUpDisplay.HeadUpDisplay_0.StopMessageBadge();
		}
		playerMessageQueueObject_0 = null;
		list_0.Clear();
	}

	public void Pause()
	{
		if (playerMessageQueueObject_0 != null)
		{
			playerMessageQueueObject_0.Pause();
		}
	}

	public void Resume()
	{
		if (playerMessageQueueObject_0 != null)
		{
			playerMessageQueueObject_0.Resume();
		}
	}
}
