using UnityEngine;

public sealed class WeaponRailgunEffect : WeaponCustomEffect
{
	public override void SetActiveEffect(bool bool_0)
	{
		if (bool_0)
		{
			WeaponSounds componentInParent = GetComponentInParent<WeaponSounds>();
			if (!(componentInParent == null) && componentInParent.WeaponData_0.Boolean_9)
			{
				Transform transform = componentInParent.gameObject.transform;
				WeaponEffectsController component = componentInParent.GetComponent<WeaponEffectsController>();
				WeaponManager.AddRay(component.Transform_0.position, transform.forward, transform.name.Replace("(Clone)", string.Empty));
			}
		}
	}
}
