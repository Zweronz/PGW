using System;
using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.data;

[ProtoContract]
[StorageDataKey(typeof(int))]
public sealed class WeaponData
{
	public const float float_0 = 0.0333f;
	
	//id
	[CompilerGenerated]
	private int int_0;
	
	//nothing
	[CompilerGenerated]
	private bool bool_0;

	//idk
	[CompilerGenerated]
	private int int_1;

	//shoot speed modifier
	[CompilerGenerated]
	private float float_1;

	//reload speed modifier
	[CompilerGenerated]
	private float float_2;

	//rocket speed
	[CompilerGenerated]
	private float float_3;

	//dps
	[CompilerGenerated]
	private float float_4;

	//damage
	[CompilerGenerated]
	private float float_5;

	//capacity
	[CompilerGenerated]
	private int int_2;

	//reserves
	[CompilerGenerated]
	private int int_3;

	//start reserves I think
	[CompilerGenerated]
	private int int_4;

	[CompilerGenerated]
	private WeaponType weaponType_0;

	//bloom modifier
	[CompilerGenerated]
	private float float_6;

	//idk
	[CompilerGenerated]
	private bool bool_1;

	[CompilerGenerated]
	private BulletType bulletType_0;

	//rocket explode delay
	[CompilerGenerated]
	private float float_7;

	[ProtoMember(1)]
	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		set
		{
			int_0 = value;
		}
	}

	[ProtoMember(2)]
	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		set
		{
			bool_0 = value;
		}
	}

	[ProtoMember(3)]
	public int Int32_1
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		set
		{
			int_1 = value;
		}
	}

	[ProtoMember(4)]
	public float Single_0
	{
		[CompilerGenerated]
		get
		{
			return float_1;
		}
		[CompilerGenerated]
		set
		{
			float_1 = value;
		}
	}

	[ProtoMember(5)]
	public float Single_1
	{
		[CompilerGenerated]
		get
		{
			return float_2;
		}
		[CompilerGenerated]
		set
		{
			float_2 = value;
		}
	}

	[ProtoMember(6)]
	public float Single_2
	{
		[CompilerGenerated]
		get
		{
			return float_3;
		}
		[CompilerGenerated]
		set
		{
			float_3 = value;
		}
	}

	[ProtoMember(7)]
	public float Single_3
	{
		[CompilerGenerated]
		get
		{
			return float_4;
		}
		[CompilerGenerated]
		set
		{
			float_4 = value;
		}
	}

	[ProtoMember(8)]
	public float Single_4
	{
		[CompilerGenerated]
		get
		{
			return float_5;
		}
		[CompilerGenerated]
		set
		{
			float_5 = value;
		}
	}

	[ProtoMember(9)]
	public int Int32_2
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		set
		{
			int_2 = value;
		}
	}

	[ProtoMember(10)]
	public int Int32_3
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		set
		{
			int_3 = value;
		}
	}

	[ProtoMember(11)]
	public int Int32_4
	{
		[CompilerGenerated]
		get
		{
			return int_4;
		}
		[CompilerGenerated]
		set
		{
			int_4 = value;
		}
	}

	[ProtoMember(12)]
	public WeaponType WeaponType_0
	{
		[CompilerGenerated]
		get
		{
			return weaponType_0;
		}
		[CompilerGenerated]
		set
		{
			weaponType_0 = value;
		}
	}

	[ProtoMember(13)]
	public float Single_5
	{
		[CompilerGenerated]
		get
		{
			return float_6;
		}
		[CompilerGenerated]
		set
		{
			float_6 = value;
		}
	}

	[ProtoMember(14)]
	public bool Boolean_1
	{
		[CompilerGenerated]
		get
		{
			return bool_1;
		}
		[CompilerGenerated]
		set
		{
			bool_1 = value;
		}
	}

	[ProtoMember(15)]
	public BulletType BulletType_0
	{
		[CompilerGenerated]
		get
		{
			return bulletType_0;
		}
		[CompilerGenerated]
		set
		{
			bulletType_0 = value;
		}
	}

	[ProtoMember(16)]
	public float Single_6
	{
		[CompilerGenerated]
		get
		{
			return float_7;
		}
		[CompilerGenerated]
		set
		{
			float_7 = value;
		}
	}

	public ArtikulData ArtikulData_0
	{
		get
		{
			return ArtikulController.ArtikulController_0.GetArtikul(Int32_0);
		}
	}

	public SlotType SlotType_0
	{
		get
		{
			return (ArtikulData_0 == null) ? SlotType.SLOT_WEAPON_PRIMARY : ArtikulData_0.SlotType_0;
		}
	}

	public KillTypeData KillTypeData_0
	{
		get
		{
			return KillTypeStorage.Get.Storage.GetObjectByKey(Int32_1);
		}
	}

	public bool Boolean_2
	{
		get
		{
			return Boolean_3 || Boolean_13 || WeaponType_0 == WeaponType.MELEE;
		}
	}

	public bool Boolean_3
	{
		get
		{
			return WeaponType_0 == WeaponType.SHOT_MELEE;
		}
	}

	public bool Boolean_4
	{
		get
		{
			return WeaponType_0 == WeaponType.SHOTGUN;
		}
	}

	public bool Boolean_5
	{
		get
		{
			return WeaponType_0 == WeaponType.DOUBLE_SHOT;
		}
	}

	public bool Boolean_6
	{
		get
		{
			return WeaponType_0 == WeaponType.MAGIC;
		}
	}

	public bool Boolean_7
	{
		get
		{
			return WeaponType_0 == WeaponType.FLAME_THROWER;
		}
	}

	public bool Boolean_8
	{
		get
		{
			return Boolean_11 || WeaponType_0 == WeaponType.BAZOOKA;
		}
	}

	public bool Boolean_9
	{
		get
		{
			return WeaponType_0 == WeaponType.RAILGUN;
		}
	}

	public bool Boolean_10
	{
		get
		{
			return WeaponType_0 == WeaponType.FREEZER;
		}
	}

	public bool Boolean_11
	{
		get
		{
			return WeaponType_0 == WeaponType.GRANADE_LAUNCHER;
		}
	}

	public bool Boolean_12
	{
		get
		{
			return WeaponType_0 == WeaponType.RAY_AOE;
		}
	}

	public bool Boolean_13
	{
		get
		{
			return WeaponType_0 == WeaponType.MELEE_AOE;
		}
	}

	public bool Boolean_14
	{
		get
		{
			return WeaponType_0 == WeaponType.HOOK;
		}
	}

	public bool Boolean_15
	{
		get
		{
			return Int32_6 > 0;
		}
	}

	public bool Boolean_16
	{
		get
		{
			return ArtikulController.ArtikulController_0.GetSkill(Int32_0, SkillId.SKILL_WEAPON_AREA_DAMAGE) != null;
		}
	}

	public bool Boolean_17
	{
		get
		{
			return ArtikulController.ArtikulController_0.GetSkill(Int32_0, SkillId.SKILL_WEAPON_WALLS_BREAK) != null;
		}
	}

	public bool Boolean_18
	{
		get
		{
			return ArtikulController.ArtikulController_0.GetSkill(Int32_0, SkillId.SKILL_WEAPON_THROUGH_ENEMIES) != null;
		}
	}

	private float Single_7
	{
		get
		{
			SkillData skill = ArtikulController.ArtikulController_0.GetSkill(Int32_0, SkillId.SKILL_SPEED_MODIFIER);
			return (skill != null) ? skill.Single_0 : 0f;
		}
	}

	public int Int32_5
	{
		get
		{
			return (int)Math.Ceiling(Single_7 * 100f);
		}
	}

	public int Int32_6
	{
		get
		{
			SkillData skill = ArtikulController.ArtikulController_0.GetSkill(Int32_0, SkillId.SKILL_WEAPON_ZOOM);
			return (skill != null) ? skill.Int32_0 : 0;
		}
	}

	public float Single_8
	{
		get
		{
			SkillData skill = ArtikulController.ArtikulController_0.GetSkill(Int32_0, SkillId.SKILL_WEAPON_ZOOM_FOV);
			return (skill != null) ? skill.Single_1 : 75f;
		}
	}

	public int Int32_7
	{
		get
		{
			SkillData skill = ArtikulController.ArtikulController_0.GetSkill(Int32_0, SkillId.SKILL_WEAPON_BULLET_COUNT);
			return (skill == null) ? 1 : skill.Int32_0;
		}
	}

	public float Single_9
	{
		get
		{
			SkillData skill = ArtikulController.ArtikulController_0.GetSkill(Int32_0, SkillId.SKILL_EXPLOSION_RADIUS_MODIFIER);
			return (skill != null) ? skill.Single_1 : 1f;
		}
	}

	public float Single_10
	{
		get
		{
			SkillData skill = ArtikulController.ArtikulController_0.GetSkill(Int32_0, SkillId.SKILL_SELF_EXPLOSION_RADIUS_MODIFIER);
			return (skill != null) ? skill.Single_0 : 0f;
		}
	}

	public float Single_11
	{
		get
		{
			SkillData skill = ArtikulController.ArtikulController_0.GetSkill(Int32_0, SkillId.SKILL_EXPLOSION_RADIUS_IMPULS_MODIFIER);
			return (skill != null) ? skill.Single_1 : 1f;
		}
	}

	public float Single_12
	{
		get
		{
			SkillData skill = ArtikulController.ArtikulController_0.GetSkill(Int32_0, SkillId.SKILL_EXPLOSION_DAMAGE_MODIFICATOR_MIN);
			return (skill != null) ? skill.Single_0 : 0f;
		}
	}

	public float Single_13
	{
		get
		{
			SkillData skill = ArtikulController.ArtikulController_0.GetSkill(Int32_0, SkillId.SKILL_SELF_EXPLOSION_DAMAGE_MODIFICATOR_MIN);
			return (skill != null) ? skill.Single_0 : 0f;
		}
	}

	public float Single_14
	{
		get
		{
			SkillData skill = ArtikulController.ArtikulController_0.GetSkill(Int32_0, SkillId.SKILL_WEAPON_RANGE);
			return (skill != null) ? skill.Single_1 : 3f;
		}
	}

	public float Single_15
	{
		get
		{
			SkillData skill = ArtikulController.ArtikulController_0.GetSkill(Int32_0, SkillId.SKILL_WEAPON_MELEE_ANGLE);
			return (skill != null) ? skill.Single_1 : 1f;
		}
	}

	public float Single_16
	{
		get
		{
			SkillData skill = ArtikulController.ArtikulController_0.GetSkill(Int32_0, SkillId.SKILL_WEAPON_MELEE_ATTACK_TIME_MOD);
			return (skill != null) ? skill.Single_1 : 1f;
		}
	}

	public float Single_17
	{
		get
		{
			return (!Boolean_4) ? Single_4 : (Single_4 * 10f);
		}
	}

	public float Single_18
	{
		get
		{
			float num = Math.Max(0.0333f, Single_0);
			float num2 = 1f / num * Single_4;
			if (Boolean_4)
			{
				num2 *= 15f;
			}
			return num2;
		}
	}

	public float Single_19
	{
		get
		{
			float num = (Boolean_2 ? 1 : Int32_2);
			float num2 = Math.Max(0.0333f, Single_0);
			return 60f / (num2 * num + Single_1) * num;
		}
	}
}
