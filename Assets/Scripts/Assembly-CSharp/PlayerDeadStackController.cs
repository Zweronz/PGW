using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadStackController : MonoBehaviour
{
	public static PlayerDeadStackController playerDeadStackController_0;

	private List<PlayerDeadController> playerDeads = new List<PlayerDeadController>();

	private int index;

	public const int MAX_PLAYERDEADS = 12;

	private void Start()
	{
		playerDeadStackController_0 = this;
		GameObject playerDead = Resources.Load<GameObject>("caching/PlayerDead");

		for (int i = 0; i < MAX_PLAYERDEADS; i++)
		{
			playerDeads.Add(Instantiate(playerDead, transform).GetComponent<PlayerDeadController>());
		}
	}

	public PlayerDeadController GetCurrentParticle(bool bool_0)
	{
		if (index >= MAX_PLAYERDEADS)
		{
			index = 0;
		}

		return playerDeads[index++];
	}

	private void OnDestroy()
	{
		playerDeadStackController_0 = null;
	}
}
