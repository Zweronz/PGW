using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public sealed class WeaponEffectsController : MonoBehaviour
{
	private Transform transform_0;

	[CompilerGenerated]
	private Transform transform_1;

	[CompilerGenerated]
	private List<Transform> list_0;

	public Transform Transform_0
	{
		[CompilerGenerated]
		get
		{
			return transform_1;
		}
		[CompilerGenerated]
		private set
		{
			transform_1 = value;
		}
	}

	public List<Transform> List_0
	{
		[CompilerGenerated]
		get
		{
			return list_0;
		}
		[CompilerGenerated]
		private set
		{
			list_0 = value;
		}
	}

	private void Awake()
	{
		transform_0 = base.transform;
		InitHierarchy();
	}

	public void SetActiveEffects(byte byte_0 = 0)
	{
		if (Transform_0 == null || List_0.Count == 0 || byte_0 >= List_0.Count)
		{
			BroadcastMessage("ShowEffects", SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			List_0[byte_0].SendMessage("ShowEffects", SendMessageOptions.DontRequireReceiver);
		}
	}

	private void InitHierarchy()
	{
		Transform_0 = transform_0.Find("BulletSpawnPoint");
		List_0 = new List<Transform>();
		if (!(Transform_0 != null))
		{
			return;
		}
		foreach (Transform item in Transform_0.transform)
		{
			if (item.name.Equals("GunFlash"))
			{
				List_0.Add(item);
			}
		}
	}
}
