using UnityEngine;

public sealed class BlinkHealthButton : MonoBehaviour
{
	public enum RegimButton
	{
		Health = 0,
		Ammo = 1
	}

	public RegimButton typeButton;

	public static bool bool_0;

	private bool bool_1;

	public float timerBlink;

	public float maxTimerBlink = 0.5f;

	public Color blinkColor = new Color(1f, 0f, 0f);

	public Color unBlinkColor = new Color(1f, 1f, 1f);

	public bool isBlinkState;

	public UISprite[] blinkObjs;

	public bool isBlinkTemp;

	public UISprite shine;

	private Player_move_c player_move_c_0;

	private Color color_0;

	private void Start()
	{
		bool_0 = false;
		isBlinkState = false;
		color_0 = new Color(blinkColor.r, blinkColor.g, blinkColor.b, 0f);
	}

	private void Update()
	{
		if (player_move_c_0 == null)
		{
			if (Defs.bool_2)
			{
				player_move_c_0 = WeaponManager.weaponManager_0.myPlayer.GetComponent<SkinName>().Player_move_c_0;
			}
			else
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
				if (gameObject != null)
				{
					player_move_c_0 = gameObject.GetComponent<SkinName>().Player_move_c_0;
				}
			}
		}
		if (player_move_c_0 == null)
		{
			return;
		}
		if (typeButton == RegimButton.Health)
		{
			if (player_move_c_0.PlayerParametersController_0.Single_2 < 3f && player_move_c_0.PlayerMechController_0.Boolean_1)
			{
				bool_0 = true;
			}
			else
			{
				bool_0 = false;
			}
		}
		if (typeButton == RegimButton.Ammo)
		{
			Weapon weaponFromCurrentSlot = WeaponManager.weaponManager_0.GetWeaponFromCurrentSlot();
			if (weaponFromCurrentSlot.Int32_1 == 0 && weaponFromCurrentSlot.Int32_0 == 0 && (!weaponFromCurrentSlot.WeaponSounds_0.WeaponData_0.Boolean_2 || weaponFromCurrentSlot.WeaponSounds_0.WeaponData_0.Boolean_3) && player_move_c_0.PlayerMechController_0.Boolean_1)
			{
				bool_0 = true;
			}
			else
			{
				bool_0 = false;
			}
		}
		isBlinkTemp = bool_0;
		if (bool_1 != bool_0)
		{
			timerBlink = maxTimerBlink;
		}
		if (bool_0)
		{
			timerBlink -= Time.deltaTime;
			if (timerBlink < 0f)
			{
				timerBlink = maxTimerBlink;
				isBlinkState = !isBlinkState;
				if (shine != null)
				{
					shine.Color_0 = ((!isBlinkState) ? color_0 : blinkColor);
				}
			}
		}
		if (!bool_0 && isBlinkState)
		{
			isBlinkState = !isBlinkState;
			if (shine != null)
			{
				shine.Color_0 = ((!isBlinkState) ? color_0 : blinkColor);
			}
		}
		bool_1 = bool_0;
	}
}
