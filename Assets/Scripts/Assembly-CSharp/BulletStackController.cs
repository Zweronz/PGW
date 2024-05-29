using System.Collections.Generic;
using UnityEngine;

public class BulletStackController : MonoBehaviour
{
	public static BulletStackController bulletStackController_0;

	private Dictionary<BulletType, List<GameObject>> dictionary_0 = new Dictionary<BulletType, List<GameObject>>();

	private Dictionary<BulletType, int> dictionary_1 = new Dictionary<BulletType, int>();

	private void Start()
	{
		bulletStackController_0 = this;
		base.transform.position = Vector3.zero;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			for (int j = 0; j < child.childCount; j++)
			{
				Transform child2 = child.GetChild(j);
				Bullet component = child2.GetComponent<Bullet>();
				if (!(component == null))
				{
					if (!dictionary_0.ContainsKey(component.type))
					{
						dictionary_0.Add(component.type, new List<GameObject>());
					}
					dictionary_0[component.type].Add(component.gameObject);
				}
			}
		}
		foreach (KeyValuePair<BulletType, List<GameObject>> item in dictionary_0)
		{
			dictionary_1.Add(item.Key, 0);
		}
	}

	public GameObject GetCurrentBullet(BulletType bulletType_0)
	{
		if (!dictionary_0.ContainsKey(bulletType_0))
		{
			return null;
		}
		Dictionary<BulletType, int> dictionary;
		Dictionary<BulletType, int> dictionary2 = (dictionary = dictionary_1);
		BulletType key;
		BulletType key2 = (key = bulletType_0);
		int num = dictionary[key];
		dictionary2[key2] = num + 1;
		if (dictionary_1[bulletType_0] >= dictionary_0[bulletType_0].Count)
		{
			dictionary_1[bulletType_0] = 0;
		}
		return dictionary_0[bulletType_0][dictionary_1[bulletType_0]];
	}

	private void OnDestroy()
	{
		bulletStackController_0 = null;
	}
}
