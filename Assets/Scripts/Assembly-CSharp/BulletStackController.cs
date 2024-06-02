using System.Collections.Generic;
using UnityEngine;

public class BulletStackController : MonoBehaviour
{
	public static BulletStackController bulletStackController_0;

	private static Dictionary<BulletType, List<GameObject>> cachedBullets = new Dictionary<BulletType, List<GameObject>>();

	private static Dictionary<BulletType, int> bulletIndex = new Dictionary<BulletType, int>();

	public const int MAX_BULLETS = 16;

	private void Awake()
	{
		bulletStackController_0 = this;
	}

	private static Dictionary<BulletType, string> bulletPaths
	{
		get
		{
			return new Dictionary<BulletType, string>
			{
				{BulletType.COMMON, "BulletsCommon/BulletCommon"},
				{BulletType.BLASTER, "BulletsBlaster/BulletBlaster"},
				{BulletType.ICICLE, "BulletIcicle/BulletIcicle"},
				{BulletType.BLASTER_RED, "BulletsBlasterRed/BulletBlaster"},
				{BulletType.BLASTER_BLUE, "BulletsBlasterBlue/BulletBlaster"},
				{BulletType.BLASTER_AOE, "BulletsBlasterRedAoe/BulletBlaster"},
				{BulletType.BOW_ARROW, "BulletsChinaBow/BulletCommon"},
				{BulletType.SHURIKEN, "BulletsSuriken/BulletCommon"},
				{BulletType.HOOK, "BulletsHook/BulletHook"},
				{BulletType.POISON_PLUSH, "PoisonPlush/BulletPlush"},
				{BulletType.GREEN_ARROW, "BulletsKryptoniteBlaster/BulletCommon"}
			};
		}
	}

	private static string GetPath(BulletType type)
	{
		return "caching/bullets/" + bulletPaths[type];
	}

	public GameObject GetCurrentBullet(BulletType bulletType_0)
	{
		if (cachedBullets.ContainsKey(bulletType_0))
		{
			if (bulletIndex[bulletType_0] >= MAX_BULLETS)
			{
				bulletIndex[bulletType_0] = 0;
			}
			
			return cachedBullets[bulletType_0][bulletIndex[bulletType_0]++];
		}

		List<GameObject> stack = new List<GameObject>();
		GameObject bullet = Resources.Load<GameObject>(GetPath(bulletType_0));

		for (int i = 0; i < MAX_BULLETS; i++)
		{
			stack.Add(Instantiate(bullet, transform));
		}

		cachedBullets.Add(bulletType_0, stack);
		bulletIndex.Add(bulletType_0, 0);

		return stack[bulletIndex[bulletType_0]++];
	}

	private void OnDestroy()
	{
		bulletStackController_0 = null;
	}
}
