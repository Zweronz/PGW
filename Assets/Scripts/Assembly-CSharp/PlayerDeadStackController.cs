using UnityEngine;

public class PlayerDeadStackController : MonoBehaviour
{
	public static PlayerDeadStackController playerDeadStackController_0;

	public PlayerDeadController[] playerDeads;

	private int int_0;

	private void Start()
	{
		playerDeadStackController_0 = this;
		Transform transform = base.transform;
		transform.position = Vector3.zero;
		int childCount = transform.childCount;
		playerDeads = new PlayerDeadController[childCount];
		for (int i = 0; i < childCount; i++)
		{
			playerDeads[i] = transform.GetChild(i).GetComponent<PlayerDeadController>();
		}
	}

	public PlayerDeadController GetCurrentParticle(bool bool_0)
	{
		bool flag = true;
		do
		{
			int_0++;
			if (int_0 >= playerDeads.Length)
			{
				if (!flag)
				{
					return null;
				}
				int_0 = 0;
				flag = false;
			}
		}
		while (playerDeads[int_0].isUseMine && !bool_0);
		return playerDeads[int_0];
	}

	private void OnDestroy()
	{
		playerDeadStackController_0 = null;
	}
}
