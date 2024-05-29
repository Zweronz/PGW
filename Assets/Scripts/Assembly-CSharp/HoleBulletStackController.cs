using System.Collections.Generic;
using UnityEngine;

public class HoleBulletStackController : MonoBehaviour
{
	public static HoleBulletStackController holeBulletStackController_0;

	private Dictionary<BulletType, List<HoleScript>> dictionary_0 = new Dictionary<BulletType, List<HoleScript>>();

	private Dictionary<BulletType, int> dictionary_1 = new Dictionary<BulletType, int>();

	private void Start()
	{
		holeBulletStackController_0 = this;
		base.transform.position = Vector3.zero;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			for (int j = 0; j < child.childCount; j++)
			{
				Transform child2 = child.GetChild(j);
				HoleScript component = child2.GetComponent<HoleScript>();
				if (!(component == null))
				{
					if (!dictionary_0.ContainsKey(component.type))
					{
						dictionary_0.Add(component.type, new List<HoleScript>());
					}
					dictionary_0[component.type].Add(component);
				}
			}
		}
		foreach (KeyValuePair<BulletType, List<HoleScript>> item in dictionary_0)
		{
			dictionary_1.Add(item.Key, 0);
		}
	}

	public HoleScript GetCurrentHole(BulletType bulletType_0, bool bool_0)
	{
		if (!dictionary_0.ContainsKey(bulletType_0))
		{
			return null;
		}
		bool flag = true;
		do
		{
			Dictionary<BulletType, int> dictionary;
			Dictionary<BulletType, int> dictionary2 = (dictionary = dictionary_1);
			BulletType key;
			BulletType key2 = (key = bulletType_0);
			int num = dictionary[key];
			dictionary2[key2] = num + 1;
			if (dictionary_1[bulletType_0] >= dictionary_0[bulletType_0].Count)
			{
				if (!flag)
				{
					return null;
				}
				dictionary_1[bulletType_0] = 0;
				flag = false;
			}
		}
		while (dictionary_0[bulletType_0][dictionary_1[bulletType_0]].Boolean_0 && !bool_0);
		return dictionary_0[bulletType_0][dictionary_1[bulletType_0]];
	}

	private void OnDestroy()
	{
		holeBulletStackController_0 = null;
	}
}
