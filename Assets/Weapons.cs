using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapons")]
public class Weapons : ScriptableObject
{
	[System.Serializable]
	public class Weapon
	{
		public int id;

		public float shootSpeedModifier = 1f, reloadSpeedModifier = 1f, rocketSpeed, dps, damage;

		public int clipSize, reserves, startingReserves;

		public WeaponType weaponType;

		public float bloomModifier;

		public BulletType bulletType;

		public float rocketExplodeDelay;
		public int bulletCount = 1;
		public float range = 300f;

		public WeaponData ToWeaponData()
		{
			return new WeaponData
			{
				Int32_0 = id,
				
				Single_0 = shootSpeedModifier,
				Single_1 = reloadSpeedModifier,

				Single_2 = rocketSpeed,
				Single_3 = dps,
				Single_4 = damage,

				Int32_2 = clipSize,
				Int32_3 = reserves,
				Int32_4 = startingReserves,

				WeaponType_0 = weaponType,

				Single_5 = bloomModifier,
				BulletType_0 = bulletType,

				Single_6 = rocketExplodeDelay
			};
		}
	}

	public static Weapon GetWeaponFromID(int id)
	{
		foreach (Weapon wep in Resources.Load<Weapons>("Weapons").weapons)
		{
			if (wep.id == id)
			{
				return wep;
			}
		}
		return null;
	}

	public List<Weapon> weapons;
}
