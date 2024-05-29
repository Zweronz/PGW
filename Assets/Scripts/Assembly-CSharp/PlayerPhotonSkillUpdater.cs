using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public sealed class PlayerPhotonSkillUpdater : MonoBehaviour
{
	private Dictionary<int, object> dictionary_0 = new Dictionary<int, object>
	{
		{ 16, 0f },
		{ 18, 0f },
		{ 41, 0f }
	};

	private PhotonView photonView_0;

	[CompilerGenerated]
	private Player_move_c player_move_c_0;

	[CompilerGenerated]
	private float float_0;

	[CompilerGenerated]
	private float float_1;

	[CompilerGenerated]
	private float float_2;

	public Player_move_c Player_move_c_0
	{
		[CompilerGenerated]
		get
		{
			return player_move_c_0;
		}
		[CompilerGenerated]
		set
		{
			player_move_c_0 = value;
		}
	}

	public float Single_0
	{
		[CompilerGenerated]
		get
		{
			return float_0;
		}
		[CompilerGenerated]
		private set
		{
			float_0 = value;
		}
	}

	public float Single_1
	{
		[CompilerGenerated]
		get
		{
			return float_1;
		}
		[CompilerGenerated]
		private set
		{
			float_1 = value;
		}
	}

	public float Single_2
	{
		[CompilerGenerated]
		get
		{
			return float_2;
		}
		[CompilerGenerated]
		private set
		{
			float_2 = value;
		}
	}

	private bool Boolean_0
	{
		get
		{
			return !Player_move_c_0.Boolean_4 || photonView_0.Boolean_1;
		}
	}

	private T GetValue<T>(SkillId skillId_0)
	{
		return GetValue(skillId_0, default(T));
	}

	private T GetValue<T>(SkillId skillId_0, T gparam_0)
	{
		object value = null;
		if (!dictionary_0.TryGetValue((int)skillId_0, out value))
		{
			return gparam_0;
		}
		if (value == null)
		{
			return gparam_0;
		}
		return (T)Convert.ChangeType(value, typeof(T));
	}

	private void Awake()
	{
		photonView_0 = PhotonView.Get(this);
	}

	private void OnEnable()
	{
		if (Player_move_c_0.Boolean_25)
		{
			Subscribe();
		}
	}

	private void OnDisable()
	{
		if (Boolean_0)
		{
			LocalUserData.Unsubscribe(LocalUserData.EventType.SKILLS_UPDATE, onSkillUpdate);
		}
	}

	public void ForceSendSkillsRpc(PhotonPlayer photonPlayer_0)
	{
		if (Boolean_0)
		{
			SendSkillsRpc(photonPlayer_0);
		}
	}

	public void ForceUpdateSkills()
	{
		if (Boolean_0)
		{
			onSkillUpdate();
		}
	}

	private void Subscribe()
	{
		if (Boolean_0)
		{
			LocalUserData.Subscribe(LocalUserData.EventType.SKILLS_UPDATE, onSkillUpdate);
		}
	}

	private void onSkillUpdate(int int_0 = 0)
	{
		bool flag = false;
		object obj = null;
		object obj2 = null;
		int[] array = dictionary_0.Keys.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			SkillId skillId = (SkillId)array[i];
			obj = dictionary_0[array[i]];
			obj2 = null;
			switch (skillId)
			{
			case SkillId.SKILL_RELOAD_WEAPON_TIME_MODIFIER:
			{
				float num = 0f;
				obj2 = WeaponManager.weaponManager_0.WeaponSounds_0.PlayReloadAnimation(out num, false);
				break;
			}
			case SkillId.SKILL_ARMOR_ABSORB:
				obj2 = UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_ARMOR_ABSORB);
				break;
			case SkillId.SKILL_HEADSHOT_DAMAG_IGNORE_MODIFIER:
				obj2 = UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_HEADSHOT_DAMAG_IGNORE_MODIFIER);
				break;
			}
			if (obj2 != null)
			{
				flag = flag || obj2 != obj;
				dictionary_0[array[i]] = obj2;
			}
		}
		if (flag)
		{
			UpdatePropertys();
			SendSkillsRpc();
		}
	}

	private void SendSkillsRpc(PhotonPlayer photonPlayer_0 = null)
	{
		if (photonPlayer_0 == null)
		{
			photonView_0.RPC("SkillsSynch", PhotonTargets.Others, dictionary_0);
		}
		else
		{
			photonView_0.RPC("SkillsSynch", photonPlayer_0, dictionary_0);
		}
	}

	private void UpdatePropertys()
	{
		Single_0 = GetValue<float>(SkillId.SKILL_RELOAD_WEAPON_TIME_MODIFIER);
		Single_1 = GetValue<float>(SkillId.SKILL_HEADSHOT_DAMAG_IGNORE_MODIFIER);
		Single_2 = GetValue<float>(SkillId.SKILL_ARMOR_ABSORB);
	}

	[RPC]
	public void SkillsSynch(Dictionary<int, object> dictionary_1)
	{
		dictionary_0 = dictionary_1;
		UpdatePropertys();
	}
}
