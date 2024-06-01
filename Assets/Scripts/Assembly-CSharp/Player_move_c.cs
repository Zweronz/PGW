using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using CodeStage.AntiCheat.ObscuredTypes;
using Holoville.HOTween;
using Holoville.HOTween.Core;
using UnityEngine;
using engine.helpers;
using engine.unity;
using pixelgun.tutorial;

public sealed class Player_move_c : MonoBehaviour
{
	public enum TypeKills
	{
		none = 0,
		himself = 1,
		headshot = 2,
		explosion = 3,
		zoomingshot = 4,
		flag = 5,
		grenade = 6,
		grenade_hell = 7,
		turret = 8,
		killTurret = 9,
		mech = 10
	}

	public static int int_0;

	public GameObject particleBonusesPoint;

	public Transform myTransform;

	public Transform myPlayerTransform;

	public SkinName mySkinName;

	public GameObject fpsPlayerBody;

	public SkinnedMeshRenderer playerBodyRenderer;

	public CapsuleCollider bodyCollayder;

	public CapsuleCollider headCollayder;

	public ThirdPersonNetwork1 myPersonNetwork;

	public GameObject invisibleParticle;

	public Camera myCamera;

	public Camera gunCamera;

	public GameObject flagPoint;

	public visibleObjPhoton visibleObj;

	public GameObject jetPackPoint;

	public GameObject jetPackPointMech;

	public ParticleSystem[] jetPackParticle;

	public GameObject jetPackSound;

	public Texture hitTexture;

	public AudioClip ChangeWeaponClip;

	public AudioClip headShotSound;

	public AudioClip shootMechClip;

	public AudioClip deadPlayerSound;

	public AudioClip damagePlayerSound;

	public AudioClip flagGetClip;

	public AudioClip flagLostClip;

	public AudioClip flagScoreEnemyClip;

	public AudioClip flagScoreMyCommandClip;

	private KillerInfo killerInfo_0 = new KillerInfo();

	private Player_move_c player_move_c_0;

	private PlayerStateController playerStateController_0 = new PlayerStateController();

	private GameObject gameObject_0;

	private Shader[] shader_0;

	private Color[] color_0;

	private GameObject gameObject_1;

	private GameObject[] gameObject_2;

	private List<int> list_0 = new List<int>();

	private GameObject gameObject_3;

	private GameObject gameObject_4;

	private Pauser pauser_0;

	private WeaponManager weaponManager_0;

	private DamageRPCData damageRPCData_0 = new DamageRPCData();

	private ObscuredInt obscuredInt_0 = default(ObscuredInt);

	private Vector2 vector2_0 = Vector2.zero;

	private Vector2 vector2_1;

	private bool bool_0;

	private bool bool_1;

	private string[] string_0 = StoreKitEventListener.string_28;

	private float float_0;

	private bool bool_2;

	private float float_1;

	private float float_2 = 3f;

	private float float_3 = 3f;

	private bool bool_3;

	private bool bool_4;

	private bool bool_5 = true;

	private bool bool_6;

	private bool bool_7;

	private bool bool_8;

	private float float_4;

	private float float_5;

	private string[] string_1 = new string[11]
	{
		string.Empty,
		"Chat_Death",
		"Chat_HeadShot",
		"Chat_Explode",
		"Chat_Sniper",
		"Chat_Flag",
		"Chat_grenade",
		"Chat_grenade_hell",
		"Chat_Turret",
		"Chat_Turret_Explode",
		string.Empty
	};

	private Action<float> action_0;

	private EventHandler<EventArgs> eventHandler_0;

	[CompilerGenerated]
	private static bool bool_9;

	[CompilerGenerated]
	private PlayerParametersController playerParametersController_0;

	[CompilerGenerated]
	private PlayerGrenadeController playerGrenadeController_0;

	[CompilerGenerated]
	private PlayerTurretController playerTurretController_0;

	[CompilerGenerated]
	private PlayerMechController playerMechController_0;

	[CompilerGenerated]
	private PlayerScoreController playerScoreController_0;

	[CompilerGenerated]
	private PlayerFlashController playerFlashController_0;

	[CompilerGenerated]
	private PlayerPhotonSkillUpdater playerPhotonSkillUpdater_0;

	[CompilerGenerated]
	private PlayerDotEffectController playerDotEffectController_0;

	[CompilerGenerated]
	private PlayerParticleController playerParticleController_0;

	[CompilerGenerated]
	private PhotonView photonView_0;

	[CompilerGenerated]
	private GameObject gameObject_5;

	[CompilerGenerated]
	private NickLabelController nickLabelController_0;

	[CompilerGenerated]
	private Texture texture_0;

	[CompilerGenerated]
	private GameObject gameObject_6;

	[CompilerGenerated]
	private NetworkStartTable networkStartTable_0;

	[CompilerGenerated]
	private FlagController flagController_0;

	[CompilerGenerated]
	private FlagController flagController_1;

	[CompilerGenerated]
	private FlagController flagController_2;

	[CompilerGenerated]
	private FlagController flagController_3;

	[CompilerGenerated]
	private SlotType slotType_0;

	[CompilerGenerated]
	private GameObject gameObject_7;

	[CompilerGenerated]
	private WeaponSounds weaponSounds_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private bool bool_10;

	[CompilerGenerated]
	private bool bool_11;

	[CompilerGenerated]
	private bool bool_12;

	[CompilerGenerated]
	private bool bool_13;

	[CompilerGenerated]
	private bool bool_14;

	[CompilerGenerated]
	private bool bool_15;

	[CompilerGenerated]
	private bool bool_16;

	[CompilerGenerated]
	private bool bool_17;

	[CompilerGenerated]
	private bool bool_18;

	[CompilerGenerated]
	private bool bool_19;

	[CompilerGenerated]
	private bool bool_20;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private float float_6;

	[CompilerGenerated]
	private bool bool_21;

	[CompilerGenerated]
	private bool bool_22;

	[CompilerGenerated]
	private bool bool_23;

	[CompilerGenerated]
	private float float_7;

	[CompilerGenerated]
	private bool bool_24;

	[CompilerGenerated]
	private bool bool_25;

	[CompilerGenerated]
	private bool bool_26;

	[CompilerGenerated]
	private bool bool_27;

	[CompilerGenerated]
	private string[][] string_2;

	[CompilerGenerated]
	private bool bool_28;

	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private float float_8;

	[CompilerGenerated]
	private float float_9;

	[CompilerGenerated]
	private float float_10;

	[CompilerGenerated]
	private float float_11;

	[CompilerGenerated]
	private bool bool_29;

	[CompilerGenerated]
	private int int_4;

	[CompilerGenerated]
	private bool bool_30;

	[CompilerGenerated]
	private bool bool_31;

	[CompilerGenerated]
	private float[] float_12;

	[CompilerGenerated]
	private bool bool_32;

	[CompilerGenerated]
	private bool bool_33;

	[CompilerGenerated]
	private SlotType slotType_1;

	public static bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_9;
		}
		[CompilerGenerated]
		private set
		{
			bool_9 = value;
		}
	}

	public PlayerParametersController PlayerParametersController_0
	{
		[CompilerGenerated]
		get
		{
			return playerParametersController_0;
		}
		[CompilerGenerated]
		private set
		{
			playerParametersController_0 = value;
		}
	}

	public PlayerGrenadeController PlayerGrenadeController_0
	{
		[CompilerGenerated]
		get
		{
			return playerGrenadeController_0;
		}
		[CompilerGenerated]
		private set
		{
			playerGrenadeController_0 = value;
		}
	}

	public PlayerTurretController PlayerTurretController_0
	{
		[CompilerGenerated]
		get
		{
			return playerTurretController_0;
		}
		[CompilerGenerated]
		private set
		{
			playerTurretController_0 = value;
		}
	}

	public PlayerMechController PlayerMechController_0
	{
		[CompilerGenerated]
		get
		{
			return playerMechController_0;
		}
		[CompilerGenerated]
		private set
		{
			playerMechController_0 = value;
		}
	}

	public PlayerScoreController PlayerScoreController_0
	{
		[CompilerGenerated]
		get
		{
			return playerScoreController_0;
		}
		[CompilerGenerated]
		private set
		{
			playerScoreController_0 = value;
		}
	}

	public PlayerFlashController PlayerFlashController_0
	{
		[CompilerGenerated]
		get
		{
			return playerFlashController_0;
		}
		[CompilerGenerated]
		private set
		{
			playerFlashController_0 = value;
		}
	}

	public PlayerPhotonSkillUpdater PlayerPhotonSkillUpdater_0
	{
		[CompilerGenerated]
		get
		{
			return playerPhotonSkillUpdater_0;
		}
		[CompilerGenerated]
		private set
		{
			playerPhotonSkillUpdater_0 = value;
		}
	}

	public PlayerDotEffectController PlayerDotEffectController_0
	{
		[CompilerGenerated]
		get
		{
			return playerDotEffectController_0;
		}
		[CompilerGenerated]
		private set
		{
			playerDotEffectController_0 = value;
		}
	}

	public PlayerParticleController PlayerParticleController_0
	{
		[CompilerGenerated]
		get
		{
			return playerParticleController_0;
		}
		[CompilerGenerated]
		private set
		{
			playerParticleController_0 = value;
		}
	}

	public FirstPersonPlayerController FirstPersonPlayerController_0
	{
		get
		{
			return mySkinName.firstPersonControlSharp;
		}
	}

	public PhotonView PhotonView_0
	{
		[CompilerGenerated]
		get
		{
			return photonView_0;
		}
		[CompilerGenerated]
		set
		{
			photonView_0 = value;
		}
	}

	public GameObject GameObject_0
	{
		[CompilerGenerated]
		get
		{
			return gameObject_5;
		}
		[CompilerGenerated]
		set
		{
			gameObject_5 = value;
		}
	}

	public NickLabelController NickLabelController_0
	{
		[CompilerGenerated]
		get
		{
			return nickLabelController_0;
		}
		[CompilerGenerated]
		set
		{
			nickLabelController_0 = value;
		}
	}

	public Texture Texture_0
	{
		[CompilerGenerated]
		get
		{
			return texture_0;
		}
		[CompilerGenerated]
		set
		{
			texture_0 = value;
		}
	}

	public GameObject GameObject_1
	{
		[CompilerGenerated]
		get
		{
			return gameObject_6;
		}
		[CompilerGenerated]
		set
		{
			gameObject_6 = value;
		}
	}

	public NetworkStartTable NetworkStartTable_0
	{
		[CompilerGenerated]
		get
		{
			return networkStartTable_0;
		}
		[CompilerGenerated]
		set
		{
			networkStartTable_0 = value;
		}
	}

	public FlagController FlagController_0
	{
		[CompilerGenerated]
		get
		{
			return flagController_0;
		}
		[CompilerGenerated]
		set
		{
			flagController_0 = value;
		}
	}

	public FlagController FlagController_1
	{
		[CompilerGenerated]
		get
		{
			return flagController_1;
		}
		[CompilerGenerated]
		set
		{
			flagController_1 = value;
		}
	}

	public FlagController FlagController_2
	{
		[CompilerGenerated]
		get
		{
			return flagController_2;
		}
		[CompilerGenerated]
		set
		{
			flagController_2 = value;
		}
	}

	public FlagController FlagController_3
	{
		[CompilerGenerated]
		get
		{
			return flagController_3;
		}
		[CompilerGenerated]
		set
		{
			flagController_3 = value;
		}
	}

	public SlotType SlotType_0
	{
		[CompilerGenerated]
		get
		{
			return slotType_0;
		}
		[CompilerGenerated]
		set
		{
			slotType_0 = value;
		}
	}

	public GameObject GameObject_2
	{
		[CompilerGenerated]
		get
		{
			return gameObject_7;
		}
		[CompilerGenerated]
		set
		{
			gameObject_7 = value;
		}
	}

	public WeaponSounds WeaponSounds_0
	{
		[CompilerGenerated]
		get
		{
			return weaponSounds_0;
		}
		[CompilerGenerated]
		set
		{
			weaponSounds_0 = value;
		}
	}

	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		set
		{
			int_1 = value;
		}
	}

	public bool Boolean_1
	{
		[CompilerGenerated]
		get
		{
			return bool_10;
		}
		[CompilerGenerated]
		set
		{
			bool_10 = value;
		}
	}

	public bool Boolean_2
	{
		[CompilerGenerated]
		get
		{
			return bool_11;
		}
		[CompilerGenerated]
		set
		{
			bool_11 = value;
		}
	}

	public bool Boolean_3
	{
		[CompilerGenerated]
		get
		{
			return bool_12;
		}
		[CompilerGenerated]
		set
		{
			bool_12 = value;
		}
	}

	public bool Boolean_4
	{
		[CompilerGenerated]
		get
		{
			return bool_13;
		}
		[CompilerGenerated]
		set
		{
			bool_13 = value;
		}
	}

	public bool Boolean_5
	{
		[CompilerGenerated]
		get
		{
			return bool_14;
		}
		[CompilerGenerated]
		set
		{
			bool_14 = value;
		}
	}

	public bool Boolean_6
	{
		[CompilerGenerated]
		get
		{
			return bool_15;
		}
		[CompilerGenerated]
		set
		{
			bool_15 = value;
		}
	}

	public bool Boolean_7
	{
		[CompilerGenerated]
		get
		{
			return bool_16;
		}
		[CompilerGenerated]
		set
		{
			bool_16 = value;
		}
	}

	public bool Boolean_8
	{
		[CompilerGenerated]
		get
		{
			return bool_17;
		}
		[CompilerGenerated]
		set
		{
			bool_17 = value;
		}
	}

	public bool Boolean_9
	{
		[CompilerGenerated]
		get
		{
			return bool_18;
		}
		[CompilerGenerated]
		set
		{
			bool_18 = value;
		}
	}

	public bool Boolean_10
	{
		[CompilerGenerated]
		get
		{
			return bool_19;
		}
		[CompilerGenerated]
		set
		{
			bool_19 = value;
		}
	}

	public bool Boolean_11
	{
		[CompilerGenerated]
		get
		{
			return bool_20;
		}
		[CompilerGenerated]
		set
		{
			bool_20 = value;
		}
	}

	public int Int32_1
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		set
		{
			int_2 = value;
		}
	}

	public float Single_0
	{
		[CompilerGenerated]
		get
		{
			return float_6;
		}
		[CompilerGenerated]
		set
		{
			float_6 = value;
		}
	}

	public bool Boolean_12
	{
		[CompilerGenerated]
		get
		{
			return bool_21;
		}
		[CompilerGenerated]
		set
		{
			bool_21 = value;
		}
	}

	public bool Boolean_13
	{
		[CompilerGenerated]
		get
		{
			return bool_22;
		}
		[CompilerGenerated]
		set
		{
			bool_22 = value;
		}
	}

	public bool Boolean_14
	{
		[CompilerGenerated]
		get
		{
			return bool_23;
		}
		[CompilerGenerated]
		set
		{
			bool_23 = value;
		}
	}

	public float Single_1
	{
		[CompilerGenerated]
		get
		{
			return float_7;
		}
		[CompilerGenerated]
		set
		{
			float_7 = value;
		}
	}

	public bool Boolean_15
	{
		[CompilerGenerated]
		get
		{
			return bool_24;
		}
		[CompilerGenerated]
		set
		{
			bool_24 = value;
		}
	}

	public bool Boolean_16
	{
		[CompilerGenerated]
		get
		{
			return bool_25;
		}
		[CompilerGenerated]
		set
		{
			bool_25 = value;
		}
	}

	public bool Boolean_17
	{
		[CompilerGenerated]
		get
		{
			return bool_26;
		}
		[CompilerGenerated]
		set
		{
			bool_26 = value;
		}
	}

	public bool Boolean_18
	{
		[CompilerGenerated]
		get
		{
			return bool_27;
		}
		[CompilerGenerated]
		set
		{
			bool_27 = value;
		}
	}

	public string[][] String_0
	{
		[CompilerGenerated]
		get
		{
			return string_2;
		}
		[CompilerGenerated]
		set
		{
			string_2 = value;
		}
	}

	public bool Boolean_19
	{
		[CompilerGenerated]
		get
		{
			return bool_28;
		}
		[CompilerGenerated]
		set
		{
			bool_28 = value;
		}
	}

	public int Int32_2
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		set
		{
			int_3 = value;
		}
	}

	public float Single_2
	{
		[CompilerGenerated]
		get
		{
			return float_8;
		}
		[CompilerGenerated]
		set
		{
			float_8 = value;
		}
	}

	public float Single_3
	{
		[CompilerGenerated]
		get
		{
			return float_9;
		}
		[CompilerGenerated]
		set
		{
			float_9 = value;
		}
	}

	public float Single_4
	{
		[CompilerGenerated]
		get
		{
			return float_10;
		}
		[CompilerGenerated]
		set
		{
			float_10 = value;
		}
	}

	public float Single_5
	{
		[CompilerGenerated]
		get
		{
			return float_11;
		}
		[CompilerGenerated]
		set
		{
			float_11 = value;
		}
	}

	public bool Boolean_20
	{
		[CompilerGenerated]
		get
		{
			return bool_29;
		}
		[CompilerGenerated]
		set
		{
			bool_29 = value;
		}
	}

	public int Int32_3
	{
		[CompilerGenerated]
		get
		{
			return int_4;
		}
		[CompilerGenerated]
		set
		{
			int_4 = value;
		}
	}

	public bool Boolean_21
	{
		[CompilerGenerated]
		get
		{
			return bool_30;
		}
		[CompilerGenerated]
		set
		{
			bool_30 = value;
		}
	}

	public bool Boolean_22
	{
		[CompilerGenerated]
		get
		{
			return bool_31;
		}
		[CompilerGenerated]
		set
		{
			bool_31 = value;
		}
	}

	public float[] Single_6
	{
		[CompilerGenerated]
		get
		{
			return float_12;
		}
		[CompilerGenerated]
		set
		{
			float_12 = value;
		}
	}

	public bool Boolean_23
	{
		[CompilerGenerated]
		get
		{
			return bool_32;
		}
		[CompilerGenerated]
		set
		{
			bool_32 = value;
		}
	}

	public bool Boolean_24
	{
		[CompilerGenerated]
		get
		{
			return bool_33;
		}
		[CompilerGenerated]
		private set
		{
			bool_33 = value;
		}
	}

	public KillerInfo KillerInfo_0
	{
		get
		{
			return killerInfo_0;
		}
	}

	public PlayerStateController PlayerStateController_0
	{
		get
		{
			return playerStateController_0;
		}
	}

	public int Int32_4
	{
		get
		{
			return Defs.Int32_1;
		}
	}

	public bool Boolean_25
	{
		get
		{
			return bool_6;
		}
	}

	public WeaponManager WeaponManager_0
	{
		get
		{
			return weaponManager_0;
		}
		set
		{
			weaponManager_0 = value;
		}
	}

	private SlotType SlotType_1
	{
		[CompilerGenerated]
		get
		{
			return slotType_1;
		}
		[CompilerGenerated]
		set
		{
			slotType_1 = value;
		}
	}

	private bool Boolean_26
	{
		get
		{
			return Defs.bool_2 && !Defs.bool_4;
		}
	}

	private int Int32_5
	{
		get
		{
			return obscuredInt_0;
		}
		set
		{
			obscuredInt_0 = value;
		}
	}

	public event Action<float> FreezerFired
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			action_0 = (Action<float>)Delegate.Combine(action_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			action_0 = (Action<float>)Delegate.Remove(action_0, value);
		}
	}

	public event EventHandler<EventArgs> WeaponChanged
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			eventHandler_0 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			eventHandler_0 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler_0, value);
		}
	}

	public static void SetBlockKeyboardControl(bool bool_34, bool bool_35)
	{
		Boolean_0 = bool_34;
		if (bool_35 && Screen.lockCursor != !bool_34)
		{
			Screen.lockCursor = !bool_34;
		}
	}

	private void Awake()
	{
		Boolean_9 = Defs.bool_0;
		Boolean_4 = true;//Defs.bool_2;
		Boolean_6 = Defs.bool_5;
		Boolean_8 = Defs.bool_4;
		Boolean_7 = Defs.bool_6;
		PlayerParametersController_0 = GetComponent<PlayerParametersController>();
		PlayerGrenadeController_0 = GetComponent<PlayerGrenadeController>();
		PlayerTurretController_0 = GetComponent<PlayerTurretController>();
		PlayerMechController_0 = GetComponent<PlayerMechController>();
		PlayerFlashController_0 = GetComponent<PlayerFlashController>();
		PlayerPhotonSkillUpdater_0 = GetComponent<PlayerPhotonSkillUpdater>();
		PlayerPhotonSkillUpdater_0.Player_move_c_0 = this;
		PlayerDotEffectController_0 = GetComponent<PlayerDotEffectController>();
		PlayerParticleController_0 = GetComponent<PlayerParticleController>();
		Int32_0 = 1;
		Single_1 = 0.5f;
		Boolean_17 = true;
		String_0 = new string[3][]
		{
			new string[6]
			{
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty
			},
			new string[6]
			{
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty
			},
			new string[6]
			{
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty
			}
		};
		Boolean_22 = true;
		Single_6 = new float[3] { -1f, -1f, -1f };
		Boolean_23 = true;
	}

	private IEnumerator Start()
	{
		PhotonView_0 = PhotonView.Get(this);
		if (PhotonView_0 == null)
		{
			Debug.Log("Player_move_c.Start():    photonView == null");
		}
		else
		{
			Boolean_5 = PhotonView_0.Boolean_1;
		}
		if (!Boolean_5)
		{
			int num = 0;
		}
		killerInfo_0.Reset();
		if (!Defs.bool_2)
		{
			WeaponManager.weaponManager_0.myPlayerMoveC = this;
			WeaponManager.weaponManager_0.myPlayer = myPlayerTransform.gameObject;
		}
		if (Boolean_7)
		{
			GameObject gameObject = null;
			GameObject gameObject2 = null;
			while (true)
			{
				gameObject = GameObject.FindGameObjectWithTag("Flag1");
				gameObject2 = GameObject.FindGameObjectWithTag("Flag2");
				if (!(gameObject == null) && !(gameObject2 == null))
				{
					break;
				}
				yield return null;
			}
			FlagController_0 = gameObject.GetComponent<FlagController>();
			FlagController_1 = gameObject2.GetComponent<FlagController>();
		}
		if (!Defs.bool_2 || Boolean_5)
		{
			SetBlockKeyboardControl(false, true);
		}
		if (!Boolean_4 || Boolean_5)
		{
			HeadUpDisplay.Show();
			PlayerGrenadeController_0.SetStateGrenadeNone();
			Defs.bool_8 = false;
			PlayerTurretController_0.SetStateTurretNone();
		}
		if (!Boolean_4)
		{
			Texture_0 = Resources.Load<Texture2D>("skinstextures/beginnerSkin");//SkinsController.Texture2D_0;
			Texture_0.filterMode = FilterMode.Point;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].GetComponent<PhotonView>().PhotonPlayer_0 == base.transform.GetComponent<PhotonView>().PhotonPlayer_0)
			{
				GameObject_1 = array[i];
				NetworkStartTable_0 = GameObject_1.GetComponent<NetworkStartTable>();
				if (NetworkStartTable_0 != null)
				{
					NetworkStartTable_0.Player_move_c_0 = this;
				}
				Int32_2 = NetworkStartTable_0.PlayerCommandController_0.Int32_1;
				if (Boolean_5)
				{
					int_0 = Int32_2;
				}
				break;
			}
		}
		GameObject[] array2 = GameObject.FindGameObjectsWithTag("NetworkScoreController");
		for (int j = 0; j < array2.Length; j++)
		{
			if (array2[j].GetComponent<PhotonView>().PhotonPlayer_0 == base.transform.GetComponent<PhotonView>().PhotonPlayer_0)
			{
				PlayerScoreController_0 = array2[j].GetComponent<PlayerScoreController>();
				if (PlayerScoreController_0 != null)
				{
					PlayerScoreController_0.Player_move_c_0 = this;
				}
				break;
			}
		}
		if (Boolean_4)
		{
			Int32_1 = myPlayerTransform.GetComponent<PhotonView>().Int32_1;
		}
		if (Boolean_4 && !Boolean_5)
		{
			base.transform.localPosition = new Vector3(0f, 0.4f, 0f);
		}
		if (!Boolean_4)
		{
			CurrentCampaignGame.ResetConditionParameters();
			CurrentCampaignGame.float_0 = Time.time;
			ZombieCreator.BossKilled += CheckTimeCondition;
		}
		if (!Boolean_4)
		{
			string_0 = StoreKitEventListener.string_28;
		}
		else
		{
			string_0 = StoreKitEventListener.string_29;
		}
		if (Boolean_4 && Boolean_5)
		{
			if (myCamera != null)
			{
				myCamera.fieldOfView = PlayerParamsSettings.Get.PlayerFOV;
			}
			if (gunCamera != null)
			{
				gunCamera.fieldOfView = PlayerParamsSettings.Get.GunFOV;
			}
		}
		if (!Boolean_4)
		{
			fpsPlayerBody.SetActive(false);
		}
		HOTween.Init(true, true, true);
		HOTween.EnableOverwriteManager();
		if (Boolean_4)
		{
			if (Boolean_5)
			{
				Boolean_17 = true;
			}
			else
			{
				Boolean_17 = false;
			}
		}
		if (Boolean_4 && !Boolean_5)
		{
			GameObject_0 = null;
		}
		else
		{
			GameObject_0 = myPlayerTransform.gameObject;
		}
		WeaponManager_0 = WeaponManager.weaponManager_0;
		WeaponManager_0.nextWeaponSlot = SlotType.SLOT_NONE;
		if (Defs.bool_2 && PhotonView_0.Boolean_1)
		{
			WeaponManager_0.ResetAmmoInAllWeapon();
		}
		if (!Boolean_4 || Boolean_5)
		{
			GameObject original = Resources.Load("Damage") as GameObject;
			gameObject_3 = (GameObject)UnityEngine.Object.Instantiate(original);
			Color color = gameObject_3.GetComponent<GUITexture>().color;
			color.a = 0f;
			gameObject_3.GetComponent<GUITexture>().color = color;
		}
		if (!Boolean_4 || Boolean_5)
		{
			pauser_0 = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pauser>();
			if (pauser_0 == null)
			{
				Debug.LogWarning("Start(): _pauser is null.");
			}
		}
		if (_singleOrMultiMine())
		{
			SlotType firstUserWeaponFromSlots = WeaponController.WeaponController_0.GetFirstUserWeaponFromSlots();
			if (firstUserWeaponFromSlots != 0)
			{
				ChangeWeaponReal(firstUserWeaponFromSlots, false);
				WeaponManager_0.WeaponSounds_0.GameObject_0.GetComponent<Animation>()["Reload"].layer = 1;
				WeaponManager_0.WeaponSounds_0.GameObject_0.GetComponent<Animation>().Stop();
			}
		}
		if (Boolean_4 && Boolean_5)
		{
			string text = FilterBadWorld.FilterString(Defs.GetPlayerNameOrDefault());
			Boolean_24 = UsersData.UsersData_0.UserData_0.user_0.bool_0;
			bool flag = !UsersData.UsersData_0.UserData_0.user_0.bool_3;
			PhotonView_0.RPC("SetNickName", PhotonTargets.AllBuffered, text);
			PhotonView_0.RPC("SetAdminFlag", PhotonTargets.AllBuffered, Boolean_24, flag);
		}
		if (_singleOrMultiMine())
		{
			SetLayerRecursively(PlayerMechController_0.mechGunAnimation.gameObject, LayerMask.NameToLayer("Gun"));
			PlayerPhotonSkillUpdater_0.ForceUpdateSkills();
		}
		yield return null;
		if (Boolean_4 && GameObject_1 != null)
		{
			Texture_0 = GameObject_1.GetComponent<NetworkStartTable>().Texture_1;
			PlayerFlashController_0.SetMySkin();
		}
		if (Boolean_4 && Boolean_5 && NetworkStartTable_0 != null)
		{
			NetworkStartTable_0.SendMyUserInfo();
		}
		Subscribe();
		bool_6 = true;
	}

	public void IndicateDamage()
	{
		bool_1 = true;
		Invoke("setisDeadFrameFalse", 1f);
	}

	public void hit(float float_13, Vector3 vector3_0)
	{
		if (!PlayerMechController_0.Boolean_1)
		{
			PlayerMechController_0.HitMech(float_13);
		}
		else
		{
			PlayerParametersController_0.HitPlayer(float_13, 0f);
		}
		ShowDamageDirection(vector3_0);
		if (!bool_3)
		{
			StartCoroutine(FlashWhenHit());
		}
	}

	public void SetJetpackEnabled(bool bool_34)
	{
		Defs.bool_8 = bool_34;
		if (Defs.bool_2)
		{
			PhotonView_0.RPC("SetJetpackEnabledRPC", PhotonTargets.Others, bool_34);
		}
	}

	[RPC]
	public void SetJetpackEnabledRPC(bool bool_34)
	{
		jetPackPoint.SetActive(bool_34);
		jetPackPointMech.SetActive(bool_34);
		for (int i = 0; i < jetPackParticle.Length; i++)
		{
			jetPackParticle[i].enableEmission = false;
		}
	}

	public void SetJetpackParticleEnabled(bool bool_34)
	{
		if (bool_34)
		{
			if (ButtonClickSound.buttonClickSound_0 != null && Defs.Boolean_0)
			{
				jetPackSound.SetActive(true);
			}
		}
		else
		{
			jetPackSound.SetActive(false);
		}
		if (Defs.bool_2)
		{
			PhotonView_0.RPC("SetJetpackParticleEnabledRPC", PhotonTargets.Others, bool_34);
		}
	}

	[RPC]
	public void SetJetpackParticleEnabledRPC(bool bool_34)
	{
		if (bool_34)
		{
			if (ButtonClickSound.buttonClickSound_0 != null && Defs.Boolean_0)
			{
				jetPackSound.SetActive(true);
			}
		}
		else
		{
			jetPackSound.SetActive(false);
		}
		for (int i = 0; i < jetPackParticle.Length; i++)
		{
			jetPackParticle[i].enableEmission = bool_34;
		}
	}

	public void WalkAnimation()
	{
		if (_singleOrMultiMine())
		{
			if (!PlayerMechController_0.Boolean_1)
			{
				PlayerMechController_0.CrossFadeWeaponAnimation(MechAnimationType.string_1);
			}
			if ((bool)WeaponManager_0 && (bool)WeaponManager_0.WeaponSounds_0 && WeaponManager_0.WeaponSounds_0.GameObject_0 != null)
			{
				WeaponManager_0.WeaponSounds_0.GameObject_0.GetComponent<Animation>().CrossFade("Walk");
			}
		}
	}

	public void IdleAnimation()
	{
		if (_singleOrMultiMine())
		{
			if (!PlayerMechController_0.Boolean_1)
			{
				PlayerMechController_0.CrossFadeWeaponAnimation(MechAnimationType.string_0);
			}
			if ((bool)weaponManager_0 && (bool)weaponManager_0.WeaponSounds_0 && weaponManager_0.WeaponSounds_0.GameObject_0 != null)
			{
				weaponManager_0.WeaponSounds_0.GameObject_0.GetComponent<Animation>().CrossFade("Idle");
			}
		}
	}

	public void ZoomPress()
	{
		if (!PlayerMechController_0.Boolean_1)
		{
			return;
		}
		Boolean_16 = !Boolean_16;
		if (Boolean_16)
		{
			WeaponManager weaponManager = WeaponManager.weaponManager_0;
			if (weaponManager != null && weaponManager.WeaponSounds_0 != null && weaponManager.WeaponSounds_0.GameObject_0.GetComponent<Animation>().IsPlaying("Reload"))
			{
				Boolean_16 = false;
				return;
			}
			myCamera.fieldOfView = WeaponManager_0.WeaponSounds_0.WeaponData_0.Single_8;
			gunCamera.gameObject.SetActive(false);
			if (HeadUpDisplay.HeadUpDisplay_0 != null)
			{
				HeadUpDisplay.HeadUpDisplay_0.SetScopeForWeapon(WeaponManager_0.WeaponSounds_0.scopeNum.ToString());
			}
			myTransform.localPosition = new Vector3(myTransform.localPosition.x, myTransform.localPosition.y, myTransform.localPosition.z);
		}
		else
		{
			myCamera.fieldOfView = PlayerParamsSettings.Get.PlayerFOV;
			gunCamera.fieldOfView = PlayerParamsSettings.Get.GunFOV;
			gunCamera.gameObject.SetActive(true);
			if (HeadUpDisplay.HeadUpDisplay_0 != null)
			{
				HeadUpDisplay.HeadUpDisplay_0.ResetScope();
			}
		}
		if (Boolean_4)
		{
			PhotonView_0.RPC("SynhIsZoming", PhotonTargets.AllBuffered, Boolean_16);
		}
	}

	[RPC]
	private void SynhIsZoming(bool bool_34)
	{
		Boolean_16 = bool_34;
	}

	public void hideGUI()
	{
		Boolean_17 = false;
	}

	public void setMyTable(GameObject gameObject_8)
	{
		GameObject_1 = gameObject_8;
		Int32_2 = GameObject_1.GetComponent<NetworkStartTable>().PlayerCommandController_0.Int32_1;
		Texture_0 = GameObject_1.GetComponent<NetworkStartTable>().Texture_1;
		PlayerFlashController_0.SetMySkin();
	}

	public void AddWeapon(int int_5)
	{
		if (WeaponManager_0.AddWeapon(int_5))
		{
			SlotType activeWeaponSlotType = WeaponController.WeaponController_0.GetActiveWeaponSlotType();
			if (activeWeaponSlotType == SlotType.SLOT_NONE)
			{
				ChangeWeapon(activeWeaponSlotType, false);
			}
		}
	}

	public void minusLiveFromZombi(int int_5, Vector3 vector3_0)
	{
		PhotonView_0.RPC("minusLiveFromZombiRPC", PhotonTargets.All, int_5, vector3_0);
	}

	[RPC]
	public void minusLiveFromZombiRPC(int int_5, Vector3 vector3_0)
	{
		if (PhotonView_0.Boolean_1 && !Boolean_20 && !Boolean_22)
		{
			if (!PlayerMechController_0.Boolean_1)
			{
				PlayerMechController_0.HitMech(int_5);
			}
			else
			{
				PlayerParametersController_0.HitPlayer(int_5, 0f);
			}
			ShowDamageDirection(vector3_0);
		}
		PlayerFlashController_0.FlashPlayer();
	}

	public static void SetLayerRecursively(GameObject gameObject_8, int int_5)
	{
		if (null == gameObject_8)
		{
			return;
		}
		gameObject_8.layer = int_5;
		int childCount = gameObject_8.transform.childCount;
		Transform transform = gameObject_8.transform;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (!(null == child))
			{
				SetLayerRecursively(child.gameObject, int_5);
			}
		}
	}

	public static void PerformActionRecurs(GameObject gameObject_8, Action<Transform> action_1)
	{
		if (action_1 == null || null == gameObject_8)
		{
			return;
		}
		action_1(gameObject_8.transform);
		int childCount = gameObject_8.transform.childCount;
		Transform transform = gameObject_8.transform;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (!(null == child))
			{
				PerformActionRecurs(child.gameObject, action_1);
			}
		}
	}

	public void ChangePreviousWeapon(bool bool_34 = true)
	{
		if (SlotType_0 != 0)
		{
			ChangeWeapon(SlotType_0, bool_34);
		}
	}

	public void ChangeWeapon(SlotType slotType_2, bool bool_34 = true, bool bool_35 = false)
	{
		if (slotType_2 != 0)
		{
			SlotType slotType = WeaponSounds_0.WeaponData_0.SlotType_0;
			if (slotType != slotType_2 || bool_35)
			{
				playerStateController_0.DispatchStartChangeWeapon();
				SlotType_0 = WeaponController.WeaponController_0.GetActiveWeaponSlotType();
				SlotType_1 = slotType_2;
				bool_7 = bool_34;
				Boolean_3 = true;
				StopCoroutine("ChangeWeaponCorutine");
				StartCoroutine("ChangeWeaponCorutine");
			}
		}
	}

	private IEnumerator ChangeWeaponCorutine()
	{
		writeLog("[Player_move_c::ChangeWeaponCorutine] Start");
		PhotonView_0.viewSynchronization_0 = ViewSynchronization.Off;
		if (PlayerTurretController_0.Boolean_1)
		{
			while (Single_0 < 40f && PlayerMechController_0.Boolean_1)
			{
				Single_0 += 300f * Time.deltaTime;
				yield return null;
			}
		}
		ChangeWeaponReal(SlotType_1, bool_7);
		if (SlotType_1 != SlotType.SLOT_CONSUM_TURRET && PlayerMechController_0.Boolean_1)
		{
			while (Single_0 > 0f)
			{
				Single_0 -= 300f * Time.deltaTime;
				if (Single_0 < 0f)
				{
					Single_0 = -0.01f;
				}
				yield return null;
			}
		}
		PhotonView_0.viewSynchronization_0 = ViewSynchronization.Unreliable;
		Boolean_23 = true;
		writeLog("[Player_move_c::ChangeWeaponCorutine] End");
	}

	public void ChangeWeaponReal(SlotType slotType_2, bool bool_34 = true)
	{
		playerStateController_0.DispatchStartChangeWeapon();
		int num = 0;
		GameObject gameObject = null;
		if (slotType_2 == SlotType.SLOT_CONSUM_TURRET)
		{
			int num2;
			gameObject = PlayerTurretController_0.GetTurretData(out num, out num2);
		}
		else
		{
			num = UserController.UserController_0.GetArtikulIdFromSlot(slotType_2);
			if (num == 0)
			{
				playerStateController_0.DispatchEndChangeWeapon();
				return;
			}
			gameObject = UserController.UserController_0.GetGameObject(num);
		}
		if (gameObject == null)
		{
			playerStateController_0.DispatchEndChangeWeapon();
			return;
		}
		EventHandler<EventArgs> eventHandler = eventHandler_0;
		if (eventHandler != null)
		{
			eventHandler(this, EventArgs.Empty);
		}
		if (Boolean_16)
		{
			ZoomPress();
		}
		PhotonView_0 = PhotonView.Get(this);
		Quaternion rotation = Quaternion.identity;
		if ((bool)GameObject_0)
		{
			rotation = GameObject_0.transform.rotation;
		}
		if (WeaponManager_0.WeaponSounds_0 != null)
		{
			rotation = WeaponManager_0.WeaponSounds_0.gameObject.transform.rotation;
			if (PlayerMechController_0.Boolean_1)
			{
				WeaponManager_0.WeaponSounds_0.gameObject.transform.parent = null;
				UnityEngine.Object.Destroy(WeaponManager_0.WeaponSounds_0.gameObject);
				WeaponManager_0.WeaponSounds_0 = null;
			}
		}
		GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(gameObject, Vector3.zero, Quaternion.identity);
		Transform transform = gameObject2.transform;
		GameObject_2 = gameObject2;
		WeaponSounds_0 = GameObject_2.GetComponent<WeaponSounds>();
		WeaponSounds_0.Init(num);
		if (WeaponSounds_0.WeaponData_0.Boolean_5)
		{
			gunCamera.transform.localPosition = Vector3.zero;
		}
		else
		{
			gunCamera.transform.localPosition = new Vector3(-0.1f, 0f, 0f);
		}
		transform.parent = base.gameObject.transform;
		transform.rotation = rotation;
		WeaponSounds_0.GameObject_0.GetComponent<Animation>().cullingType = AnimationCullingType.AlwaysAnimate;
		if (!PlayerMechController_0.Boolean_1)
		{
			GameObject_2.SetActive(false);
		}
		if (Boolean_4)
		{
			PhotonView_0.RPC("SetWeaponRPC", PhotonTargets.Others, WeaponSounds_0.WeaponData_0.Int32_0);
		}
		switch (slotType_2)
		{
		case SlotType.SLOT_CONSUM_GRENADE:
			PlayerGrenadeController_0.CreateGrenade(WeaponSounds_0);
			break;
		case SlotType.SLOT_CONSUM_TURRET:
			PlayerTurretController_0.CreateTurret();
			break;
		}
		PlayerFlashController_0.SetMySkin();
		SetLayerRecursively(gameObject2, LayerMask.NameToLayer("Gun"));
		WeaponManager_0.WeaponSounds_0 = WeaponSounds_0;
		Boolean_3 = false;
		if (transform.parent == null)
		{
			Log.AddLine("[Player_move_c::ChangeWeaponReal. weaponInstanceTransform.parent == null. SlotType = ]" + slotType_2, Log.LogLevel.ERROR);
		}
		else if (WeaponManager_0.WeaponSounds_0 == null)
		{
			Log.AddLine("[Player_move_c::ChangeWeaponReal. _weaponManager.currentWeaponModel == null. SlotType = ]" + slotType_2, Log.LogLevel.ERROR);
		}
		else
		{
			transform.position = transform.parent.TransformPoint(WeaponManager_0.WeaponSounds_0.gunPosition);
		}
		WeaponData weaponData_ = WeaponSounds_0.WeaponData_0;
		if (ArtikulData.IsWeapon(weaponData_.SlotType_0))
		{
			Weapon weaponFromSlot = WeaponManager_0.GetWeaponFromSlot(slotType_2);
			if (weaponFromSlot != null)
			{
				if (weaponFromSlot.Int32_1 <= 0 && (!weaponData_.Boolean_2 || weaponData_.Boolean_3))
				{
					BlinkReloadButton.bool_0 = true;
					if (HeadUpDisplay.HeadUpDisplay_0 != null)
					{
						HeadUpDisplay.HeadUpDisplay_0.BlinkNoAmmo(1);
					}
				}
				else
				{
					BlinkReloadButton.bool_0 = false;
					if (HeadUpDisplay.HeadUpDisplay_0 != null)
					{
						HeadUpDisplay.HeadUpDisplay_0.BlinkNoAmmo(0);
					}
				}
			}
		}
		GameObject gameObject3 = WeaponSounds_0.GameObject_0;
		if (gameObject3 != null)
		{
			if (gameObject3.GetComponent<Animation>().GetClip("Reload") != null)
			{
				gameObject3.GetComponent<Animation>()["Reload"].layer = 1;
			}
			if (!weaponData_.Boolean_5)
			{
				if (gameObject3.GetComponent<Animation>().GetClip("Shoot") != null)
				{
					gameObject3.GetComponent<Animation>()["Shoot"].layer = 1;
				}
				if (weaponData_.Boolean_2)
				{
					for (int i = 0; i < WeaponSounds_0.weaponAnimations.Count; i++)
					{
						string text = WeaponSounds_0.weaponAnimations[i].name;
						if (gameObject3.GetComponent<Animation>().GetClip(text) != null)
						{
							gameObject3.GetComponent<Animation>()[text].layer = 1;
						}
					}
				}
			}
			else
			{
				gameObject3.GetComponent<Animation>()["Shoot1"].layer = 1;
				gameObject3.GetComponent<Animation>()["Shoot2"].layer = 1;
			}
		}
		if (Defs.Boolean_0)
		{
			AudioClip clip = ((slotType_2 != SlotType.SLOT_CONSUM_GRENADE) ? ChangeWeaponClip : PlayerGrenadeController_0.ChangeGrenadeClip);
			base.gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
		}
		if (Boolean_14)
		{
			SetInVisibleShaders(Boolean_14);
		}
		playerStateController_0.DispatchEndChangeWeapon();
	}

	[RPC]
	private void SetWeaponRPC(int int_5)
	{
		Boolean_13 = false;
		if (int_5 == 0)
		{
			Log.AddLine("[Player_move_c::SetWeaponRPC. weapon artikulId = 0]", Log.LogLevel.ERROR);
			return;
		}
		GameObject gameObject = null;
		SlotType slotType = SlotType.SLOT_NONE;
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(int_5);
		if (artikul != null)
		{
			slotType = artikul.SlotType_0;
			gameObject = UserController.UserController_0.GetGameObject(int_5);
		}
		else
		{
			Log.AddLine("[Player_move_c::SetWeaponRPC. Weapon artikulData = null, artikulId = ]:" + int_5, Log.LogLevel.ERROR);
		}
		if (gameObject == null)
		{
			Log.AddLine("[Player_move_c::SetWeaponRPC. Weapon prefab = null, artikulId = ]:" + int_5, Log.LogLevel.ERROR);
			return;
		}
		Boolean_13 = true;
		if (Defs.Boolean_0 && slotType == SlotType.SLOT_CONSUM_GRENADE)
		{
			base.gameObject.GetComponent<AudioSource>().PlayOneShot(PlayerGrenadeController_0.ChangeGrenadeClip);
		}
		GameObject gameObject2 = null;
		gameObject2 = (GameObject)UnityEngine.Object.Instantiate(gameObject, Vector3.zero, Quaternion.identity);
		if (!PlayerMechController_0.Boolean_1)
		{
			gameObject2.SetActive(false);
		}
		GameObject_2 = gameObject2;
		WeaponSounds_0 = GameObject_2.GetComponent<WeaponSounds>();
		WeaponSounds_0.Init(int_5);
		Transform transform = mySkinName.armorPoint.transform;
		try
		{
			if (transform.childCount > 0)
			{
				ArmorRefs component = transform.GetChild(0).GetChild(0).GetComponent<ArmorRefs>();
				component.leftBone.GetComponent<SetPosInArmor>().target = WeaponSounds_0.Transform_0;
				component.rightBone.GetComponent<SetPosInArmor>().target = WeaponSounds_0.Transform_1;
			}
		}
		catch (Exception ex)
		{
			Log.AddLineError("[Player_move_c::SetWeaponRPC] ERROR something wrong with the armor Exception = {0}", ex.ToString());
			MonoSingleton<Log>.Prop_0.DumpError(ex, true);
		}
		foreach (Transform item in base.transform)
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}
		gameObject2.transform.parent = base.gameObject.transform;
		base.transform.localPosition = new Vector3(0f, 0.4f, 0f);
		gameObject2.transform.localPosition = new Vector3(0f, -1.4f, 0f);
		gameObject2.transform.rotation = base.transform.rotation;
		PlayerFlashController_0.SetMySkin();
	}

	public void SwitchPause()
	{
		SetPause();
	}

	public void RanksPressed()
	{
		if (!Boolean_18 && !TutorialController.TutorialController_0.Boolean_0 && !(PauseBattleWindow.PauseBattleWindow_0 != null) && !(BattleOverWindow.BattleOverWindow_0 != null))
		{
			Boolean_18 = true;
			BattleStatWindow.Show();
		}
	}

	public void RanksUnPressed()
	{
		if (Boolean_18)
		{
			Boolean_18 = false;
			BattleStatWindow.Hide();
		}
	}

	private void OnEnable()
	{
		if (bool_6)
		{
			Subscribe();
		}
	}

	private void OnDisable()
	{
		if (!Boolean_4 || Boolean_5)
		{
			UsersData.Unsubscribe(UsersData.EventType.SLOT_CHANGED, UserSlotChanged);
			LocalUserData.Unsubscribe(LocalUserData.EventType.SKILL_ADD, onSkillAdd);
			LocalUserData.Unsubscribe(LocalUserData.EventType.SKILL_REMOVE, onSkillRemove);
		}
	}

	private void Subscribe()
	{
		if (!Boolean_4 || Boolean_5)
		{
			UsersData.Subscribe(UsersData.EventType.SLOT_CHANGED, UserSlotChanged);
			LocalUserData.Subscribe(LocalUserData.EventType.SKILL_ADD, onSkillAdd);
			LocalUserData.Subscribe(LocalUserData.EventType.SKILL_REMOVE, onSkillRemove);
		}
	}

	private void onSkillAdd(int int_5 = 0)
	{
		switch (int_5)
		{
		case 33:
			SetInvisible(true);
			break;
		case 34:
			SetJetpackEnabled(true);
			break;
		}
	}

	private void onSkillRemove(int int_5 = 0)
	{
		switch (int_5)
		{
		case 33:
			SetInvisible(false);
			break;
		case 34:
			SetJetpackEnabled(false);
			break;
		}
	}

	private void CheckTimeCondition()
	{
		CampaignLevel campaignLevel = null;
		foreach (LevelBox item in LevelBox.list_0)
		{
			if (!item.string_0.Equals(CurrentCampaignGame.string_0))
			{
				continue;
			}
			foreach (CampaignLevel item2 in item.list_1)
			{
				if (item2.string_0.Equals(CurrentCampaignGame.string_1))
				{
					campaignLevel = item2;
					break;
				}
			}
			break;
		}
		float num = campaignLevel.float_0;
		if (float_1 >= num)
		{
			CurrentCampaignGame.bool_1 = false;
		}
	}

	[RPC]
	public void SetNickName(string string_3)
	{
		PhotonView_0 = PhotonView.Get(this);
		mySkinName.NickName = string_3;
		if (Boolean_5 || !(gameObject_1 == null))
		{
			return;
		}
		gameObject_1 = NickLabelStack.nickLabelStack_0.GetNextCurrentLabel().gameObject;
		if (gameObject_1 == null)
		{
			Log.AddLine("[Player_move_c::SetNickName] ERROR _label==null", Log.LogLevel.ERROR);
			return;
		}
		NickLabelController_0 = gameObject_1.GetComponent<NickLabelController>();
		if (NickLabelController_0 == null)
		{
			Log.AddLine("[Player_move_c::SetNickName] ERROR myNickLabelController==null", Log.LogLevel.ERROR);
			return;
		}
		if (NickLabelController_0.nickLabel == null)
		{
			Log.AddLine("[Player_move_c::SetNickName] ERROR myNickLabelController.nickLabel==null", Log.LogLevel.ERROR);
			return;
		}
		int layer = LayerMask.NameToLayer("NickLabel");
		gameObject_1.layer = layer;
		Transform[] componentsInChildren = gameObject_1.GetComponentsInChildren<Transform>(true);
		foreach (Transform transform in componentsInChildren)
		{
			transform.gameObject.layer = layer;
		}
		NickLabelController_0.Transform_0 = myTransform;
		NickLabelController_0.Player_move_c_0 = this;
		NickLabelController_0.nickLabel.String_0 = string_3;
		NickLabelController_0.Boolean_3 = false;
		NickLabelController_0.StartShow();
	}

	[RPC]
	public void SetAdminFlag(bool bool_34, bool bool_35)
	{
		if (!Boolean_5)
		{
			Boolean_24 = bool_34;
			if (NickLabelController_0 != null)
			{
				NickLabelController_0.Boolean_2 = bool_34 && bool_35;
			}
		}
	}

	public bool _singleOrMultiMine()
	{
		return !Boolean_4 || Boolean_5;
	}

	private void OnDestroy()
	{
		if (_singleOrMultiMine() && HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.Hide();
		}
		if (_singleOrMultiMine() || (WeaponManager_0 != null && WeaponManager_0.myPlayer == myPlayerTransform.gameObject))
		{
			if (pauser_0 != null && pauser_0.Boolean_0)
			{
				pauser_0.Boolean_0 = !pauser_0.Boolean_0;
				Time.timeScale = 1f;
			}
			else if (pauser_0 == null && Time.timeScale != 1f)
			{
				Time.timeScale = 1f;
			}
			GameObject gameObject = GameObject.FindGameObjectWithTag("DamageFrame");
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			ZombieCreator.BossKilled -= CheckTimeCondition;
		}
	}

	public void SetActiveWeaponEffect(bool bool_34, int int_5 = 0)
	{
		WeaponEffectsController weaponEffectsController_ = WeaponManager_0.WeaponSounds_0.WeaponEffectsController_0;
		if (!(weaponEffectsController_ == null))
		{
			WeaponData weaponData_ = WeaponManager_0.WeaponSounds_0.WeaponData_0;
			byte activeEffects = (byte)((!weaponData_.Boolean_5) ? int_5 : (Int32_0 - 1));
			weaponEffectsController_.SetActiveEffects(activeEffects);
		}
	}

	[RPC]
	private void ReloadGun()
	{
		if (myTransform.childCount != 0)
		{
			WeaponSounds component = myTransform.GetChild(0).GetComponent<WeaponSounds>();
			component.GameObject_0.GetComponent<Animation>().Play("Reload");
			component.GameObject_0.GetComponent<Animation>()["Reload"].speed = PlayerPhotonSkillUpdater_0.Single_0;
			if (Defs.Boolean_0)
			{
				base.GetComponent<AudioSource>().PlayOneShot(component.AudioClip_1);
			}
		}
	}

	private void Reload()
	{
		WeaponManager weaponManager = WeaponManager.weaponManager_0;
		if (weaponManager != null && weaponManager.WeaponSounds_0 != null)
		{
			WeaponSounds weaponSounds = weaponManager.WeaponSounds_0;
			if (HeadUpDisplay.HeadUpDisplay_0 != null)
			{
				float num = 0f;
				weaponSounds.PlayReloadAnimation(out num, false);
				HeadUpDisplay.HeadUpDisplay_0.ShowCircularIndicatorOnReload(num);
			}
			WeaponManager.weaponManager_0.Reload();
		}
	}

	[Obfuscation(Exclude = true)]
	public void ReloadPressed()
	{
		if (!PlayerGrenadeController_0.Boolean_0 || Boolean_10 || FirstPersonPlayerController_0.State_0 == FirstPersonPlayerController.State.Hook)
		{
			return;
		}
		WeaponData weaponData_ = WeaponManager_0.WeaponSounds_0.WeaponData_0;
		if (weaponData_.Boolean_2 && !weaponData_.Boolean_3)
		{
			return;
		}
		if (Boolean_16)
		{
			ZoomPress();
		}
		Weapon weaponFromSlot = WeaponManager_0.GetWeaponFromSlot(weaponData_.SlotType_0);
		if (weaponFromSlot == null)
		{
			Log.AddLine("[Player_move_c::ReloadPressed. Weapon artikul id not in weapon manager in list weapon? weaponId]: " + weaponData_.Int32_0, Log.LogLevel.ERROR);
		}
		else
		{
			if (weaponFromSlot.Int32_0 <= 0 || weaponFromSlot.Int32_1 == WeaponManager_0.WeaponSounds_0.WeaponData_0.Int32_2)
			{
				return;
			}
			Reload();
			if (!WeaponManager_0.WeaponSounds_0.WeaponData_0.Boolean_3)
			{
				if (Boolean_4)
				{
					PhotonView_0.RPC("ReloadGun", PhotonTargets.Others);
				}
				if (Defs.Boolean_0)
				{
					base.GetComponent<AudioSource>().PlayOneShot(WeaponManager_0.WeaponSounds_0.AudioClip_1);
				}
				BlinkReloadButton.bool_0 = false;
				if (HeadUpDisplay.HeadUpDisplay_0 != null)
				{
					HeadUpDisplay.HeadUpDisplay_0.BlinkNoAmmo(0);
				}
			}
		}
	}

	public void ShotPressed()
	{
		Debug.LogError(WeaponManager_0.WeaponSounds_0 == null);
		bool flag;
		if (Single_0 > 10f || (Boolean_4 && !PhotonView_0.Boolean_1) || (!(flag = !PlayerMechController_0.Boolean_1) && WeaponManager_0.WeaponSounds_0 == null))
		{
			return;
		}
		Animation animation_ = null;
		if (CurrentWeaponAnimation(out animation_, flag))
		{
			return;
		}
		animation_.Stop();
		if (!PlayerTurretController_0.Boolean_1)
		{
			return;
		}
		if (WeaponManager_0.WeaponSounds_0.WeaponData_0.Boolean_2 && !WeaponManager_0.WeaponSounds_0.WeaponData_0.Boolean_3)
		{
			_Shot();
			return;
		}
		Weapon weaponFromSlot = WeaponManager_0.GetWeaponFromSlot(WeaponManager_0.WeaponSounds_0.WeaponData_0.SlotType_0);
		if (weaponFromSlot == null && !flag)
		{
			return;
		}
		if (!flag && weaponFromSlot.Int32_1 <= 0)
		{
			if (HeadUpDisplay.HeadUpDisplay_0 != null)
			{
				HeadUpDisplay.HeadUpDisplay_0.BlinkNoAmmo(1);
				if (weaponFromSlot.Int32_0 == 0)
				{
					HeadUpDisplay.HeadUpDisplay_0.PlayLowResourceBeepIfNotPlaying(1);
					playerStateController_0.DispatchNoAmmo();
				}
			}
			if (!WeaponManager_0.WeaponSounds_0.WeaponData_0.Boolean_2)
			{
				WeaponManager_0.WeaponSounds_0.GameObject_0.GetComponent<Animation>().Play("Empty");
				if (Defs.Boolean_0)
				{
					base.GetComponent<AudioSource>().PlayOneShot(WeaponManager_0.WeaponSounds_0.AudioClip_2);
				}
			}
			return;
		}
		if (!flag)
		{
			weaponFromSlot.Int32_1--;
			if (weaponFromSlot.Int32_1 == 0)
			{
				if (weaponFromSlot.Int32_0 > 0)
				{
					if (WeaponManager_0.WeaponSounds_0.WeaponData_0.Boolean_3)
					{
						Reload();
					}
				}
				else
				{
					BlinkReloadButton.bool_0 = true;
					if (HeadUpDisplay.HeadUpDisplay_0 != null)
					{
						HeadUpDisplay.HeadUpDisplay_0.BlinkNoAmmo(3);
						HeadUpDisplay.HeadUpDisplay_0.PlayLowResourceBeep(3);
					}
					playerStateController_0.DispatchNoAmmo();
				}
			}
		}
		_Shot();
	}

	private bool CurrentWeaponAnimation(out Animation animation_0, bool bool_34)
	{
		WeaponSounds weaponSounds = WeaponManager_0.WeaponSounds_0;
		animation_0 = ((!bool_34) ? weaponSounds.GameObject_0.GetComponent<Animation>() : PlayerMechController_0.mechGunAnimation);
		int num = 0;
		while (true)
		{
			if (num < WeaponAnimationType.list_1.Count)
			{
				if (animation_0.IsPlaying(WeaponAnimationType.list_1[num]))
				{
					break;
				}
				num++;
				continue;
			}
			int num2 = 0;
			while (true)
			{
				if (num2 < weaponSounds.weaponAnimations.Count)
				{
					if (animation_0.IsPlaying(weaponSounds.weaponAnimations[num2].name))
					{
						break;
					}
					num2++;
					continue;
				}
				return false;
			}
			return true;
		}
		return true;
	}

	private int GetNumShootInDouble()
	{
		Int32_0++;
		if (Int32_0 == 3)
		{
			Int32_0 = 1;
		}
		return Int32_0;
	}

	private void _Shot()
	{
		if (!PlayerGrenadeController_0.Boolean_0 || Boolean_19)
		{
			return;
		}
		int num = 0;
		float num2 = 0f;
		if (!PlayerMechController_0.Boolean_1)
		{
			int numShootInDouble = GetNumShootInDouble();
			string text = MechAnimationType.string_2 + numShootInDouble;
			PlayerMechController_0.PlayWeaponAnimation(text);
			num2 = PlayerMechController_0.mechGunAnimation[text].length;
			float speed = ((!(PlayerMechController_0.ConsumableData_0.Single_2 > 0f)) ? 1f : (num2 / PlayerMechController_0.ConsumableData_0.Single_2));
			PlayerMechController_0.mechGunAnimation[text].speed = speed;
			if (Defs.Boolean_0)
			{
				base.GetComponent<AudioSource>().PlayOneShot(shootMechClip);
			}
		}
		else
		{
			WeaponSounds weaponSounds = WeaponManager_0.WeaponSounds_0;
			if (!weaponSounds.WeaponData_0.Boolean_5)
			{
				string string_ = WeaponAnimationType.string_4;
				if (weaponSounds.weaponAnimations.Count > 0)
				{
					if (weaponSounds.weaponAnimations.Count > 1)
					{
						num = UnityEngine.Random.Range(0, weaponSounds.weaponAnimations.Count);
					}
					string_ = weaponSounds.weaponAnimations[num].name;
				}
				weaponSounds.GameObject_0.GetComponent<Animation>().Play(string_);
				num2 = weaponSounds.GameObject_0.GetComponent<Animation>()[string_].length;
				weaponSounds.GameObject_0.GetComponent<Animation>()[string_].speed = num2 / weaponSounds.WeaponData_0.Single_0;
			}
			else
			{
				int numShootInDouble2 = GetNumShootInDouble();
				weaponSounds.GameObject_0.GetComponent<Animation>().Play(WeaponAnimationType.string_4 + numShootInDouble2);
				num2 = weaponSounds.GameObject_0.GetComponent<Animation>()[WeaponAnimationType.string_4 + numShootInDouble2].length;
				weaponSounds.GameObject_0.GetComponent<Animation>()[WeaponAnimationType.string_4 + numShootInDouble2].speed = num2 / weaponSounds.WeaponData_0.Single_0;
			}
			if (Defs.Boolean_0)
			{
				base.GetComponent<AudioSource>().PlayOneShot(weaponSounds.AudioClip_0);
			}
		}
		shootS(num);
	}

	public void sendImDeath(string string_3, byte byte_0, int int_5 = 0)
	{
		PhotonView_0.RPC("imDeath", PhotonTargets.All, string_3, byte_0);
		killerInfo_0.bool_0 = true;
		switch (int_5)
		{
		case 1:
			killerInfo_0.bool_1 = true;
			break;
		case 2:
			killerInfo_0.bool_4 = true;
			break;
		}
	}

	[RPC]
	public void imDeath(string string_3, byte byte_0)
	{
		if (!(WeaponManager_0 == null) && !(WeaponManager_0.myPlayer == null))
		{
			SendImKilled();
			WeaponManager_0.myPlayerMoveC.AddSystemMessage(string_3, 1, string.Empty, string.Empty, byte_0);
			MonoSingleton<FightController>.Prop_0.FightStatController_0.OnDeath((int)PhotonView_0.PhotonPlayer_0.Hashtable_0["uid"], (!(WeaponSounds_0 == null)) ? WeaponSounds_0.WeaponData_0.Int32_0 : 0);
			if (Boolean_5)
			{
				PlayerScoreController_0.AddDeath();
			}
		}
	}

	public void AddSystemMessage(string string_3, string string_4, string string_5, string string_6 = null, string string_7 = null, string string_8 = null)
	{
		for (int i = 0; i < 6; i++)
		{
			String_0[2][i] = String_0[1][i];
		}
		for (int j = 0; j < 6; j++)
		{
			String_0[1][j] = String_0[0][j];
		}
		String_0[0][0] = string_3;
		String_0[0][1] = string_4;
		String_0[0][2] = string_5;
		String_0[0][3] = string_6;
		String_0[0][4] = string_7;
		String_0[0][5] = string_8;
		Single_6[2] = Single_6[1];
		Single_6[1] = Single_6[0];
		Single_6[0] = 3f;
	}

	public void AddSystemMessage(string string_3, int int_5)
	{
		AddSystemMessage(string_3, string_1[int_5], string.Empty);
	}

	public void AddSystemMessage(string string_3, int int_5, string string_4, string string_5, int int_6 = 0, int int_7 = 0)
	{
		AddSystemMessage(string_3, string_1[int_5], string_4, string_5, int_6.ToString(), int_7.ToString());
	}

	public void AddSystemMessage(string string_3)
	{
		AddSystemMessage(string_3, string.Empty, string.Empty);
	}

	public void SendGlobalMessageToTeamates(int int_5)
	{
		int int32_ = WeaponManager.weaponManager_0.myPlayerMoveC.Int32_2;
		PhotonView_0.RPC("ShowCommandMessageRPC", PhotonTargets.All, int32_, int_5, mySkinName.NickName);
	}

	[RPC]
	public void ShowCommandMessageRPC(int int_5, int int_6, string string_3)
	{
		if (!(WeaponManager.weaponManager_0.myPlayer == null) && !(WeaponManager.weaponManager_0.myPlayerMoveC == null))
		{
			int int32_ = WeaponManager.weaponManager_0.myPlayerMoveC.Int32_2;
			if (int32_ == int_5)
			{
				HeadUpDisplay.HeadUpDisplay_0.TeamCommadLabel.String_0 = string.Format(Localizer.Get("hud.team_command.mode_" + (int)MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 + ".id_" + int_6), string_3);
				HeadUpDisplay.HeadUpDisplay_0.TeamCommadLabel.gameObject.SetActive(true);
				Invoke("HideCommandMessage", 3f);
			}
		}
	}

	private void HideCommandMessage()
	{
		HeadUpDisplay.HeadUpDisplay_0.TeamCommadLabel.gameObject.SetActive(false);
	}

	[RPC]
	public void SendSystemMessegeFromFlagDroppedRPC(bool bool_34, string string_3)
	{
		if (!(WeaponManager.weaponManager_0.myPlayer == null) && !(WeaponManager.weaponManager_0.myPlayerMoveC == null))
		{
			int int32_ = WeaponManager.weaponManager_0.myPlayerMoveC.Int32_2;
			if ((bool_34 && int32_ == 1) || (!bool_34 && int32_ == 2))
			{
				WeaponManager.weaponManager_0.myPlayerMoveC.AddSystemMessage(string.Format(Localizer.Get("ui.msg.dropped_my_flag"), string_3));
				PlayerMessageBadgeObject playerMessageBadgeObject = new PlayerMessageBadgeObject();
				playerMessageBadgeObject.string_0 = string.Format(Localizer.Get("ui.msg.dropped_my_flag"), string_3);
				WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0.PlayerMessageQueueConroller_0.Add(playerMessageBadgeObject);
			}
			else if ((bool_34 && int32_ == 2) || (!bool_34 && int32_ == 1))
			{
				WeaponManager.weaponManager_0.myPlayerMoveC.AddSystemMessage(string.Format(Localizer.Get("ui.msg.dropped_enemy_flag"), string_3));
				PlayerMessageBadgeObject playerMessageBadgeObject2 = new PlayerMessageBadgeObject();
				playerMessageBadgeObject2.string_0 = string.Format(Localizer.Get("ui.msg.dropped_enemy_flag"), string_3);
				WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0.PlayerMessageQueueConroller_0.Add(playerMessageBadgeObject2);
			}
		}
	}

	[RPC]
	public void SendSystemMessegeFromFlagCaptureRPC(bool bool_34, string string_3)
	{
		if (!(WeaponManager.weaponManager_0.myPlayer != null))
		{
			return;
		}
		if (WeaponManager.weaponManager_0.myPlayerMoveC.Int32_2 == 1 == bool_34)
		{
			WeaponManager.weaponManager_0.myPlayerMoveC.AddSystemMessage(string.Format("{0} {1}", string_3, LocalizationStore.Get("Key_1001")));
			if (Defs.Boolean_0)
			{
				base.GetComponent<AudioSource>().PlayOneShot(flagLostClip);
			}
		}
		else
		{
			WeaponManager.weaponManager_0.myPlayerMoveC.AddSystemMessage(LocalizationStore.Get("Key_1002"));
			if (Defs.Boolean_0)
			{
				base.GetComponent<AudioSource>().PlayOneShot(flagGetClip);
			}
		}
	}

	[RPC]
	public void SendSystemMessegeFromFlagAddScoreRPC(bool bool_34, string string_3)
	{
		if (WeaponManager.weaponManager_0.myPlayer != null)
		{
			if (Defs.Boolean_0)
			{
				base.GetComponent<AudioSource>().PlayOneShot((bool_34 != (WeaponManager_0.myPlayerMoveC.Int32_2 == 1)) ? flagScoreEnemyClip : flagScoreMyCommandClip);
			}
			Boolean_15 = false;
			WeaponManager.weaponManager_0.myPlayerMoveC.AddSystemMessage(string_3, 5);
		}
	}

	public void addMultyKill(Vector3 vector3_0)
	{
		Int32_3++;
		DominationController.TypeDomination typeDomination = DominationController.TypeDomination.NONE;
		if (Int32_3 > 1)
		{
			switch (Int32_3)
			{
			case 2:
				typeDomination = DominationController.TypeDomination.MULTY_KILL2;
				PlayerScoreController_0.AddScoreOnEvent(KillStreakType.KILLSTREAK_MULTIKILL_2, vector3_0);
				break;
			case 3:
				typeDomination = DominationController.TypeDomination.MULTY_KILL3;
				PlayerScoreController_0.AddScoreOnEvent(KillStreakType.KILLSTREAK_MULTIKILL_3, vector3_0);
				break;
			case 4:
				typeDomination = DominationController.TypeDomination.MULTY_KILL4;
				PlayerScoreController_0.AddScoreOnEvent(KillStreakType.KILLSTREAK_MULTIKILL_4, vector3_0);
				break;
			case 5:
				typeDomination = DominationController.TypeDomination.MULTY_KILL5;
				PlayerScoreController_0.AddScoreOnEvent(KillStreakType.KILLSTREAK_MULTIKILL_5, vector3_0);
				break;
			case 6:
				typeDomination = DominationController.TypeDomination.MULTY_KILL6;
				PlayerScoreController_0.AddScoreOnEvent(KillStreakType.KILLSTREAK_MULTIKILL_6, vector3_0);
				break;
			case 10:
				typeDomination = DominationController.TypeDomination.MULTY_KILL10;
				PlayerScoreController_0.AddScoreOnEvent(KillStreakType.KILLSTREAK_MULTIKILL_10, vector3_0);
				break;
			case 20:
				typeDomination = DominationController.TypeDomination.MULTY_KILL50;
				PlayerScoreController_0.AddScoreOnEvent(KillStreakType.KILLSTREAK_MULTIKILL_50, vector3_0);
				break;
			case 15:
				typeDomination = DominationController.TypeDomination.MULTY_KILL20;
				PlayerScoreController_0.AddScoreOnEvent(KillStreakType.KILLSTREAK_MULTIKILL_20, vector3_0);
				break;
			}
			if (Defs.bool_2)
			{
				PhotonView_0.RPC("ShowMultyKillRPC", PhotonTargets.Others, Int32_3);
				PlayerScoreController_0.photonView.RPC("FirstBloodRPC", PhotonTargets.AllBuffered, mySkinName.NickName, (int)typeDomination);
			}
		}
	}

	[RPC]
	public void ShowMultyKillRPC(int int_5)
	{
		if (NickLabelController_0 != null)
		{
			NickLabelController_0.ShowMultyKill(int_5);
		}
	}

	public void resetMultyKill()
	{
		Int32_3 = 0;
		if (Defs.bool_2)
		{
			PhotonView_0.RPC("ShowMultyKillRPC", PhotonTargets.Others, 0);
		}
	}

	public void ImKill(int int_5, int int_6, int int_7, string string_3, string string_4, Vector3 vector3_0)
	{
		addMultyKill(vector3_0);
		PlayerScoreController_0.AddKill();
		PlayerScoreController_0.IKill(int_7);
		int iKill = PlayerScoreController_0.GetIKill(int_7);
		int iKilled = PlayerScoreController_0.GetIKilled(int_7);
		int num = iKill - iKilled;
		DominationController.TypeDomination dominationLevel = DominationController.GetDominationLevel(Math.Abs(num));
		if (num < 0)
		{
			string text = string_3;
			string_3 = string_4;
			string_4 = text;
		}
		if (dominationLevel >= DominationController.TypeDomination.NONE)
		{
			PhotonView_0.RPC("ShowDominationRPC", PhotonTargets.All, string_3, string_4, (int)dominationLevel);
		}
	}

	[RPC]
	public void ShowDominationRPC(string string_3, string string_4, int int_5)
	{
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			Color color_ = Color.white;
			Color white = Color.white;
			DominationController.GetDominationColor(string_3, out white, out color_);
			if (int_5 == 0 && HeadUpDisplay.HeadUpDisplay_0.String_0 == string_3 && HeadUpDisplay.HeadUpDisplay_0.String_1 == string_4)
			{
				HeadUpDisplay.HeadUpDisplay_0.dominationController.SetItem(string.Empty, Color.white, string.Empty, Color.white, DominationController.TypeDomination.NOVILLIAN);
			}
			else if (int_5 > 0)
			{
				HeadUpDisplay.HeadUpDisplay_0.dominationController.SetItem(string_3, white, string_4, color_, (DominationController.TypeDomination)int_5);
				HeadUpDisplay.HeadUpDisplay_0.String_0 = string_3;
				HeadUpDisplay.HeadUpDisplay_0.String_1 = string_4;
			}
		}
	}

	[RPC]
	public void KilledPhoton(int int_5, byte byte_0, string string_3, byte byte_1, int int_6, bool bool_34, Vector3 vector3_0, int int_7)
	{
		if (myPlayerTransform == null)
		{
			return;
		}
		ImKilled(myPlayerTransform.position, myPlayerTransform.rotation, byte_1);
		if (WeaponManager_0 == null || WeaponManager_0.myPlayer == null || mySkinName == null || PhotonView_0 == null || PhotonView_0.PhotonPlayer_0 == null)
		{
			return;
		}
		string string_4 = string.Empty;
		string nickName = mySkinName.NickName;
		int int_8 = 0;
		int int32_ = Int32_2;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
		MonoSingleton<FightController>.Prop_0.FightStatController_0.OnDeath((int)PhotonView_0.PhotonPlayer_0.Hashtable_0["uid"], (!(WeaponSounds_0 == null)) ? WeaponSounds_0.WeaponData_0.Int32_0 : 0);
		for (int i = 0; i < array.Length; i++)
		{
			if (!(array[i].GetComponent<PhotonView>() != null) || array[i].GetComponent<PhotonView>().Int32_1 != int_5)
			{
				continue;
			}
			SkinName component = array[i].GetComponent<SkinName>();
			if (component == null)
			{
				continue;
			}
			Player_move_c player_move_c = component.Player_move_c_0;
			PhotonView photonView = PhotonView.Get(array[i]);
			if (photonView == null || photonView.PhotonPlayer_0 == null || player_move_c == null)
			{
				continue;
			}
			int customProperties = photonView.PhotonPlayer_0.Hashtable_0.GetCustomProperties("uid", int.TryParse, -1);
			if (customProperties == -1)
			{
				continue;
			}
			switch (byte_0)
			{
			case 6:
				MonoSingleton<FightController>.Prop_0.FightStatController_0.OnKill(customProperties, int_6);
				MonoSingleton<FightController>.Prop_0.FightStatController_0.OnGrenadeKill(customProperties);
				break;
			case 10:
				MonoSingleton<FightController>.Prop_0.FightStatController_0.OnKill(customProperties, int_6);
				MonoSingleton<FightController>.Prop_0.FightStatController_0.OnMechKill(customProperties);
				break;
			case 8:
				MonoSingleton<FightController>.Prop_0.FightStatController_0.OnKill(customProperties, int_6);
				MonoSingleton<FightController>.Prop_0.FightStatController_0.OnTurretKill(customProperties);
				break;
			default:
				MonoSingleton<FightController>.Prop_0.FightStatController_0.OnKill(customProperties, player_move_c.WeaponSounds_0.WeaponData_0.Int32_0);
				break;
			}
			string_4 = component.NickName;
			int_8 = player_move_c.Int32_2;
			if (WeaponManager_0 != null && array[i] == WeaponManager_0.myPlayer && player_move_c.PlayerScoreController_0 != null)
			{
				player_move_c.ImKill(int_5, byte_0, (int)PhotonView_0.PhotonPlayer_0.Hashtable_0["uid"], string_4, nickName, vector3_0);
				KillStreakType killStreakType_ = KillStreakType.KILL_COMMON;
				switch (byte_0)
				{
				case 6:
					killStreakType_ = KillStreakType.KILL_GERENADE;
					break;
				case 9:
					killStreakType_ = KillStreakType.KILL_TURRET;
					break;
				case 2:
					killStreakType_ = KillStreakType.KILL_HEADSHOT;
					break;
				case 3:
					killStreakType_ = KillStreakType.KILL_EXPLOSION;
					break;
				}
				player_move_c.PlayerScoreController_0.AddScoreOnEvent(killStreakType_);
				if (Boolean_14)
				{
					player_move_c.PlayerScoreController_0.AddScoreOnEvent(KillStreakType.KILL_INVISIBLE_KILL);
				}
				if (Boolean_11)
				{
					player_move_c.PlayerScoreController_0.AddScoreOnEvent(KillStreakType.REVENGE);
				}
				if (Equals(WeaponManager_0.myPlayerMoveC.player_move_c_0))
				{
					WeaponManager_0.myPlayerMoveC.player_move_c_0 = null;
					Boolean_11 = false;
				}
			}
			if (Boolean_5)
			{
				player_move_c.Boolean_11 = true;
				player_move_c_0 = player_move_c;
				PlayerScoreController_0.AddDeath();
			}
			UpdateKillerInfo(array[i], byte_0, int_6, bool_34, int_7);
			break;
		}
		if ((bool)WeaponManager_0 && WeaponManager_0.myPlayerMoveC != null)
		{
			WeaponManager_0.myPlayerMoveC.AddSystemMessage(string_4, byte_0, nickName, string_3, int_8, int32_);
		}
	}

	private void UpdateKillerInfo(GameObject gameObject_8, int int_5, int int_6, bool bool_34, int int_7)
	{
		killerInfo_0.bool_1 = int_5 == 6;
		killerInfo_0.bool_2 = int_5 == 10;
		killerInfo_0.bool_3 = int_5 == 8;
		SkinName component = gameObject_8.GetComponent<SkinName>();
		Player_move_c player_move_c = component.Player_move_c_0;
		killerInfo_0.string_0 = component.NickName;
		if (player_move_c.GameObject_1 != null && (bool)player_move_c.NetworkStartTable_0)
		{
			NetworkStartTable networkStartTable = player_move_c.NetworkStartTable_0;
			if (networkStartTable.Texture_2 != null)
			{
				killerInfo_0.texture_0 = networkStartTable.Texture_2;
			}
			killerInfo_0.string_1 = networkStartTable.String_3;
			killerInfo_0.int_11 = networkStartTable.Int32_2;
			killerInfo_0.int_12 = networkStartTable.Int32_3;
		}
		killerInfo_0.int_0 = int_6;
		killerInfo_0.texture_1 = player_move_c.Texture_0;
		killerInfo_0.int_3 = component.Int32_0;
		killerInfo_0.int_4 = component.Int32_1;
		killerInfo_0.int_5 = component.Int32_2;
		killerInfo_0.texture_2 = component.Texture_0;
		killerInfo_0.int_6 = component.Int32_3;
		killerInfo_0.transform_0 = player_move_c.myPlayerTransform;
		killerInfo_0.int_8 = (int)player_move_c.PlayerParametersController_0.Single_1;
		killerInfo_0.int_7 = ((!killerInfo_0.bool_2) ? ((int)player_move_c.PlayerParametersController_0.Single_2) : ((int)player_move_c.PlayerMechController_0.Single_0));
		killerInfo_0.int_10 = (int)player_move_c.PlayerParametersController_0.Single_6;
		killerInfo_0.int_9 = (int)player_move_c.PlayerParametersController_0.Single_7;
		int num = 0;
		try
		{
			num = (int)player_move_c.PhotonView_0.PhotonPlayer_0.Hashtable_0["uid"];
		}
		catch (Exception ex)
		{
			Log.AddLine(string.Format("[Player_move_c::UpdateKillerInfo] cant get uid exc = {0}", ex.Message), Log.LogLevel.ERROR);
			MonoSingleton<Log>.Prop_0.DumpError(ex);
		}
		killerInfo_0.int_1 = num;
		killerInfo_0.bool_5 = bool_34;
		killerInfo_0.int_2 = int_7;
	}

	public void MinusLive(int int_5, float float_13, TypeKills typeKills_0, int int_6, string string_3, int int_7, int int_8, float float_14, float float_15, bool bool_34, bool bool_35, bool bool_36, float float_16, float float_17)
	{
		if (Boolean_22)
		{
			return;
		}
		if (!PlayerMechController_0.Boolean_1)
		{
			if (float_13 > 0f && PlayerMechController_0.Single_0 <= 0f)
			{
				return;
			}
			if (PlayerMechController_0.HitMech(float_13))
			{
				WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0.AddScoreOnEvent(KillStreakType.KILL_PLAYER_MECH);
			}
		}
		else
		{
			if (float_13 > 0f && PlayerParametersController_0.Single_2 <= 0f)
			{
				return;
			}
			PlayerParametersController_0.HitPlayer(float_13, PlayerPhotonSkillUpdater_0.Single_2, false);
			if (PlayerParametersController_0.Single_2 <= 0f)
			{
				if (Boolean_15)
				{
					WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0.AddScoreOnEvent(KillStreakType.KILL_PLAYER_WITH_FLAG);
				}
				ImKilled(myPlayerTransform.position, myPlayerTransform.rotation, int_6);
				myPersonNetwork.StartAngel();
			}
		}
		DamageRPCData damageRPCData = damageRPCData_0;
		damageRPCData.int_0 = int_5;
		damageRPCData.float_0 = float_13;
		damageRPCData.float_1 = float_14;
		damageRPCData.float_2 = float_15;
		damageRPCData.byte_0 = (byte)typeKills_0;
		damageRPCData.byte_1 = (byte)int_6;
		damageRPCData.int_1 = int_8;
		damageRPCData.int_2 = int_7;
		damageRPCData.bool_0 = bool_34;
		damageRPCData.bool_1 = bool_35;
		damageRPCData.bool_2 = bool_36;
		damageRPCData.float_3 = float_16;
		damageRPCData.float_4 = PlayerPhotonSkillUpdater_0.Single_2;
		damageRPCData.float_5 = float_17;
		PhotonView_0.RPC("MinusLiveRPCPhoton", PhotonTargets.All, damageRPCData);
	}

	[RPC]
	public void MinusLiveRPCPhoton(DamageRPCData damageRPCData_1)
	{
		if (WeaponManager.weaponManager_0 == null || WeaponManager.weaponManager_0.myPlayerMoveC == null)
		{
			return;
		}
		float single_ = PlayerParametersController_0.Single_2;
		bool bool_ = false;
		bool flag;
		if (!(flag = damageRPCData_1.int_0 == WeaponManager.weaponManager_0.myPlayerMoveC.Int32_1) && single_ <= 0f)
		{
			return;
		}
		if (WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.myPlayerMoveC != null)
		{
			if (!flag)
			{
				if (PlayerMechController_0.Boolean_1)
				{
					PlayerParametersController_0.HitPlayer(damageRPCData_1.float_0, damageRPCData_1.float_4, false);
				}
				else
				{
					PlayerMechController_0.HitMech(damageRPCData_1.float_0);
				}
			}
			else if (single_ <= 0f)
			{
				bool_ = true;
			}
		}
		if (Boolean_5)
		{
		}
		if (PlayerParametersController_0.Single_2 <= 0f && single_ > 0f)
		{
			bool_ = true;
		}
		StartShowHeadShotParticle(damageRPCData_1);
		if (Defs.Boolean_0)
		{
			base.gameObject.GetComponent<AudioSource>().PlayOneShot((damageRPCData_1.byte_0 != 2) ? damagePlayerSound : headShotSound);
		}
		if (Boolean_5 && !Boolean_22)
		{
			ReceiveDamage(damageRPCData_1, bool_);
		}
		PlayerFlashController_0.FlashPlayer();
		PlayerDotEffectController_0.Check(damageRPCData_1);
	}

	public void ReceiveDamage(DamageRPCData damageRPCData_1, bool bool_34, bool bool_35 = true)
	{
		if (Boolean_20)
		{
			return;
		}
		PhotonView_0.RPC("OnDamageObtained", PhotonTargets.All, damageRPCData_1.float_0, damageRPCData_1.byte_0, damageRPCData_1.int_0, damageRPCData_1.int_1);
		GameObject killerObject = GetKillerObject(damageRPCData_1);
		if (bool_34)
		{
			if (list_0.Contains(damageRPCData_1.int_0))
			{
				list_0.Remove(damageRPCData_1.int_0);
			}
			if (player_move_c_0 != null)
			{
				player_move_c_0.Boolean_11 = false;
			}
			string text = string.Empty;
			if (damageRPCData_1.int_2 == 0)
			{
				if (damageRPCData_1.bool_0)
				{
					text = "Chat_Mech";
				}
				else
				{
					ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(damageRPCData_1.int_1);
					if (artikul != null)
					{
						text = artikul.String_3;
						while (text.Contains("/"))
						{
							text = text.Remove(0, text.IndexOf('/') + 1);
						}
					}
				}
			}
			int num = 0;
			if (damageRPCData_1.byte_0 == 8 && killerObject != null)
			{
				TurretController component = killerObject.GetComponent<TurretController>();
				if (component != null && component.ConsumableData_0 != null)
				{
					num = component.ConsumableData_0.Int32_0;
				}
			}
			PhotonView_0.RPC("KilledPhoton", PhotonTargets.All, damageRPCData_1.int_0, damageRPCData_1.byte_0, text, damageRPCData_1.byte_1, damageRPCData_1.int_1, damageRPCData_1.bool_1, myPlayerTransform.position, num);
		}
		else if (!list_0.Contains(damageRPCData_1.int_0))
		{
			list_0.Add(damageRPCData_1.int_0);
		}
		if (killerObject != null)
		{
			if (bool_35)
			{
				ShowDamageDirection(killerObject.transform.position);
			}
			if (bool_34)
			{
				PlayerScoreController_0.IKilled((int)killerObject.GetComponent<PhotonView>().PhotonPlayer_0.Hashtable_0["uid"]);
			}
		}
	}

	private GameObject GetKillerObject(DamageRPCData damageRPCData_1)
	{
		if (damageRPCData_1.byte_0 != 8)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
			for (int i = 0; i < array.Length; i++)
			{
				PhotonView component = array[i].GetComponent<PhotonView>();
				if (component != null && component.Int32_1 == damageRPCData_1.int_0)
				{
					return array[i];
				}
			}
		}
		else
		{
			GameObject[] array2 = GameObject.FindGameObjectsWithTag("Turret");
			for (int j = 0; j < array2.Length; j++)
			{
				PhotonView component2 = array2[j].GetComponent<PhotonView>();
				if (component2 != null && component2.Int32_1 == damageRPCData_1.int_2)
				{
					return array2[j];
				}
			}
		}
		return null;
	}

	private void validateDamage(DamageRPCData damageRPCData_1)
	{
		bool flag = false;
		string arg = string.Empty;
		WeaponData weaponData = null;
		ConsumableData consumableData = null;
		float float_ = 0f;
		if (!damageRPCData_1.bool_0 && damageRPCData_1.int_2 == 0)
		{
			weaponData = WeaponController.WeaponController_0.GetWeapon(damageRPCData_1.int_1);
			if (weaponData == null)
			{
				flag = true;
				arg = string.Format("[Player_move_c::validateDamage] weaponData = null  WeaponArtikulId = {0} ", damageRPCData_1.int_1);
			}
			else
			{
				float_ = weaponData.Single_4;
			}
		}
		else
		{
			consumableData = ConsumablesController.ConsumablesController_0.GetConsumable(damageRPCData_1.int_1);
			if (consumableData == null)
			{
				flag = true;
				arg = string.Format("[Player_move_c::validateDamage] consumableData = null  WeaponArtikulId = {0}  IdTurret = {1}  IsMesh = {2}", damageRPCData_1.int_1, damageRPCData_1.int_2, damageRPCData_1.bool_0.ToString());
			}
			else
			{
				float_ = consumableData.Single_1;
			}
		}
		if (!flag)
		{
			float_ = calculateDamage(float_, damageRPCData_1.bool_1, damageRPCData_1.float_1, damageRPCData_1.float_2, damageRPCData_1.bool_2, damageRPCData_1.float_3, damageRPCData_1.float_5);
			if (float_ != damageRPCData_1.float_0)
			{
				if ((double)float_ * 1.5 < (double)damageRPCData_1.float_0)
				{
					flag = true;
					arg = string.Format("[Player_move_c::validateDamage] CRITICAL damage error damage = {0} data.Damage = {1} ", float_, damageRPCData_1.float_0);
				}
				else
				{
					flag = true;
					arg = string.Format("[Player_move_c::validateDamage] damage error damage = {0} data.Damage = {1} ", float_, damageRPCData_1.float_0);
				}
			}
			if (!flag && (damageRPCData_1.float_1 > 1f || damageRPCData_1.float_2 > 1f))
			{
				flag = true;
				arg = string.Format("[Player_move_c::validateDamage] skill error = {0} DamageSkillModifier = {0}  HeadShotDamageSkillModifier = {1}  WeaponArtikulId = {2}", damageRPCData_1.float_1, damageRPCData_1.float_2, damageRPCData_1.int_1);
			}
		}
		if (flag)
		{
			Log.AddLine(string.Format("Player damage cheat!!! KillerId = {0}  message = {1}", damageRPCData_1.int_0, arg), Log.LogLevel.WARNING);
		}
	}

	private void StartShowHeadShotParticle(DamageRPCData damageRPCData_1)
	{
		if (Boolean_5)
		{
			return;
		}
		if (damageRPCData_1.byte_0 == 2)
		{
			HitParticle currentParticle = HeadShotStackController.headShotStackController_0.GetCurrentParticle(false);
			if (currentParticle != null)
			{
				currentParticle.StartShowParticle(myPlayerTransform.position, myPlayerTransform.rotation, false);
			}
		}
		else
		{
			HitParticle currentParticle2 = HitStackController.hitStackController_0.GetCurrentParticle(false);
			if (currentParticle2 != null)
			{
				currentParticle2.StartShowParticle(myPlayerTransform.position, myPlayerTransform.rotation, false);
			}
		}
	}

	[RPC]
	private void OnDamageObtained(float float_13, byte byte_0, int int_5, int int_6)
	{
		Player_move_c player_move_c = null;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
		if (array != null)
		{
			GameObject[] array2 = array;
			foreach (GameObject gameObject in array2)
			{
				PhotonView photonView = PhotonView.Get(gameObject);
				if (photonView != null && photonView.Int32_1 == int_5)
				{
					player_move_c = gameObject.GetComponent<SkinName>().Player_move_c_0;
					break;
				}
			}
		}
		if (!(player_move_c == null) && !(player_move_c.PhotonView_0 == null))
		{
			bool flag = byte_0 == 2;
			int customProperties = player_move_c.PhotonView_0.PhotonPlayer_0.Hashtable_0.GetCustomProperties("uid", int.TryParse, 0);
			int customProperties2 = PhotonView_0.PhotonPlayer_0.Hashtable_0.GetCustomProperties("uid", int.TryParse, 0);
			if (customProperties != 0 && customProperties2 != 0)
			{
				MonoSingleton<FightController>.Prop_0.FightStatController_0.OnHit(customProperties, int_6, flag, float_13);
				MonoSingleton<FightController>.Prop_0.FightStatController_0.OnDamage(customProperties2, float_13);
			}
		}
	}

	private void ShowDamageDirection(Vector3 vector3_0)
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		Vector3 vector = vector3_0 - myPlayerTransform.position;
		float num = Mathf.Atan(vector.z / vector.x);
		num = num * 180f / (float)Math.PI;
		if (vector.x > 0f)
		{
			num = 90f - num;
		}
		if (vector.x < 0f)
		{
			num = 270f - num;
		}
		float y = myPlayerTransform.rotation.eulerAngles.y;
		float num2 = num - y;
		if (num2 > 180f)
		{
			num2 -= 360f;
		}
		if (num2 < -180f)
		{
			num2 += 360f;
		}
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.AddDamageTaken(num);
		}
		if (num2 > -45f && num2 <= 45f)
		{
			flag3 = true;
		}
		if (num2 < -45f && num2 >= -135f)
		{
			flag = true;
		}
		if (num2 > 45f && num2 <= 135f)
		{
			flag2 = true;
		}
		if (num2 < -135f || num2 >= 135f)
		{
			flag4 = true;
		}
		if (flag3)
		{
			Single_2 = Single_1;
		}
		if (flag4)
		{
			Single_4 = Single_1;
		}
		if (flag)
		{
			Single_3 = Single_1;
		}
		if (flag2)
		{
			Single_5 = Single_1;
		}
	}

	[RPC]
	private void SetActiveEffectsRPC(bool bool_34, int int_5)
	{
		bool flag;
		WeaponSounds weaponSounds = ((!(flag = !PlayerMechController_0.Boolean_1)) ? WeaponSounds_0 : PlayerMechController_0.mechWeaponSounds);
		if (weaponSounds == null)
		{
			return;
		}
		if (bool_34)
		{
			weaponSounds.WeaponEffectsController_0.SetActiveEffects((byte)((int_5 != 0) ? ((uint)(int_5 - 1)) : 0u));
		}
		string text;
		if (!weaponSounds.WeaponData_0.Boolean_5)
		{
			text = "Shoot";
			if (int_5 > 0 && weaponSounds.weaponAnimations.Count > 0)
			{
				int_5 %= weaponSounds.weaponAnimations.Count;
				text = weaponSounds.weaponAnimations[int_5].name;
			}
		}
		else
		{
			text = "Shoot" + int_5;
		}
		if (flag)
		{
			PlayerMechController_0.PlayWeaponAnimation(text);
			if (Defs.Boolean_0)
			{
				base.GetComponent<AudioSource>().PlayOneShot(shootMechClip);
			}
		}
		else
		{
			weaponSounds.GameObject_0.GetComponent<Animation>().Play(text);
			if (Defs.Boolean_0)
			{
				base.GetComponent<AudioSource>().PlayOneShot(weaponSounds.AudioClip_0);
			}
		}
	}

	[RPC]
	public void HoleRPC(bool bool_34, Vector3 vector3_0, Quaternion quaternion_0, int int_5)
	{
		if (bool_34)
		{
			if (BloodParticleStackController.bloodParticleStackController_0 != null)
			{
				WallBloodParticle currentParticle = BloodParticleStackController.bloodParticleStackController_0.GetCurrentParticle((BulletType)int_5, false);
				if (currentParticle != null)
				{
					currentParticle.StartShowParticle(vector3_0, quaternion_0, false);
				}
			}
			return;
		}
		if (HoleBulletStackController.holeBulletStackController_0 != null)
		{
			HoleScript currentHole = HoleBulletStackController.holeBulletStackController_0.GetCurrentHole((BulletType)int_5, false);
			if (currentHole != null)
			{
				currentHole.StartShowHole(vector3_0, quaternion_0, false);
			}
		}
		if (WallParticleStackController.wallParticleStackController_0 != null)
		{
			WallBloodParticle currentParticle2 = WallParticleStackController.wallParticleStackController_0.GetCurrentParticle((BulletType)int_5, false);
			if (currentParticle2 != null)
			{
				currentParticle2.StartShowParticle(vector3_0, quaternion_0, false);
			}
		}
	}

	private void FixedUpdate()
	{
		if (!PlayerParametersController_0.isStarted)
		{
			return;
		}
		if (Boolean_4 && !Boolean_5 && NickLabelController_0 != null)
		{
			bool flag = false;
			if (visibleObj.isVisible && WeaponManager.weaponManager_0.myPlayer != null)
			{
				Ray ray = new Ray(myPlayerTransform.position, WeaponManager.weaponManager_0.myPlayer.transform.position - myPlayerTransform.position);
				RaycastHit hitInfo;
				if (Physics.Raycast(ray, out hitInfo, 50f) && hitInfo.collider.gameObject.transform.parent != null && hitInfo.collider.gameObject.transform.parent.gameObject.Equals(WeaponManager.weaponManager_0.myPlayer))
				{
					flag = true;
				}
			}
			Boolean_12 = flag;
			NickLabelController_0.Boolean_1 = flag;
			if (KillCamWindow.KillCamWindow_0 != null && KillCamWindow.KillCamWindow_0.Boolean_0)
			{
				NickLabelController_0.Boolean_1 = true;
			}
		}
		if (Time.realtimeSinceStartup - float_0 >= 1.25f && gameObject_4 != null)
		{
			float num = ((!(WeaponManager_0.WeaponSounds_0.WeaponData_0.Single_2 > 0f)) ? 1f : WeaponManager_0.WeaponSounds_0.WeaponData_0.Single_2);
			gameObject_4.GetComponent<Rigidbody>().AddForce(187.5f * gameObject_4.transform.forward * ((!WeaponManager_0.WeaponSounds_0.WeaponData_0.Boolean_11) ? 1f : 0.75f) * num);
			gameObject_4 = null;
		}
		if (!Boolean_4 || !Boolean_5 || Camera.main == null)
		{
			return;
		}
		Ray ray2 = Camera.main.ScreenPointToRay(new Vector3((float)Screen.width * 0.5f, (float)Screen.height * 0.5f, 0f));
		RaycastHit hitInfo2;
		if (!Physics.Raycast(ray2, out hitInfo2, 50f, Int32_4))
		{
			return;
		}
		if (hitInfo2.collider.gameObject.transform.parent != null && hitInfo2.collider.gameObject.transform.parent.CompareTag("Player"))
		{
			GameObject gameObject = hitInfo2.collider.gameObject.transform.parent.GetComponent<SkinName>().Player_move_c_0.gameObject_1;
			if (gameObject != null)
			{
				gameObject.GetComponent<NickLabelController>().ResetTimeShow();
			}
		}
		if (hitInfo2.collider.gameObject.CompareTag("Turret"))
		{
			NickLabelController nickLabelController = hitInfo2.collider.gameObject.GetComponent<TurretController>().NickLabelController_0;
			if (nickLabelController != null)
			{
				nickLabelController.ResetTimeShow();
			}
		}
	}

	public void SetActiveEffects(int int_5 = 0)
	{
		if (Boolean_4)
		{
			PhotonView_0.RPC("SetActiveEffectsRPC", PhotonTargets.Others, true, int_5);
		}
	}

	public static float calculateDamage(float float_13, bool bool_34, float float_14, float float_15, bool bool_35, float float_16, float float_17)
	{
		float num = 1f;
		if (bool_34)
		{
			num = 2f + float_15;
		}
		if (!bool_35)
		{
			float_16 = 0f;
		}
		return float_13 * (1f + float_14) * (1f + float_16) * num * float_17;
	}

	public void HitTurret(HitStruct hitStruct_0, GameObject gameObject_8)
	{
		if (gameObject_8.GetComponent<TurretController>().Boolean_0)
		{
			float float_ = 0f;
			float float_2 = 0f;
			float float_3 = 0f;
			bool bool_ = UnityEngine.Random.Range(0f, 1f) <= hitStruct_0.float_5;
			float damage = getDamage(hitStruct_0, false, false, out float_, out float_2, out float_3, bool_);
			gameObject_8.GetComponent<TurretController>().MinusLive(damage, myPlayerTransform.GetComponent<PhotonView>().Int32_1);
		}
	}

	public void HitZombie(HitStruct hitStruct_0, GameObject gameObject_8)
	{
		if (hitStruct_0 != null)
		{
			if (HeadUpDisplay.HeadUpDisplay_0 != null && !hitStruct_0.bool_2)
			{
				HeadUpDisplay.HeadUpDisplay_0.AimHitAnimPlay();
			}
			int num = UserController.UserController_0.UserData_0.user_0.int_0;
			float float_ = 0f;
			float float_2 = 0f;
			float float_3 = 0f;
			bool bool_ = UnityEngine.Random.Range(0f, 1f) <= hitStruct_0.float_5;
			float damage = getDamage(hitStruct_0, false, false, out float_, out float_2, out float_3, bool_);
			if (!Boolean_4)
			{
				BotHealth component = gameObject_8.transform.parent.GetComponent<BotHealth>();
				component.adjustHealth(num, 0f - damage, Camera.main.transform);
			}
		}
	}

	public void HitPlayer(HitStruct hitStruct_0, GameObject gameObject_8, GameObject gameObject_9, RocketSettings rocketSettings_0 = null)
	{
		if (hitStruct_0 == null)
		{
			return;
		}
		Player_move_c player_move_c = gameObject_8.GetComponent<SkinName>().Player_move_c_0;
		bool flag = hitStruct_0.bool_0;
		bool bool_ = hitStruct_0.bool_1;
		bool flag2 = player_move_c.Equals(this);
		if (((Boolean_6 || Boolean_7) && Int32_2 == player_move_c.Int32_2 && !flag2) || (!Boolean_4 && !flag2) || (flag2 && hitStruct_0.bool_2) || Boolean_8)
		{
			return;
		}
		if (!hitStruct_0.bool_2)
		{
			Boolean_1 = true;
			GameObject gameObject = player_move_c.gameObject_1;
			if (gameObject != null)
			{
				gameObject.GetComponent<NickLabelController>().ResetTimeShow();
			}
		}
		if (!player_move_c.Boolean_22 && HeadUpDisplay.HeadUpDisplay_0 != null && !hitStruct_0.bool_2)
		{
			HeadUpDisplay.HeadUpDisplay_0.AimHitAnimPlay();
		}
		bool flag3 = false;
		if (gameObject_9 != null && !hitStruct_0.bool_8 && gameObject_9.CompareTag("HeadCollider"))
		{
			Boolean_2 = true;
			flag3 = UnityEngine.Random.Range(0f, 1f) >= player_move_c.PlayerPhotonSkillUpdater_0.Single_1;
		}
		float float_ = 0f;
		float float_2 = 0f;
		float float_3 = 0f;
		bool bool_2 = UnityEngine.Random.Range(0f, 1f) <= hitStruct_0.float_5;
		float damage = getDamage(hitStruct_0, flag2, flag3, out float_, out float_2, out float_3, bool_2);
		if (flag2)
		{
			player_move_c.PlayerParametersController_0.HitPlayer(damage, player_move_c.PlayerPhotonSkillUpdater_0.Single_2);
			if (player_move_c.PlayerParametersController_0.Single_2 <= 0f)
			{
				player_move_c.sendImDeath(gameObject_8.GetComponent<SkinName>().NickName, (byte)player_move_c.Int32_2, (hitStruct_0.slotType_0 == SlotType.SLOT_CONSUM_GRENADE) ? 1 : 2);
			}
			else
			{
				player_move_c.IndicateDamage();
			}
			return;
		}
		TypeKills typeKill = getTypeKill(hitStruct_0.bool_2, flag, flag3, bool_, rocketSettings_0);
		player_move_c.MinusLive(Int32_1, damage, typeKill, (int)hitStruct_0.deadType_0, hitStruct_0.string_0, hitStruct_0.int_1, hitStruct_0.int_0, float_, float_2, flag, flag3, bool_2, hitStruct_0.float_6, float_3);
		if (damage > 0f)
		{
			PlayerScoreController_0.Single_0 += damage;
		}
	}

	private float getDamage(HitStruct hitStruct_0, bool bool_34, bool bool_35, out float float_13, out float float_14, out float float_15, bool bool_36)
	{
		float num = 0f;
		float_13 = 0f;
		float_15 = 1f - (1f - hitStruct_0.float_9) * hitStruct_0.float_13;
		float_13 = hitStruct_0.float_1;
		if (bool_34)
		{
			float_13 = hitStruct_0.float_3 - 1f;
			float_15 = 1f - (1f - hitStruct_0.float_10) * hitStruct_0.float_13;
		}
		else if (hitStruct_0.slotType_0 == SlotType.SLOT_CONSUM_GRENADE)
		{
			float_13 = hitStruct_0.float_4;
		}
		float_14 = hitStruct_0.float_2;
		float float_16 = hitStruct_0.float_5;
		return calculateDamage(hitStruct_0.float_0, bool_35, float_13, float_14, bool_36, float_16, float_15);
	}

	private TypeKills getTypeKill(bool bool_34, bool bool_35, bool bool_36, bool bool_37, RocketSettings rocketSettings_0 = null)
	{
		if (bool_34)
		{
			return TypeKills.turret;
		}
		if (bool_35)
		{
			return TypeKills.mech;
		}
		if (bool_36)
		{
			return TypeKills.headshot;
		}
		if (rocketSettings_0 != null)
		{
			return rocketSettings_0.typeKilsIconChat;
		}
		if (bool_37)
		{
			return TypeKills.zoomingshot;
		}
		return TypeKills.none;
	}

	[RPC]
	private void OnShoot()
	{
		if (MonoSingleton<FightController>.Prop_0 != null && PhotonView_0 != null && PhotonView_0.PhotonPlayer_0 != null && WeaponSounds_0 != null && WeaponSounds_0.WeaponData_0 != null)
		{
			MonoSingleton<FightController>.Prop_0.FightStatController_0.OnShoot((int)PhotonView_0.PhotonPlayer_0.Hashtable_0["uid"], WeaponSounds_0.WeaponData_0.Int32_0);
		}
	}

	public void shootS(int int_5 = 0)
	{
		if (PlayerGrenadeController_0.Boolean_0)
		{
			bool flag = !PlayerMechController_0.Boolean_1;
			WeaponSounds weaponSounds = WeaponManager_0.WeaponSounds_0;
			PhotonView_0.RPC("OnShoot", PhotonTargets.All);
			PlayerScoreController_0.Int32_1++;
			if (!flag && weaponSounds.WeaponData_0.Boolean_2)
			{
				ShootFromMeleeWeapon(int_5);
			}
			else if (!flag && weaponSounds.WeaponData_0.Boolean_7)
			{
				ShootFromConeWeapon();
			}
			else if (!flag && weaponSounds.WeaponData_0.Boolean_8)
			{
				ShootFromBazooka();
			}
			else if (weaponSounds.WeaponData_0.Boolean_12)
			{
				ShootFromRayAOEWeapon(int_5);
			}
			else
			{
				ShootFromRayWeapon(int_5);
			}
		}
	}

	public void SendHoleRPC(BulletType bulletType_0, bool bool_34, Vector3 vector3_0, Quaternion quaternion_0)
	{
		PhotonView_0.RPC("HoleRPC", PhotonTargets.Others, bool_34, vector3_0, quaternion_0, (int)bulletType_0);
	}

	public void SendAddFreezerRayWithLength(float float_13)
	{
		PhotonView_0.RPC("AddFreezerRayWithLength", PhotonTargets.Others, float_13);
	}

	private void ShootFromRayWeapon(int int_5 = 0)
	{
		bool flag = !PlayerMechController_0.Boolean_1;
		WeaponSounds weaponSounds = WeaponManager_0.WeaponSounds_0;
		int int32_ = weaponSounds.WeaponData_0.Int32_7;
		Boolean_1 = false;
		Boolean_2 = false;
		string text = weaponSounds.gameObject.name.Replace("(Clone)", string.Empty);
		HitStruct hitStruct = HitStruct.GenerateHitStruct(weaponSounds.WeaponData_0, false, flag, Boolean_16, text, weaponSounds);
		for (int i = 0; i < int32_; i++)
		{
			PlayerDamageController.PlayerDamageController_0.RayShoot(hitStruct, Boolean_4, int_5);
			hitStruct.IncrementCounterBulletsInShoot();
		}
		if (Boolean_1)
		{
			PlayerScoreController_0.Int32_2++;
		}
		if (Boolean_2)
		{
			PlayerScoreController_0.Int32_3++;
		}
		Boolean_1 = false;
		Boolean_2 = false;
	}

	private void ShootFromConeWeapon()
	{
		bool flag = !PlayerMechController_0.Boolean_1;
		WeaponSounds weaponSounds = WeaponManager_0.WeaponSounds_0;
		Boolean_1 = false;
		Boolean_2 = false;
		string text = weaponSounds.gameObject.name.Replace("(Clone)", string.Empty);
		HitStruct hitStruct_ = HitStruct.GenerateHitStruct(weaponSounds.WeaponData_0, false, flag, Boolean_16, text, weaponSounds);
		PlayerDamageController.PlayerDamageController_0.ConeShoot(hitStruct_);
		SetActiveWeaponEffect(true);
		SetActiveEffects();
		if (Boolean_1)
		{
			PlayerScoreController_0.Int32_2++;
		}
		if (Boolean_2)
		{
			PlayerScoreController_0.Int32_3++;
		}
		Boolean_1 = false;
		Boolean_2 = false;
	}

	private void ShootFromBazooka()
	{
		bool flag = !PlayerMechController_0.Boolean_1;
		WeaponSounds weaponSounds = WeaponManager_0.WeaponSounds_0;
		weaponSounds.Fire();
		SetActiveWeaponEffect(true);
		SetActiveEffects();
		WeaponEffectsController weaponEffectsController_ = weaponSounds.WeaponEffectsController_0;
		Vector3 vector = ((!(weaponEffectsController_.Transform_0 != null)) ? (myTransform.position + myTransform.forward * 0.2f) : weaponEffectsController_.Transform_0.position);
		GameObject gameObject = null;
		if (Boolean_4)
		{
			gameObject = PhotonNetwork.Instantiate("Prefabs/Grenades/Rocket", vector, myTransform.rotation, 0);
		}
		else
		{
			GameObject original = Resources.Load("Prefabs/Grenades/Rocket") as GameObject;
			gameObject = UnityEngine.Object.Instantiate(original, vector, myTransform.rotation) as GameObject;
		}
		Rocket component = gameObject.GetComponent<Rocket>();
		component.Int32_0 = weaponSounds.rocketNum;
		string text = weaponSounds.gameObject.name.Replace("(Clone)", string.Empty);
		component.HitStruct_0 = HitStruct.GenerateHitStruct(weaponSounds.WeaponData_0, true, flag, Boolean_16, text, weaponSounds);
		component.Single_0 = weaponSounds.WeaponData_0.Single_11 * (1f + UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_EXPLOSION_RADIUS_IMPULS_MODIFIER));
		gameObject.GetComponent<Rigidbody>().useGravity = weaponSounds.WeaponData_0.Boolean_11;
		gameObject_4 = gameObject;
	}

	private void ShootFromMeleeWeapon(int int_5 = 0)
	{
		WeaponSounds weaponSounds = WeaponManager_0.WeaponSounds_0;
		SetActiveWeaponEffect(true);
		SetActiveEffects(int_5);
		bool flag = !PlayerMechController_0.Boolean_1;
		string text = weaponSounds.gameObject.name.Replace("(Clone)", string.Empty);
		HitStruct hitStruct_ = HitStruct.GenerateHitStruct(weaponSounds.WeaponData_0, true, flag, Boolean_16, text, weaponSounds);
		if (weaponSounds.WeaponData_0.Boolean_13)
		{
			StartCoroutine(HitByAOEMelee(hitStruct_));
			return;
		}
		GameObject mileeAim = PlayerDamageController.PlayerDamageController_0.GetMileeAim(hitStruct_);
		if (mileeAim != null)
		{
			StartCoroutine(HitByMelee(mileeAim, hitStruct_));
		}
	}

	private void ShootFromRayAOEWeapon(int int_5 = 0)
	{
		bool flag = !PlayerMechController_0.Boolean_1;
		WeaponSounds weaponSounds = WeaponManager_0.WeaponSounds_0;
		Boolean_1 = false;
		Boolean_2 = false;
		string text = weaponSounds.gameObject.name.Replace("(Clone)", string.Empty);
		HitStruct hitStruct_ = HitStruct.GenerateHitStruct(weaponSounds.WeaponData_0, false, flag, Boolean_16, text, weaponSounds);
		HashSet<GameObject> hashSet_ = new HashSet<GameObject>();
		Vector3 vector3_ = PlayerDamageController.PlayerDamageController_0.RayShoot(hitStruct_, Boolean_4, int_5, hashSet_);
		if (vector3_.x != -999f || vector3_.y != -999f || vector3_.z != -999f)
		{
			PlayerDamageController.PlayerDamageController_0.RadiusShoot(hitStruct_, vector3_, null, hashSet_);
			float num = weaponSounds.WeaponData_0.Single_11 * (1f + UserController.UserController_0.GetFloatSummModifier(SkillId.SKILL_EXPLOSION_RADIUS_IMPULS_MODIFIER));
			if (num > 0f)
			{
				PhotonView_0.RPC("ImpactRPC", PhotonTargets.All, num, vector3_.x, vector3_.y, vector3_.z);
			}
		}
		if (Boolean_1)
		{
			PlayerScoreController_0.Int32_2++;
		}
		if (Boolean_2)
		{
			PlayerScoreController_0.Int32_3++;
		}
		Boolean_1 = false;
		Boolean_2 = false;
	}

	[RPC]
	public void ImpactRPC(float float_13, float float_14, float float_15, float float_16)
	{
		PlayerDamageController.PlayerDamageController_0.ExplosionImpact(float_13, new Vector3(float_14, float_15, float_16));
	}

	private IEnumerator HitByMelee(GameObject gameObject_8, HitStruct hitStruct_0)
	{
		if (!WeaponManager_0.WeaponSounds_0.WeaponData_0.Boolean_3)
		{
			yield return new WaitForSeconds(WeaponManager_0.WeaponSounds_0.GameObject_0.GetComponent<Animation>()["Shoot"].length * WeaponManager_0.WeaponSounds_0.WeaponData_0.Single_16);
		}
		if (!(gameObject_8 == null))
		{
			Boolean_2 = false;
			Boolean_1 = false;
			PlayerDamageController.PlayerDamageController_0.DoHitObject(hitStruct_0, gameObject_8);
			if (Boolean_2)
			{
				PlayerScoreController_0.Int32_3++;
			}
			if (Boolean_1)
			{
				PlayerScoreController_0.Int32_2++;
			}
			Boolean_2 = false;
			Boolean_1 = false;
		}
	}

	private IEnumerator HitByAOEMelee(HitStruct hitStruct_0)
	{
		WeaponSounds weaponSounds = WeaponManager_0.WeaponSounds_0;
		yield return new WaitForSeconds(weaponSounds.GameObject_0.GetComponent<Animation>()["Shoot"].length * weaponSounds.WeaponData_0.Single_16);
		Boolean_2 = false;
		Boolean_1 = false;
		Vector3 position = myTransform.position;
		PlayerDamageController.PlayerDamageController_0.RadiusShoot(hitStruct_0, position);
		if (Boolean_2)
		{
			PlayerScoreController_0.Int32_3++;
		}
		if (Boolean_1)
		{
			PlayerScoreController_0.Int32_2++;
		}
		Boolean_2 = false;
		Boolean_1 = false;
	}

	[RPC]
	public void AddFreezerRayWithLength(float float_13)
	{
		if (WeaponSounds_0 == null)
		{
			return;
		}
		Transform transform_ = WeaponSounds_0.WeaponEffectsController_0.Transform_0;
		if (transform_ == null)
		{
			return;
		}
		Transform transform = WeaponSounds_0.transform;
		if (action_0 != null)
		{
			action_0(float_13);
			return;
		}
		GameObject gameObject = WeaponManager.AddRay(transform_.position, transform.forward, transform.name.Replace("(Clone)", string.Empty));
		if (gameObject != null)
		{
			FreezerRay component = gameObject.GetComponent<FreezerRay>();
			if (component != null)
			{
				component.SetParentMoveC(this);
				component.Single_0 = float_13;
			}
		}
	}

	private IEnumerator Fade(float float_13, float float_14, float float_15, GameObject gameObject_8)
	{
		for (float num = 0f; num < 1f; num += Time.deltaTime / float_15)
		{
			if (gameObject_8 == null)
			{
				break;
			}
			if (gameObject_8.GetComponent<GUITexture>() == null)
			{
				break;
			}
			Color color = gameObject_8.GetComponent<GUITexture>().color;
			color.a = Mathf.Lerp(float_13, float_14, num);
			gameObject_8.GetComponent<GUITexture>().color = color;
			yield return 0;
			if (!(gameObject_8 == null) && !(gameObject_8.GetComponent<GUITexture>() == null))
			{
				Color color2 = gameObject_8.GetComponent<GUITexture>().color;
				color2.a = float_14;
				gameObject_8.GetComponent<GUITexture>().color = color2;
				continue;
			}
			break;
		}
	}

	public void SendImKilled()
	{
		PhotonView_0.RPC("ImKilledRPC", PhotonTargets.All, myPlayerTransform.position, myPlayerTransform.rotation, 0);
	}

	[RPC]
	private void ImKilledRPC(Vector3 vector3_0, Quaternion quaternion_0, int int_5 = 0)
	{
		ImKilled(vector3_0, quaternion_0, int_5);
	}

	private void ImKilled(Vector3 vector3_0, Quaternion quaternion_0, int int_5 = 0)
	{
		if (!Boolean_21 || Defs.bool_4)
		{
			Boolean_21 = true;
			PlayerDeadController currentParticle = PlayerDeadStackController.playerDeadStackController_0.GetCurrentParticle(false);
			if (currentParticle != null)
			{
				currentParticle.StartShow(vector3_0, quaternion_0, int_5, false, Texture_0);
			}
			if (Defs.Boolean_0)
			{
				base.gameObject.GetComponent<AudioSource>().PlayOneShot(deadPlayerSound);
			}
		}
	}

	private IEnumerator FlashWhenHit()
	{
		bool_3 = true;
		Color color = gameObject_3.GetComponent<GUITexture>().color;
		color.a = 0f;
		gameObject_3.GetComponent<GUITexture>().color = color;
		float float_ = 0.15f;
		yield return StartCoroutine(Fade(0f, 1f, float_, gameObject_3));
		yield return new WaitForSeconds(0.01f);
		yield return StartCoroutine(Fade(1f, 0f, float_, gameObject_3));
		bool_3 = false;
	}

	private IEnumerator FlashWhenDead()
	{
		if (!(gameObject_3 == null))
		{
			bool_3 = true;
			Color color = gameObject_3.GetComponent<GUITexture>().color;
			color.a = 0f;
			gameObject_3.GetComponent<GUITexture>().color = color;
			float num = 0.15f;
			yield return StartCoroutine(Fade(0f, 1f, num, gameObject_3));
			while (bool_1)
			{
				yield return null;
			}
			yield return StartCoroutine(Fade(1f, 0f, num / 3f, gameObject_3));
			bool_3 = false;
		}
	}

	[Obfuscation(Exclude = true)]
	private void setisDeadFrameFalse()
	{
		bool_1 = false;
	}

	private void Update()
	{
		if (!PlayerParametersController_0.isStarted)
		{
			return;
		}
		if (!Boolean_20 && float_3 > 0f)
		{
			float_3 -= Time.deltaTime;
			if (float_3 <= 0f)
			{
				Boolean_22 = false;
			}
		}
		if (Boolean_5)
		{
			if (InputManager.GetButtonDown("Tab"))
			{
				RanksPressed();
			}
			else if (!InputManager.GetButton("Tab"))
			{
				RanksUnPressed();
			}
		}
		if (Boolean_4 && Boolean_5)
		{
			if ((Boolean_6 || Boolean_7) && Int32_2 == 0 && GameObject_1 != null)
			{
				Int32_2 = GameObject_1.GetComponent<NetworkStartTable>().PlayerCommandController_0.Int32_1;
			}
			if (Boolean_7 && gameObject_0 == null && Int32_2 != 0)
			{
				if (Int32_2 == 1)
				{
					gameObject_0 = GameObject.FindGameObjectWithTag("BazaZoneCommand1");
				}
				else
				{
					gameObject_0 = GameObject.FindGameObjectWithTag("BazaZoneCommand2");
				}
			}
			if (Boolean_7 && (FlagController_2 == null || FlagController_3 == null) && Int32_2 != 0)
			{
				FlagController_2 = ((Int32_2 != 1) ? FlagController_1 : FlagController_0);
				FlagController_3 = ((Int32_2 != 1) ? FlagController_0 : FlagController_1);
			}
			if (Boolean_7 && FlagController_2 != null && FlagController_3 != null)
			{
				if (!FlagController_2.Boolean_0 && !FlagController_2.Boolean_1 && Vector3.SqrMagnitude(myPlayerTransform.position - FlagController_2.transform.position) < 2.25f)
				{
					PhotonView_0.RPC("SendSystemMessegeFromFlagReturnedRPC", PhotonTargets.All, FlagController_2.isBlue);
					FlagController_2.GoBaza();
				}
				GameObject gameObject = FlagController_3.GetComponent<FlagController>().GameObject_0;
				if (!FlagController_3.Boolean_0 && !Boolean_20 && gameObject != null && gameObject.activeSelf && Vector3.SqrMagnitude(myPlayerTransform.position - FlagController_3.transform.position) < 2.25f)
				{
					FlagController_3.SetCapture(PhotonView_0.int_1);
					Boolean_15 = true;
					PhotonView_0.RPC("SendSystemMessegeFromFlagCaptureRPC", PhotonTargets.All, FlagController_3.isBlue, mySkinName.NickName);
				}
			}
			if (Boolean_15 && FlagController_2.Boolean_1 && Vector3.SqrMagnitude(myPlayerTransform.position - gameObject_0.transform.position) < 2.25f)
			{
				if (Defs.Boolean_0)
				{
					base.GetComponent<AudioSource>().PlayOneShot(flagScoreMyCommandClip);
				}
				PlayerScoreController_0.AddFlag();
				Int32_5++;
				PlayerScoreController_0.AddScoreOnEvent((Int32_5 == 3) ? KillStreakType.KILLSTREAK_FLAGTOUCH_3 : ((Int32_5 != 2) ? KillStreakType.KILLSTREAK_FLAGTOUCH : KillStreakType.KILLSTREAK_FLAGTOUCH_2));
				Boolean_15 = false;
				PhotonView_0.RPC("SendSystemMessegeFromFlagAddScoreRPC", PhotonTargets.Others, !FlagController_3.isBlue, mySkinName.NickName);
				AddSystemMessage(LocalizationStore.Get("Key_1003"));
				FlagController_3.GoBaza();
			}
			UpdateFlagState();
		}
		if (Single_2 > 0f)
		{
			Single_2 -= Time.deltaTime;
		}
		if (Single_4 > 0f)
		{
			Single_4 -= Time.deltaTime;
		}
		if (Single_3 > 0f)
		{
			Single_3 -= Time.deltaTime;
		}
		if (Single_5 > 0f)
		{
			Single_5 -= Time.deltaTime;
		}
		if (!Boolean_4 || Boolean_5)
		{
			Weapon weaponFromCurrentSlot = WeaponManager_0.GetWeaponFromCurrentSlot();
			if (PlayerMechController_0.Boolean_1 && weaponFromCurrentSlot != null && weaponFromCurrentSlot.Int32_1 == 0 && weaponFromCurrentSlot.Int32_0 > 0 && !WeaponManager_0.WeaponSounds_0.GameObject_0.GetComponent<Animation>().IsPlaying("Shoot") && !Boolean_10 && !Boolean_3 && !playerStateController_0.Boolean_3)
			{
				ReloadPressed();
			}
		}
		float_1 += Time.deltaTime;
		if ((Boolean_6 || Boolean_7) && Int32_2 == 0 && GameObject_1 != null)
		{
			Int32_2 = GameObject_1.GetComponent<NetworkStartTable>().PlayerCommandController_0.Int32_1;
		}
		slideScroll();
		if (Single_6[0] > 0f)
		{
			Single_6[0] -= Time.deltaTime;
		}
		if (Single_6[1] > 0f)
		{
			Single_6[1] -= Time.deltaTime;
		}
		if (Single_6[2] > 0f)
		{
			Single_6[2] -= Time.deltaTime;
		}
		UpdateMouseWhell();
		if ((!Boolean_4 || Boolean_5) && PlayerParametersController_0.Single_2 <= 0f && !Boolean_20 && !Boolean_19 && (pauser_0 == null || !pauser_0.Boolean_0))
		{
			deathProcess();
		}
	}

	private void UpdateFlagState()
	{
		if (!Boolean_7 || !(HeadUpDisplay.HeadUpDisplay_0 != null))
		{
			return;
		}
		if (Boolean_15)
		{
			if (!HeadUpDisplay.HeadUpDisplay_0.flagRedCaptureTexture.activeSelf)
			{
				HeadUpDisplay.HeadUpDisplay_0.flagRedCaptureTexture.SetActive(true);
			}
		}
		else if (HeadUpDisplay.HeadUpDisplay_0.flagRedCaptureTexture.activeSelf)
		{
			HeadUpDisplay.HeadUpDisplay_0.flagRedCaptureTexture.SetActive(false);
		}
	}

	private void UpdateMouseWhell()
	{
		if (TutorialController.TutorialController_0.Boolean_0 || (Boolean_4 && !Boolean_5) || (pauser_0 != null && pauser_0.Boolean_0) || !Screen.lockCursor || Defs.bool_11)
		{
			return;
		}
		Weapon weapon = null;
		if (InputManager.GetButtonUp("WeaponChange"))
		{
			weapon = WeaponManager_0.GetWeaponFromSlot(WeaponManager_0.nextWeaponSlot);
		}
		else
		{
			float axis = InputManager.GetAxis("Mouse ScrollWheel");
			float time = Time.time;
			if (time < float_4)
			{
				if (axis != 0f)
				{
					float_5 = axis;
				}
				return;
			}
			float_4 = time + 0.35f;
			if (axis != 0f)
			{
				float_5 = axis;
			}
			if (float_5 == 0f)
			{
				return;
			}
			float_5 = ((axis == 0f) ? float_5 : axis);
			if (float_5 > 0f)
			{
				weapon = WeaponManager_0.GetPrevNextWeapon();
			}
			else if (float_5 < 0f)
			{
				weapon = WeaponManager_0.GetPrevNextWeapon(true);
			}
			float_5 = 0f;
		}
		if (HeadUpDisplay.HeadUpDisplay_0 != null && weapon != null && weapon.WeaponSounds_0 != null && weapon.WeaponSounds_0.WeaponData_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.SelectWeaponFromCategory(weapon.WeaponSounds_0.WeaponData_0.SlotType_0);
		}
		else if (weapon == null)
		{
			Log.AddLine("Player_move_c::UpdateMouseWhell() weapon == null", Log.LogLevel.WARNING);
		}
		else if (weapon.WeaponSounds_0 == null)
		{
			Log.AddLine("Player_move_c::UpdateMouseWhell() weapon.weaponModel == null", Log.LogLevel.WARNING);
		}
		else if (weapon.WeaponSounds_0.WeaponData_0 == null)
		{
			Log.AddLine("Player_move_c::UpdateMouseWhell() weapon.weaponModel.weaponData == null", Log.LogLevel.WARNING);
		}
	}

	private void writeLog(string string_3)
	{
	}

	private void deathProcess()
	{
		writeLog("[Player_move_c::deathProcess] Start");
		Int32_5 = 0;
		if (Defs.bool_2 && !Defs.bool_4)
		{
			if (list_0.Count > 0)
			{
				PhotonView_0.RPC("AddScoreKillAssisit", PhotonTargets.Others, (list_0.Count > 0) ? list_0[0] : 0, (list_0.Count > 1) ? list_0[1] : 0, (list_0.Count > 2) ? list_0[2] : 0, (list_0.Count > 3) ? list_0[3] : 0, (list_0.Count > 4) ? list_0[4] : 0, (list_0.Count > 5) ? list_0[5] : 0, (list_0.Count > 6) ? list_0[6] : 0, (list_0.Count > 7) ? list_0[7] : 0);
			}
			list_0.Clear();
		}
		if (Defs.bool_4)
		{
			SendImKilled();
		}
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.ResetDamageTaken();
			if (PlayerScoreController_0 != null && PlayerScoreController_0.PlayerMessageQueueConroller_0 != null)
			{
				PlayerScoreController_0.PlayerMessageQueueConroller_0.Clear();
			}
		}
		if (!PlayerTurretController_0.Boolean_1)
		{
			PlayerTurretController_0.CancelTurret();
			if (HeadUpDisplay.HeadUpDisplay_0 != null)
			{
				HeadUpDisplay.HeadUpDisplay_0.HideTurretInterface();
			}
		}
		if (!PlayerGrenadeController_0.Boolean_0 || WeaponSounds_0.WeaponData_0.SlotType_0 == SlotType.SLOT_CONSUM_GRENADE)
		{
			writeLog("[Player_move_c::deathProcess] Error");
			PlayerGrenadeController_0.SetStateGrenadeNone();
			PlayerGrenadeController_0.CancelGrenadeProcessInvokes();
			ChangePreviousWeapon(false);
		}
		if (Boolean_16)
		{
			ZoomPress();
		}
		if (Boolean_5)
		{
			playerStateController_0.DispatchStartRespawn();
			Boolean_18 = false;
			BattleStatWindow.Hide();
		}
		if (Boolean_4)
		{
			if (Boolean_5 && GameObject_0 != null && (bool)GameObject_0)
			{
				ImpactReceiverTrampoline component = GameObject_0.GetComponent<ImpactReceiverTrampoline>();
				if (component != null)
				{
					UnityEngine.Object.Destroy(component);
				}
			}
			if (Boolean_7 && Boolean_15)
			{
				Boolean_15 = false;
				PhotonView_0.RPC("SendSystemMessegeFromFlagDroppedRPC", PhotonTargets.All, FlagController_3.isBlue, mySkinName.NickName);
				FlagController_3.SetNOCapture(flagPoint.transform.position, flagPoint.transform.rotation);
			}
			resetMultyKill();
			Boolean_20 = true;
			if (Defs.Boolean_0)
			{
				base.gameObject.GetComponent<AudioSource>().PlayOneShot(deadPlayerSound);
			}
			if (Boolean_5)
			{
				int artikulIdFromSlot = UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_CONSUM_GRENADE);
				UserArtikul userArtikulByArtikulId = UserController.UserController_0.GetUserArtikulByArtikulId(artikulIdFromSlot);
				if (userArtikulByArtikulId != null && userArtikulByArtikulId.int_1 == 0)
				{
					PhotonView_0.RPC("OnGrenadePickedUpAfterDeathRPC", PhotonTargets.All, (int)PhotonView_0.PhotonPlayer_0.Hashtable_0["uid"], 1);
				}
			}
			bool_1 = true;
			AutoFade.fadeKilled(0.5f, (!Boolean_26 || Defs.bool_11) ? 1.5f : 0.5f, 0.5f, Color.white);
			Invoke("setisDeadFrameFalse", 1f);
			StartCoroutine(FlashWhenDead());
			if (Defs.bool_11)
			{
				Defs.bool_11 = false;
				RespawnPlayer();
			}
			else
			{
				Vector3 localPosition = myPlayerTransform.localPosition;
				TweenParms p_parms = new TweenParms().Prop("localPosition", new Vector3(localPosition.x, 100f, localPosition.z)).Ease(EaseType.EaseInCubic).OnComplete((TweenDelegate.TweenCallback)delegate
				{
					myPlayerTransform.localPosition = new Vector3(0f, -1000f, 0f);
					playerStateController_0.DispatchEndRespawn();
					if (Boolean_26 && !Defs.bool_11)
					{
						SetMapCameraActive(true);
						StartCoroutine(KillCam());
					}
					else
					{
						Defs.bool_11 = false;
						RespawnPlayer();
					}
				});
				HOTween.To(myPlayerTransform, (!Boolean_26) ? 2f : 0.75f, p_parms);
			}
		}
		else
		{
			UserController.UserController_0.UserData_0.localUserData_0.Boolean_1 = true;
			MonoSingleton<FightController>.Prop_0.LeaveRoom();
		}
		writeLog("[Player_move_c::deathProcess] End");
	}

	private IEnumerator KillCam()
	{
		if (killerInfo_0 == null)
		{
			yield return null;
		}
		CameraSceneController.SetState(CameraSceneController.States.KillKam, killerInfo_0.transform_0);
		while (PauseBattleWindow.PauseBattleWindow_0 != null || BugReportWindow.BugReportWindow_0 != null)
		{
			yield return new WaitForEndOfFrame();
		}
		KillCamWindow.Show(new RespawnBattleWindowParams(killerInfo_0));
		if (KillCamWindow.KillCamWindow_0 != null)
		{
			KillCamWindow.KillCamWindow_0.gameObject_1.SetActive(false);
			KillCamWindow.KillCamWindow_0.gameObject_2.SetActive((!KillCamWindow.KillCamWindow_0.Boolean_1) ? true : false);
			float num = 0f;
			Defs.bool_11 = true;
			Player_move_c player_move_c = ((!(killerInfo_0.transform_0 == null)) ? killerInfo_0.transform_0.GetComponent<SkinName>().Player_move_c_0 : null);
			while (Defs.bool_11 && player_move_c != null && !player_move_c.Boolean_20)
			{
				player_move_c.NickLabelController_0.ResetTimeShow();
				yield return null;
				num += Time.deltaTime;
			}
			if (Defs.bool_11 && !KillCamWindow.KillCamWindow_0.Boolean_1)
			{
				KillCamWindow.KillCamWindow_0.gameObject_1.SetActive(true);
				KillCamWindow.KillCamWindow_0.gameObject_2.SetActive(false);
				KillCamWindow.KillCamWindow_0.ShowCharacter(KillerInfo_0);
			}
		}
		CameraSceneController.SetState(CameraSceneController.States.None);
	}

	private void SetMapCameraActive(bool bool_34)
	{
		Camera component = Initializer.Initializer_0.tc.GetComponent<Camera>();
		Camera camera = myCamera;
		component.gameObject.SetActive(bool_34);
		camera.gameObject.SetActive(!bool_34);
		Camera camera_ = ((!bool_34) ? camera : component);
		NickLabelController.camera_0 = camera_;
	}

	public void RespawnPlayer()
	{
		writeLog("[Player_move_c::RespawnPlayer] Start");
		SetMapCameraActive(false);
		killerInfo_0.Reset();
		Func<bool> func = (Func<bool>)(() => pauser_0 != null && pauser_0.Boolean_0);
		if (base.transform.parent == null)
		{
			Debug.Log("transform.parent == null");
			return;
		}
		myPlayerTransform.localScale = new Vector3(1f, 1f, 1f);
		myTransform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
		bool_1 = false;
		Boolean_22 = true;
		float_3 = float_2;
		SetNoKilled();
		if (WeaponManager_0.myPlayer == null)
		{
			Debug.Log("_weaponManager.myPlayer == null");
			return;
		}
		WeaponManager_0.myPlayer.GetComponent<SkinName>().camPlayer.transform.parent = WeaponManager_0.myPlayer.transform;
		Weapon weaponFromCurrentSlot = WeaponManager_0.GetWeaponFromCurrentSlot();
		if (weaponFromCurrentSlot != null && weaponFromCurrentSlot.Int32_1 == 0)
		{
			WeaponManager_0.StopReload();
			if (HeadUpDisplay.HeadUpDisplay_0 != null)
			{
				HeadUpDisplay.HeadUpDisplay_0.StopReloadAmmo();
			}
		}
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.BlinkNoAmmo(1);
		}
		BlinkReloadButton.bool_0 = false;
		PlayerParametersController_0.ResetHealthAndArmor();
		gameObject_2 = GameObject.FindGameObjectsWithTag(Boolean_8 ? "MultyPlayerCreateZoneCOOP" : (Boolean_6 ? ("MultyPlayerCreateZoneCommand" + Int32_2) : ((!Boolean_7) ? "MultyPlayerCreateZone" : ("MultyPlayerCreateZoneFlagCommand" + Int32_2))));
		GameObject gameObject = gameObject_2[UnityEngine.Random.Range(0, gameObject_2.Length - 1)];
		BoxCollider component = gameObject.GetComponent<BoxCollider>();
		Vector2 vector = new Vector2(component.size.x * gameObject.transform.localScale.x, component.size.z * gameObject.transform.localScale.z);
		Rect rect = new Rect(gameObject.transform.position.x - vector.x / 2f, gameObject.transform.position.z - vector.y / 2f, vector.x, vector.y);
		Vector3 position = new Vector3(rect.x + UnityEngine.Random.Range(0f, rect.width), gameObject.transform.position.y, rect.y + UnityEngine.Random.Range(0f, rect.height));
		Quaternion rotation = gameObject.transform.rotation;
		myPlayerTransform.position = position;
		myPlayerTransform.rotation = rotation;
		Vector3 eulerAngles = myCamera.transform.rotation.eulerAngles;
		myCamera.transform.rotation = Quaternion.Euler(0f, eulerAngles.y, eulerAngles.z);
		Invoke("ChangePositionAfterRespawn", 0.01f);
		ProvideAmmo(string.Empty, true);
		if (Boolean_16)
		{
			ZoomPress();
		}
		if (!PlayerGrenadeController_0.Boolean_0 || WeaponSounds_0.WeaponData_0.SlotType_0 == SlotType.SLOT_CONSUM_GRENADE)
		{
			writeLog("[Player_move_c::RespawnPlayer] Error");
			PlayerGrenadeController_0.SetStateGrenadeNone();
			ChangePreviousWeapon(false);
		}
		writeLog("[Player_move_c::RespawnPlayer] End");
	}

	[Obfuscation(Exclude = true)]
	private void SetNoKilled()
	{
		Boolean_20 = false;
		resetMultyKill();
	}

	[Obfuscation(Exclude = true)]
	private void ChangePositionAfterRespawn()
	{
		myPlayerTransform.position += Vector3.forward * 0.01f;
	}

	[RPC]
	private void AddScoreKillAssisit(int int_5, int int_6, int int_7, int int_8, int int_9, int int_10, int int_11, int int_12)
	{
		if (!(WeaponManager.weaponManager_0.myPlayerMoveC == null) && (int_5 == WeaponManager.weaponManager_0.myPlayerMoveC.Int32_1 || int_6 == WeaponManager.weaponManager_0.myPlayerMoveC.Int32_1 || int_7 == WeaponManager.weaponManager_0.myPlayerMoveC.Int32_1 || int_8 == WeaponManager.weaponManager_0.myPlayerMoveC.Int32_1 || int_9 == WeaponManager.weaponManager_0.myPlayerMoveC.Int32_1 || int_10 == WeaponManager.weaponManager_0.myPlayerMoveC.Int32_1 || int_11 == WeaponManager.weaponManager_0.myPlayerMoveC.Int32_1 || int_12 == WeaponManager.weaponManager_0.myPlayerMoveC.Int32_1))
		{
			WeaponManager.weaponManager_0.myPlayerMoveC.PlayerScoreController_0.AddScoreOnEvent(KillStreakType.KILL_ASSIST);
			MonoSingleton<FightController>.Prop_0.FightStatController_0.OnKillAssists((int)WeaponManager.weaponManager_0.myPlayerMoveC.PhotonView_0.PhotonPlayer_0.Hashtable_0["uid"]);
		}
	}

	public void SetPause(bool bool_34 = true)
	{
		if ((!Boolean_5 && Boolean_4) || pauser_0 == null)
		{
			return;
		}
		pauser_0.Boolean_0 = !pauser_0.Boolean_0;
		SetBlockKeyboardControl(pauser_0.Boolean_0, true);
		if (bool_34 && HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			if (pauser_0.Boolean_0)
			{
				mySkinName.SetAnim("Idle");
				if (!WeaponManager.weaponManager_0.myPlayerMoveC.PlayerGrenadeController_0.Boolean_0)
				{
					WeaponManager.weaponManager_0.myPlayerMoveC.PlayerGrenadeController_0.GrenadeFire();
				}
				PauseBattleWindow.Show();
			}
			else if (PauseBattleWindow.PauseBattleWindow_0 != null)
			{
				PauseBattleWindow.PauseBattleWindow_0.Hide();
			}
		}
		if (pauser_0.Boolean_0)
		{
			if (!Boolean_4)
			{
				Time.timeScale = 0f;
			}
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	public void PlayerBattleOver()
	{
		RanksUnPressed();
		if (KillCamWindow.KillCamWindow_0 != null && Boolean_5)
		{
			RespawnPlayer();
		}
	}

	private void ProvideAmmo(string string_3, bool bool_34 = false)
	{
		WeaponManager_0.SetMaxAmmoFrAllWeapons(bool_34);
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.BlinkNoAmmo(0);
		}
		BlinkReloadButton.bool_0 = false;
	}

	private void slideScroll()
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			bool_4 = true;
			vector2_1 = Input.GetTouch(0).position;
		}
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && bool_4)
		{
			Vector2 position = Input.GetTouch(0).position;
			vector2_0.x += vector2_1.x - position.x;
			vector2_1 = position;
		}
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && bool_4)
		{
			bool_4 = false;
		}
	}

	public void OnPhotonPlayerConnected(PhotonPlayer photonPlayer_0)
	{
		if ((bool)PhotonView_0 && PhotonView_0.Boolean_1)
		{
			PhotonView_0.RPC("SetInvisibleRPC", PhotonTargets.Others, Boolean_14);
			WeaponManager_0.GetWeaponFromCurrentSlot();
			PhotonView_0.RPC("SetWeaponRPC", PhotonTargets.Others, (!(WeaponSounds_0 == null)) ? WeaponSounds_0.WeaponData_0.Int32_0 : 0);
			if (Defs.bool_8)
			{
				PhotonView_0.RPC("SetJetpackEnabledRPC", PhotonTargets.Others, Defs.bool_8);
			}
			if (!PlayerMechController_0.Boolean_1)
			{
				PlayerMechController_0.ActiveteMechSendRPC();
			}
			PlayerPhotonSkillUpdater_0.ForceSendSkillsRpc(photonPlayer_0);
		}
	}

	public void SetInvisible(bool bool_34)
	{
		if (Boolean_4)
		{
			PhotonView_0.RPC("SetInvisibleRPC", PhotonTargets.All, bool_34);
		}
		else
		{
			SetInvisibleRPC(bool_34);
		}
	}

	[RPC]
	private void SetInvisibleRPC(bool bool_34)
	{
		Boolean_14 = bool_34;
		if (Boolean_4 && !Boolean_5)
		{
			if (!Boolean_14)
			{
				invisibleParticle.SetActive(false);
				if (!PlayerMechController_0.Boolean_1)
				{
					PlayerMechController_0.mechPoint.SetActive(true);
				}
				else
				{
					mySkinName.FPSplayerObject.SetActive(true);
				}
			}
			else
			{
				invisibleParticle.SetActive(true);
				mySkinName.FPSplayerObject.SetActive(false);
				PlayerMechController_0.mechPoint.SetActive(false);
			}
		}
		else
		{
			SetInVisibleShaders(Boolean_14);
		}
	}

	private void SetInVisibleShaders(bool bool_34)
	{
		GameObject gameObject = null;
		Transform transform = null;
		WeaponData weaponData_ = WeaponSounds_0.WeaponData_0;
		if (weaponData_ != null && weaponData_.SlotType_0 == SlotType.SLOT_CONSUM_GRENADE)
		{
			try
			{
				GameObject gameObject2 = WeaponSounds_0.GameObject_0;
				Transform child = gameObject2.transform.GetChild(0);
				transform = child.Find("Arms_Mesh");
			}
			catch (Exception exception_)
			{
				MonoSingleton<Log>.Prop_0.DumpError(exception_);
				transform = null;
			}
		}
		else
		{
			gameObject = WeaponSounds_0.GameObject_1;
			if (gameObject != null)
			{
				transform = gameObject.transform.parent;
			}
		}
		if (bool_34)
		{
			if (gameObject != null || transform != null)
			{
				int num = transform.GetComponent<Renderer>().materials.Length;
				int num2 = 0;
				if (gameObject != null)
				{
					num2 = ((gameObject.GetComponent<Renderer>() != null) ? gameObject.GetComponent<Renderer>().materials.Length : 0);
				}
				shader_0 = new Shader[num + num2];
				color_0 = new Color[num + num2];
				shader_0[0] = transform.GetComponent<Renderer>().material.shader;
				if (transform.GetComponent<Renderer>().material.HasProperty("_Color"))
				{
					color_0[0] = transform.GetComponent<Renderer>().material.color;
				}
				transform.GetComponent<Renderer>().material.shader = Shader.Find("Mobile/Diffuse-Color");
				transform.GetComponent<Renderer>().material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 0.5f));
				if (gameObject != null && gameObject.GetComponent<Renderer>() != null)
				{
					for (int i = 0; i < gameObject.GetComponent<Renderer>().materials.Length; i++)
					{
						shader_0[i + 1] = gameObject.GetComponent<Renderer>().materials[i].shader;
						if (gameObject.GetComponent<Renderer>().materials[i].HasProperty("_Color"))
						{
							color_0[i + 1] = gameObject.GetComponent<Renderer>().materials[i].color;
						}
						gameObject.GetComponent<Renderer>().materials[i].shader = Shader.Find("Mobile/Diffuse-Color");
						gameObject.GetComponent<Renderer>().materials[i].SetColor("_ColorRili", new Color(1f, 1f, 1f, 0.5f));
					}
				}
			}
			PlayerMechController_0.mechBodyRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 0.5f));
			PlayerMechController_0.mechHandRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 0.5f));
			PlayerMechController_0.mechGunRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 0.5f));
			return;
		}
		if (gameObject != null || transform != null)
		{
			transform.GetComponent<Renderer>().material.shader = shader_0[0];
			if (transform.GetComponent<Renderer>().material.HasProperty("_Color"))
			{
				transform.GetComponent<Renderer>().material.color = color_0[0];
			}
			if (transform.GetComponent<Renderer>().material.HasProperty("_ColorRili"))
			{
				transform.GetComponent<Renderer>().material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 1f));
			}
			if (gameObject != null && gameObject.GetComponent<Renderer>() != null)
			{
				for (int j = 0; j < gameObject.GetComponent<Renderer>().materials.Length; j++)
				{
					gameObject.GetComponent<Renderer>().materials[j].shader = shader_0[j + 1];
					if (gameObject.GetComponent<Renderer>().materials[j].HasProperty("_Color"))
					{
						gameObject.GetComponent<Renderer>().materials[j].color = color_0[j + 1];
					}
					if (gameObject.GetComponent<Renderer>().materials[j].HasProperty("_ColorRili"))
					{
						gameObject.GetComponent<Renderer>().materials[j].SetColor("_ColorRili", new Color(1f, 1f, 1f, 1f));
					}
				}
			}
		}
		PlayerMechController_0.mechBodyRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 1f));
		PlayerMechController_0.mechHandRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 1f));
		PlayerMechController_0.mechGunRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 1f));
	}

	private void UserSlotChanged(UsersData.EventData eventData_0)
	{
		if (!(mySkinName == null))
		{
			switch ((SlotType)eventData_0.int_0)
			{
			case SlotType.SLOT_WEAR_HAT:
				mySkinName.SetHat();
				break;
			case SlotType.SLOT_WEAR_ARMOR:
				mySkinName.SetArmor();
				break;
			case SlotType.SLOT_WEAR_CAPE:
				mySkinName.SetCape();
				break;
			case SlotType.SLOT_WEAR_BOOTS:
				mySkinName.SetBoots();
				break;
			case SlotType.SLOT_WEAR_SKIN:
				break;
			}
		}
	}

	public string GetTimeLeft()
	{
		float num = (float)GetTimeDown();
		int num2 = Mathf.FloorToInt(num / 60f);
		int num3 = Mathf.FloorToInt(num - (float)(Mathf.FloorToInt(num / 60f) * 60));
		return string.Format("{0}:{1:00}", num2, num3);
	}

	public double GetTimeDown()
	{
		double num = MonoSingleton<FightController>.Prop_0.FightTimeController_0.Double_0;
		if (num < 0.0)
		{
			num = 0.0;
		}
		return num;
	}

	[RPC]
	public void OnGrenadePickedUpAfterDeathRPC(int int_5, int int_6)
	{
		MonoSingleton<FightController>.Prop_0.FightStatController_0.OnPickedUpGrenade(int_5, int_6);
	}

	[RPC]
	public void StartHookRPC(Vector3 vector3_0, bool bool_34)
	{
		PlayerDamageController.PlayerDamageController_0.StartHookBullet(this, vector3_0, bool_34);
	}

	[RPC]
	public void EndHookRPC()
	{
		mySkinName.firstPersonControlSharp.SetState(FirstPersonPlayerController.State.Default);
	}

	public void ForceSuicide()
	{
		if (Boolean_4 && !Boolean_5)
		{
			return;
		}
		mySkinName.Boolean_1 = false;
		if (!Boolean_4)
		{
			if (PlayerParametersController_0.Single_2 > 0f)
			{
				PlayerParametersController_0.ForceKillPlayer();
				PlayerFlashController_0.FlashPlayer();
				UserController.UserController_0.UserData_0.localUserData_0.Boolean_0 = true;
			}
			return;
		}
		if (PlayerParametersController_0.Single_2 > 0f)
		{
			PlayerParametersController_0.ForceKillPlayer();
			PlayerScoreController_0.SelfKill();
			sendImDeath(mySkinName.NickName, (byte)Int32_2);
			if (Boolean_7 && Boolean_15)
			{
				FlagController_3.GoBaza();
				Boolean_15 = false;
				NetworkStartTable_0.SendSystemMessegeFromFlagReturned(FlagController_3.isBlue);
			}
		}
		SendImKilled();
	}
}
