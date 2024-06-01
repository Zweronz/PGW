using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using engine.unity;
using pixelgun.tutorial;

public class PlayerParametersController : MonoBehaviour
{
	private Player_move_c player_move_c_0;

	private PhotonView photonView_0;

	private float float_0;

	private float float_1;

	private ObscuredFloat obscuredFloat_0 = default(ObscuredFloat);

	private ObscuredFloat obscuredFloat_1 = default(ObscuredFloat);

	private ObscuredFloat obscuredFloat_2 = default(ObscuredFloat);

	private ObscuredFloat obscuredFloat_3 = default(ObscuredFloat);

	public bool isStarted;

	public float Single_0
	{
		get
		{
			return 100f;//UserController.UserController_0.GetUserLeveData().Single_0;
		}
	}

	public float Single_1
	{
		get
		{
			return obscuredFloat_0;
		}
		private set
		{
			obscuredFloat_0 = value;
		}
	}

	public float Single_2
	{
		get
		{
			return obscuredFloat_1;
		}
		private set
		{
			obscuredFloat_1 = value;
		}
	}

	public bool Boolean_0
	{
		get
		{
			return Single_2 >= Single_0;
		}
	}

	public float Single_3
	{
		get
		{
			return Mathf.Max(0f, Single_0 - Single_2);
		}
	}

	public float Single_4
	{
		get
		{
			float single_ = UserController.UserController_0.GetUserLeveData().Single_1;
			return single_ + Single_5;
		}
	}

	public float Single_5
	{
		get
		{
			float floatSummModifier = UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_ARMOR);
			float num = 1f + UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_ARMOR_MODIFIER);
			return floatSummModifier * num;
		}
	}

	public float Single_6
	{
		get
		{
			return obscuredFloat_2;
		}
		private set
		{
			obscuredFloat_2 = value;
		}
	}

	public float Single_7
	{
		get
		{
			return obscuredFloat_3;
		}
		private set
		{
			obscuredFloat_3 = value;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return Single_7 >= Single_4;
		}
	}

	private float Single_8
	{
		get
		{
			return UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_ARMOR_REGENERATION_TIME);
		}
	}

	private void Awake()
	{
		player_move_c_0 = base.gameObject.GetComponent<Player_move_c>();
		photonView_0 = PhotonView.Get(player_move_c_0);
	}

	private void Start()
	{
		ResetHealthAndArmor();
		isStarted = true;
	}

	private void Update()
	{
		RegenerationArmorTick();
	}

	public void ResetHealthAndArmor(bool bool_0 = true)
	{
		if (photonView_0.Boolean_1)
		{
			Single_1 = Single_0;
			Single_2 = Single_0;
			if (bool_0)
			{
				Single_6 = Single_5;
				Single_7 = Single_5;
			}
			float_0 = Single_8;
			float_1 = ((float_0 == 0f) ? 0f : (Time.time + float_0));
			SendSynhAndArmorHealth();
		}
	}

	public void ForceKillPlayer()
	{
		HitPlayer(Single_7 + Single_2, 0f);
	}

	public void HitPlayer(float float_2, float float_3 = 0f, bool bool_0 = true)
	{
		if (!TutorialController.TutorialController_0.Boolean_0)
		{
			float num = float_2 * ((float_3 != 0f) ? float_3 : 1f);
			float num2 = float_2 - num;
			if (Single_7 >= num)
			{
				Single_7 -= num;
			}
			else
			{
				num2 += num - Single_7;
				Single_7 = 0f;
			}
			Single_2 = Mathf.Max(Single_2 - num2, 0f);
			CurrentCampaignGame.bool_0 = false;
			if (bool_0)
			{
				SendSynhAndArmorHealth();
			}
		}
	}

	public void AddHealth(float float_2)
	{
		float value = Single_2 + float_2;
		Single_2 = Mathf.Clamp(value, 0f, Single_0);
		SendSynhAndArmorHealth();
	}

	public float AddArmor(float float_2)
	{
		float single_ = Single_7;
		float value = Single_7 + float_2;
		Single_7 = Mathf.Clamp(value, 0f, Single_4);
		SendSynhAndArmorHealth();
		return Single_7 - single_;
	}

	public void SendSynhAndArmorHealth()
	{
		if (Defs.bool_2)
		{
			photonView_0.PhotonView_0.RPC("SynhHealthAndArmorRPC", PhotonTargets.All, Single_2, Single_7, Single_1, Single_6);
		}
	}

	public void OnUseConsumableForAllStatistics(SlotType slotType_0)
	{
		photonView_0.PhotonView_0.RPC("OnUseConsumableForAllStatisticsRPC", PhotonTargets.Others, UserController.UserController_0.UserData_0.user_0.int_0, (int)slotType_0);
	}

	[RPC]
	private void OnUseConsumableForAllStatisticsRPC(int int_0, int int_1)
	{
		MonoSingleton<FightController>.Prop_0.FightStatController_0.OnUseConsumable(int_0, (SlotType)int_1);
	}

	private void OnPhotonPlayerConnected(PhotonPlayer photonPlayer_0)
	{
		if (photonView_0.Boolean_1)
		{
			SendSynhAndArmorHealth();
		}
	}

	[RPC]
	private void SynhHealthAndArmorRPC(float float_2, float float_3, float float_4, float float_5)
	{
		Single_1 = float_4;
		Single_2 = float_2;
		Single_6 = float_5;
		Single_7 = float_3;
		if (float_2 > 0f)
		{
			player_move_c_0.Boolean_21 = false;
			player_move_c_0.myPersonNetwork.bool_2 = false;
		}
	}

	private void RegenerationArmorTick()
	{
		if (float_0 != 0f && float_1 <= Time.time)
		{
			float_1 = Time.time + float_0;
			AddArmor(1f);
		}
	}
}
