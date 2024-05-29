using System;

[Serializable]
public class InvStat
{
	public enum Identifier
	{
		Strength = 0,
		Constitution = 1,
		Agility = 2,
		Intelligence = 3,
		Damage = 4,
		Crit = 5,
		Armor = 6,
		Health = 7,
		Mana = 8,
		Other = 9
	}

	public enum Modifier
	{
		Added = 0,
		Percent = 1
	}

	public Identifier id;

	public Modifier modifier;

	public int amount;

	public static string GetName(Identifier identifier_0)
	{
		return identifier_0.ToString();
	}

	public static string GetDescription(Identifier identifier_0)
	{
		switch (identifier_0)
		{
		default:
			return null;
		case Identifier.Strength:
			return "Strength increases melee damage";
		case Identifier.Constitution:
			return "Constitution increases health";
		case Identifier.Agility:
			return "Agility increases armor";
		case Identifier.Intelligence:
			return "Intelligence increases mana";
		case Identifier.Damage:
			return "Damage adds to the amount of damage done in combat";
		case Identifier.Crit:
			return "Crit increases the chance of landing a critical strike";
		case Identifier.Armor:
			return "Armor protects from damage";
		case Identifier.Health:
			return "Health prolongs life";
		case Identifier.Mana:
			return "Mana increases the number of spells that can be cast";
		}
	}

	public static int CompareArmor(InvStat invStat_0, InvStat invStat_1)
	{
		int num = (int)invStat_0.id;
		int num2 = (int)invStat_1.id;
		if (invStat_0.id == Identifier.Armor)
		{
			num -= 10000;
		}
		else if (invStat_0.id == Identifier.Damage)
		{
			num -= 5000;
		}
		if (invStat_1.id == Identifier.Armor)
		{
			num2 -= 10000;
		}
		else if (invStat_1.id == Identifier.Damage)
		{
			num2 -= 5000;
		}
		if (invStat_0.amount < 0)
		{
			num += 1000;
		}
		if (invStat_1.amount < 0)
		{
			num2 += 1000;
		}
		if (invStat_0.modifier == Modifier.Percent)
		{
			num += 100;
		}
		if (invStat_1.modifier == Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	public static int CompareWeapon(InvStat invStat_0, InvStat invStat_1)
	{
		int num = (int)invStat_0.id;
		int num2 = (int)invStat_1.id;
		if (invStat_0.id == Identifier.Damage)
		{
			num -= 10000;
		}
		else if (invStat_0.id == Identifier.Armor)
		{
			num -= 5000;
		}
		if (invStat_1.id == Identifier.Damage)
		{
			num2 -= 10000;
		}
		else if (invStat_1.id == Identifier.Armor)
		{
			num2 -= 5000;
		}
		if (invStat_0.amount < 0)
		{
			num += 1000;
		}
		if (invStat_1.amount < 0)
		{
			num2 += 1000;
		}
		if (invStat_0.modifier == Modifier.Percent)
		{
			num += 100;
		}
		if (invStat_1.modifier == Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}
}
