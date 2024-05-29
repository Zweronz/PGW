using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using engine.helpers;

public class PersController : MonoBehaviour
{
	public enum PersMode
	{
		LOBBY = 0,
		SHOP = 1,
		LEVELUP = 2
	}

	private static int int_0 = 215;

	public GameObject persShadow;

	public Transform weapon;

	public Transform hat;

	public Transform armor;

	public Transform cape;

	public Transform boots;

	public Transform footLeftBone;

	public PersMode mode;

	public PersRotator persRotator;

	public bool DontCreateNickLabel;

	private GameObject gameObject_0;

	private GameObject gameObject_1;

	private NickLabelController nickLabelController_0;

	private AnimationClip animationClip_0;

	private SlotType slotType_0;

	private Shader shader_0;

	private Shader shader_1;

	private Shader shader_2;

	private int int_1;

	private bool bool_0;

	public int tryOnHatId;

	public int tryOnArmorId;

	public int tryOnBootsId;

	public int tryOnCapeId;

	private Dictionary<PersMode, string> dictionary_0 = new Dictionary<PersMode, string>
	{
		{
			PersMode.LOBBY,
			"Default"
		},
		{
			PersMode.SHOP,
			"NGUIRoot"
		},
		{
			PersMode.LEVELUP,
			"LevelUpWindowPers"
		}
	};

	public bool Boolean_0
	{
		get
		{
			return bool_0;
		}
	}

	public NickLabelController NickLabelController_0
	{
		get
		{
			return nickLabelController_0;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return base.isActiveAndEnabled;
		}
		set
		{
			base.gameObject.SetActive(value);
			NickLabelController nickLabelController = NickLabelController_0;
			if (nickLabelController != null)
			{
				NGUITools.SetActive(nickLabelController.gameObject, value);
			}
			GameObject gameObject = persShadow;
			if (gameObject != null)
			{
				gameObject.gameObject.SetActive(value);
			}
		}
	}

	private int Int32_0
	{
		get
		{
			string layerName = dictionary_0[mode];
			return LayerMask.NameToLayer(layerName);
		}
	}

	private void Awake()
	{
		InitShader();
	}

	private IEnumerator Start()
	{
		Init();
		if (!DontCreateNickLabel)
		{
			while (NickLabelStack.nickLabelStack_0 == null)
			{
				yield return null;
			}
			NickLabelController.camera_0 = Camera.main;
			nickLabelController_0 = NickLabelStack.nickLabelStack_0.GetNextCurrentLabel();
			nickLabelController_0.Transform_0 = base.transform;
			nickLabelController_0.Boolean_0 = true;
			nickLabelController_0.Boolean_3 = false;
			nickLabelController_0.Boolean_2 = false;
			nickLabelController_0.StartShow();
		}
		bool_0 = true;
	}

	private void OnDestroy()
	{
		UsersData.Unsubscribe(UsersData.EventType.SLOTS_CHANGED, OnUserSlotsChanged);
		UsersData.Unsubscribe(UsersData.EventType.SKINS_CHANGED, OnUserSlotsChanged);
		UsersData.Unsubscribe(UsersData.EventType.RESET_VISIBLE_ARMOR, ResetArmor);
		UsersData.Unsubscribe(UsersData.EventType.RESET_VISIBLE_HAT, ResetHat);
		UsersData.Unsubscribe(UsersData.EventType.RESET_VISIBLE_BOOTS, ResetBoots);
		UsersData.Unsubscribe(UsersData.EventType.RESET_VISIBLE_CAPE, ResetCape);
	}

	private void OnUserSlotsChanged(UsersData.EventData eventData_0)
	{
		InitInLobby();
	}

	private void Init()
	{
		switch (mode)
		{
		case PersMode.LOBBY:
			InitInLobby();
			break;
		case PersMode.SHOP:
			InitInShop();
			break;
		case PersMode.LEVELUP:
			InitInLevelup();
			break;
		}
		UsersData.Subscribe(UsersData.EventType.RESET_VISIBLE_ARMOR, ResetArmor);
		UsersData.Subscribe(UsersData.EventType.RESET_VISIBLE_HAT, ResetHat);
		UsersData.Subscribe(UsersData.EventType.RESET_VISIBLE_BOOTS, ResetBoots);
		UsersData.Subscribe(UsersData.EventType.RESET_VISIBLE_CAPE, ResetCape);
	}

	public void InitInLobby()
	{
		Reset();
		InitRandomWeapon();
		UsersData.Subscribe(UsersData.EventType.SLOTS_CHANGED, OnUserSlotsChanged);
		UsersData.Subscribe(UsersData.EventType.SKINS_CHANGED, OnUserSlotsChanged);
	}

	private void ResetArmor(UsersData.EventData eventData_0)
	{
		int num = 0;
		num = ((int_1 != 0) ? UserController.UserController_0.GetAnyUserArtikulIdFromSlot(SlotType.SLOT_WEAR_ARMOR, int_1) : UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAR_ARMOR));
		SetArmor(num);
	}

	private void ResetHat(UsersData.EventData eventData_0)
	{
		int num = 0;
		num = ((int_1 != 0) ? UserController.UserController_0.GetAnyUserArtikulIdFromSlot(SlotType.SLOT_WEAR_HAT, int_1) : UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAR_HAT));
		SetHat(num);
	}

	private void ResetBoots(UsersData.EventData eventData_0)
	{
		int num = 0;
		num = ((int_1 != 0) ? UserController.UserController_0.GetAnyUserArtikulIdFromSlot(SlotType.SLOT_WEAR_BOOTS, int_1) : UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAR_BOOTS));
		SetBoots(num);
	}

	private void ResetCape(UsersData.EventData eventData_0)
	{
		int num = 0;
		num = ((int_1 != 0) ? UserController.UserController_0.GetAnyUserArtikulIdFromSlot(SlotType.SLOT_WEAR_CAPE, int_1) : UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAR_CAPE));
		SetCape(num);
	}

	private void InitInShop()
	{
		shader_2 = shader_1;
		int artikulIdFromSlot = UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAPON_PRIMARY);
		if (artikulIdFromSlot != 0)
		{
			SetWeapon(artikulIdFromSlot);
		}
		else
		{
			InitRandomWeapon();
		}
		SetSkin(SkinsController.Int32_0);
		SetHat(UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAR_HAT));
		SetArmor(UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAR_ARMOR));
		SetCape(UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAR_CAPE));
		SetBoots(UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAR_BOOTS));
	}

	private void InitInLevelup()
	{
		shader_2 = shader_1;
		Reset();
		SetWeapon(int_0);
	}

	private void InitShader()
	{
		shader_0 = Shader.Find("Mobile/Diffuse");
		shader_2 = shader_0;
		shader_1 = Shader.Find("Unlit/Texture");
	}

	private void InitRandomWeapon()
	{
		int num = 0;
		List<UserArtikul> list = null;
		list = ((int_1 != 0) ? WeaponController.WeaponController_0.GetAllWeaponsFormIventoryOtherUser(int_1) : WeaponController.WeaponController_0.GetAllWeaponsFormIventory());
		if (list.Count != 0)
		{
			num = list[UnityEngine.Random.Range(0, list.Count)].int_0;
		}
		else
		{
			Weapon randomWeaponFromAvailableWeapons = WeaponManager.weaponManager_0.GetRandomWeaponFromAvailableWeapons();
			num = randomWeaponFromAvailableWeapons.WeaponSounds_0.WeaponData_0.Int32_0;
		}
		SetWeapon(num);
	}

	public void ReinitLobbyToProfile(int int_2)
	{
		int_1 = int_2;
		Reset();
		InitRandomWeapon();
		UsersData.Unsubscribe(UsersData.EventType.SLOTS_CHANGED, OnUserSlotsChanged);
		UsersData.Unsubscribe(UsersData.EventType.SKINS_CHANGED, OnUserSlotsChanged);
		UsersData.Unsubscribe(UsersData.EventType.RESET_VISIBLE_ARMOR, ResetArmor);
		UsersData.Unsubscribe(UsersData.EventType.RESET_VISIBLE_HAT, ResetHat);
		UsersData.Unsubscribe(UsersData.EventType.RESET_VISIBLE_BOOTS, ResetBoots);
		UsersData.Unsubscribe(UsersData.EventType.RESET_VISIBLE_CAPE, ResetCape);
	}

	public void ReinitProfileToLobby()
	{
		int_1 = 0;
		Init();
	}

	public void SetItem(int int_2, bool bool_1 = false)
	{
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(int_2);
		if (artikul != null)
		{
			switch (artikul.SlotType_0)
			{
			case SlotType.SLOT_WEAPON_PRIMARY:
			case SlotType.SLOT_WEAPON_BACKUP:
			case SlotType.SLOT_WEAPON_MELEE:
			case SlotType.SLOT_WEAPON_SPECIAL:
			case SlotType.SLOT_WEAPON_PREMIUM:
			case SlotType.SLOT_WEAPON_SNIPER:
				slotType_0 = artikul.SlotType_0;
				SetWeapon(int_2);
				break;
			case SlotType.SLOT_WEAR_HAT:
				SetHat(int_2, bool_1);
				break;
			case SlotType.SLOT_WEAR_ARMOR:
				SetArmor(int_2, bool_1);
				break;
			case SlotType.SLOT_WEAR_SKIN:
				SetSkin(int_2);
				break;
			case SlotType.SLOT_WEAR_CAPE:
				SetCape(int_2, bool_1);
				break;
			case SlotType.SLOT_WEAR_BOOTS:
				SetBoots(int_2, bool_1);
				break;
			}
		}
	}

	public void Reset()
	{
		int artikulIdFromSlot = UserController.UserController_0.GetArtikulIdFromSlot(slotType_0);
		if (artikulIdFromSlot == 0)
		{
			SetWeapon(UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAPON_PRIMARY));
		}
		else
		{
			SetWeapon(artikulIdFromSlot);
		}
		tryOnArmorId = 0;
		tryOnBootsId = 0;
		tryOnCapeId = 0;
		tryOnHatId = 0;
		ClearHat();
		ClearArmor();
		ClearCape();
		ClearBoots();
		int num = 0;
		num = ((int_1 == 0 || int_1 == UserController.UserController_0.UserData_0.user_0.int_0) ? SkinsController.Int32_0 : SkinsController.GetOtherUserCurrentSkinId(int_1));
		SetSkin(num);
		if (persRotator != null)
		{
			persRotator.Reset();
		}
	}

	private void ClearSlot(SlotType slotType_1)
	{
		switch (slotType_1)
		{
		case SlotType.SLOT_WEAR_HAT:
			ClearHat();
			break;
		case SlotType.SLOT_WEAR_ARMOR:
			ClearArmor();
			break;
		case SlotType.SLOT_WEAR_CAPE:
			ClearCape();
			break;
		case SlotType.SLOT_WEAR_BOOTS:
			ClearBoots();
			break;
		case SlotType.SLOT_WEAR_SKIN:
			break;
		}
	}

	private void SetWeapon(int int_2)
	{
		GameObject gameObject = UserController.UserController_0.GetGameObject(int_2);
		if (gameObject == null)
		{
			Log.AddLine(string.Format("PersController::SetWeapon > no prefab for artikulId {0}", int_2), Log.LogLevel.WARNING);
			return;
		}
		ClearWeapon();
		gameObject_0 = UnityEngine.Object.Instantiate(gameObject) as GameObject;
		gameObject_0.transform.parent = weapon.transform;
		gameObject_0.transform.localPosition = Vector3.zero;
		gameObject_0.transform.localRotation = Quaternion.identity;
		gameObject_0.transform.localScale = Vector3.one;
		Utility.SetLayerRecursive(gameObject_0, Int32_0);
		gameObject_1 = gameObject_0.GetComponent<WeaponSounds>().GameObject_1;
		animationClip_0 = Resources.Load<AnimationClip>("ProfileAnimClips/" + gameObject.name + "_Profile");
		if (animationClip_0 != null)
		{
			gameObject_0.GetComponent<WeaponSounds>().GameObject_0.GetComponent<Animation>().AddClip(animationClip_0, "Profile");
			gameObject_0.GetComponent<WeaponSounds>().GameObject_0.GetComponent<Animation>().Play("Profile");
		}
		int num = 0;
		num = ((int_1 != 0) ? UserController.UserController_0.GetAnyUserArtikulIdFromSlot(SlotType.SLOT_WEAR_ARMOR, int_1) : UserController.UserController_0.GetArtikulIdFromSlot(SlotType.SLOT_WEAR_ARMOR));
		if (num > 0)
		{
			SetArmor(num);
		}
		else
		{
			ClearArmor();
		}
		int num2 = 0;
		num2 = ((int_1 == 0 || int_1 == UserController.UserController_0.UserData_0.user_0.int_0) ? SkinsController.Int32_0 : SkinsController.GetOtherUserCurrentSkinId(int_1));
		SetSkin(num2);
		Utility.SetShaderRecursive(gameObject_0.transform, shader_2);
	}

	private void ClearWeapon()
	{
		if (gameObject_0 != null)
		{
			gameObject_0.transform.parent = null;
			UnityEngine.Object.Destroy(gameObject_0);
		}
	}

	private void SetHat(int int_2, bool bool_1 = false)
	{
		if (bool_1)
		{
			tryOnHatId = int_2;
		}
		GameObject gameObject = UserController.UserController_0.GetGameObject((int_2 <= 0) ? tryOnHatId : int_2);
		if (gameObject == null)
		{
			if (int_2 != 0)
			{
				Log.AddLine(string.Format("PersController::SetHat > no prefab for artikulId {0}", int_2), Log.LogLevel.WARNING);
			}
			return;
		}
		ClearHat();
		GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, Vector3.zero, Quaternion.identity) as GameObject;
		Utility.SetLayerRecursive(gameObject2, Int32_0);
		gameObject2.transform.parent = hat;
		gameObject2.transform.localPosition = Vector3.zero;
		gameObject2.transform.localRotation = Quaternion.identity;
		gameObject2.transform.localScale = Vector3.one;
		if (!HideStuffSettings.HideStuffSettings_0.GetShowHat() && mode != PersMode.LEVELUP && int_1 == 0)
		{
			Utility.SetAlphaToGameObject(gameObject2, 0.3f);
		}
		else
		{
			Utility.SetShaderRecursive(gameObject2.transform, shader_2);
		}
	}

	private void ClearHat()
	{
		foreach (Transform item in hat)
		{
			item.parent = null;
			UnityEngine.Object.Destroy(item.gameObject);
		}
	}

	private void SetArmor(int int_2, bool bool_1 = false)
	{
		if (bool_1)
		{
			tryOnArmorId = int_2;
		}
		GameObject gameObject = UserController.UserController_0.GetGameObject((int_2 <= 0) ? tryOnArmorId : int_2);
		if (gameObject == null)
		{
			if (int_2 != 0)
			{
				Log.AddLine(string.Format("PersController::SetArmor > no prefab for artikulId {0}", int_2), Log.LogLevel.WARNING);
			}
			return;
		}
		ClearArmor();
		GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, Vector3.zero, Quaternion.identity) as GameObject;
		Utility.SetLayerRecursive(gameObject2, Int32_0);
		ArmorRefs component = gameObject2.transform.GetChild(0).GetComponent<ArmorRefs>();
		if (component != null && gameObject_0 != null)
		{
			WeaponSounds component2 = gameObject_0.GetComponent<WeaponSounds>();
			if (component2 != null && component.leftBone != null && component2.Transform_0 != null)
			{
				component.leftBone.parent = component2.Transform_0;
				component.leftBone.localPosition = Vector3.zero;
				component.leftBone.localRotation = Quaternion.identity;
				component.leftBone.localScale = new Vector3(1f, 1f, 1f);
			}
			if (component2 != null && component.rightBone != null && component2.Transform_1 != null)
			{
				component.rightBone.parent = component2.Transform_1;
				component.rightBone.localPosition = Vector3.zero;
				component.rightBone.localRotation = Quaternion.identity;
				component.rightBone.localScale = new Vector3(1f, 1f, 1f);
			}
		}
		gameObject2.transform.parent = armor;
		gameObject2.transform.localPosition = Vector3.zero;
		gameObject2.transform.localRotation = Quaternion.identity;
		gameObject2.transform.localScale = Vector3.one;
		if (!HideStuffSettings.HideStuffSettings_0.GetShowArmor() && mode != PersMode.LEVELUP && int_1 == 0)
		{
			Utility.SetAlphaToGameObject(gameObject2, 0.3f);
		}
		else
		{
			Utility.SetShaderRecursive(gameObject2.transform, shader_2);
		}
	}

	private void ClearArmor()
	{
		foreach (Transform item in armor)
		{
			Transform child = item.GetChild(0);
			ArmorRefs component = child.GetComponent<ArmorRefs>();
			if (component != null)
			{
				if (component.leftBone != null)
				{
					component.leftBone.parent = child;
				}
				if (component.rightBone != null)
				{
					component.rightBone.parent = child;
				}
			}
			item.parent = null;
			UnityEngine.Object.Destroy(item.gameObject);
		}
	}

	private void SetCape(int int_2, bool bool_1 = false)
	{
		if (bool_1)
		{
			tryOnCapeId = int_2;
		}
		GameObject gameObject = UserController.UserController_0.GetGameObject((int_2 <= 0) ? tryOnCapeId : int_2);
		if (gameObject == null)
		{
			if (int_2 != 0)
			{
				Log.AddLine(string.Format("PersController::SetCape > no prefab for artikulId {0}", int_2), Log.LogLevel.WARNING);
			}
			return;
		}
		ClearCape();
		GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, Vector3.zero, Quaternion.identity) as GameObject;
		Utility.SetLayerRecursive(gameObject2, Int32_0);
		gameObject2.transform.parent = cape;
		gameObject2.transform.localPosition = new Vector3(0f, -0.8f, 0f);
		gameObject2.transform.localRotation = Quaternion.identity;
		gameObject2.transform.localScale = Vector3.one;
		gameObject2.GetComponent<Animation>().Play("Profile");
		WearData wearData_ = SkinsController.WearData_1;
		if (wearData_ != null && wearData_.Boolean_0)
		{
			string s = SkinsController.StringFromTexture(SkinsController.GetCapeTexture(wearData_.Int32_0));
			byte[] data = Convert.FromBase64String(s);
			Texture2D texture2D = new Texture2D(12, 16);
			texture2D.LoadImage(data);
			texture2D.filterMode = FilterMode.Point;
			texture2D.Apply();
			if (wearData_.Boolean_0)
			{
				SkinsController.SetTextureRecursivelyFrom(gameObject2, texture2D);
			}
		}
		if (!HideStuffSettings.HideStuffSettings_0.GetShowCape() && mode != PersMode.LEVELUP && int_1 == 0)
		{
			Utility.SetAlphaToGameObject(gameObject2, 0.3f);
		}
		else
		{
			Utility.SetShaderRecursive(gameObject2.transform, shader_2);
		}
	}

	private void ClearCape()
	{
		foreach (Transform item in cape)
		{
			item.parent = null;
			UnityEngine.Object.Destroy(item.gameObject);
		}
	}

	private void SetBoots(int int_2, bool bool_1 = false)
	{
		if (bool_1)
		{
			tryOnBootsId = int_2;
		}
		GameObject gameObject = UserController.UserController_0.GetGameObject((int_2 <= 0) ? tryOnBootsId : int_2);
		if (gameObject == null)
		{
			if (int_2 != 0)
			{
				Log.AddLine(string.Format("PersController::SetBoots > no prefab for artikulId {0}", int_2), Log.LogLevel.WARNING);
			}
			return;
		}
		ClearBoots();
		foreach (Transform boot in boots)
		{
			if (boot.gameObject.name.Equals(gameObject.name))
			{
				boot.gameObject.SetActive(true);
			}
			else
			{
				boot.gameObject.SetActive(false);
			}
		}
		if (!HideStuffSettings.HideStuffSettings_0.GetShowBoots() && mode != PersMode.LEVELUP && int_1 == 0)
		{
			Utility.SetAlphaToGameObject(boots.gameObject, 0.3f);
		}
		else
		{
			Utility.SetShaderRecursive(boots.transform, shader_2);
		}
	}

	private void ClearBoots()
	{
		foreach (Transform boot in boots)
		{
			boot.gameObject.SetActive(false);
		}
	}

	private void SetSkin(int int_2)
	{
		Texture texture = null;
		texture = ((int_1 == 0 || int_1 == UserController.UserController_0.UserData_0.user_0.int_0) ? SkinsController.GetSkinTexture(int_2) : SkinsController.GetOtherUserSkinTexture(int_2, int_1));
		if (texture == null)
		{
			if (int_2 != 0)
			{
				Log.AddLine(string.Format("PersController::SetSkin > no texture for artikulId {0}", int_2), Log.LogLevel.WARNING);
			}
			return;
		}
		texture.filterMode = FilterMode.Point;
		GameObject[] collection = new GameObject[5] { gameObject_1, cape.gameObject, hat.gameObject, boots.gameObject, armor.gameObject };
		List<GameObject> list = new List<GameObject>(collection);
		if (gameObject_0 != null)
		{
			WeaponSounds component = gameObject_0.GetComponent<WeaponSounds>();
			if (component.Transform_0 != null)
			{
				list.Add(component.Transform_0.gameObject);
			}
			if (component.Transform_1 != null)
			{
				list.Add(component.Transform_1.gameObject);
			}
			if (component.Transform_2 != null)
			{
				list.Add(component.Transform_2.gameObject);
			}
			if (component.GameObject_2 != null)
			{
				list.Add(component.GameObject_2);
			}
		}
		Utility.SetTextureRecursiveFrom(base.gameObject, texture, list.ToArray());
		Utility.SetShaderRecursive(base.transform, shader_2);
		ResetHat(null);
		ResetArmor(null);
		ResetCape(null);
		ResetBoots(null);
	}

	private void ClearSkin()
	{
	}

	public void ResetTryOnStuffs()
	{
		tryOnArmorId = 0;
		tryOnHatId = 0;
		tryOnBootsId = 0;
		tryOnCapeId = 0;
	}
}
