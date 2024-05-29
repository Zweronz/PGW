using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.helpers;

public sealed class SkinName : MonoBehaviour
{
	public const string string_0 = "Idle";

	public const string string_1 = "Walk";

	public const string string_2 = "Jump";

	public const string string_3 = "Dead";

	public const string string_4 = "Walk_Left";

	public const string string_5 = "Walk_Right";

	public const string string_6 = "Walk_Back";

	public const string string_7 = "Jetpack_Idle";

	public const string string_8 = "Jetpack_Run_Front";

	public const string string_9 = "Jetpack_Run_Back";

	public const string string_10 = "Jetpack_Run_Righte";

	public const string string_11 = "Jetpack_Run_Left";

	private static Dictionary<string, byte> dictionary_0 = new Dictionary<string, byte>
	{
		{ "Idle", 0 },
		{ "Walk", 1 },
		{ "Jump", 2 },
		{ "Dead", 3 },
		{ "Walk_Left", 4 },
		{ "Walk_Right", 5 },
		{ "Walk_Back", 6 },
		{ "Jetpack_Idle", 7 },
		{ "Jetpack_Run_Front", 8 },
		{ "Jetpack_Run_Back", 9 },
		{ "Jetpack_Run_Righte", 10 },
		{ "Jetpack_Run_Left", 11 }
	};

	public GameObject playerGameObject;

	public GameObject hatsPoint;

	public GameObject capesPoint;

	public GameObject bootsPoint;

	public GameObject armorPoint;

	public GameObject camPlayer;

	public GameObject headObj;

	public GameObject bodyLayer;

	public AudioClip walkAudio;

	public AudioClip jumpAudio;

	public AudioClip jumpDownAudio;

	public GameObject FPSplayerObject;

	public ThirdPersonNetwork1 interpolateScript;

	public FirstPersonPlayerController firstPersonControlSharp;

	public string NickName;

	private string string_12 = "Idle";

	private ImpactReceiverTrampoline impactReceiverTrampoline_0;

	private bool bool_0;

	private int int_0;

	private int int_1;

	private int int_2;

	private int int_3;

	[CompilerGenerated]
	private Player_move_c player_move_c_0;

	[CompilerGenerated]
	private Texture texture_0;

	[CompilerGenerated]
	private bool bool_1;

	[CompilerGenerated]
	private CharacterController characterController_0;

	[CompilerGenerated]
	private bool bool_2;

	[CompilerGenerated]
	private PhotonView photonView_0;

	public Player_move_c Player_move_c_0
	{
		[CompilerGenerated]
		get
		{
			return player_move_c_0;
		}
		[CompilerGenerated]
		private set
		{
			player_move_c_0 = value;
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
		private set
		{
			texture_0 = value;
		}
	}

	public int Int32_0
	{
		get
		{
			return int_0;
		}
		private set
		{
			if (int_0 != value && Player_move_c_0 != null && Player_move_c_0.PlayerFlashController_0 != null)
			{
				Player_move_c_0.PlayerFlashController_0.NeedUpdateTextures();
			}
			int_0 = value;
		}
	}

	public int Int32_1
	{
		get
		{
			return int_1;
		}
		private set
		{
			if (int_1 != value && Player_move_c_0 != null && Player_move_c_0.PlayerFlashController_0 != null)
			{
				Player_move_c_0.PlayerFlashController_0.NeedUpdateTextures();
			}
			int_1 = value;
		}
	}

	public int Int32_2
	{
		get
		{
			return int_2;
		}
		private set
		{
			if (int_2 != value && Player_move_c_0 != null && Player_move_c_0.PlayerFlashController_0 != null)
			{
				Player_move_c_0.PlayerFlashController_0.NeedUpdateTextures();
			}
			int_2 = value;
		}
	}

	public int Int32_3
	{
		get
		{
			return int_3;
		}
		private set
		{
			if (int_3 != value && Player_move_c_0 != null && Player_move_c_0.PlayerFlashController_0 != null)
			{
				Player_move_c_0.PlayerFlashController_0.NeedUpdateTextures();
			}
			int_3 = value;
		}
	}

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_1;
		}
		[CompilerGenerated]
		private set
		{
			bool_1 = value;
		}
	}

	public CharacterController CharacterController_0
	{
		[CompilerGenerated]
		get
		{
			return characterController_0;
		}
		[CompilerGenerated]
		private set
		{
			characterController_0 = value;
		}
	}

	public bool Boolean_1
	{
		[CompilerGenerated]
		get
		{
			return bool_2;
		}
		[CompilerGenerated]
		set
		{
			bool_2 = value;
		}
	}

	private PhotonView PhotonView_0
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

	public static byte GetAnimIntByString(string string_13)
	{
		if (dictionary_0.ContainsKey(string_13))
		{
			return dictionary_0[string_13];
		}
		return 0;
	}

	public static string GetAnimStringByInt(byte byte_0)
	{
		foreach (KeyValuePair<string, byte> item in dictionary_0)
		{
			if (item.Value == byte_0)
			{
				return item.Key;
			}
		}
		return "Idle";
	}

	private void Awake()
	{
		Player_move_c_0 = playerGameObject.GetComponent<Player_move_c>();
		CharacterController_0 = base.transform.GetComponent<CharacterController>();
		PhotonView_0 = PhotonView.Get(this);
	}

	private void OnEnable()
	{
		Boolean_0 = ((UserController.UserController_0.GetIntSummModifier(SkillId.SKILL_STEALTH) != 0) ? true : false);
	}

	private void Start()
	{
		if (Defs.bool_2 && !PhotonView_0.Boolean_1)
		{
			camPlayer.SetActive(false);
			CharacterController_0.enabled = false;
			ClearBoots();
		}
		else
		{
			FPSplayerObject.SetActive(false);
		}
		if (!Defs.bool_2 || PhotonView_0.Boolean_1)
		{
			base.gameObject.layer = LayerMask.NameToLayer("Player");
			bodyLayer.layer = LayerMask.NameToLayer("Player");
			headObj.layer = LayerMask.NameToLayer("Player");
		}
		if (Defs.bool_2 && !PhotonView_0.Boolean_1)
		{
			base.gameObject.layer = LayerMask.NameToLayer("Enemy");
			bodyLayer.layer = LayerMask.NameToLayer("Enemy");
			headObj.layer = LayerMask.NameToLayer("Enemy");
			bodyLayer.GetComponent<CapsuleCollider>().isTrigger = false;
			headObj.GetComponent<CapsuleCollider>().isTrigger = false;
		}
		if (Defs.bool_2 && PhotonView_0.Boolean_1)
		{
			SetCape();
			SetHat();
			SetBoots();
			SetArmor();
		}
	}

	private void Update()
	{
		base.GetComponent<AudioSource>().volume = ((!Defs.Boolean_0) ? 0f : 1f);
		if (Defs.bool_2 && !PhotonView_0.Boolean_1)
		{
			return;
		}
		if (Player_move_c_0.Boolean_20)
		{
			Boolean_1 = false;
		}
		if (CharacterController_0.velocity.y < -2.5f && !CharacterController_0.isGrounded)
		{
			Boolean_1 = true;
		}
		if (Boolean_1 && CharacterController_0.isGrounded)
		{
			if (Defs.Boolean_0)
			{
				NGUITools.PlaySound(jumpDownAudio);
			}
			Boolean_1 = false;
		}
	}

	public void SetAnim(string string_13, bool bool_3)
	{
		if (string_12.Contains(string_13))
		{
			return;
		}
		string_12 = string_13;
		interpolateScript.string_0 = string_12;
		interpolateScript.bool_3 = Boolean_0;
		if (string_12.Contains("Walk"))
		{
			base.GetComponent<AudioSource>().loop = true;
			base.GetComponent<AudioSource>().clip = walkAudio;
			if (!bool_3)
			{
				base.GetComponent<AudioSource>().Play();
			}
		}
		else
		{
			base.GetComponent<AudioSource>().Stop();
		}
		if (PhotonView_0.Boolean_1)
		{
			return;
		}
		if (!Player_move_c_0.PlayerMechController_0.Boolean_1)
		{
			Player_move_c_0.PlayerMechController_0.PlayBodyAnimation(string_12);
		}
		if (FPSplayerObject.GetComponent<Animation>().GetClip(string_12) != null)
		{
			FPSplayerObject.GetComponent<Animation>().Play(string_12);
		}
		else
		{
			Log.AddLine(string.Format("[SkinName::SetAnim] no animation name = {0}  in FPSplayerObject", string_12), LogType.Error);
		}
		if (capesPoint.transform.childCount > 0)
		{
			Animation animation = capesPoint.transform.GetChild(0).GetComponent<Animation>();
			if (animation.GetClip(string_12) != null)
			{
				animation.Play(string_12);
			}
		}
	}

	public void SetAnim(string string_13)
	{
		SetAnim(string_13, Boolean_0);
	}

	public IEnumerator _SetAndResetImpactedByTrampoline()
	{
		bool_0 = true;
		yield return new WaitForSeconds(0.1f);
		bool_0 = false;
	}

	private void OnControllerColliderHit(ControllerColliderHit controllerColliderHit_0)
	{
		CollisionProccessDeadCollider(controllerColliderHit_0.gameObject, controllerColliderHit_0.transform);
		CollisionProccessTrampoline(controllerColliderHit_0.gameObject, controllerColliderHit_0.transform);
	}

	public void OnCollisionEnter(Collision collision_0)
	{
	}

	public void OnTriggerEnter(Collider collider_0)
	{
		CollisionProccessDeadCollider(collider_0.gameObject, collider_0.transform);
	}

	private void CollisionProccessDeadCollider(GameObject gameObject_0, Transform transform_0)
	{
		if (gameObject_0.name.Equals("DeadCollider"))
		{
			Player_move_c_0.ForceSuicide();
		}
	}

	private void CollisionProccessTrampoline(GameObject gameObject_0, Transform transform_0)
	{
		if ((!Defs.bool_2 || PhotonView_0.Boolean_1) && impactReceiverTrampoline_0 != null && !bool_0)
		{
			UnityEngine.Object.Destroy(impactReceiverTrampoline_0);
			impactReceiverTrampoline_0 = null;
		}
		if (!bool_0 && gameObject_0.CompareTag("Trampoline") && (!Defs.bool_2 || PhotonView_0.Boolean_1))
		{
			if (impactReceiverTrampoline_0 == null)
			{
				impactReceiverTrampoline_0 = base.gameObject.AddComponent<ImpactReceiverTrampoline>();
			}
			base.transform.up.Equals(transform_0.up);
			float float_ = 50f;
			Vector3 up = transform_0.up;
			impactReceiverTrampoline_0.AddImpact(up, float_);
			StartCoroutine(_SetAndResetImpactedByTrampoline());
		}
	}

	public void SetCape()
	{
		WearData wearData_ = SkinsController.WearData_1;
		if (wearData_ != null)
		{
			if (wearData_.Boolean_0)
			{
				string text = SkinsController.StringFromTexture(SkinsController.GetCapeTexture(wearData_.Int32_0));
				PhotonView_0.RPC("setCapeCustomRPC", PhotonTargets.Others, text, wearData_.Int32_0, HideStuffSettings.HideStuffSettings_0.GetShowCape());
			}
			else
			{
				PhotonView_0.RPC("setCapeRPC", PhotonTargets.Others, wearData_.Int32_0, HideStuffSettings.HideStuffSettings_0.GetShowCape());
			}
		}
	}

	public void SetArmor()
	{
		int artikulIdFromSlot = UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAR_ARMOR);
		if (artikulIdFromSlot != 0)
		{
			PhotonView_0.RPC("SetArmorVisInvisibleRPC", PhotonTargets.Others, artikulIdFromSlot, HideStuffSettings.HideStuffSettings_0.GetShowArmor());
		}
	}

	public void SetBoots()
	{
		int artikulIdFromSlot = UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAR_BOOTS);
		if (artikulIdFromSlot != 0)
		{
			PhotonView_0.RPC("setBootsRPC", PhotonTargets.Others, artikulIdFromSlot, HideStuffSettings.HideStuffSettings_0.GetShowBoots());
		}
	}

	public void SetHat()
	{
		int artikulIdFromSlot = UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAR_HAT);
		if (artikulIdFromSlot != 0)
		{
			PhotonView_0.RPC("SetHatWithInvisebleRPC", PhotonTargets.Others, artikulIdFromSlot, HideStuffSettings.HideStuffSettings_0.GetShowHat());
		}
	}

	public void ClearBoots()
	{
		if (!(bootsPoint == null) && Int32_3 == 0)
		{
			for (int i = 0; i < bootsPoint.transform.childCount; i++)
			{
				Transform child = bootsPoint.transform.GetChild(i);
				child.gameObject.SetActive(false);
			}
		}
	}

	public void OnPhotonPlayerConnected(PhotonPlayer photonPlayer_0)
	{
		if (PhotonView_0.Boolean_1)
		{
			SetHat();
			SetCape();
			SetBoots();
			SetArmor();
		}
	}

	[RPC]
	private void setCapeCustomRPC(string string_13, int int_4, bool bool_3)
	{
		byte[] data = Convert.FromBase64String(string_13);
		Texture2D texture2D = new Texture2D(12, 16);
		texture2D.LoadImage(data);
		texture2D.filterMode = FilterMode.Point;
		texture2D.Apply();
		Texture_0 = texture2D;
		Int32_2 = int_4;
		if (!bool_3)
		{
			return;
		}
		if (capesPoint.transform.childCount > 0)
		{
			for (int i = 0; i < capesPoint.transform.childCount; i++)
			{
				UnityEngine.Object.Destroy(capesPoint.transform.GetChild(i).gameObject);
			}
		}
		GameObject gameObject = UserController.UserController_0.GetGameObject(int_4);
		if (gameObject != null)
		{
			CustomCapePicker component = gameObject.GetComponent<CustomCapePicker>();
			if (component != null)
			{
				component.artikulId = 0;
				component.shouldLoadTexture = false;
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject) as GameObject;
			Transform transform = gameObject2.transform;
			transform.parent = capesPoint.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			SkinsController.SetTextureRecursivelyFrom(gameObject2, texture2D);
		}
	}

	[RPC]
	private void setCapeRPC(int int_4, bool bool_3)
	{
		Texture_0 = null;
		Int32_2 = int_4;
		if (!bool_3)
		{
			return;
		}
		if (capesPoint.transform.childCount > 0)
		{
			for (int i = 0; i < capesPoint.transform.childCount; i++)
			{
				UnityEngine.Object.Destroy(capesPoint.transform.GetChild(i).gameObject);
			}
		}
		GameObject gameObject = UserController.UserController_0.GetGameObject(int_4);
		if (!(gameObject == null))
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject) as GameObject;
			Transform transform = gameObject2.transform;
			transform.parent = capesPoint.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
		}
	}

	[RPC]
	private void SetArmorVisInvisibleRPC(int int_4, bool bool_3)
	{
		Int32_1 = int_4;
		if (!bool_3)
		{
			return;
		}
		if (armorPoint.transform.childCount > 0)
		{
			for (int i = 0; i < armorPoint.transform.childCount; i++)
			{
				UnityEngine.Object.Destroy(armorPoint.transform.GetChild(i).gameObject);
			}
		}
		GameObject gameObject = UserController.UserController_0.GetGameObject(Int32_1);
		if (!(gameObject == null))
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject) as GameObject;
			Transform transform = gameObject2.transform;
			transform.parent = armorPoint.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = new Vector3(1f, 1f, 1f);
			ArmorRefs component = transform.GetChild(0).GetComponent<ArmorRefs>();
			if (!(component == null) && !(Player_move_c_0 == null) && Player_move_c_0.transform.childCount != 0)
			{
				WeaponSounds component2 = Player_move_c_0.transform.GetChild(0).GetComponent<WeaponSounds>();
				component.leftBone.GetComponent<SetPosInArmor>().target = component2.Transform_0;
				component.rightBone.GetComponent<SetPosInArmor>().target = component2.Transform_1;
			}
		}
	}

	[RPC]
	private void setBootsRPC(int int_4, bool bool_3)
	{
		Int32_3 = int_4;
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(int_4);
		if (artikul == null)
		{
			return;
		}
		GameObject gameObject = UserController.UserController_0.GetGameObject(Int32_3);
		if (gameObject == null)
		{
			if (Int32_3 != 0)
			{
				Log.AddLine(string.Format("PersController::SetBoots > no prefab for artikulId {0}", Int32_3), Log.LogLevel.WARNING);
			}
			return;
		}
		ClearBoots();
		for (int i = 0; i < bootsPoint.transform.childCount; i++)
		{
			Transform child = bootsPoint.transform.GetChild(i);
			if (child.gameObject.name.Equals(gameObject.name))
			{
				child.gameObject.SetActive(bool_3);
				Player_move_c_0.PlayerFlashController_0.currentBoots = child.gameObject;
			}
			else
			{
				child.gameObject.SetActive(false);
			}
		}
	}

	[RPC]
	private void SetHatWithInvisebleRPC(int int_4, bool bool_3)
	{
		Int32_0 = int_4;
		if (!bool_3)
		{
			return;
		}
		if (hatsPoint.transform.childCount > 0)
		{
			for (int i = 0; i < hatsPoint.transform.childCount; i++)
			{
				UnityEngine.Object.Destroy(hatsPoint.transform.GetChild(i).gameObject);
			}
		}
		GameObject gameObject = UserController.UserController_0.GetGameObject(Int32_0);
		if (!(gameObject == null))
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject) as GameObject;
			Transform transform = gameObject2.transform;
			transform.parent = hatsPoint.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
		}
	}
}
