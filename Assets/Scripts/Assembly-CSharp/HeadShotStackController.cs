using System.Collections.Generic;
using UnityEngine;

public class HeadShotStackController : MonoBehaviour
{
	public static HeadShotStackController headShotStackController_0;

	private List<HitParticle> particles = new List<HitParticle>();

	private int index;

	public const int MAX_HEADSHOTS = 24;

	private void Start()
	{
		headShotStackController_0 = this;
		GameObject headshot = Resources.Load<GameObject>("caching/Headshot_particle");

		for (int i = 0; i < MAX_HEADSHOTS; i++)
		{
			particles.Add(Instantiate(headshot, transform).GetComponent<HitParticle>());
		}
	}

	public HitParticle GetCurrentParticle(bool bool_0)
	{
		if (index >= MAX_HEADSHOTS)
		{
			index = 0;
		}

		return particles[index++];
	}

	private void OnDestroy()
	{
		headShotStackController_0 = null;
	}
}
