using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.unity;
using pixelgun.tutorial;

public sealed class PlayerGrenadeController : MonoBehaviour
{
	private enum GrenadeStates
	{
		None = 0,
		CreateProcess = 1,
		MayBeDrop = 2,
		Drop = 3
	}

	public const string string_0 = "Prefabs/Grenades/Rocket";

	public AudioClip ChangeGrenadeClip;

	private Player_move_c player_move_c_0;

	private float float_0;

	[CompilerGenerated]
	private GameObject gameObject_0;

	[CompilerGenerated]
	private GrenadeStates grenadeStates_0;

	private GameObject GameObject_0
	{
		[CompilerGenerated]
		get
		{
			return gameObject_0;
		}
		[CompilerGenerated]
		set
		{
			gameObject_0 = value;
		}
	}

	private GrenadeStates GrenadeStates_0
	{
		[CompilerGenerated]
		get
		{
			return grenadeStates_0;
		}
		[CompilerGenerated]
		set
		{
			grenadeStates_0 = value;
		}
	}

	public string String_0
	{
		get
		{
			return GrenadeStates_0.ToString();
		}
	}

	public bool Boolean_0
	{
		get
		{
			return GrenadeStates_0 == GrenadeStates.None;
		}
	}

	private bool Boolean_1
	{
		get
		{
			return GrenadeStates_0 == GrenadeStates.CreateProcess;
		}
	}

	private bool Boolean_2
	{
		get
		{
			return GrenadeStates_0 == GrenadeStates.MayBeDrop;
		}
	}

	private bool Boolean_3
	{
		get
		{
			return GrenadeStates_0 == GrenadeStates.Drop;
		}
	}

	private bool Boolean_4
	{
		get
		{
			if (!player_move_c_0.PlayerMechController_0.Boolean_1)
			{
				return false;
			}
			if (!player_move_c_0.PlayerTurretController_0.Boolean_1)
			{
				return false;
			}
			if (!Boolean_0)
			{
				return false;
			}
			if (!ConsumablesController.ConsumablesController_0.IsSlotAnyTypeMayBeUsed(SlotType.SLOT_CONSUM_GRENADE))
			{
				return false;
			}
			if (player_move_c_0.PlayerParametersController_0.Single_2 <= 0f)
			{
				return false;
			}
			if (player_move_c_0.Boolean_20)
			{
				return false;
			}
			return true;
		}
	}

	private void Awake()
	{
		player_move_c_0 = GetComponent<Player_move_c>();
	}

	public void SetStateGrenadeNone()
	{
		GrenadeStates_0 = GrenadeStates.None;
	}

	private void SetStateGrenadeCreateProcess()
	{
		GrenadeStates_0 = GrenadeStates.CreateProcess;
	}

	private void SetStateGrenadeMayBeDrop()
	{
		GrenadeStates_0 = GrenadeStates.MayBeDrop;
	}

	private void SetStateGrenadeDrop()
	{
		GrenadeStates_0 = GrenadeStates.Drop;
	}

	public bool GrenadePress()
	{
		if (!Boolean_4)
		{
			return false;
		}
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.StopReloadAmmo();
		}
		SetStateGrenadeCreateProcess();
		player_move_c_0.PlayerStateController_0.Subscribe(OnChangeWeaponGrenade, PlayerEvents.EndChangeWeapon);
		player_move_c_0.ChangeWeapon(SlotType.SLOT_CONSUM_GRENADE, false);
		float_0 = Time.realtimeSinceStartup;
		MonoSingleton<FightController>.Prop_0.FightStatController_0.OnUseConsumable(UsersData.UsersData_0.UserData_0.user_0.int_0, SlotType.SLOT_CONSUM_GRENADE);
		player_move_c_0.PlayerParametersController_0.OnUseConsumableForAllStatistics(SlotType.SLOT_CONSUM_GRENADE);
		return true;
	}

	public void CreateGrenade(WeaponSounds weaponSounds_0)
	{
		if (Boolean_1 || Boolean_2)
		{
			GameObject gameObject = null;
			if (player_move_c_0.Boolean_4)
			{
				gameObject = PhotonNetwork.Instantiate("Prefabs/Grenades/Rocket", new Vector3(-10000f, -10000f, -10000f), base.transform.rotation, 0);
			}
			else
			{
				GameObject original = Resources.Load("Prefabs/Grenades/Rocket") as GameObject;
				gameObject = Object.Instantiate(original, new Vector3(-10000f, -10000f, -10000f), base.transform.rotation) as GameObject;
			}
			if (gameObject != null)
			{
				bool bool_ = !player_move_c_0.PlayerMechController_0.Boolean_1;
				Rocket component = gameObject.GetComponent<Rocket>();
				component.Int32_0 = 10;
				component.Single_0 = ((!TutorialController.TutorialController_0.Boolean_0) ? (weaponSounds_0.WeaponData_0.Single_11 * (1f + UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_EXPLOSION_RADIUS_IMPULS_MODIFIER))) : 0f);
				component.HitStruct_0 = HitStruct.GenerateHitStruct(weaponSounds_0.WeaponData_0, true, bool_, player_move_c_0.Boolean_16, "WeaponGrenade", weaponSounds_0);
				gameObject.GetComponent<Rigidbody>().useGravity = false;
				gameObject.GetComponent<Rigidbody>().isKinematic = true;
			}
			GameObject_0 = gameObject;
		}
	}

	public void GrenadeFire()
	{
		if (Boolean_1)
		{
			SetStateGrenadeMayBeDrop();
		}
		else if (Boolean_2)
		{
			SetStateGrenadeDrop();
			float num = Time.realtimeSinceStartup - float_0;
			if (num - 0.4f > 0f)
			{
				GrenadeStartFire();
			}
			else
			{
				Invoke("GrenadeStartFire", 0.4f - num);
			}
		}
	}

	[Obfuscation(Exclude = true)]
	private void GrenadeStartFire()
	{
		player_move_c_0.Boolean_23 = false;
		player_move_c_0.PhotonView_0.RPC("SetActiveEffectsRPC", PhotonTargets.All, false, 0);
		player_move_c_0.PhotonView_0.RPC("OnShoot", PhotonTargets.All);
		Invoke("RunGrenade", 0.2667f);
		player_move_c_0.PlayerStateController_0.DispatchGrenadeFire();
	}

	[Obfuscation(Exclude = true)]
	private void RunGrenade()
	{
		if (GameObject_0 != null)
		{
			GameObject_0.GetComponent<Rigidbody>().isKinematic = false;
			GameObject_0.GetComponent<Rigidbody>().AddForce(150f * player_move_c_0.myTransform.forward);
			GameObject_0.GetComponent<Rigidbody>().useGravity = true;
			GameObject_0.GetComponent<Rocket>().StartRocket();
		}
		Invoke("ReturnWeaponAfterGrenade", 0.5f);
	}

	private void ReturnWeaponAfterGrenade()
	{
		player_move_c_0.PlayerStateController_0.Subscribe(OnEndUsedGrenade, PlayerEvents.EndChangeWeapon);
		player_move_c_0.ChangePreviousWeapon(false);
	}

	private void OnChangeWeaponGrenade()
	{
		player_move_c_0.PlayerStateController_0.Unsubscribe(OnChangeWeaponGrenade, PlayerEvents.EndChangeWeapon);
		if (Boolean_2)
		{
			GrenadeFire();
		}
		else
		{
			SetStateGrenadeMayBeDrop();
		}
	}

	private void OnEndUsedGrenade()
	{
		player_move_c_0.PlayerStateController_0.Unsubscribe(OnEndUsedGrenade, PlayerEvents.EndChangeWeapon);
		SetStateGrenadeNone();
	}

	public void CancelGrenadeProcessInvokes()
	{
		CancelInvoke("GrenadeStartFire");
		CancelInvoke("RunGrenade");
		CancelInvoke("ReturnWeaponAfterGrenade");
	}
}
