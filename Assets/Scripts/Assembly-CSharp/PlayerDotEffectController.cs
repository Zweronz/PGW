using System.Collections.Generic;
using UnityEngine;
using engine.events;

public sealed class PlayerDotEffectController : MonoBehaviour
{
	private sealed class DotEffect
	{
		public DamageRPCData damageRPCData_0;

		public SkillId skillId_0;

		public float float_0;

		public int int_0;

		public int int_1;

		public int int_2;
	}

	private static PlayerDotEffectController playerDotEffectController_0;

	private PhotonView photonView_0;

	private List<SkillId> list_0 = new List<SkillId>
	{
		SkillId.SKILL_WEAPON_DOT_POISON,
		SkillId.SKILL_WEAPON_DOT_FLAME,
		SkillId.SKILL_WEAPON_DOT_ACID,
		SkillId.SKILL_WEAPON_DOT_FREEZE
	};

	private DotEffect dotEffect_0;

	private Player_move_c player_move_c_0;

	public static float Single_0
	{
		get
		{
			if (!(playerDotEffectController_0 == null) && playerDotEffectController_0.dotEffect_0 != null)
			{
				return (float)(100 - playerDotEffectController_0.dotEffect_0.int_2) / 100f;
			}
			return 1f;
		}
	}

	private bool Boolean_0
	{
		get
		{
			bool result;
			if (result = player_move_c_0.PlayerParametersController_0.Single_2 <= 0f)
			{
				Clear();
			}
			return result;
		}
	}

	private void Awake()
	{
		player_move_c_0 = GetComponent<Player_move_c>();
		photonView_0 = PhotonView.Get(this);
	}

	private void Start()
	{
		playerDotEffectController_0 = this;
		Subscribe();
	}

	private void OnDestroy()
	{
		playerDotEffectController_0 = null;
		Unsubscribe();
		Clear();
	}

	public void Check(DamageRPCData damageRPCData_0)
	{
		if (damageRPCData_0 != null && photonView_0.Boolean_1 && !Boolean_0)
		{
			for (int i = 0; i < list_0.Count; i++)
			{
				UpdateEffect(damageRPCData_0, list_0[i]);
			}
		}
	}

	public bool IsDotDamage(out float float_0)
	{
		float_0 = 0f;
		if (dotEffect_0 == null)
		{
			return false;
		}
		float_0 = (float)dotEffect_0.int_0 / (float)dotEffect_0.int_1;
		return true;
	}

	private void Subscribe()
	{
		if (!DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateOneSecond))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(UpdateOneSecond);
		}
		if (!player_move_c_0.PlayerStateController_0.Contains(OnStartPlayerRespawn, PlayerEvents.StartRespawn))
		{
			player_move_c_0.PlayerStateController_0.Subscribe(OnStartPlayerRespawn, PlayerEvents.StartRespawn);
		}
	}

	public void Unsubscribe()
	{
		if (DependSceneEvent<MainUpdateOneSecond>.Contains(UpdateOneSecond))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalUnsubscribe(UpdateOneSecond);
		}
		if (player_move_c_0.PlayerStateController_0.Contains(OnStartPlayerRespawn, PlayerEvents.StartRespawn))
		{
			player_move_c_0.PlayerStateController_0.Unsubscribe(OnStartPlayerRespawn, PlayerEvents.StartRespawn);
		}
	}

	private void OnStartPlayerRespawn()
	{
		Clear();
	}

	private void Clear()
	{
		dotEffect_0 = null;
	}

	private void UpdateOneSecond()
	{
		if (!Boolean_0 && dotEffect_0 != null)
		{
			if (--dotEffect_0.int_0 <= 0)
			{
				Clear();
				return;
			}
			player_move_c_0.PlayerParametersController_0.HitPlayer(dotEffect_0.float_0, 0f);
			player_move_c_0.ReceiveDamage(dotEffect_0.damageRPCData_0, Boolean_0, false);
		}
	}

	private void UpdateEffect(DamageRPCData damageRPCData_0, SkillId skillId_0)
	{
		SkillData skill = ArtikulController.ArtikulController_0.GetSkill(damageRPCData_0.int_1, skillId_0);
		if (skill == null)
		{
			return;
		}
		if (dotEffect_0 == null)
		{
			dotEffect_0 = new DotEffect
			{
				damageRPCData_0 = damageRPCData_0,
				skillId_0 = skillId_0,
				float_0 = skill.Single_0,
				int_0 = skill.Int32_0,
				int_1 = skill.Int32_0,
				int_2 = ((skillId_0 == SkillId.SKILL_WEAPON_DOT_FREEZE) ? skill.Int32_1 : 0)
			};
			player_move_c_0.PlayerParticleController_0.PlayAnimationDot(skillId_0, true, skill.Int32_0);
			return;
		}
		float num = skill.Single_0 * (float)skill.Int32_0;
		float num2 = dotEffect_0.float_0 * (float)dotEffect_0.int_0;
		if (num > num2)
		{
			if (dotEffect_0.skillId_0 != skillId_0)
			{
				player_move_c_0.PlayerParticleController_0.RemoveAnimationByDot(skillId_0);
			}
			player_move_c_0.PlayerParticleController_0.PlayAnimationDot(skillId_0, true, skill.Int32_0);
			dotEffect_0.damageRPCData_0 = damageRPCData_0;
			dotEffect_0.skillId_0 = skillId_0;
			dotEffect_0.float_0 = skill.Single_0;
			dotEffect_0.int_0 = skill.Int32_0;
			dotEffect_0.int_1 = skill.Int32_0;
			dotEffect_0.int_2 = ((skillId_0 == SkillId.SKILL_WEAPON_DOT_FREEZE) ? skill.Int32_1 : 0);
		}
	}
}
