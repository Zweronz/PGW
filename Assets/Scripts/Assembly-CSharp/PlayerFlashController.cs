using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using engine.helpers;
using pixelgun.tutorial;

public class PlayerFlashController : MonoBehaviour
{
	private enum RespawnState
	{
		NotRespawn = 0,
		Respawn = 1
	}

	private Player_move_c player_move_c_0;

	private SkinName skinName_0;

	private Transform transform_0;

	private PlayerMechController playerMechController_0;

	private Texture texture_0;

	private bool bool_0 = true;

	private bool bool_1 = true;

	private bool bool_2 = true;

	private bool bool_3 = true;

	private Texture texture_1;

	private Texture texture_2;

	private Texture texture_3;

	private Texture texture_4;

	private bool bool_4;

	public GameObject currentBoots;

	private RespawnState respawnState_0;

	private float float_0;

	private Shader shader_0;

	private void Awake()
	{
		player_move_c_0 = base.gameObject.GetComponent<Player_move_c>();
		playerMechController_0 = base.gameObject.GetComponent<PlayerMechController>();
		transform_0 = player_move_c_0.myPlayerTransform;
		skinName_0 = transform_0.GetComponent<SkinName>();
		texture_0 = player_move_c_0.hitTexture;
		shader_0 = Shader.Find("Mobile/Diffuse-Color");
	}

	private void Update()
	{
		FlashPlayerRespawn();
	}

	public void SetMySkin()
	{
		Utility.SetTextureRecursiveFrom(transform_0.gameObject, player_move_c_0.Texture_0, GetStopObjFromPlayer());
	}

	public void FlashPlayer()
	{
		StartCoroutine(FlashHitPlayer());
	}

	public void NeedUpdateTextures()
	{
		bool_4 = true;
	}

	private void FlashPlayerRespawn()
	{
		if (respawnState_0 == RespawnState.NotRespawn)
		{
			if (player_move_c_0 == null || !player_move_c_0.Boolean_22)
			{
				return;
			}
			respawnState_0 = RespawnState.Respawn;
		}
		float b = 1f;
		if (player_move_c_0.Boolean_22)
		{
			float_0 += Time.deltaTime * 2f;
			b = Mathf.Max(0.4f, 1f - Mathf.Sin(float_0 * (float)Math.PI % (float)Math.PI));
		}
		else
		{
			respawnState_0 = RespawnState.NotRespawn;
			float_0 = 0f;
		}
		b = Mathf.Max(0.001f, b);
		player_move_c_0.playerBodyRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, b));
		WeaponSounds weaponSounds_ = player_move_c_0.WeaponSounds_0;
		if (weaponSounds_ != null && weaponSounds_.GameObject_1 != null && weaponSounds_.GameObject_1.transform.parent != null)
		{
			SkinnedMeshRenderer component = weaponSounds_.GameObject_1.transform.parent.GetComponent<SkinnedMeshRenderer>();
			if (component != null)
			{
				component.material.shader = shader_0;
				component.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, b));
			}
		}
		MeshRenderer componentInChildren = skinName_0.hatsPoint.GetComponentInChildren<MeshRenderer>();
		if (componentInChildren != null)
		{
			componentInChildren.material.shader = shader_0;
			componentInChildren.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, b));
		}
		MeshRenderer componentInChildren2 = skinName_0.capesPoint.GetComponentInChildren<MeshRenderer>();
		if (componentInChildren2 != null)
		{
			componentInChildren2.material.shader = shader_0;
			componentInChildren2.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, b));
		}
		SkinnedMeshRenderer componentInChildren3 = skinName_0.bootsPoint.GetComponentInChildren<SkinnedMeshRenderer>();
		if (componentInChildren3 != null)
		{
			componentInChildren3.material.shader = shader_0;
			componentInChildren3.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, b));
		}
		SkinnedMeshRenderer componentInChildren4 = skinName_0.armorPoint.GetComponentInChildren<SkinnedMeshRenderer>();
		if (componentInChildren4 != null)
		{
			componentInChildren4.material.shader = shader_0;
			componentInChildren4.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, b));
		}
	}

	private IEnumerator FlashHitPlayer()
	{
		Utility.SetTextureRecursiveFrom(transform_0.gameObject, texture_0, GetStopObjFromPlayer());
		playerMechController_0.mechBodyRenderer.material.SetColor("_ColorRili", new Color(1f, 0f, 0f, 1f));
		playerMechController_0.mechHandRenderer.material.SetColor("_ColorRili", new Color(1f, 0f, 0f, 1f));
		FlashWear();
		yield return new WaitForSeconds(0.125f);
		SetMySkin();
		playerMechController_0.mechBodyRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 1f));
		playerMechController_0.mechHandRenderer.material.SetColor("_ColorRili", new Color(1f, 1f, 1f, 1f));
		SetWear();
	}

	private void FlashWear()
	{
		if (bool_0)
		{
			if (texture_1 == null || bool_4)
			{
				texture_1 = GetTextureRecursiveFrom(skinName_0.armorPoint);
			}
			if (texture_1 != null)
			{
				Utility.SetTextureRecursiveFrom(skinName_0.armorPoint, texture_0, null, false);
			}
		}
		if (bool_1)
		{
			if (texture_2 == null || bool_4)
			{
				texture_2 = GetTextureRecursiveFrom(skinName_0.hatsPoint);
			}
			if (texture_2 != null)
			{
				Utility.SetTextureRecursiveFrom(skinName_0.hatsPoint, texture_0, null, false);
			}
		}
		if (bool_2)
		{
			if (texture_3 == null || bool_4)
			{
				texture_3 = GetTextureRecursiveFrom(currentBoots);
			}
			if (texture_3 != null && (bool)currentBoots.GetComponent<Renderer>() && (bool)currentBoots.GetComponent<Renderer>().material)
			{
				currentBoots.GetComponent<Renderer>().material.mainTexture = texture_0;
			}
		}
		if (bool_3)
		{
			if (texture_4 == null || bool_4)
			{
				texture_4 = GetTextureRecursiveFrom(skinName_0.capesPoint);
			}
			if (texture_4 != null)
			{
				Utility.SetTextureRecursiveFrom(skinName_0.capesPoint, texture_0, null, false);
			}
		}
		bool_4 = false;
	}

	private void SetWear()
	{
		if (bool_0 && texture_1 != null)
		{
			Utility.SetTextureRecursiveFrom(skinName_0.armorPoint, texture_1, null, false);
		}
		if (bool_1 && texture_2 != null)
		{
			Utility.SetTextureRecursiveFrom(skinName_0.hatsPoint, texture_2, null, false);
		}
		if (bool_2 && texture_3 != null && (bool)currentBoots.GetComponent<Renderer>() && (bool)currentBoots.GetComponent<Renderer>().material)
		{
			currentBoots.GetComponent<Renderer>().material.mainTexture = texture_3;
		}
		if (bool_3 && texture_4 != null)
		{
			Utility.SetTextureRecursiveFrom(skinName_0.capesPoint, texture_4, null, false);
		}
	}

	private GameObject[] GetStopObjFromPlayer()
	{
		List<GameObject> list = new List<GameObject>();
		Transform transform = player_move_c_0.gameObject.transform;
		if (transform.childCount > 0)
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				WeaponSounds component = child.gameObject.GetComponent<WeaponSounds>();
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
				if (component.GameObject_0 != null && component.GameObject_0.GetComponent<InnerWeaponPars>() != null && component.GameObject_0.GetComponent<InnerWeaponPars>().particlePoint != null)
				{
					list.Add(component.GameObject_0.GetComponent<InnerWeaponPars>().particlePoint);
				}
				GameObject gameObject_ = component.GameObject_1;
				if (gameObject_ != null)
				{
					list.Add(gameObject_);
				}
				Transform transform2 = child.Find("BulletSpawnPoint");
				if (transform2 != null)
				{
					list.Add(transform2.gameObject);
				}
				if (component.GameObject_2 != null)
				{
					list.Add(component.GameObject_2);
				}
			}
		}
		list.Add(skinName_0.capesPoint);
		list.Add(skinName_0.hatsPoint);
		list.Add(skinName_0.bootsPoint);
		list.Add(currentBoots);
		list.Add(skinName_0.armorPoint);
		list.Add(player_move_c_0.flagPoint);
		list.Add(player_move_c_0.invisibleParticle);
		list.Add(player_move_c_0.jetPackPoint);
		list.Add(player_move_c_0.jetPackPointMech);
		list.Add(player_move_c_0.PlayerTurretController_0.turretPoint);
		list.Add(player_move_c_0.PlayerMechController_0.mechPoint);
		list.Add(player_move_c_0.PlayerMechController_0.mechExplossion);
		list.Add(player_move_c_0.particleBonusesPoint);
		if (TutorialController.TutorialController_0.Boolean_0)
		{
			Transform transform3 = transform_0.Find("Main Camera");
			if (transform3 != null)
			{
				Transform transform4 = transform3.Find("ArrowNavigate(Clone)");
				if (transform4 != null)
				{
					list.Add(transform4.gameObject);
				}
			}
		}
		return list.ToArray();
	}

	private Texture GetTextureRecursiveFrom(GameObject gameObject_0)
	{
		if (gameObject_0 == null)
		{
			return null;
		}
		if (!gameObject_0.activeSelf)
		{
			return null;
		}
		Transform transform = gameObject_0.transform;
		int childCount = gameObject_0.transform.childCount;
		int num = 0;
		Texture textureRecursiveFrom;
		while (true)
		{
			if (num < childCount)
			{
				Transform child = transform.GetChild(num);
				if (child.gameObject.activeSelf)
				{
					if ((bool)child.gameObject.GetComponent<Renderer>() && (bool)child.gameObject.GetComponent<Renderer>().material)
					{
						return child.gameObject.GetComponent<Renderer>().material.mainTexture;
					}
					textureRecursiveFrom = GetTextureRecursiveFrom(child.gameObject);
					if (textureRecursiveFrom != null)
					{
						break;
					}
				}
				num++;
				continue;
			}
			if (childCount == 0 && (bool)gameObject_0.GetComponent<Renderer>() && (bool)gameObject_0.GetComponent<Renderer>().material)
			{
				return gameObject_0.GetComponent<Renderer>().material.mainTexture;
			}
			return null;
		}
		return textureRecursiveFrom;
	}
}
