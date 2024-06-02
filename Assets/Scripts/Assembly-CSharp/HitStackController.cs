using System.Collections.Generic;
using UnityEngine;

public class HitStackController : MonoBehaviour
{
	public static HitStackController hitStackController_0;

	private List<HitParticle> particles = new List<HitParticle>();

	private int index;

	public const int MAX_HITS = 24;

	private void Start()
	{
		hitStackController_0 = this;
		GameObject headshot = Resources.Load<GameObject>("caching/Hit_particle");

		for (int i = 0; i < MAX_HITS; i++)
		{
			particles.Add(Instantiate(headshot, transform).GetComponent<HitParticle>());
		}
	}

	public HitParticle GetCurrentParticle(bool bool_0)
	{
		if (index >= MAX_HITS)
		{
			index = 0;
		}

		return particles[index++];
	}

	private void OnDestroy()
	{
		hitStackController_0 = null;
	}
}
