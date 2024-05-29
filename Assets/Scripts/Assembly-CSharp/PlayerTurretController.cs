using System.Runtime.CompilerServices;
using UnityEngine;

public sealed class PlayerTurretController : MonoBehaviour
{
	private enum TurretStates
	{
		None = 0,
		ProcessSettins = 1,
		ProcessBuilding = 2
	}

	public GameObject turretPoint;

	public GameObject currentTurret;

	private Player_move_c player_move_c_0;

	[CompilerGenerated]
	private TurretStates turretStates_0;

	[CompilerGenerated]
	private bool bool_0;

	private TurretStates TurretStates_0
	{
		[CompilerGenerated]
		get
		{
			return turretStates_0;
		}
		[CompilerGenerated]
		set
		{
			turretStates_0 = value;
		}
	}

	private bool Boolean_0
	{
		get
		{
			return TurretStates_0 == TurretStates.ProcessSettins;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return TurretStates_0 == TurretStates.None;
		}
	}

	public bool Boolean_2
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

	private void Awake()
	{
		player_move_c_0 = GetComponent<Player_move_c>();
	}

	public GameObject GetTurretData(out int int_0, out int int_1)
	{
		int_1 = 0;
		int_0 = 0;
		int artikulIdFromSlot = UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_CONSUM_TURRET);
		if (artikulIdFromSlot == 0)
		{
			return null;
		}
		ConsumableData consumable = ConsumablesController.ConsumablesController_0.GetConsumable(artikulIdFromSlot);
		if (consumable == null)
		{
			return null;
		}
		int_1 = consumable.Int32_0;
		int_0 = consumable.Int32_1;
		return UserController.UserController_0.GetGameObject(consumable.Int32_1);
	}

	public void SetStateTurretNone()
	{
		TurretStates_0 = TurretStates.None;
	}

	public void CreateTurret()
	{
		TurretStates_0 = TurretStates.ProcessSettins;
		ConsumablesController.Subscribe(ConsumablesController.EventType.CONS_DEACTIVATE, TurretDeactiveteTime);
		ArtikulData artikulDataFromSlot = UserController.UserController_0.GetArtikulDataFromSlot(SlotType.SLOT_CONSUM_TURRET);
		GameObject gameObject = PhotonNetwork.Instantiate(artikulDataFromSlot.String_3, new Vector3(-10000f, -10000f, -10000f), base.transform.rotation, 0);
		if (gameObject != null)
		{
			TurretController component = gameObject.GetComponent<TurretController>();
			gameObject.GetComponent<Rigidbody>().useGravity = false;
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
			component.Int32_0 = artikulDataFromSlot.Int32_0;
		}
		currentTurret = gameObject;
	}

	public void CancelTurret(bool bool_1 = true)
	{
		if (Boolean_1)
		{
			return;
		}
		if (TurretStates_0 == TurretStates.ProcessSettins)
		{
			if (bool_1)
			{
				player_move_c_0.ChangePreviousWeapon(false);
			}
			TurretDeactivation();
			SetStateTurretNone();
		}
		else
		{
			if (HeadUpDisplay.HeadUpDisplay_0 != null)
			{
				HeadUpDisplay.HeadUpDisplay_0.EndTurretBuilding();
			}
			TurretDeactivation();
			TurretEndBuilding();
		}
	}

	public bool RunTurret()
	{
		if (!Boolean_0)
		{
			return false;
		}
		if (!Boolean_2)
		{
			return false;
		}
		if (ConsumablesController.ConsumablesController_0.UseDurationConsumableSlot(SlotType.SLOT_CONSUM_TURRET) != 0)
		{
			return false;
		}
		currentTurret.transform.parent = null;
		currentTurret.GetComponent<TurretController>().StartTurret();
		player_move_c_0.PlayerParametersController_0.OnUseConsumableForAllStatistics(SlotType.SLOT_CONSUM_TURRET);
		ConsumableData consumable = ConsumablesController.ConsumablesController_0.GetConsumable(UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_CONSUM_TURRET));
		if (consumable.Int32_3 > 0)
		{
			FirstPersonPlayerController.FirstPersonPlayerController_0.SetState(FirstPersonPlayerController.State.Turret_building);
			TurretStates_0 = TurretStates.ProcessBuilding;
			HeadUpDisplay.HeadUpDisplay_0.ShowCircularIndicatorOnTurretBuilding(consumable.Int32_3);
		}
		else
		{
			player_move_c_0.ChangePreviousWeapon(false);
			SetStateTurretNone();
		}
		return true;
	}

	public void TurretEndBuilding()
	{
		player_move_c_0.ChangePreviousWeapon(false);
		FirstPersonPlayerController.FirstPersonPlayerController_0.SetState(FirstPersonPlayerController.State.Default);
		SetStateTurretNone();
	}

	private void TurretDeactiveteTime(SlotType slotType_0)
	{
		if (slotType_0 == SlotType.SLOT_CONSUM_TURRET)
		{
			TurretDeactivation();
		}
	}

	private void TurretDeactivation(bool bool_1 = true)
	{
		ConsumablesController.Unsubscribe(ConsumablesController.EventType.CONS_DEACTIVATE, TurretDeactiveteTime);
		if (!(currentTurret == null))
		{
			if (Defs.bool_2)
			{
				PhotonNetwork.Destroy(currentTurret);
			}
			else
			{
				Object.Destroy(currentTurret);
			}
			currentTurret = null;
			if (bool_1)
			{
				ConsumablesController.ConsumablesController_0.ForceStopDurationConsumable(SlotType.SLOT_CONSUM_TURRET);
			}
		}
	}
}
