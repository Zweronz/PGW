using System.Collections.Generic;
using UnityEngine;

public class WallParticleStackController : MonoBehaviour
{
	public static WallParticleStackController wallParticleStackController_0;

	private static Dictionary<BulletType, List<WallBloodParticle>> cachedWallParticles = new Dictionary<BulletType, List<WallBloodParticle>>();

	private static Dictionary<BulletType, int> wallParticleIndex = new Dictionary<BulletType, int>();

	public const int MAX_WALLPARTICLE = 24;

	private void Awake()
	{
		wallParticleStackController_0 = this;
	}

	private static Dictionary<BulletType, string> wallParticlePaths
	{
		get
		{
			return new Dictionary<BulletType, string>
			{
				{BulletType.COMMON, "WallCommon/WallEffectCommon"},
				{BulletType.BLASTER, "WallBlaster/WallEffectBlaster"},
				{BulletType.ICICLE, "WallIcicle/WallEffectIcicle"},
				{BulletType.BLASTER_RED, "WallBlasterRed/WallEffectBlaster"},
				{BulletType.BLASTER_BLUE, "WallBlasterBlue/WallEffectBlaster"},
				{BulletType.BLASTER_AOE, "WallBlasterRedAoe/WallEffectBlasterRedAoe"},
				{BulletType.BOW_ARROW, "WallChinaBow/WallEffectSuriken"},
				{BulletType.SHURIKEN, "WallSuriken/WallEffectSuriken"},
				{BulletType.POISON_PLUSH, "PoisonPlush/WallEffectPluh"},
				{BulletType.GREEN_ARROW, "WallKryptoniteBlaster/WallEffectKryptoniteBlaster"}
			};
		}
	}

	private static string GetPath(BulletType type)
	{
		if (!wallParticlePaths.ContainsKey(type))
		{
			return wallParticlePaths[type];
		}

		return "caching/wallparticle/" + wallParticlePaths[type];
	}

	public WallBloodParticle GetCurrentParticle(BulletType bulletType_0, bool bool_0)
	{
		if (cachedWallParticles.ContainsKey(bulletType_0))
		{
			if (wallParticleIndex[bulletType_0] >= MAX_WALLPARTICLE)
			{
				wallParticleIndex[bulletType_0] = 0;
			}
			
			return cachedWallParticles[bulletType_0][wallParticleIndex[bulletType_0]++];
		}

		List<WallBloodParticle> stack = new List<WallBloodParticle>();
		GameObject bullet = Resources.Load<GameObject>(GetPath(bulletType_0));

		for (int i = 0; i < MAX_WALLPARTICLE; i++)
		{
			GameObject stackObject = Instantiate(bullet, transform);
			stackObject.SendMessage("Init");
			stack.Add(stackObject.GetComponent<WallBloodParticle>());
		}

		cachedWallParticles.Add(bulletType_0, stack);
		wallParticleIndex.Add(bulletType_0, 0);

		return stack[wallParticleIndex[bulletType_0]++];
	}

	private void OnDestroy()
	{
		wallParticleStackController_0 = null;
	}
}
