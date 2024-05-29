using UnityEngine;

public class HitStackController : MonoBehaviour
{
	public static HitStackController hitStackController_0;

	public HitParticle[] particles;

	private int int_0;

	private void Start()
	{
		hitStackController_0 = this;
		Transform transform = base.transform;
		transform.position = Vector3.zero;
		int childCount = transform.childCount;
		particles = new HitParticle[childCount];
		for (int i = 0; i < childCount; i++)
		{
			particles[i] = transform.GetChild(i).GetComponent<HitParticle>();
		}
	}

	public HitParticle GetCurrentParticle(bool bool_0)
	{
		bool flag = true;
		do
		{
			int_0++;
			if (int_0 >= particles.Length)
			{
				if (!flag)
				{
					return null;
				}
				int_0 = 0;
				flag = false;
			}
		}
		while (particles[int_0].isUseMine && !bool_0);
		return particles[int_0];
	}

	private void OnDestroy()
	{
		hitStackController_0 = null;
	}
}
