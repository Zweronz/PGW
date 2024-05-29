using System.Runtime.CompilerServices;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class PlayerMechController : MonoBehaviour
{
	private enum MechStates
	{
		None = 0,
		Active = 1
	}

	public GameObject mechPoint;

	public GameObject mechBody;

	public SkinnedMeshRenderer mechBodyRenderer;

	public SkinnedMeshRenderer mechHandRenderer;

	public SkinnedMeshRenderer mechGunRenderer;

	public Material[] mechGunMaterials;

	public Material[] mechBodyMaterials;

	public WeaponSounds mechWeaponSounds;

	public GameObject mechExplossion;

	public Animation mechBodyAnimation;

	public Animation mechGunAnimation;

	private Player_move_c player_move_c_0;

	private ObscuredFloat obscuredFloat_0 = default(ObscuredFloat);

	private ConsumableData consumableData_0;

	[CompilerGenerated]
	private MechStates mechStates_0;

	[CompilerGenerated]
	private int int_0;

	private MechStates MechStates_0
	{
		[CompilerGenerated]
		get
		{
			return mechStates_0;
		}
		[CompilerGenerated]
		set
		{
			mechStates_0 = value;
		}
	}

	private int Int32_0
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

	private bool Boolean_0
	{
		get
		{
			return MechStates_0 == MechStates.Active;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return MechStates_0 == MechStates.None;
		}
	}

	public float Single_0
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

	public float Single_1
	{
		get
		{
			return ConsumableData_0.Single_0;
		}
	}

	public ConsumableData ConsumableData_0
	{
		get
		{
			if (consumableData_0 != null)
			{
				return consumableData_0;
			}
			consumableData_0 = ConsumablesController.ConsumablesController_0.GetConsumable(Int32_0);
			return consumableData_0;
		}
	}

	private void Awake()
	{
		player_move_c_0 = GetComponent<Player_move_c>();
	}

	public void CrossFadeWeaponAnimation(string string_0)
	{
		mechGunAnimation.CrossFade(string_0);
	}

	public void PlayWeaponAnimation(string string_0)
	{
		mechGunAnimation.Play(string_0);
	}

	public void PlayBodyAnimation(string string_0)
	{
		mechBodyAnimation.Play(string_0);
	}

	public bool ActivateMech()
	{
		if (Boolean_0)
		{
			return false;
		}
		bool bool_;
		if (bool_ = !player_move_c_0.Boolean_4 || player_move_c_0.Boolean_5)
		{
			ConsumablesController.Subscribe(ConsumablesController.EventType.CONS_DEACTIVATE, MechDeactiveteTime);
		}
		InitData(bool_);
		ResetHealthMech();
		SetStateMechActive();
		InitMechBodyState(bool_);
		ActiveteMechSendRPC();
		return true;
	}

	public void ActiveteMechSendRPC()
	{
		if (player_move_c_0.Boolean_5)
		{
			player_move_c_0.PhotonView_0.RPC("ActivateMechRPC", PhotonTargets.Others, Int32_0);
		}
	}

	private void InitData(bool bool_0)
	{
		if (bool_0)
		{
			Int32_0 = UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_CONSUM_MECH);
			if (player_move_c_0.Boolean_16)
			{
				player_move_c_0.ZoomPress();
			}
		}
	}

	private void InitMechBodyState(bool bool_0)
	{
		player_move_c_0.Single_0 = 0f;
		player_move_c_0.fpsPlayerBody.SetActive(false);
		if (player_move_c_0.GameObject_2 != null)
		{
			player_move_c_0.GameObject_2.SetActive(false);
		}
		if (player_move_c_0.Boolean_5 || (!player_move_c_0.Boolean_5 && !player_move_c_0.Boolean_14))
		{
			mechPoint.SetActive(true);
		}
		mechPoint.GetComponent<ActiveObjectFromTimer>().Period = 0f;
		player_move_c_0.myCamera.transform.localPosition = new Vector3(0.12f, 1.34f, -0.3f);
		if (bool_0)
		{
			mechBody.SetActive(false);
			player_move_c_0.gunCamera.fieldOfView = 45f;
		}
		else
		{
			player_move_c_0.bodyCollayder.height = 2.07f;
			player_move_c_0.bodyCollayder.center = new Vector3(0f, 0.19f, 0f);
			player_move_c_0.headCollayder.center = new Vector3(0f, 0.54f, 0f);
		}
		int num = Mathf.Clamp(ArtikulController.ArtikulController_0.GetDowngrades(Int32_0).Count, 0, 4);
		mechBodyRenderer.material = mechBodyMaterials[num];
		mechHandRenderer.material = mechBodyMaterials[num];
		mechGunRenderer.material = mechGunMaterials[num];
		if (player_move_c_0.Boolean_14 && (!player_move_c_0.Boolean_4 || player_move_c_0.Boolean_5))
		{
			mechBodyRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 0.5f));
			mechHandRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 0.5f));
			mechGunRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 0.5f));
		}
		else
		{
			mechBodyRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 1f));
			mechHandRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 1f));
		}
		for (int i = 0; i < mechWeaponSounds.gunFlashDouble.Length; i++)
		{
			mechWeaponSounds.gunFlashDouble[i].GetChild(0).gameObject.SetActive(false);
		}
	}

	public bool HitMech(float float_0)
	{
		float_0 = ((!(Single_0 >= float_0)) ? Single_0 : float_0);
		Single_0 -= float_0;
		if (Single_0 <= 0f)
		{
			DeactivateMech(false);
			return true;
		}
		return false;
	}

	private void ResetHealthMech()
	{
		Single_0 = ConsumableData_0.Single_0;
	}

	private void MechDeactiveteTime(SlotType slotType_0)
	{
		if (slotType_0 == SlotType.SLOT_CONSUM_MECH && !Boolean_1)
		{
			DeactivateMech();
		}
	}

	private void DeactivateMech(bool bool_0 = true)
	{
		if (!Boolean_1)
		{
			SetStateMechNone();
			if (player_move_c_0.Boolean_5)
			{
				ConsumablesController.Unsubscribe(ConsumablesController.EventType.CONS_DEACTIVATE, MechDeactiveteTime);
			}
			ResetMechBodyState();
			if (!player_move_c_0.Boolean_4 || player_move_c_0.Boolean_5)
			{
				ConsumablesController.ConsumablesController_0.ForceStopDurationConsumable(SlotType.SLOT_CONSUM_MECH);
			}
			if (player_move_c_0.Boolean_4 && player_move_c_0.Boolean_5 && bool_0)
			{
				player_move_c_0.PhotonView_0.RPC("DeactivateMechRPC", PhotonTargets.Others);
			}
		}
	}

	private void ResetMechBodyState()
	{
		if (player_move_c_0.GameObject_2 != null)
		{
			if (player_move_c_0.WeaponManager_0.WeaponSounds_0 != null && player_move_c_0.Boolean_5)
			{
				Transform transform = player_move_c_0.GameObject_2.transform;
				transform.position = transform.parent.TransformPoint(player_move_c_0.WeaponManager_0.WeaponSounds_0.gunPosition);
			}
			SlotType activeWeaponSlotType = WeaponController.WeaponController_0.GetActiveWeaponSlotType();
			if (activeWeaponSlotType == SlotType.SLOT_NONE)
			{
				player_move_c_0.ChangeWeapon(activeWeaponSlotType, false);
			}
			player_move_c_0.GameObject_2.SetActive(true);
		}
		player_move_c_0.myCamera.transform.localPosition = new Vector3(0f, 0.7f, 0f);
		if (player_move_c_0.Boolean_4 && !player_move_c_0.Boolean_5)
		{
			if (!player_move_c_0.Boolean_14)
			{
				player_move_c_0.fpsPlayerBody.SetActive(true);
			}
			player_move_c_0.bodyCollayder.height = 1.51f;
			player_move_c_0.bodyCollayder.center = Vector3.zero;
			player_move_c_0.headCollayder.center = Vector3.zero;
			mechExplossion.SetActive(true);
			mechExplossion.GetComponent<ActiveObjectFromTimer>().Period = 1f;
			mechBodyAnimation.Play("Dead");
			mechGunAnimation.Play("Dead");
			mechPoint.GetComponent<ActiveObjectFromTimer>().Period = 0.46f;
		}
		else
		{
			mechPoint.SetActive(false);
			player_move_c_0.gunCamera.fieldOfView = 75f;
		}
	}

	private void SetStateMechActive()
	{
		MechStates_0 = MechStates.Active;
	}

	private void SetStateMechNone()
	{
		MechStates_0 = MechStates.None;
	}

	[RPC]
	public void ActivateMechRPC(int int_1)
	{
		Int32_0 = int_1;
		ActivateMech();
	}

	[RPC]
	public void DeactivateMechRPC()
	{
		if (!player_move_c_0.Boolean_5)
		{
			DeactivateMech(false);
		}
	}
}
