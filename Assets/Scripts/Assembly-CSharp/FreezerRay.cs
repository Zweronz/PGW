using UnityEngine;

public class FreezerRay : MonoBehaviour
{
	public float lifetime = 0.1f;

	public float timeLeft;

	private Player_move_c player_move_c_0;

	public float Single_0
	{
		set
		{
			base.transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(1, new Vector3(0f, 0f, value));
		}
	}

	private void Start()
	{
		timeLeft += lifetime;
	}

	private void Update()
	{
		timeLeft -= Time.deltaTime;
		if (timeLeft <= 0f)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		WeaponSounds weaponSounds_ = player_move_c_0.WeaponSounds_0;
		Transform transform_ = weaponSounds_.WeaponEffectsController_0.Transform_0;
		if (!(transform_ == null))
		{
			base.transform.position = transform_.position;
			base.transform.forward = weaponSounds_.transform.forward;
		}
	}

	public void SetParentMoveC(Player_move_c player_move_c_1)
	{
		player_move_c_0 = player_move_c_1;
		if (player_move_c_0 != null)
		{
			player_move_c_0.FreezerFired += HandleFreezerFired;
		}
	}

	private void HandleFreezerFired(float float_0)
	{
		timeLeft += lifetime;
		Single_0 = float_0;
	}

	private void OnDestroy()
	{
		if (player_move_c_0 != null)
		{
			player_move_c_0.FreezerFired -= HandleFreezerFired;
		}
	}
}
