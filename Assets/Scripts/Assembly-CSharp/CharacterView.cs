using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.helpers;

public class CharacterView : MonoBehaviour
{
	public enum CharacterType
	{
		Player = 0,
		Mech = 1,
		Turret = 2
	}

	public Transform character;

	public Transform mech;

	public SkinnedMeshRenderer mechBodyRenderer;

	public SkinnedMeshRenderer mechHandRenderer;

	public SkinnedMeshRenderer mechGunRenderer;

	public Material[] mechGunMaterials;

	public Material[] mechBodyMaterials;

	public Transform turret;

	public Transform hatPoint;

	public Transform capePoint;

	public Transform bootsPoint;

	public Transform armorPoint;

	public Transform body;

	private AnimationCoroutineRunner animationCoroutineRunner_0;

	private AnimationClip animationClip_0;

	private GameObject gameObject_0;

	[CompilerGenerated]
	private static Action<Transform> action_0;

	[CompilerGenerated]
	private static Action<Transform> action_1;

	private AnimationCoroutineRunner AnimationCoroutineRunner_0
	{
		get
		{
			if (animationCoroutineRunner_0 == null)
			{
				animationCoroutineRunner_0 = GetComponent<AnimationCoroutineRunner>();
			}
			return animationCoroutineRunner_0;
		}
	}

	public void ShowCharacterType(CharacterType characterType_0, KillerInfo killerInfo_0)
	{
		character.gameObject.SetActive(false);
		if (mech != null)
		{
			mech.gameObject.SetActive(false);
		}
		if (turret != null)
		{
			turret.gameObject.SetActive(false);
		}
		switch (characterType_0)
		{
		case CharacterType.Player:
			character.gameObject.SetActive(true);
			break;
		case CharacterType.Mech:
			ShowMechCharacterType(killerInfo_0);
			break;
		case CharacterType.Turret:
			turret.gameObject.SetActive(true);
			break;
		}
	}

	private void ShowMechCharacterType(KillerInfo killerInfo_0)
	{
		if (!(mech == null))
		{
			mech.gameObject.SetActive(true);
			int num = Mathf.Clamp(ArtikulController.ArtikulController_0.GetDowngrades(killerInfo_0.int_0).Count, 0, 4);
			mechBodyRenderer.material = mechBodyMaterials[num];
			mechHandRenderer.material = mechBodyMaterials[num];
			mechGunRenderer.material = mechGunMaterials[num];
			mechBodyRenderer.material.SetColor("_ColorRili", Color.white);
			mechHandRenderer.material.SetColor("_ColorRili", Color.white);
		}
	}

	public void SetWeaponAndSkin(int int_0, Texture texture_0)
	{
		AnimationCoroutineRunner_0.StopAllCoroutines();
		if (armorPoint.childCount > 0)
		{
			ArmorRefs component = armorPoint.GetChild(0).GetChild(0).GetComponent<ArmorRefs>();
			if (component != null)
			{
				if (component.leftBone != null)
				{
					component.leftBone.parent = armorPoint.GetChild(0).GetChild(0);
				}
				if (component.rightBone != null)
				{
					component.rightBone.parent = armorPoint.GetChild(0).GetChild(0);
				}
			}
		}
		List<Transform> list = new List<Transform>();
		foreach (Transform item in body)
		{
			list.Add(item);
		}
		foreach (Transform item2 in list)
		{
			item2.parent = null;
			UnityEngine.Object.Destroy(item2.gameObject);
		}
		if (int_0 == 0)
		{
			return;
		}
		animationClip_0 = null;
		GameObject gameObject = UserController.UserController_0.GetGameObject(int_0);
		if (gameObject == null)
		{
			Log.AddLine("CharacterView::SetWeaponAndSkin. Weapon prefab == null. Weapon id = " + int_0);
			return;
		}
		animationClip_0 = Resources.Load<AnimationClip>("ProfileAnimClips/" + gameObject.name + "_Profile");
		gameObject_0 = UnityEngine.Object.Instantiate(gameObject) as GameObject;
		Player_move_c.PerformActionRecurs(gameObject_0, delegate(Transform transform_0)
		{
			MeshRenderer component4 = transform_0.GetComponent<MeshRenderer>();
			SkinnedMeshRenderer component5 = transform_0.GetComponent<SkinnedMeshRenderer>();
			if (component4 != null)
			{
				component4.useLightProbes = false;
			}
			if (component5 != null)
			{
				component5.useLightProbes = false;
			}
		});
		Player_move_c.SetLayerRecursively(gameObject_0, base.gameObject.layer);
		gameObject_0.transform.parent = body;
		gameObject_0.transform.localScale = new Vector3(1f, 1f, 1f);
		gameObject_0.transform.position = body.transform.position;
		gameObject_0.transform.localPosition = Vector3.zero;
		gameObject_0.transform.localRotation = Quaternion.identity;
		WeaponSounds component2 = gameObject_0.GetComponent<WeaponSounds>();
		component2.Init(int_0);
		if (armorPoint.childCount > 0 && component2 != null)
		{
			ArmorRefs component3 = armorPoint.GetChild(0).GetChild(0).GetComponent<ArmorRefs>();
			if (component3 != null)
			{
				if (component3.leftBone != null && component2.Transform_0 != null)
				{
					component3.leftBone.parent = component2.Transform_0;
					component3.leftBone.localPosition = Vector3.zero;
					component3.leftBone.localRotation = Quaternion.identity;
					component3.leftBone.localScale = new Vector3(1f, 1f, 1f);
				}
				if (component3.rightBone != null && component2.Transform_1 != null)
				{
					component3.rightBone.parent = component2.Transform_1;
					component3.rightBone.localPosition = Vector3.zero;
					component3.rightBone.localRotation = Quaternion.identity;
					component3.rightBone.localScale = new Vector3(1f, 1f, 1f);
				}
			}
		}
		PlayWeaponAnimation();
		SetSkinTexture(texture_0);
		if (component2 != null && component2.WeaponData_0.SlotType_0 == SlotType.SLOT_CONSUM_GRENADE)
		{
			SetupWeaponGrenade(gameObject_0);
		}
	}

	public void SetupWeaponGrenade(GameObject gameObject_1)
	{
		GameObject original = Resources.Load<GameObject>("Prefabs/Grenades/Rocket");
		Rocket component = (UnityEngine.Object.Instantiate(original, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<Rocket>();
		component.enabled = false;
		component.dontExecStart = true;
		component.GetComponent<Rigidbody>().useGravity = false;
		component.GetComponent<Rigidbody>().isKinematic = true;
		component.rockets[10].SetActive(true);
		Player_move_c.SetLayerRecursively(component.gameObject, base.gameObject.layer);
		component.transform.parent = gameObject_1.GetComponent<WeaponSounds>().Transform_2;
		component.transform.localPosition = Vector3.zero;
		component.transform.localRotation = Quaternion.identity;
		component.transform.localScale = Vector3.one;
	}

	public void SetSkinTexture(Texture texture_0)
	{
		if (!(texture_0 == null))
		{
			GameObject gameObject = ((body.transform.childCount <= 0) ? null : body.transform.GetChild(0).GetComponent<WeaponSounds>().GameObject_1);
			SkinsController.SetTextureRecursivelyFrom(character.gameObject, texture_0, new GameObject[5] { capePoint.gameObject, hatPoint.gameObject, bootsPoint.gameObject, armorPoint.gameObject, gameObject });
		}
	}

	public static void DisableLightProbesRecursively(GameObject gameObject_1)
	{
		Player_move_c.PerformActionRecurs(gameObject_1, delegate(Transform transform_0)
		{
			MeshRenderer component = transform_0.GetComponent<MeshRenderer>();
			SkinnedMeshRenderer component2 = transform_0.GetComponent<SkinnedMeshRenderer>();
			if (component != null)
			{
				component.useLightProbes = false;
			}
			if (component2 != null)
			{
				component2.useLightProbes = false;
			}
		});
	}

	public void UpdateHat(int int_0)
	{
		RemoveHat();
		GameObject gameObject = UserController.UserController_0.GetGameObject(int_0);
		if (!(gameObject == null))
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject) as GameObject;
			DisableLightProbesRecursively(gameObject2);
			Transform transform = gameObject2.transform;
			transform.parent = hatPoint.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = new Vector3(1f, 1f, 1f);
			Player_move_c.SetLayerRecursively(gameObject2, base.gameObject.layer);
		}
	}

	public void RemoveHat()
	{
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < hatPoint.childCount; i++)
		{
			list.Add(hatPoint.GetChild(i));
		}
		foreach (Transform item in list)
		{
			item.parent = null;
			UnityEngine.Object.Destroy(item.gameObject);
		}
	}

	public void UpdateCape(int int_0, Texture texture_0 = null)
	{
		RemoveCape();
		WearData wear = WearController.WearController_0.GetWear(int_0);
		if (wear == null || !wear.Boolean_2)
		{
			return;
		}
		GameObject gameObject = UserController.UserController_0.GetGameObject(int_0);
		if (gameObject == null)
		{
			Debug.LogWarning("capePrefab == null");
			return;
		}
		if (wear.Boolean_0)
		{
			CustomCapePicker component = gameObject.GetComponent<CustomCapePicker>();
			if (component != null)
			{
				component.artikulId = 0;
				component.shouldLoadTexture = false;
			}
		}
		GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject) as GameObject;
		DisableLightProbesRecursively(gameObject2);
		gameObject2.transform.parent = capePoint.transform;
		gameObject2.transform.localPosition = new Vector3(0f, -0.8f, 0f);
		gameObject2.transform.localRotation = Quaternion.identity;
		gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
		Player_move_c.SetLayerRecursively(gameObject2, base.gameObject.layer);
		if (texture_0 != null && wear.Boolean_0)
		{
			SkinsController.SetTextureRecursivelyFrom(gameObject2, texture_0);
		}
	}

	public void RemoveCape()
	{
		for (int i = 0; i < capePoint.transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(capePoint.transform.GetChild(i).gameObject);
		}
	}

	public void UpdateBoots(int int_0)
	{
		RemoveBoots();
		GameObject gameObject = UserController.UserController_0.GetGameObject(int_0);
		string value = gameObject.name;
		foreach (Transform item in bootsPoint.transform)
		{
			item.gameObject.SetActive(item.gameObject.name.Equals(value));
		}
	}

	public void RemoveBoots()
	{
		foreach (Transform item in bootsPoint.transform)
		{
			item.gameObject.SetActive(false);
		}
	}

	public void UpdateArmor(int int_0)
	{
		RemoveArmor();
		GameObject gameObject = UserController.UserController_0.GetGameObject(int_0);
		if (gameObject == null)
		{
			return;
		}
		GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject) as GameObject;
		DisableLightProbesRecursively(gameObject2);
		Transform transform = gameObject2.transform;
		ArmorRefs component = transform.GetChild(0).GetComponent<ArmorRefs>();
		if (component != null && gameObject_0 != null)
		{
			WeaponSounds component2 = gameObject_0.GetComponent<WeaponSounds>();
			if (component.leftBone != null && component2.Transform_0 != null)
			{
				component.leftBone.parent = component2.Transform_0;
				component.leftBone.localPosition = Vector3.zero;
				component.leftBone.localRotation = Quaternion.identity;
				component.leftBone.localScale = new Vector3(1f, 1f, 1f);
			}
			if (component.rightBone != null && component2.Transform_1 != null)
			{
				component.rightBone.parent = component2.Transform_1;
				component.rightBone.localPosition = Vector3.zero;
				component.rightBone.localRotation = Quaternion.identity;
				component.rightBone.localScale = new Vector3(1f, 1f, 1f);
			}
			transform.parent = armorPoint.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = new Vector3(1f, 1f, 1f);
			Player_move_c.SetLayerRecursively(gameObject2, base.gameObject.layer);
		}
	}

	public void RemoveArmor()
	{
		if (armorPoint.childCount <= 0)
		{
			return;
		}
		Transform child = armorPoint.GetChild(0);
		ArmorRefs component = child.GetChild(0).GetComponent<ArmorRefs>();
		if (component != null)
		{
			if (component.leftBone != null)
			{
				component.leftBone.parent = child.GetChild(0);
			}
			if (component.rightBone != null)
			{
				component.rightBone.parent = child.GetChild(0);
			}
			child.parent = null;
			UnityEngine.Object.Destroy(child.gameObject);
		}
	}

	private void PlayWeaponAnimation()
	{
		if (animationClip_0 != null)
		{
			Animation animation = gameObject_0.GetComponent<WeaponSounds>().GameObject_0.GetComponent<Animation>();
			if (Time.timeScale != 0f)
			{
				if (animation.GetClip("Profile") == null)
				{
					animation.AddClip(animationClip_0, "Profile");
				}
				else
				{
					Debug.LogWarning("Animation clip is null.");
				}
				animation.Play("Profile");
				return;
			}
			AnimationCoroutineRunner_0.StopAllCoroutines();
			if (animation.GetClip("Profile") == null)
			{
				animation.AddClip(animationClip_0, "Profile");
			}
			else
			{
				Debug.LogWarning("Animation clip is null.");
			}
			AnimationCoroutineRunner_0.StartPlay(animation, "Profile", false, null);
		}
		else
		{
			Debug.LogWarning("_profile == null");
		}
	}

	public static Texture2D GetClanLogo(string string_0)
	{
		if (string.IsNullOrEmpty(string_0))
		{
			return null;
		}
		byte[] data = Convert.FromBase64String(string_0);
		Texture2D texture2D = new Texture2D(Defs.int_16, Defs.int_17, TextureFormat.ARGB32, false);
		texture2D.LoadImage(data);
		texture2D.filterMode = FilterMode.Point;
		texture2D.Apply();
		return texture2D;
	}
}
