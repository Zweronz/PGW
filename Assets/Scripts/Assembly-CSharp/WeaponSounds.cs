using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.helpers;

public sealed class WeaponSounds : MonoBehaviour
{
	public enum TypeWeaponSound
	{
		Weapon = 0,
		Grenade = 1,
		Turret = 2,
		Mech = 3
	}

	private static readonly string string_0 = "Prefabs/Grenades/{0}";

	private static readonly string string_1 = "Prefabs/Turrets/{0}";

	private static readonly string string_2 = "Weapons/{0}";

	public TypeWeaponSound WeaponSoundType;

	public int rocketNum;

	public int scopeNum;

	public float tekKoof = 1f;

	public float upKoofFire = 0.5f;

	public float maxKoof = 4f;

	public float downKoofFirst = 0.2f;

	public float downKoof = 0.2f;

	private float float_0;

	private float float_1 = 1000f;

	public Transform[] gunFlashDouble;

	private InnerWeaponPars innerWeaponPars_0;

	private WeaponData weaponData_0;

	public Vector3 gunPosition = new Vector3(0.35f, -0.25f, 0.6f);

	public List<AnimationClip> weaponAnimations = new List<AnimationClip>();

	public List<Vector2> shiftPositionTrunks = new List<Vector2>();

	[CompilerGenerated]
	private WeaponEffectsController weaponEffectsController_0;

	public WeaponData WeaponData_0
	{
		get
		{
			return weaponData_0;
		}
	}

	public WeaponEffectsController WeaponEffectsController_0
	{
		[CompilerGenerated]
		get
		{
			return weaponEffectsController_0;
		}
		[CompilerGenerated]
		private set
		{
			weaponEffectsController_0 = value;
		}
	}

	public float Single_0
	{
		get
		{
			return tekKoof * weaponData_0.Single_5;
		}
	}

	public float Single_1
	{
		get
		{
			return tekKoof * weaponData_0.Single_5;
		}
	}

	public GameObject GameObject_0
	{
		get
		{
			return (!(innerWeaponPars_0 != null)) ? null : innerWeaponPars_0.animationObject;
		}
	}

	public Texture Texture_0
	{
		get
		{
			return (!(innerWeaponPars_0 != null)) ? null : innerWeaponPars_0.preview;
		}
	}

	public AudioClip AudioClip_0
	{
		get
		{
			return (!(innerWeaponPars_0 != null)) ? null : innerWeaponPars_0.shoot;
		}
	}

	public AudioClip AudioClip_1
	{
		get
		{
			return (!(innerWeaponPars_0 != null)) ? null : innerWeaponPars_0.reload;
		}
	}

	public AudioClip AudioClip_2
	{
		get
		{
			return (!(innerWeaponPars_0 != null)) ? null : innerWeaponPars_0.empty;
		}
	}

	public GameObject GameObject_1
	{
		get
		{
			return (!(innerWeaponPars_0 != null)) ? null : innerWeaponPars_0.bonusPrefab;
		}
	}

	public Texture2D Texture2D_0
	{
		get
		{
			return (!(innerWeaponPars_0 != null)) ? null : innerWeaponPars_0.aimTextureV;
		}
	}

	public Texture2D Texture2D_1
	{
		get
		{
			return (!(innerWeaponPars_0 != null)) ? null : innerWeaponPars_0.aimTextureH;
		}
	}

	public Transform Transform_0
	{
		get
		{
			return (!(innerWeaponPars_0 != null)) ? null : innerWeaponPars_0.LeftArmorHand;
		}
	}

	public Transform Transform_1
	{
		get
		{
			return (!(innerWeaponPars_0 != null)) ? null : innerWeaponPars_0.RightArmorHand;
		}
	}

	public Transform Transform_2
	{
		get
		{
			return (!(innerWeaponPars_0 != null)) ? null : innerWeaponPars_0.grenatePoint;
		}
	}

	public GameObject GameObject_2
	{
		get
		{
			return (!(innerWeaponPars_0 != null)) ? null : innerWeaponPars_0.particlePoint;
		}
	}

	public int Int32_0
	{
		get
		{
			return (int)((float)WeaponData_0.Int32_3 * UserController.UserController_0.GetAmmoModifierBySlotType(WeaponData_0.SlotType_0));
		}
	}

	public int Int32_1
	{
		get
		{
			return (int)((float)WeaponData_0.Int32_4 * UserController.UserController_0.GetAmmoModifierBySlotType(WeaponData_0.SlotType_0));
		}
	}

	public bool Boolean_0
	{
		get
		{
			return shiftPositionTrunks.Count == WeaponData_0.Int32_7;
		}
	}

	public void ResetWeaponData()
	{
		weaponData_0 = null;
	}

	private void Awake()
	{
		WeaponEffectsController_0 = GetComponent<WeaponEffectsController>();
		Prerender();
		InitMechData();
	}

	private void Prerender()
	{
		if (WeaponSoundType != TypeWeaponSound.Mech)
		{
			string path = "WeaponSystem/Inner/" + base.gameObject.name.Replace("(Clone)", string.Empty) + Defs.String_3;
			GameObject gameObject = Resources.Load(path) as GameObject;
			if (gameObject != null)
			{
				GameObject gameObject2 = Object.Instantiate(gameObject, new Vector3(0f, base.gameObject.transform.position.y, 0f), Quaternion.identity) as GameObject;
				innerWeaponPars_0 = gameObject2.GetComponent<InnerWeaponPars>();
				innerWeaponPars_0.gameObject.transform.parent = base.gameObject.transform;
			}
		}
	}

	public void Init(int int_0)
	{
		weaponData_0 = WeaponController.WeaponController_0.GetWeapon(int_0);
		if (weaponData_0 == null)
		{
			Log.AddLine("WeaponSounds::Init. Data for weapon == null. id = " + int_0, Log.LogLevel.ERROR);
		}
	}

	private void InitMechData()
	{
		if (WeaponSoundType == TypeWeaponSound.Mech && WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.myPlayerMoveC != null && WeaponManager.weaponManager_0.myPlayerMoveC.PlayerMechController_0 != null && WeaponManager.weaponManager_0.myPlayerMoveC.PlayerMechController_0.ConsumableData_0 != null)
		{
			Init(WeaponManager.weaponManager_0.myPlayerMoveC.PlayerMechController_0.ConsumableData_0.Int32_1);
		}
	}

	private void OnDestroy()
	{
		if (innerWeaponPars_0 != null)
		{
			innerWeaponPars_0.transform.parent = null;
			Object.Destroy(innerWeaponPars_0.gameObject);
		}
	}

	private void Start()
	{
		if (GameObject_0 != null && GameObject_0.GetComponent<Animation>()["Shoot"] != null)
		{
			float_0 = GameObject_0.GetComponent<Animation>()["Shoot"].length;
		}
	}

	private void Update()
	{
		Player_move_c player_move_c = ((!(base.transform.parent != null)) ? null : base.transform.parent.GetComponent<Player_move_c>());
		if (player_move_c != null && !player_move_c.Boolean_5 && player_move_c.Boolean_4)
		{
			GameObject_0.SetActive(!player_move_c.Boolean_14);
		}
		float num = Time.deltaTime / float_0;
		if (float_1 < float_0)
		{
			float_1 += Time.deltaTime;
			tekKoof = Mathf.Max(1f, tekKoof - downKoofFirst * num);
		}
		else
		{
			tekKoof = Mathf.Max(1f, tekKoof - downKoof * num);
		}
	}

	public void Fire()
	{
		float_1 = 0f;
		tekKoof += upKoofFire + downKoofFirst;
		tekKoof = Mathf.Min(tekKoof, maxKoof + downKoofFirst);
	}

	public float PlayReloadAnimation(out float float_2, bool bool_0 = true)
	{
		float_2 = 1f;
		if (WeaponData_0 == null)
		{
			return 1f;
		}
		float floatSummModifier = UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_RELOAD_WEAPON_TIME_MODIFIER);
		if (GameObject_0 == null)
		{
			return 1f;
		}
		AnimationState animationState = GameObject_0.GetComponent<Animation>()["Reload"];
		if (animationState == null)
		{
			return 1f;
		}
		float_2 = WeaponData_0.Single_1 * (1f - floatSummModifier);
		float_2 = ((float_2 != 0f) ? float_2 : 1f);
		float num = animationState.length / float_2;
		if (bool_0)
		{
			GameObject_0.GetComponent<Animation>()["Reload"].speed = num;
			GameObject_0.GetComponent<Animation>().Play("Reload");
		}
		return num;
	}
}
