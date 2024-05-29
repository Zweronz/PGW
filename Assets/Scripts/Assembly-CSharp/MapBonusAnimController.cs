using System.Collections.Generic;
using UnityEngine;

public class MapBonusAnimController : MonoBehaviour
{
	public AudioClip ItemUpSound;

	public GameObject Effects;

	public void ItemPickup()
	{
		if (Effects == null)
		{
			return;
		}
		List<ParticleSystem> list = new List<ParticleSystem>(Effects.GetComponents<ParticleSystem>());
		list.AddRange(Effects.GetComponentsInChildren<ParticleSystem>());
		if (list.Count != 0)
		{
			Effects.transform.parent = null;
			for (int i = 0; i < list.Count; i++)
			{
				list[i].Play();
			}
			Object.Destroy(Effects, list[0].duration);
		}
	}
}
