using System.Collections.Generic;
using UnityEngine;

public class BloodParticleStackController : MonoBehaviour
{
	public static BloodParticleStackController bloodParticleStackController_0;

	private static Dictionary<BulletType, List<WallBloodParticle>> cachedBloodParticles = new Dictionary<BulletType, List<WallBloodParticle>>();

	private static Dictionary<BulletType, int> bloodParticleIndex = new Dictionary<BulletType, int>();

	public const int MAX_BLOODPARTICLE = 24;

	private void Awake()
	{
		bloodParticleStackController_0 = this;
	}

	private static Dictionary<BulletType, string> bloodParticlePaths
	{
		get
		{
			return new Dictionary<BulletType, string>
			{
				{BulletType.COMMON, "BloodCommon/BloodEffectCommon"},
				{BulletType.BLASTER, "BloodBlaster/BloodEffectBlaster"},
				{BulletType.ICICLE, "BloodIcicle/WallEffectIcicle"},
				{BulletType.BLASTER_RED, "BloodBlasterRed/WallEffectBlaster"},
				{BulletType.BLASTER_BLUE, "BloodBlasterBlue/WallEffectBlaster"},
				{BulletType.BLASTER_AOE, "BloodBlasterRedAoe/WallEffectBlasterRedAoe"},
				{BulletType.BOW_ARROW, "BloodChinaBow/WallEffectSuriken"},
				{BulletType.POISON_PLUSH, "PoisonPlush/WallEffectPluh"},
				{BulletType.GREEN_ARROW, "BloodKryptoniteBlaster/WallEffectKryptoniteBlaster"}
			};
		}
	}

	private static string GetPath(BulletType type)
	{
		if (!bloodParticlePaths.ContainsKey(type))
		{
			return bloodParticlePaths[type];
		}

		return "caching/bloodparticle/" + bloodParticlePaths[type];
	}

	public WallBloodParticle GetCurrentParticle(BulletType bulletType_0, bool bool_0)
	{
		if (cachedBloodParticles.ContainsKey(bulletType_0))
		{
			if (bloodParticleIndex[bulletType_0] >= MAX_BLOODPARTICLE)
			{
				bloodParticleIndex[bulletType_0] = 0;
			}
			
			return cachedBloodParticles[bulletType_0][bloodParticleIndex[bulletType_0]++];
		}

		List<WallBloodParticle> stack = new List<WallBloodParticle>();
		GameObject bullet = Resources.Load<GameObject>(GetPath(bulletType_0));

		for (int i = 0; i < MAX_BLOODPARTICLE; i++)
		{
			stack.Add(Instantiate(bullet, transform).GetComponent<WallBloodParticle>());
		}

		cachedBloodParticles.Add(bulletType_0, stack);
		bloodParticleIndex.Add(bulletType_0, 0);

		return stack[bloodParticleIndex[bulletType_0]++];
	}

	private void OnDestroy()
	{
		bloodParticleStackController_0 = null;
	}
}
