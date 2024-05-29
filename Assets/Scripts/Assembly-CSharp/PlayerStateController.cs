using engine.events;

public sealed class PlayerStateController : BaseEvent
{
	private PlayerStates playerStates_0;

	public bool Boolean_0
	{
		get
		{
			return playerStates_0 == PlayerStates.Default;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return playerStates_0 == PlayerStates.Shoot;
		}
	}

	public bool Boolean_2
	{
		get
		{
			return playerStates_0 == PlayerStates.ChangeWeapon;
		}
	}

	public bool Boolean_3
	{
		get
		{
			return playerStates_0 == PlayerStates.Respawn;
		}
	}

	public void DispatchStartShoot()
	{
		if (playerStates_0 != PlayerStates.Shoot)
		{
			playerStates_0 = PlayerStates.Shoot;
			Dispatch(PlayerEvents.StartShoot);
		}
	}

	public void DispatchStopShoot()
	{
		if (playerStates_0 == PlayerStates.Shoot)
		{
			playerStates_0 = PlayerStates.Default;
			Dispatch(PlayerEvents.StopShoot);
		}
	}

	public void DispatchStartRespawn()
	{
		if (playerStates_0 != PlayerStates.Respawn)
		{
			playerStates_0 = PlayerStates.Respawn;
			Dispatch(PlayerEvents.StartRespawn);
		}
	}

	public void DispatchEndRespawn()
	{
		if (playerStates_0 == PlayerStates.Respawn)
		{
			playerStates_0 = PlayerStates.Default;
			Dispatch(PlayerEvents.EndRespawn);
		}
	}

	public void DispatchStartChangeWeapon()
	{
		if (playerStates_0 != PlayerStates.ChangeWeapon)
		{
			playerStates_0 = PlayerStates.ChangeWeapon;
			Dispatch(PlayerEvents.StartChangeWeapon);
		}
	}

	public void DispatchEndChangeWeapon()
	{
		if (playerStates_0 == PlayerStates.ChangeWeapon)
		{
			playerStates_0 = PlayerStates.Default;
			Dispatch(PlayerEvents.EndChangeWeapon);
		}
	}

	public void DispatchGrenadeFire()
	{
		Dispatch(PlayerEvents.GrenadeFire);
	}

	public void DispatchChangeCameraSN()
	{
		Dispatch(PlayerEvents.ChangeCameraSensitive);
	}

	public void DispatchNoAmmo()
	{
		Dispatch(PlayerEvents.NoAmmo);
	}
}
