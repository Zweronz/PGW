using System.Collections.Generic;
using UnityEngine;

public class HoleBulletStackController : MonoBehaviour
{
	public static HoleBulletStackController holeBulletStackController_0;

	private static Dictionary<BulletType, List<HoleScript>> cachedHoles = new Dictionary<BulletType, List<HoleScript>>();

	private static Dictionary<BulletType, int> holeIndex = new Dictionary<BulletType, int>();

	public const int MAX_HOLES = 24;

	private static Dictionary<BulletType, string> holePaths
	{
		get
		{
			return new Dictionary<BulletType, string>
			{
				{BulletType.COMMON, "BulletHolesCommon/BulletHoleCommon"},
				{BulletType.BLASTER, "BulletHolesBlaster/BulletHolesBlaster"},
				{BulletType.ICICLE, "BulletHolesIcicle/BulletHolesIcicle"},
				{BulletType.BLASTER_RED, "BulletHolesBlasterRed/BulletHolesBlaster"},
				{BulletType.BLASTER_BLUE, "BulletHolesBlasterBlue/BulletHolesBlaster"}
			};
		}
	}

	private static string GetPath(BulletType type)
	{
		if (!holePaths.ContainsKey(type))
		{
			return "caching/holes/" + holePaths[BulletType.COMMON];
		}

		return "caching/holes/" + holePaths[type];
	}

	private void Awake()
	{
		holeBulletStackController_0 = this;
	}

	public HoleScript GetCurrentHole(BulletType bulletType_0, bool bool_0)
	{
		if (cachedHoles.ContainsKey(bulletType_0))
		{
			if (holeIndex[bulletType_0] >= MAX_HOLES)
			{
				holeIndex[bulletType_0] = 0;
			}
			
			return cachedHoles[bulletType_0][holeIndex[bulletType_0]++];
		}

		List<HoleScript> stack = new List<HoleScript>();
		GameObject hole = Resources.Load<GameObject>(GetPath(bulletType_0));

		for (int i = 0; i < MAX_HOLES; i++)
		{
			stack.Add(Instantiate(hole, transform).GetComponent<HoleScript>());
		}

		cachedHoles.Add(bulletType_0, stack);
		holeIndex.Add(bulletType_0, 0);

		return stack[holeIndex[bulletType_0]++];
	}

	private void OnDestroy()
	{
		holeBulletStackController_0 = null;
	}
}
