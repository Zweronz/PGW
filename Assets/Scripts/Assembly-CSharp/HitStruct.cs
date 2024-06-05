using System.Runtime.CompilerServices;
using UnityEngine;
using pixelgun.tutorial;

public sealed class HitStruct
{
	private static HitStruct hitStruct_0 = new HitStruct();

	public string string_0 = string.Empty;

	public int int_0;

	public int int_1;

	public bool bool_0;

	public bool bool_1;

	public bool bool_2;

	public SlotType slotType_0;

	public DeadType deadType_0;

	public float float_0;

	public float float_1;

	public float float_2;

	public float float_3;

	public float float_4;

	public float float_5;

	public float float_6;

	public bool bool_3;

	public bool bool_4;

	public bool bool_5;

	public bool bool_6;

	public bool bool_7;

	public bool bool_8;

	public bool bool_9;

	public bool bool_10;

	public bool bool_11;

	public bool bool_12;

	public bool bool_13;

	public bool bool_14;

	public bool bool_15;

	public bool bool_16;

	public float float_7;

	public float float_8;

	public float float_9;

	public float float_10;

	public float float_11;

	public float float_12;

	public float float_13;

	public WeaponSounds weaponSounds_0;

	public Vector2 vector2_0 = default(Vector2);

	public Vector3 vector3_0;

	public Vector3 vector3_1;

	[CompilerGenerated]
	private int int_2;

	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		private set
		{
			int_2 = value;
		}
	}

	public Vector2 Vector2_0
	{
		get
		{
			if (!bool_13)
			{
				return Vector2.zero;
			}
			return weaponSounds_0.shiftPositionTrunks[Int32_0];
		}
	}

	public void IncrementCounterBulletsInShoot()
	{
		if (bool_13)
		{
			Int32_0 = (Int32_0 + 1) % weaponSounds_0.shiftPositionTrunks.Count;
		}
	}

	private void Clear()
	{
		string_0 = string.Empty;
		int_0 = 0;
		int_1 = 0;
		bool_0 = false;
		bool_1 = false;
		bool_2 = false;
		slotType_0 = SlotType.SLOT_NONE;
		deadType_0 = DeadType.ANGEL;
		float_0 = 0f;
		float_1 = 0f;
		float_2 = 0f;
		float_3 = 0f;
		float_4 = 0f;
		float_5 = 0f;
		float_6 = 0f;
		bool_3 = false;
		bool_4 = false;
		bool_5 = false;
		bool_6 = false;
		bool_7 = false;
		bool_8 = false;
		bool_9 = false;
		bool_10 = false;
		bool_11 = false;
		bool_12 = false;
		bool_13 = false;
		bool_14 = false;
		bool_15 = false;
		bool_16 = false;
		float_7 = 0f;
		float_8 = 0f;
		float_9 = 0f;
		float_10 = 0f;
		float_11 = 0f;
		float_12 = 0f;
		float_13 = 0f;
		weaponSounds_0 = null;
		vector2_0 = Vector2.zero;
		vector3_0 = Vector3.zero;
		vector3_1 = Vector3.zero;
	}

	public static HitStruct GenerateHitStruct(WeaponData weaponData_0, bool bool_17, bool bool_18, bool bool_19, string string_1, WeaponSounds weaponSounds_1 = null)
	{
		HitStruct hitStruct = null;
		if (bool_17)
		{
			hitStruct = new HitStruct();
		}
		else
		{
			hitStruct = hitStruct_0;
			hitStruct.Clear();
		}
		hitStruct.string_0 = string_1;
		hitStruct.int_0 = weaponData_0.Int32_0;
		hitStruct.bool_0 = bool_18;
		hitStruct.bool_1 = bool_19;
		hitStruct.slotType_0 = weaponData_0.SlotType_0;
		hitStruct.deadType_0 = DeadType.ANGEL;//((weaponData_0.KillTypeData_0 != null) ? weaponData_0.KillTypeData_0.DeadType_0 : DeadType.ANGEL);
		hitStruct.float_0 = weaponData_0.Single_4;
		hitStruct.float_1 = UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_WEAPON_DAMAGE_MODIFIER);
		hitStruct.float_2 = UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_HEADSHOT_DAMAG_MODIFIER);
		hitStruct.float_3 = UserController.UserController_0.GetFloatMultModifier(SkillId.SKILL_SELF_EXPLOSION_DAMAGE_MODIFICATOR);
		hitStruct.float_4 = UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_EXPLOSION_DAMAGE_MODIFIER);
		hitStruct.float_5 = UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_WEAPON_DAMAGE_CRIT_CHANCE);
		hitStruct.float_6 = UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_WEAPON_DAMAGE_CRIT_MODIFIER);
		hitStruct.bool_3 = weaponData_0.Boolean_2;
		hitStruct.bool_4 = weaponData_0.Boolean_3;
		hitStruct.bool_5 = weaponData_0.Boolean_4;
		hitStruct.bool_6 = weaponData_0.Boolean_5;
		hitStruct.bool_7 = weaponData_0.Boolean_6;
		hitStruct.bool_8 = weaponData_0.Boolean_7;
		hitStruct.bool_9 = weaponData_0.Boolean_8;
		hitStruct.bool_10 = weaponData_0.Boolean_9;
		hitStruct.bool_11 = weaponData_0.Boolean_10;
		hitStruct.bool_12 = weaponData_0.Boolean_11;
		hitStruct.bool_13 = !(weaponSounds_1 == null) && weaponSounds_1.Boolean_0;
		hitStruct.bool_14 = weaponData_0.Boolean_18;
		hitStruct.bool_15 = weaponData_0.Boolean_13;
		hitStruct.bool_16 = weaponData_0.Boolean_14;
		hitStruct.float_7 = ((!TutorialController.TutorialController_0.Boolean_0) ? 1f : 2f) * weaponData_0.Single_9 * (1f + UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_EXPLOSION_RADIUS_MODIFIER));
		hitStruct.float_8 = weaponData_0.Single_10;
		hitStruct.float_9 = weaponData_0.Single_12;
		hitStruct.float_10 = weaponData_0.Single_13;
		hitStruct.float_13 = 0f;
		hitStruct.float_11 = weaponData_0.Single_14;
		if (bool_18)
		{
			ConsumableData consumableData_ = WeaponManager.weaponManager_0.myPlayerMoveC.PlayerMechController_0.ConsumableData_0;
			hitStruct.float_0 = consumableData_.Single_1;
			SkillData skill = ArtikulController.ArtikulController_0.GetSkill(consumableData_.Int32_0, SkillId.SKILL_WEAPON_RANGE);
			if (skill != null)
			{
				hitStruct.float_11 = skill.Single_1;
			}
			else
			{
				hitStruct.float_11 = 100f;
			}
		}
		hitStruct.float_12 = weaponData_0.Single_15;
		hitStruct.weaponSounds_0 = weaponSounds_1;
		hitStruct.vector2_0.x = ((!(weaponSounds_1 == null)) ? weaponSounds_1.Single_0 : 0f);
		hitStruct.vector2_0.y = ((!(weaponSounds_1 == null)) ? weaponSounds_1.Single_1 : 0f);
		Debug.Log(hitStruct.vector2_0);
		if (bool_18)
		{
			Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
			hitStruct.string_0 = "Chat_Mech";
			hitStruct.int_0 = myPlayerMoveC.PlayerMechController_0.ConsumableData_0.Int32_0;
			hitStruct.deadType_0 = DeadType.ANGEL;
		}
		return hitStruct;
	}

	public static HitStruct GenerateHitStruct(float float_14, float float_15)
	{
		HitStruct hitStruct = new HitStruct();
		hitStruct.float_0 = float_15;
		hitStruct.float_7 = float_14;
		return hitStruct;
	}

	public static HitStruct GenerateHitStructTurret(int int_3, int int_4, float float_14, float float_15, Vector3 vector3_2, Vector3 vector3_3)
	{
		HitStruct hitStruct = hitStruct_0;
		hitStruct.Clear();
		hitStruct.bool_2 = true;
		hitStruct.int_1 = int_4;
		hitStruct.int_0 = int_3;
		hitStruct.float_0 = float_14;
		hitStruct.float_11 = float_15;
		hitStruct.vector3_0 = vector3_2;
		hitStruct.vector3_1 = vector3_3;
		return hitStruct;
	}
}
