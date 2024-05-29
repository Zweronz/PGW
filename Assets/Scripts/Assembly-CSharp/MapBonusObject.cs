using System.Collections.Generic;
using Photon;
using UnityEngine;
using engine.events;
using engine.network;
using engine.unity;

public class MapBonusObject : Photon.MonoBehaviour
{
	public int int_0;

	public string string_0;

	public bool bool_0;

	private MapBonusItemData mapBonusItemData_0;

	private GameObject gameObject_0;

	private Transform transform_0;

	private void Awake()
	{
		transform_0 = base.gameObject.transform;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (CheckItemPickup())
		{
			ItemPickup();
			DestroyItem();
		}
	}

	private void OnDestroy()
	{
		BonusMapController bonusMapController_ = BonusMapController.bonusMapController_0;
		if (bonusMapController_ != null && !string.IsNullOrEmpty(string_0))
		{
			bonusMapController_.DeleteObjectFromPoint(string_0);
		}
	}

	public void Init(int int_1, string string_1)
	{
		init(int_1, string_1);
		base.PhotonView_0.RPC("InitMapBonusObject", PhotonTargets.OthersBuffered, int_1, string_1);
	}

	private void init(int int_1, string string_1)
	{
		int_0 = int_1;
		mapBonusItemData_0 = MapBonusItemStorage.Get.Storage.GetObjectByKey(int_1);
		string_0 = string_1;
		bool_0 = true;
		string path = mapBonusItemData_0.String_0;
		GameObject gameObject = Resources.Load(path) as GameObject;
		if (gameObject != null)
		{
			gameObject_0 = (GameObject)Object.Instantiate(gameObject);
			if (gameObject_0 != null)
			{
				gameObject_0.transform.parent = base.gameObject.transform;
				gameObject_0.transform.localPosition = new Vector3(0f, 0f, 0f);
			}
		}
		MapPointBonusController point = BonusMapController.bonusMapController_0.GetPoint(string_1);
		if (point != null)
		{
			point.SetMapBonusObject(this);
		}
	}

	[RPC]
	public void InitMapBonusObject(int int_1, string string_1)
	{
		init(int_1, string_1);
	}

	public void DestroyItem()
	{
		if (PhotonNetwork.Boolean_9)
		{
			PhotonNetwork.Destroy(base.gameObject);
			return;
		}
		MoveItemInBlackHole();
		base.PhotonView_0.RPC("DestroyItemRPC", PhotonTargets.All);
	}

	private bool CheckItemPickup()
	{
		Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
		if (!(myPlayerMoveC == null) && myPlayerMoveC.PlayerParametersController_0.Single_2 > 0f)
		{
			float num = Vector3.SqrMagnitude(transform_0.position - myPlayerMoveC.myTransform.position);
			return num < 4f;
		}
		return false;
	}

	private void ItemPickup()
	{
		if (mapBonusItemData_0 == null || mapBonusItemData_0.Dictionary_0 == null || mapBonusItemData_0.Dictionary_0.Count == 0)
		{
			return;
		}
		foreach (KeyValuePair<SkillId, SkillData> item in mapBonusItemData_0.Dictionary_0)
		{
			useMapBonusSkill(item.Key, item.Value);
		}
		ShowEffect();
	}

	private void ShowEffect()
	{
		MapBonusAnimController component = gameObject_0.GetComponent<MapBonusAnimController>();
		Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
		if (component != null && Defs.Boolean_0 && component.ItemUpSound != null)
		{
			myPlayerMoveC.gameObject.GetComponent<AudioSource>().PlayOneShot(component.ItemUpSound);
		}
		SetActiveEffect();
		base.PhotonView_0.RPC("SetActiveEffectRPC", PhotonTargets.Others);
	}

	private void SetActiveEffect()
	{
		if (gameObject_0 != null)
		{
			MapBonusAnimController component = gameObject_0.GetComponent<MapBonusAnimController>();
			if (component != null)
			{
				component.ItemPickup();
			}
		}
	}

	private void useMapBonusSkill(SkillId skillId_0, SkillData skillData_0)
	{
		switch (skillId_0)
		{
		case SkillId.SKILL_MAP_BONUS_HEALTH:
			changeHealth(skillData_0);
			break;
		case SkillId.SKILL_MAP_BONUS_ARMOR:
			addArmor(skillData_0);
			break;
		case SkillId.SKILL_MAP_BONUS_AMMO:
			addAmmo(skillData_0);
			break;
		case SkillId.SKILL_MAP_BONUS_GRENADES:
			addGrenade(skillData_0);
			break;
		case SkillId.SKILL_MAP_BONUS_EXPLOSION_DAMAGE:
		{
			float single_ = skillData_0.Single_0;
			float float_ = 7f;
			foreach (KeyValuePair<SkillId, SkillData> item in mapBonusItemData_0.Dictionary_0)
			{
				if (item.Key == SkillId.SKILL_EXPLOSION_RADIUS_MODIFIER)
				{
					float_ = item.Value.Single_0;
				}
			}
			addExplosion(single_, float_);
			break;
		}
		case SkillId.SKILL_MAP_BONUS_WEAPON:
			addWeapon(skillData_0);
			break;
		}
	}

	private void changeHealth(SkillData skillData_0)
	{
		Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
		if (!(myPlayerMoveC == null))
		{
			float num = myPlayerMoveC.PlayerParametersController_0.Single_0 * skillData_0.Single_0 / 100f;
			if (num > 0f)
			{
				addHealth(num, myPlayerMoveC);
			}
			else if (num < 0f)
			{
				removeHealth(0f - num, myPlayerMoveC);
			}
			base.PhotonView_0.RPC("OnHealthBonusPickedUpRPC", PhotonTargets.All, (int)myPlayerMoveC.PhotonView_0.PhotonPlayer_0.Hashtable_0["uid"], num);
		}
	}

	private void addHealth(float float_0, Player_move_c player_move_c_0)
	{
		if (float_0 + player_move_c_0.PlayerParametersController_0.Single_2 > player_move_c_0.PlayerParametersController_0.Single_0)
		{
			float_0 = player_move_c_0.PlayerParametersController_0.Single_0 - player_move_c_0.PlayerParametersController_0.Single_2;
		}
		player_move_c_0.PlayerParametersController_0.AddHealth(float_0);
		player_move_c_0.PlayerParticleController_0.PlayAnimation(PlayerParticleController.AnimationType.HEALTH, false, 2f);
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			if (float_0 > 0f)
			{
				HeadUpDisplay.HeadUpDisplay_0.ShowAddBonusHealth(Mathf.CeilToInt(float_0));
			}
			else
			{
				HeadUpDisplay.HeadUpDisplay_0.ShowMaxBonusHealth();
			}
		}
	}

	private void removeHealth(float float_0, Player_move_c player_move_c_0)
	{
		if (player_move_c_0.PlayerParametersController_0.Single_2 > float_0)
		{
			player_move_c_0.PlayerParametersController_0.AddHealth(0f - float_0);
			if (HeadUpDisplay.HeadUpDisplay_0 != null)
			{
				HeadUpDisplay.HeadUpDisplay_0.ShowRemoveBonusHealth(Mathf.CeilToInt(float_0));
			}
		}
		else
		{
			if (HeadUpDisplay.HeadUpDisplay_0 != null)
			{
				HeadUpDisplay.HeadUpDisplay_0.ShowRemoveBonusHealth(Mathf.CeilToInt(player_move_c_0.PlayerParametersController_0.Single_2));
			}
			player_move_c_0.ForceSuicide();
		}
	}

	private void addArmor(SkillData skillData_0)
	{
		Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
		if (myPlayerMoveC == null)
		{
			return;
		}
		int num = Mathf.CeilToInt(myPlayerMoveC.PlayerParametersController_0.AddArmor(skillData_0.Int32_0));
		myPlayerMoveC.PlayerParticleController_0.PlayAnimation(PlayerParticleController.AnimationType.ARMOR, false, 2f);
		base.PhotonView_0.RPC("OnArmorBonusPickedUpRPC", PhotonTargets.All, (int)myPlayerMoveC.PhotonView_0.PhotonPlayer_0.Hashtable_0["uid"], num);
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			if (num > 0)
			{
				HeadUpDisplay.HeadUpDisplay_0.ShowAddBonusArmor(num);
			}
			else if (num < 0)
			{
				num = ((!(myPlayerMoveC.PlayerParametersController_0.Single_7 + (float)num < 0f)) ? (-num) : Mathf.CeilToInt(myPlayerMoveC.PlayerParametersController_0.Single_7));
				HeadUpDisplay.HeadUpDisplay_0.ShowRemoveBonusArmor(num);
			}
			else
			{
				HeadUpDisplay.HeadUpDisplay_0.ShowMaxBonusArmor();
			}
		}
	}

	private void addAmmo(SkillData skillData_0)
	{
		WeaponManager weaponManager_ = WeaponManager.weaponManager_0;
		Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
		if (myPlayerMoveC == null)
		{
			return;
		}
		int num = weaponManager_.AddAmmo();
		myPlayerMoveC.PlayerParticleController_0.PlayAnimation(PlayerParticleController.AnimationType.AMMO, false, 2f);
		base.PhotonView_0.RPC("OnAmmoBonusPickedUpRPC", PhotonTargets.All, (int)myPlayerMoveC.PhotonView_0.PhotonPlayer_0.Hashtable_0["uid"], weaponManager_.WeaponSounds_0.WeaponData_0.Int32_0, num);
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			if (num > 0)
			{
				HeadUpDisplay.HeadUpDisplay_0.ShowAddBonusAmmo(num);
			}
			else
			{
				HeadUpDisplay.HeadUpDisplay_0.ShowMaxBonusAmmo();
			}
		}
	}

	private void addGrenade(SkillData skillData_0)
	{
		Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
		if (myPlayerMoveC == null)
		{
			return;
		}
		if (ConsumablesController.ConsumablesController_0.IsConsumableMaxCountForSlot(SlotType.SLOT_CONSUM_GRENADE))
		{
			if (HeadUpDisplay.HeadUpDisplay_0 != null)
			{
				HeadUpDisplay.HeadUpDisplay_0.ShowMaxBonusGrenade();
			}
			return;
		}
		myPlayerMoveC.PlayerParticleController_0.PlayAnimation(PlayerParticleController.AnimationType.GRENADE, false, 2f);
		base.PhotonView_0.RPC("OnGrenadeBonusPickedUpRPC", PhotonTargets.All, (int)myPlayerMoveC.PhotonView_0.PhotonPlayer_0.Hashtable_0["uid"], skillData_0.Int32_0);
		if (HeadUpDisplay.HeadUpDisplay_0 != null)
		{
			HeadUpDisplay.HeadUpDisplay_0.ShowAddBonusGrenade(1);
		}
	}

	private void addExplosion(float float_0, float float_1)
	{
		Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
		if (!(myPlayerMoveC == null))
		{
			HitStruct hitStruct = HitStruct.GenerateHitStruct(float_1, float_0);
			if (WeaponManager.weaponManager_0.GetWeaponFromCurrentSlot() != null)
			{
				hitStruct.int_0 = WeaponManager.weaponManager_0.GetWeaponFromCurrentSlot().WeaponSounds_0.WeaponData_0.Int32_0;
			}
			else if (!WeaponManager.weaponManager_0.myPlayerMoveC.PlayerGrenadeController_0.Boolean_0)
			{
				hitStruct.int_0 = WeaponManager.weaponManager_0.GetWeaponFromSlot(WeaponManager.weaponManager_0.myPlayerMoveC.SlotType_0).WeaponSounds_0.WeaponData_0.Int32_0;
			}
			PlayerDamageController.PlayerDamageController_0.RadiusShoot(hitStruct, transform_0.position);
			base.PhotonView_0.RPC("OnExplosionBonusPickedUpRPC", PhotonTargets.All, (int)myPlayerMoveC.PhotonView_0.PhotonPlayer_0.Hashtable_0["uid"], 1);
		}
	}

	private void addWeapon(SkillData skillData_0)
	{
		Player_move_c myPlayerMoveC = WeaponManager.weaponManager_0.myPlayerMoveC;
		if (!(myPlayerMoveC == null))
		{
			WeaponData weapon = WeaponController.WeaponController_0.GetWeapon(skillData_0.Int32_0);
			if (weapon != null)
			{
				PickedupArtikulNetworkCommand pickedupArtikulNetworkCommand = new PickedupArtikulNetworkCommand();
				pickedupArtikulNetworkCommand.int_1 = weapon.Int32_0;
				AbstractNetworkCommand.Send(pickedupArtikulNetworkCommand);
				UserController.UserController_0.EquipArtikul(weapon.Int32_0);
				myPlayerMoveC.AddWeapon(weapon.Int32_0);
				DependSceneEvent<EventTakenWeaponBonus, int>.GlobalDispatch(weapon.Int32_0);
			}
		}
	}

	private void MoveItemInBlackHole()
	{
		base.transform.position = new Vector3(0f, -30000f, 0f);
	}

	[RPC]
	public void SetActiveEffectRPC()
	{
		SetActiveEffect();
	}

	[RPC]
	public void DestroyItemRPC()
	{
		if (PhotonNetwork.Boolean_9)
		{
			DestroyItem();
		}
		else
		{
			MoveItemInBlackHole();
		}
	}

	[RPC]
	public void OnHealthBonusPickedUpRPC(int int_1, float float_0)
	{
		MonoSingleton<FightController>.Prop_0.FightStatController_0.OnPickedUpHealth(int_1, float_0);
	}

	[RPC]
	public void OnArmorBonusPickedUpRPC(int int_1, int int_2)
	{
		MonoSingleton<FightController>.Prop_0.FightStatController_0.OnPickedUpArmor(int_1, int_2);
	}

	[RPC]
	public void OnAmmoBonusPickedUpRPC(int int_1, int int_2, int int_3)
	{
		MonoSingleton<FightController>.Prop_0.FightStatController_0.OnPickedUpAmmo(int_1, int_2, int_3);
	}

	[RPC]
	public void OnGrenadeBonusPickedUpRPC(int int_1, int int_2)
	{
		MonoSingleton<FightController>.Prop_0.FightStatController_0.OnPickedUpGrenade(int_1, int_2);
	}

	[RPC]
	public void OnExplosionBonusPickedUpRPC(int int_1, int int_2)
	{
		MonoSingleton<FightController>.Prop_0.FightStatController_0.OnPickedUpExplosion(int_1, int_2);
	}
}
