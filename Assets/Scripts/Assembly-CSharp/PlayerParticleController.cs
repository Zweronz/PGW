using System.Collections.Generic;
using UnityEngine;
using engine.helpers;

public class PlayerParticleController : MonoBehaviour
{
	public enum AnimationType
	{
		HEALTH = 0,
		AMMO = 1,
		ARMOR = 2,
		GRENADE = 3,
		POISON = 4,
		FIRE = 5,
		ELECTRIC = 6,
		FROZEN = 7,
		MAX_TYPE = 8
	}

	public GameObject AnimationPoint;

	private Player_move_c player_move_c_0;

	private PhotonView photonView_0;

	private Dictionary<AnimationType, string> dictionary_0 = new Dictionary<AnimationType, string>
	{
		{
			AnimationType.POISON,
			"Prefabs/Player/Poison_particles"
		},
		{
			AnimationType.FIRE,
			"Prefabs/Player/Fire_particles"
		},
		{
			AnimationType.ELECTRIC,
			"Prefabs/Player/Electric_particles"
		},
		{
			AnimationType.FROZEN,
			"Prefabs/Player/Frozen_particles"
		},
		{
			AnimationType.HEALTH,
			"Prefabs/Player/Health_particles"
		},
		{
			AnimationType.AMMO,
			"Prefabs/Player/Ammo_particles"
		},
		{
			AnimationType.ARMOR,
			"Prefabs/Player/Armor_particles"
		},
		{
			AnimationType.GRENADE,
			"Prefabs/Player/Grenade_particles"
		}
	};

	private Dictionary<AnimationType, GameObject> dictionary_1 = new Dictionary<AnimationType, GameObject>();

	private Dictionary<AnimationType, GameObject> dictionary_2 = new Dictionary<AnimationType, GameObject>();

	private Dictionary<GameObject, float> dictionary_3 = new Dictionary<GameObject, float>();

	private List<GameObject> list_0 = new List<GameObject>();

	private List<GameObject> list_1 = new List<GameObject>();

	private void Awake()
	{
		player_move_c_0 = base.gameObject.GetComponent<Player_move_c>();
	}

	private void Start()
	{
		photonView_0 = PhotonView.Get(this);
	}

	public void PlayAnimationDot(SkillId skillId_0, bool bool_0, float float_0 = 0f)
	{
		AnimationType typeBySkill = GetTypeBySkill(skillId_0);
		if (typeBySkill != AnimationType.MAX_TYPE)
		{
			PlayAnimation(typeBySkill, bool_0, float_0);
		}
	}

	public void PlayAnimation(AnimationType animationType_0, bool bool_0, float float_0 = 0f)
	{
		if (bool_0)
		{
			StartAnimation(animationType_0, float_0);
		}
		if (player_move_c_0.Boolean_4 && player_move_c_0.Boolean_5)
		{
			if (float_0 == 0f)
			{
				photonView_0.RPC("PlayParticleAnimationRPC", PhotonTargets.Others, (int)animationType_0);
			}
			else
			{
				photonView_0.RPC("PlayParticleAnimationTimeRPC", PhotonTargets.Others, (int)animationType_0, float_0);
			}
		}
	}

	public void StartAnimation(AnimationType animationType_0, float float_0)
	{
		if (!dictionary_1.ContainsKey(animationType_0))
		{
			GameObject gameObject = Resources.Load<GameObject>(dictionary_0[animationType_0]);
			if (gameObject == null)
			{
				Log.AddLineError("[PlayerParticleController::PlayAnimation] ERROR can not load animation type: {0}  path: {1}", animationType_0.ToString(), dictionary_0[animationType_0]);
				return;
			}
			dictionary_1.Add(animationType_0, gameObject);
		}
		GameObject gameObject2 = null;
		if (dictionary_2.ContainsKey(animationType_0))
		{
			gameObject2 = dictionary_2[animationType_0];
			if (float_0 <= 0f)
			{
				float_0 = GetTime(gameObject2);
			}
			dictionary_3[gameObject2] = float_0;
			return;
		}
		gameObject2 = Object.Instantiate(dictionary_1[animationType_0]) as GameObject;
		gameObject2.transform.parent = AnimationPoint.transform;
		gameObject2.transform.localPosition = Vector3.zero;
		gameObject2.SetActive(true);
		if (float_0 <= 0f)
		{
			float_0 = GetTime(gameObject2);
		}
		dictionary_3.Add(gameObject2, float_0);
		dictionary_2.Add(animationType_0, gameObject2);
	}

	public void RemoveAnimationByDot(SkillId skillId_0)
	{
		AnimationType typeBySkill = GetTypeBySkill(skillId_0);
		if (dictionary_2.ContainsKey(typeBySkill))
		{
			dictionary_3[dictionary_2[typeBySkill]] = 0f;
		}
	}

	public void RemoveAnimation(AnimationType animationType_0)
	{
		if (dictionary_2.ContainsKey(animationType_0))
		{
			dictionary_3[dictionary_2[animationType_0]] = 0f;
		}
	}

	private void Update()
	{
		if (dictionary_3.Count == 0)
		{
			return;
		}
		list_1.Clear();
		list_1.AddRange(dictionary_3.Keys);
		float num = 0f;
		float deltaTime = Time.deltaTime;
		list_0.Clear();
		foreach (GameObject item in list_1)
		{
			num = dictionary_3[item] - deltaTime;
			if (num > 0f)
			{
				dictionary_3[item] = num;
			}
			else
			{
				list_0.Add(item);
			}
		}
		for (int i = 0; i < list_0.Count; i++)
		{
			GameObject gameObject = list_0[i];
			dictionary_3.Remove(gameObject);
			foreach (KeyValuePair<AnimationType, GameObject> item2 in dictionary_2)
			{
				if (item2.Value.Equals(gameObject))
				{
					dictionary_2.Remove(item2.Key);
					break;
				}
			}
			gameObject.SetActive(false);
			Object.Destroy(gameObject);
		}
	}

	private float GetTime(GameObject gameObject_0)
	{
		float num = 0f;
		if (gameObject_0.GetComponent<ParticleSystem>() != null)
		{
			num = gameObject_0.GetComponent<ParticleSystem>().duration;
		}
		else if (gameObject_0.transform.childCount > 0)
		{
			GameObject gameObject = gameObject_0.transform.GetChild(0).gameObject;
			if (gameObject.GetComponent<ParticleSystem>() != null)
			{
				num = gameObject.GetComponent<ParticleSystem>().duration;
			}
		}
		if (num == 0f)
		{
			num = 1f;
			Log.AddLineWarning("[PlayerParticleController::GetTime] WARNING time not set! Set default time = 1s");
		}
		return num;
	}

	private AnimationType GetTypeBySkill(SkillId skillId_0)
	{
		AnimationType result = AnimationType.MAX_TYPE;
		switch (skillId_0)
		{
		case SkillId.SKILL_WEAPON_DOT_POISON:
			result = AnimationType.POISON;
			break;
		case SkillId.SKILL_WEAPON_DOT_FLAME:
			result = AnimationType.FIRE;
			break;
		case SkillId.SKILL_WEAPON_DOT_ACID:
			result = AnimationType.ELECTRIC;
			break;
		case SkillId.SKILL_WEAPON_DOT_FREEZE:
			result = AnimationType.FROZEN;
			break;
		}
		return result;
	}

	[RPC]
	public void PlayParticleAnimationRPC(int int_0)
	{
		StartAnimation((AnimationType)int_0, 0f);
	}

	[RPC]
	public void PlayParticleAnimationTimeRPC(int int_0, float float_0)
	{
		StartAnimation((AnimationType)int_0, float_0);
	}
}
